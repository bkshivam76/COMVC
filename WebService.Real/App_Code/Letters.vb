Imports System.Data

Namespace Real
#Region "Facility"
    <Serializable>
    Public Class Letters
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_Letters
            Public InstID As String
            Public LetterDate As String
            Public Reference As String
            Public Language As String
            Public Matter As String
            Public Status_Action As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Letters
            Public InstID As String
            Public LetterDate As String
            Public Reference As String
            Public Language As String
            Public Matter As String
            'Public Status_Action As String
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
        ''' <remarks>RealServiceFunctions.Letters_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim OnlineQuery As String = " SELECT INS_NAME AS Institute, L_DATE AS 'Date' ,L_REF AS 'Reference', L_LANG AS 'Language',L_H_INS_ID  AS INS_ID,L.REC_ID AS ID  ," & Common.Rec_Detail("L") & "" &
            '                            " FROM LETTER_INFO L" &
            '                            " INNER JOIN INSTITUTE_INFO I ON L.L_H_INS_ID = I.INS_ID" &
            '                            " Where   L.REC_STATUS IN (0,1,2) AND L_CEN_ID = " & inBasicParam.openCenID.ToString & " ; "
            'Return dbService.List(ConnectOneWS.Tables.LETTER_INFO, OnlineQuery, ConnectOneWS.Tables.LETTER_INFO.ToString(), inBasicParam)

            Dim paramters As String() = {"@CEN_ID", "@YEAR_ID", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {4, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LETTER_INFO, "[sp_get_Letters]", ConnectOneWS.Tables.LETTER_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)

        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Letters_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Letters, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO LETTER_INFO(L_CEN_ID,L_H_INS_ID,L_DATE,L_REF,L_LANG,L_MATTER," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "'" & InParam.InstID & "'," & _
                                                  " " & If(IsDate(InParam.LetterDate), "'" & Convert.ToDateTime(InParam.LetterDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  "'" & InParam.Reference & "', " & _
                                                  "'" & InParam.Language & "', " & _
                                                  "'" & InParam.Matter & "', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.Rec_ID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.LETTER_INFO, OnlineQuery, inBasicParam, InParam.Rec_ID)
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
        ''' <remarks>RealServiceFunctions.Letters_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Letters, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE LETTER_INFO SET " & _
                                        "L_H_INS_ID       ='" & UpParam.InstID & "', " & _
                                        "L_DATE         =" & If(IsDate(UpParam.LetterDate), "'" & Convert.ToDateTime(UpParam.LetterDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        "L_REF          ='" & UpParam.Reference & "', " & _
                                        "L_LANG         ='" & UpParam.Language & "', " & _
                                        "L_MATTER       ='" & UpParam.Matter & "',  " & _
                                         " " & _
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.LETTER_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
#End Region
End Namespace

