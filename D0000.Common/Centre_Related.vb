'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
#Region "Profile"
    <Serializable>
    Public Class Core
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Get Centre Details, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCenterDetails() As DataTable
            Return GetCenterDetailsForCenID(ClientScreen.Profile_Core)
        End Function

        ''' <summary>
        ''' Get Centre Details By BKPAD, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCenterDetailsByBKPAD() As DataTable
            Return GetCentersByBKPAD(ClientScreen.Profile_Core)
        End Function

        'No Need To Shift
        Public Function GetCenSupportInfo() As DataTable
            Return GetCenSupportInfo(ClientScreen.Core_Add_AssetLocation)
        End Function

        ''' <summary>
        ''' Gets Centre Support Info, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_GetCenSupportInfo</remarks>
        Public Function GetCenSupportInfo(ByVal Screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Core_GetCenSupportInfo, Screen)
        End Function

        'Shifted
        Public Function GetInstitutes() As DataTable
            Return GetInstituteList(ClientScreen.Profile_Core)
        End Function

        'No need to Shift
        Public Function GetLocations() As DataTable
            Dim _al As AssetLocations = New AssetLocations(cBase)
            Return _al.GetFullList(ClientScreen.Profile_Core)
        End Function


        ''' <summary>
        ''' Gets Support Row Count, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_GetSupportRowCount</remarks>
        Public Function GetSupportRowCount() As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_Core_GetSupportRowCount, ClientScreen.Profile_Core)
        End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_GetList</remarks>
        Public Function GetList() As DataTable
            Dim Query As String = "SELECT CURR_AB.C_NAME, CURR_AB.REC_ID AS CEN_ACC_RES_PERSON_AB_ID , CEN_OPEN_DATE, CEN_COMMUNICATION_OPTION " &
                                  ", CS.REC_ADD_BY, CS.REC_ADD_ON,CS.REC_EDIT_BY,CS.REC_EDIT_ON,CS.REC_ID,CS.REC_STATUS,CS.REC_STATUS_BY,CS.REC_STATUS_ON  " &
                                  " from Centre_Support_Info  CS  " &
                                  " INNER Join address_book AS AB ON CEN_ACC_RES_PERSON_AB_ID = AB.REC_ID  " &
                                  " INNER JOIN address_book AS CURR_AB ON AB.C_ORG_REC_ID = CURR_AB.C_ORG_REC_ID AND CURR_AB.C_COD_YEAR_ID = " + cBase._open_Year_ID.ToString() &
                                  " WHERE CS.REC_STATUS In (0, 1, 2) And CEN_ID = " + cBase._open_Cen_ID.ToString()
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Core_GetList, ClientScreen.Profile_Core)
        End Function

        'Shifted
        Public Function GetPartyList(Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _address As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            Param.Party_Rec_ID = Party_Rec_ID
            Return _address.GetList(ClientScreen.Profile_Core, Param)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_Insert</remarks>
        Public Function Insert(ByVal RecID As String) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_Core_Insert, ClientScreen.Profile_Core, RecID)
        End Function

        ''' <summary>
        ''' Updates CentreInfo, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Core_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Core) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CentreRelated_Core_Update, ClientScreen.Profile_Core, UpParam)
        End Function

    End Class
#End Region


