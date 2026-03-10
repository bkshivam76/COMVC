Imports System.Data

Namespace Real
#Region "--Profile--"
    <Serializable>
    Public Class ServicePlaces
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_ServicePlaces
            Public PlaceType As String
            Public Name As String
            Public StartDate As String
            Public PlaceAtABID As String
            Public Weekdays As String
            Public Timing As String
            Public ResponsiblePersonABID As String
            Public OtherDetails As String
            Public Status As String
            Public Status_Action As String
            Public Max_Capacity As Int32
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_ServicePlaces
            Public PlaceType As String
            Public Name As String
            Public StartDate As String
            Public PlaceAtABID As String
            Public Weekdays As String
            Public Timing As String
            Public ResponsiblePersonABID As String
            Public OtherDetails As String
            Public Status As String
            Public Max_Capacity As Int32
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_InsertServicePlaces
            Public param_Insert As Parameter_Insert_ServicePlaces
            Public param_InsertAssetLoc As AssetLocations.Param_AssetLoc_Insert = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_UpdateServicePlaces
            Public param_Update As Parameter_Update_ServicePlaces
            Public param_UpdateByReference As AssetLocations.Param_AssetLoc_Update = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_DeleteServicePlaces
            Public RecID_DelAssetLoc As String = Nothing
            Public RecID_Delete As String = Nothing
        End Class
