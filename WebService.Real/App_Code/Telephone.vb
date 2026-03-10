Imports System.Data

Namespace Real
    <Serializable>
    Public Class Telephones
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_Telephones
            Public TP_NO As String
            Public TelMiscId As String
            Public Category As String
            Public Type As String
            Public Other_Det As String
            Public Status_Action As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Telephones
            Public TP_NO As String
            Public TELECOM_MISC_ID As String
            Public CATEGORY As String
            Public Cmd_Type As String
            Public Details As String
            'Public Status_Action As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Close_Telephones
            Public CloseDate As String
            Public Reason As String
            Public Rec_ID As String
        End Class
#End Region
        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Telephone_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim onlineQuery As String = " SELECT TP_NO,TP_TELECOM_MISC_ID,TP_CATEGORY AS Category,TP_TYPE ,TP_OTHER_DETAIL,REC_ID AS ID," & Common.Rec_Detail("Telephone_Info") & " " & _
            '                                " FROM Telephone_Info " & _
            '                                " Where   REC_STATUS IN (0,1,2) AND TP_CEN_ID=" & inBasicParam.openCenID.ToString & " ; "
            'Return dbService.List(ConnectOneWS.Tables.TELEPHONE_INFO, onlineQuery, ConnectOneWS.Tables.TELEPHONE_INFO.ToString(), inBasicParam)
            Dim SPName As String = "sp_get_Telephone_Profile"
            Dim params() As String = {"CENID", "YEARID", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TELEPHONE_INFO, SPName, ConnectOneWS.Tables.TELEPHONE_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)

        End Function

        Public Shared Function GetListByCondition(ByVal OtherCondition As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  TP.TP_NO,MI.MISC_NAME AS 'TP_COMPANY',TP.TP_CATEGORY ,TP.TP_TYPE , TP.REC_ID AS 'TP_ID' " & _
                                  " FROM TELEPHONE_INFO AS TP INNER JOIN MISC_INFO AS MI ON (TP.TP_TELECOM_MISC_ID = MI.REC_ID AND MI.REC_STATUS IN (0,1,2)) " & _
                                  " Where   TP.REC_STATUS IN (0,1,2) AND TP.TP_CEN_ID =" & inBasicParam.openCenID.ToString & " " & OtherCondition
            Return dbService.List(ConnectOneWS.Tables.TELEPHONE_INFO, Query, ConnectOneWS.Tables.TELEPHONE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Record By TeleNumber
        ''' </summary>
        ''' <param name="Tel_No"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Telephone_GetRecordByTeleNumber</remarks>
        Public Shared Function GetRecordByTeleNumber(ByVal Tel_No As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM TELEPHONE_INFO WHERE  REC_STATUS IN (0,1,2) AND UPPER(TP_NO)='" & Trim(UCase(Tel_No)) & "' AND  TP_CEN_ID =" & inBasicParam.openCenID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.TELEPHONE_INFO, OnlineQuery, ConnectOneWS.Tables.TELEPHONE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetCountInTxn(ByVal TP_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(TR_REF_ID) FROM TRANSACTION_D_OTHER_INFO  WHERE  TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TR_REF_ID  = '" & TP_ID & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_OTHER_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_OTHER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetLastEntryDate(ByVal Tel_Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT MAX(TR_DATE) AS LAST_EDT FROM TRANSACTION_INFO AS TI INNER JOIN TRANSACTION_D_OTHER_INFO AS OTHER ON TI.TR_M_ID = OTHER.TR_M_ID WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND OTHER.TR_REF_ID = '" & Tel_Rec_Id & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Telephone_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Telephones, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO TELEPHONE_INFO(TP_CEN_ID,TP_NO,TP_TELECOM_MISC_ID,TP_CATEGORY,TP_TYPE,TP_OTHER_DETAIL," &
                                                         "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                         ") VALUES(" &
                                                         "" & inBasicParam.openCenID.ToString & "," &
                                                         "'" & InParam.TP_NO & "', " &
                                                         "'" & InParam.TelMiscId & "', " &
                                                         "'" & InParam.Category & "', " &
                                                         "'" & InParam.Type & "', " &
                                                         "'" & InParam.Other_Det & "', " &
                                                         "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TELEPHONE_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Telephone_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Telephones, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TELEPHONE_INFO SET " &
                            "TP_NO             ='" & UpParam.TP_NO & "', " &
                            "TP_TELECOM_MISC_ID   ='" & UpParam.TELECOM_MISC_ID & "', " &
                            "TP_CATEGORY       ='" & UpParam.CATEGORY & "', " &
                            "TP_TYPE           ='" & UpParam.Cmd_Type & "', " &
                            "TP_OTHER_DETAIL   ='" & UpParam.Details & "', " &
                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "-WA" & "' " &
                            "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.TELEPHONE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Close(ByVal Cls As Telephones.Parameter_Close_Telephones, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE telephone_info SET " &
                                        "TP_CLOSE_DATE     = " & If(IsDate(Cls.CloseDate), "'" & Convert.ToDateTime(Cls.CloseDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                        "TP_CLOSE_REMARKS  ='" & Cls.Reason & "', " &
                                        " " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " &
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," &
                                        "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "-WA" & "'," &
                                        "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " &
                                        "  WHERE REC_ID    ='" & Cls.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.TELEPHONE_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        ' ''' <summary>
        ' ''' Updates AssetLocation Where not Present: Global_Set
        ' ''' </summary>
        ' ''' <param name="defaultLocationID"></param>
        ' ''' <param name="Screen"></param>
        ' ''' <param name="openUserID"></param>
        ' ''' <param name="openCenID"></param>
        ' ''' <param name="PCID"></param>
        ' ''' <param name="version"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.Telephone_UpdateAssetLocationIfNotPresent</remarks>
        'Public Shared Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim OnlineQuery As String = " UPDATE Telephone_Info SET TP_LOC_AL_ID  ='" & defaultLocationID & "', REC_EDIT_ON  = '" & Common.DateTimePlaceHolder & "'  WHERE COALESCE(TP_LOC_AL_ID,'')=''"
        '    dbService.Update(ConnectOneWS.Tables.TELEPHONE_INFO, OnlineQuery, inBasicParam)
        '    Return True
        'End Function
    End Class
End Namespace
