Imports System.Data

Namespace Real
#Region "--Profile--"
    <Serializable>
    Public Class Membership
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetSubscriptionFee_Membership
            Public SubsID As String
            Public StartDate As Date
            Public UseSubID As Boolean = True
            Public InsID As String
            Public Subscription As String
        End Class
        <Serializable>
        Public Class Param_GetDuplicateCount_Membership
            Public Rec_Id As String
            Public AB_ID As String
            Public WING_ID As String
        End Class
        <Serializable>
        Public Class Param_GetCountForContinue_Membership
            Public AB_ID As String
            Public WING_ID As String
        End Class
        <Serializable>
        Public Class Param_GetDuplicateOldNoCount_Membership
            Public Rec_Id As String
            Public OLD_NO As String
        End Class
        <Serializable>
        Public Class Param_GetMasterTransactionList_Membership
            Public ByMasterID As Boolean
            Public ID As String
        End Class
        <Serializable>
        Public Class Param_GetDiscontinued_Membership
            Public ByMasterID As Boolean
            Public Id As String
        End Class
        <Serializable>
        Public Class Param_GetList_Membership
            Public VoucherEntry As String
            Public ProfileEntry As String
            Public OtherCondition As String
        End Class
        <Serializable>
        Public Class Param_GetBalancesList
            Public OtherCondition As String
            Public YearEndDate As DateTime
        End Class
        <Serializable>
        Public Class Parameter_Insert_Membership
            Public AB_ID As String
            Public SUBS_ID As String
            Public Wing_Id As String
            Public StartDate As String
            Public Mem_Old_No As String
            Public Mem_No As String
            Public OtherDetails As String
            Public Status_Action As String
            Public Rec_ID As String
            Public Txn_ID As String
            ' Public Mem_Condition As String
            Public Screen As ConnectOneWS.ClientScreen
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertBalances_Membership
            Public REC_ID As String
            Public Sr_No As Integer
            Public SUBS_ID As String
            Public Item_ID As String
            Public Entry_Date As String
            Public Period_From As String
            Public Period_To As String
            Public Amount As Double
            Public Status_Action As String
            Public Txn_ID As String
            Public Screen As ConnectOneWS.ClientScreen
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Update_Membership
            Public AB_ID As String
            Public SUBS_ID As String
            Public Wing_Id As String
            Public StartDate As String
            Public Mem_Old_No As String
            Public Mem_No As String
            Public OtherDetails As String
            'Public Status_Action As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Close_Membership
            Public CloseDate As String
            Public Reason As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_Membership
            Public param_InsertMembership As Parameter_Insert_Membership
            Public param_InsertBalances As Parameter_InsertBalances_Membership = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Update_Membership
            Public param_UpdateMembership As Parameter_Update_Membership
            Public RecID_DeleteBalances As String = Nothing
            Public param_InsertBalances As Parameter_InsertBalances_Membership = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_Membership
            Public RecID_Delete As String = Nothing
            Public RecID_DeleteBalances As String = Nothing
        End Class
        <Serializable>
        Public Class Param_GetCountForWing2WingConversion
            Public AB_ID As String
            Public Wing_Id As String
        End Class
        <Serializable>
        Public Class Param_Inset_SubcriptionList
            Public Ins_Id As Integer
            Public SrNo As Integer
            Public SIName As String
            Public SI_Category As String
            Public SI_StartMonth As Integer
            Public SI_TotMonth As Integer
            Public Rec_Add_Dt As Date
            Public Rec_AddBy As String
            Public Rec_Status As Integer
            Public Rec_StatusDt As Date
            Public Rec_StatusBy As String
            Public Rec_Id As String
        End Class
        <Serializable>
        Public Class Param_Update_SubscirptionList
            Public Rec_ID As String
            Public SI_Name As String
            Public SI_Category As String
            Public SI_StartMonth As Integer
            Public SI_TotMonth As Integer
            Public Rec_EditDate As DateTime
            Public Rec_Edit_By As String
        End Class

        <Serializable>
        Public Class Param_Insert_SubscriptionFee
            Public SI_Rec_Id As String
            Public Sub_EntFee As Integer
            Public Sub_Fee As Integer
            Public Sub_RenewFee As Integer
            Public Sub_EffDate As DateTime
            Public Rec_Add_Dt As DateTime
            Public Rec_Add_By As String
            Public Rec_EditOn As DateTime
            Public Rec_EditBy As String
            Public Rec_Status As Integer
            Public Rec_StatusDt As Date
            Public Rec_StatusBy As String
            Public Rec_Id As String
        End Class

        <Serializable>
        Public Class Param_Update_SubscriptionFee
            Public Rec_ID As String
            Public Sub_EntFee As Integer
            Public Sub_Fee As Integer
            Public Sub_RenewFee As Integer
            Public Sub_EffDate As DateTime
            Public Sub_EditOn As DateTime
            Public Rec_Edit_By As String
        End Class
        <Serializable>
        Public Class Param_Delet_SubscriptionFee
            Public SI_Rec_Id As String
            Public Rec_Id As String
        End Class