#End Region
        ''' <summary>
        ''' GetList
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServicePlaces_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_ServicePlace_Listing"
            Dim params() As String = {"CEN_ID", "YEARID", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, DbType.String}
            Dim lengths() As Integer = {5, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SERVICE_PLACE_INFO, SPName, ConnectOneWS.Tables.SERVICE_PLACE_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' Returns SP Record
        Public Shared Function GetRecord(inBasicParam As ConnectOneWS.Basic_Param, sp_RecID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT SP_CEN_ID,SP_SERVICE_PLACE_TYPE,SP_STARTDATE,(select	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = SP_PLACEAT_AB_ID) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") SP_PLACEAT_AB_ID " &
                                        ",SP_WEEKDAYS,SP_TIMING,(select	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = SP_RESPONSIBLE_PERSON_AB_ID) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") SP_RESPONSIBLE_PERSON_AB_ID " &
                                        ",SP_OTHER_DETAIL,SP_STATUS,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,SP_SERVICE_PLACE_NAME,SP_COD_YEAR_ID, SP_MAXCAPACITY " &
                                        " FROM service_place_info" &
                                        " Where REC_ID = '" & sp_RecID & "' "
            Return dbService.List(ConnectOneWS.Tables.SERVICE_PLACE_INFO, onlineQuery, ConnectOneWS.Tables.SERVICE_PLACE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetAllServicePlaceList(ByVal BK_PAD_NO As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT I.INS_SHORT AS 'Institute',C.CEN_NAME AS 'Centre No.',C.CEN_UID AS 'UID',C.CEN_PAD_NO AS 'No.',SP.SP_SERVICE_PLACE_NAME AS 'Service Place Name',SP.SP_SERVICE_PLACE_TYPE AS 'Place Type', SP.REC_ID AS 'SP_ID',SP.REC_EDIT_ON " & _
                                        " FROM Service_place_info as SP  " & _
                                        " INNER JOIN Centre_info AS C ON (SP.SP_CEN_ID=C.CEN_ID) " & _
                                        " INNER JOIN Institute_info AS I ON (C.CEN_INS_ID=I.INS_ID) " & _
                                        " Where SP.REC_STATUS IN (0,1,2) AND C.CEN_BK_PAD_NO='" & BK_PAD_NO & "' " & _
                                        " and SP_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( SP_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR SP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = SP_CEN_ID))  AND (SP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = SP_CEN_ID) OR SP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " ) ; " & _
                                        " "
            Return dbService.List(ConnectOneWS.Tables.SERVICE_PLACE_INFO, onlineQuery, ConnectOneWS.Tables.SERVICE_PLACE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Count By Place Name
        ''' </summary>
        ''' <param name="Name"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServicePlaces_GetCountByPlaceName</remarks>
        Public Shared Function GetCountByPlaceName(ByVal Name As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM SERVICE_PLACE_INFO WHERE  REC_STATUS IN (0,1,2) AND UPPER(SP_SERVICE_PLACE_NAME)='" & Trim(UCase(Name)) & "' AND  SP_CEN_ID =" & inBasicParam.openCenID.ToString & " and SP_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( SP_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR SP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = SP_CEN_ID)) AND (SP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = SP_CEN_ID) OR SP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " )"
            Return dbService.GetScalar(ConnectOneWS.Tables.SERVICE_PLACE_INFO, OnlineQuery, ConnectOneWS.Tables.SERVICE_PLACE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServicePlaces_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_ServicePlaces, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.PlaceAtABID.Trim.Length = 0 Then InParam.PlaceAtABID = "NULL" Else InParam.PlaceAtABID = "'" & InParam.PlaceAtABID & "'"
            If InParam.ResponsiblePersonABID.Trim.Length = 0 Then InParam.ResponsiblePersonABID = "NULL" Else InParam.ResponsiblePersonABID = "'" & InParam.ResponsiblePersonABID & "'"
            Dim OnlineQuery As String = "INSERT INTO Service_Place_Info(SP_CEN_ID, SP_SERVICE_PLACE_TYPE, SP_SERVICE_PLACE_NAME,SP_STARTDATE, SP_PLACEAT_AB_ID, SP_WEEKDAYS, SP_TIMING, SP_RESPONSIBLE_PERSON_AB_ID, SP_OTHER_DETAIL, SP_STATUS,SP_COD_YEAR_ID, " &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID, SP_ORG_REC_ID, SP_MAXCAPACITY" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "'" & InParam.PlaceType & "', " &
                                                  " '" & InParam.Name & "' , " &
                                                  " " & If(IsDate(InParam.StartDate), "'" & Convert.ToDateTime(InParam.StartDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  " " & InParam.PlaceAtABID & " , " &
                                                  "'" & InParam.Weekdays & "', " &
                                                  " '" & InParam.Timing & "' , " &
                                                  " " & InParam.ResponsiblePersonABID & " , " &
                                                  "'" & InParam.OtherDetails & "', " &
                                                  "'" & InParam.Status & "', " &
                                                  "" & inBasicParam.openYearID.ToString & ", " &
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ", '" & InParam.RecID & "', " & InParam.Max_Capacity & ")"

            dbService.Insert(ConnectOneWS.Tables.SERVICE_PLACE_INFO, OnlineQuery, inBasicParam, InParam.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServicePlaces_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_ServicePlaces, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.PlaceAtABID.Trim.Length = 0 Then UpParam.PlaceAtABID = "NULL" Else UpParam.PlaceAtABID = "'" & UpParam.PlaceAtABID & "'"
            If UpParam.ResponsiblePersonABID.Trim.Length = 0 Then UpParam.ResponsiblePersonABID = "NULL" Else UpParam.ResponsiblePersonABID = "'" & UpParam.ResponsiblePersonABID & "'"
            Dim OnlineQuery As String = " UPDATE Service_Place_Info SET " &
                                       "SP_SERVICE_PLACE_TYPE ='" & UpParam.PlaceType & "', " &
                                       "SP_SERVICE_PLACE_NAME ='" & UpParam.Name & "', " &
                                       "SP_STARTDATE          =" & If(IsDate(UpParam.StartDate), "'" & Convert.ToDateTime(UpParam.StartDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                       "SP_PLACEAT_AB_ID      = " & UpParam.PlaceAtABID & "  ," &
                                       "SP_WEEKDAYS           ='" & UpParam.Weekdays & "', " &
                                       "SP_TIMING             ='" & UpParam.Timing & "', " &
                                       "SP_RESPONSIBLE_PERSON_AB_ID     = " & UpParam.ResponsiblePersonABID & " , " &
                                       "SP_OTHER_DETAIL     ='" & UpParam.OtherDetails & "', " &
                                       "SP_STATUS      ='" & UpParam.Status & "', " &
                                       "SP_MAXCAPACITY      =" & UpParam.Max_Capacity & ", " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.SERVICE_PLACE_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function InsertServicePlaces_Txn(inParam As Param_Txn_InsertServicePlaces, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_Insert Is Nothing Then
                If Not Insert(inParam.param_Insert, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertAssetLoc Is Nothing Then
                If Not AssetLocations.Insert_AllSisterUIDs(inParam.param_InsertAssetLoc, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            '   End Using
            '  txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function UpdateServicePlaces_Txn(upParam As Param_Txn_UpdateServicePlaces, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_Update Is Nothing Then
                If Not Update(upParam.param_Update, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'If Not upParam.param_UpdateByReference Is Nothing Then
            '    If Not AssetLocations.UpdateByReference(upParam.param_UpdateByReference, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            'End If 'Commented as locations shall not be edited on update of Godly Service Places 
            'End Using
            '  txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function DeleteServicePlaces_Txn(delParam As Param_Txn_DeleteServicePlaces, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.RecID_DelAssetLoc Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_LOCATION_INFO, " SP_REC_ID = '" & delParam.RecID_DelAssetLoc & "' ", inBasicParam)
            End If
            If Not delParam.RecID_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.SERVICE_PLACE_INFO, delParam.RecID_DelAssetLoc, inBasicParam)
            End If
            '   End Using
            ' txn.Complete()
            ' End Using
            Return True
        End Function

    End Class
#End Region
End Namespace