#Region "Start Menu"
    <Serializable>
    Public Class Center
        Inherits SharedVariables
        <Serializable>
        Public Class Return_Get_Contact_Info
            Public Property Details As String
            Public Property Name As String
            Public Property Contact1 As String
            Public Property Contact2 As String
            Public Property Contact3 As String
            Public Property Contact4 As String
        End Class
        <Serializable>
        Public Class Return_Get_Zone
            Public Property Zone_Name As String
        End Class
        <Serializable>
        Public Class Return_GetInstUIDList
            Public UID As String
            Public Center As String
            Public Zone As String
            Public SubZone As String
            Public AccType As String
            Public CEN_ID As Int32
            Public State As String
        End Class
        <Serializable>
        Public Class Return_GetSubZoneList
            Public Name As String
            Public ShortName As String
            Public Zone As String
        End Class

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetCentreMaxOpenYear(ByVal cenid As String) As Int32
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "SELECT MAX(COD_YEAR_ID) FROM cod_info 
                                    WHERE CEN_ID=" & cenid & " AND COD_AUDITED=0"
            Dim YearID As DataTable = _RealService.List(Tables.COD_INFO, query, Tables.COD_INFO.ToString(), GetBaseParams(ClientScreen.Facility_ServiceReport))
            If YearID.Rows.Count > 0 Then
                If Convert.IsDBNull(YearID.Rows(0)(0)) = False Then
                    Return Convert.ToInt32(YearID.Rows(0)(0))
                End If
            End If
            Return 0
        End Function
        ''' <summary>
        ''' Select Center Page : For Local Mode Only 
        ''' </summary>
        ''' <param name="BKCertNo"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetSelectCentreList(Optional ByVal BKCertNo As String = "") As DataTable
            Return Base_GetSelectCentreList(BKCertNo)
        End Function

        ''' <summary>
        ''' Select Center Page : Gets Child Center List, Shifted 
        ''' </summary>
        ''' <param name="Main_Cen_PAD_No"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetChildCenterList</remarks>
        Public Function GetChildCenterList(ByVal Main_Cen_PAD_No As String) As DataTable
            Dim SQL_STR As String = " select CEN_ID from CENTRE_INFO WHERE REC_STATUS IN (0,1,2) and CEN_BK_PAD_NO='" & Main_Cen_PAD_No & " ';"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetChildCenterList, SQL_STR, ClientScreen.Start_Select_Center, RealTimeService.Tables.CENTRE_INFO, Main_Cen_PAD_No)
        End Function

        'Select Center Page :Gets Ins List for a Center , no need to shift
        Public Function GetCen_Ins_List(ByVal Main_Cen_PAD_No As String) As Object
            Return Base_GetCen_Ins_List(Main_Cen_PAD_No)
        End Function

        'Select Center Page :Gets Ins List for a Auditor, no need to shift
        Public Function GetIns_List(ByVal Screen As ClientScreen) As DataTable
            Return GetInstituteList(Screen)
        End Function

        ''' <summary>
        ''' Gets Centre list by Auditor filtered by InsttId, Shifted
        ''' </summary>
        ''' <param name="UserID"></param>
        ''' <param name="InsttID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterListByAuditor_Instt</remarks>
        Public Function GetCenterListByAuditor_Instt(ByVal UserID As String, ByVal InsttID As String) As DataTable
            'Dim Query As String = ""
            'Query = " SELECT CI.CEN_UID,CI.cen_pad_no AS CEN_PAD_NO, CI_MAIN.CEN_NAME, CI.CEN_ID,CI.REC_ID, CI_MAIN.CEN_ZONE_ID,CI_MAIN.CEN_ZONE_SUB_ID,CASE WHEN CI.CEN_ACC_TYPE_ID IS NULL THEN '' ELSE CI.CEN_ACC_TYPE_ID END AS CEN_ACC_TYPE_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, CI_MAIN.CEN_ID AS CEN_ID_MAIN, CI_MAIN.cen_pad_no AS CEN_PAD_NO_MAIN " & _
            '                      " FROM centre_info AS CI INNER JOIN so_client_user_task AS CUT ON CUT.CUT_CEN_ID = CI.CEN_ID INNER JOIN centre_info AS CI_MAIN ON ci.cen_bk_pad_no = CI_MAIN.cen_pad_no AND CI_MAIN.CEN_MAIN = 1" & _
            '                      " INNER JOIN COD_INFO AS cod ON cod.CEN_ID = ci.CEN_ID AND COD_ACTIVE=1 WHERE CUT_CU_ID = '" & UserID & "' AND  CI.REC_STATUS IN (0,1,2) AND CI.CEN_INS_ID = '" & InsttID & "' order by cen_name"

            Dim Param As Param_Center_GetCenterListByAuditor_Instt = New Param_Center_GetCenterListByAuditor_Instt()
            Param.InsttID = InsttID
            Param.UserID = UserID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetCenterListByAuditor_Instt, ClientScreen.Start_Auditor_Login, Param)
        End Function

        ''' <summary>
        ''' Gets Centre List by PAD, Shifted
        ''' </summary>
        ''' <param name="PAD_NAME"></param>
        ''' <param name="InsttID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterListByPAD_Name</remarks>
        Public Function GetCenterListByPAD_Name(ByVal PAD_NAME As String, ByVal InsttID As String) As DataTable
            Dim Param As Param_Center_GetCenterListByPAD_Name = New Param_Center_GetCenterListByPAD_Name()
            Param.InsttID = InsttID
            Param.PAD_NAME = PAD_NAME
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetCenterListByPAD_Name, ClientScreen.Start_Auditor_Login, Param)
        End Function

        ''' <summary>
        ''' Gets Centre List by PAD, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterListByPAD_Name</remarks>
        Public Function GetCenterListByCenID(ByVal CenID As Integer, YearId As Integer) As DataTable
            Dim param As Param_Center_GetCenterListByCenID = New Param_Center_GetCenterListByCenID
            param.CenID = CenID
            param.YearID = YearId
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetCenterListByCenID, ClientScreen.Start_Auditor_Login, param)
        End Function
        Public Function GetCenter_ByInstitute(ByVal InsttID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inBasicParam = GetBaseParams(ClientScreen.Start_Select_Center)
            Dim Query As String = "Select DISTINCT  ins.INS_ID,INS_SHORT ,cen.CEN_NAME AS CEN_NAME,COALESCE(cen.CEN_UID,'') AS CEN_UID,cen.CEN_PAD_NO,Type.ACC_TYPE AS CEN_ACC_TYPE_ID,ci.CEN_ID,cen.CEN_CANCELLATION_DATE FROM COD_INFO AS ci INNER JOIN centre_info AS cen ON ci.CEN_ID = cen.CEN_ID INNER JOIN institute_info AS Ins ON cen.cen_ins_id = ins.INS_ID inner join centre_acc_type_info as Type on ci.COD_CEN_ACC_TYPE_ID = Type.REC_ID WHERE(ci.REC_STATUS  IN (0,1,2)) AND COD_ACTIVE=1"
            If (String.IsNullOrWhiteSpace(InsttID) = False) Then
                Query = Query & " AND ins.INS_ID='" & InsttID & "'"
            End If
            Query = Query & " ORDER BY INS_SHORT,CEN_UID "
            Return _RealService.List(Tables.COD_INFO, Query, "COD_INFO", inBasicParam)
        End Function
        Public Function GetCenterDetails_Login(ByVal cenid As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inBasicParam = GetBaseParams(ClientScreen.Start_Select_Center)
            Dim Query As String = "Select DISTINCT  INS_SHORT ,cen.CEN_NAME AS CEN_NAME ,INS_NAME,INS_ID,COALESCE(cen.CEN_UID,'') AS CEN_UID,cen.CEN_PAD_NO,Type.ACC_TYPE AS CEN_ACC_TYPE_ID,ci.CEN_ID,COD_YEAR_ID,COD_YEAR_NAME,COD_YEAR_SDT,COD_YEAR_EDT, cen.REC_ID as CEN_REC_ID, CASE WHEN CI.CEN_ID IN (4216,4218) AND '" & inBasicParam.openUserID & "' IN ('bkgajendra', 'bkgokul','bkmili','bksaurabh') THEN 1 ELSE 0 END As Is_volume,cen.CEN_CANCELLATION_DATE,COALESCE(MAIN.CEN_ZONE_ID,'') AS MAIN_CEN_ZONE_ID,COALESCE(MAIN.CEN_ZONE_SUB_ID,'') AS MAIN_CEN_ZONE_SUB_ID,main.cen_name as 'Main_cen_name',main.cen_pad_no as 'main_cen_pad_no',main.cen_id as 'main_cen_id' FROM COD_INFO AS ci INNER JOIN centre_info AS cen ON ci.CEN_ID = cen.CEN_ID INNER JOIN institute_info AS Ins ON cen.cen_ins_id = ins.INS_ID inner join centre_acc_type_info as Type on ci.COD_CEN_ACC_TYPE_ID = Type.REC_ID INNER JOIN centre_info AS MAIN ON cen.cen_bk_pad_no = MAIN.cen_pad_no AND MAIN.CEN_MAIN = 1 WHERE(ci.REC_STATUS  IN (0,1,2)) AND COD_ACTIVE=1"
            If (String.IsNullOrWhiteSpace(cenid) = False) Then
                Query = Query & " AND cen.cen_id=" & cenid & ""
            End If
            Query = Query & " ORDER BY INS_ID,CEN_UID "
            Return _RealService.List(Tables.COD_INFO, Query, "COD_INFO", inBasicParam)
        End Function
        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Core, RealTimeService.Tables.CENTRE_INFO, Common.ClientDBFolderCode.CORE)
        End Function

        'Main Page : Returns No. of Centers created on client side, no need to shift
        Public Function Get_Client_Cen_Creation_Count() As Object
            Dim _cod As CodInfo = New CodInfo(cBase)
            Return _cod.Get_Cen_Creation_Count_OnClient()
        End Function

        'CreateCenter Page : Gets Center Details for PAD no Typed, Shifted
        ''' <summary>
        ''' Gets Center Details for PAD no Typed, Shifted
        ''' </summary>
        ''' <param name="PAD"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterDetailsByCertNo</remarks>
        Public Function GetCenterDetailsByCertNo(ByVal PAD As String) As DataTable
            Dim LocalQuery As String = " SELECT CEN_NAME AS [CEN_NAME],  " &
                                                  "        IIF(ISNULL(CEN_CON_SCANCODE),'',CEN_CON_SCANCODE) AS CEN_CON_SCANCODE, " &
                                                  "        IIF(ISNULL(CEN_UID),'',CEN_UID) AS UID ," &
                                                  "        IIF(ISNULL(CEN_REG_NO),'',CEN_REG_NO) AS REG_NO ," &
                                                  "        IIF(ISNULL(CEN_B_NAME),'',CEN_B_NAME) AS CEN_B_NAME, " &
                                                  "        IIF(ISNULL(CEN_ADD1),'',CEN_ADD1) AS CEN_ADD1, " &
                                                  "        IIF(ISNULL(CEN_ADD2),'',CEN_ADD2) AS CEN_ADD2, " &
                                                  "        IIF(ISNULL(CEN_ADD3),'',CEN_ADD3) AS CEN_ADD3, " &
                                                  "        IIF(ISNULL(CEN_ADD4),'',CEN_ADD4) AS CEN_ADD4, " &
                                                  "        IIF(ISNULL(CEN_CITY),'',CEN_CITY) AS CEN_CITY, " &
                                                  "        IIF(ISNULL(CEN_STATE),'',CEN_STATE) AS CEN_STATE, " &
                                                  "        IIF(ISNULL(CEN_COUNTRY),'',CEN_COUNTRY) AS CEN_COUNTRY, " &
                                                  "        IIF(ISNULL(CEN_INCHARGE),'',CEN_INCHARGE) AS CEN_INCHARGE   " &
                                                  " FROM CENTRE_INFO WHERE  REC_STATUS IN (0,1,2) AND CEN_INS_ID='00001' AND ucase(CEN_PAD_NO)='" & PAD.Trim.ToUpper & "' AND CEN_MAIN=TRUE "

            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetCenterDetailsByCertNo, LocalQuery, ClientScreen.Start_Create_Center, RealTimeService.Tables.CENTRE_INFO, PAD)
        End Function

        'Create Center Page : Gets List of all institutions' centers present for a BK PAD no 
        ''' <summary>
        ''' Gets List of all institutions' centers present for a BK PAD no, Shifted
        ''' </summary>
        ''' <param name="PAD"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetCenterListByBKCertNo</remarks>
        Public Function GetCenterListByBKCertNo(ByVal PAD As String) As DataTable
            Dim LocalQuery As String = " SELECT I.INS_NAME,C.CEN_INS_ID , C.CEN_ID ,C.CEN_PAD_NO ,C.CEN_UID ,'Not Created' AS CREATION_STATUS, CEN_CON_SCANCODE AS 'Password' " &
                                            " FROM CENTRE_INFO  C  INNER JOIN INSTITUTE_INFO  I ON C.CEN_INS_ID=I.INS_ID " &
                                            " WHERE  C.REC_STATUS IN (0,1,2) AND  I.REC_STATUS IN (0,1,2) AND C.CEN_BK_PAD_NO='" & PAD.ToUpper.Trim & "'  order by C.CEN_INS_ID,C.CEN_ID"
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetCenterListByBKCertNo, LocalQuery, ClientScreen.Start_Create_Center, RealTimeService.Tables.CENTRE_INFO, PAD)
        End Function

        Public Function Get_Contact_Info(ByVal PAD As Int32) As List(Of Return_Get_Contact_Info)
            Dim RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_Get_Contact_Info, ClientScreen.Start_Create_Center, PAD)
            Dim _ContactList As List(Of Return_Get_Contact_Info) = New List(Of Return_Get_Contact_Info)()

            For Each row As DataRow In RetTable.Rows
                Dim newdata = New Return_Get_Contact_Info()
                newdata.Details = row.Field(Of String)("Details")
                newdata.Name = row.Field(Of String)("Name")
                newdata.Contact1 = row.Field(Of String)("Contact1")
                newdata.Contact2 = row.Field(Of String)("Contact2")
                newdata.Contact3 = row.Field(Of String)("Contact3")
                newdata.Contact4 = row.Field(Of String)("Contact4")
                _ContactList.Add(newdata)
            Next
            Return _ContactList
        End Function

        Public Function GetCentre_BKPadNo_Inst(Optional ByVal InsttID As String = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inBasicParam = GetBaseParams(ClientScreen.Start_Select_Center)
            Dim query As String = "select * from (select cen_id,cen_uid,cen_name,ii.ins_name,ii.ins_short,ii.INS_ID from centre_info ci inner join institute_info ii on ci.cen_ins_id=ii.ins_id  where ci.rec_status in(0,1,2)"
            If Not InsttID = "00010" Then
                query = query & "AND CEN_BK_PAD_NO = (SELECT CEN_BK_PAD_NO FROM CENTRE_INFO WHERE CEN_ID = " & cBase._open_Cen_ID & ")"
            End If
            If (String.IsNullOrWhiteSpace(InsttID) = False) Then
                query = query & " OR ci.cen_ins_id='" & InsttID & "'"
            End If

            query = query & " UNION select cen_id,cen_uid,cen_name,ii.ins_name,ii.ins_short,ii.INS_ID from centre_info ci inner join institute_info ii on ci.cen_ins_id=ii.ins_id where ci.rec_status in(0,1,2) AND ci.cen_id in (4424)  )as centre ORDER BY INS_ID,CEN_UID "
            Return _RealService.List(Tables.CENTRE_INFO, query, "CENTRE_INFO", inBasicParam)
        End Function

        'Create Center Page : Creates Entry for currently selected Center Entries in COD_Info and Client_User_Info, no need to shift
        Public Function CreateCenterForClient(ByVal Cen_Id As Integer, ByVal Cen_PAD_No As String, ByVal Cen_Password As String, F_Year As String) As Boolean
            Dim Result As Boolean = True
            Dim _CodInfo As New CodInfo(cBase)
            If Not _CodInfo.Delete(Cen_Id, F_Year, ClientScreen.Start_Create_Center) Then Result = False
            If Not _CodInfo.AddNew(Cen_Id, F_Year) Then Result = False

            Dim _clientUser As New ClientUserInfo(cBase)

            If _clientUser.GetUserCount(Cen_Id, ClientScreen.Start_Create_Center) <= 0 Then
                Dim InParam As Parameter_AddNew_ClientUserInfo = New Parameter_AddNew_ClientUserInfo()
                InParam.Cen_Id = Cen_Id
                InParam.Cen_PAD_No = Cen_PAD_No
                InParam.Cen_Password = Cen_Password

                If Not _clientUser.AddNew(InParam) Then Result = False
            End If
            Return Result
        End Function

        ''' <summary>
        ''' Makes Session State Active = false for current user, Shifted
        ''' </summary>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_LogOut</remarks>
        Public Sub LogOut()
            If cBase._open_User_ID <> Nothing Then
                UpdateRecord(RealTimeService.RealServiceFunctions.CentreRelated_Center_LogOut, ClientScreen.Home_StartUp, Nothing) 'As no param is required on sErver
            End If
        End Sub

        'Basically a Update Function , which settles certain more things 
        ''' <summary>
        ''' Makes CURRENT CENTER AUDIT STATUS COMPLETED, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_SubmitReport</remarks>
        Public Function SubmitReport() As Boolean
            Dim Result As Boolean = False
            If cBase._open_User_ID <> Nothing Then
                Result = UpdateRecord(RealTimeService.RealServiceFunctions.CentreRelated_Center_SubmitReport, ClientScreen.Home_StartUp, "")
            End If
            Return Result
        End Function

        ''' <summary>
        ''' Gets Audit Txn Period, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetAuditTxnPeriod</remarks>
        Public Function GetAuditTxnPeriod() As DataTable
            Dim Query As String = ""
            Query = "SELECT TOP 1 HE_TNXS_FROM AS 'FROM',  HE_TNXS_TO AS 'TO' FROM SO_HO_EVENT_INFO WHERE HE_EVENT_ID = (" &
                                        "SELECT CAS_EVENT_ID FROM so_center_audit_stats WHERE CAS_CEN_ID = '" + cBase._open_Cen_ID.ToString + "' AND CAS_AUDITOR_ID = '" + cBase._open_User_ID + "' AND CAS_STATUS = 1 )"

            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetAuditTxnPeriod, Query, ClientScreen.Start_Auditor_Submit_Report, RealTimeService.Tables.SO_HO_EVENT_INFO)
        End Function

        'GETS UNLOCKED ENTRIES COUNT FOR CURRENT AUDIT EVENT SPECIFIED PERIOD LEAVING NOTEBOOK ENTRIES 
        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetUnlockedTxnCount</remarks>
        Public Function GetUnlockedTxnCount() As String
            Dim Message As String = ""
            Dim Value As Object = GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetUnlockedTxnCount, ClientScreen.Start_Auditor_Submit_Report)
            If IsDBNull(Value) Then Return Nothing
            Message = CType(Value, String)
            Return Message
        End Function

        ''' <summary>
        ''' GETS UNLOCKED ENTRIES COUNT FOR CURRENT AUDIT EVENT SPECIFIED PERIOD LEAVING NOTEBOOK ENTRIES, Shifted
        ''' </summary>
        ''' <param name="FromDate"></param>
        ''' <param name="ToDate"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_Center_GetTxnStatusCount</remarks>
        Public Function GetTxnStatusCount(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
            Dim Param As Param_Center_GetTxnStatusCount = New Param_Center_GetTxnStatusCount()
            Param.FromDate = FromDate
            Param.ToDate = ToDate
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetTxnStatusCount, ClientScreen.Start_Auditor_Submit_Report, Param)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="FromDate"></param>
        ''' <param name="ToDate"></param>
        ''' <returns></returns>
        ''' <remarks>CentreRelated_Center_GetTransfersStatus</remarks>
        Public Function GetTransfersStatus(ByVal FromDate As Date, ByVal ToDate As Date) As String
            Dim Query As String = "SELECT (SELECT COUNT(TR_AMOUNT) FROM TRANSACTION_INFO WHERE TR_CODE = 8 AND REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID IS NULL AND TR_SR_NO = 1 AND TR_CEN_ID ='" & cBase._open_Cen_ID & "'AND (CAST(TR_DATE AS DATE) BETWEEN '" & FromDate.ToString(cBase._Server_Date_Format_Short) & "' AND '" & ToDate.ToString(cBase._Server_Date_Format_Short) & "')) AS UNMATCHED , "
            Query += "(SELECT COUNT(TR_AMOUNT) FROM TRANSACTION_INFO WHERE TR_CODE = 8 AND REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID IS NOT NULL AND TR_SR_NO = 1 AND TR_CEN_ID ='" & cBase._open_Cen_ID & "' AND (CAST(TR_DATE AS DATE) BETWEEN '" & FromDate.ToString(cBase._Server_Date_Format_Short) & "' AND '" & ToDate.ToString(cBase._Server_Date_Format_Short) & "')) AS MATCHED"
            Dim inParam As Param_Center_GetTransfersStatus = New Param_Center_GetTransfersStatus()
            inParam.FromDate = FromDate
            inParam.ToDate = ToDate
            Dim Record As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetTransfersStatus, ClientScreen.Start_Auditor_Submit_Report, inParam)
            Return "Matched Internal Transfers: " & Record.Rows(0)("Matched") & vbNewLine & "Unmatched Internal Transfers: " & Record.Rows(0)("Unmatched")
        End Function

        Public Function Get_Audit_Verifications() As DataTable
            Return GetMisc("AUDIT VERIFICATIONS", ClientScreen.Start_Auditor_Audit_Verification, "Name", "RecID")
        End Function

        Public Function GetAuditPeriod() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_GetAuditPeriod, ClientScreen.Start_Auditor_Submit_Report)
        End Function

        ''' <summary>
        ''' uses DAL 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="InstituteID">ins id from base</param>
        ''' <param name="ZoneID">comma separated list of zone short id</param>
        ''' <param name="SubZoneID">comma separated list of subzone short id</param>
        ''' <param name="StateID">comma separated list of state rec id</param>
        ''' <returns></returns>
        Public Function GetInstUIDList(screen As ClientScreen, InstituteID As String, Optional ZoneIDs As String() = Nothing, Optional SubZoneIDs As String() = Nothing, Optional StateIDs As String() = Nothing) As List(Of Return_GetInstUIDList)
            Dim _ZoneIDs As String = ""
            Dim _SubZoneIDs As String = ""
            Dim _StateIDs As String = ""
            Dim Counter As Int32 = 0
            If ZoneIDs IsNot Nothing Then
                For Each ID As String In ZoneIDs
                    Counter += 1
                    If ID.Length > 0 Then
                        _ZoneIDs = _ZoneIDs + IIf(ID.StartsWith("'"), "", "'") + ID + IIf(ID.EndsWith("'"), "", "'")
                        If Counter < ZoneIDs.Length Then _ZoneIDs = _ZoneIDs + ","
                    End If
                Next
            End If
            Counter = 0
            If SubZoneIDs IsNot Nothing Then
                For Each ID As String In SubZoneIDs
                    Counter += 1
                    If ID.Length > 0 Then
                        _SubZoneIDs = _SubZoneIDs + IIf(ID.StartsWith("'"), "", "'") + ID + IIf(ID.EndsWith("'"), "", "'")
                        If Counter < SubZoneIDs.Length Then _SubZoneIDs = _SubZoneIDs + ","
                    End If
                Next
            End If
            Counter = 0
            If StateIDs IsNot Nothing Then
                For Each ID As String In StateIDs
                    Counter += 1
                    If ID.Length > 0 Then
                        _StateIDs = _StateIDs + IIf(ID.StartsWith("'"), "", "'") + ID + IIf(ID.EndsWith("'"), "", "'")
                        If Counter < StateIDs.Length Then _StateIDs = _StateIDs + ","
                    End If
                Next
            End If
            'Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = "SELECT CI.CEN_UID,CI.CEN_NAME, CI_MAIN.CEN_ZONE_ID,CI_MAIN.CEN_ZONE_SUB_ID, Type.ACC_TYPE AS CEN_ACC_TYPE, ci.CEN_ID, ST.ST_NAME " &
                                    " FROM centre_info AS CI " &
                                    " INNER JOIN centre_info AS CI_MAIN ON ci.cen_bk_pad_no = CI_MAIN.cen_pad_no AND CI_MAIN.CEN_MAIN = 1 " &
                                    " INNER JOIN COD_INFO AS cod ON cod.CEN_ID = ci.CEN_ID AND COD_ACTIVE=1 and cod.REC_STATUS  IN (0,1,2)  " &
                                    " inner join centre_acc_type_info as Type on cod.COD_CEN_ACC_TYPE_ID = Type.REC_ID  " &
                                    " LEFT JOIN map_state_info AS ST ON CI_MAIN.CEN_STATE_ID = ST.REC_ID " &
                                    " WHERE CI.REC_STATUS IN (0,1,2) AND CI.CEN_INS_ID = '" + InstituteID + "' " &
                                    " AND (CI_MAIN.CEN_ZONE_ID IN( " + IIf(_ZoneIDs = "", "''", _ZoneIDs) + ") OR " + _ZoneIDs.Length.ToString() + "=0) " &
                                    " AND (CI_MAIN.CEN_ZONE_SUB_ID IN( " + IIf(_SubZoneIDs = "", "''", _SubZoneIDs) + ") OR " + _SubZoneIDs.Length.ToString() + "=0) " &
                                    " AND (CI_MAIN.CEN_STATE_ID IN(" + IIf(_StateIDs = "", "''", _StateIDs) + ") OR " + _StateIDs.Length.ToString() + "=0) ORDER BY CI.CEN_UID "
            Dim retTable As DataTable = _RealService.List(Tables.CENTRE_INFO, Query, Tables.CENTRE_INFO.ToString(), GetBaseParams(screen))
            Dim CenterList As New List(Of Return_GetInstUIDList)
            If Not retTable Is Nothing Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata As New Return_GetInstUIDList()
                    newdata.UID = row.Field(Of String)("CEN_UID")
                    newdata.Center = row.Field(Of String)("CEN_NAME")
                    newdata.Zone = row.Field(Of String)("CEN_ZONE_ID")
                    newdata.SubZone = row.Field(Of String)("CEN_ZONE_SUB_ID")
                    newdata.AccType = row.Field(Of String)("CEN_ACC_TYPE")
                    newdata.State = row.Field(Of String)("ST_NAME")
                    newdata.CEN_ID = row.Field(Of Int32)("CEN_ID")
                    CenterList.Add(newdata)
                Next
            End If
            Return CenterList
        End Function
        ''' <summary>
        ''' Directly uses DB Layer
        ''' </summary>
        ''' <returns></returns>
        Public Function GetZoneList(screen As ClientScreen) As List(Of Return_Get_Zone)
            'Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = " select ZONE_NAME from zone_info order by ZONE_NAME "
            Dim retTable As DataTable = _RealService.List(Tables.ZONE_INFO, Query, Tables.ZONE_INFO.ToString(), GetBaseParams(screen))
            Dim ZoneList As New List(Of Return_Get_Zone)
            If Not retTable Is Nothing Then
                For Each row As DataRow In retTable.Rows
                    Dim newRow = New Return_Get_Zone()
                    newRow.Zone_Name = row.Field(Of String)("ZONE_NAME")
                    ZoneList.Add(newRow)
                Next
            End If
            Return ZoneList
        End Function

        ''' <summary>
        ''' Reurns Subzones for selected Zones
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="Zone_Short_IDs">Comma Separated list of Zone IDs</param>
        ''' <returns></returns>
        Public Function GetSubZoneList(screen As ClientScreen, Optional Zone_Short_IDs As String() = Nothing) As List(Of Return_GetSubZoneList)
            Dim _Zone_Short_IDs As String = ""
            Dim Counter As Int32 = 0
            For Each ID As String In Zone_Short_IDs
                Counter += 1
                If ID.Length > 0 Then
                    _Zone_Short_IDs = _Zone_Short_IDs + IIf(ID.StartsWith("'"), "", "'") + ID + IIf(ID.EndsWith("'"), "", "'")
                    If Counter < Zone_Short_IDs.Length Then _Zone_Short_IDs = _Zone_Short_IDs + ","
                End If
            Next

            'Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = "select ZONE_NAME Name, ZONE_SUB_ID Short, zone_id Zone from zone_sub_info WHERE (ZONE_ID IN( " + IIf(_Zone_Short_IDs = "", "''", _Zone_Short_IDs) + ")) OR " + _Zone_Short_IDs.Length.ToString() + " = 0  order by ZONE_NAME"
            Dim retTable As DataTable = _RealService.List(Tables.ZONE_SUB_INFO, Query, Tables.ZONE_SUB_INFO.ToString(), GetBaseParams(screen))
            Dim SubZoneList As New List(Of Return_GetSubZoneList)
            If Not retTable Is Nothing Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata As New Return_GetSubZoneList()
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.ShortName = row.Field(Of String)("Short")
                    newdata.Zone = row.Field(Of String)("Zone")
                    SubZoneList.Add(newdata)
                Next
            End If
            Return SubZoneList
        End Function

        Public Overloads Function DeleteVerifications(ByVal CenId As Integer, ByVal YearId As Integer) As Boolean
            Return DeleteByCondition(" AV_CEN_ID=" & CenId.ToString & "  AND AV_YEAR_ID = " & YearId.ToString & " ", Tables.SO_AUDIT_VERIFICATIONS, ClientScreen.Start_Auditor_Audit_Verification)
        End Function

        Public Overloads Function DeleteInsuranceVerifications(ByVal Cen_BK_PAD_No As String, ByVal YearId As Integer, ByVal MiscId As String) As Boolean
            Return DeleteByCondition(" AV_CEN_ID IN (SELECT CEN_ID FROM centre_info WHERE CEN_BK_PAD_NO = '" & Cen_BK_PAD_No & "') AND AV_YEAR_ID = " & YearId.ToString & " AND AV_VERIFICATION_MISC_ID = '" & MiscId & "' ", Tables.SO_AUDIT_VERIFICATIONS, ClientScreen.Start_Auditor_Audit_Verification)
        End Function

        Public Function AddVerification(ByVal InParam As Param_AddVerification) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_AddAuditVerifications, ClientScreen.Start_Auditor_Audit_Verification, InParam)
        End Function

        Public Function AddInsuranceVerification(ByVal InParam As Param_AddInsuranceVerification) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.CentreRelated_AddInsuranceVerification, ClientScreen.Start_Auditor_Audit_Verification, InParam)
        End Function

        Public Function GetCompletedVerification() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_GetCompletedAuditVerifications, ClientScreen.Start_Auditor_Audit_Verification)
        End Function

        Public Function IsFinalAuditCompleted() As Boolean
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_Center_IsFinalAuditCompleted, ClientScreen.Facility_AddressBook)
        End Function

        Public Function IsFinalReportSubmitted(Cen_BK_Pad_no As String) As Boolean
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_Center_IsFinalReportSubmitted, ClientScreen.Start_Auditor_Audit_Verification, Cen_BK_Pad_no)
        End Function

        Public Function Get_Base_OpenEventId() As DataSet
            Return GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.CentreRelated_Center_Get_Base_OpenEventId, ClientScreen.Start_Auditor_Submit_Report, Nothing)
        End Function

        Public Function IsReportSubmitted(EventId As Integer) As Boolean
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_Center_IsReportSubmitted, ClientScreen.Start_Auditor_Submit_Report, EventId)
        End Function

        Public Function GetReportsToBePrintedInfo(ByVal YearId As Integer) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_GetReportsToBePrinted, ClientScreen.Start_Select_Center, YearId)
        End Function



        Public Function VerifyAudit() As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_VerifyAudit, ClientScreen.CommonFunctions, Nothing)
        End Function

        Public Function ReturnedForCorrection() As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_ReturnForCorrection, ClientScreen.CommonFunctions, Nothing)
        End Function

        Public Function ResumeAudit(Cen_ID As Integer, YearID As Integer) As Boolean
            Dim param As Param_ResumeAudit = New Param_ResumeAudit
            param.Cen_ID = Cen_ID
            param.Year_ID = YearID
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_ResumeAudit, ClientScreen.CommonFunctions, param)
        End Function

        Public Function Client_Audit_Info() As DataSet
            Dim param As New Param_get_Client_Audit_Info
            param.INSTTID = cBase._open_Ins_ID
            Return GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.CentreRelated_get_Client_Audit_Info, ClientScreen.CommonFunctions, param)
        End Function

        Public Function GetlatestCenterGrade(Optional EventID As Integer = 0) As String
            Return GetScalarBySP(RealTimeService.RealServiceFunctions.CentreRelated_Center_GetlatestCenterGrade, ClientScreen.CommonFunctions, EventID)
        End Function

        Public Function GetAccountsSubmittedPeriod() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_GetAccountsSubmittedPeriod, ClientScreen.CommonFunctions)
        End Function
        Public Function GetLatestAccountsSubmittedPeriod() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim _base_obj As Basic_Param = GetBaseParams(ClientScreen.Facility_ServiceReport)
            Dim Query As String = "SELECT top 1 CSA_SUBMIT_FROM, CSA_SUBMIT_TO, CSA_SUBMIT_ON, CSA_SUBMIT_BY FROM so_center_submission_accounts WHERE CSA_CEN_ID = " & _base_obj.openCenID.ToString & " ORDER BY CSA_SUBMIT_ON DESC "
            Return _RealService.List(Tables.SO_CENTER_SUBMISSION_ACCOUNTS, Query, Tables.SO_CENTER_SUBMISSION_ACCOUNTS.ToString(), _base_obj)
        End Function

        Public Function AddAccountsSubmissionPeriod(inParam As Param_AddAccountsSubmissionPeriod)
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_AddAccountsSubmissionPeriod, ClientScreen.CommonFunctions, inParam)
        End Function

        Public Function GetAccountsSubmissionReport() As DataTable
            Dim inParam As New Param_GetAccountsSubmissionReport
            inParam.YearStartDate = cBase._open_Year_Sdt
            inParam.YearEndDate = cBase._open_Year_Edt
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.CentreRelated_GetAccountsSubmissionReport, ClientScreen.CommonFunctions, inParam)
        End Function
        Public Function GetCentreEmails(WingShort As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim DataBase_xls As DataTable = _RealService.List(Tables.CENTRE_INFO, "SELECT CEN_EMAIL_ID_1,CEN_NAME FROM centre_info WHERE CEN_NAME LIKE 'D%' AND LEN(COALESCE(CEN_EMAIL_ID_1,''))>0 ORDER BY CEN_NAME", Tables.CENTRE_INFO.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))

            Return DataBase_xls
        End Function
    End Class
    <Serializable>
    Public Class ClientUserInfo
        Inherits SharedVariables