#End Region
        ''' <summary>
        ''' Get Wings
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetWings</remarks>
        Public Shared Function GetWings(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select WING_NAME,WING_SHORT_MS,REC_ID AS WING_REC_ID FROM WINGS_INFO WHERE  REC_STATUS IN (0,1,2)  ORDER BY WING_NAME "
            Return dbService.List(ConnectOneWS.Tables.WINGS_INFO, Query, ConnectOneWS.Tables.WINGS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Subscription List
        ''' </summary>
        ''' <param name="InsID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetSubscriptionList</remarks>
        Public Shared Function GetSubscriptionList(ByVal InsID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select SI_NAME,SI_CATEGORY,SI_START_MONTH,SI_TOTAL_MONTH,REC_ID AS SI_REC_ID FROM SUBSCRIPTION_INFO WHERE  REC_STATUS IN (0,1,2) AND INS_ID = '" & InsID & "'  ORDER BY SI_SRNO "
            Return dbService.List(ConnectOneWS.Tables.SUBSCRIPTION_INFO, Query, ConnectOneWS.Tables.SUBSCRIPTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Subscription List-StoreProcedure
        ''' </summary>
        ''' <param name="InsID"></param>
        ''' <returns></returns>
        ''' <remarks>Membership_GetSubscriptionList</remarks>
        Public Shared Function GetSubscritonList_Master(ByVal InsID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"InsID"}
            Dim values As Object() = {InsID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32}
            Dim lengths As Integer() = {5}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUBSCRIPTION_INFO, "sp_Get_subscription_info", ConnectOneWS.Tables.SUBSCRIPTION_INFO, paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Get Subscription Fee
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetSubscriptionFee</remarks>
        Public Shared Function GetSubscriptionFee(ByVal Param As Param_GetSubscriptionFee_Membership, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "select TOP 1 SI_REC_ID,SF_ENT_FEE,SF_SUBS_FEE,SF_RENEW_FEE,SF_EFF_DATE,REC_ID AS SF_REC_ID FROM SUBSCRIPTION_FEE_INFO WHERE  REC_STATUS IN (0,1,2) AND SI_REC_ID = '" & Param.SubsID & "' AND CAST(SF_EFF_DATE AS DATE) <='" & Format(Param.StartDate, Common.Server_Date_Format_Short) & "'  ORDER BY SF_EFF_DATE DESC "
            If Not Param.UseSubID Then
                onlineQuery = "select TOP 1 SI_REC_ID,SF_ENT_FEE,SF_SUBS_FEE,SF_RENEW_FEE,SF_EFF_DATE,SF.REC_ID AS SF_REC_ID FROM SUBSCRIPTION_FEE_INFO as SF INNER JOIN SUBSCRIPTION_INFO AS SI ON SF.SI_REC_ID = SI.REC_ID WHERE  SF.REC_STATUS IN (0,1,2) AND SI_NAME = '" & Param.Subscription & "' AND SI.INS_ID = '" & Param.InsID & "' AND CAST(SF_EFF_DATE AS DATE) <='" & Format(Param.StartDate, Common.Server_Date_Format_Short) & "'  ORDER BY SF_EFF_DATE DESC "
            End If
            Return dbService.List(ConnectOneWS.Tables.SUBSCRIPTION_FEE_INFO, onlineQuery, ConnectOneWS.Tables.SUBSCRIPTION_FEE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Subscription Fee - Store Procedure
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetSubscriptionFee</remarks>
        Public Shared Function GetSubscritonFeeList_Master(ByVal SI_RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"SI_RecId"}
            Dim values As Object() = {SI_RecID}
            Dim dbTypes As System.Data.DbType() = {DbType.String}
            Dim lengths As Integer() = {36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUBSCRIPTION_FEE_INFO, "sp_get_Subcription_Fee_Info", ConnectOneWS.Tables.SUBSCRIPTION_FEE_INFO, paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Duplicate Count
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetDuplicateCount</remarks>
        Public Shared Function GetDuplicateCount(ByVal Param As Param_GetDuplicateCount_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim WingFilter As String = ""
            If Not Param.WING_ID Is Nothing Then WingFilter = " AND MS_WING_ID ='" & Param.WING_ID & "'"
            Dim onlineQuery As String = "SELECT COUNT(*) FROM MEMBERSHIP_INFO WHERE MS_AB_ID ='" & Param.AB_ID & "' " & WingFilter & " AND REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS_CEN_ID='" & inBasicParam.openCenID & "' AND REC_ID NOT IN ('" & Param.Rec_Id & "') AND MS_CLOSE_DATE IS NULL "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Count For Continue
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetCountForContinue</remarks>
        Public Shared Function GetCountForContinue(ByVal Param As Param_GetCountForContinue_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT COUNT(*) FROM MEMBERSHIP_INFO WHERE MS_AB_ID ='" & Param.AB_ID & "' AND MS_WING_ID ='" & Param.WING_ID & "' AND REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MS_CLOSE_DATE IS NULL "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Discontinue date of member
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetDiscontinued_Date(ByVal Param As Param_GetCountForContinue_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT MS_CLOSE_DATE FROM MEMBERSHIP_INFO AS MI INNER JOIN SUBSCRIPTION_INFO AS SI ON MI.MS_SI_ID = SI.REC_ID WHERE MS_AB_ID ='" & Param.AB_ID & "' AND MS_WING_ID ='" & Param.WING_ID & "' AND MI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MS_CLOSE_DATE IS NOT NULL AND SI.SI_CATEGORY = 'YEARLY' "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Duplicate OldNo Count
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetDuplicateOldNoCount</remarks>
        Public Shared Function GetDuplicateOldNoCount(ByVal Param As Param_GetDuplicateOldNoCount_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT COUNT(*) FROM MEMBERSHIP_INFO WHERE MS_OLD_NO ='" & Param.OLD_NO & "' AND REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_ID NOT IN ('" & Param.Rec_Id & "') "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Membership No
        ''' </summary>
        ''' <param name="M_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetMembershipNo</remarks>
        Public Shared Function GetMembershipNo(ByVal M_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT MS_NO FROM MEMBERSHIP_INFO WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MS_TR_ID = '" & M_Id & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get New Membership No
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetNewMembershipNo</remarks>
        Public Shared Function GetNewMembershipNo(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT  COUNT(MS_NO) FROM  MEMBERSHIP_INFO  WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND  CAST(SUBSTRING(MS_NO,1,6) AS INTEGER ) > 100000 "
            Dim XCOUNT As Double = dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, OnlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
            If XCOUNT = 0 Then
                Return 100001
            Else
                Dim onlineQuery2 As String = "SELECT CASE WHEN  MAX(MS_NO) IS NULL THEN 100001 ELSE MAX(CAST(SUBSTRING(MS_NO,1,6) AS INTEGER ) + 1 ) END  AS NEW_NO FROM   MEMBERSHIP_INFO  WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")  AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & ""
                Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery2, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
            End If
        End Function

        ''' <summary>
        ''' Get Last Period
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetLastPeriod</remarks>
        Public Shared Function GetLastPeriod(ByVal Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT MAX(MB_DATE) AS 'ENTRY_DATE', MAX(MB_PERIOD_FROM) AS 'PERIOD_FROM' FROM membership_balances_info WHERE MS_REC_ID='" & Rec_Id & "'  AND MB_CEN_ID=" & inBasicParam.openCenID.ToString & ""
            Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, Query, ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Last Entry Date
        ''' </summary>
        ''' <param name="Mem_Rec_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetLastEntryDate(ByVal Mem_Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT MAX(MB_DATE) AS LAST_EDT FROM MEMBERSHIP_BALANCES_INFO WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MS_REC_ID = '" & Mem_Rec_Id & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Last Transaction Date
        ''' </summary>
        ''' <param name="Mem_Rec_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetLastTransactionDate</remarks>
        Public Shared Function GetLastTransactionDate(ByVal Mem_Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT MAX(TR_DATE) AS LAST_TRANS_DT FROM TRANSACTION_D_MASTER_INFO WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TR_REF_ID = '" & Mem_Rec_Id & "' AND TR_CODE IN (12,13)"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, onlineQuery, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetMasterTransactionList</remarks>
        Public Shared Function GetMasterTransactionList(ByVal Param As Param_GetMasterTransactionList_Membership, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = ""
            If Param.ByMasterID Then
                onlineQuery = " SELECT TM.TR_REF_ID,TM.REC_ID,TM.REC_ADD_ON,MR.MR_NO " & _
                              " FROM TRANSACTION_D_MASTER_INFO  AS TM " & _
                              " LEFT  JOIN MEMBERSHIP_RECEIPT_INFO    AS MR ON (TM.REC_ID = MR.MR_TR_M_ID AND MR.MR_CANCEL IS NULL AND MR.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                              " WHERE TM.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " " & _
                              " AND TM.TR_REF_ID IN ( SELECT DISTINCT TR_REF_ID FROM TRANSACTION_D_MASTER_INFO WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_ID ='" & Param.ID & "' AND TR_CODE IN (12,13) ) " & _
                              " AND TM.TR_CODE IN (12,13) ORDER BY TM.REC_ADD_ON"
            Else
                onlineQuery = " SELECT TM.TR_REF_ID,TM.REC_ID,TM.REC_ADD_ON,MR.MR_NO " & _
                              " FROM TRANSACTION_D_MASTER_INFO  AS TM " & _
                              " LEFT  JOIN MEMBERSHIP_RECEIPT_INFO    AS MR ON (TM.REC_ID = MR.MR_TR_M_ID AND MR.MR_CANCEL IS NULL AND MR.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                              " WHERE TM.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " " & _
                              " AND TM.TR_REF_ID  = '" & Param.ID & "' " & _
                              " AND TM.TR_CODE IN (12,13) ORDER BY TM.REC_ADD_ON"
            End If
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, onlineQuery, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Discontinued
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetDiscontinued</remarks>
        Public Shared Function GetDiscontinued(ByVal Param As Param_GetDiscontinued_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = ""
            If Param.ByMasterID Then
                Dim Query As String = " SELECT DISTINCT TR_REF_ID FROM TRANSACTION_D_MASTER_INFO WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_ID ='" & Param.Id & "' "
                onlineQuery = " SELECT CASE WHEN MS_CLOSE_DATE IS NULL THEN 'Continue' ELSE 'Discontinued' END FROM MEMBERSHIP_INFO WHERE  REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " " & _
                              " AND REC_ID IN ('" & dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam) & "') "
            Else
                onlineQuery = " SELECT CASE WHEN MS_CLOSE_DATE IS NULL THEN 'Continue' ELSE 'Discontinued' END FROM MEMBERSHIP_INFO WHERE  REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_ID = '" & Param.Id & "' "
            End If
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_GetList_Membership, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            'Dim yr_Start_Date As String = "20" & inBasicParam.openYearID.ToString.Substring(0, 2) & "-04-01"
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim onlineQuery As String = " SELECT AB.C_NAME AS 'Member Name',MS.MS_NO as 'Membership No.', CASE WHEN SI.SI_NAME = 'ANNUAL' THEN 'ANNUAL' ELSE 'LIFE' END AS 'Membership',WS.WING_NAME AS 'Wing',COALESCE(CONVERSION.MS_START_DATE, MS.MS_START_DATE)'Original Start Date',MS.MS_START_DATE as 'Start Date', (SELECT   MAX(MB_PERIOD_TO)  FROM MEMBERSHIP_BALANCES_INFO WHERE REC_STATUS IN(0,1,2) AND MB_CEN_ID=MS.MS_CEN_ID AND MS_REC_ID = MS.REC_ID  AND MB_DATE <= cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',3,2) +'/3/31' as date) ) AS 'End Date', CASE WHEN COALESCE(AB.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END AS 'Centre Name', CASE WHEN COALESCE(AB.C_CEN_CATEGORY,0) = 0 THEN ST.ST_NAME ELSE '' END AS 'Centre State',CASE WHEN COALESCE(AB.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END AS 'Centre UID' ,MS.MS_WING_ID as 'Wing ID',CASE WHEN MS.MS_CLOSE_DATE IS NULL OR (MS.MS_CLOSE_DATE > cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',3,2) +'/3/31' as date)) THEN 'Continue' ELSE 'Discontinued' END AS Status,CASE WHEN MS.MS_CLOSE_DATE IS NULL OR (MS.MS_CLOSE_DATE > cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',3,2) +'/3/31' as date)) THEN NULL ELSE MS.MS_CLOSE_DATE END as 'Discontinued From',CASE WHEN MS.MS_CLOSE_DATE IS NULL OR (MS.MS_CLOSE_DATE > cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',3,2) +'/3/31' as date)) THEN NULL ELSE MS.MS_CLOSE_REMARKS END  AS 'Reason of Discontinued'," &
            '                            " CASE WHEN MS.MS_CLOSE_DATE IS NULL OR (MS.MS_CLOSE_DATE > cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',3,2) +'/3/31' as date)) THEN COALESCE((SELECT SUM(MB_AMOUNT) FROM MEMBERSHIP_BALANCES_INFO WHERE  REC_STATUS IN(0,1,2)  AND MB_CEN_ID=MS.MS_CEN_ID AND MS_REC_ID = MS.REC_ID AND MB_PERIOD_FROM > cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',1,2) +'/4/1'  as date)  AND MB_DATE <= cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',3,2) +'/3/31' as date) ),0) ELSE 0 END AS ADVANCE," &
            '                            " CASE WHEN MS.MS_CLOSE_DATE IS NULL OR (MS.MS_CLOSE_DATE > cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',3,2) +'/3/31' as date)) THEN COALESCE((FEE.FEE * (SELECT DATEDIFF(yy, (SELECT MAX(MB_PERIOD_TO)   FROM MEMBERSHIP_BALANCES_INFO  WHERE REC_STATUS IN(0,1,2)  AND MB_CEN_ID=MS.MS_CEN_ID AND MS_REC_ID = MS.REC_ID AND MB_PERIOD_FROM < cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',1,2) +'/4/1' as date)  AND MB_DATE <= cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',3,2) +'/3/31' as date)  ),cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',1,2) +'/3/31' as date)))),0) ELSE 0 END as 'Arrear', " &
            '                            " MS.MS_OTHER_DETAIL as 'Other Detail',MS.MS_OLD_NO AS 'Old Membership No.',MS.MS_AB_ID AS 'Member ID',COALESCE(MS.MS_TR_ID,'') AS TR_ID,MS.REC_ID AS ID ,CASE WHEN MS.MS_TR_ID IS NULL OR MS.MS_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " THEN '" & Param.ProfileEntry & "' ELSE  '" & Param.VoucherEntry & "' END as 'Entry Type',MS.MS_COD_YEAR_ID AS 'YearID' ," & Common.Rec_Detail("MS") & ", " &
            '                            " AB.C_EMAIL_ID_1 Email1, AB.C_EMAIL_ID_2 Email2, AB.C_MOB_NO_1 Mobile1, AB.C_MOB_NO_2 Mobile2, AB.C_FAX_NO_R_1 Fax1,  AB.C_FAX_NO_R_2 Fax2, AB.C_TEL_NO_R_1 Telephone1, AB.C_TEL_NO_R_2 Telephone2, " &
            '                            " COALESCE(C_R_ADD1,'') + CASE WHEN LEN(RTRIM(LTRIM(COALESCE(C_R_ADD1,''))))>0 THEN ', ' ELSE '' END + COALESCE(C_R_ADD2,'')  + CASE WHEN LEN(RTRIM(LTRIM(COALESCE(C_R_ADD2,''))))>0 THEN ', ' ELSE '' END 	+ COALESCE(C_R_ADD3,'')  + CASE WHEN LEN(RTRIM(LTRIM(COALESCE(C_R_ADD3,''))))>0 THEN ', ' ELSE '' END 	+ COALESCE(C_R_ADD4,'') + ',' +COALESCE(MEM_CITY.CI_NAME,C_R_CITY) + ',' +COALESCE(MEM_ST.ST_NAME,C_R_STATE) + '-' +COALESCE(AB.C_R_PINCODE,C_R_STATE) 	AS Address " &
            '                            " FROM       MEMBERSHIP_INFO      AS MS " &
            '                            " LEFT OUTER JOIN MEMBERSHIP_INFO AS CONVERSION ON (MS.MS_OLD_NO = CONVERSION.MS_NO) " &
            '                            " INNER JOIN ADDRESS_BOOK         AS AB ON (MS.MS_AB_ID       = AB.C_ORG_REC_ID AND C_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & "  AND AB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " &
            '                            " LEFT OUTER JOIN map_city_info AS MEM_CITY ON AB.C_R_CITY_ID = MEM_CITY.REC_ID " &
            '                            " Left OUTER JOIN map_state_info AS MEM_ST ON AB.C_R_STATE_ID = MEM_ST.REC_ID " &
            '                            " LEFT OUTER JOIN WINGS_INFO           AS WS ON (MS.MS_WING_ID     = WS.REC_ID AND WS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " &
            '                            " INNER JOIN SUBSCRIPTION_INFO    AS SI ON (MS.MS_SI_ID       = SI.REC_ID AND SI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " &
            '                            " INNER JOIN (SELECT SF.SI_REC_ID, SF.SF_RENEW_FEE AS FEE FROM SUBSCRIPTION_FEE_INFO AS SF INNER JOIN ( SELECT MAX(SF_EFF_DATE) SF_EFF_DATE, SI_REC_ID FROM subscription_fee_info WHERE SF_EFF_DATE < cast('20' +SUBSTRING('" & inBasicParam.openYearID.ToString & "',1,2) +'/4/1'  as date) GROUP BY SI_REC_ID) AS FEE_SLAB ON SF.SI_REC_ID = FEE_SLAB.SI_REC_ID AND SF.SF_EFF_DATE = FEE_SLAB.SF_EFF_DATE ) AS FEE ON SI.REC_ID = FEE.SI_REC_ID " &
            '                            " LEFT JOIN CENTRE_INFO           AS C  ON (AB.C_CLASS_CEN_ID = C.CEN_ID  AND AB.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
            '                            " LEFT OUTER JOIN MAP_STATE_INFO AS ST ON CEN_STATE_ID = ST.REC_ID " &
            '                            " LEFT JOIN OVERSEAS_CENTRE_INFO  AS O  ON (AB.C_CLASS_CEN_ID = O.CEN_ID  AND AB.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
            '                            " Where MS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND (MS.MS_CEN_ID=" & inBasicParam.openCenID.ToString & " OR (AB.C_CLASS_CEN_ID = " & inBasicParam.openCenID.ToString & " AND AB.C_CEN_CATEGORY = 0))" & Param.OtherCondition &
            '                            " AND MS.MS_NO NOT IN (select MS_OLD_NO from MEMBERSHIP_INFO WHERE MS_COD_YEAR_ID <= " & inBasicParam.openYearID.ToString & " ) " &
            '                            " AND COALESCE(MS.MS_CLOSE_DATE,'" & yr_Start_Date & "') >= '" & yr_Start_Date & "' " &
            '                            " AND MS.MS_COD_YEAR_ID <= " & inBasicParam.openYearID.ToString & " " &
            '                            " AND ( MS.MS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = MS.MS_CEN_ID) OR MS.MS_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & ") " &
            '                            " ORDER BY MS.MS_NO "

            'Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)

            Dim SPName As String = "sp_get_Wing_Membership"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "@UserID", "@ProfileEntry", "@VoucherEntry", "@OtherCondition"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, Param.ProfileEntry, Param.VoucherEntry, Param.OtherCondition}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255, 255, 255, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)

        End Function

        ''' <summary>
        ''' Get Balances List
        ''' </summary>
        ''' <param name="OtherCondition"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetBalancesList</remarks>
        Public Shared Function GetBalancesList(ByVal inParam As Param_GetBalancesList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT MB.MB_DATE AS 'Entry Date',MS.MS_NO as 'Mem. No.',si_name as 'Membership',I.ITEM_NAME AS 'Description',MB_PERIOD_FROM AS 'Period From',MB_PERIOD_TO AS 'Period To',MB.MB_AMOUNT as 'Amount',CASE WHEN CONVERSION.REC_ID IS NOT NULL THEN CONVERSION.REC_ID  ELSE MB.MS_REC_ID END as 'ID'  " & _
                                        " FROM  MEMBERSHIP_BALANCES_INFO    AS MB " & _
                                        " INNER JOIN MEMBERSHIP_INFO        AS MS ON (MS.REC_ID         = MB.MS_REC_ID  AND MS.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT OUTER JOIN MEMBERSHIP_INFO AS CONVERSION ON (MS.MS_NO = CONVERSION.MS_OLD_NO AND CONVERSION.MS_COD_YEAR_ID <= " & inBasicParam.openYearID.ToString & ") " & _
                                        " INNER JOIN ITEM_INFO              AS I  ON (MB.MB_ITEM_ID     = I.REC_ID      AND I.REC_STATUS    IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND I.REC_ID IN ('6b4e2492-14c7-11e1-9111-00ffddbf0f50','45afe059-14c8-11e1-9111-00ffddbf0f50','4e25ff2a-14c8-11e1-9111-00ffddbf0f50','5870e47d-14c8-11e1-9111-00ffddbf0f50','60966821-14c8-11e1-9111-00ffddbf0f50','68d917b2-14c8-11e1-9111-00ffddbf0f50') ) " & _
                                        " INNER JOIN ADDRESS_BOOK           AS AB ON (MS.MS_AB_ID       = AB.REC_ID     AND AB.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN SUBSCRIPTION_INFO      AS SI ON (MS.MS_SI_ID       = SI.REC_ID     AND SI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT  JOIN CENTRE_INFO            AS C  ON (AB.C_CLASS_CEN_ID = C.CEN_ID      AND AB.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                        " LEFT  JOIN OVERSEAS_CENTRE_INFO   AS O  ON (AB.C_CLASS_CEN_ID = O.CEN_ID      AND AB.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                        " Where  MB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND (MB.MB_CEN_ID=" & inBasicParam.openCenID.ToString & " OR AB.C_CLASS_CEN_ID = " & inBasicParam.openCenID.ToString & ") " & inParam.OtherCondition & _
                                        " AND MB.MB_DATE <= '" & inParam.YearEndDate.ToString(Common.Server_Date_Format_Long) & "'" & _
                                        " ORDER BY MB.MB_DATE,MS.REC_ID, MB.MB_TR_ID,MB.MB_SR_NO  "
            Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Membership No where Referred party has been used in past 
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CheckUsageAsPastMember(Org_Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT MS_NO FROM MEMBERSHIP_INFO WHERE REC_STATUS IN (0,1,2) AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MS_AB_ID = '" & Org_Rec_ID & "'"
            Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_INFO, Query, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets  discontinued member count in the wing other than the currently selected
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetCountForWing2WingConversion(Param As Param_GetCountForWing2WingConversion, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT  COUNT(*) FROM MEMBERSHIP_INFO WHERE MS_AB_ID = '" & Param.AB_ID & "' AND MS_WING_ID <> '" & Param.Wing_Id & "' AND MS_CLOSE_DATE IS NOT null AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_INFO, Query, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
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
        ''' <remarks>RealServiceFunctions.Membership_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Membership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Wing_Id = Nothing Then
                InParam.Wing_Id = " NULL "
            Else
                InParam.Wing_Id = "'" & InParam.Wing_Id & "'"
            End If
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Membership_info(MS_CEN_ID,MS_COD_YEAR_ID,MS_AB_ID,MS_SI_ID,MS_WING_ID,MS_START_DATE,MS_OLD_NO,MS_NO,MS_OTHER_DETAIL,MS_TR_ID," & _
                                              "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                              ") VALUES(" & _
                                              "" & inBasicParam.openCenID.ToString & "," & _
                                              "" & inBasicParam.openYearID.ToString & "," & _
                                              "'" & InParam.AB_ID & "', " & _
                                              "'" & InParam.SUBS_ID & "', " & _
                                              "" & InParam.Wing_Id & ", " & _
                                              " " & If(IsDate(InParam.StartDate), "'" & Convert.ToDateTime(InParam.StartDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                              "'" & InParam.Mem_Old_No & "', " & _
                                              "'" & InParam.Mem_No & "', " & _
                                              "'" & InParam.OtherDetails & "', " & _
                                              " " & If(InParam.Txn_ID.Length > 0, "'" & InParam.Txn_ID & "'", " NULL ") & ", " & _
                                              "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.MEMBERSHIP_INFO, OnlineQuery, inBasicParam, InParam.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Balances
        ''' </summary>
        ''' <param name="InBal"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_InsertBalances</remarks>
        Public Shared Function InsertBalances(ByVal InBal As Parameter_InsertBalances_Membership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InBal.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Membership_Balances_Info(MB_CEN_ID,MB_COD_YEAR_ID,MS_REC_ID,MB_SR_NO,MB_SI_ID,MB_ITEM_ID,MB_DATE,MB_PERIOD_FROM,MB_PERIOD_TO,MB_AMOUNT,MB_TR_ID," & _
                                              "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                              ") VALUES(" & _
                                              "" & inBasicParam.openCenID.ToString & "," & _
                                              "" & inBasicParam.openYearID.ToString & "," & _
                                              "'" & InBal.REC_ID & "', " & _
                                              " " & InBal.Sr_No & " , " & _
                                              "'" & InBal.SUBS_ID & "', " & _
                                              "'" & InBal.Item_ID & "', " & _
                                              " " & If(IsDate(InBal.Entry_Date), "'" & Convert.ToDateTime(InBal.Entry_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                              " " & If(IsDate(InBal.Period_From), "'" & Convert.ToDateTime(InBal.Period_From).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                              " " & If(IsDate(InBal.Period_To), "'" & Convert.ToDateTime(InBal.Period_To).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                              "" & InBal.Amount & ", " & _
                                              " " & If(InBal.Txn_ID.Length > 0, "'" & InBal.Txn_ID & "'", " NULL ") & ", " & _
                                              "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & System.Guid.NewGuid().ToString() & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, OnlineQuery, inBasicParam, InBal.REC_ID, Nothing, AddTime)
            Return True
        End Function
        ''' <summary>
        ''' Insert Subscription List-Store Procedure
        ''' </summary>
        ''' <param name="Ins_id"></param>
        ''' <param name="Sr.No."></param>
        ''' <param name="SI_Name"></param>
        ''' <param name="SI_Category"></param>
        ''' <param name="SI_StartMonth"></param>
        ''' <param name="SI_TotMonth"></param>
        ''' <param name="Rec_Add_Dt"></param>
        ''' <param name="Rec_AddBy"></param>
        ''' <param name="Rec_Status"></param>
        ''' <param name="Rec_StatusDt"></param>
        ''' <param name="Rec_StatusBy"></param>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Subscription List</remarks>
        Public Shared Function InsertSubscriptionList(ByVal Param As Param_Inset_SubcriptionList, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"InsId", "Sr.No.", "SI_Name", "SI_Category", "SI_StartMoth", "SI_TotalMonth", "Recd_AddDt", "Recd_AddBy", "Recd_Status", "Recd_StatusDt", "Recd_StatusBy", "Recd_Id"}
            Dim values As Object() = {Param.Ins_Id, Param.SrNo, Param.SIName, Param.SI_Category, Param.SI_StartMonth, Param.SI_TotMonth, Param.Rec_Add_Dt, Param.Rec_AddBy, Param.Rec_Status, Param.Rec_StatusDt, Param.Rec_StatusBy, Param.Rec_Id}
            Dim dbTypes As System.Data.DbType() = {DbType.String, DbType.Int32, DbType.String, DbType.String, DbType.Int16, DbType.Int16, DbType.DateTime, DbType.String, DbType.Int16, DbType.DateTime, DbType.String, DbType.String}
            Dim lengths As Integer() = {5, 1000, 255, 255, 26, 26, 26, 255, 255, 26, 255, 36}
            dbService.InsertBySP(ConnectOneWS.Tables.SUBSCRIPTION_INFO, "sp_Insert_Subscription_Info", paramters, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        ''' <summary>
        ''' Insert Subscription Fee - StoreProcedure 
        ''' </summary>
        ''' <param name="SI_RecID"></param>
        ''' <param name="Sub_EntFee"></param>
        ''' <param name="Sub_Fee"></param>
        ''' <param name="Sub_RenewFee"></param>
        ''' <param name="Sub_EffDate"></param>
        ''' <param name="Rec_Add_Dt"></param>
        ''' <param name="Rec_AddBy"></param>
        ''' <param name="Rec_EditOn"></param>
        ''' <param name="Rec_EditBy"></param>
        ''' <param name="Rec_Status"></param>
        ''' <param name="Rec_StatusDt"></param>
        ''' <param name="Rec_StatusBy"></param>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Subscription Fee</remarks>
        Public Shared Function InsertSubscriptionFee(ByVal Param As Param_Insert_SubscriptionFee, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"SI_Rec_Id", "Sub_EntFee", "Sub_Fee", "Sub_RenewFee", "Sub_EffDate", "Rec_Add_Dt", "Rec_AddBy", "Rec_EditOn", "Rec_EditBy", "Rec_Status", "Rec_StatusDt", "Recd_StatusBy", "Recd_Id"}
            Dim values As Object() = {Param.SI_Rec_Id, Param.Sub_EntFee, Param.Sub_Fee, Param.Sub_RenewFee, Param.Sub_EffDate, Param.Rec_Add_Dt, Param.Rec_Add_By, Param.Rec_EditOn, Param.Rec_EditBy, Param.Rec_Status, Param.Rec_StatusDt, Param.Rec_StatusBy, Param.Rec_Id}
            Dim dbTypes As System.Data.DbType() = {DbType.String, DbType.Int16, DbType.Int16, DbType.Int16, DbType.DateTime2, DbType.DateTime2, DbType.String, DbType.DateTime2, DbType.String, DbType.Int16, DbType.DateTime2, DbType.String, DbType.String}
            Dim lengths As Integer() = {36, 255, 255, 255, 26, 26, 255, 26, 255, 10, 26, 255, 36}
            dbService.InsertBySP(ConnectOneWS.Tables.SUBSCRIPTION_FEE_INFO, "sp_Insert_SubscriptionFee_Info", paramters, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Updates MembershipInfo
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Membership, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.Wing_Id = Nothing Then
                UpParam.Wing_Id = " NULL "
            Else
                UpParam.Wing_Id = "'" & UpParam.Wing_Id & "'"
            End If
            Dim OnlineQuery As String = " UPDATE Membership_info SET " & _
                                        "MS_AB_ID          ='" & UpParam.AB_ID & "', " & _
                                        "MS_SI_ID          ='" & UpParam.SUBS_ID & "', " & _
                                        "MS_START_DATE     = " & If(IsDate(UpParam.StartDate), "'" & Convert.ToDateTime(UpParam.StartDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        "MS_WING_ID        = " & UpParam.Wing_Id & " , " & _
                                        "MS_NO             ='" & UpParam.Mem_No & "', " & _
                                        "MS_OLD_NO         ='" & UpParam.Mem_Old_No & "', " & _
                                        "MS_OTHER_DETAIL   ='" & UpParam.OtherDetails & "', " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'" 'Bug #4964 fix 
            '",REC_STATUS        =" & UpParam.Status_Action & "," & _
            '"REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
            '"REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _

            dbService.Update(ConnectOneWS.Tables.MEMBERSHIP_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function
        ''' <summary>
        ''' Update Subscription List
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="SI_Name"></param>
        ''' <param name="SI_Category"></param>
        ''' <param name="SI_StartMonth"></param>
        ''' <param name="SI_TotMonth"></param>        
        ''' <param name="Rec_EditDate"></param>
        ''' <param name="Rec_Edit_By"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Subscription List</remarks>
        Public Shared Function UpdateSubscriptionList(ByVal Param As Param_Update_SubscirptionList, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"Rec_ID", "SI_Name", "SI_Category", "SI_StartMoth", "SI_TotMonth", "Rec_EditDate", "Rec_Edit_By"}
            Dim values As Object() = {Param.Rec_ID, Param.SI_Name, Param.SI_Category, Param.SI_StartMonth, Param.SI_TotMonth, Param.Rec_EditDate, Param.Rec_Edit_By}
            Dim dbTypes As System.Data.DbType() = {DbType.String, DbType.String, DbType.String, DbType.Int16, DbType.Int16, DbType.DateTime2, DbType.String}
            Dim lengths As Integer() = {36, 255, 255, 26, 26, 26, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.SUBSCRIPTION_INFO, "sp_Update_Subscription_Info", paramters, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        ''' <summary>
        ''' Update Subscription Fee
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="Sub_EntFee"></param>
        ''' <param name="Sub_Fee"></param>
        ''' <param name="Sub_RenewFee"></param>
        ''' <param name="Sub_EffDate"></param>        
        ''' <param name="Sub_EditOn"></param>
        ''' <param name="Rec_Edit_By"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Subscription Fee</remarks>
        Public Shared Function UpdateSubscriptionFee(ByVal Param As Param_Update_SubscriptionFee, ByVal inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"Rec_ID", "Sub_EntFee", "Sub_Fee", "Sub_RenewFee", "Sub_EffDate", "Sub_EditOn", "Rec_Edit_By"}
            Dim values As Object() = {Param.Rec_ID, Param.Sub_EntFee, Param.Sub_Fee, Param.Sub_RenewFee, Param.Sub_EffDate, Param.Sub_EditOn, Param.Rec_Edit_By}
            Dim dbTypes As System.Data.DbType() = {DbType.String, DbType.Int16, DbType.Int16, DbType.Int16, DbType.DateTime2, DbType.DateTime2, DbType.String}
            Dim lengths As Integer() = {36, 255, 255, 255, 255, 26, 26, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.SUBSCRIPTION_FEE_INFO, "sp_Update_SubscriptionFee_Info", paramters, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function


        ''' <summary>
        ''' Close
        ''' </summary>
        ''' <param name="Cls"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Close</remarks>
        Public Shared Function Close(ByVal Cls As Parameter_Close_Membership, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Membership_info SET " & _
                                        "MS_CLOSE_DATE     = " & If(IsDate(Cls.CloseDate), "'" & Convert.ToDateTime(Cls.CloseDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        "MS_CLOSE_REMARKS  ='" & Cls.Reason & "', " & _
                                        " " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                        "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                        "  WHERE REC_ID    ='" & Cls.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MEMBERSHIP_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        ''' <summary>
        ''' Reopens membership
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Reopen</remarks>
        Public Shared Function Reopen(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Membership_info SET " & _
                                        "MS_CLOSE_DATE     = NULL, " & _
                                        "MS_CLOSE_REMARKS  = NULL, " & _
                                        " " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                        "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                        "  WHERE REC_ID    ='" & Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.MEMBERSHIP_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function InsertMembership_Txn(inParam As Param_Txn_Insert_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMembership Is Nothing Then
                If Not Insert(inParam.param_InsertMembership, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertBalances Is Nothing Then
                If Not InsertBalances(inParam.param_InsertBalances, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'End Using
            'txn.Complete()
            'End Using
            Return True
        End Function

        Public Shared Function UpdateMembership_Txn(upParam As Param_Txn_Update_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.MEMBERSHIP_INFO, upParam.param_UpdateMembership.Rec_ID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            'Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMembership Is Nothing Then
                If Not Update(upParam.param_UpdateMembership, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.RecID_DeleteBalances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, "MS_REC_ID    ='" & upParam.RecID_DeleteBalances & "'", inBasicParam)
            End If
            If Not upParam.param_InsertBalances Is Nothing Then
                If Not InsertBalances(upParam.param_InsertBalances, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'End Using
            'txn.Complete()
            'End Using
            Return True
        End Function

        Public Shared Function DeleteMembership_Txn(delParam As Param_Txn_Delete_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            ' Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.RecID_DeleteBalances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, "MS_REC_ID    ='" & delParam.RecID_DeleteBalances & "'", inBasicParam)
            End If
            If Not delParam.RecID_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.MEMBERSHIP_INFO, delParam.RecID_Delete, inBasicParam)
            End If
            'End Using
            'txn.Complete()
            'End Using
            Return True
        End Function

        ''' <summary>
        ''' Delete Subscription List -Store Procedure
        ''' </summary>
        ''' <param name="Rec_Id"></param>        
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Delete Subscription List</remarks>
        Public Shared Function DeleteSubscriptionList(ByVal Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"Rec_Id"}
            Dim values As Object() = {Rec_Id}
            Dim dbTypes As System.Data.DbType() = {DbType.String}
            Dim lengths As Integer() = {36}
            dbService.InsertBySP(ConnectOneWS.Tables.SUBSCRIPTION_INFO, "sp_Delete_Subscription_Info", paramters, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Delete Subscription Fee -Store Procedure
        ''' </summary>
        ''' <param name="SI_Rec_Id"></param>
        ''' <param name="Rec_Id"></param>        
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Delete Subscription Fee</remarks>
        Public Shared Function DeleteSubscriptionFee(ByVal param As Param_Delet_SubscriptionFee, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"SI_Rec_Id", "Rec_Id"}
            Dim values As Object() = {param.SI_Rec_Id, param.Rec_Id}
            Dim dbTypes As System.Data.DbType() = {DbType.String, DbType.String}
            Dim lengths As Integer() = {36, 36}
            dbService.InsertBySP(ConnectOneWS.Tables.SUBSCRIPTION_FEE_INFO, "sp_Delete_SubscriptionFee_Info", paramters, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

    End Class
#End Region
#Region "Accounts"
    <Serializable>
    Public Class Membership_Receipt_Register
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetList_Membership_Receipt_Register
            Public FromDate As Date
            Public ToDate As Date
            Public openYearSdt As Date
        End Class
        <Serializable>
        Public Class Param_InsertReceipt_Membership_Receipt_Register
            Public M_ID As String
            Public VDate As String
            Public openYearSdt As Date
            Public openYearID As Integer
        End Class
        <Serializable>
        Public Class Param_DeleteReceipt_Membership_Receipt_Register
            Public Reason As String
            Public Rec_Id As String
        End Class

#End Region

        ''' <summary>
        ''' Get Max Transaction Date
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_GetMaxTransactionDate</remarks>
        Public Shared Function GetMaxTransactionDate(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT MAX(TR_DATE) AS MAXDATE FROM Transaction_Info WHERE  REC_STATUS IN (0,1,2)  AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TR_CODE IN (12,13) "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_GetList_Membership_Receipt_Register, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT dbo.LPAD( DATEDIFF(DD,DATEADD(DD,-1,'" & Param.openYearSdt.ToString(Common.Server_Date_Format_Long) & "'),CAST(MR_DATE AS DATE)) ,3,'0') + dbo.LPAD(COALESCE(MR.MR_NO,''),'4','0')  AS 'Receipt No.', MR.MR_DATE AS 'Receipt Date',TM.TR_DATE AS 'Voucher Date',CASE WHEN TM.TR_CODE=12 THEN 'New' ELSE 'Renew' END AS 'Entry', CURR_AB.C_NAME AS 'Member Name',MS.MS_NO AS 'Membership No.',SI.SI_NAME AS 'Membership',MS.MS_START_DATE AS 'Start Date', " & _
                                        " CASE WHEN SI.SI_CATEGORY='LIFETIME' THEN (SELECT 'Life Time') ELSE (SELECT dbo.fn_FORMATDATE(MIN(MB_PERIOD_FROM),'DD MON, YYYY')+'  to  '+dbo.fn_FORMATDATE(MAX(MB_PERIOD_TO),'DD MON, YYYY') FROM MEMBERSHIP_BALANCES_INFO AS MB  WHERE MS.REC_ID=MB.MS_REC_ID AND MB.MB_TR_ID=TM.REC_ID AND MB.REC_STATUS  IN (0,1,2) ) END AS 'Period', " & _
                                        " CASE WHEN COALESCE(CURR_AB.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END AS 'Centre Name',  CASE WHEN COALESCE(AB.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END  AS 'Centre UID',WS.WING_NAME AS 'Wing',TM.TR_CASH_AMT AS 'Cash Amount',TM.TR_BANK_AMT AS 'Bank Amount',TM.TR_SUB_AMT AS 'Total Amount',RTRIM(LTRIM(COALESCE(TI.TR_REMARKS,'') + ' ' + COALESCE(TI.TR_REFERENCE,''))) AS 'Other Details' , " & _
                                        " MR.REC_ID AS 'Receipt ID',TM.TR_CODE as 'Entry Tr. Code',TM.REC_ID as ID ," & Common.Rec_Detail_OrgField("TM") & " " & _
                                        " FROM  TRANSACTION_D_MASTER_INFO       AS TM     " & _
                                        " LEFT  JOIN  (SELECT MAX(TR_REMARKS) TR_REMARKS,MAX(TR_REFERENCE)TR_REFERENCE,MAX(REC_STATUS) REC_STATUS,MAX(TR_CEN_ID) TR_CEN_ID,MAX(TR_CODE) TR_CODE,MAX(TR_DATE) TR_DATE,TR_M_ID  FROM TRANSACTION_INFO WHERE TR_SR_NO = 1 AND TR_CODE IN (12,13,16) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " GROUP BY TR_M_ID )   AS TI ON (TM.REC_ID= TI.TR_M_ID )  " & _
                                        " INNER JOIN MEMBERSHIP_INFO            AS MS ON (MS.REC_ID         = TM.TR_REF_ID  AND MS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT  JOIN MEMBERSHIP_RECEIPT_INFO    AS MR ON (TM.REC_ID         = MR.MR_TR_M_ID AND MR.MR_CANCEL IS NULL AND MR.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN ADDRESS_BOOK               AS AB ON (MS.MS_AB_ID       = AB.REC_ID AND AB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN ADDRESS_BOOK               AS CURR_AB ON (AB.C_ORG_REC_ID       = CURR_AB.C_ORG_REC_ID AND CURR_AB.C_COD_YEAR_ID =  " & inBasicParam.openYearID.ToString & " AND CURR_AB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT OUTER JOIN WINGS_INFO            AS WS ON (MS.MS_WING_ID     = WS.REC_ID AND WS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " INNER JOIN SUBSCRIPTION_INFO          AS SI ON (MS.MS_SI_ID       = SI.REC_ID AND SI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                        " LEFT  JOIN CENTRE_INFO                AS C  ON (CURR_AB.C_CLASS_CEN_ID = C.CEN_ID  AND CURR_AB.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                        " LEFT  JOIN OVERSEAS_CENTRE_INFO       AS O  ON (CURR_AB.C_CLASS_CEN_ID = O.CEN_ID  AND CURR_AB.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                        " Where TM.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TM.TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TM.TR_CODE IN (12,13,16) AND (CAST(TM.TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' ) " & _
                                        " and   TI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TI.TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND TI.TR_CODE IN (12,13,16) AND (CAST(TI.TR_DATE AS DATE) BETWEEN '" & Format(Param.FromDate, Common.Server_Date_Format_Short) & "' AND '" & Format(Param.ToDate, Common.Server_Date_Format_Short) & "' ) " & _
                                        " Order By dbo.LPAD( DATEPART(dy,MR.MR_DATE),3,'0') + dbo.LPAD(COALESCE(MR.MR_NO,''),'4','0') ,TM.TR_DATE "
            Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Receipt Count
        ''' </summary>
        ''' <param name="M_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_GetReceiptCount</remarks>
        Public Shared Function GetReceiptCount(ByVal M_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM MEMBERSHIP_RECEIPT_INFO  WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")  AND MR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MR_TR_M_ID ='" & M_ID & "'  AND MR_CANCEL IS NULL "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_RECEIPT_INFO, OnlineQuery, ConnectOneWS.Tables.MEMBERSHIP_RECEIPT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert Receipt
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_InsertReceipt</remarks>
        Public Shared Function InsertReceipt(ByVal Param As Param_InsertReceipt_Membership_Receipt_Register, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery1 As String = "SELECT  CASE WHEN MAX(MR_NO) IS NULL THEN 1 ELSE MAX(MR_NO)+1 END  AS NEW_NO FROM   MEMBERSHIP_RECEIPT_INFO  WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")  AND MR_CEN_ID=" & inBasicParam.openCenID.ToString & " " & _
                                         " AND   DATEDIFF(DD,CAST(MR_DATE AS DATE),DATEADD(DD,-1,'" & Param.openYearSdt.ToString(Common.Server_Date_Format_Long) & "')) = DATEDIFF(DD,'" & Convert.ToDateTime(Param.VDate).ToString(Common.Server_Date_Format_Long) & "',DATEADD(DD,-1,'" & Param.openYearSdt.ToString(Common.Server_Date_Format_Long) & "')) "
            Dim xReceiptNo As Double = dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_RECEIPT_INFO, onlineQuery1, ConnectOneWS.Tables.MEMBERSHIP_RECEIPT_INFO.ToString(), inBasicParam)
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO MEMBERSHIP_RECEIPT_INFO(MR_CEN_ID,MR_COD_YEAR_ID,MR_TR_M_ID,MR_DATE,MR_NO," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & Param.openYearID.ToString & "," & _
                                        "'" & Param.M_ID & "'," & _
                                        " " & If(IsDate(Param.VDate), "'" & Convert.ToDateTime(Param.VDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        " " & xReceiptNo & " ," & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.MEMBERSHIP_RECEIPT_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' Delete Receipt
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_DeleteReceipt</remarks>
        Public Overloads Shared Function DeleteReceipt(ByVal Param As Param_DeleteReceipt_Membership_Receipt_Register, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE MEMBERSHIP_RECEIPT_INFO SET " & _
                                        " MR_CANCEL         = 'YES'," & _
                                        " MR_CANCEL_REMARKS  ='" & Param.Reason & "', " & _
                                        " " & _
                                        " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                        " REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                        " REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                        " WHERE REC_ID    ='" & Param.Rec_Id & "'"
            dbService.Update(ConnectOneWS.Tables.MEMBERSHIP_RECEIPT_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function DeleteReceiptRef(ByVal Param As Param_DeleteReceipt_Membership_Receipt_Register, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE MEMBERSHIP_RECEIPT_INFO SET " & _
                                        " MR_TR_M_ID         = NULL, " & _
                                        " " & _
                                        " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                        " REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                        " REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                        " WHERE MR_TR_M_ID  ='" & Param.Rec_Id & "'"
            dbService.Update(ConnectOneWS.Tables.MEMBERSHIP_RECEIPT_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class Voucher_Membership
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherMembership
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public Ref_ID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherMembership
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public SUB_Cr_Led_ID As String
            Public SUB_Dr_Led_ID As String
            Public Amount As Double
            Public Mode As String
            Public Ref_BANK_ID As String
            Public Ref_Branch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_CDate As String
            Public Party1 As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public MasterTxnID As String
            Public SrNo As String
            Public Status_Action As String
            Public RecID As String
            Public CrossRefId As String = ""
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertItem_VoucherMembership
            Public Txn_M_ID As String
            Public TxnSrNo As Integer
            Public ItemID As String
            Public LedID As String
            Public Type As String
            Public PartyReq As String
            Public Profile As String
            Public ItemName As String
            Public Head As String
            Public Qty As Double
            Public Unit As String
            Public Rate As Double
            Public Amount As Double
            Public Remarks As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertPayment_VoucherMembership
            Public TxnMID As String
            Public Type As String
            Public SrNo As String
            Public Mode As String
            Public RefID As String
            Public RefBranch As String
            Public RefNo As String
            Public RefDate As String
            Public ClearingDate As String
            Public RefAmount As Double
            Public Dep_BA_ID As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherMembership
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public SrNo As Integer
            Public Status_Action As String
            Public RecID As String
            ' Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_UpdateMaster_VoucherMembership
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public Ref_ID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_VoucherMembership
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Sub_Dr_Led_ID As String
            Public Mode As String
            Public RefBankID As String
            Public RefBranch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_ChequeDate As String
            Public Amount As Double
            Public DonorID As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            'Public Status_Action As String
            Public RecID As String
        End Class

        <Serializable>
        Public Class Parameter_UpdatePurpose_VoucherMembership
            Public PurposeID As String
            Public Amount As Double
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherMembership
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherMembership
            Public Insert() As Parameter_Insert_VoucherMembership = Nothing
            Public param_InsertMembership As Membership.Parameter_Insert_Membership = Nothing
            Public InsertItem() As Parameter_InsertItem_VoucherMembership = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherMembership = Nothing
            Public InsertBalNotAdvFee() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertBalFeeInAdvance()() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertPayment() As Parameter_InsertPayment_VoucherMembership = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherMembership
            Public param_UpdateMaster As Parameter_UpdateMaster_VoucherMembership
            Public MID_Delete As String = Nothing
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeleteBalances As String = Nothing
            Public MID_DeleteMembership As String = Nothing
            Public Insert() As Parameter_Insert_VoucherMembership = Nothing
            Public param_InsertMembership As Membership.Parameter_Insert_Membership = Nothing
            Public InsertItem() As Parameter_InsertItem_VoucherMembership = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherMembership = Nothing
            Public InsertBalNotAdvFee() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertBalFeeInAdvance()() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertPayment() As Parameter_InsertPayment_VoucherMembership = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherMembership
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeleteBalances As String = Nothing
            Public MID_DeleteMembership As String = Nothing
            Public MID_DeleteReceiptRef As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
        End Class

#End Region
        ''' <summary>
        ''' Get MasterID
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_GetMasterID</remarks>
        Public Shared Function GetMasterID(ByVal Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_M_ID FROM transaction_info WHERE REC_ID ='" & Rec_Id & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Txn Bank Payment Detail
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_GetTxnBankPaymentDetail</remarks>
        Public Shared Function GetTxnBankPaymentDetail(ByVal MasterID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " select TP.*,BI.BI_BANK_NAME AS REF_BANK_NAME,BI2.BI_BANK_NAME AS DEP_BANK_NAME,BB.BB_BRANCH_NAME AS DEP_BRANCH_NAME,BA.BA_ACCOUNT_NO AS DEP_BANK_ACC_NO, BA.REC_EDIT_ON AS BA_EDIT_ON " &
                                  " FROM TRANSACTION_D_PAYMENT_INFO AS TP " &
                                  " LEFT JOIN bank_info             AS BI   ON (TP.TR_REF_ID        = BI.REC_ID   AND BI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " &
                                  " LEFT JOIN bank_account_info     AS BA   ON (TP.TR_DEP_BA_REC_ID = BA.REC_ID   AND BA.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " &
                                  " LEFT JOIN bank_branch_info      AS BB   ON (BA.BA_BRANCH_ID     = BB.REC_ID   AND BB.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " &
                                  " LEFT JOIN bank_info             AS BI2  ON (BB.BI_BANK_ID       = BI2.REC_ID  AND BI2.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " &
                                  " WHERE TP.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " &
                                  "   AND TP.TR_M_ID= '" & MasterID & "' AND TP.TR_PAY_TYPE='BANK' ORDER BY TP.TR_SR_NO"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Last Period
        ''' </summary>
        ''' <param name="M_Rec_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_GetLastPeriod</remarks>
        Public Shared Function GetLastPeriod(ByVal M_Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT  MAX(MB_PERIOD_FROM) AS 'PERIOD_FROM' FROM membership_balances_info WHERE MB_TR_ID='" & M_Rec_Id & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, Query, ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert Master Info
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertMasterInfo</remarks>
        Public Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_REF_ID,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_ADVANCE_AMT,TR_LB_AMT,TR_CREDIT_AMT,TR_TDS_AMT," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InMInfo.TxnCode & "'," &
                                                  "'" & InMInfo.VNo & "', " &
                                                  " " & If(IsDate(InMInfo.TDate), "'" & Convert.ToDateTime(InMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & InMInfo.PartyID & "', " &
                                                  "'" & InMInfo.Ref_ID & "', " &
                                                  " " & InMInfo.SubTotal & "," &
                                                  " " & InMInfo.Cash & "," &
                                                  " " & InMInfo.Bank & "," &
                                                  " " & 0 & "," &
                                                  " " & 0 & "," &
                                                  " " & 0 & "," &
                                                  " " & 0 & "," &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
            Return True
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
        ''' <remarks>RealServiceFunctions.VoucherMembership_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.SUB_Cr_Led_ID.Length = 0 Then InParam.SUB_Cr_Led_ID = "NULL" Else If Not InParam.SUB_Cr_Led_ID.StartsWith("'") Then InParam.SUB_Cr_Led_ID = "'" & InParam.SUB_Cr_Led_ID & "'"
            If InParam.SUB_Dr_Led_ID.Length = 0 Then InParam.SUB_Dr_Led_ID = "NULL" Else If Not InParam.SUB_Dr_Led_ID.StartsWith("'") Then InParam.SUB_Dr_Led_ID = "'" & InParam.SUB_Dr_Led_ID & "'"
            If InParam.Ref_BANK_ID.Length = 0 Then InParam.Ref_BANK_ID = "NULL" Else If Not InParam.Ref_BANK_ID.StartsWith("'") Then InParam.Ref_BANK_ID = "'" & InParam.Ref_BANK_ID & "'"
            If InParam.Ref_Branch.Length = 0 Then InParam.Ref_Branch = "NULL" Else If Not InParam.Ref_Branch.StartsWith("'") Then InParam.Ref_Branch = "'" & InParam.Ref_Branch & "'"
            If InParam.Ref_No.Length = 0 Then InParam.Ref_No = "NULL" Else If Not InParam.Ref_No.StartsWith("'") Then InParam.Ref_No = "'" & InParam.Ref_No & "'"
            If InParam.MasterTxnID.Length = 0 Then InParam.MasterTxnID = "NULL" Else If Not InParam.MasterTxnID.StartsWith("'") Then InParam.MasterTxnID = "'" & InParam.MasterTxnID & "'"
            If InParam.Mode.Length = 0 Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.Party1.Length = 0 Then InParam.Party1 = "NULL" Else If Not InParam.Party1.StartsWith("'") Then InParam.Party1 = "'" & InParam.Party1 & "'"
            If InParam.SrNo.Length = 0 Then InParam.SrNo = "NULL"
            If InParam.CrossRefId.Length = 0 Then InParam.CrossRefId = "NULL" Else If Not InParam.CrossRefId.StartsWith("'") Then InParam.CrossRefId = "'" & InParam.CrossRefId & "'"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_SUB_DR_LED_ID," &
                                        "TR_MODE,TR_REF_BANK_ID, TR_REF_BRANCH, TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_AMOUNT,TR_AB_ID_1,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_M_ID,TR_SR_NO,TR_TRF_CROSS_REF_ID," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                        ") VALUES(" &
                                                        "" & inBasicParam.openCenID.ToString & "," &
                                                        "" & inBasicParam.openYearID.ToString & "," &
                                                        " " & InParam.TransCode & "," &
                                                            "'" & InParam.VNo & "', " &
                                                            " " & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                            "'" & InParam.ItemID & "', " &
                                                            "'" & InParam.Type & "', " &
                                                            " " & InParam.Cr_Led_ID & " , " &
                                                            " " & InParam.Dr_Led_ID & " , " &
                                                            " " & InParam.SUB_Cr_Led_ID & " , " &
                                                            " " & InParam.SUB_Dr_Led_ID & " , " &
                                                            " " & InParam.Mode & " , " &
                                                            " " & InParam.Ref_BANK_ID & " , " &
                                                            " " & InParam.Ref_Branch & " , " &
                                                            " " & InParam.Ref_No & " , " &
                                                            " " & If(IsDate(InParam.Ref_Date), "'" & Convert.ToDateTime(InParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                            " " & If(IsDate(InParam.Ref_CDate), "'" & Convert.ToDateTime(InParam.Ref_CDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                            " " & InParam.Amount & ", " &
                                                            " " & InParam.Party1 & ", " &
                                                            "'" & InParam.Narration & "', " &
                                                            "'" & InParam.Remarks & "', " &
                                                            "'" & InParam.Reference & "', " &
                                                            " " & InParam.MasterTxnID & " , " &
                                                            " " & InParam.SrNo & " , " &
                                                            " " & InParam.CrossRefId & " , " &
                                                          "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Item
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertItem</remarks>
        Public Shared Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InItem.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_ITEM_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_SR_NO,TR_ITEM_ID,TR_LED_ID,TR_TRANS_TYPE,TR_PARTY_REQ,TR_PROFILE,TR_ITEM_NAME,TR_ITEM_HEAD,TR_QTY,TR_UNIT,TR_RATE,TR_AMOUNT,TR_REMARKS," &
                                            "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                            ") VALUES(" &
                                            "" & inBasicParam.openCenID.ToString & "," &
                                            "" & inBasicParam.openYearID.ToString & "," &
                                            "'" & InItem.Txn_M_ID & "', " &
                                            " " & InItem.TxnSrNo & " , " &
                                            "'" & InItem.ItemID & "', " &
                                            "'" & InItem.LedID & "', " &
                                            "'" & InItem.Type & "', " &
                                            "'" & InItem.PartyReq & "', " &
                                            "'" & InItem.Profile & "', " &
                                            "'" & InItem.ItemName & "', " &
                                            "'" & InItem.Head & "', " &
                                            " " & InItem.Qty & ", " &
                                            "'" & InItem.Unit & "', " &
                                            " " & InItem.Rate & ", " &
                                            " " & InItem.Amount & ", " &
                                            "'" & InItem.Remarks & "', " &
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InItem.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, OnlineQuery, inBasicParam, InItem.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Payment
        ''' </summary>
        ''' <param name="InPmt"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertPayment</remarks>
        Public Shared Function InsertPayment(ByVal InPmt As Parameter_InsertPayment_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If InPmt.RefID.Length = 0 Then InPmt.RefID = "NULL" Else If Not InPmt.RefID.StartsWith("'") Then InPmt.RefID = "'" & InPmt.RefID & "'"
            If InPmt.Dep_BA_ID.Length = 0 Then InPmt.Dep_BA_ID = "NULL" Else If Not InPmt.Dep_BA_ID.StartsWith("'") Then InPmt.Dep_BA_ID = "'" & InPmt.Dep_BA_ID & "'"
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPmt.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_MODE,TR_REF_ID,TR_REF_BRANCH,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_AMT,TR_DEP_BA_REC_ID," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                        ") VALUES(" &
                                        "" & inBasicParam.openCenID.ToString & "," &
                                        "" & inBasicParam.openYearID.ToString & "," &
                                        "'" & InPmt.TxnMID & "', " &
                                        "'" & InPmt.Type & "', " &
                                        " " & InPmt.SrNo & " , " &
                                        "'" & InPmt.Mode & "', " &
                                        " " & InPmt.RefID & " , " &
                                        "'" & InPmt.RefBranch & "', " &
                                        "'" & InPmt.RefNo & "', " &
                                        " " & If(IsDate(InPmt.RefDate), "'" & Convert.ToDateTime(InPmt.RefDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                        " " & If(IsDate(InPmt.ClearingDate), "'" & Convert.ToDateTime(InPmt.ClearingDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                        " " & InPmt.RefAmount & ", " &
                                        " " & InPmt.Dep_BA_ID & " , " &
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPmt.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InPmt.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Purpose
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InPurpose.TxnID & "'," &
                                                  "'" & InPurpose.PurposeID & "', " &
                                                  " " & InPurpose.Amount & ", " &
                                                  " " & InPurpose.SrNo & ", " &
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update Master
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_UpdateMaster</remarks>
        Public Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " &
                                            " TR_VNO         ='" & UpParam.VNo & "', " &
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                            " TR_AB_ID_1     ='" & UpParam.PartyID & "', " &
                                            " TR_REF_ID      ='" & UpParam.Ref_ID & "', " &
                                            " TR_SUB_AMT     = " & UpParam.SubTotal & ", " &
                                            " TR_CASH_AMT    = " & UpParam.Cash & ", " &
                                            " TR_BANK_AMT    = " & UpParam.Bank & ", " &
                                            " TR_ADVANCE_AMT = " & 0 & ", " &
                                            " TR_LB_AMT      = " & 0 & ", " &
                                            " TR_CREDIT_AMT  = " & 0 & ", " &
                                            " TR_TDS_AMT     = " & 0 & ", " &
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="Updt"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_Update</remarks>
        Public Shared Function Update(ByVal Updt As Parameter_Update_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If Updt.Sub_Cr_Led_ID.Trim.Length = 0 Then Updt.Sub_Cr_Led_ID = "NULL" Else Updt.Sub_Cr_Led_ID = "'" & Updt.Sub_Cr_Led_ID & "'"
            If Updt.Sub_Dr_Led_ID.Trim.Length = 0 Then Updt.Sub_Dr_Led_ID = "NULL" Else Updt.Sub_Dr_Led_ID = "'" & Updt.Sub_Dr_Led_ID & "'"
            If Updt.Cr_Led_ID.Length = 0 Then Updt.Cr_Led_ID = "NULL" Else If Not Updt.Cr_Led_ID.StartsWith("'") Then Updt.Cr_Led_ID = "'" & Updt.Cr_Led_ID & "'"
            If Updt.Dr_Led_ID.Length = 0 Then Updt.Dr_Led_ID = "NULL" Else If Not Updt.Dr_Led_ID.StartsWith("'") Then Updt.Dr_Led_ID = "'" & Updt.Dr_Led_ID & "'"
            If Updt.RefBankID.Length = 0 Then Updt.RefBankID = "NULL" Else If Not Updt.RefBankID.StartsWith("'") Then Updt.RefBankID = "'" & Updt.RefBankID & "'"
            If Updt.DonorID.Length = 0 Then Updt.DonorID = "NULL" Else If Not Updt.DonorID.StartsWith("'") Then Updt.DonorID = "'" & Updt.DonorID & "'"
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " &
                                            " TR_VNO         ='" & Updt.VNo & "', " &
                                                " TR_DATE        =" & If(IsDate(Updt.TDate), "'" & Convert.ToDateTime(Updt.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                " TR_ITEM_ID     ='" & Updt.ItemID & "', " &
                                                " TR_TYPE        ='" & Updt.Type & "', " &
                                                " TR_CR_LED_ID   = " & Updt.Cr_Led_ID & " , " &
                                                " TR_DR_LED_ID   = " & Updt.Dr_Led_ID & " , " &
                                                " TR_SUB_CR_LED_ID  =" & Updt.Sub_Cr_Led_ID & ", " &
                                                " TR_SUB_DR_LED_ID  =" & Updt.Sub_Dr_Led_ID & ", " &
                                                " TR_MODE        ='" & Updt.Mode & "', " &
                                                " TR_REF_BANK_ID = " & Updt.RefBankID & " , " &
                                                " TR_REF_BRANCH  ='" & Updt.RefBranch & "', " &
                                                " TR_REF_NO      ='" & Updt.Ref_No & "', " &
                                                " TR_REF_DATE    = " & If(IsDate(Updt.Ref_Date), "'" & Convert.ToDateTime(Updt.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                " TR_REF_CDATE   = " & If(IsDate(Updt.Ref_ChequeDate), "'" & Convert.ToDateTime(Updt.Ref_ChequeDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                " TR_AMOUNT      = " & Updt.Amount & ", " &
                                                " TR_AB_ID_1     = " & Updt.DonorID & " , " &
                                                " TR_NARRATION   ='" & Updt.Narration & "', " &
                                                " TR_REMARKS     ='" & Updt.Remarks & "', " &
                                                " TR_REFERENCE   ='" & Updt.Reference & "', " &
                                                "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                                "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                                "  WHERE REC_ID    ='" & Updt.RecID & "'"

            '",REC_STATUS        =" & Updt.Status_Action & "," & _
            '"REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
            '"REC_STATUS_BY     ='" & inBasicParam.openUserID & "' 
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, Nothing, Updt.TDate)
            Return True
        End Function

        ''' <summary>
        ''' Updates Purpose
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_UpdatePurpose</remarks>
        Public Shared Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Transaction_D_Purpose_Info SET " &
                                         " TR_PURPOSE_MISC_ID    ='" & UpPurpose.PurposeID & "', " &
                                         " TR_AMOUNT             =" & UpPurpose.Amount & ", " &
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                            "  WHERE REC_ID    ='" & UpPurpose.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function InsertMembershipVoucher_Txn(inParam As Param_Txn_Insert_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_Insert_VoucherMembership In inParam.Insert
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.param_InsertMembership Is Nothing Then
                If Not Membership.Insert(inParam.param_InsertMembership, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_InsertItem_VoucherMembership In inParam.InsertItem
                If Not Param Is Nothing Then InsertItem(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertPurpose_VoucherMembership In inParam.InsertPurpose
                If Not Param Is Nothing Then InsertPurpose(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Membership.Parameter_InsertBalances_Membership In inParam.InsertBalNotAdvFee
                If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.InsertBalFeeInAdvance Is Nothing Then
                For Each InBal() As Membership.Parameter_InsertBalances_Membership In inParam.InsertBalFeeInAdvance
                    If Not InBal Is Nothing Then
                        For Each Param In InBal
                            If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime)
                        Next
                    End If
                Next
            End If
            For Each Param As Parameter_InsertPayment_VoucherMembership In inParam.InsertPayment
                If Not Param Is Nothing Then InsertPayment(Param, inBasicParam, RequestTime)
            Next
            'End Using
            'txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function UpdateMembershipVoucher_Txn(upParam As Param_Txn_Update_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            'Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then
                If Not UpdateMaster(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & upParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & upParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteBalances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, "MB_TR_ID    ='" & upParam.MID_DeleteBalances & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteMembership Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_INFO, "MS_TR_ID    ='" & upParam.MID_DeleteMembership & "'", inBasicParam)
            End If
            For Each Param As Parameter_Insert_VoucherMembership In upParam.Insert
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime, CommonParam)
            Next
            If Not upParam.param_InsertMembership Is Nothing Then
                If Not Membership.Insert(upParam.param_InsertMembership, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_InsertItem_VoucherMembership In upParam.InsertItem
                If Not Param Is Nothing Then InsertItem(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Parameter_InsertPurpose_VoucherMembership In upParam.InsertPurpose
                If Not Param Is Nothing Then InsertPurpose(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Membership.Parameter_InsertBalances_Membership In upParam.InsertBalNotAdvFee
                If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime, CommonParam)
            Next
            If Not upParam.InsertBalFeeInAdvance Is Nothing Then
                For Each InBal() As Membership.Parameter_InsertBalances_Membership In upParam.InsertBalFeeInAdvance
                    If Not InBal Is Nothing Then
                        For Each Param In InBal
                            If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime, CommonParam)
                        Next
                    End If
                Next
            End If
            For Each Param As Parameter_InsertPayment_VoucherMembership In upParam.InsertPayment
                If Not Param Is Nothing Then InsertPayment(Param, inBasicParam, RequestTime, CommonParam)
            Next
            'End Using
            'Commit here 
            'txn.Complete()
            'End Using
            Return True
        End Function

        Public Shared Function DeleteMembershipVoucher_Txn(delParam As Param_Txn_Delete_VoucherMembership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & delParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & delParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteBalances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, "MB_TR_ID    ='" & delParam.MID_DeleteBalances & "'", inBasicParam)
            End If

            If Not delParam.MID_DeleteMembership Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_INFO, "MS_TR_ID    ='" & delParam.MID_DeleteMembership & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteReceiptRef Is Nothing Then
                Dim Param As Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register = New Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register()
                Param.Rec_Id = delParam.MID_DeleteReceiptRef
                Membership_Receipt_Register.DeleteReceiptRef(Param, inBasicParam)
            End If

            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            'End Using
            'Commit here 
            'txn.Complete()
            'End Using
            Return True
        End Function
    End Class
    <Serializable>
    Public Class Voucher_Membership_Renewal
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetPartyDetails_VoucherMembershipRenewal
            Public OtherCondition As String
            Public SearchStr As String
            Public Use_Rec_ID As Boolean
            Public Membership_REC_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherMembershipRenewal
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public Ref_ID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherMembershipRenewal
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public SUB_Cr_Led_ID As String
            Public SUB_Dr_Led_ID As String
            Public Amount As Double
            Public Mode As String
            Public Ref_BANK_ID As String
            Public Ref_Branch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_CDate As String
            Public Party1 As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public MasterTxnID As String
            Public SrNo As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam ins
        End Class
        <Serializable>
        Public Class Parameter_InsertItem_VoucherMembershipRenewal
            Public Txn_M_ID As String
            Public TxnSrNo As Integer
            Public ItemID As String
            Public LedID As String
            Public Type As String
            Public PartyReq As String
            Public Profile As String
            Public ItemName As String
            Public Head As String
            Public Qty As Double
            Public Unit As String
            Public Rate As Double
            Public Amount As Double
            Public Remarks As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam ins
        End Class
        <Serializable>
        Public Class Parameter_InsertPayment_VoucherMembershipRenewal
            Public TxnMID As String
            Public Type As String
            Public SrNo As String
            Public Mode As String
            Public RefID As String
            Public RefBranch As String
            Public RefNo As String
            Public RefDate As String
            Public ClearingDate As String
            Public RefAmount As Double
            Public Dep_BA_ID As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam ins
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherMembershipRenewal
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public SrNo As Integer
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam ins
        End Class
        <Serializable>
        Public Class Parameter_UpdateMaster_VoucherMembershipRenewal
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public Ref_ID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_VoucherMembershipRenewal
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Sub_Dr_Led_ID As String
            Public Mode As String
            Public RefBankID As String
            Public RefBranch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_ChequeDate As String
            Public Amount As Double
            Public DonorID As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_UpdatePurpose_VoucherMembershipRenewal
            Public PurposeID As String
            Public Amount As Double
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherMembershipRenewal
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherMembershipRenewal
            Public Insert() As Parameter_Insert_VoucherMembershipRenewal = Nothing
            Public InsertItem() As Parameter_InsertItem_VoucherMembershipRenewal = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherMembershipRenewal = Nothing
            Public param_InsertBalancesLifetime As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertBalances() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertPayment() As Parameter_InsertPayment_VoucherMembershipRenewal = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherMembershipRenewal
            Public param_UpdateMaster As Parameter_UpdateMaster_VoucherMembershipRenewal
            Public MID_Delete As String = Nothing
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeleteMembership As String = Nothing
            Public MID_DeleteBalances As String = Nothing
            Public Insert() As Parameter_Insert_VoucherMembershipRenewal = Nothing
            Public InsertItem() As Parameter_InsertItem_VoucherMembershipRenewal = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherMembershipRenewal = Nothing
            Public param_InsertBalancesLifetime As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertBalances() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertPayment() As Parameter_InsertPayment_VoucherMembershipRenewal = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherMembershipRenewal
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeleteMembership As String = Nothing
            Public MID_DeleteBalances As String = Nothing
            Public MID_DeleteReceiptRef As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
        End Class
#End Region

        ''' <summary>
        ''' Gets MasterID
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetMasterID</remarks>
        Public Shared Function GetMasterID(ByVal Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_M_ID FROM transaction_info WHERE REC_ID ='" & Rec_Id & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Txn Bank Payment Detail
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetTxnBankPaymentDetail</remarks>
        Public Shared Function GetTxnBankPaymentDetail(ByVal MasterID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " select TP.*,BI.BI_BANK_NAME AS REF_BANK_NAME,BI2.BI_BANK_NAME AS DEP_BANK_NAME,BB.BB_BRANCH_NAME AS DEP_BRANCH_NAME,BA.BA_ACCOUNT_NO AS DEP_BANK_ACC_NO ,BA.REC_EDIT_ON AS BA_EDIT_ON " & _
                                  " FROM TRANSACTION_D_PAYMENT_INFO AS TP " & _
                                  " LEFT JOIN bank_info             AS BI   ON (TP.TR_REF_ID        = BI.REC_ID   AND BI.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT JOIN bank_account_info     AS BA   ON (TP.TR_DEP_BA_REC_ID = BA.REC_ID   AND BA.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT JOIN bank_branch_info      AS BB   ON (BA.BA_BRANCH_ID     = BB.REC_ID   AND BB.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT JOIN bank_info             AS BI2  ON (BB.BI_BANK_ID       = BI2.REC_ID  AND BI2.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " WHERE TP.REC_STATUS   IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & _
                                  "   AND TP.TR_M_ID= '" & MasterID & "' AND TP.TR_PAY_TYPE='BANK' ORDER BY TP.TR_SR_NO"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Party Details
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' 
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetPartyDetails</remarks>
        Public Shared Function GetPartyDetails(ByVal Param As Param_GetPartyDetails_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SearchCondition As String = ""
            If Param.Use_Rec_ID = True Then
                SearchCondition = " ( A.REC_ID ='" & Param.SearchStr & "' AND MS.REC_ID='" & Param.Membership_REC_ID & "'  )"
            Else
                SearchCondition = " ( A.C_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                  "   OR  MS.MS_NO LIKE '%" & Param.SearchStr & "%' " & _
                                  "   OR  CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END LIKE '%" & Param.SearchStr & "%' " & _
                                  "   OR  CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END  LIKE '%" & Param.SearchStr & "%' " & _
                                  "  ) "
            End If

            Dim Query As String = " SELECT MS.MS_NO,MS.MS_OLD_NO,SI.SI_NAME,MS.MS_SI_ID,WS.WING_NAME,MS.MS_START_DATE, (SELECT MAX(MB_PERIOD_TO) FROM MEMBERSHIP_BALANCES_INFO AS MB WHERE MS.REC_ID=MB.MS_REC_ID AND MB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & Param.OtherCondition & " ) AS LAST_PERIOD_TO, " & _
                                  " A.C_NAME,A.C_R_ADD1,A.C_R_ADD2,A.C_R_ADD3,A.C_R_ADD4,A.C_R_PINCODE,CT.CI_NAME, ST.ST_NAME, DI.DI_NAME, CO.CO_NAME,A.C_EDUCATION,MI.MISC_NAME  AS C_OCCUPATION,CAST(A.C_DOB_L AS DATE) AS C_DOB,CASE WHEN ISDATE(CONVERT(VARCHAR(10),C_DOB_L,111))=1 THEN ROUND ((DATEDIFF(DD,C_DOB_L,GETDATE())/365),0) END AS C_AGE " & _
                                  " ,CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0 THEN A.C_TEL_NO_R_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0  AND  LEN(COALESCE(A.C_TEL_NO_R_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_2,'')) > 0 THEN A.C_TEL_NO_R_2 ELSE '' END  AS TEL_NOS " & _
                                  " ,CASE WHEN LEN(COALESCE(A.C_MOB_NO_1  ,'')) > 0 THEN A.C_MOB_NO_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_MOB_NO_1  ,'')) > 0  AND  LEN(COALESCE(A.C_MOB_NO_2  ,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_MOB_NO_2  ,'')) > 0 THEN A.C_MOB_NO_2 ELSE '' END  AS MOB_NOS " & _
                                  " ,CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0 THEN A.C_EMAIL_ID_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0  AND  LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0  THEN ', ' ELSE '' END , CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0 THEN A.C_EMAIL_ID_2 ELSE '' END AS EMAILS  " & _
                                  " ,A.C_CEN_CATEGORY,A.C_CLASS_CEN_ID,CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END AS 'CEN_NAME' ,CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END AS 'CEN_UID',A.REC_ID AS C_ID ,A.C_ORG_REC_ID AS C_ORG_ID , MS.REC_ID AS MEM_ID, MS.REC_EDIT_ON AS MS_REC_EDIT_ON,A.REC_EDIT_ON AS C_REC_EDIT_ON " & _
                                  " FROM       MEMBERSHIP_INFO       AS MS   " & _
                                  " INNER JOIN ADDRESS_BOOK          AS A    ON (MS.MS_AB_ID        = A.C_ORG_REC_ID  AND C_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & "  AND A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT OUTER JOIN WINGS_INFO            AS WS   ON (MS.MS_WING_ID      = WS.REC_ID         AND WS.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT OUTER JOIN CENTRE_TASK_INFO AS TASK ON WS.REC_ID = TASK.TASK_REF_ID AND TASK.CEN_ID =   '" & inBasicParam.openCenID & "' AND TASK_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  AND TASK.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                  " LEFT OUTER JOIN SUBSCRIPTION_INFO     AS SI   ON (MS.MS_SI_ID        = SI.REC_ID         AND MS.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT  JOIN CENTRE_INFO           AS C    ON (C.CEN_ID           = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN OVERSEAS_CENTRE_INFO  AS O    ON (O.CEN_ID           = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID      = CT.REC_ID         AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID     = ST.REC_ID         AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MAP_DISTRICT_INFO     AS DI 	 ON (A.C_R_DISTRICT_ID  = DI.REC_ID         AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MAP_COUNTRY_INFO      AS CO 	 ON (A.C_R_COUNTRY_ID   = CO.REC_ID         AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MISC_INFO             AS MI   ON (A.C_OCCUPATION_ID  = MI.REC_ID         AND MI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " WHERE MS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                  "   AND MS.MS_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND MS.MS_CLOSE_DATE IS NULL AND SI.SI_CATEGORY NOT IN ('LIFETIME') " & _
                                  "   AND " & SearchCondition & " " & _
                                  " ORDER BY A.C_NAME "

            Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_INFO, Query, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Last Period
        ''' </summary>
        ''' <param name="M_Rec_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetLastPeriod</remarks>
        Public Shared Function GetLastPeriod(ByVal M_Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT  MAX(MB_PERIOD_FROM) AS 'PERIOD_FROM' FROM membership_balances_info WHERE MB_TR_ID='" & M_Rec_Id & "' and  REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")"
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, Query, ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert Master Info
        ''' </summary>
        ''' <param name="InMinfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_InsertMasterInfo</remarks>
        Public Shared Function InsertMasterInfo(ByVal InMinfo As Parameter_InsertMasterInfo_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_REF_ID,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_ADVANCE_AMT,TR_LB_AMT,TR_CREDIT_AMT,TR_TDS_AMT," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InMinfo.TxnCode & "'," & _
                                                  "'" & InMinfo.VNo & "', " & _
                                                  " " & If(IsDate(InMinfo.TDate), "'" & Convert.ToDateTime(InMinfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  "'" & InMinfo.PartyID & "', " & _
                                                  "'" & InMinfo.Ref_ID & "', " & _
                                                  " " & InMinfo.SubTotal & "," & _
                                                  " " & InMinfo.Cash & "," & _
                                                  " " & InMinfo.Bank & "," & _
                                                  " " & 0 & "," & _
                                                  " " & 0 & "," & _
                                                  " " & 0 & "," & _
                                                  " " & 0 & "," & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMinfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMinfo.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMinfo.RecID, InMinfo.TDate, AddTime)
            Return True
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
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.SUB_Cr_Led_ID.Length = 0 Then InParam.SUB_Cr_Led_ID = "NULL" Else If Not InParam.SUB_Cr_Led_ID.StartsWith("'") Then InParam.SUB_Cr_Led_ID = "'" & InParam.SUB_Cr_Led_ID & "'"
            If InParam.SUB_Dr_Led_ID.Length = 0 Then InParam.SUB_Dr_Led_ID = "NULL" Else If Not InParam.SUB_Dr_Led_ID.StartsWith("'") Then InParam.SUB_Dr_Led_ID = "'" & InParam.SUB_Dr_Led_ID & "'"
            If InParam.Ref_BANK_ID.Length = 0 Then InParam.Ref_BANK_ID = "NULL" Else If Not InParam.Ref_BANK_ID.StartsWith("'") Then InParam.Ref_BANK_ID = "'" & InParam.Ref_BANK_ID & "'"
            If InParam.Ref_Branch.Length = 0 Then InParam.Ref_Branch = "NULL" Else If Not InParam.Ref_Branch.StartsWith("'") Then InParam.Ref_Branch = "'" & InParam.Ref_Branch & "'"
            If InParam.Ref_No.Length = 0 Then InParam.Ref_No = "NULL" Else If Not InParam.Ref_No.StartsWith("'") Then InParam.Ref_No = "'" & InParam.Ref_No & "'"
            If InParam.MasterTxnID.Length = 0 Then InParam.MasterTxnID = "NULL" Else If Not InParam.MasterTxnID.StartsWith("'") Then InParam.MasterTxnID = "'" & InParam.MasterTxnID & "'"
            If InParam.Mode.Length = 0 Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.Party1.Length = 0 Then InParam.Party1 = "NULL" Else If Not InParam.Party1.StartsWith("'") Then InParam.Party1 = "'" & InParam.Party1 & "'"
            If InParam.SrNo.Length = 0 Then InParam.SrNo = "NULL"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_SUB_DR_LED_ID," & _
                                        "TR_MODE,TR_REF_BANK_ID, TR_REF_BRANCH, TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_AMOUNT,TR_AB_ID_1,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_M_ID,TR_SR_NO," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                        ") VALUES(" & _
                                                        "" & inBasicParam.openCenID.ToString & "," & _
                                                        "" & inBasicParam.openYearID.ToString & "," & _
                                                        " " & InParam.TransCode & "," & _
                                                            "'" & InParam.VNo & "', " & _
                                                            " " & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            "'" & InParam.ItemID & "', " & _
                                                            "'" & InParam.Type & "', " & _
                                                            " " & InParam.Cr_Led_ID & " , " & _
                                                            " " & InParam.Dr_Led_ID & " , " & _
                                                            " " & InParam.SUB_Cr_Led_ID & " , " & _
                                                            " " & InParam.SUB_Dr_Led_ID & " , " & _
                                                            " " & InParam.Mode & " , " & _
                                                            " " & InParam.Ref_BANK_ID & " , " & _
                                                            " " & InParam.Ref_Branch & " , " & _
                                                            " " & InParam.Ref_No & " , " & _
                                                            " " & If(IsDate(InParam.Ref_Date), "'" & Convert.ToDateTime(InParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            " " & If(IsDate(InParam.Ref_CDate), "'" & Convert.ToDateTime(InParam.Ref_CDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            " " & InParam.Amount & ", " & _
                                                            " " & InParam.Party1 & ", " & _
                                                            "'" & InParam.Narration & "', " & _
                                                            "'" & InParam.Remarks & "', " & _
                                                            "'" & InParam.Reference & "', " & _
                                                            " " & InParam.MasterTxnID & " , " & _
                                                            " " & InParam.SrNo & " , " & _
                                                          "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Item
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_InsertItem</remarks>
        Public Shared Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InItem.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_ITEM_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_SR_NO,TR_ITEM_ID,TR_LED_ID,TR_TRANS_TYPE,TR_PARTY_REQ,TR_PROFILE,TR_ITEM_NAME,TR_ITEM_HEAD,TR_QTY,TR_UNIT,TR_RATE,TR_AMOUNT,TR_REMARKS," & _
                                            "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                            ") VALUES(" & _
                                            "" & inBasicParam.openCenID.ToString & "," & _
                                            "" & inBasicParam.openYearID.ToString & "," & _
                                            "'" & InItem.Txn_M_ID & "', " & _
                                            " " & InItem.TxnSrNo & " , " & _
                                            "'" & InItem.ItemID & "', " & _
                                            "'" & InItem.LedID & "', " & _
                                            "'" & InItem.Type & "', " & _
                                            "'" & InItem.PartyReq & "', " & _
                                            "'" & InItem.Profile & "', " & _
                                            "'" & InItem.ItemName & "', " & _
                                            "'" & InItem.Head & "', " & _
                                            " " & InItem.Qty & ", " & _
                                            "'" & InItem.Unit & "', " & _
                                            " " & InItem.Rate & ", " & _
                                            " " & InItem.Amount & ", " & _
                                            "'" & InItem.Remarks & "', " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InItem.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, OnlineQuery, inBasicParam, InItem.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Payment
        ''' </summary>
        ''' <param name="InPayment"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_InsertPayment</remarks>
        Public Shared Function InsertPayment(ByVal InPayment As Parameter_InsertPayment_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPayment.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            If InPayment.Dep_BA_ID.Length = 0 Then InPayment.Dep_BA_ID = "NULL" Else If Not InPayment.Dep_BA_ID.StartsWith("'") Then InPayment.Dep_BA_ID = "'" & InPayment.Dep_BA_ID & "'"
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_MODE,TR_REF_ID,TR_REF_BRANCH,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_AMT,TR_DEP_BA_REC_ID," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & inBasicParam.openYearID.ToString & "," & _
                                        "'" & InPayment.TxnMID & "', " & _
                                        "'" & InPayment.Type & "', " & _
                                        " " & InPayment.SrNo & " , " & _
                                        "'" & InPayment.Mode & "', " & _
                                        "'" & InPayment.RefID & "', " & _
                                        "'" & InPayment.RefBranch & "', " & _
                                        "'" & InPayment.RefNo & "', " & _
                                        " " & If(IsDate(InPayment.RefDate), "'" & Convert.ToDateTime(InPayment.RefDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        " " & If(IsDate(InPayment.ClearingDate), "'" & Convert.ToDateTime(InPayment.ClearingDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        " " & InPayment.RefAmount & ", " & _
                                        " " & InPayment.Dep_BA_ID & " , " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPayment.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InPayment.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Purpose
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InPurpose.TxnID & "'," & _
                                                  "'" & InPurpose.PurposeID & "', " & _
                                                  " " & InPurpose.Amount & ", " & _
                                                  " " & InPurpose.SrNo & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update Master
        ''' </summary>
        ''' <param name="UpMaster"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_UpdateMaster</remarks>
        Public Shared Function UpdateMaster(ByVal UpMaster As Parameter_UpdateMaster_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " & _
                                            " TR_VNO         ='" & UpMaster.VNo & "', " & _
                                            " TR_DATE        =" & If(IsDate(UpMaster.TDate), "'" & Convert.ToDateTime(UpMaster.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_AB_ID_1     ='" & UpMaster.PartyID & "', " & _
                                            " TR_REF_ID      ='" & UpMaster.Ref_ID & "', " & _
                                            " TR_SUB_AMT     = " & UpMaster.SubTotal & ", " & _
                                            " TR_CASH_AMT    = " & UpMaster.Cash & ", " & _
                                            " TR_BANK_AMT    = " & UpMaster.Bank & ", " & _
                                            " TR_ADVANCE_AMT = " & 0 & ", " & _
                                            " TR_LB_AMT      = " & 0 & ", " & _
                                            " TR_CREDIT_AMT  = " & 0 & ", " & _
                                            " TR_TDS_AMT     = " & 0 & ", " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpMaster.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpMaster.TDate)
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
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.Sub_Cr_Led_ID.Trim.Length = 0 Then UpParam.Sub_Cr_Led_ID = "NULL" Else UpParam.Sub_Cr_Led_ID = "'" & UpParam.Sub_Cr_Led_ID & "'"
            If UpParam.Sub_Dr_Led_ID.Trim.Length = 0 Then UpParam.Sub_Dr_Led_ID = "NULL" Else UpParam.Sub_Dr_Led_ID = "'" & UpParam.Sub_Dr_Led_ID & "'"
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                                " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                " TR_ITEM_ID     ='" & UpParam.ItemID & "', " & _
                                                " TR_TYPE        ='" & UpParam.Type & "', " & _
                                                " TR_CR_LED_ID   ='" & UpParam.Cr_Led_ID & "', " & _
                                                " TR_DR_LED_ID   ='" & UpParam.Dr_Led_ID & "', " & _
                                                " TR_SUB_CR_LED_ID  =" & UpParam.Sub_Cr_Led_ID & ", " & _
                                                " TR_SUB_DR_LED_ID  =" & UpParam.Sub_Dr_Led_ID & ", " & _
                                                " TR_MODE        ='" & UpParam.Mode & "', " & _
                                                " TR_REF_BANK_ID ='" & UpParam.RefBankID & "', " & _
                                                " TR_REF_BRANCH  ='" & UpParam.RefBranch & "', " & _
                                                " TR_REF_NO      ='" & UpParam.Ref_No & "', " & _
                                                " TR_REF_DATE    = " & If(IsDate(UpParam.Ref_Date), "'" & Convert.ToDateTime(UpParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                " TR_REF_CDATE   = " & If(IsDate(UpParam.Ref_ChequeDate), "'" & Convert.ToDateTime(UpParam.Ref_ChequeDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                " TR_AMOUNT      = " & UpParam.Amount & ", " & _
                                                " TR_AB_ID_1     ='" & UpParam.DonorID & "', " & _
                                                " TR_NARRATION   ='" & UpParam.Narration & "', " & _
                                                " TR_REMARKS     ='" & UpParam.Remarks & "', " & _
                                                " TR_REFERENCE   ='" & UpParam.Reference & "', " & _
                                                "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                                "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                                "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, Nothing, UpParam.TDate)
            Return True
        End Function

        ''' <summary>
        ''' Update Purpose
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_UpdatePurpose</remarks>
        Public Shared Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Transaction_D_Purpose_Info SET " & _
                                         " TR_PURPOSE_MISC_ID    ='" & UpPurpose.PurposeID & "', " & _
                                         " TR_AMOUNT             =" & UpPurpose.Amount & ", " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  " & _
                                            "  WHERE REC_ID    ='" & UpPurpose.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function InsertMemRenewalVoucher_Txn(inParam As Param_Txn_Insert_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_Insert_VoucherMembershipRenewal In inParam.Insert
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertItem_VoucherMembershipRenewal In inParam.InsertItem
                If Not Param Is Nothing Then InsertItem(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertPurpose_VoucherMembershipRenewal In inParam.InsertPurpose
                If Not Param Is Nothing Then InsertPurpose(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.param_InsertBalancesLifetime Is Nothing Then
                If Not Membership.InsertBalances(inParam.param_InsertBalancesLifetime, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Membership.Parameter_InsertBalances_Membership In inParam.InsertBalances
                If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertPayment_VoucherMembershipRenewal In inParam.InsertPayment
                If Not Param Is Nothing Then InsertPayment(Param, inBasicParam, RequestTime)
            Next
            'End Using
            'txn.Complete()
            'End Using
            Return True
        End Function

        Public Shared Function UpdateMemRenewalVoucher_Txn(upParam As Param_Txn_Update_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            'Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then
                If Not UpdateMaster(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & upParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & upParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteMembership Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_INFO, "MS_TR_ID    ='" & upParam.MID_DeleteMembership & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteBalances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, "MB_TR_ID    ='" & upParam.MID_DeleteBalances & "'", inBasicParam)
            End If
            For Each Param As Parameter_Insert_VoucherMembershipRenewal In upParam.Insert
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Parameter_InsertItem_VoucherMembershipRenewal In upParam.InsertItem
                If Not Param Is Nothing Then InsertItem(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Parameter_InsertPurpose_VoucherMembershipRenewal In upParam.InsertPurpose
                If Not Param Is Nothing Then InsertPurpose(Param, inBasicParam, RequestTime, CommonParam)
            Next
            If Not upParam.param_InsertBalancesLifetime Is Nothing Then
                If Not Membership.InsertBalances(upParam.param_InsertBalancesLifetime, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Membership.Parameter_InsertBalances_Membership In upParam.InsertBalances
                If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Parameter_InsertPayment_VoucherMembershipRenewal In upParam.InsertPayment
                If Not Param Is Nothing Then InsertPayment(Param, inBasicParam, RequestTime, CommonParam)
            Next
            'End Using
            'Commit here 
            'txn.Complete()
            'End Using
            Return True
        End Function

        Public Shared Function DeleteMemRenewalVoucher_Txn(delParam As Param_Txn_Delete_VoucherMembershipRenewal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            ' Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & delParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & delParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteMembership Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_INFO, "MS_TR_ID    ='" & delParam.MID_DeleteMembership & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteBalances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, "MB_TR_ID    ='" & delParam.MID_DeleteBalances & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteReceiptRef Is Nothing Then
                Dim Param As Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register = New Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register()
                Param.Rec_Id = delParam.MID_DeleteReceiptRef
                Membership_Receipt_Register.DeleteReceiptRef(Param, inBasicParam)
            End If

            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            'End Using
            'Commit here 
            'txn.Complete()
            'End Using
            Return True
        End Function

    End Class
    <Serializable>
    Public Class Voucher_Membership_Conversion
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_InsertMasterInfo_VoucherMembershipConversion
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public Ref_ID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherMembershipConversion
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public SUB_Cr_Led_ID As String
            Public SUB_Dr_Led_ID As String
            Public Amount As Double
            Public Mode As String
            Public Ref_BANK_ID As String
            Public Ref_Branch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_CDate As String
            Public Party1 As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public MasterTxnID As String
            Public SrNo As String
            Public Status_Action As String
            Public RecID As String
            Public CrossRefId As String = ""
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertItem_VoucherMembershipConversion
            Public Txn_M_ID As String
            Public TxnSrNo As Integer
            Public ItemID As String
            Public LedID As String
            Public Type As String
            Public PartyReq As String
            Public Profile As String
            Public ItemName As String
            Public Head As String
            Public Qty As Double
            Public Unit As String
            Public Rate As Double
            Public Amount As Double
            Public Remarks As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertPayment_VoucherMembershipConversion
            Public TxnMID As String
            Public Type As String
            Public SrNo As String
            Public Mode As String
            Public RefID As String
            Public RefBranch As String
            Public RefNo As String
            Public RefDate As String
            Public ClearingDate As String
            Public RefAmount As Double
            Public Dep_BA_ID As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherMembershipConversion
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public SrNo As Integer
            Public Status_Action As String
            Public RecID As String
            ' Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_UpdateMaster_VoucherMembershipConversion
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public Ref_ID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_VoucherMembershipConversion
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Sub_Dr_Led_ID As String
            Public Mode As String
            Public RefBankID As String
            Public RefBranch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_ChequeDate As String
            Public Amount As Double
            Public DonorID As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_UpdatePurpose_VoucherMembershipConversion
            Public PurposeID As String
            Public Amount As Double
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherMembershipConversion
            Public param_InsertMaster As Parameter_InsertMasterInfo_VoucherMembershipConversion
            Public Insert() As Parameter_Insert_VoucherMembershipConversion = Nothing
            Public param_InsertMembership As Membership.Parameter_Insert_Membership = Nothing
            Public InsertItem() As Parameter_InsertItem_VoucherMembershipConversion = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherMembershipConversion = Nothing
            Public InsertBalNotAdvFee() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertBalFeeInAdvance()() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertPayment() As Parameter_InsertPayment_VoucherMembershipConversion = Nothing
            Public CloseOldMembership As Membership.Parameter_Close_Membership = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherMembershipConversion
            Public param_UpdateMaster As Parameter_UpdateMaster_VoucherMembershipConversion
            Public MID_Delete As String = Nothing
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeleteBalances As String = Nothing
            Public MID_DeleteMembership As String = Nothing
            Public MemID_Reopen_Membership As String = Nothing
            Public Insert() As Parameter_Insert_VoucherMembershipConversion = Nothing
            Public param_InsertMembership As Membership.Parameter_Insert_Membership = Nothing
            Public InsertItem() As Parameter_InsertItem_VoucherMembershipConversion = Nothing
            Public InsertPurpose() As Parameter_InsertPurpose_VoucherMembershipConversion = Nothing
            Public InsertBalNotAdvFee() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertBalFeeInAdvance()() As Membership.Parameter_InsertBalances_Membership = Nothing
            Public InsertPayment() As Parameter_InsertPayment_VoucherMembershipConversion = Nothing
            Public OrgMemID_Reopen_Membership As String = Nothing
            Public CloseOldMembership As Membership.Parameter_Close_Membership = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherMembershipConversion
            Public MID_DeleteItems As String = Nothing
            Public MID_DeletePurpose As String = Nothing
            Public MID_DeletePayment As String = Nothing
            Public MID_DeleteBalances As String = Nothing
            Public MID_DeleteMembership As String = Nothing
            Public MID_DeleteReceiptRef As String = Nothing
            Public MID_Delete As String = Nothing
            Public MID_DeleteMaster As String = Nothing
            Public OrgMemID_Reopen_Membership As String = Nothing
        End Class

#End Region
        <Serializable>
        Public Class Param_GetPartyDetails_VoucherMembershipConversion
            Public OtherCondition As String
            Public SearchStr As String
            Public Use_Rec_ID As Boolean
            Public Membership_REC_ID As String
            Public Converted_Member_ID As String
        End Class

        ''' <summary>
        ''' Get Party Details
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' 
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetPartyDetails</remarks>
        Public Shared Function GetPartyDetails(ByVal Param As Param_GetPartyDetails_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SearchCondition As String = ""
            If Param.Use_Rec_ID = True Then
                SearchCondition = " ( A.C_ORG_REC_ID ='" & Param.SearchStr & "' AND MS.REC_ID='" & Param.Membership_REC_ID & "'  )"
            Else
                SearchCondition = " ( A.C_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                  "   OR  MS.MS_NO LIKE '%" & Param.SearchStr & "%' " & _
                                  "   OR  CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END LIKE '%" & Param.SearchStr & "%' " & _
                                  "   OR  CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END  LIKE '%" & Param.SearchStr & "%' " & _
                                  "  ) "
            End If

            Dim Query As String = " SELECT MS.MS_NO,MS.MS_OLD_NO,SI.SI_NAME,MS.MS_SI_ID,WS.WING_NAME, WS.REC_ID AS WING_ID,MS.MS_START_DATE, (SELECT MAX(MB_PERIOD_TO) FROM MEMBERSHIP_BALANCES_INFO AS MB WHERE MS.REC_ID=MB.MS_REC_ID AND MB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & Param.OtherCondition & " ) AS LAST_PERIOD_TO, " & _
                                  " A.C_NAME,A.C_R_ADD1,A.C_R_ADD2,A.C_R_ADD3,A.C_R_ADD4,A.C_R_PINCODE,CT.CI_NAME, ST.ST_NAME, DI.DI_NAME, CO.CO_NAME,A.C_EDUCATION,MI.MISC_NAME  AS C_OCCUPATION,CAST(A.C_DOB_L AS DATE) AS C_DOB,CASE WHEN ISDATE(CONVERT(VARCHAR(10),C_DOB_L,111))=1 THEN ROUND ((DATEDIFF(DD,C_DOB_L,GETDATE())/365),0) END AS C_AGE " & _
                                  " ,CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0 THEN A.C_TEL_NO_R_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0  AND  LEN(COALESCE(A.C_TEL_NO_R_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_2,'')) > 0 THEN A.C_TEL_NO_R_2 ELSE '' END  AS TEL_NOS " & _
                                  " ,CASE WHEN LEN(COALESCE(A.C_MOB_NO_1  ,'')) > 0 THEN A.C_MOB_NO_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_MOB_NO_1  ,'')) > 0  AND  LEN(COALESCE(A.C_MOB_NO_2  ,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_MOB_NO_2  ,'')) > 0 THEN A.C_MOB_NO_2 ELSE '' END  AS MOB_NOS " & _
                                  " ,CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0 THEN A.C_EMAIL_ID_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0  AND  LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0  THEN ', ' ELSE '' END , CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0 THEN A.C_EMAIL_ID_2 ELSE '' END AS EMAILS  " & _
                                  " ,A.C_CEN_CATEGORY,A.C_CLASS_CEN_ID,CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END AS 'CEN_NAME' ,CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END AS 'CEN_UID',A.REC_ID AS C_ID ,A.C_ORG_REC_ID AS C_ORG_ID , MS.REC_ID AS MEM_ID, MS.REC_EDIT_ON AS MS_REC_EDIT_ON,A.REC_EDIT_ON AS C_REC_EDIT_ON, " & _
                                  " (SELECT SUM(MB_AMOUNT) FROM MEMBERSHIP_BALANCES_INFO WHERE  REC_STATUS IN(0,1,2)  AND MB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MS_REC_ID = MS.REC_ID AND MB_PERIOD_FROM > cast('20' +SUBSTRING(CAST(" & inBasicParam.openYearID.ToString & " AS VARCHAR),1,2) +'/4/1'  as date)) AS ADVANCE," & _
                                  "	(SELECT DATEDIFF(yy, (SELECT MAX(MB_PERIOD_TO)   FROM MEMBERSHIP_BALANCES_INFO  WHERE REC_STATUS IN(0,1,2)  AND MB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MS_REC_ID = MS.REC_ID AND MB_PERIOD_FROM <cast('20' +SUBSTRING(CAST(" & inBasicParam.openYearID.ToString & " AS VARCHAR),1,2) +'/4/1' as date)  ),cast('20' +SUBSTRING(CAST(" & inBasicParam.openYearID.ToString & " AS VARCHAR),1,2) +'/3/31' as date))) as 'Arrear Years'" & _
                                  " FROM       MEMBERSHIP_INFO       AS MS   " & _
                                  " INNER JOIN ADDRESS_BOOK          AS A    ON (MS.MS_AB_ID        = A.C_ORG_REC_ID  AND C_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & "   AND A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT OUTER JOIN WINGS_INFO            AS WS   ON (MS.MS_WING_ID      = WS.REC_ID         AND WS.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT OUTER JOIN CENTRE_TASK_INFO AS TASK ON WS.REC_ID = TASK.TASK_REF_ID AND TASK.CEN_ID =   " & inBasicParam.openCenID.ToString & " AND TASK_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  AND TASK.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                  " LEFT OUTER JOIN SUBSCRIPTION_INFO     AS SI   ON (MS.MS_SI_ID        = SI.REC_ID         AND MS.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
                                  " LEFT  JOIN CENTRE_INFO           AS C    ON (C.CEN_ID           = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN OVERSEAS_CENTRE_INFO  AS O    ON (O.CEN_ID           = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID      = CT.REC_ID         AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID     = ST.REC_ID         AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MAP_DISTRICT_INFO     AS DI 	 ON (A.C_R_DISTRICT_ID  = DI.REC_ID         AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MAP_COUNTRY_INFO      AS CO 	 ON (A.C_R_COUNTRY_ID   = CO.REC_ID         AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " LEFT  JOIN MISC_INFO             AS MI   ON (A.C_OCCUPATION_ID  = MI.REC_ID         AND MI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " WHERE MS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                  "   AND MS.MS_CEN_ID  = " & inBasicParam.openCenID.ToString & "" & _
                                  "   AND " & SearchCondition & " " & _
                                  "   AND SI_NAME ='ANNUAL' AND (MS.MS_CLOSE_DATE IS NULL OR MS.REC_ID = '" & Param.Converted_Member_ID & "')" & _
                                  "   ORDER BY A.C_NAME "

            Return dbService.List(ConnectOneWS.Tables.MEMBERSHIP_INFO, Query, ConnectOneWS.Tables.MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert Master Info
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertMasterInfo</remarks>
        Public Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_REF_ID,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_ADVANCE_AMT,TR_LB_AMT,TR_CREDIT_AMT,TR_TDS_AMT," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InMInfo.TxnCode & "'," & _
                                                  "'" & InMInfo.VNo & "', " & _
                                                  " " & If(IsDate(InMInfo.TDate), "'" & Convert.ToDateTime(InMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  "'" & InMInfo.PartyID & "', " & _
                                                  "'" & InMInfo.Ref_ID & "', " & _
                                                  " " & InMInfo.SubTotal & "," & _
                                                  " " & InMInfo.Cash & "," & _
                                                  " " & InMInfo.Bank & "," & _
                                                  " " & 0 & "," & _
                                                  " " & 0 & "," & _
                                                  " " & 0 & "," & _
                                                  " " & 0 & "," & _
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
            Return True
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
        ''' <remarks>RealServiceFunctions.VoucherMembership_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = "NULL" Else If Not InParam.Cr_Led_ID.StartsWith("'") Then InParam.Cr_Led_ID = "'" & InParam.Cr_Led_ID & "'"
            If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = "NULL" Else If Not InParam.Dr_Led_ID.StartsWith("'") Then InParam.Dr_Led_ID = "'" & InParam.Dr_Led_ID & "'"
            If InParam.SUB_Cr_Led_ID.Length = 0 Then InParam.SUB_Cr_Led_ID = "NULL" Else If Not InParam.SUB_Cr_Led_ID.StartsWith("'") Then InParam.SUB_Cr_Led_ID = "'" & InParam.SUB_Cr_Led_ID & "'"
            If InParam.SUB_Dr_Led_ID.Length = 0 Then InParam.SUB_Dr_Led_ID = "NULL" Else If Not InParam.SUB_Dr_Led_ID.StartsWith("'") Then InParam.SUB_Dr_Led_ID = "'" & InParam.SUB_Dr_Led_ID & "'"
            If InParam.Ref_BANK_ID.Length = 0 Then InParam.Ref_BANK_ID = "NULL" Else If Not InParam.Ref_BANK_ID.StartsWith("'") Then InParam.Ref_BANK_ID = "'" & InParam.Ref_BANK_ID & "'"
            If InParam.Ref_Branch.Length = 0 Then InParam.Ref_Branch = "NULL" Else If Not InParam.Ref_Branch.StartsWith("'") Then InParam.Ref_Branch = "'" & InParam.Ref_Branch & "'"
            If InParam.Ref_No.Length = 0 Then InParam.Ref_No = "NULL" Else If Not InParam.Ref_No.StartsWith("'") Then InParam.Ref_No = "'" & InParam.Ref_No & "'"
            If InParam.MasterTxnID.Length = 0 Then InParam.MasterTxnID = "NULL" Else If Not InParam.MasterTxnID.StartsWith("'") Then InParam.MasterTxnID = "'" & InParam.MasterTxnID & "'"
            If InParam.Mode.Length = 0 Then InParam.Mode = "NULL" Else If Not InParam.Mode.StartsWith("'") Then InParam.Mode = "'" & InParam.Mode & "'"
            If InParam.Party1.Length = 0 Then InParam.Party1 = "NULL" Else If Not InParam.Party1.StartsWith("'") Then InParam.Party1 = "'" & InParam.Party1 & "'"
            If InParam.SrNo.Length = 0 Then InParam.SrNo = "NULL"
            If InParam.CrossRefId.Length = 0 Then InParam.CrossRefId = "NULL" Else If Not InParam.CrossRefId.StartsWith("'") Then InParam.CrossRefId = "'" & InParam.CrossRefId & "'"
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_SUB_DR_LED_ID," & _
                                        "TR_MODE,TR_REF_BANK_ID, TR_REF_BRANCH, TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_AMOUNT,TR_AB_ID_1,TR_NARRATION,TR_REMARKS,TR_REFERENCE,TR_M_ID,TR_SR_NO,TR_TRF_CROSS_REF_ID," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                        ") VALUES(" & _
                                                        "" & inBasicParam.openCenID.ToString & "," & _
                                                        "" & inBasicParam.openYearID.ToString & "," & _
                                                        " " & InParam.TransCode & "," & _
                                                            "'" & InParam.VNo & "', " & _
                                                            " " & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            "'" & InParam.ItemID & "', " & _
                                                            "'" & InParam.Type & "', " & _
                                                            " " & InParam.Cr_Led_ID & " , " & _
                                                            " " & InParam.Dr_Led_ID & " , " & _
                                                            " " & InParam.SUB_Cr_Led_ID & " , " & _
                                                            " " & InParam.SUB_Dr_Led_ID & " , " & _
                                                            " " & InParam.Mode & " , " & _
                                                            " " & InParam.Ref_BANK_ID & " , " & _
                                                            " " & InParam.Ref_Branch & " , " & _
                                                            " " & InParam.Ref_No & " , " & _
                                                            " " & If(IsDate(InParam.Ref_Date), "'" & Convert.ToDateTime(InParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            " " & If(IsDate(InParam.Ref_CDate), "'" & Convert.ToDateTime(InParam.Ref_CDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                            " " & InParam.Amount & ", " & _
                                                            " " & InParam.Party1 & ", " & _
                                                            "'" & InParam.Narration & "', " & _
                                                            "'" & InParam.Remarks & "', " & _
                                                            "'" & InParam.Reference & "', " & _
                                                            " " & InParam.MasterTxnID & " , " & _
                                                            " " & InParam.SrNo & " , " & _
                                                            " " & InParam.CrossRefId & " , " & _
                                                          "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Item
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertItem</remarks>
        Public Shared Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InItem.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_ITEM_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_SR_NO,TR_ITEM_ID,TR_LED_ID,TR_TRANS_TYPE,TR_PARTY_REQ,TR_PROFILE,TR_ITEM_NAME,TR_ITEM_HEAD,TR_QTY,TR_UNIT,TR_RATE,TR_AMOUNT,TR_REMARKS," & _
                                            "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                            ") VALUES(" & _
                                            "" & inBasicParam.openCenID.ToString & "," & _
                                            "" & inBasicParam.openYearID.ToString & "," & _
                                            "'" & InItem.Txn_M_ID & "', " & _
                                            " " & InItem.TxnSrNo & " , " & _
                                            "'" & InItem.ItemID & "', " & _
                                            "'" & InItem.LedID & "', " & _
                                            "'" & InItem.Type & "', " & _
                                            "'" & InItem.PartyReq & "', " & _
                                            "'" & InItem.Profile & "', " & _
                                            "'" & InItem.ItemName & "', " & _
                                            "'" & InItem.Head & "', " & _
                                            " " & InItem.Qty & ", " & _
                                            "'" & InItem.Unit & "', " & _
                                            " " & InItem.Rate & ", " & _
                                            " " & InItem.Amount & ", " & _
                                            "'" & InItem.Remarks & "', " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InItem.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, OnlineQuery, inBasicParam, InItem.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Payment
        ''' </summary>
        ''' <param name="InPmt"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertPayment</remarks>
        Public Shared Function InsertPayment(ByVal InPmt As Parameter_InsertPayment_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPmt.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            If InPmt.Dep_BA_ID.Length = 0 Then InPmt.Dep_BA_ID = "NULL" Else If Not InPmt.Dep_BA_ID.StartsWith("'") Then InPmt.Dep_BA_ID = "'" & InPmt.Dep_BA_ID & "'"
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_MODE,TR_REF_ID,TR_REF_BRANCH,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_AMT,TR_DEP_BA_REC_ID," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & inBasicParam.openYearID.ToString & "," & _
                                        "'" & InPmt.TxnMID & "', " & _
                                        "'" & InPmt.Type & "', " & _
                                        " " & InPmt.SrNo & " , " & _
                                        "'" & InPmt.Mode & "', " & _
                                        "'" & InPmt.RefID & "', " & _
                                        "'" & InPmt.RefBranch & "', " & _
                                        "'" & InPmt.RefNo & "', " & _
                                        " " & If(IsDate(InPmt.RefDate), "'" & Convert.ToDateTime(InPmt.RefDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        " " & If(IsDate(InPmt.ClearingDate), "'" & Convert.ToDateTime(InPmt.ClearingDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        " " & InPmt.RefAmount & ", " & _
                                        " " & InPmt.Dep_BA_ID & " , " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPmt.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InPmt.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Purpose
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InPurpose.TxnID & "'," & _
                                                  "'" & InPurpose.PurposeID & "', " & _
                                                  " " & InPurpose.Amount & ", " & _
                                                  " " & InPurpose.SrNo & ", " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update Master
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_UpdateMaster</remarks>
        Public Shared Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE TRANSACTION_D_MASTER_INFO SET " & _
                                            " TR_VNO         ='" & UpParam.VNo & "', " & _
                                            " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                            " TR_AB_ID_1     ='" & UpParam.PartyID & "', " & _
                                            " TR_REF_ID      ='" & UpParam.Ref_ID & "', " & _
                                            " TR_SUB_AMT     = " & UpParam.SubTotal & ", " & _
                                            " TR_CASH_AMT    = " & UpParam.Cash & ", " & _
                                            " TR_BANK_AMT    = " & UpParam.Bank & ", " & _
                                            " TR_ADVANCE_AMT = " & 0 & ", " & _
                                            " TR_LB_AMT      = " & 0 & ", " & _
                                            " TR_CREDIT_AMT  = " & 0 & ", " & _
                                            " TR_TDS_AMT     = " & 0 & ", " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        ' ''' <summary>
        ' ''' Update
        ' ''' </summary>
        ' ''' <param name="Updt"></param>
        ' ''' <param name="Screen"></param>
        ' ''' <param name="openUserID"></param>
        ' ''' <param name="openCenID"></param>
        ' ''' <param name="PCID"></param>
        ' ''' <param name="version"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.VoucherMembership_Update</remarks>
        'Public Shared Function Update(ByVal Updt As Parameter_Update_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    If Updt.Sub_Cr_Led_ID.Trim.Length = 0 Then Updt.Sub_Cr_Led_ID = "NULL" Else Updt.Sub_Cr_Led_ID = "'" & Updt.Sub_Cr_Led_ID & "'"
        '    If Updt.Sub_Dr_Led_ID.Trim.Length = 0 Then Updt.Sub_Dr_Led_ID = "NULL" Else Updt.Sub_Dr_Led_ID = "'" & Updt.Sub_Dr_Led_ID & "'"
        '    If Updt.Cr_Led_ID.Length = 0 Then Updt.Cr_Led_ID = "NULL" Else If Not Updt.Cr_Led_ID.StartsWith("'") Then Updt.Cr_Led_ID = "'" & Updt.Cr_Led_ID & "'"
        '    If Updt.Dr_Led_ID.Length = 0 Then Updt.Dr_Led_ID = "NULL" Else If Not Updt.Dr_Led_ID.StartsWith("'") Then Updt.Dr_Led_ID = "'" & Updt.Dr_Led_ID & "'"
        '    If Updt.RefBankID.Length = 0 Then Updt.RefBankID = "NULL" Else If Not Updt.RefBankID.StartsWith("'") Then Updt.RefBankID = "'" & Updt.RefBankID & "'"
        '    If Updt.DonorID.Length = 0 Then Updt.DonorID = "NULL" Else If Not Updt.DonorID.StartsWith("'") Then Updt.DonorID = "'" & Updt.DonorID & "'"
        '    Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " & _
        '                                    " TR_VNO         ='" & Updt.VNo & "', " & _
        '                                        " TR_DATE        =" & If(IsDate(Updt.TDate), "'" & Convert.ToDateTime(Updt.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
        '                                        " TR_ITEM_ID     ='" & Updt.ItemID & "', " & _
        '                                        " TR_TYPE        ='" & Updt.Type & "', " & _
        '                                        " TR_CR_LED_ID   = " & Updt.Cr_Led_ID & " , " & _
        '                                        " TR_DR_LED_ID   = " & Updt.Dr_Led_ID & " , " & _
        '                                        " TR_SUB_CR_LED_ID  =" & Updt.Sub_Cr_Led_ID & ", " & _
        '                                        " TR_SUB_DR_LED_ID  =" & Updt.Sub_Dr_Led_ID & ", " & _
        '                                        " TR_MODE        ='" & Updt.Mode & "', " & _
        '                                        " TR_REF_BANK_ID = " & Updt.RefBankID & " , " & _
        '                                        " TR_REF_BRANCH  ='" & Updt.RefBranch & "', " & _
        '                                        " TR_REF_NO      ='" & Updt.Ref_No & "', " & _
        '                                        " TR_REF_DATE    = " & If(IsDate(Updt.Ref_Date), "'" & Convert.ToDateTime(Updt.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
        '                                        " TR_REF_CDATE   = " & If(IsDate(Updt.Ref_ChequeDate), "'" & Convert.ToDateTime(Updt.Ref_ChequeDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
        '                                        " TR_AMOUNT      = " & Updt.Amount & ", " & _
        '                                        " TR_AB_ID_1     = " & Updt.DonorID & " , " & _
        '                                        " TR_NARRATION   ='" & Updt.Narration & "', " & _
        '                                        " TR_REMARKS     ='" & Updt.Remarks & "', " & _
        '                                        " TR_REFERENCE   ='" & Updt.Reference & "', " & _
        '                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
        '                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
        '                                        "  WHERE REC_ID    ='" & Updt.RecID & "'"

        '    '",REC_STATUS        =" & Updt.Status_Action & "," & _
        '    '"REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
        '    '"REC_STATUS_BY     ='" & inBasicParam.openUserID & "' 
        '    dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, Nothing, Updt.TDate)
        '    Return True
        'End Function

        ''' <summary>
        ''' Updates Purpose
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_UpdatePurpose</remarks>
        Public Shared Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Transaction_D_Purpose_Info SET " & _
                                         " TR_PURPOSE_MISC_ID    ='" & UpPurpose.PurposeID & "', " & _
                                         " TR_AMOUNT             =" & UpPurpose.Amount & ", " & _
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                            "  WHERE REC_ID    ='" & UpPurpose.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function InsertMembershipVoucherConversion_Txn(inParam As Param_Txn_Insert_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertMaster Is Nothing Then
                If Not InsertMasterInfo(inParam.param_InsertMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_Insert_VoucherMembershipConversion In inParam.Insert
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.param_InsertMembership Is Nothing Then
                If Not Membership.Insert(inParam.param_InsertMembership, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_InsertItem_VoucherMembershipConversion In inParam.InsertItem
                If Not Param Is Nothing Then InsertItem(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertPurpose_VoucherMembershipConversion In inParam.InsertPurpose
                If Not Param Is Nothing Then InsertPurpose(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Membership.Parameter_InsertBalances_Membership In inParam.InsertBalNotAdvFee
                If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.InsertBalFeeInAdvance Is Nothing Then
                For Each InBal() As Membership.Parameter_InsertBalances_Membership In inParam.InsertBalFeeInAdvance
                    If Not InBal Is Nothing Then
                        For Each Param In InBal
                            If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime)
                        Next
                    End If
                Next
            End If
            For Each Param As Parameter_InsertPayment_VoucherMembershipConversion In inParam.InsertPayment
                If Not Param Is Nothing Then InsertPayment(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.CloseOldMembership Is Nothing Then
                If Not Membership.Close(inParam.CloseOldMembership, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'End Using
            'txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function UpdateMembershipVoucherConversion_Txn(upParam As Param_Txn_Update_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, upParam.param_UpdateMaster.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            'Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateMaster Is Nothing Then
                If Not UpdateMaster(upParam.param_UpdateMaster, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & upParam.MID_Delete & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & upParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & upParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not upParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & upParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteBalances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, "MB_TR_ID    ='" & upParam.MID_DeleteBalances & "'", inBasicParam)
            End If
            If Not upParam.MID_DeleteMembership Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_INFO, "MS_TR_ID    ='" & upParam.MID_DeleteMembership & "'", inBasicParam)
            End If
            If Not upParam.MemID_Reopen_Membership Is Nothing Then
                If Not Membership.Reopen(upParam.MemID_Reopen_Membership, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_Insert_VoucherMembershipConversion In upParam.Insert
                If Not Param Is Nothing Then Insert(Param, inBasicParam, RequestTime, CommonParam)
            Next
            If Not upParam.param_InsertMembership Is Nothing Then
                If Not Membership.Insert(upParam.param_InsertMembership, inBasicParam, RequestTime, CommonParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_InsertItem_VoucherMembershipConversion In upParam.InsertItem
                If Not Param Is Nothing Then InsertItem(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Parameter_InsertPurpose_VoucherMembershipConversion In upParam.InsertPurpose
                If Not Param Is Nothing Then InsertPurpose(Param, inBasicParam, RequestTime, CommonParam)
            Next
            For Each Param As Membership.Parameter_InsertBalances_Membership In upParam.InsertBalNotAdvFee
                If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime, CommonParam)
            Next
            If Not upParam.InsertBalFeeInAdvance Is Nothing Then
                For Each InBal() As Membership.Parameter_InsertBalances_Membership In upParam.InsertBalFeeInAdvance
                    If Not InBal Is Nothing Then
                        For Each Param In InBal
                            If Not Param Is Nothing Then Membership.InsertBalances(Param, inBasicParam, RequestTime, CommonParam)
                        Next
                    End If
                Next
            End If
            For Each Param As Parameter_InsertPayment_VoucherMembershipConversion In upParam.InsertPayment
                If Not Param Is Nothing Then InsertPayment(Param, inBasicParam, RequestTime, CommonParam)
            Next
            If Not upParam.OrgMemID_Reopen_Membership Is Nothing Then
                If Not Membership.Reopen(upParam.OrgMemID_Reopen_Membership, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.CloseOldMembership Is Nothing Then
                If Not Membership.Close(upParam.CloseOldMembership, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'End Using
            'Commit here 
            'txn.Complete()
            'End Using
            Return True
        End Function

        Public Shared Function DeleteMembershipVoucherConversion_Txn(delParam As Param_Txn_Delete_VoucherMembershipConversion, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            If Not delParam.MID_Delete Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_INFO, "TR_M_ID = '" & delParam.MID_Delete & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteItems Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_ITEM_INFO, "TR_M_ID = '" & delParam.MID_DeleteItems & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.MID_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.MID_DeletePayment Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, "TR_M_ID = '" & delParam.MID_DeletePayment & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteBalances Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, "MB_TR_ID    ='" & delParam.MID_DeleteBalances & "'", inBasicParam)
            End If

            If Not delParam.MID_DeleteMembership Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.MEMBERSHIP_INFO, "MS_TR_ID    ='" & delParam.MID_DeleteMembership & "'", inBasicParam)
            End If
            If Not delParam.MID_DeleteReceiptRef Is Nothing Then
                Dim Param As Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register = New Membership_Receipt_Register.Param_DeleteReceipt_Membership_Receipt_Register()
                Param.Rec_Id = delParam.MID_DeleteReceiptRef
                Membership_Receipt_Register.DeleteReceiptRef(Param, inBasicParam)
            End If

            If Not delParam.MID_DeleteMaster Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, delParam.MID_DeleteMaster, inBasicParam)
            End If
            If Not delParam.OrgMemID_Reopen_Membership Is Nothing Then
                If Not Membership.Reopen(delParam.OrgMemID_Reopen_Membership, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'End Using
            'Commit here 
            'txn.Complete()
            'End Using
            Return True
        End Function
    End Class
#End Region
End Namespace
