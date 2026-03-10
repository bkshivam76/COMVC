Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class DataRestriction
        Inherits SharedVariables
        ''' <summary>
        ''' These are Types of possible Data Restrictions that can be put in Connectone 
        ''' </summary>
        Public Enum LockType
            LOGIN_BLOCKED
            MAG_DATE_BLOCKED 'Dont give on screen
            PROFILE_BLOCKED
            READ_ALL_WRITE_BLOCKED_ACCOUNTS_SUBMITTED 'Dont give on screen
            READ_ALL_WRITE_BLOCKED_FOR_SOME_DURATION
            READ_ALL_WRITE_BLOCKED_FOR_SOME_DURATION_ALL_USERS 'Dont give on screen
        End Enum
        <Serializable>
        Public Class Parameter_Insert_DataRestriction
            Public RestrictedID As String
            Public RestrictionOn As String
            Public PeriodFrom As DateTime?
            Public PeriodTo As DateTime?
            Public LockType As LockType
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Parameter_Update_DataRestriction
            Public RestrictedID As String
            Public RestrictionOn As String
            Public PeriodFrom As DateTime?
            Public PeriodTo As DateTime?
            Public LockType As LockType
            Public RECID As Int32
            Public Remarks As String
        End Class
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        <Serializable>
        Public Class LedgerList
            Public Property ID As String
            Public Property Name As String
            Public Property Type As String
            Public Property Sub_Led_ID As String
        End Class
        Public Function DataRestriction_GetList() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CENID", "@YEARID", "@UserID"}
            Dim values() As Object = {cBase._open_Cen_ID, cBase._open_Year_ID, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {8, 8, 255}
            Return _RealService.ListFromSP(Tables.SO_CLIENT_RESTRICTIONS, "[sp_get_Data_Restrictions]", Tables.SO_CLIENT_RESTRICTIONS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Auditor_DataRestriction))
        End Function
        Public Function GetRecord(ByVal ID As Int32) As DataTable
            Return GetRecordByCustom("WHERE ID=" & ID, ClientScreen.Auditor_DataRestriction, Tables.SO_CLIENT_RESTRICTIONS)
        End Function
        Public Function GetLedgerList() As List(Of LedgerList)
            Dim _Ledgers_Table As DataTable = GetLedgers(ClientScreen.Auditor_DataRestriction)
            Dim _LedgerList As List(Of LedgerList) = New List(Of LedgerList)
            If (Not (_Ledgers_Table) Is Nothing) Then
                For Each row As DataRow In _Ledgers_Table.Rows
                    Dim newdata = New LedgerList
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Sub_Led_ID = row.Field(Of String)("Sub_Led_ID")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Type = row.Field(Of String)("Type")
                    _LedgerList.Add(newdata)
                Next
            End If
            Return _LedgerList
        End Function
        Public Function InsertDataRestrictions(ByVal InParam As Parameter_Insert_DataRestriction) As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_Insert_Data_Restrictions"
            Dim params() As String = {"@CENID", "@YEARID", "@UserID", "@Restriction_Type", "@Restriction_Ref_ID", "@FromDate", "@ToDate", "@LockType", "@Remarks"}
            Dim values() As Object = {cBase._open_Cen_ID, cBase._open_Year_ID, cBase._open_User_ID, InParam.RestrictionOn, InParam.RestrictedID, InParam.PeriodFrom, InParam.PeriodTo, InParam.LockType.ToString(), InParam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255, 50, 36, 12, 12, 50, 8000}
            'used public insert function as there are no transactional data involved 
            Return _RealService.ListFromSP(Tables.SO_CLIENT_RESTRICTIONS, SPName, Tables.SO_CLIENT_RESTRICTIONS.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Auditor_DataRestriction)).Rows(0)(0)
        End Function
        Public Function UpdateDataRestrictions(ByVal InParam As Parameter_Update_DataRestriction) As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_Update_Data_Restrictions"
            Dim params() As String = {"@ID", "@UserID", "@Restriction_Type", "@Restriction_Ref_ID", "@FromDate", "@ToDate", "@LockType", "@Remarks"}
            Dim values() As Object = {InParam.RECID, cBase._open_User_ID, InParam.RestrictionOn, InParam.RestrictedID, InParam.PeriodFrom, InParam.PeriodTo, InParam.LockType.ToString(), InParam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 255, 36, 12, 12, 50, 8000}
            'used public update function as there are no transactional data involved 
            Return _RealService.ListFromSP(Tables.SO_CLIENT_RESTRICTIONS, SPName, Tables.SO_CLIENT_RESTRICTIONS.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Auditor_DataRestriction)).Rows(0)(0)
        End Function
        Public Function Delete(ByVal ID As Int32) As Boolean
            Return DeleteByCondition("ID  = " + ID.ToString(), Tables.SO_CLIENT_RESTRICTIONS, ClientScreen.Auditor_DataRestriction)
        End Function
    End Class
End Class