Imports System.Data

Namespace Real
#Region "Facility"
    <Serializable>
    Public Class Notes
#Region "Param Classes"
        <Serializable>
        Public Class Param_AddQuickNote_Notes
            Public Note As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Notes
            Public Note As String
            Public Status As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Notes
            Public Note As String
            Public Status As String
            Public Rec_ID As String
        End Class
#End Region
        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim OnlineQuery As String = "select NOTE_DETAIL AS Notes  ,REC_ADD_ON as 'Add On',NOTE_STATUS AS Status,REC_ID AS ID," & Common.Rec_Detail("NOTES_INFO") & "  FROM NOTES_INFO WHERE  REC_STATUS IN (0,1,2) AND NOTE_CEN_ID = " & inBasicParam.openCenID.ToString & " order by  REC_ADD_ON,REC_ID"
            'Return dbService.List(ConnectOneWS.Tables.NOTES_INFO, OnlineQuery, ConnectOneWS.Tables.NOTES_INFO.ToString(), inBasicParam)

            Dim paramters As String() = {"@CEN_ID", "@YEAR_ID", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {4, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.NOTES_INFO, "[sp_get_Notes]", ConnectOneWS.Tables.NOTES_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)

        End Function

        ''' <summary>
        ''' Gets ShortList
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_GetShortList</remarks>
        Public Shared Function GetShortList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select NOTE_DETAIL AS Notes  ,REC_ADD_ON as 'Add On',REC_ID AS ID FROM NOTES_INFO WHERE  REC_STATUS IN (0,1,2) AND NOTE_CEN_ID = " & inBasicParam.openCenID.ToString & " order by  REC_ADD_ON,REC_ID"
            Return dbService.List(ConnectOneWS.Tables.NOTES_INFO, OnlineQuery, ConnectOneWS.Tables.NOTES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Add Quick Note
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_AddQuickNote</remarks>
        Public Shared Function AddQuickNote(ByVal Param As Param_AddQuickNote_Notes, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO NOTES_INFO(NOTE_CEN_ID,NOTE_DETAIL,NOTE_STATUS," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                        ") VALUES(" &
                                        "" & inBasicParam.openCenID.ToString & ", " &
                                        "N'" & Param.Note & "', " &
                                        "'" & "INCOMPLETE" & "'," &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Param.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.NOTES_INFO, OnlineQuery, inBasicParam, Param.Rec_ID)
            Return True
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Notes, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO NOTES_INFO(NOTE_CEN_ID,NOTE_DETAIL,NOTE_STATUS," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                        ") VALUES(" &
                                        "" & inBasicParam.openCenID.ToString & ", " &
                                        "N'" & InParam.Note & "', " &
                                        "'" & InParam.Status & "', " &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.NOTES_INFO, OnlineQuery, inBasicParam, InParam.Rec_ID)
            Return True
        End Function

        ''' <summary>
        ''' Updates note Status as Completed
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_Complete</remarks>
        Public Shared Function Complete(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE NOTES_INFO SET " & _
                                        " NOTE_STATUS       = '" & "COMPLETED" & "', " & _
                                         " " & _
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                         "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                         "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                         "  WHERE REC_ID    ='" & Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.NOTES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Updates Note status to Incomplete
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_Incomplete</remarks>
        Public Shared Function Incomplete(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE NOTES_INFO SET " & _
                                        " NOTE_STATUS       = '" & "INCOMPLETE" & "', " & _
                                         " " & _
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                         "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                         "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                         "  WHERE REC_ID    ='" & Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.NOTES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Notes_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Notes, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE NOTES_INFO SET " &
                                       "NOTE_DETAIL  = N'" & UpParam.Note & "', " &
                                       "NOTE_STATUS       = '" & UpParam.Status & "', " &
                                         " " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"

            '",REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
            '"REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
            '"REC_STATUS_BY     ='" & inBasicParam.openUserID & "' 
            dbService.Update(ConnectOneWS.Tables.NOTES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
#End Region
End Namespace