Imports System.Data

Namespace Real
    <Serializable>
    Public Class Cash
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_AddDefault_Cash
            Public Cash_Acc_ID As String
            Public openYearID As Integer
        End Class
        <Serializable>
        Public Class Parameter_Update_Cash
            Public Amount As Double
            'Public Status_Action As String
            Public Rec_ID As String
        End Class
        'Public Class Param_GetOpeningBalance
        '    Public RecIDColHeader As String
        '    Public AccountRecID As String
        '    Public RecID As Object
        'End Class
#End Region

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Cash_AddDefault</remarks>
        Public Shared Function AddDefault(ByVal param As Parameter_AddDefault_Cash, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO Opening_Balances_Info(OP_CEN_ID,OP_COD_YEAR_ID,OP_AMOUNT," &
                                     "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,OP_ORG_REC_ID" &
                                     ") VALUES(" &
                                     "" & inBasicParam.openCenID.ToString & "," & param.openYearID.ToString & ",0," &
                                     "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & param.Cash_Acc_ID & "', '" & param.Cash_Acc_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.OPENING_BALANCES_INFO, OnlineQuery, inBasicParam, param.Cash_Acc_ID)
            Return True
        End Function

        ''' <summary>
        ''' Updates info
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Cash_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Cash, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Opening_Balances_Info SET " &
                                         "OP_AMOUNT         = " & UpParam.Amount & ", " &
                                         " " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.OPENING_BALANCES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Cash_GetCashOpeningBalance</remarks>
        Public Shared Function GetCashOpeningBalance(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS() 'step:5
            Dim Query As String = " SELECT OP_AMOUNT ," &
                              " REC_EDIT_ON " &
                              " FROM Opening_Balances_Info " &
                              " Where   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND OP_CEN_ID='" & inBasicParam.openCenID & "' and REC_ID='" & RecID & "'; "
            Return dbService.List(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
        End Function
    End Class

End Namespace
