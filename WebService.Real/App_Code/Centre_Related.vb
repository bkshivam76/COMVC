Imports System.Data
Imports ConnectOne.Encrytion
Namespace Real

#Region "Profile"
    <Serializable>
    Public Class Core
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Update_Core
            '' Public OpeningDate As String
            Public ResponsiblePersonID As String
            'Public Status_Action As String
        End Class
#End Region

        ''' <summary>
        ''' Gets Centre Support Info
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_GetCenSupportInfo</remarks>
        Public Shared Function GetCenSupportInfo(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT CI.CEN_DOS CEN_OPEN_DATE,CASE WHEN AB_CURR.C_TITLE IS NULL THEN '' + AB.C_NAME ELSE AB_CURR.C_TITLE + ' ' + AB_CURR.C_NAME END  AS RES_PERSON, AB_CURR.C_MOB_NO_1 + case when LEN(COALESCE(AB_CURR.C_MOB_NO_1,'')) > 0 AND LEN(COALESCE(AB_CURR.C_TEL_NO_R_1,'')) > 0 THEN ', '  ELSE '' END  + AB_CURR.C_TEL_NO_R_1 C_MOB_NO_1 , AB_CURR.C_MOB_NO_2, CS.REC_EDIT_ON  from CENTRE_INFO AS CI LEFT JOIN  Centre_Support_Info AS CS ON CS.CEN_ID = CI.CEN_ID LEFT JOIN Address_Book AS AB ON CS.CEN_ACC_RES_PERSON_AB_ID = AB.REC_ID LEFT JOIN address_book AS AB_CURR ON AB.C_ORG_REC_ID = AB_CURR.C_ORG_REC_ID AND AB_CURR.C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString() & " where  CI.CEN_ID = " & inBasicParam.openCenID.ToString()
            Return dbService.List(ConnectOneWS.Tables.CENTRE_SUPPORT_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_SUPPORT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Support Row Count
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_GetSupportRowCount</remarks>
        Public Shared Function GetSupportRowCount(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM Centre_Support_Info  WHERE REC_STATUS IN (0,1,2) AND CEN_ID =" & inBasicParam.openCenID.ToString & " "
            Return dbService.GetScalar(ConnectOneWS.Tables.CENTRE_SUPPORT_INFO, Query, ConnectOneWS.Tables.CENTRE_SUPPORT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CURR_AB.C_NAME, CURR_AB.REC_ID AS CEN_ACC_RES_PERSON_AB_ID , CEN_OPEN_DATE, CEN_COMMUNICATION_OPTION " &
                                  ", CS.REC_ADD_BY, CS.REC_ADD_ON,CS.REC_EDIT_BY,CS.REC_EDIT_ON,CS.REC_ID,CS.REC_STATUS,CS.REC_STATUS_BY,CS.REC_STATUS_ON  " &
                                  " from Centre_Support_Info  CS  " &
                                  " INNER Join address_book AS AB ON CEN_ACC_RES_PERSON_AB_ID = AB.REC_ID  " &
                                  " INNER JOIN address_book AS CURR_AB ON AB.C_ORG_REC_ID = CURR_AB.C_ORG_REC_ID AND CURR_AB.C_COD_YEAR_ID = " + inBasicParam.openYearID.ToString() &
                                  " WHERE CS.REC_STATUS In (0, 1, 2) And CEN_ID = " + inBasicParam.openCenID.ToString()
            Return dbService.List(ConnectOneWS.Tables.CENTRE_SUPPORT_INFO, Query, ConnectOneWS.Tables.CENTRE_SUPPORT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_Insert</remarks>
        Public Shared Function Insert(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO Centre_Support_Info(CEN_ID,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID) VALUES (" & inBasicParam.openCenID.ToString & ",'" & Common.DateTimePlaceHolder & "','" & inBasicParam.openUserID & "','" & Common.DateTimePlaceHolder & "','" & inBasicParam.openUserID & "'," & Common_Lib.Common.Record_Status._Completed & ",'" & Common.DateTimePlaceHolder & "','" & inBasicParam.openUserID & "','" & RecID & "')"
            dbService.Insert(ConnectOneWS.Tables.CENTRE_SUPPORT_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' Updates Centre Info 
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Core, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select COUNT(*) CNT from centre_support_info WHERE CEN_ID    =" & inBasicParam.openCenID.ToString & ""
            Dim Cnt As Int32 = dbService.GetScalar(ConnectOneWS.Tables.CENTRE_SUPPORT_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_SUPPORT_INFO.ToString(), inBasicParam)
            If Cnt > 0 Then
                OnlineQuery = " UPDATE Centre_Support_Info SET " &
                                         "CEN_ACC_RES_PERSON_AB_ID ='" & UpParam.ResponsiblePersonID & "', " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE CEN_ID    =" & inBasicParam.openCenID.ToString & ""
                dbService.Update(ConnectOneWS.Tables.CENTRE_SUPPORT_INFO, OnlineQuery, inBasicParam)
            Else
                Dim RecID As String = Guid.NewGuid.ToString()
                OnlineQuery = " INSERT INTO [dbo].[centre_support_info] " &
                              " ([CEN_ID],[CEN_ACC_RES_PERSON_AB_ID],[REC_ADD_ON],[REC_ADD_BY],[REC_EDIT_ON],[REC_EDIT_BY],[REC_STATUS],[REC_STATUS_ON],[REC_STATUS_BY],[REC_ID])" &
                              " VALUES(" & inBasicParam.openCenID.ToString & ",'" & UpParam.ResponsiblePersonID & "',GETDATE(),'" & inBasicParam.openUserID & "',GETDATE(),'" & inBasicParam.openUserID & "',1,GETDATE(),'" & inBasicParam.openUserID & "','" & RecID & "')"
                dbService.Insert(ConnectOneWS.Tables.CENTRE_SUPPORT_INFO, OnlineQuery, inBasicParam, RecID)
            End If

            Return True
        End Function
    End Class
#End Region

#Region "Start Menu"
    <Serializable>
    Public Class Center

#Region "Param Classes"
        <Serializable>
        Public Class Param_Center_GetCenterListByAuditor_Instt
            Public UserID As String
            Public InsttID As String
        End Class
        <Serializable>
        Public Class Param_Center_GetCenterListByPAD_Name
            Public PAD_NAME As String
            Public InsttID As String
        End Class
        <Serializable>
        Public Class Param_Center_GetCenterListByCenID
            Public CenID As String
            Public YearID As String
        End Class
        <Serializable>
        Public Class Param_Center_GetTxnStatusCount
            Public FromDate As Date
            Public ToDate As Date
        End Class
        <Serializable>
        Public Class Param_Center_GetTransfersStatus
            Public FromDate As Date
            Public ToDate As Date
        End Class
        <Serializable>
        Public Class Param_AddVerification
            Public Verification_Misc_ID As String
            Public Year_ID As String
        End Class
        <Serializable>
        Public Class Param_AddInsuranceVerification
            Public Year_ID As String
            Public Cen_BK_PAD_No As String
        End Class
        <Serializable>
        Public Class Param_ResumeAudit
            Public Year_ID As Integer
            Public Cen_ID As Integer
        End Class
        <Serializable>
        Public Class Param_get_Client_Audit_Info
            Public INSTTID As String
        End Class
        <Serializable>
        Public Class Param_AddAccountsSubmissionPeriod
            Public FromDate As Date
            Public ToDate As Date
            Public PrevSubmittedTill As Date
        End Class
        <Serializable>
        Public Class Param_GetAccountsSubmissionReport
            Public YearStartDate As Date
            Public YearEndDate As Date
        End Class
#End Region

        ''' <summary>
        ''' Gets List of Child Centres
        ''' </summary>
        ''' <param name="Main_Cen_PAD_No"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetChildCenterList</remarks>
        Public Shared Function GetChildCenterList(ByVal Main_Cen_PAD_No As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR As String = " select CEN_ID from CENTRE_INFO WHERE REC_STATUS IN (0,1,2) and CEN_BK_PAD_NO='" & Main_Cen_PAD_No & " ';"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, SQL_STR, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets ALL siblings
        ''' </summary>
        ''' <param name="Main_Cen_PAD_No"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetChildCenterList</remarks>
        Public Shared Function GetSiblingCenterList(ByVal Cen_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR As String = " select CEN_ID from CENTRE_INFO WHERE REC_STATUS IN (0,1,2) and CEN_BK_PAD_NO=( SELECT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE CEN_ID = " & Cen_ID.ToString & " AND REC_STATUS IN (0,1,2));"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, SQL_STR, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Centre list by Auditor filtered by InsttId
        ''' </summary>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterListByAuditor_Instt</remarks>
        Public Shared Function GetCenterListByAuditor_Instt(ByVal Param As Param_Center_GetCenterListByAuditor_Instt, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim Query As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Query = " SELECT DISTINCT CI.CEN_UID,CI.cen_pad_no AS CEN_PAD_NO, CI.CEN_NAME, CI.CEN_ID,CI.REC_ID, CI_MAIN.CEN_ZONE_ID,CI_MAIN.CEN_ZONE_SUB_ID,Type.ACC_TYPE AS CEN_ACC_TYPE_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, CI_MAIN.CEN_ID AS CEN_ID_MAIN, CI_MAIN.cen_pad_no AS CEN_PAD_NO_MAIN,'' as AuditStatus, CASE WHEN CI.CEN_ID IN ('04216','04218') AND LOWER('" & inBasicParam.openUserID & "') IN ('bkgajendra', 'bkgokul','bkmili','bksaurabh') THEN 1 ELSE 0 END AS IS_VOLUME,CI.CEN_CANCELLATION_DATE " &
                                  " FROM centre_info AS CI INNER JOIN so_client_user_task AS CUT ON CUT.CUT_CEN_ID = CI.CEN_ID INNER JOIN so_client_task_info AS CT ON CUT.CUT_CT_ID = CT.CT_ID AND CT.CT_NAME IN ( 'AUDIT','Account_CashbookAuditor') INNER JOIN centre_info AS CI_MAIN ON ci.cen_bk_pad_no = CI_MAIN.cen_pad_no AND CI_MAIN.CEN_MAIN = 1" &
                                  " INNER JOIN COD_INFO AS cod ON cod.CEN_ID = ci.CEN_ID AND COD_ACTIVE=1 and cod.REC_STATUS  IN (0,1,2) inner join centre_acc_type_info as Type on cod.COD_CEN_ACC_TYPE_ID = Type.REC_ID WHERE CUT_CU_ID = '" & Param.UserID & "' AND  CI.REC_STATUS IN (0,1,2) AND CI.CEN_INS_ID = '" & Param.InsttID & "'" &
                                  " UNION ALL " &
                                  " SELECT CI.CEN_UID,CI.cen_pad_no AS CEN_PAD_NO, CI.CEN_NAME + '(Returned)', CI.CEN_ID,CI.REC_ID, CI_MAIN.CEN_ZONE_ID,CI_MAIN.CEN_ZONE_SUB_ID,Type.ACC_TYPE AS CEN_ACC_TYPE_ID, " &
                                  " COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, CI_MAIN.CEN_ID AS CEN_ID_MAIN, CI_MAIN.cen_pad_no AS CEN_PAD_NO_MAIN,'Returned' as AuditStatus, CASE WHEN CI.CEN_ID IN ('04216','04218') AND '" & inBasicParam.openUserID & "' IN ('bkgajendra', 'bkgokul','bkmili','bksaurabh') THEN 1 ELSE 0 END AS IS_VOLUME,CI.CEN_CANCELLATION_DATE " &
                                  " FROM centre_info CI " &
                                  " INNER JOIN CENTRE_INFO AS CI_MAIN ON CI.CEN_BK_PAD_NO = CI_MAIN.CEN_PAD_NO AND CI_MAIN.CEN_MAIN = 1 " &
                                  " INNER JOIN COD_INFO AS cod ON cod.CEN_ID = ci.CEN_ID AND COD_ACTIVE=1 and cod.REC_STATUS  IN (0,1,2) " &
                                  " INNER JOIN centre_acc_type_info as Type on cod.COD_CEN_ACC_TYPE_ID = Type.REC_ID " &
                                  " LEFT OUTER JOIN So_Center_Audit_Stats A ON CI.CEN_ID= A.CAS_CEN_ID " &
                                  " WHERE A.CAS_AUDIT_STATUS_ID='F32C210E-BC0A-4A8F-8A3F-6FC01C916379' AND A.CAS_STATUS = 1 " &
                                  " AND CI.CEN_INS_ID='" & Param.InsttID & "' AND A.CAS_AUDITOR_ID= '" & Param.UserID & "'" &
                                  " order by cen_name"
            Return dbService.List(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Centre List by PAD
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterListByPAD_Name</remarks>
        Public Shared Function GetCenterListByPAD_Name(ByVal Param As Param_Center_GetCenterListByPAD_Name, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim Query As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Query = " SELECT CI.CEN_UID,CI.cen_pad_no AS CEN_PAD_NO, CI.CEN_NAME + CASE WHEN A.CAS_EVENT_ID IS NULL THEN '' ELSE '(Returned)'END as CEN_NAME, CI.CEN_ID,CI.REC_ID, CI_MAIN.CEN_ZONE_ID,CI_MAIN.CEN_ZONE_SUB_ID, Type.ACC_TYPE AS CEN_ACC_TYPE_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, CI_MAIN.CEN_ID AS CEN_ID_MAIN, CI_MAIN.cen_pad_no AS CEN_PAD_NO_MAIN,CASE WHEN A.CAS_EVENT_ID IS NULL THEN '' ELSE 'Returned' END as AuditStatus, CASE WHEN CI.CEN_ID IN ('04216','04218') AND '" & inBasicParam.openUserID & "' IN ('bkgajendra', 'bkgokul','bkmili','bksaurabh') THEN 1 ELSE 0 END AS IS_VOLUME,CI.CEN_CANCELLATION_DATE " &
                                  " FROM centre_info AS CI INNER JOIN centre_info AS CI_MAIN ON ci.cen_bk_pad_no = CI_MAIN.cen_pad_no AND CI_MAIN.CEN_MAIN = 1" &
                                  " INNER JOIN COD_INFO AS cod ON cod.CEN_ID = ci.CEN_ID AND COD_ACTIVE=1 and cod.REC_STATUS  IN (0,1,2) inner join centre_acc_type_info as Type on cod.COD_CEN_ACC_TYPE_ID = Type.REC_ID " &
                                  " LEFT OUTER JOIN So_Center_Audit_Stats A ON CI.CEN_ID= A.CAS_CEN_ID AND  A.CAS_AUDIT_STATUS_ID='F32C210E-BC0A-4A8F-8A3F-6FC01C916379' AND A.CAS_STATUS = 1  AND A.CAS_AUDITOR_ID= '" & inBasicParam.openUserID & "' WHERE (CI.CEN_UID LIKE '%" & Param.PAD_NAME & "%' OR CI.CEN_NAME LIKE '%" & Param.PAD_NAME & "%' OR CI.cen_pad_no LIKE '%" & Param.PAD_NAME & "%' ) AND  CI.REC_STATUS IN (0,1,2) AND CI.CEN_INS_ID = '" & Param.InsttID & "' order by cen_name"
            Return dbService.List(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetCenterListByCenID(ByVal param As Param_Center_GetCenterListByCenID, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim Query As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Query = " SELECT CI.CEN_UID,CI.cen_pad_no AS CEN_PAD_NO, CI.CEN_NAME, CI.CEN_ID,CI.REC_ID, CI_MAIN.CEN_ZONE_ID,CI_MAIN.CEN_ZONE_SUB_ID, Type.ACC_TYPE AS CEN_ACC_TYPE_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, CI_MAIN.CEN_ID AS CEN_ID_MAIN, CI_MAIN.cen_pad_no AS CEN_PAD_NO_MAIN, CI.CEN_INS_ID, INS_NAME, INS_SHORT, CASE WHEN CI.CEN_ID IN ('04216','04218') AND '" & inBasicParam.openUserID & "' IN ('bkgajendra', 'bkgokul','bkmili','bksaurabh') THEN 1 ELSE 0 END AS IS_VOLUME " &
                                  " FROM centre_info AS CI INNER JOIN centre_info AS CI_MAIN ON ci.cen_bk_pad_no = CI_MAIN.cen_pad_no AND CI_MAIN.CEN_MAIN = 1" &
                                  " INNER JOIN COD_INFO AS cod ON cod.CEN_ID = ci.CEN_ID and cod.REC_STATUS  IN (0,1,2) inner join centre_acc_type_info as Type on cod.COD_CEN_ACC_TYPE_ID = Type.REC_ID INNER JOIN institute_info AS INS ON CI.CEN_INS_ID = INS_ID WHERE CI.CEN_ID = " & param.CenID.ToString & " AND COD_YEAR_ID = " & param.YearID.ToString & " order by cen_name"
            Return dbService.List(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Centre Details By Certificate Number
        ''' </summary>
        ''' <param name="PAD"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterDetailsByCertNo</remarks>
        Public Shared Function GetCenterDetailsByCertNo(ByVal PAD As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim OnlineQuery As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            OnlineQuery = "SELECT CEN_NAME AS CEN_NAME,  " &
                                "COALESCE(CEN_CON_SCANCODE,'') AS CEN_CON_SCANCODE," &
                                "COALESCE(CEN_UID,'') AS UID ," &
                                "COALESCE(CEN_REG_NO,'') AS REG_NO ," &
                                "COALESCE(CEN_B_NAME,'') AS CEN_B_NAME, " &
                                "COALESCE(CEN_ADD1,'') AS CEN_ADD1, " &
                                "COALESCE(CEN_ADD2,'') AS CEN_ADD2, " &
                                "COALESCE(CEN_ADD3,'') AS CEN_ADD3, " &
                                "COALESCE(CEN_ADD4,'') AS CEN_ADD4, " &
                                "COALESCE(CEN_CITY,'') AS CEN_CITY, " &
                                "COALESCE(CEN_STATE,'') AS CEN_STATE, " &
                                "COALESCE(CEN_COUNTRY,'') AS CEN_COUNTRY, " &
                                "COALESCE(CEN_INCHARGE,'') AS CEN_INCHARGE " &
                                "FROM CENTRE_INFO WHERE  REC_STATUS IN (0,1,2) " &
                                "AND CEN_INS_ID='00001' " &
                                "AND UPPER(CEN_PAD_NO)='" & PAD & "' " &
                                "AND CEN_MAIN=1 "
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets List of all institutions' centers present for a BK PAD no 
        ''' </summary>
        ''' <param name="PAD"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterListByBKCertNo</remarks>
        Public Shared Function GetCenterListByBKCertNo(ByVal PAD As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT I.INS_NAME,C.CEN_INS_ID , C.CEN_ID ,C.CEN_PAD_NO ,C.CEN_UID ,CASE WHEN (SELECT COUNT(CEN_ID) FROM cod_info WHERE CEN_ID = C.CEN_ID) > 0 THEN 'Created' ELSE 'Not Created' END  AS CREATION_STATUS, CEN_CON_SCANCODE AS 'Password' " &
                                "FROM CENTRE_INFO  C  INNER JOIN INSTITUTE_INFO  I ON C.CEN_INS_ID=I.INS_ID " &
                                "WHERE(C.REC_STATUS IN (0,1,2)) AND  I.REC_STATUS IN (0,1,2) AND C.CEN_BK_PAD_NO='" & PAD.ToUpper.Trim & "'  " &
                                "ORDER BY C.CEN_INS_ID,C.CEN_ID"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_Contact_Info(ByVal PADNO As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[sp_get_Center_Contact]"
            Dim params() As String = {"@BK_PAD_NO", "@YearID"}
            Dim values() As Object = {PADNO, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.CENTRE_INFO, SPName, ConnectOneWS.Tables.CENTRE_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Makes Session State Active = false for current user  
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_LogOut</remarks>
        Public Shared Function LogOut(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If inBasicParam.openUserID.Length > 0 Then
                Dim Query As String = ""
                Query = " UPDATE so_last_user_session SET LUS_SESSION_ACTIVE = 0, LUS_LAST_ACTIVITY = GETDATE() WHERE LUS_USER_ID = '" & inBasicParam.openUserID & "';"
                dbService.Update(ConnectOneWS.Tables.SO_LAST_USER_SESSION, Query, inBasicParam)
                Return True
            End If
        End Function

        ''' <summary>
        ''' Makes CURRENT CENTER AUDIT STATUS COMPLETED
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_SubmitReport</remarks>
        'Public Shared Function SubmitReport(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    'For Saurabh:Add txn specific Functionality here 
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim Result As Boolean = False
        '    If inBasicParam.openUserID.Length > 0 Then

        '        'delete allotment for self
        '        Dim Query As String = "DELETE FROM so_client_user_task WHERE CUT_CEN_ID ='" + inBasicParam.openCenID + "' AND UPPER(CUT_CU_ID) =UPPER('" + inBasicParam.openUserID + "')"
        '        dbService.Delete_SO_Table_Record(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, Query, inBasicParam)
        '        Result = True

        '        'ADD COMPLETED STATUS 
        '        Query = "INSERT INTO SO_CENTER_AUDIT_STATS (CAS_CEN_ID,CAS_AUDIT_STATUS_ID,CAS_DOCUMENTS_FULLY_COLLECTED,CAS_AUDITOR_ID,CAS_STATUS_START_ON,CAS_STATUS_BY,CAS_STATUS,CAS_EVENT_ID) "
        '        Query += "SELECT TOP 1 '" + inBasicParam.openCenID + "', '0033301d-a56f-4dab-9547-0114ec36830a',CAS_DOCUMENTS_FULLY_COLLECTED,'" + inBasicParam.openUserID + "', GETDATE(),NULL,1,CAS_EVENT_ID from SO_CENTER_AUDIT_STATS WHERE CAS_CEN_ID = '" & inBasicParam.openCenID & "' AND CAS_AUDITOR_ID ='" & inBasicParam.openUserID & "' AND CAS_STATUS = 1 AND CAS_AUDIT_STATUS_ID IN ('003dbf36-a0ad-4c49-946a-0245c5d392f6','3D616AB1-D659-4579-9A42-329DB3882299','E15B3E64-E6A2-4018-9F25-6F3C7791A39D','E7C1571C-F766-4A82-BAF9-38D82E5C213E'); "
        '        If Result Then
        '            dbService.Insert(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, Query, inBasicParam, "")
        '            Result = True ' this is true because if above function fails , the function will exit 
        '        End If

        '        'deactivate last ALLOCATED status
        '        If Result Then
        '            Query = " UPDATE SO_CENTER_AUDIT_STATS SET CAS_STATUS = 0 WHERE CAS_CEN_ID = '" & inBasicParam.openCenID & "' AND UPPER(CAS_AUDITOR_ID) = UPPER('" & inBasicParam.openUserID & "') AND CAS_AUDIT_STATUS_ID IN ('003dbf36-a0ad-4c49-946a-0245c5d392f6','3D616AB1-D659-4579-9A42-329DB3882299','E15B3E64-E6A2-4018-9F25-6F3C7791A39D','E7C1571C-F766-4A82-BAF9-38D82E5C213E') AND CAS_STATUS = 1;"
        '            dbService.Update(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, Query, inBasicParam)
        '        End If
        '        Result = True

        '    End If
        '    Return Result

        'End Function

        Public Shared Function SubmitReport(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Txn_ReportSubmit"
            Dim params() As String = {"cen_id", "user_id"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 255}
            Dim d1 As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, SPName, ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS.ToString(), params, values, dbTypes, lengths, inBasicParam)
            If d1.Rows(0)(0) <> 0 Then
                Return False
            End If
            Return True
        End Function

        Public Shared Function get_Client_Audit_Info(inparam As Param_get_Client_Audit_Info, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Client_Audit_Info"
            Dim params() As String = {"@CENID", "@YEARID", "@AUDITORID", "@INSTTID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, inparam.INSTTID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255, 5}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Audit Txn Period
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetAuditTxnPeriod</remarks>
        Public Shared Function GetAuditTxnPeriod(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim Query As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Query = "SELECT TOP 1 HE_TNXS_FROM AS 'FROM',  HE_TNXS_TO AS 'TO' FROM SO_HO_EVENT_INFO WHERE HE_EVENT_ID = (" &
                                        "SELECT MAX(CAS_EVENT_ID) FROM so_center_audit_stats WHERE CAS_CEN_ID = " + inBasicParam.openCenID.ToString + " AND CAS_AUDITOR_ID = '" + inBasicParam.openUserID + "' AND CAS_STATUS = 1  AND CAS_AUDIT_STATUS_ID IN ('003dbf36-a0ad-4c49-946a-0245c5d392f6','3D616AB1-D659-4579-9A42-329DB3882299','E15B3E64-E6A2-4018-9F25-6F3C7791A39D','E7C1571C-F766-4A82-BAF9-38D82E5C213E'))" &
                                        " AND HE_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ""
            Return dbService.List(ConnectOneWS.Tables.SO_HO_EVENT_INFO, Query, ConnectOneWS.Tables.SO_HO_EVENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetUnlockedTxnCount</remarks>
        Public Shared Function GetUnlockedTxnCount(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim AuditTxnPeriod As DataTable = GetAuditTxnPeriod(inBasicParam)
            If AuditTxnPeriod.Rows.Count = 0 Then Return Nothing
            Dim Message As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Count As Integer = 0
            Dim Query As String = "SELECT COUNT(DISTINCT CASE WHEN tr_m_id IS NULL THEN rec_id ELSE tr_m_id END) AS Cnt FROM transaction_info WHERE CAST(TR_DATE AS DATE) BETWEEN '" + Convert.ToDateTime(AuditTxnPeriod.Rows(0)("FROM").ToString()).ToString(Common.Server_Date_Format_Short) + "' AND '" + Convert.ToDateTime(AuditTxnPeriod.Rows(0)("TO").ToString()).ToString(Common.Server_Date_Format_Short) + "' AND REC_STATUS = 1 AND TR_CEN_ID =" + inBasicParam.openCenID.ToString + " and TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ""
            Count = dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Vouchers : " & Count & vbNewLine

            Query = "SELECT COUNT(OP_AMOUNT) AS Cnt FROM OPENING_BALANCES_INFO WHERE REC_ID LIKE '%CASH-A/C-OP-BALANCE%' AND REC_STATUS = 1 AND OP_CEN_ID =" + inBasicParam.openCenID.ToString + " and OP_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( OP_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR OP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = OP_CEN_ID))  AND (OP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = OP_CEN_ID) OR OP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.OPENING_BALANCES_INFO, Query, ConnectOneWS.Tables.OPENING_BALANCES_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Cash : " & Count & vbNewLine

            Query = "SELECT COUNT(BA_ACCOUNT_NO) AS Cnt FROM BANK_ACCOUNT_INFO WHERE BA_ACCOUNT_NEW IS NULL AND REC_STATUS = 1 AND BA_CEN_ID =" + inBasicParam.openCenID.ToString + "  and BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID))  AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, Query, ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Banks : " & Count & vbNewLine

            Query = "SELECT COUNT(VI_ITEM_ID) AS Cnt FROM VEHICLES_INFO WHERE VI_TR_ID IS NULL AND REC_STATUS = 1 AND vi_cen_id =" + inBasicParam.openCenID.ToString + " and VI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( VI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR VI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = VI_CEN_ID))  AND (VI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = VI_CEN_ID) OR VI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.VEHICLES_INFO, Query, ConnectOneWS.Tables.VEHICLES_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Vehicles : " & Count & vbNewLine

            Query = "SELECT COUNT(LS_NAME) AS Cnt FROM LIVE_STOCK_INFO WHERE LS_TR_ID IS NULL AND REC_STATUS = 1 AND ls_cen_id =" + inBasicParam.openCenID.ToString + "  and LS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LS_CEN_ID))  AND (LS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LS_CEN_ID) OR LS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.LIVE_STOCK_INFO, Query, ConnectOneWS.Tables.LIVE_STOCK_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Livestock : " & Count & vbNewLine

            Query = "SELECT COUNT(LB_PRO_NAME) AS Cnt FROM LAND_BUILDING_INFO WHERE Lb_TR_ID IS NULL AND REC_STATUS = 1 AND lb_CEN_ID =" + inBasicParam.openCenID.ToString + "  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.LAND_BUILDING_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Property : " & Count & vbNewLine

            Query = "SELECT COUNT(DI_DEP_AMT) AS Cnt FROM DEPOSITS_INFO WHERE DI_TR_ID IS NULL AND REC_STATUS = 1 AND Di_CEN_ID =" + inBasicParam.openCenID.ToString + "  and DI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( DI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR DI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = DI_CEN_ID))  AND (DI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = DI_CEN_ID) OR DI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.DEPOSITS_INFO, Query, ConnectOneWS.Tables.DEPOSITS_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Deposits : " & Count & vbNewLine

            Query = "SELECT COUNT(AI_ADV_AMT) AS Cnt FROM ADVANCES_INFO WHERE aI_TR_ID IS NULL AND REC_STATUS = 1 AND ai_cen_id =" + inBasicParam.openCenID.ToString + " and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.ADVANCES_INFO, Query, ConnectOneWS.Tables.ADVANCES_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Advances : " & Count & vbNewLine

            Query = "SELECT COUNT(LI_AMT) AS Cnt FROM LIABILITIES_INFO WHERE lI_TR_ID IS NULL AND REC_STATUS = 1 AND Li_cen_id =" + inBasicParam.openCenID.ToString + "  and LI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LI_CEN_ID))  AND (LI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LI_CEN_ID) OR LI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.LIABILITIES_INFO, Query, ConnectOneWS.Tables.LIABILITIES_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Liabilities : " & Count & vbNewLine

            Query = "SELECT COUNT(AI_AMT_FOR_INS) AS Cnt FROM ASSET_INFO WHERE AI_TR_ID IS NULL AND REC_STATUS = 1 AND Ai_cen_id =" + inBasicParam.openCenID.ToString + "  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.ASSET_INFO, Query, ConnectOneWS.Tables.ASSET_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Assets : " & Count & vbNewLine

            Query = "SELECT COUNT(FD_AMT) AS Cnt FROM FD_INFO WHERE (FD_TR_ID IS NULL OR FD_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & ") AND REC_STATUS = 1 AND FD_cen_id =" + inBasicParam.openCenID.ToString + " and FD_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( FD_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID))  AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.FD_INFO, Query, ConnectOneWS.Tables.FD_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "FD : " & Count & vbNewLine

            Query = "SELECT COUNT(CS_AMOUNT) AS Cnt FROM consumables_stock_info WHERE REC_STATUS = 1 AND cs_cen_id =" + inBasicParam.openCenID.ToString + " and CS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( CS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR CS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = CS_CEN_ID))  AND (CS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = CS_CEN_ID) OR CS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Count = dbService.GetScalar(ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO, Query, ConnectOneWS.Tables.CONSUMABLES_STOCK_INFO.ToString(), inBasicParam)
            If Count > 0 Then Message += "Opening Consumable Stock : " & Count & vbNewLine
            Return Message
        End Function

        ''' <summary>
        ''' GETS UNLOCKED ENTRIES COUNT FOR CURRENT AUDIT EVENT SPECIFIED PERIOD LEAVING NOTEBOOK ENTRIES 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetTxnStatusCount</remarks>
        Public Shared Function GetTxnStatusCount(ByVal Param As Param_Center_GetTxnStatusCount, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim Query As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Query = "SELECT 'VOUCHERS' as Screen, 'NOT LOCKED' as Status, COUNT(DISTINCT CASE WHEN tr_m_id IS NULL THEN rec_id ELSE tr_m_id END) AS Cnt, COALESCE(SUM(TR_AMOUNT),0) AS Amount  FROM transaction_info WHERE REC_STATUS IN (1) AND TR_CEN_ID =" + inBasicParam.openCenID.ToString + " AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FromDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') and TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  GROUP BY TR_CEN_ID "
            Query += "UNION SELECT 'VOUCHERS' as Screen, 'LOCKED' as Status, COUNT(DISTINCT CASE WHEN tr_m_id IS NULL THEN rec_id ELSE tr_m_id END) AS Cnt, COALESCE(SUM(TR_AMOUNT),0) AS Amount  FROM transaction_info WHERE REC_STATUS IN (2) AND TR_CEN_ID =" + inBasicParam.openCenID.ToString + "  AND (CAST(TR_DATE AS DATE) BETWEEN '" & Param.FromDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Short) & "') AND TR_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & "  GROUP BY TR_CEN_ID "
            Query += "UNION SELECT 'CASH ' as Screen, 'NOT LOCKED' as Status, COUNT(OP_AMOUNT)  AS Cnt, COALESCE(SUM(OP_AMOUNT),0) AS Amount FROM OPENING_BALANCES_INFO WHERE REC_ID LIKE '%CASH-A/C-OP-BALANCE%' AND REC_STATUS = 1 AND OP_CEN_ID =" + inBasicParam.openCenID.ToString + "  and OP_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( OP_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR OP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = OP_CEN_ID))  AND (OP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = OP_CEN_ID) OR OP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'CASH ' as Screen, 'LOCKED' as Status, COUNT(OP_AMOUNT)  AS Cnt, COALESCE(SUM(OP_AMOUNT),0) AS Amount FROM OPENING_BALANCES_INFO WHERE REC_ID LIKE '%CASH-A/C-OP-BALANCE%' AND REC_STATUS = 2 AND OP_CEN_ID =" + inBasicParam.openCenID.ToString + "  and OP_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( OP_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR OP_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = OP_CEN_ID))  AND (OP_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = OP_CEN_ID) OR OP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'BANK ' as Screen, 'NOT LOCKED' as Status, COUNT(BA_ACCOUNT_NO) AS Cnt, COALESCE(SUM(op.OP_AMOUNT),0) AS Amount  FROM BANK_ACCOUNT_INFO AS BA INNER JOIN OPENING_BALANCES_INFO  AS OP ON BA.rec_id = OP.REC_ID WHERE BA_ACCOUNT_NEW IS NULL AND ba.REC_STATUS = 1 AND BA_CEN_ID =" + inBasicParam.openCenID.ToString + "  and BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID))  AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'BANK ' as Screen, 'LOCKED' as Status, COUNT(BA_ACCOUNT_NO) AS Cnt, COALESCE(SUM(op.OP_AMOUNT),0) AS Amount FROM BANK_ACCOUNT_INFO AS BA INNER JOIN OPENING_BALANCES_INFO  AS OP ON BA.rec_id = OP.REC_ID  WHERE BA_ACCOUNT_NEW IS NULL AND ba.REC_STATUS = 2 AND BA_CEN_ID =" + inBasicParam.openCenID.ToString + "  and BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID))  AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'VEHICLES ' as Screen, 'NOT LOCKED' as Status, COUNT(vI_ITEM_ID) AS Cnt, COALESCE(SUM(VI_AMOUNT),0) AS Amount FROM VEHICLES_INFO WHERE REC_STATUS = 1 AND vi_cen_id =" + inBasicParam.openCenID.ToString + "  and VI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( VI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR VI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = VI_CEN_ID))  AND (VI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = VI_CEN_ID) OR VI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'VEHICLES ' as Screen, 'LOCKED' as Status, COUNT(vI_ITEM_ID) AS Cnt, COALESCE(SUM(VI_AMOUNT),0) AS Amount FROM VEHICLES_INFO WHERE REC_STATUS = 2 AND vi_cen_id =" + inBasicParam.openCenID.ToString + "  and VI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( VI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR VI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = VI_CEN_ID))  AND (VI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = VI_CEN_ID) OR VI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'LIVESTOCK ' as Screen, 'NOT LOCKED' as Status, COUNT(LS_NAME) AS Cnt, COALESCE(SUM(LS_AMT),0) AS Amount FROM LIVE_STOCK_INFO WHERE REC_STATUS = 1 AND ls_cen_id =" + inBasicParam.openCenID.ToString + "  and LS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LS_CEN_ID))  AND (LS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LS_CEN_ID) OR LS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'LIVESTOCK ' as Screen, 'LOCKED' as Status, COUNT(LS_NAME) AS Cnt, COALESCE(SUM(LS_AMT),0) AS Amount FROM LIVE_STOCK_INFO WHERE REC_STATUS = 2 AND ls_cen_id =" + inBasicParam.openCenID.ToString + "  and LS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LS_CEN_ID))  AND (LS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LS_CEN_ID) OR LS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'PROPERTY ' as Screen, 'NOT LOCKED' as Status, COUNT(LB_PRO_NAME) AS Cnt, COALESCE(SUM(LB_VALUE),0) AS Amount FROM LAND_BUILDING_INFO WHERE REC_STATUS = 1 AND lb_CEN_ID =" + inBasicParam.openCenID.ToString + "  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'PROPERTY ' as Screen, 'LOCKED' as Status, COUNT(LB_PRO_NAME) AS Cnt, COALESCE(SUM(LB_VALUE),0) AS Amount FROM LAND_BUILDING_INFO WHERE REC_STATUS = 2 AND lb_CEN_ID =" + inBasicParam.openCenID.ToString + "  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'DEPOSITS ' as Screen, 'NOT LOCKED' as Status, COUNT(DI_DEP_AMT) AS Cnt, COALESCE(SUM(DI_DEP_AMT),0) AS Amount FROM DEPOSITS_INFO WHERE REC_STATUS = 1 AND Di_CEN_ID =" + inBasicParam.openCenID.ToString + "  and DI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( DI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR DI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = DI_CEN_ID))  AND (DI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = DI_CEN_ID) OR DI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'DEPOSITS ' as Screen, 'LOCKED' as Status, COUNT(DI_DEP_AMT) AS Cnt, COALESCE(SUM(DI_DEP_AMT),0) AS Amount  FROM DEPOSITS_INFO WHERE REC_STATUS = 2 AND Di_CEN_ID =" + inBasicParam.openCenID.ToString + "  and DI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( DI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR DI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = DI_CEN_ID))  AND (DI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = DI_CEN_ID) OR DI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'ADVANCES ' as Screen, 'NOT LOCKED' as Status, COUNT(AI_ADV_AMT) AS Cnt, COALESCE(SUM(AI_ADV_AMT),0) AS Amount FROM ADVANCES_INFO WHERE REC_STATUS = 1 AND ai_cen_id =" + inBasicParam.openCenID.ToString + "  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'ADVANCES ' as Screen, 'LOCKED' as Status, COUNT(AI_ADV_AMT) AS Cnt, COALESCE(SUM(AI_ADV_AMT),0) AS Amount  FROM ADVANCES_INFO WHERE REC_STATUS = 2 AND ai_cen_id =" + inBasicParam.openCenID.ToString + "  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'LIABILITIES ' as Screen, 'NOT LOCKED' as Status, COUNT(LI_AMT) AS Cnt, COALESCE(SUM(LI_AMT),0) AS Amount  FROM LIABILITIES_INFO WHERE REC_STATUS = 1 AND Li_cen_id =" + inBasicParam.openCenID.ToString + "  and LI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LI_CEN_ID))  AND (LI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LI_CEN_ID) OR LI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'LIABILITIES ' as Screen, 'LOCKED' as Status, COUNT(LI_AMT) AS Cnt, COALESCE(SUM(LI_AMT),0) AS Amount  FROM LIABILITIES_INFO WHERE REC_STATUS = 2 AND Li_cen_id =" + inBasicParam.openCenID.ToString + "  and LI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LI_CEN_ID))  AND (LI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LI_CEN_ID) OR LI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'ASSETS ' as Screen, 'NOT LOCKED' as Status, COUNT(AI_AMT_FOR_INS) AS Cnt, COALESCE(SUM(AI_PUR_AMT),0) AS Amount  FROM ASSET_INFO WHERE REC_STATUS = 1 AND Ai_cen_id =" + inBasicParam.openCenID.ToString + "  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'ASSETS ' as Screen, 'LOCKED' as Status, COUNT(AI_AMT_FOR_INS) AS Cnt, COALESCE(SUM(AI_PUR_AMT),0) AS Amount  FROM ASSET_INFO WHERE REC_STATUS = 2 AND Ai_cen_id =" + inBasicParam.openCenID.ToString + "  and AI_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( AI_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR AI_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = AI_CEN_ID))  AND (AI_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = AI_CEN_ID) OR AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'CONSUMABLES ' as Screen, 'NOT LOCKED' as Status, COUNT(CS_AMOUNT) AS Cnt, COALESCE(SUM(CS_AMOUNT),0) AS Amount  FROM consumables_stock_info WHERE REC_STATUS = 1 AND cs_cen_id =" + inBasicParam.openCenID.ToString + "  and CS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( CS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR CS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = CS_CEN_ID))  AND (CS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = CS_CEN_ID) OR CS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'CONSUMABLES ' as Screen, 'LOCKED' as Status, COUNT(CS_AMOUNT) AS Cnt, COALESCE(SUM(CS_AMOUNT),0) AS Amount  FROM consumables_stock_info WHERE REC_STATUS = 2 AND cs_cen_id =" + inBasicParam.openCenID.ToString + "  and CS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( CS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR CS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = CS_CEN_ID))  AND (CS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = CS_CEN_ID) OR CS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'GOLD/SILVER ' as Screen, 'NOT LOCKED' as Status, COUNT(GS_ITEM_WEIGHT) AS Cnt,0 AS Amount  FROM gold_silver_info WHERE REC_STATUS = 1 AND gs_cen_id =" + inBasicParam.openCenID.ToString + "  and GS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( GS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR GS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = GS_CEN_ID))  AND (GS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = GS_CEN_ID) OR GS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'GOLD/SILVER ' as Screen, 'LOCKED' as Status, COUNT(GS_ITEM_WEIGHT) AS Cnt,0 AS Amount  FROM gold_silver_info WHERE REC_STATUS = 2 AND gs_cen_id =" + inBasicParam.openCenID.ToString + "  and GS_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( GS_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR GS_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = GS_CEN_ID))  AND (GS_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = GS_CEN_ID) OR GS_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'FD ' as Screen, 'NOT LOCKED' as Status, COUNT(FD_AMT) AS Cnt,COALESCE(SUM(FD_AMT),0) AS Amount  FROM FD_info WHERE REC_STATUS = 1 AND FD_cen_id =" + inBasicParam.openCenID.ToString + "  and FD_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( FD_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID))  AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Query += "UNION SELECT 'FD ' as Screen, 'LOCKED' as Status, COUNT(FD_AMT) AS Cnt,COALESCE(SUM(FD_AMT),0) AS Amount  FROM FD_info WHERE REC_STATUS = 2 AND FD_cen_id =" + inBasicParam.openCenID.ToString + "  and FD_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( FD_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR FD_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = FD_CEN_ID))  AND (FD_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = FD_CEN_ID) OR FD_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"

            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Transfer Status
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>CentreRelated_Center_GetTransfersStatus, uses Param_Center_GetTransfersStatus</remarks>
        Public Shared Function GetTransfersStatus(ByVal inParam As Param_Center_GetTransfersStatus, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT (SELECT COUNT(TR_AMOUNT) FROM TRANSACTION_INFO WHERE TR_CODE = 8 AND REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID IS NULL AND TR_SR_NO = 1 AND TR_CEN_ID =" & inBasicParam.openCenID.ToString & "AND (CAST(TR_DATE AS DATE) BETWEEN '" & inParam.FromDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & inParam.ToDate.ToString(Common.Server_Date_Format_Short) & "')) AS UNMATCHED , "
            Query += "(SELECT COUNT(TR_AMOUNT) FROM TRANSACTION_INFO WHERE TR_CODE = 8 AND REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID IS NOT NULL AND TR_SR_NO = 1 AND TR_CEN_ID =" & inBasicParam.openCenID.ToString & " AND (CAST(TR_DATE AS DATE) BETWEEN '" & inParam.FromDate.ToString(Common.Server_Date_Format_Short) & "' AND '" & inParam.ToDate.ToString(Common.Server_Date_Format_Short) & "')) AS MATCHED"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Inserts audit Verifications
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AddNewVerification(ByVal InParam As Param_AddVerification, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO so_audit_verifications(AV_CEN_ID,AV_YEAR_ID,AV_VERIFICATION_MISC_ID" &
                                                        ") VALUES(" &
                                                        "" & inBasicParam.openCenID.ToString & ", " &
                                                        "" & InParam.Year_ID.ToString & ", " &
                                                        "'" & InParam.Verification_Misc_ID & "') "
            dbService.Insert(ConnectOneWS.Tables.SO_AUDIT_VERIFICATIONS, OnlineQuery, inBasicParam, "")
            Return True
        End Function

        Public Shared Function AddInsuranceVerification(ByVal InParam As Param_AddInsuranceVerification, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_InsuranceVerification"
            Dim params() As String = {"CEN_BK_PADNO", "AV_YEAR_ID"}
            Dim values() As Object = {InParam.Cen_BK_PAD_No, InParam.Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SO_AUDIT_VERIFICATIONS, SPName, ConnectOneWS.Tables.SO_AUDIT_VERIFICATIONS.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function VerifyAudit(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Txn_VerifyAudit"
            Dim params() As String = {"CEN_ID", "USER_ID", "YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openUserID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 255, 4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function ReturnForCorrection(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Txn_ReturnForCorrection"
            Dim params() As String = {"CEN_ID", "USER_ID", "YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openUserID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 255, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function ResumeAudit(inparam As Param_ResumeAudit, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            inBasicParam.openCenID = inparam.Cen_ID
            inBasicParam.openYearID = inparam.Year_ID
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Txn_ResumeAudit"
            Dim params() As String = {"CEN_ID", "USER_ID", "YEAR_ID"}
            Dim values() As Object = {inparam.Cen_ID, inBasicParam.openUserID, inparam.Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 255, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Returns Completed Audit Verifications for a Centre and Year ID 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <param name="openYearID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAuditsVerificationsCompleted(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select AV_VERIFICATION_MISC_ID from so_audit_verifications where AV_CEN_ID =" & inBasicParam.openCenID.ToString & " and AV_YEAR_ID=" & inBasicParam.openYearID.ToString & ""
            Return dbService.List(ConnectOneWS.Tables.SO_AUDIT_VERIFICATIONS, Query, ConnectOneWS.Tables.SO_AUDIT_VERIFICATIONS.ToString(), inBasicParam)
        End Function

        Public Shared Function IsFinalAuditCompleted(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select COUNT(CAS_CEN_ID) from so_center_audit_stats as CAS INNER JOIN so_ho_event_info AS EVENT ON CAS.CAS_EVENT_ID = event.HE_EVENT_ID where cas_cen_id = " & inBasicParam.openCenID.ToString & " AND HE_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  AND CAS_STATUS = 1 AND CAS_AUDIT_STATUS_ID IN ('00351e0d-d326-4cd0-b1f1-ba42752b2242','0685db2b-26ae-40ec-85f8-5d43488c02b4') and HE_CATEGORY = 'FINAL AUDIT'"
            Dim VerificationCount As Int32 = dbService.GetScalar(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, Query, ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS.ToString(), inBasicParam)
            If VerificationCount > 0 Then Return True
            Return False
        End Function

        Public Shared Function IsFinalReportSubmitted(Cen_BK_Pad_No As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select case when ((SELECT count(cas_cen_id)" &
                 "FROM so_center_audit_stats AS CAS INNER JOIN so_ho_event_info AS EVENT ON CAS.CAS_EVENT_ID = event.HE_EVENT_ID " &
                 "  WHERE" &
                 " (CAS_AUDIT_STATUS_ID = '0033301d-a56f-4dab-9547-0114ec36830a'  " &
                 " OR CAS_AUDIT_STATUS_ID = '0685db2b-26ae-40ec-85f8-5d43488c02b4'  " &
                 " OR CAS_AUDIT_STATUS_ID = '00351e0d-d326-4cd0-b1f1-ba42752b2242' ) " &
                 " AND CAS_CEN_ID in (select cen_id from centre_info where CEN_BK_PAD_NO = '" & Cen_BK_Pad_No & "' AND CEN_CANCELLATION_DATE IS NULL) " &
                 " AND HE_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " &
                 " AND CAS_STATUS = 1 AND HE_CATEGORY = 'FINAL AUDIT') = (select  count(ci.cen_id) from centre_info CI inner join cod_info as cod on ci.CEN_ID = cod.CEN_ID where  CEN_BK_PAD_NO = '" & Cen_BK_Pad_No & "' and COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  AND CEN_CANCELLATION_DATE IS NULL)) then 1 else 0 end as 'Report_Submitted' "
            Dim IsReportSubmitted As Int32 = dbService.GetScalar(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, Query, ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS.ToString(), inBasicParam)
            If IsReportSubmitted = 1 Then Return True
            Return False
        End Function

        Public Shared Function GetAuditPeriod(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select eve.HE_TNXS_FROM, eve.HE_TNXS_TO as HE_TO from so_center_audit_stats as cas " &
                                "inner join so_ho_event_info as eve on cas.CAS_EVENT_ID = eve.HE_EVENT_ID " &
                                "where  CAS_AUDIT_STATUS_ID IN ('003dbf36-a0ad-4c49-946a-0245c5d392f6','3D616AB1-D659-4579-9A42-329DB3882299','E15B3E64-E6A2-4018-9F25-6F3C7791A39D','E7C1571C-F766-4A82-BAF9-38D82E5C213E') " &
                                "AND CAS_STATUS = 1 AND CAS_CEN_ID = " & inBasicParam.openCenID.ToString & " AND CAS_AUDITOR_ID ='" & inBasicParam.openUserID & "' "
            Return dbService.List(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, Query, ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_Base_OpenEventId(inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_openEventID"
            Dim params() As String = {"CENID", "@YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 5}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function IsReportSubmitted(openEventID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT count(cas_cen_id)" &
                        "FROM so_center_audit_stats" &
                        "WHERE" &
                        "(CAS_AUDIT_STATUS_ID = '0033301d-a56f-4dab-9547-0114ec36830a'  " &
                        "OR CAS_AUDIT_STATUS_ID = '0685db2b-26ae-40ec-85f8-5d43488c02b4'  " &
                        "OR CAS_AUDIT_STATUS_ID = '00351e0d-d326-4cd0-b1f1-ba42752b2242' ) " &
                        " AND CAS_CEN_ID =" & inBasicParam.openCenID.ToString & "" &
                        " AND CAS_STATUS = 1 AND CAS_EVENT_ID  = " & openEventID & " "
            Dim IsRepSubmitted As Int32 = dbService.GetScalar(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, Query, ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS.ToString(), inBasicParam)
            If IsRepSubmitted = 1 Then Return True
            Return False
        End Function

        Public Shared Function GetAddressesForLabels(param As Addresses.Param_GetAddressesForLabels, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim _SqlCityStyle As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If param.rbt_CityStyle1_Checked Then
                _SqlCityStyle = " LTRIM(RTRIM(ISNULL(CT.CI_NAME,''))) "
            End If

            If param.rbt_CityStyle2_Checked Then
                _SqlCityStyle = " CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 THEN LTRIM(RTRIM(ISNULL(CT.CI_NAME,''))) ELSE '' END " &
                                " +  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 AND LEN(LTRIM(RTRIM(ISNULL(A.CEN_PINCODE,'')))) > 0 THEN ' - ' ELSE '' END " &
                                " +  LTRIM(RTRIM(ISNULL(A.CEN_PINCODE,''))) "
            End If

            If param.rbt_CityStyle3_Checked Then
                _SqlCityStyle = " CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 THEN LTRIM(RTRIM(ISNULL(CT.CI_NAME,''))) ELSE '' END " &
                                " +  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(ST.ST_SHORT,'')))) > 0 THEN ' ( ' + LTRIM(RTRIM(ISNULL(ST.ST_SHORT + ' )',''))) ELSE '' END " &
                                " +  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 AND LEN(LTRIM(RTRIM(ISNULL(A.CEN_PINCODE,'')))) > 0 THEN ' - ' ELSE '' END " &
                                " +  LTRIM(RTRIM(ISNULL(A.CEN_PINCODE,''))) "



            End If

            Dim BKPADs As String = " "
            If param.FilterBy.Contains("BK PAD") And param.FilterValue.Trim.Length > 0 Then
                For Each PAD As String In param.FilterValue.Split(",")
                    BKPADs += "'" + PAD + "',"
                Next
                BKPADs = BKPADs.Substring(0, BKPADs.Length - 1)
            Else
                BKPADs = " A.CEN_BK_PAD_NO "
            End If
            '" + CASE WHEN LEN(COALESCE(A.CEN_TEL_NO_1,'')) > 0 THEN ' Tel: ' + A.CEN_TEL_NO_1 ELSE '' END " & _
            '           " + CASE WHEN LEN(COALESCE(A.CEN_MOB_NO_1,'')) > 0 THEN ' Mob: ' + A.CEN_MOB_NO_1 ELSE '' END," & _
            Dim _query = " SELECT A.CEN_BK_PAD_NO AS NO,                              " &
                        " 'BRAHMA KUMARIS' AS NAME,              " &
                        "  CASE WHEN LEN(COALESCE(A.CEN_B_NAME ,'')) = 0 THEN '' ELSE A.CEN_B_NAME + ', ' END + RTRIM(LTRIM(ISNULL(A.CEN_ADD1,''))) AS ADD1, " &
                        "  RTRIM(LTRIM(ISNULL(A.CEN_ADD2,''))) AS ADD2, " &
                        "  RTRIM(LTRIM(ISNULL(A.CEN_ADD3,''))) AS ADD3, " &
                        "  RTRIM(LTRIM(ISNULL(A.CEN_ADD4,''))) + CASE WHEN LEN(RTRIM(LTRIM(ISNULL(A.CEN_ADD4,'')))) > 0 THEN ', ' ELSE '' END + CASE WHEN COALESCE(DI_NAME,'') <> COALESCE(CI_NAME,'') THEN ISNULL('Dist. ' +DI.DI_NAME + ', ','') ELSE '' END  AS ADD4, " &
                        "  " & _SqlCityStyle & " AS ADD5, " &
                        "  CASE WHEN LEN(COALESCE(A.CEN_TEL_NO_1,'')) > 0 THEN 'Tel: ' + A.CEN_TEL_NO_1 + ' ' ELSE '' END + CASE WHEN LEN(COALESCE(A.CEN_MOB_NO_1,'')) > 0 THEN 'Mob: ' + A.CEN_MOB_NO_1 ELSE '' END AS ADD6," &
                        "  A.REC_ID AS ID                               " &
                        "  FROM      CENTRE_INFO       AS A    " &
                        "  LEFT JOIN MAP_CITY_INFO      AS CT   ON (A.CEN_CITY_ID     = CT.REC_ID   AND     CT.REC_STATUS IN (0,1,2) ) " &
                        "  LEFT JOIN MAP_STATE_INFO     AS ST   ON (A.CEN_STATE_ID    = ST.REC_ID   AND     ST.REC_STATUS IN (0,1,2) ) " &
                        "  LEFT JOIN MAP_DISTRICT_INFO  AS DI 	ON (A.CEN_DISTRICT_ID = DI.REC_ID   AND     DI.REC_STATUS IN (0,1,2) ) " &
                        "  WHERE A.REC_STATUS IN (0,1,2) AND CEN_MAIN =1 " &
                        "  AND COALESCE(CT.CI_NAME,'') LIKE CASE WHEN '" & param.FilterBy & "' = 'a) City' THEN '%" & param.FilterValue & "%' ELSE COALESCE(CT.CI_NAME,'') END " &
                        "  AND COALESCE(ST.ST_NAME,'') LIKE CASE WHEN '" & param.FilterBy & "' = 'b) State' THEN '%" & param.FilterValue & "%' ELSE COALESCE(ST.ST_NAME,'') END " &
                        "  AND COALESCE(CEN_ZONE_ID,'') LIKE CASE WHEN '" & param.FilterBy & "' = 'c) Zone' THEN '%" & param.FilterValue & "%' ELSE COALESCE(CEN_ZONE_ID,'') END " &
                        "  AND A.CEN_NAME LIKE CASE WHEN '" & param.FilterBy & "' = 'd) Name (Contains)' THEN '%" & param.FilterValue & "%' ELSE A.CEN_NAME END " &
                        "  AND A.CEN_PINCODE LIKE CASE WHEN '" & param.FilterBy & "' = 'e) Pincode' THEN '%" & param.FilterValue & "%' ELSE A.CEN_PINCODE END " &
                        "  AND A.CEN_BK_PAD_NO IN (" & BKPADs & ")" &
                        "  ORDER BY RTRIM(LTRIM(A.CEN_NAME)) "
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, _query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetlatestCenterGrade(EventID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Latest_CenterEvent_Grade"
            Dim params() As String = {"CENID", "AUDITORID", "EVENT_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openUserID, EventID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 255, 4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.SO_CENTER_AUDIT_STATS, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Accounts Submitted period for Open Center and Open Year
        ''' </summary>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAccountsSubmittedPeriod(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'change logic as after period submitted previously, once locks are implemented
            Dim Query As String = "SELECT CSA_SUBMIT_FROM, CSA_SUBMIT_TO, CSA_SUBMIT_ON, CSA_SUBMIT_BY FROM so_center_submission_accounts WHERE CSA_CEN_ID = " & inBasicParam.openCenID.ToString & " AND CSA_YEAR_ID =  " & inBasicParam.openYearID.ToString & " ORDER BY CSA_SUBMIT_ON DESC "
            Return dbService.List(ConnectOneWS.Tables.SO_CENTER_SUBMISSION_ACCOUNTS, Query, ConnectOneWS.Tables.SO_CENTER_SUBMISSION_ACCOUNTS.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Adds/update Period of Accounts Submission 
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AddAccountsSubmissionPeriod(ByVal InParam As Param_AddAccountsSubmissionPeriod, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'use InParam.PrevSubmittedTill to change From date as after period submitted previously, once locks are implemented
            Dim OnlineQuery As String = "INSERT INTO so_center_submission_accounts(CSA_CEN_ID,CSA_YEAR_ID,CSA_SUBMIT_FROM,CSA_SUBMIT_TO,CSA_SUBMIT_ON,CSA_SUBMIT_BY" &
                                        ") VALUES(" &
                                        "" & inBasicParam.openCenID.ToString & "," & inBasicParam.openYearID.ToString & ", " &
                                        IIf(InParam.ToDate <> Date.MinValue, "'" & InParam.FromDate.ToString(Common.Server_Date_Format_Long) & "'", "NULL") & ", " &
                                        IIf(InParam.ToDate <> Date.MinValue, "'" & InParam.ToDate.ToString(Common.Server_Date_Format_Long) & "'", "NULL") & ",GETDATE(),'" & inBasicParam.openUserID & "') "
            dbService.Insert(ConnectOneWS.Tables.SO_CENTER_SUBMISSION_ACCOUNTS, OnlineQuery, inBasicParam, "")

            'Delete Existing Restriction for accounts submission 
            OnlineQuery = "DELETE FROM SO_CLIENT_RESTRICTIONS WHERE CR_TYPE='READ_ALL_WRITE_BLOCKED_ACCOUNTS_SUBMITTED' AND CR_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            dbService.Delete_SO_Table_Record(ConnectOneWS.Tables.SO_CLIENT_RESTRICTIONS, OnlineQuery, inBasicParam)

            'Insert Restriction for accounts submission
            If (InParam.ToDate > DateTime.MinValue) Then
                OnlineQuery = "INSERT INTO [dbo].[so_client_restrictions]([CR_CEN_ID],[CR_SCREEN],[CR_FROMDATE],[CR_TODATE],[CR_TYPE],[CR_EVENT_ID],[CR_USER_ID]) " &
                " VALUES   (" & inBasicParam.openCenID.ToString & " , NULL , '" & InParam.FromDate.ToString(Common.Server_Date_Format_Long) & "', '" & InParam.ToDate.ToString(Common.Server_Date_Format_Long) & "','READ_ALL_WRITE_BLOCKED_ACCOUNTS_SUBMITTED',NULL,'" & inBasicParam.openUserID & "')"
                dbService.Insert(ConnectOneWS.Tables.SO_CLIENT_RESTRICTIONS, OnlineQuery, inBasicParam, "")
            End If

            Return True
        End Function

        Public Shared Function GetAccountsSubmissionReport(inparam As Param_GetAccountsSubmissionReport, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_rpt_Center_Account_Submission"
            Dim params() As String = {"CENID", "YEARID", "YR_START_DATE", "YR_END_DATE"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inparam.YearStartDate, inparam.YearEndDate}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.DateTime2}
            Dim lengths() As Integer = {5, 4, 20, 20}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SO_CENTER_SUBMISSION_ACCOUNTS, SPName, ConnectOneWS.Tables.SO_CENTER_SUBMISSION_ACCOUNTS.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
    End Class
    <Serializable>
    Public Class ClientUserInfo
#Region "Param Classes"
        <Serializable>
        Public Class Param_ClientUserInfo_GetUserInfo
            Public Cen_ID As Integer
            Public User_ID As String
        End Class
        <Serializable>
        Public Class Parameter_AddNew_ClientUserInfo
            Public Cen_Id As Integer
            Public Cen_PAD_No As String
            Public Cen_Password As String
        End Class
        <Serializable>
        Public Class Parameter_CPwd_ClientUserInfo
            Public Cen_Id As Integer
            Public UserID As String
            Public Cen_Password As String
            Public screen As ConnectOneWS.ClientScreen
        End Class
        <Serializable>
        Public Class Param_ClientUserInfo_GetListFilteredByCenIDUserID
            Public UserID As String
            Public CenID As Integer
        End Class
        <Serializable>
        Public Class Param_InsertClientUser
            Public USER_Name As String
            Public USER_PERSONNEL_ID As Int32?
            Public USER_IS_ADMIN As Boolean = False
            Public SelfPostedOnly As Boolean = False
            Public Mapped_Group_IDs As List(Of Int32)
            Public User_Password As String
        End Class
        <Serializable>
        Public Class Param_UpdateClientUser
            Public USER_ID As String
            Public USER_Name As String
            Public USER_PERSONNEL_ID As Int32?
            Public USER_IS_ADMIN As Boolean = False
            Public SelfPostedOnly As Boolean = False
            Public Mapped_Group_IDs As List(Of Int32)
        End Class
        <Serializable>
        Public Class Param_InsertClientUserGroup
            Public Group_Name As String
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_UpdateClientUserGroup
            Public Group_Name As String
            Public Remarks As String
            Public Group_ID As Int32
        End Class
        <Serializable>
        Public Class Param_GetUserNameCount
            Public User_Name As String
            Public CenID As Int32
            Public UserRecID As String
        End Class
        <Serializable>
        Public Class Param_GetGroupNameCount
            Public Group_Name As String
            Public GroupID As Int32
        End Class
        <Serializable>
        Public Class Param_SaveClientUserGroupMapping
            Public GroupID As Int32
            Public MappedUserIDs As List(Of String)
        End Class
        <Serializable>
        Public Class Param_GetPrivilegeRegister
            Public GroupID As Int32?
            Public UserID As String
        End Class
        <Serializable>
        Public Class Param_InsertClientUserPrivileges
            Public UserName As String
            Public TaskID As Int32
            Public Privilege_Code As String
            Public GroupID As Int32?
        End Class
        <Serializable>
        Public Class Param_UpdateClientUserPrivileges
            Inherits Param_InsertClientUserPrivileges
            Public Rec_ID As Int32
        End Class


#End Region

        ''' <summary>
        ''' Gets User Info with custom CenID
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfoCustomCenId</remarks>
        Public Shared Function GetCenterUserInfo(ByVal Param As Param_ClientUserInfo_GetUserInfo, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR As String = " SELECT USER_ID, USER_PWD, USER_ROLE_ID, USER_IS_ADMIN, COALESCE(SUB_DEPT.SD_Is_Central_Store,0) IS_CENTRAL_STORE, PERS.REC_ID AS PersonnelID, COALESCE(MAIN_DEPT.Rec_ID,SUB_DEPT.Rec_ID) as MainDeptID, SUB_DEPT.Rec_ID as SubDeptID, SELFPOSTED  FROM CLIENT_USER_INFO CU LEFT OUTER JOIN Stock_Personnel_Info AS PERS ON CU.USER_PERSONNEL_ID = PERS.REC_ID LEFT OUTER JOIN Store_Dept_Info AS SUB_DEPT ON PERS.Pers_Dept_ID = SUB_DEPT.REC_ID LEFT OUTER JOIN Store_Dept_Info AS MAIN_DEPT ON MAIN_DEPT.REC_ID = SUB_DEPT.SD_Dept_ID  WHERE  REC_STATUS IN (0,1,2) AND (CEN_ID=" & Param.Cen_ID.ToString & ") and USER_ID='" & Param.User_ID.ToUpper & "'"
            Return dbService.List(ConnectOneWS.Tables.CLIENT_USER_INFO, SQL_STR, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetUsersList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR As String = " SELECT USER_ID FROM CLIENT_USER_INFO WHERE  REC_STATUS IN (0,1,2) AND (CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(USER_ROLE_ID)='CLIENT ROLE') "
            Return dbService.List(ConnectOneWS.Tables.CLIENT_USER_INFO, SQL_STR, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets User Info
        ''' </summary>
        ''' <param name="User_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfo</remarks>
        Public Shared Function GetUserInfo(ByVal User_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR As String = " SELECT USER_ID, USER_PWD, USER_ROLE_ID, USER_IS_ADMIN, COALESCE(SUB_DEPT.SD_Is_Central_Store,0) IS_CENTRAL_STORE,PERS.REC_ID AS PersonnelID, COALESCE(MAIN_DEPT.Rec_ID,SUB_DEPT.Rec_ID) as MainDeptID, SUB_DEPT.Rec_ID as SubDeptID   FROM CLIENT_USER_INFO  CU LEFT OUTER JOIN Stock_Personnel_Info AS PERS ON CU.USER_PERSONNEL_ID = PERS.REC_ID LEFT OUTER JOIN Store_Dept_Info AS SUB_DEPT ON PERS.Pers_Dept_ID = SUB_DEPT.REC_ID LEFT OUTER JOIN Store_Dept_Info AS MAIN_DEPT ON MAIN_DEPT.REC_ID = SUB_DEPT.SD_Dept_ID WHERE  REC_STATUS IN (0,1,2) AND USER_ID='" & User_ID.ToUpper & "'"
            Return dbService.List(ConnectOneWS.Tables.CLIENT_USER_INFO, SQL_STR, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Inserts Data
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_AddNew</remarks>
        Public Shared Function AddNew(ByVal InParam As Parameter_AddNew_ClientUserInfo, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim RecID As String = System.Guid.NewGuid().ToString()
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO CLIENT_USER_INFO(CEN_ID,USER_ID,USER_PWD,USER_ROLE_ID," &
                                                    "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                    ") VALUES(" &
                                                    "" & InParam.Cen_Id.ToString() & "," &
                                                    "'" & Convert.ToInt32(InParam.Cen_Id).ToString() & "', " &
                                                    "'" & InParam.Cen_Password.ToUpper.Trim & "', " &
                                                    "'" & "CLIENT ROLE" & "', " &
                                                    "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "')"
            dbService.Insert(ConnectOneWS.Tables.CLIENT_USER_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' Changes Password
        ''' </summary>
        ''' <param name="CPwd"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_ChangePassword</remarks>
        Public Shared Function ChangePassword(ByVal CPwd As Parameter_CPwd_ClientUserInfo, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim CenIDStr As String = " = " & CPwd.Cen_Id & ""
            If CPwd.Cen_Id = 0 Or CPwd.Cen_Id = Nothing Then CenIDStr = " IS NULL"
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE CLIENT_USER_INFO SET " &
                                       " USER_PWD       = '" & CPwd.Cen_Password & "', " &
                                           " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " &
                                            "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," &
                                            "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                            "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " &
                                           " WHERE  CEN_ID " & CenIDStr &
                                           " and UPPER(USER_ID)='" & CPwd.UserID.Trim.ToUpper & "'"
            dbService.Update(ConnectOneWS.Tables.CLIENT_USER_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Gets pwd and RoleId based on UserID
        ''' </summary>
        ''' <param name="UserID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetList</remarks>
        Public Shared Function GetList(ByVal UserID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT USER_PWD ,USER_ROLE_ID  " &
                                  " FROM CLIENT_USER_INFO WHERE   REC_STATUS IN (0,1,2) AND UPPER(USER_ID)='" & UserID.Trim.ToUpper & "'"
            Return dbService.List(ConnectOneWS.Tables.CLIENT_USER_INFO, Query, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets User Pwd and RoleID filtered by CenId and userID
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetListFilteredByCenIdAndUserId</remarks>
        Public Shared Function GetList(ByVal Param As Param_ClientUserInfo_GetListFilteredByCenIDUserID, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CenIDStr As String = " = " & Param.CenID & ""
            If Param.CenID.ToString.Trim.Length = 0 Then CenIDStr = " IS NULL"
            Dim Query As String = " SELECT USER_PWD ,USER_ROLE_ID  " &
                                  " FROM CLIENT_USER_INFO WHERE  REC_STATUS IN (0,1,2) AND  CEN_ID " & CenIDStr & "  and UPPER(USER_ID)='" & Param.UserID.Trim.ToUpper & "'"
            Return dbService.List(ConnectOneWS.Tables.CLIENT_USER_INFO, Query, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets UserId based on CenID
        ''' </summary>
        ''' <param name="CenID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetListByCenID</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param, ByVal CenID As Integer) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT USER_ID as 'User Name',REC_ADD_ON AS 'Create Date',CEN_ID AS ID " &
                                  " from Client_User_Info WHERE REC_STATUS IN (0,1,2) and CEN_ID =" & CenID & " ;"
            Return dbService.List(ConnectOneWS.Tables.CLIENT_USER_INFO, OnlineQuery, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets User Count
        ''' </summary>
        ''' <param name="CenID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserCount</remarks>
        Public Shared Function GetUserCount(ByVal CenID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(*) FROM Client_User_Info WHERE  REC_STATUS IN (0,1,2) AND CEN_ID=" & CenID.ToString & " "
            Return dbService.GetScalar(ConnectOneWS.Tables.CLIENT_USER_INFO, Query, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Max User Id
        ''' </summary>
        ''' <param name="Cen_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetMaxUserID</remarks>
        Public Shared Function GetMaxUserID(ByVal Cen_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT max(COALESCE(USER_ID,'')) FROM Client_User_Info WHERE  REC_STATUS IN (0,1,2) AND CEN_ID=" & Cen_ID.ToString & " "
            Return dbService.GetScalar(ConnectOneWS.Tables.CLIENT_USER_INFO, OnlineQuery, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets All user notifications from current user 
        ''' </summary>
        ''' <param name="Cen_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetNotifications(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select ID, CEN_NAME AS CENTRE, CEN_UID AS UID, CN_CALLER_ID AS AUDITOR,CAST(CONVERT(DATETIME, CN_ADD_ON ,120) AS VARCHAR) AS INITIATED from so_Centre_Notifications as CN INNER JOIN centre_info AS CI  ON CN.CN_CEN_ID = CI.CEN_ID  WHERE CN_CALLER_ID= CASE WHEN (SELECT USER_ROLE_ID FROM CLIENT_USER_INFO WHERE USER_ID ='" & inBasicParam.openUserID & "') = 'SUPERUSER' THEN CN_CALLER_ID ELSE '" & inBasicParam.openUserID & "' END  ORDER BY CN_ADD_ON"
            Return dbService.List(ConnectOneWS.Tables.SO_CENTRE_NOTIFICATIONS, OnlineQuery, ConnectOneWS.Tables.SO_CENTRE_NOTIFICATIONS.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Add new Notification 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function AddNewNotification(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select count(*) from so_Centre_Notifications where CN_CEN_ID = " & inBasicParam.openCenID.ToString & " and CN_CALLER_ID = '" & inBasicParam.openUserID & "'"
            If dbService.GetScalar(ConnectOneWS.Tables.SO_CENTRE_NOTIFICATIONS, OnlineQuery, ConnectOneWS.Tables.SO_CENTRE_NOTIFICATIONS.ToString(), inBasicParam) > 0 Then
                Return False
            End If
            OnlineQuery = "INSERT INTO so_Centre_Notifications(CN_CEN_ID ,CN_CALLER_ID ,CN_ADD_ON ,CN_MESSAGE " &
                                                    ") VALUES(" &
                                                    "" & inBasicParam.openCenID.ToString & "," &
                                                    "'" & inBasicParam.openUserID & "', " &
                                                    "'" & Common.DateTimePlaceHolder & "', " &
                                                    "'" & "" & "') "
            dbService.Insert(ConnectOneWS.Tables.SO_CENTRE_NOTIFICATIONS, OnlineQuery, inBasicParam, "")
            Return True
        End Function

        Public Shared Function GetControlVisibility(screen_name As Common_Lib.RealTimeService.ClientScreen, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = " "
            Dim params() As String = {}
            Dim values() As Object = {}
            Dim dbTypes() As System.Data.DbType = {}
            Dim lengths() As Integer = {}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetUserNameCount(ByVal inparam As Param_GetUserNameCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(*) FROM Client_User_Info WHERE  REC_STATUS IN (0,1,2) AND CEN_ID=" & inparam.CenID.ToString & " AND USER_ID='" & inparam.User_Name.ToString & "' AND REC_ID <> '" & inparam.UserRecID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.CLIENT_USER_INFO, Query, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_ClientUser_EntriesCount(UserName As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_ClientUser_EntriesCount"
            Dim params() As String = {"@UserID"}
            Dim values() As Object = {UserName}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {255}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.CLIENT_USER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUserRegister(CenterUserOnly As Boolean, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_ClientUser_Register"
            Dim params() As String = {"CENID", "@CENTER_USER_ONLY"}
            Dim values() As Object = {inBasicParam.openCenID, CenterUserOnly}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Boolean}
            Dim lengths() As Integer = {4, 1}
            Return dbService.ListFromSP(ConnectOneWS.Tables.CLIENT_USER_INFO, SPName, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetGroupRegister(inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_ClientUserGroup_Register"
            Dim params() As String = {"CENID"}
            Dim values() As Object = {inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.CLIENT_GROUP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPrivilegeRegister(inparam As Param_GetPrivilegeRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_CU_Privileges_Register"
            Dim params() As String = {"@groupRecID", "@UserID", "@CENID"}
            Dim values() As Object = {inparam.GroupID, inparam.UserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.CLIENT_USER_TASK, SPName, ConnectOneWS.Tables.CLIENT_USER_TASK.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetGroupNameCount(ByVal inparam As Param_GetGroupNameCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COALESCE((Select 1 Grp_count from Client_Group_Info Where Group_Name = '" + inparam.Group_Name + "' and (REC_ID <> " + inparam.GroupID.ToString() + ") And Group_Cen_ID = " + inBasicParam.openCenID.ToString() + "),0) Grp_Count"
            Return dbService.GetScalar(ConnectOneWS.Tables.CLIENT_GROUP_INFO, Query, ConnectOneWS.Tables.CLIENT_GROUP_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetScreens(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select CT_ID TaskID,CT_Name TaskName, CT_Display_Name as TaskDisplayName from so_client_task_info a inner join so_client_task_group_info b on a.CTG_ID=b.CTG_ID where b.CTG_NAME!='Non-Screen' order by a.CT_DISPLAY_NAME"
            Return dbService.List(ConnectOneWS.Tables.SO_CLIENT_TASK_INFO, Query, ConnectOneWS.Tables.SO_CLIENT_TASK_INFO.ToString(), inBasicParam)
            'Dim ScreenModule As String
            'If inBasicParam.screen.ToString().StartsWith("Stock") Then
            '    ScreenModule = ""
            'End If

            'Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim SPName As String = "Get_Module_Screens"
            'Dim params() As String = {"@Module"}
            'Dim values() As Object = {inBasicParam.screen.ToString()}
            'Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            'Dim lengths() As Integer = {50}
            'Return dbService.ListFromSP(ConnectOneWS.Tables.SO_CLIENT_TASK_INFO, SPName, ConnectOneWS.Tables.SO_CLIENT_TASK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)

        End Function
        Public Shared Function GetPrivileges(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select CPL_CODE Code,CPL_VALUE Value from so_client_permission_list"
            Return dbService.List(ConnectOneWS.Tables.SO_CLIENT_PERMISSION_LIST, Query, ConnectOneWS.Tables.SO_CLIENT_PERMISSION_LIST.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUserDetails(UserRecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_User_Details"
            Dim params() As String = {"@UserID"}
            Dim values() As Object = {UserRecID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.CLIENT_USER_INFO, SPName, ConnectOneWS.Tables.CLIENT_USER_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetGroupDetails(GroupRecID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select * from Client_Group_Info Where Rec_ID = " + GroupRecID.ToString()
            Return dbService.List(ConnectOneWS.Tables.CLIENT_GROUP_INFO, Query, ConnectOneWS.Tables.CLIENT_GROUP_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function InsertClientUser(ByVal InParam As Param_InsertClientUser, inBasicParam As ConnectOneWS.Basic_Param) As String
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim RandomPassword As String = Password.GeneratePassword(15, Password.Password_Type.Only_Aplha_Numeric)
            Dim SPName As String = "Insert_ClientUser"
            Dim params() As String = {"@CEN_ID", "@USER_Name", "@USER_PWD", "@LoggedInUserID", "@USER_PERSONNEL_ID", "@USER_IS_ADMIN", "@SELFPOSTEDONLY"}
            Dim values() As Object = {inBasicParam.openCenID, InParam.USER_Name, InParam.User_Password, inBasicParam.openUserID, InParam.USER_PERSONNEL_ID, InParam.USER_IS_ADMIN, InParam.SelfPostedOnly}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.Boolean}
            Dim lengths() As Integer = {4, 255, 255, 255, 4, 2, 2}
            Dim ClientUserID As String = dbService.ScalarFromSP(ConnectOneWS.Tables.CLIENT_USER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Delete Existing Group Mapping
            DeleteClientUserGroupMapping(ClientUserID, inBasicParam)
            'Add new Group Mapping, as mentioned by user
            If Not InParam.Mapped_Group_IDs Is Nothing Then
                For Each GroupID As Int32 In InParam.Mapped_Group_IDs
                    InsertClientUserGroupMapping(ClientUserID, GroupID, inBasicParam)
                Next
            End If
            Return True
        End Function
        Public Shared Function InsertClientUserGroup(ByVal InParam As Param_InsertClientUserGroup, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_ClientUser_Group"
            Dim params() As String = {"@Group_Cen_ID", "@Group_Name", "@Group_Remarks", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, InParam.Group_Name, InParam.Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 8000, 255}
            Dim ClientUserGroupID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.CLIENT_GROUP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return ClientUserGroupID
        End Function
        Private Shared Function InsertClientUserGroupMapping(ByVal UserRecID As String, ByVal GroupRecID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_ClientUserGroup_Mapping"
            Dim params() As String = {"@CUG_CU_ID", "@CUG_CG_ID"}
            Dim values() As Object = {UserRecID, GroupRecID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.CLIENT_USER_GROUP, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertClientUserPrivileges(ByVal inparam As Param_InsertClientUserPrivileges, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_ClientUser_Privileges"
            Dim params() As String = {"@CUT_CEN_ID", "@UserName", "@TaskID", "@PERMISSION_CODE", "@GroupID", "@LoggedInUser"}
            Dim values() As Object = {inBasicParam.openCenID, inparam.UserName, inparam.TaskID, inparam.Privilege_Code, inparam.GroupID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 4, 15, 4, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.SO_CLIENT_USER_TASK, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdateClientUser(ByVal InParam As Param_UpdateClientUser, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_ClientUser"
            Dim params() As String = {"@USER_Name", "@LoggedInUserID", "@USER_PERSONNEL_ID", "@USER_IS_ADMIN", "@SELFPOSTEDONLY", "@USER_ID"}
            Dim values() As Object = {InParam.USER_Name, inBasicParam.openUserID, InParam.USER_PERSONNEL_ID, InParam.USER_IS_ADMIN, InParam.SelfPostedOnly, InParam.USER_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {255, 255, 4, 2, 2, 36}
            dbService.UpdateBySP(ConnectOneWS.Tables.CLIENT_USER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            'Delete Existing Group Mapping
            DeleteClientUserGroupMapping(InParam.USER_ID, inBasicParam)
            'Add new Group Mapping, as mentioned by user
            If Not InParam.Mapped_Group_IDs Is Nothing Then
                For Each GroupID As Int32 In InParam.Mapped_Group_IDs
                    InsertClientUserGroupMapping(InParam.USER_ID, GroupID, inBasicParam)
                Next
            End If
            Return True
        End Function
        Public Shared Function UpdateClientUserGroup(ByVal InParam As Param_UpdateClientUserGroup, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_ClientUser_Group"
            Dim params() As String = {"@Group_Name", "@Group_Remarks", "@UserID", "@Group_ID"}
            Dim values() As Object = {InParam.Group_Name, InParam.Remarks, inBasicParam.openUserID, InParam.Group_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 8000, 255, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.CLIENT_GROUP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdateClientUserPrivileges(ByVal inparam As Param_UpdateClientUserPrivileges, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_ClientUser_Privileges"
            Dim params() As String = {"@REC_ID", "@UserName", "@TaskID", "@PERMISSION_CODE", "@GroupID", "@LoggedInUser"}
            Dim values() As Object = {inparam.Rec_ID, inparam.UserName, inparam.TaskID, inparam.Privilege_Code, inparam.GroupID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 4, 15, 4, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.SO_CLIENT_USER_TASK, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function SaveClientUserGroupMapping(ByVal inparam As Param_SaveClientUserGroupMapping, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            'Remove Provious mappings for Group
            DeleteClientUserGroupMapping(inparam.GroupID, inBasicParam)
            'Add mappings for mentioned users 
            For Each UserID As String In inparam.MappedUserIDs
                InsertClientUserGroupMapping(UserID, inparam.GroupID, inBasicParam)
            Next
            Return True
        End Function
        Private Shared Function DeleteClientUserGroupMapping(ByVal UserRecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.CLIENT_USER_GROUP, "CUG_CU_ID = '" + UserRecID + "'", inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteClientUserGroupMapping(ByVal GroupID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.CLIENT_USER_GROUP, "CUG_CG_ID = " + GroupID.ToString() + "", inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class CodInfo
        <Serializable>
        Public Class Param_CodInfo_AddNew
            Public Cen_Id As String = ""
            Public YearID As String = ""
            Public Year As String = ""
            Public YearStartDate As Date
            Public YearEndDate As Date
        End Class

        ''' <summary>
        ''' Inserts COD Info
        ''' </summary>
        ''' <param name="Cen_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_CodInfo_AddNew</remarks>
        Public Shared Function AddNew(ByVal InParam As Param_CodInfo_AddNew, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim RecID As String = Guid.NewGuid.ToString
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO COD_INFO(CEN_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT,COD_ACTIVE," &
                                                        "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,COD_CEN_ACC_TYPE_ID" &
                                                        ") VALUES(" &
                                                        "" & InParam.Cen_Id.ToString & ", " &
                                                        "" & InParam.YearID.ToString & ", " &
                                                        "'" & InParam.Year & "', " &
                                                        "'" & InParam.YearStartDate.ToString(Common.Server_Date_Format_Long) & "', " &
                                                        "'" & InParam.YearEndDate.ToString(Common.Server_Date_Format_Long) & "',1, " &
                                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ",'4af2b833-974f-4de5-9dbe-922a3def1eea')"
            dbService.Insert(ConnectOneWS.Tables.COD_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        ''' <summary>
        ''' Get Created Centres
        ''' </summary>
        ''' <param name="SelectedCentre"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_CodInfo_GetCreatedCentersFromSelected</remarks>
        Public Shared Function GetCreatedCentersFromSelected(ByVal SelectedCentre As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " select CEN_ID from COD_INFO WHERE REC_STATUS IN (0,1,2) AND CEN_ID IN (" & SelectedCentre & ")"
            Return dbService.List(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetYearCount(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            '=====================================
            'DO NOT USE YEAR ID IN THIS FUNCTION
            '=====================================

            Dim Query As String = "SELECT COUNT(COD_YEAR_ID) FROM COD_INFO WHERE REC_STATUS IN (0,1,2) AND CEN_ID =" & inBasicParam.openCenID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetCompletedYearCount(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(COD_YEAR_ID) FROM COD_INFO WHERE REC_STATUS IN (0,1,2) AND CEN_ID =" & inBasicParam.openCenID.ToString & " AND COD_YEAR_ID NOT IN (SELECT DISTINCT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = COD_INFO.CEN_ID) AND COD_YEAR_ID < " & inBasicParam.openYearID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetFinancialYearList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT Type.ACC_TYPE , COD.COD_YEAR_NAME as 'Financial Year', COD.COD_YEAR_SDT as 'From',COD.COD_YEAR_EDT AS 'To',CASE WHEN COD.COD_ACTIVE=1 THEN 'No' else 'Yes' END as 'Lock',COD.COD_YEAR_ID  FROM  cod_info as COD inner join centre_acc_type_info as Type on COD.COD_CEN_ACC_TYPE_ID = Type.REC_ID INNER JOIN client_user_info AS CU ON CU.USER_ID = '" & inBasicParam.openUserID & "' AND COALESCE(CU.CEN_ID,COD.CEN_ID) = COD.CEN_ID where COD.REC_STATUS IN (0,1,2) AND COD.CEN_ID =" & inBasicParam.openCenID.ToString & " AND COD.COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " AND COD.COD_YEAR_ID BETWEEN COALESCE(CU.USER_FROM_YEAR_ID,COD.COD_YEAR_ID) AND COALESCE(CU.USER_TO_YEAR_ID,COD.COD_YEAR_ID) order by COD.COD_YEAR_ID desc "
            Return dbService.List(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetUnAuditedFinancialYearList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT COD_YEAR_SDT as 'From',COD_YEAR_EDT AS 'To',COD_YEAR_ID  FROM  cod_info  where REC_STATUS IN (0,1,2) AND CEN_ID =" & inBasicParam.openCenID.ToString & " AND COD_AUDITED = 0 AND COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " AND((COD_YEAR_ID > " & inBasicParam.openYearID.ToString & " AND " & inBasicParam.openYearID.ToString & " IN (SELECT DISTINCT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0 AND CEN_ID =" & inBasicParam.openCenID.ToString & ")) OR COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & ") AND (COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = " & inBasicParam.openCenID.ToString & ")) "
            If inBasicParam.openCenID = "3054" Or inBasicParam.openCenID = "3045" Then Query += "union SELECT COD_YEAR_SDT as 'From',COD_YEAR_EDT AS 'To',COD_YEAR_ID  FROM  cod_info where CEN_ID =" & inBasicParam.openCenID.ToString & " and COD_AUDITED = 0 AND COD_YEAR_ID = 1819 "
            Query += " order by COD_YEAR_ID desc "
            Return dbService.List(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetUnAuditedFinancialYearOfTransferorCentre(CenId As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Trial Balance Imported years are being considered in this Query , as asset tfs are acceptable in such years/centers. Please dont use this Query for any other purposes.
            Dim Query As String = " SELECT COD_YEAR_SDT as 'From',COD_YEAR_EDT AS 'To',COD_YEAR_ID  FROM  cod_info  where REC_STATUS IN (0,1,2) AND CEN_ID =" & CenId & " AND COD_AUDITED = 0 AND COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " AND((COD_YEAR_ID > " & inBasicParam.openYearID.ToString & " AND " & inBasicParam.openYearID.ToString & " IN (SELECT DISTINCT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0 AND CEN_ID =" & inBasicParam.openCenID.ToString & ")) OR COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & ") order by COD_YEAR_ID desc " ' AND (COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = '" & inBasicParam.openCenID & "')) 
            Return dbService.List(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Base_GetCen_Ins_List(ByVal Main_Cen_PAD_No As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select DISTINCT  INS_SHORT ,cen.CEN_NAME AS CEN_NAME_X ,INS_NAME,INS_ID,COALESCE(CEN_UID,'') AS CEN_UID,CEN_PAD_NO,Type.ACC_TYPE AS CEN_ACC_TYPE_ID,ci.CEN_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, cen.REC_ID as CEN_REC_ID, CASE WHEN CI.CEN_ID IN (4216,4218) AND '" & inBasicParam.openUserID & "' IN ('bkgajendra', 'bkgokul','bkmili','bksaurabh') THEN 1 ELSE 0 END As Is_volume,cen.CEN_CANCELLATION_DATE FROM COD_INFO AS ci INNER JOIN centre_info AS cen ON ci.CEN_ID = cen.CEN_ID INNER JOIN institute_info AS Ins ON cen.cen_ins_id = ins.INS_ID inner join centre_acc_type_info as Type on ci.COD_CEN_ACC_TYPE_ID = Type.REC_ID WHERE(ci.REC_STATUS  IN (0,1,2)) AND COD_ACTIVE=1 AND CEN_BK_PAD_NO='" + Main_Cen_PAD_No + "' ORDER BY INS_ID,CEN_UID"
            Return dbService.List(ConnectOneWS.Tables.COD_INFO, Query, "COD_INFO", inBasicParam)
        End Function

        Public Shared Function Base_GetSelectCentreList(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal BKCertNo As String = "") As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT CEN_NAME as 'Centre Name',CEN_PAD_NO as 'Certificate No',CEN_ID  as ID,COALESCE(CEN_ZONE_ID,'') AS XCEN_ZONE_ID,COALESCE(CEN_ZONE_SUB_ID,'') AS XCEN_ZONE_SUB_ID  " &
                                                    " FROM CENTRE_INFO WHERE  REC_STATUS  IN (0,1,2) AND CEN_INS_ID = '00001' AND CEN_MAIN=1 AND " &
                                                    " CEN_PAD_NO IN ('" & BKCertNo & "') ORDER BY CEN_NAME "
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, Query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function IsYearOpenForCenterCreation(YearId As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT COUNT(CEN_ID) FROM COD_INFO WHERE COD_AUDITED = 1 AND COD_YEAR_ID = (select distinct COD_YEAR_ID from cod_info where COD_YEAR_NAME = '" & YearId & "')"
            If dbService.GetScalar(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam) > 0 Then Return False
            Return True
        End Function

        Public Shared Function GetReportsToBePrintedInfo(YearId As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT CASE WHEN cod_ReportsToBePrinted = 1 THEN 'Audited' ELSE '' END As cod_ReportsToBePrinted FROM COD_INFO WHERE COD_YEAR_ID = " & YearId.ToString & " AND  REC_STATUS IN (0,1,2) AND CEN_ID =" & inBasicParam.openCenID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.COD_INFO, Query, ConnectOneWS.Tables.COD_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function CheckVolumeCenter(inBasicParam As ConnectOneWS.Basic_Param) As Object
            If inBasicParam.openCenID = 4216 Or inBasicParam.openCenID = 4218 Then
                '  If inBasicParam.openUserID.ToLower = "bkgajendra" Or inBasicParam.openUserID.ToLower = "bkgokul" Or inBasicParam.openUserID.ToLower = "bkmili" Or inBasicParam.openUserID.ToLower = "bksaurabh" Then
                Return True
                ' End If
            End If
            Return False
        End Function
    End Class
    <Serializable>
    Public Class Center_Purpose_Info
        <Serializable>
        Public Class Parameter_Insert_Center_Purpose
            Public PurposeName As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Center_Purpose
            Public PurposeName As String
            Public Rec_ID As String
        End Class

        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " select CPR_NAME as Purpose, CPR_ACTIVE as Active,REC_ADD_ON as 'Add On',REC_ID AS ID," & Common.Rec_Detail("center_purpose_info") & " from center_purpose_info WHERE REC_STATUS IN (0,1,2) AND CPR_CEN_ID IN (" & inBasicParam.openCenID.ToString & ") ORDER BY CPR_NAME"
            Return dbService.List(ConnectOneWS.Tables.CENTER_PURPOSE_INFO, Query, ConnectOneWS.Tables.CENTER_PURPOSE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function CheckUsageCount(Purpose_id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT COUNT(*) from TRANSACTION_D_PURPOSE_INFO WHERE REC_STATUS IN (0,1,2) AND TR_PURPOSE_MISC_ID IN ('" & Purpose_id & "')"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Center_Purpose, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO center_purpose_info(CPR_CEN_ID,CPR_NAME,CPR_ACTIVE," & _
                                                         "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                         ") VALUES(" & _
                                                         "" & inBasicParam.openCenID.ToString & "," & _
                                                         "'" & InParam.PurposeName & "', " & _
                                                         "1, " & _
                                                         "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', 1, '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.CENTER_PURPOSE_INFO, OnlineQuery, inBasicParam, RecID)
            Return True
        End Function

        Public Shared Function Update(ByVal inParam As Parameter_Update_Center_Purpose, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE center_purpose_info SET " & _
                            "CPR_NAME             ='" & inParam.PurposeName & "', " & _
                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                            "  WHERE REC_ID    ='" & inParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.CENTER_PURPOSE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Activate(ByVal Rec_id As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE center_purpose_info SET " & _
                            "CPR_ACTIVE             =1, " & _
                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                            "  WHERE REC_ID    ='" & Rec_id & "'"
            dbService.Update(ConnectOneWS.Tables.CENTER_PURPOSE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function DeActivate(ByVal Rec_id As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE center_purpose_info SET " & _
                            "CPR_ACTIVE             =0, " & _
                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                            "  WHERE REC_ID    ='" & Rec_id & "'"
            dbService.Update(ConnectOneWS.Tables.CENTER_PURPOSE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

    End Class
#End Region
End Namespace
