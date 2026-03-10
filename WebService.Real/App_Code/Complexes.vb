Imports System.Data

Namespace Real
    <Serializable>
    Public Class Complexes

#Region "Param Classes"
        <Serializable>
        Public Class Param_Insert_Complexes
            Public ComplexName As String
            Public CM_Add1 As String
            Public CM_Add2 As String
            Public CM_Add3 As String
            Public CM_Add4 As String
            Public CM_CityID As String
            Public CM_DistrictID As String
            Public CM_StateID As String
            Public CM_Pincode As String
            Public CM_CountryID As String
            Public CM_Remarks As String
            Public RecID As String
            Public Status_Action As String
            Public YearID As Integer = Nothing
        End Class
        <Serializable>
        Public Class Param_InsertBuilding_Complexes
            Public LB_ID As String
            Public ComplexID As String
            Public YearID As Integer
            Public RecID As String
            Public Status_Action As String
        End Class
        <Serializable>
        Public Class Param_Update_Complexes
            Public ComplexName As String
            Public CM_Add1 As String
            Public CM_Add2 As String
            Public CM_Add3 As String
            Public CM_Add4 As String
            Public CM_CityID As String
            Public CM_DistrictID As String
            Public CM_StateID As String
            Public CM_Pincode As String
            Public CM_CountryID As String
            Public CM_Remarks As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_GetBuildingList_Complexes
            Public Cen_BK_PAD_NO As String
            Public ComplexID As String
            Public Prev_Year_ID As Integer = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_Complexes
            Public Insert_Complexes As Param_Insert_Complexes
            Public InsertBuildings() As Param_InsertBuilding_Complexes
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_Complexes
            Public RecID_Delete As String = Nothing
            Public RecID_DeleteBuilding As String = Nothing
        End Class
        <Serializable>
        Public Class Param_Update_ManageBuildings_Txn
            Public RecID_DeleteBuilding As String = Nothing
            Public InsertBuildings() As Param_InsertBuilding_Complexes
        End Class
        <Serializable>
        Public Class Param_GetRecordCountByName_Complexes
            Public Name As String
            Public ID As String = Nothing
        End Class
        <Serializable>
        Public Class Param_Get_LB_Listing_Profile
            Public Prev_Year_ID As Integer = Nothing
            Public Cen_BK_PAD_NO As String
        End Class
        <Serializable>
        Public Class Param_IsPropertyAlreadyMapped
            Public PropertyID As String
            Public ComplexID As String = ""
            Public IsManageBuildings As Boolean = False
        End Class
