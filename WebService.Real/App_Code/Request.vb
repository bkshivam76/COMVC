Imports System.Data
Imports Google.Apis.Sheets.v4.Data

Namespace Real
#Region "Help"
    <Serializable>
    Public Class Request
        Friend UpdateSheetProperties As UpdateSheetPropertiesRequest
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_Request
            Public Request As String
            Public RequestFrom As String
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Request
            Public Request As String
            Public Rec_ID As String
        End Class
#End Region
        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim OnlineQuery As String = "select (select sum(1) from Request_Box S where S.rec_add_on<=R.rec_add_on and REC_STATUS IN (0,1,2) AND R_CEN_ID = " & inBasicParam.openCenID.ToString & "" & ") AS Sr," & _
            '                      " R_DETAIL AS 'Request Detail',R_REMARKS AS 'Administrator Remarks',R_STATUS AS 'Status',COALESCE(R_READ,'No') AS 'Read',R_SEND_FROM AS 'Send From', REC_ID AS ID," & Common.Rec_Detail("R") & _
            '                      " FROM Request_Box R WHERE REC_STATUS IN (0,1,2) AND R_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            'Return dbService.List(ConnectOneWS.Tables.REQUEST_BOX, OnlineQuery, ConnectOneWS.Tables.REQUEST_BOX.ToString(), inBasicParam)
            Dim SPName As String = "sp_get_Request_Box"
            Dim params() As String = {"CENID", "YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.REQUEST_BOX, SPName, ConnectOneWS.Tables.REQUEST_BOX.ToString(), params, values, dbTypes, lengths, inBasicParam)

        End Function

        ''' <summary>
        ''' Get Unread Count
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_GetUnreadCount</remarks>
        Public Shared Function GetUnreadCount(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM Request_Box WHERE  REC_STATUS IN (0,1,2)  AND  UPPER(COALESCE(R_READ,'No')) ='NO' AND UPPER(R_STATUS)<> 'PENDING' AND R_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.REQUEST_BOX, OnlineQuery, ConnectOneWS.Tables.REQUEST_BOX.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Marks Request As Read
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_MarkAsRead</remarks>
        Public Shared Function MarkAsRead(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Request_Box SET " & _
                                        "R_READ             ='Yes' " & _
                                        "  WHERE REC_ID    ='" & Rec_ID & "'"
            '",REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
            '"REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
            '"REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
            '"REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
            '"REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _


            dbService.Update(ConnectOneWS.Tables.REQUEST_BOX, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Marks Request As UnRead
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_MarkAsUnread</remarks>
        Public Shared Function MarkAsUnread(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Request_Box SET " & _
                                        "R_READ             ='No' " & _
                                         "  WHERE REC_ID    ='" & Rec_ID & "'"
            '  "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
            '  "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
            ' "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
            ' "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
            ' "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _

            dbService.Update(ConnectOneWS.Tables.REQUEST_BOX, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Inserts request
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Request, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO Request_Box(R_CEN_ID,R_DETAIL,R_SEND_FROM,R_STATUS,R_READ," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "'" & InParam.Request & "', " & _
                                                  "'" & InParam.RequestFrom & "', " & _
                                                  "'" & "PENDING" & "','No', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.REQUEST_BOX, OnlineQuery, inBasicParam, InParam.RecID)
            Return True
        End Function

        ''' <summary>
        ''' Updates RequestBox
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Request_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Request, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Request_Box SET " & _
                                         "R_DETAIL             ='" & UpParam.Request & "', " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                        "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.REQUEST_BOX, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
#End Region
End Namespace