#Region "Param Classes"
        ''' <summary>
        ''' Return class for GetControlVisibility()
        ''' </summary>
        <Serializable>
        Public Class Return_ControlVisibility
            Public Screen_Name As Common_Lib.RealTimeService.ClientScreen
            Public Control_Name As String
            Public Visible As Boolean
            'Public Area As Common_Lib.RealTimeService.scree
        End Class
        <Serializable>
        Public Class Return_GetRegister
            Public Property UserName As String
            Public Property PersonnelName As String
            Public Property SewaDept As String
            Public Property ContactNo As String
            Public Property EmailID As String
            Public Property Groups As String
            Public Property Is_Admin As Boolean
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedBy As String
            Public Property EditedOn As DateTime
            Public Property ID As String
            Public Property UserType As String
            Public Property UserSkills As String
            Public Property SELFPOSTED As Boolean
        End Class
        <Serializable>
        Public Class Return_GetGroupRegister
            Public Property main_Register As List(Of Return_GetGroupRegister_MainTable)
            Public Property nested_Register As List(Of Return_GetGroupRegister_NestedTable)
        End Class
        <Serializable>
        Public Class Return_GetGroupRegister_MainTable
            Public Property Group_Name As String
            Public Property Group_Desc As String
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedBy As String
            Public Property EditedOn As DateTime
            Public Property ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetGroupRegister_NestedTable
            Public Property UserName As String
            Public Property Personnel_Name As String
            Public Property Sewa_Dept As String
            Public Property SkillTypes As String
            Public Property UserType As String
            Public Property ID As String
            Public Property GroupID As Int32
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedBy As String
            Public Property EditedOn As DateTime
        End Class
        <Serializable>
        Public Class Return_GetPrivilegesRegister
            Public Property UserName As String
            Public Property GroupName As String
            Public Property EntityName As String
            Public Property PrivilegesGiven As String
            Public Property UserNameV As String
            Public Property GroupNameV As String
            Public Property PrivilegeID As String
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedBy As String
            Public Property EditedOn As DateTime
            Public Property ScreenID As Int32?
            Public Property GrpID As Int32?
            Public Property UserID As String
        End Class
        <Serializable>
        Public Class Return_GetScreens
            Public Property TaskID As Int32
            Public Property TaskName As String
            Public Property TaskDisplayName As String

        End Class
        <Serializable>
        Public Class Return_GetPrivileges
            Public Property Privilege_Code As String
            Public Property Privilege_Value As String
        End Class
        <Serializable>
        Public Class Return_GetUserDetails
            Public Property PersonnelName As String
            Public Property PersonnelID As Int32?
            Public Property SewaDept As String
            Public Property UserName As String
            Public Property UserPassword As String
            Public Property ContactNo As String
            Public Property EmailID As String
            Public Property Groups As String
            Public Property Is_Admin As Boolean?
            Public Property SelfPosted As Boolean?

        End Class
        <Serializable>
        Public Class Return_GetGroupDetails
            Public Property GroupName As String
            Public Property GroupRemarks As String
        End Class
        <Serializable>
        Public Class Return_GetAuditors_Superusers
            Public Property USER_ID As String
            Public Property REC_ID As String
        End Class
        <Serializable>
        Public Class Return_VouchingCategoryList
            Public Category As String
            Public Code As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCenterTasks() As DataTable
            Return GetCenterTaskInfo(ClientScreen.Start_Select_Center)
        End Function

        Public Function GetWingsCenterTasks() As DataTable
            Return GetWingsCenterTaskInfo(ClientScreen.Start_Select_Center)
        End Function

        Public Overloads Function Delete(ByVal Cen_Id As Integer) As Boolean
            Return DeleteByCondition(" CEN_ID =" & Cen_Id.ToString & " ", Tables.CLIENT_USER_INFO, ClientScreen.Start_Create_Center)
        End Function

        ''' <summary>
        ''' Gets User Info with custom CenId, Shifted
        ''' </summary>
        ''' <param name="Cen_ID"></param>
        ''' <param name="User_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfoCustomCenId</remarks>
        Public Function GetCenterUserInfo(ByVal Cen_ID As String, ByVal User_ID As String) As DataTable
            'Dim SQL_STR As String = " SELECT USER_PWD, USER_ROLE_ID FROM CLIENT_USER_INFO WHERE  REC_STATUS IN (0,1,2) AND (CEN_ID='" & Cen_ID & "' OR UPPER(USER_ROLE_ID)='SUPERUSER') and USER_ID='" & User_ID.ToUpper & "'"
            Dim Param As Param_ClientUserInfo_GetUserInfo = New Param_ClientUserInfo_GetUserInfo()
            Param.Cen_ID = Cen_ID
            Param.User_ID = User_ID
            Return GetDataListOfRecordsWithCenID(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfoCustomCenId, ClientScreen.Start_Login, Cen_ID, Param)
        End Function

        ''' <summary>
        ''' Gets User Info, Shifted
        ''' </summary>
        ''' <param name="User_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfo</remarks>
        Public Function GetUserInfo(ByVal User_ID As String) As DataTable
            Dim SQL_STR As String = " SELECT USER_PWD, USER_ROLE_ID FROM CLIENT_USER_INFO WHERE  REC_STATUS IN (0,1,2) AND USER_ID='" & User_ID.ToUpper & "'"
            Return GetDataListOfRecords(RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserInfo, ClientScreen.Start_Auditor_Login, User_ID)
        End Function

        Public Function GetUsersList() As DataTable
            Return GetDataListOfRecords(RealServiceFunctions.CentreRelated_ClientUserInfo_GetUsersList, ClientScreen.Profile_Magazine)
        End Function

        ''' <summary>
        '''  Add new Id and Pwd, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_AddNew</remarks>
        Public Function AddNew(ByVal InParam As Parameter_AddNew_ClientUserInfo) As Boolean
            Dim RecID As String = System.Guid.NewGuid().ToString()
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_AddNew, ClientScreen.Start_Create_Center, InParam.Cen_Id, InParam)
        End Function

        ''' <summary>
        ''' Changes Password, Shifted
        ''' </summary>
        ''' <param name="CPwd"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_ChangePassword</remarks>
        Public Function ChangePassword(ByVal CPwd As Parameter_CPwd_ClientUserInfo) As Boolean
            'Dim CenIDStr As String = " = '" & CPwd.Cen_Id & "'"
            'If CPwd.Cen_Id = Nothing Or CPwd.Cen_Id = 0 Then CenIDStr = " IS NULL"
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_ChangePassword, CPwd.screen, CPwd)
        End Function

        ''' <summary>
        ''' Gets pwd and RoleId based on UserID, Shifted
        ''' </summary>
        ''' <param name="UserID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetList</remarks>
        Public Function GetList(ByVal UserID As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetList, screen, UserID)
        End Function

        ''' <summary>
        ''' Gets User Pwd and RoleID filtered by CenId and userID, Shifted
        ''' </summary>
        ''' <param name="UserID"></param>
        ''' <param name="CenID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetListFilteredByCenIdAndUserId</remarks>
        Public Function GetList(ByVal UserID As String, ByVal CenID As Integer, ByVal screen As ClientScreen) As DataTable
            Dim Param As Param_ClientUserInfo_GetListFilteredByCenIDUserID = New Param_ClientUserInfo_GetListFilteredByCenIDUserID()
            Param.CenID = CenID
            Param.UserID = UserID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetListFilteredByCenIdAndUserId, screen, Param)
        End Function

        ''' <summary>
        ''' Gets Username based on CenId, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="CenID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetListByCenID</remarks>
        Public Function GetList(ByVal screen As ClientScreen, ByVal CenID As Integer) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetListByCenID, screen, CenID)
        End Function

        ''' <summary>
        ''' Gets User Count, Shifted
        ''' </summary>
        ''' <param name="CenID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserCount</remarks>
        Public Function GetUserCount(ByVal CenID As Integer, ByVal screen As ClientScreen) As Object
            Dim Query As String = "SELECT COUNT(*) FROM Client_User_Info WHERE  REC_STATUS IN (0,1,2) AND CEN_ID=" & CenID.ToString & " "
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserCount, screen, CenID)
        End Function

        ''' <summary>
        ''' Gets Max UserID, Shifted
        ''' </summary>
        ''' <param name="Cen_ID"></param>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_ClientUserInfo_GetMaxUserID</remarks>
        Public Function GetMaxUserID(ByVal Cen_ID As Integer, ByVal Screen As ClientScreen) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetMaxUserID, Screen, Cen_ID)
        End Function

        Public Function GetNotifications() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_GetNotifications, ClientScreen.Start_Auditor_Notify)
        End Function

        Public Overloads Function DeleteNotifications(ByVal ID As String) As Boolean
            Return DeleteByCondition("ID=" & ID & " ", Tables.SO_CENTRE_NOTIFICATIONS, ClientScreen.Start_Auditor_Notify)
        End Function

        Public Function AddNotification() As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_AddNotification, ClientScreen.Start_Auditor_Notify, Nothing)
        End Function

        Public Function GetRegister(Optional CenterUserOnly As Boolean = True) As List(Of Return_GetRegister)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.CentreRelated_ClientUserInfo_GetRegister, ClientScreen.Options_ClientUser, CenterUserOnly)
            Dim _User_data As List(Of Return_GetRegister) = New List(Of Return_GetRegister)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetRegister
                    newdata.UserName = row.Field(Of String)("UserName")
                    newdata.PersonnelName = row.Field(Of String)("PersonnelName")
                    newdata.SewaDept = row.Field(Of String)("SewaDept")
                    newdata.ContactNo = row.Field(Of String)("ContactNo")
                    newdata.EmailID = row.Field(Of String)("EmailID")
                    newdata.Groups = row.Field(Of String)("Groups")
                    newdata.Is_Admin = row.Field(Of Boolean)("Is_Admin")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    newdata.UserType = row.Field(Of String)("UserType")
                    newdata.UserSkills = row.Field(Of String)("UserSkills")
                    newdata.SELFPOSTED = row.Field(Of Boolean)("SELFPOSTED")
                    _User_data.Add(newdata)
                Next
            End If
            Return _User_data
        End Function

        Public Function GetUserDetails(UserRecID As String) As Return_GetUserDetails
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserDetails, ClientScreen.Options_ClientUser, UserRecID)
            Dim _User_data As Return_GetUserDetails = New Return_GetUserDetails
            If (Not (retTable) Is Nothing) Then
                _User_data.PersonnelName = retTable.Rows(0).Field(Of String)("PersonnelName")
                _User_data.PersonnelID = retTable.Rows(0).Field(Of Int32?)("PersonnelID")
                _User_data.SewaDept = retTable.Rows(0).Field(Of String)("SewaDept")
                _User_data.UserName = retTable.Rows(0).Field(Of String)("UserName")
                _User_data.ContactNo = retTable.Rows(0).Field(Of String)("ContactNo")
                _User_data.EmailID = retTable.Rows(0).Field(Of String)("EmailID")
                _User_data.Groups = retTable.Rows(0).Field(Of String)("Groups")
                _User_data.Is_Admin = retTable.Rows(0).Field(Of Boolean?)("Is_Admin")
                _User_data.SelfPosted = retTable.Rows(0).Field(Of Boolean?)("SELFPOSTED")
                _User_data.UserPassword = retTable.Rows(0).Field(Of String)("user_pwd")
            End If
            Return _User_data
        End Function
        Public Function Get_ClientUser_EntriesCount(ByVal UserID As String) As Int32
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_Get_ClientUser_EntriesCount, ClientScreen.Options_ClientUser, UserID))
        End Function
        Public Function GetUserNameCount(ByVal UserName As String, Optional UserRecID As String = "") As Object
            Dim Inparam As New Param_GetUserNameCount
            Inparam.CenID = cBase._open_Cen_ID
            Inparam.User_Name = UserName
            Inparam.UserRecID = UserRecID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_GetUserNameCount, ClientScreen.Options_ClientUser, Inparam)
        End Function
        Public Function GetUserDataIfAlreadyExists(ByVal UserName As String, Optional UserRecID As String = "") As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Options_ClientUser)
            Dim Query As String = Nothing
            If (String.IsNullOrWhiteSpace(UserRecID)) Then
                Query = "select centre_info.CEN_UID, centre_info.CEN_NAME from client_user_info INNER JOIN centre_info ON client_user_info.CEN_ID = centre_info.CEN_ID where client_user_info.USER_ID= '" & UserName & "'"
            Else
                Query = "select centre_info.CEN_UID, centre_info.CEN_NAME from client_user_info INNER JOIN centre_info ON client_user_info.CEN_ID = centre_info.CEN_ID where client_user_info.USER_ID= '" & UserName & "' AND client_user_info.REC_ID NOT IN('" & UserRecID & "')"
            End If
            Return _RealService.List(Tables.CLIENT_USER_INFO, Query, Tables.CLIENT_USER_INFO.ToString, inbasicparam)
        End Function

        Public Function GetAuditors_Superusers(screen As ClientScreen) As List(Of Return_GetAuditors_Superusers)
            ' Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = "SELECT USER_ID,REC_ID FROM client_user_info WHERE USER_ROLE_ID IN ('SUPERUSER','AUDITOR') ORDER BY USER_ID "
            Dim retTable As DataTable = _RealService.List(Tables.CLIENT_USER_INFO, Query, Tables.CLIENT_USER_INFO.ToString(), GetBaseParams(screen))
            Dim UserList As New List(Of Return_GetAuditors_Superusers)
            If Not retTable Is Nothing Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata As New Return_GetAuditors_Superusers()
                    newdata.USER_ID = row.Field(Of String)("USER_ID")
                    newdata.REC_ID = row.Field(Of String)("REC_ID")
                    UserList.Add(newdata)
                Next
            End If
            Return UserList
        End Function
        Public Function GetVouchingCategories() As List(Of Return_VouchingCategoryList)
            ' Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim Query As String = "SELECT CPL_VALUE Category, CPL_CODE Code FROM so_client_permission_list WHERE CPL_CODE LIKE '%VOUCH'"
            Dim retTable As DataTable = _RealService.List(Tables.SO_CLIENT_PERMISSION_LIST, Query, Tables.SO_CLIENT_PERMISSION_LIST.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Dim VouchingList As New List(Of Return_VouchingCategoryList)
            If Not retTable Is Nothing Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata As New Return_VouchingCategoryList()
                    newdata.Category = row.Field(Of String)("Category")
                    newdata.Code = row.Field(Of String)("Code")
                    VouchingList.Add(newdata)
                Next
            End If
            Return VouchingList
        End Function

        Public Function GetAllowedVouchingCategories() As DataTable
            ' Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CENID", "@USERID"}
            Dim values() As Object = {cBase._open_Cen_ID, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String}
            Dim lengths() As Integer = {8, 255}
            Return _RealService.ListFromSP(Tables.SO_CLIENT_PERMISSION_LIST, "sp_get_Allowed_Vouching_Categories", Tables.SO_CLIENT_PERMISSION_LIST.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function

        Public Function InsertClientUser(ByVal InParam As Param_InsertClientUser) As Boolean
            InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_InsertClientUser, ClientScreen.Options_ClientUser, InParam)
            Return True
        End Function
        Public Function UpdateClientUser(UpParam As Param_UpdateClientUser) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_UpdateClientUser, ClientScreen.Options_ClientUser, UpParam)
        End Function
        Public Function DeleteClientUser(Rec_ID As String) As Boolean
            DeleteByCondition("CUG_CU_ID = '" + Rec_ID + "'", Tables.CLIENT_USER_GROUP, ClientScreen.Options_ClientUser)
            DeleteByCondition("Rec_ID = '" + Rec_ID + "'", Tables.CLIENT_USER_INFO, ClientScreen.Options_ClientUser)
            Return True
        End Function
        Public Function GetControlVisibility(screen_name As RealTimeService.ClientScreen) As List(Of Return_ControlVisibility)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.CentreRelated_ClientUserInfo_GetControlVisibility, screen_name)
            Dim _Client_User_Info_data As List(Of Return_ControlVisibility) = New List(Of Return_ControlVisibility)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_ControlVisibility
                    newdata.Screen_Name = row.Field(Of String)("Screen Name")
                    newdata.Control_Name = row.Field(Of String)("Control Name")
                    newdata.Visible = row.Field(Of String)("Visible")
                    'newdata.Area = row.Field(Of String)("Area")
                    _Client_User_Info_data.Add(newdata)
                Next
            End If
            Return _Client_User_Info_data
        End Function



#Region "Group Related"
        Public Function GetGroupRegister() As Return_GetGroupRegister
            Dim _main_data As New Return_GetGroupRegister
            Dim _Group_main_data As List(Of Return_GetGroupRegister_MainTable) = New List(Of Return_GetGroupRegister_MainTable)
            Dim _Group_nested_data As List(Of Return_GetGroupRegister_NestedTable) = New List(Of Return_GetGroupRegister_NestedTable)
            _main_data.main_Register = _Group_main_data
            _main_data.nested_Register = _Group_nested_data

            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.CentreRelated_ClientGroupInfo_GetGroupRegister, ClientScreen.Stock_PO, Nothing)
            If (Not (retDataset) Is Nothing) Then
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetGroupRegister_MainTable
                    newdata.Group_Name = row.Field(Of String)("Group_Name")
                    newdata.Group_Desc = row.Field(Of String)("Group_Desc")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    _Group_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim newdata = New Return_GetGroupRegister_NestedTable
                    newdata.UserName = row.Field(Of String)("UserName")
                    newdata.Personnel_Name = row.Field(Of String)("Personnel_Name")
                    newdata.Sewa_Dept = row.Field(Of String)("Sewa_Dept")
                    newdata.SkillTypes = row.Field(Of String)("SkillTypes")
                    newdata.UserType = row.Field(Of String)("UserType")
                    newdata.GroupID = row.Field(Of Int32)("GroupID")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    _Group_nested_data.Add(newdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function GetGroupNameCount(ByVal GroupName As String, GroupID As Int32) As Object
            Dim Inparam As New Param_GetGroupNameCount
            Inparam.GroupID = GroupID
            Inparam.Group_Name = GroupName
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_ClientGroupInfo_GetGroupNameCount, ClientScreen.Options_ClientUser, Inparam)
        End Function
        Public Function GetGroupDetails(GroupRecID As String) As Return_GetGroupDetails
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.CentreRelated_ClientUserInfo_Get_ClientUser_GetGroupDetails, ClientScreen.Options_GroupMaster, GroupRecID)
            Dim _Group_data As Return_GetGroupDetails = New Return_GetGroupDetails
            If (Not (retTable) Is Nothing) Then
                _Group_data.GroupName = retTable.Rows(0).Field(Of String)("Group_Name")
                _Group_data.GroupRemarks = retTable.Rows(0).Field(Of String)("Group_Remarks")
            End If
            Return _Group_data
        End Function
        Public Function InsertClientUserGroup(ByVal InParam As Param_InsertClientUserGroup) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_ClientGroupInfo_InsertClientUserGroup, ClientScreen.Options_ClientUser, InParam)
        End Function
        Public Function UpdateClientUserGroup(UpParam As Param_UpdateClientUserGroup) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CentreRelated_ClientGroupInfo_UpdateClientUserGroup, ClientScreen.Options_ClientUser, UpParam)
        End Function
        Public Function DeleteClientUserGroup(GroupID As Int32) As Boolean
            DeleteByCondition("CUT_CUG_ID = " + GroupID.ToString(), Tables.SO_CLIENT_USER_TASK, ClientScreen.Options_ClientUser) 'Mantis bug 0001031 fixed
            DeleteByCondition("Rec_ID = " + GroupID.ToString(), Tables.CLIENT_GROUP_INFO, ClientScreen.Options_ClientUser)
            Return True
        End Function
        Public Function SaveClientUserGroupMapping(UpParam As Param_SaveClientUserGroupMapping) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_SaveClientUserGroupMapping, ClientScreen.Options_ClientUser, UpParam)
        End Function
#End Region
#Region "Priviledges Related"
        Public Function GetPrivilegesRegister(Inparam As Param_GetPrivilegeRegister) As List(Of Return_GetPrivilegesRegister)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.CentreRelated_ClientUserInfo_GetPrivilegeRegister, ClientScreen.Options_ClientUser, Inparam)
            Dim _User_data As List(Of Return_GetPrivilegesRegister) = New List(Of Return_GetPrivilegesRegister)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPrivilegesRegister
                    newdata.UserName = row.Field(Of String)("UserName")
                    newdata.GroupName = row.Field(Of String)("GroupName")
                    newdata.EntityName = row.Field(Of String)("EntityName")
                    newdata.PrivilegesGiven = row.Field(Of String)("PrivilegesGiven")
                    If (retTable.Columns.Contains("UserNameV")) Then
                        newdata.UserNameV = row.Field(Of String)("UserNameV")
                    End If
                    If (retTable.Columns.Contains("GroupNameV")) Then
                        newdata.GroupNameV = row.Field(Of String)("GroupNameV")
                    End If
                    newdata.PrivilegeID = row.Field(Of String)("PrivilegeID")
                    If (retTable.Columns.Contains("AddedBy")) Then
                        newdata.AddedBy = row.Field(Of String)("AddedBy")
                    End If
                    If (retTable.Columns.Contains("AddedOn")) Then
                        newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    End If
                    newdata.ScreenID = row.Field(Of Int32?)("ScreenID")
                    newdata.GrpID = row.Field(Of Int32?)("GrpID")
                    If (retTable.Columns.Contains("EditedOn")) Then
                        newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    End If
                    If (retTable.Columns.Contains("EditedBy")) Then
                        newdata.EditedBy = row.Field(Of String)("EditedBy")
                    End If
                    newdata.UserID = row.Field(Of String)("UserID")
                    _User_data.Add(newdata)
                Next
            End If
            Return _User_data
        End Function
        Public Function GetScreens(Screen As ClientScreen) As List(Of Return_GetScreens)
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.CentreRelated_ClientUserInfo_GetScreens, ClientScreen.Options_ClientUser)
            Dim _Screen_data As List(Of Return_GetScreens) = New List(Of Return_GetScreens)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetScreens
                    newdata.TaskName = row.Field(Of String)("TaskName")
                    newdata.TaskID = row.Field(Of Int32)("TaskID")
                    newdata.TaskDisplayName = row.Field(Of String)("TaskDisplayName")
                    _Screen_data.Add(newdata)
                Next
            End If
            Return _Screen_data
        End Function
        Public Function GetPrivileges() As List(Of Return_GetPrivileges)
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.CentreRelated_ClientUserInfo_GetPrivileges, ClientScreen.Options_ClientUser)
            Dim _Screen_data As List(Of Return_GetPrivileges) = New List(Of Return_GetPrivileges)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPrivileges
                    newdata.Privilege_Code = row.Field(Of String)("Code")
                    newdata.Privilege_Value = row.Field(Of String)("Value")
                    _Screen_data.Add(newdata)
                Next
            End If
            Return _Screen_data
        End Function
        Public Function InsertClientUserPrivileges(ByVal InParam As Param_InsertClientUserPrivileges) As Boolean
            If InParam.UserName <> Nothing Then
                InParam.GroupID = Nothing
            End If
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_InsertClientUserPrivileges, ClientScreen.Options_ClientUser, InParam)
        End Function

        'This function has been removed as we are using delete/insert instead 
        'Public Function UpdateClientUserPrivileges(UpParam As Param_UpdateClientUserPrivileges) As Boolean
        '    Return UpdateRecord(RealTimeService.RealServiceFunctions.CentreRelated_ClientUserInfo_UpdateClientUserPrivileges, ClientScreen.Options_ClientUser, UpParam)
        'End Function
        Public Function DeleteClientUserPrivileges(ScreenID As Int32, CenID As Int32, Optional UserID As String = Nothing, Optional GroupID As Int32 = 0) As Boolean
            If UserID = Nothing Then
                UserID = ""
            End If
            If UserID.Length > 0 Then
                DeleteByCondition("CUT_CT_ID = " + ScreenID.ToString() + " AND CUT_CU_ID = '" + UserID + "'" + " AND CUT_CEN_ID = " + CenID.ToString() + "", Tables.SO_CLIENT_USER_TASK, ClientScreen.Options_ClientUser)
            Else
                DeleteByCondition("CUT_CT_ID = " + ScreenID.ToString() + " AND CUT_CUG_ID = " + GroupID.ToString() + "" + " AND CUT_CEN_ID = " + CenID.ToString() + "", Tables.SO_CLIENT_USER_TASK, ClientScreen.Options_ClientUser)
            End If

            Return True
        End Function
#End Region
    End Class
    <Serializable>
    Public Class CodInfo
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Overloads Function Delete(ByVal Cen_Id As String, Year As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As Boolean
            Return DeleteByCondition(" CEN_ID    =" & Cen_Id & " AND " &
                              " COD_YEAR_ID   =" & Year.Split("-")(0).Replace(" ", "").Substring(2, 2) & Year.Split("-")(1).Replace(" ", "").Substring(2, 2) & " ", Tables.COD_INFO, screen)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Cen_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_CodInfo_AddNew</remarks>
        Public Function AddNew(ByVal Cen_Id As String, fyear As String) As Boolean
            Dim RecID As String = Guid.NewGuid.ToString
            Dim inparam As Param_CodInfo_AddNew = New Param_CodInfo_AddNew
            inparam.Cen_Id = Cen_Id
            inparam.YearID = fyear.Split("-")(0).Replace(" ", "").Substring(2, 2) & fyear.Split("-")(1).Replace(" ", "").Substring(2, 2)
            inparam.Year = fyear
            inparam.YearStartDate = New Date(Convert.ToInt32(fyear.Split("-")(0).Replace(" ", "")), 4, 1)
            inparam.YearEndDate = New Date(Convert.ToInt32(fyear.Split("-")(1).Replace(" ", "")), 3, 31)
            Return InsertRecord(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_AddNew, ClientScreen.Start_Create_Center, inparam)
        End Function

        Public Function GetActiveCentersFromSelected(ByVal SelectedCentre As String) As DataTable
            Return LocalGetActiveCentersFromSelected(SelectedCentre)
        End Function

        ''' <summary>
        ''' Get Created Centres From Selected, Shifted
        ''' </summary>
        ''' <param name="SelectedCentre"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.CentreRelated_CodInfo_GetCreatedCentersFromSelected</remarks>
        Public Function GetCreatedCentersFromSelected(ByVal SelectedCentre As String) As DataTable
            Dim Query As String = " select CEN_ID from COD_INFO WHERE REC_STATUS IN (0,1,2) AND CEN_ID IN (" & SelectedCentre & ")"
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_GetCreatedCentersFromSelected, ClientScreen.Options_ResetPassword, SelectedCentre)
        End Function

        Public Function Get_Cen_Creation_Count_OnClient() As Object
            Return LocalGet_Cen_Creation_Count_OnClient()
        End Function

        Public Function GetYearCount() As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_GetYearCount, ClientScreen.Start_Select_Center)
        End Function

        Public Function GetCompletedtYearCount() As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_GetCompletedYearCount, ClientScreen.Start_Select_Center)
        End Function

        Public Function CheckVolumeCenter() As Boolean
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_CheckVolumeCenter, ClientScreen.Start_Select_Center)
        End Function

        Public Function GetFinancialYearList() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_GetFinancialYearList, ClientScreen.Start_Select_Center)
        End Function
        Public Function UpdateDefaultFinancialYear(ByVal YearID As Int32) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_Update_default_financial_year"
            Dim params() As String = {"@CENID", "@YEARID"}
            Dim values() As Object = {cBase._open_Cen_ID, YearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            'used public update function as there are no transactional data involved 
            Return _RealService.UpdateBySPPublic(Tables.COD_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Start_Select_Center))
        End Function
        Public Function GetUnAuditedFinalYears() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_GetUnAuditedYearList, ClientScreen.Profile_Cash) 'step:2
        End Function

        Public Function IsYearOpenForCenterCreation(YearId As String) As Boolean
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CentreRelated_CodInfo_IsYearOpenForCenterCreation, ClientScreen.Start_Create_Center, YearId)
        End Function


    End Class
    <Serializable>
    Public Class Center_Purpose_Info
        Inherits SharedVariables
#Region "Parameter Classes"
        <Serializable>
        Public Class Return_CenterPurpose
            Inherits CommonReturnFields
            Public Property Purpose As String
            Public Property Active As Boolean
        End Class
#End Region
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetList() As List(Of Return_CenterPurpose)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.CenterPurpose_GetList, ClientScreen.Facility_Center_Purpose)
            Dim _Letter As List(Of Return_CenterPurpose) = New List(Of Return_CenterPurpose)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_CenterPurpose
                    newdata.Purpose = row.Field(Of String)("Purpose")
                    newdata.Active = row.Field(Of Boolean)("Active")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Add_Date = row.Field(Of DateTime)("Add Date").ToString("yyyy-MM-dd HH:mm:ss")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit Date").ToString("yyyy-MM-dd HH:mm:ss")
                    newdata.Action_Date = row.Field(Of DateTime)("Action Date").ToString("yyyy-MM-dd HH:mm:ss")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    _Letter.Add(newdata)
                Next
            End If
            Return _Letter
        End Function

        Public Function CheckUsageCount(ByVal Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.CenterPurpose_CheckUsageCount, ClientScreen.Facility_Center_Purpose, Rec_Id)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Facility_Center_Purpose, RealTimeService.Tables.CENTER_PURPOSE_INFO, Common.ClientDBFolderCode.SYS)
        End Function

        Public Function Insert(ByVal Purpose As String) As Boolean
            Dim InParam As New Parameter_Insert_Center_Purpose
            InParam.PurposeName = Purpose
            Return InsertRecord(RealTimeService.RealServiceFunctions.CenterPurpose_Insert, ClientScreen.Facility_Center_Purpose, InParam)
        End Function

        Public Function Update(ByVal Purpose As String, Rec_ID As String) As Boolean
            Dim UpParam As New Parameter_Update_Center_Purpose
            UpParam.PurposeName = Purpose
            UpParam.Rec_ID = Rec_ID
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CenterPurpose_Update, ClientScreen.Facility_Center_Purpose, UpParam)
        End Function

        Public Function Delete(Rec_ID As String) As Boolean
            Return DeleteRecord(Rec_ID, Tables.CENTER_PURPOSE_INFO, ClientScreen.Facility_Center_Purpose)
        End Function

        Public Function Activate(Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CenterPurpose_Activate, ClientScreen.Facility_Center_Purpose, Rec_ID)
        End Function

        Public Function DeActivate(Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.CenterPurpose_DeActivate, ClientScreen.Facility_Center_Purpose, Rec_ID)
        End Function
    End Class
#End Region
    <Serializable>
    Public Class HelpVideos
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function get_HelpVideos() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = "Select MISC_ID, MISC_NAME as Title, MISC_REMARK1 AS Category, MISC_REMARK2 as URL, REC_ID As ID, MISC_SRNO FROM MISC_INFO WHERE  REC_STATUS <>  " & Common_Lib.Common.Record_Status._Deleted & "  And MISC_ID ='help videos' ORDER BY MISC_SRNO,MISC_REMARK1"
            Return _RealService.List(Tables.MISC_INFO, query, Tables.MISC_INFO.ToString, GetBaseParams(ClientScreen.Facility_ChartResponsesInfo))
        End Function
    End Class
End Class