#End Region
        Public Shared Function GetList(Param As Param_Get_LB_Listing_Profile, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Complexes_Profile"
            Dim params() As String = {"CEN_BK_PADNO", "CEN_ID", "YEARID", "PREV_YEARID"}
            'If Param.Prev_Year_ID = "" Then Param.Prev_Year_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {Param.Cen_BK_PAD_NO, inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 5, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.COMPLEX_INFO, SPName, ConnectOneWS.Tables.COMPLEX_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetInstList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_INS_Complexes"
            Dim params() As String = {"CENID", "YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.COMPLEX_INFO, SPName, ConnectOneWS.Tables.COMPLEX_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetBuildingList(Param As Param_GetBuildingList_Complexes, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_ComplexBuilding_Listing"
            Dim params() As String = {"CEN_BK_PADNO", "Complex_ID", "YEARID", "PREV_YEARID"}
            'If Param.Prev_Year_ID = "" Then Param.Prev_Year_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {Param.Cen_BK_PAD_NO, Param.ComplexID, inBasicParam.openYearID, Param.Prev_Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 36, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, SPName, ConnectOneWS.Tables.COMPLEX_BUILDING_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetAllComplexBuildings(Param As Param_Get_LB_Listing_Profile, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Complex_Buildings_All"
            Dim params() As String = {"CEN_BK_PADNO", "YEARID", "PREV_YEARID"}
            'If Param.Prev_Year_ID = "" Then Param.Prev_Year_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {Param.Cen_BK_PAD_NO, inBasicParam.openYearID, Param.Prev_Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, SPName, ConnectOneWS.Tables.COMPLEX_BUILDING_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function



        ''' <summary>
        ''' For Select Property Screen
        ''' </summary>
        ''' <param name="Prev_Year_Id"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_LB_Listing_Profile(Param As Param_Get_LB_Listing_Profile, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Property_Listing_Complexes"
            Dim params() As String = {"CEN_BK_PADNO", "YEARID", "PREV_YEARID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_Year_ID = "" Then Param.Prev_Year_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.ExcludeRecID = "" Then Param.ExcludeRecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {Param.Cen_BK_PAD_NO, inBasicParam.openYearID, Param.Prev_Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, SPName, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetRecordCountByName(ByVal Param As Param_GetRecordCountByName_Complexes, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(REC_ID) FROM COMPLEX_INFO  WHERE REC_STATUS IN (0,1,2) AND CM_CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(CM_COMPLEX_Name)  = '" & Param.Name.ToUpper & "'   "
            'and CM_COD_YEAR_ID <='" & inBasicParam.openYearID & "' and ( CM_COD_YEAR_ID ='" & inBasicParam.openYearID & "' OR CM_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = CM_CEN_ID))"
            If Not Param.ID Is Nothing Then
                OnlineQuery += " AND REC_ID <> '" & Param.ID & "'"
            End If
            Return dbService.GetScalar(ConnectOneWS.Tables.COMPLEX_INFO, OnlineQuery, ConnectOneWS.Tables.COMPLEX_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMaxEditOn(ComplexID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT MAX(REC_EDIT_ON) FROM complex_building_info WHERE CB_COMPLEX_ID = '" & ComplexID & "' AND CB_COD_YEAR_ID <= " & inBasicParam.openYearID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, OnlineQuery, ConnectOneWS.Tables.COMPLEX_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function IsPropertyAlreadyMapped(ByVal Param As Param_IsPropertyAlreadyMapped, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(CB_LB_ID) FROM complex_building_info WHERE CB_LB_ID = '" & Param.PropertyID & "'"
            If Param.IsManageBuildings Then
                OnlineQuery += " AND CB_COMPLEX_ID <> '" & Param.ComplexID & "'"
            End If
            Dim Cnt As Integer = dbService.GetScalar(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, OnlineQuery, ConnectOneWS.Tables.COMPLEX_BUILDING_INFO.ToString(), inBasicParam)
            If Cnt > 0 Then Return True
            Return False
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Insert(ByVal InParam As Param_Insert_Complexes, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO complex_info(CM_CEN_ID,CM_COMPLEX_Name,CM_R_ADD1,CM_R_ADD2,CM_R_ADD3,CM_R_ADD4,CM_R_CITY_ID,CM_R_DISTRICT_ID,CM_R_STATE_ID," &
                                        "CM_R_PINCODE,CM_R_COUNTRY_ID,CM_REMARKS,CM_COD_YEAR_ID," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,CM_ORG_REC_ID " &
                                        ") VALUES(" &
                                        " " & inBasicParam.openCenID.ToString & " ," &
                                        " '" & InParam.ComplexName & "' ," &
                                        " '" & InParam.CM_Add1 & "' ," &
                                        " '" & InParam.CM_Add2 & "' ," &
                                        " '" & InParam.CM_Add3 & "' ," &
                                        " '" & InParam.CM_Add4 & "' ," &
                                        " '" & InParam.CM_CityID & "' ," &
                                        " '" & InParam.CM_DistrictID & "' ," &
                                        " '" & InParam.CM_StateID & "' ," &
                                        " '" & InParam.CM_Pincode & "' ," &
                                        " '" & InParam.CM_CountryID & "' ," &
                                        " '" & InParam.CM_Remarks & "' ," &
                                        " " & InParam.YearID.ToString & " ," &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.COMPLEX_INFO, OnlineQuery, inBasicParam, InParam.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Insert_Building(InParam As Param_InsertBuilding_Complexes, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO complex_building_info(CB_CEN_ID,CB_LB_ID,CB_COMPLEX_ID,CB_COD_YEAR_ID," &
                                        "REC_ADD_ON, REC_ADD_BY, REC_EDIT_ON, REC_EDIT_BY, REC_STATUS, REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                        ") VALUES(" &
                                        " " & inBasicParam.openCenID.ToString & " ," &
                                        " '" & InParam.LB_ID & "' ," &
                                        " '" & InParam.ComplexID & "' ," &
                                        " " & InParam.YearID.ToString & " ," &
                                          "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, OnlineQuery, inBasicParam, InParam.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function Update(UpParam As Param_Update_Complexes, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "  UPDATE complex_info SET " &
                                        "CM_COMPLEX_Name = '" & UpParam.ComplexName & "' ," &
                                        "CM_R_ADD1 =  '" & UpParam.CM_Add1 & "' ," &
                                        "CM_R_ADD2 = '" & UpParam.CM_Add2 & "' ," &
                                        "CM_R_ADD3 = '" & UpParam.CM_Add3 & "' ," &
                                        "CM_R_ADD4 = '" & UpParam.CM_Add4 & "' ," &
                                        "CM_R_CITY_ID = '" & UpParam.CM_CityID & "' ," &
                                        "CM_R_DISTRICT_ID = '" & UpParam.CM_DistrictID & "' ," &
                                        "CM_R_STATE_ID = '" & UpParam.CM_StateID & "' ," &
                                        "CM_R_PINCODE = '" & UpParam.CM_Pincode & "' ," &
                                        "CM_R_COUNTRY_ID = '" & UpParam.CM_CountryID & "' ," &
                                        "CM_REMARKS = '" & UpParam.CM_Remarks & "' ," &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.COMPLEX_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        'Public Shared Function UpdateBuilding(RecId As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim OnlineQuery As String = "UPDATE complex_building_info SET " & _
        '                                "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
        '                                "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
        '                                "REC_ID ='" & RecId & "'"
        '    dbService.Update(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, OnlineQuery, inBasicParam)
        '    Return True
        'End Function

        Public Shared Function Insert_Complexes_Txn(inParam As Param_Txn_Insert_Complexes, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.Insert_Complexes Is Nothing Then
                If Not Insert(inParam.Insert_Complexes, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Param_InsertBuilding_Complexes In inParam.InsertBuildings
                If Not Param Is Nothing Then Insert_Building(Param, inBasicParam, RequestTime)
            Next

            ' End Using
            'Commit here 
            ' txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function Update_ManageBuildings_Txn(inParam As Param_Update_ManageBuildings_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.RecID_DeleteBuilding Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, "CB_COMPLEX_ID   = '" & inParam.RecID_DeleteBuilding & "' ", inBasicParam)
            End If
            For Each Param As Param_InsertBuilding_Complexes In inParam.InsertBuildings
                If Not Param Is Nothing Then Insert_Building(Param, inBasicParam, RequestTime)
            Next
            '  End Using
            'Commit here 
            ' txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function DeleteComplexBuilding_Txn(DelParam As Param_Txn_Delete_Complexes, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not DelParam.RecID_DeleteBuilding Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, "CB_COMPLEX_ID   = '" & DelParam.RecID_DeleteBuilding & "' ", inBasicParam)
            End If
            If Not DelParam.RecID_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.COMPLEX_INFO, DelParam.RecID_Delete, inBasicParam)
            End If
            '  End Using
            'Commit here 
            ' txn.Complete()
            ' End Using
            Return True
        End Function

    End Class

End Namespace
