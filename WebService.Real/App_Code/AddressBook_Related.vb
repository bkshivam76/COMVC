Imports System.Data

Namespace Real
#Region "--Profile--"
    <Serializable>
    Public Class ServiceableSouls

        ''' <summary>
        ''' Gets List of Souls
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_ServiceableSouls_GetListOfSouls</remarks>
        Public Shared Function GetListOfSouls(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select C_TITLE + ' ' + C_NAME as Name, C_BK_TITLE AS 'BK Title',C_DOB_L AS 'Lokik Date of Birth',C_DOB_A as 'Alokik Date of Birth',C_BK_PAD_NO as  'PAD No',C_MOB_NO_1 as 'Mobile No' from ADDRESS_BOOK WHERE (C_BK_TITLE = 'Teacher' OR C_BK_TITLE =  'Surrender Kumar' OR C_BK_TITLE =  'Surrender Sister') AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  AND  REC_STATUS IN (0,1,2)"
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, OnlineQuery, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function
    End Class

    <Serializable>
    Public Class Students

        ''' <summary>
        ''' Gets List Of Students
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Students_GetListOfStudents</remarks>
        Public Shared Function GetListOfStudents(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select C_TITLE + ' ' + C_NAME as Name,C_OCCUPATION as Occupation, C_BK_TITLE AS 'BK Title',dbo.fn_FORMATDATE(C_DOB_L,'dd-MON-yyyy') AS 'Lokik Date of Birth',dbo.fn_FORMATDATE(C_DOB_A,'dd-MON-yyyy') as 'Alokik Date of Birth',C_MOB_NO_1 as 'Mobile No' from ADDRESS_BOOK WHERE (C_BK_TITLE NOT IN ('Teacher','Surrender Kumar','Surrender Sister')) AND  C_STATUS ='B.K.'  AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND  REC_STATUS  IN (0,1,2)"
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, OnlineQuery, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function
    End Class
#End Region

#Region "Facility"
    <Serializable>
    Public Class Addresses
#Region "Get/Input/Update Parameter Classes"
        <Serializable>
        Public Class Param_MergeParties
            Public Merged_AB_ID As String
            Public Target_AB_ID As String
            Public MergeInNextYearToo As Boolean = False
        End Class
        <Serializable>
        Public Class Param_GetAddressUsageCount
            Public TableName As ConnectOneWS.Tables
            Public AB_Rec_ID As String
        End Class
        'Public Class Param_GetDuplicateCount
        '    Public Name As String
        '    Public PAN As String
        '    Public Passport As String
        '    Public VAT_TIN As String
        '    Public CST_TIN As String
        '    Public TAN As String
        '    Public UID As String
        '    Public STR_NO As String
        '    Public RecID As String
        'End Class
        <Serializable>
        Public Class Param_GetAddressRecID
            Public AB_RecID As String
            Public YearID As String
        End Class
        <Serializable>
        Public Class Param_GetAddressesForLabels
            Public rbt_CityStyle1_Checked As Boolean
            Public rbt_CityStyle2_Checked As Boolean
            Public rbt_CityStyle3_Checked As Boolean
            Public FilterBy As String
            Public FilterValue As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Addresses
            Public Title As String
            Public Name As String
            Public Gender As String
            Public OrgName As String
            Public Designation As String
            Public Education As String
            Public OccupationID As String
            Public Reference As String
            Public Remarks1 As String
            Public Remarks2 As String
            Public LokikDob As String
            Public BloodGroup As String
            Public ContactModeID As String
            Public PANNo As String
            Public VAT_TIN As String
            Public CST_TIN As String
            Public GST_TIN As String
            Public TAN As String
            Public UID As String
            Public STRNo As String
            Public VoterID As String
            Public RationCardNo As String
            Public DLNo As String
            Public TaxpayerID As String
            Public PassportNo As String
            Public Magazine As String
            Public Res_Add1 As String
            Public Res_Add2 As String
            Public Res_Add3 As String
            Public Res_Add4 As String
            Public Res_cityID As String
            Public Res_city As String
            Public Res_StateID As String
            Public Res_DisttID As String
            Public Res_CountryID As String
            Public Res_PinCode As String
            Public Off_Add1 As String
            Public Off_Add2 As String
            Public Off_Add3 As String
            Public Off_Add4 As String
            Public Off_CityID As String
            Public Off_StateID As String
            Public Off_DistID As String
            Public Off_CountryID As String
            Public Off_PinCode As String
            Public ResTel1 As String
            Public ResTel2 As String
            Public ResFax1 As String
            Public ResFax2 As String
            Public OffTel1 As String
            Public OffTel2 As String
            Public OffFax1 As String
            Public OffFax2 As String
            Public Mob1 As String
            Public Mob2 As String
            Public Email1 As String
            Public Email2 As String
            Public Website As String
            Public SkypeID As String
            Public FaceBookID As String
            Public TwitterID As String
            Public GtalkID As String
            Public Status As String
            Public ContactDate As String
            Public AlokikDOB As String
            Public BKTitle As String
            Public PADNo As String
            Public ClassAt As String
            Public CenCategory As Integer
            Public ClassCID As String
            Public ClassAdd1 As String
            Public Category As String
            Public Category_Other As String
            Public WingsMember As String
            Public Specialities As String
            Public Special_Other As String
            Public Events As String
            Public EventsOther As String
            Public Status_Action As String
            Public Rec_ID As String
            Public OrgAB_RecId As String
            Public YearID As Integer
            Public SubCityID As String
            Public DateOfSurr As DateTime
            Public AccountingParty As Boolean = False
        End Class
        <Serializable>
        Public Class Parameter_Update_Addresses
            Public Title As String
            Public Name As String
            Public Gender As String
            Public OrgName As String
            Public Designation As String
            Public Education As String
            Public OccupationID As String
            Public Reference As String
            Public Remarks1 As String
            Public Remarks2 As String
            Public LokikDob As String
            Public BloodGroup As String
            Public ContactModeID As String
            Public PANNo As String
            Public VAT_TIN As String
            Public CST_TIN As String
            Public GST_TIN As String
            Public TAN As String
            Public UID As String
            Public STRNo As String
            Public PassportNo As String
            Public VoterID As String
            Public RationCardNo As String
            Public DLNo As String
            Public TaxpayerID As String
            Public Magazine As String
            Public Res_Add1 As String
            Public Res_Add2 As String
            Public Res_Add3 As String
            Public Res_Add4 As String
            Public Res_cityID As String
            Public Res_city As String
            Public Res_StateID As String
            Public Res_DisttID As String
            Public Res_CountryID As String
            Public Res_PinCode As String
            Public Off_Add1 As String
            Public Off_Add2 As String
            Public Off_Add3 As String
            Public Off_Add4 As String
            Public Off_CityID As String
            Public Off_StateID As String
            Public Off_DistID As String
            Public Off_CountryID As String
            Public Off_PinCode As String
            Public ResTel1 As String
            Public ResTel2 As String
            Public ResFax1 As String
            Public ResFax2 As String
            Public OffTel1 As String
            Public OffTel2 As String
            Public OffFax1 As String
            Public OffFax2 As String
            Public Mob1 As String
            Public Mob2 As String
            Public Email1 As String
            Public Email2 As String
            Public Website As String
            Public SkypeID As String
            Public FaceBookID As String
            Public TwitterID As String
            Public GtalkID As String
            Public Status As String
            Public ContactDate As String
            Public AlokikDOB As String
            Public BKTitle As String
            Public PADNo As String
            Public ClassAt As String
            Public CenCategory As Integer
            Public ClassCID As String
            Public ClassAdd1 As String
            Public Category As String
            Public Category_Other As String
            Public WingsMember As String
            Public Specialities As String
            Public Special_Other As String
            Public Events As String
            Public EventsOther As String
            'Public Status_Action As String
            Public Rec_ID As String
            Public YearID As Integer = Nothing
            Public ReplicationUpdate As Boolean = False
            Public SubCityID As String
            Public DateOfSurr As DateTime
            Public OrgRecID As String
            Public AccountingParty As Boolean = False
        End Class
        <Serializable>
        Public Class Parameter_InsertMagazine_Addresses
            Public AB_Rec_ID As String
            Public Magazine_Misc_ID As String
            Public Status_Action As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertWings_Addresses
            Public AB_Rec_ID As String
            Public Wings_Misc_ID As String
            Public Status_Action As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertSpecialities_Addresses
            Public AB_Rec_ID As String
            Public Specialities_Misc_ID As String
            Public Status_Action As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertEvents_Addresses
            Public AB_Rec_ID As String
            Public Events_Misc_ID As String
            Public Status_Action As String
            Public Rec_ID As String
        End Class

        Public Class Parameter_InsertQuery_ExcelRawDataUpload
            Public query As String
            Public RecID As String
            Public table_name As String
        End Class
        <Serializable>
        Public Class Parameter_Addresses_GetList_Common
            Public Type As String
            Public SearchCondition As String
            Public SearchStr As String
            Public Use_Rec_ID As Boolean
            Public NameColHead As String
            Public IdColHead As String
            Public AB_IDs As String
            Public IndianOnly As Boolean
            Public PartyIDs As String
            Public Signatories_RecID As String
            Public Party_Rec_ID As String
            Public Member_Rec_Id As String
            Public ShowAccountingPartyOnly? As Boolean = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_Addresses
            Public param_InsertAddresses As Parameter_Insert_Addresses
            Public param_InsertAddresses_NextYear As Parameter_Insert_Addresses
            Public InsertMagazine() As Parameter_InsertMagazine_Addresses = Nothing
            Public InsertMagazine_NextYear() As Parameter_InsertMagazine_Addresses = Nothing
            Public InsertWings() As Parameter_InsertWings_Addresses = Nothing
            Public InsertWings_NextYear() As Parameter_InsertWings_Addresses = Nothing
            Public InsertSpecialities() As Parameter_InsertSpecialities_Addresses = Nothing
            Public InsertSpecialities_NextYear() As Parameter_InsertSpecialities_Addresses = Nothing
            Public InsertEvents() As Parameter_InsertEvents_Addresses = Nothing
            Public InsertEvents_NextYear() As Parameter_InsertEvents_Addresses = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Update_Addresses
            Public param_UpdateAddresses As Parameter_Update_Addresses
            Public param_UpdateAddresses_NextYear As Parameter_Update_Addresses
            Public RecID_DeleteMagazine As String = Nothing
            Public RecID_DeleteMagazine_NextYear As String = Nothing
            Public InsertMagazine() As Parameter_InsertMagazine_Addresses = Nothing
            Public InsertMagazine_NextYear() As Parameter_InsertMagazine_Addresses = Nothing
            Public RecID_DelteWings As String = Nothing
            Public RecID_DelteWings_NextYear As String = Nothing
            Public InsertWings() As Parameter_InsertWings_Addresses = Nothing
            Public InsertWings_NextYear() As Parameter_InsertWings_Addresses = Nothing
            Public RecID_DeleteSpeciality As String = Nothing
            Public RecID_DeleteSpeciality_NextYear As String = Nothing
            Public InsertSpecialities() As Parameter_InsertSpecialities_Addresses = Nothing
            Public InsertSpecialities_NextYear() As Parameter_InsertSpecialities_Addresses = Nothing
            Public RecID_DeleteAdditionalAddress As String = Nothing
            Public RecID_DeleteAdditionalAddress_NextYear As String = Nothing
            Public param_AdditionalAddress As Param_Insert_additional_info_address = Nothing
            Public param_AdditionalAddress_NextYear As Param_Insert_additional_info_address = Nothing
            Public RecID_DeleteEvents As String = Nothing
            Public RecID_DeleteEvents_NextYear As String = Nothing
            Public InsertEvents() As Parameter_InsertEvents_Addresses = Nothing
            Public InsertEvents_NextYear() As Parameter_InsertEvents_Addresses = Nothing
        End Class
        <Serializable>
        Public Class Param_Insert_additional_info_address
            Public Mobile3 As String
            Public Mobile4 As String
            Public Mobile5 As String
            Public Email3 As String
            Public Email4 As String
            Public Email5 As String
            Public Rec_ID As String
            Public Status_Action As String
            Public AB_Rec_ID As String
            Public File As Byte()
            Public File_Name As String
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_Addresses
            Public DeleteAddressSets() As Param_Txn_Delete_AddressSet = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_AddressSet
            Public RecID_DeleteMagazine As String = Nothing
            Public RecID_DelteWings As String = Nothing
            Public RecID_DeleteSpeciality As String = Nothing
            Public RecID_DeleteEvents As String = Nothing
            Public RecID_Delete As String = Nothing
        End Class
        <Serializable>
        Public Class Param_Get_Duplicates
            Public insertPAram As Parameter_Insert_Addresses = Nothing
            Public updatePAram As Parameter_Update_Addresses = Nothing
            Public Rec_ID As String = ""
        End Class
#End Region
        Public Shared Function MergeParties(inParam As Param_MergeParties, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Txn_Merge_Party"
            Dim params() As String = {"@MERGED_AB_ID", "@TARGET_AB_ID", "@MergeInNextYearToo"}
            Dim values() As Object = {inParam.Merged_AB_ID, inParam.Target_AB_ID, inParam.MergeInNextYearToo}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Boolean}
            Dim lengths() As Integer = {36, 36, 1}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.ADDRESS_BOOK, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        ''' <summary>
        ''' Returns Address list, Common function
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetList_Common</remarks>
        Public Shared Function GetList_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal param As Parameter_Addresses_GetList_Common = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Dim AccPartyOnly As String = ""
            If Not param Is Nothing Then
                If param.ShowAccountingPartyOnly.HasValue Then
                    If param.ShowAccountingPartyOnly Then
                        AccPartyOnly = " AND COALESCE(C_ACC_PARTY,0) = 1 "
                    Else
                        AccPartyOnly = " AND COALESCE(C_ACC_PARTY,0) = 0 "
                    End If
                End If
            End If
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Profile_BankAccounts
                    Query = "select  C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS Name,C_ORG_NAME  AS Organization,REC_ID AS ID, REC_EDIT_ON  from ADDRESS_BOOK   WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID = " & inBasicParam.openCenID.ToString & " AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & " ORDER BY C_NAME "
                    If Not param.Signatories_RecID Is Nothing Then 'added for multi user check 
                        Query = "select  C_NAME AS Name,C_ORG_NAME  AS Organization,REC_ID AS ID,REC_EDIT_ON  from ADDRESS_BOOK   WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID = " & inBasicParam.openCenID.ToString & " And REC_ID = '" & param.Signatories_RecID & "' AND C_COD_YEAR_ID = " & inBasicParam.openYearID & " " & AccPartyOnly & ""
                    End If
                Case ConnectOneWS.ClientScreen.Profile_Core
                    Query = " SELECT C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS Name, REC_ID AS ID, REC_EDIT_ON FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  " & AccPartyOnly & ""
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & param.Party_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Profile_Advances
                    Query = " SELECT C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  as C_NAME,REC_ID AS C_ID, REC_EDIT_ON FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = '" & inBasicParam.openCenID & "' AND REC_ID IN (" & param.PartyIDs & ")  " & AccPartyOnly & ""
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multi user check
                        Query = " SELECT C_NAME,REC_ID AS C_ID, REC_EDIT_ON FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & " And REC_ID = '" & param.Party_Rec_ID & "' " & AccPartyOnly & ""
                    End If
                Case ConnectOneWS.ClientScreen.Profile_Deposit
                    Query = " SELECT C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END AS C_NAME ,REC_ID AS C_ID FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND REC_ID IN (" & param.PartyIDs & ")  " & AccPartyOnly & ""
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_CollectionBox
                    Query = " SELECT C_NAME  + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS C_NAME,C_OCCUPATION ,REC_ID AS C_ID, REC_EDIT_ON FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND (C_BK_TITLE = 'Teacher' OR C_BK_TITLE =  'Surrender Kumar' OR C_BK_TITLE =  'Surrender Sister' OR C_BK_TITLE ='Trialkumar' OR C_BK_TITLE ='Trialkumari' OR C_BK_TITLE ='Adharkumari')  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "   " & AccPartyOnly & ""
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And REC_ID = '" & param.Party_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Donation
                    If param.IndianOnly Then
                        Query = " SELECT A.C_NAME + CASE WHEN A.C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(A.C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END C_NAME,A.C_PAN_NO,A.C_PASSPORT_NO,A.C_R_ADD1,A.C_R_ADD2,A.C_R_ADD3,A.C_R_ADD4,A.C_R_PINCODE,A.C_UID_NO,A.C_TAX_ID_NO,CT.CI_NAME, ST.ST_NAME, DI.DI_NAME, CO.CO_NAME,A.REC_ID AS C_ID, A.REC_EDIT_ON, " &
                                " CASE WHEN LEN(COALESCE(A.C_PASSPORT_NO,'')) > 0 THEN A.C_PASSPORT_NO " &
                                " WHEN LEN(COALESCE(A.C_DL_NO,'')) > 0 THEN A.C_DL_NO " &
                                " WHEN LEN(COALESCE(A.C_VOTER_ID_NO,'')) > 0 THEN A.C_VOTER_ID_NO" &
                                " WHEN LEN(COALESCE(A.C_RATION_NO,'')) > 0 THEN A.C_RATION_NO " &
                                " ELSE '' END AS C_OTHER_ID," &
                                " CASE WHEN LEN(COALESCE(A.C_PASSPORT_NO,'')) > 0 THEN 'Passport No.:' " &
                                " WHEN LEN(COALESCE(A.C_DL_NO,'')) > 0 THEN 'DL No.:' " &
                                " WHEN LEN(COALESCE(A.C_VOTER_ID_NO,'')) > 0 THEN 'Voter ID:'" &
                                " WHEN LEN(COALESCE(A.C_RATION_NO,'')) > 0 THEN 'Ration No.:' " &
                                " ELSE 'Passport No.:' END AS C_OTHER_ID_LABEL " &
                                " FROM ADDRESS_BOOK               AS A    " &
                                " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID           AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_DISTRICT_INFO     AS DI 	ON (A.C_R_DISTRICT_ID = DI.REC_ID           AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (A.C_R_COUNTRY_ID  = CO.REC_ID           AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" &
                                "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND  C_R_COUNTRY_ID='f9970249-121c-4b8f-86f9-2b53e850809e' AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                    Else
                        Query = " SELECT A.C_NAME  + CASE WHEN A.C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(A.C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END C_NAME ,A.C_PAN_NO,A.C_PASSPORT_NO,A.C_R_ADD1,A.C_R_ADD2,A.C_R_ADD3,A.C_R_ADD4,A.C_R_PINCODE,A.C_UID_NO,A.C_TAX_ID_NO,CT.CI_NAME, ST.ST_NAME, DI.DI_NAME, CO.CO_NAME,A.REC_ID AS C_ID, A.REC_EDIT_ON, " &
                                " CASE WHEN LEN(COALESCE(A.C_PASSPORT_NO,'')) > 0 THEN A.C_PASSPORT_NO " &
                                " WHEN LEN(COALESCE(A.C_DL_NO,'')) > 0 THEN A.C_DL_NO " &
                                " WHEN LEN(COALESCE(A.C_VOTER_ID_NO,'')) > 0 THEN A.C_VOTER_ID_NO" &
                                " WHEN LEN(COALESCE(A.C_RATION_NO,'')) > 0 THEN A.C_RATION_NO " &
                                " ELSE '' END AS C_OTHER_ID," &
                                " CASE WHEN LEN(COALESCE(A.C_PASSPORT_NO,'')) > 0 THEN 'Passport No.:' " &
                                " WHEN LEN(COALESCE(A.C_DL_NO,'')) > 0 THEN 'DL No.:' " &
                                " WHEN LEN(COALESCE(A.C_VOTER_ID_NO,'')) > 0 THEN 'Voter ID:'" &
                                " WHEN LEN(COALESCE(A.C_RATION_NO,'')) > 0 THEN 'Ration No.:' " &
                                " ELSE 'Passport No.:' END AS C_OTHER_ID_LABEL , C_MOB_NO_1 as MOB " &
                                " FROM ADDRESS_BOOK               AS A    " &
                                " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID           AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_DISTRICT_INFO     AS DI 	ON (A.C_R_DISTRICT_ID = DI.REC_ID           AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (A.C_R_COUNTRY_ID  = CO.REC_ID           AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" &
                                "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                    End If
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And A.REC_ID = '" & param.Party_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Property
                    Query = " SELECT C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS Name,C_ORG_NAME  AS Organization,C_STATUS AS Status,REC_ID AS ID FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""

                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    Query = " SELECT C_NAME  AS C_NAME,REC_ID AS C_ID FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND REC_ID IN (" & param.AB_IDs & ") " & AccPartyOnly & ""
                Case ConnectOneWS.ClientScreen.Profile_Vehicles
                    Query = "select  C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS Name,C_ORG_NAME  AS Organization,C_STATUS AS Status,REC_ID AS ID, REC_EDIT_ON  from ADDRESS_BOOK   WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID & "" & AccPartyOnly & "  ORDER BY C_NAME "
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multi user check 
                        Query = "select  C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS Name,C_ORG_NAME  AS Organization,C_STATUS AS Status,REC_ID AS ID, REC_EDIT_ON  from ADDRESS_BOOK   WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID = " & inBasicParam.openCenID.ToString & " AND REC_ID = '" & param.Party_Rec_ID & "'" & AccPartyOnly & "  ORDER BY C_NAME "
                    End If
                Case ConnectOneWS.ClientScreen.Profile_ServicePlaces
                    Query = " SELECT C_TITLE + ' ' + C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END as Name , C_R_ADD1 AS BUILDING, C_R_ADD2 AS 'HOUSE NO',C_R_ADD3 AS 'AREA/STREET' ," & _
                                         "C_R_ADD4 AS DISTRICT,C_MOB_NO_1 as MOBILE,  REC_ID as ID, REC_EDIT_ON FROM ADDRESS_BOOK WHERE  C_CEN_ID = " & inBasicParam.openCenID.ToString & " AND REC_STATUS IN (0,1,2) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " AND REC_ID = '" & param.Party_Rec_ID & "' "
                    End If
                    Query += " ORDER BY C_TITLE + ' ' + C_NAME "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    If param.Type.ToUpper() = "PARTY" Then
                        Query = " SELECT A.C_NAME  + CASE WHEN A.C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(A.C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END C_NAME,A.C_PAN_NO,A.C_PASSPORT_NO,A.C_R_ADD1,A.C_R_ADD2,A.C_R_ADD3,A.C_R_ADD4,A.C_R_PINCODE,A.C_UID_NO,CT.CI_NAME, ST.ST_NAME, DI.DI_NAME, CO.CO_NAME,A.REC_ID AS C_ID, " &
                                  " CASE WHEN LEN(COALESCE(A.C_PASSPORT_NO,'')) > 0 THEN A.C_PASSPORT_NO " &
                                  " WHEN LEN(COALESCE(A.C_DL_NO,'')) > 0 THEN A.C_DL_NO " &
                                  " WHEN LEN(COALESCE(A.C_VOTER_ID_NO,'')) > 0 THEN A.C_VOTER_ID_NO" &
                                  " WHEN LEN(COALESCE(A.C_RATION_NO,'')) > 0 THEN A.C_RATION_NO " &
                                  " ELSE '' END AS C_OTHER_ID," &
                                  " CASE WHEN LEN(COALESCE(A.C_PASSPORT_NO,'')) > 0 THEN 'Passport No.:' " &
                                  " WHEN LEN(COALESCE(A.C_DL_NO,'')) > 0 THEN 'DL No.:' " &
                                  " WHEN LEN(COALESCE(A.C_VOTER_ID_NO,'')) > 0 THEN 'Voter ID:'" &
                                  " WHEN LEN(COALESCE(A.C_RATION_NO,'')) > 0 THEN 'Ration No.:' " &
                                  " ELSE 'Passport No.:' END AS C_OTHER_ID_LABEL," &
                                  " COALESCE(NULLIF(C_CATEGORY,''),'') AS C_CATEGORY" &
                                  " FROM ADDRESS_BOOK               AS A    " &
                                  " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                  " LEFT JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID           AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                  " LEFT JOIN MAP_DISTRICT_INFO     AS DI 	ON (A.C_R_DISTRICT_ID = DI.REC_ID           AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                  " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (A.C_R_COUNTRY_ID  = CO.REC_ID           AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                  " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" &
                                  "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                    Else
                        Query = " select  C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS Name,C_ORG_NAME  AS Organization,C_STATUS AS Status,REC_ID AS ID  from ADDRESS_BOOK   WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "   " & AccPartyOnly & ""
                    End If
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And A.REC_ID = '" & param.Party_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = " SELECT A.C_NAME  + CASE WHEN A.C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(A.C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END C_NAME ,A.C_PAN_NO,CT.CI_NAME as 'C_CITY',A.REC_ID AS C_ID, A.REC_EDIT_ON " & _
                                  " FROM ADDRESS_BOOK               AS A    " & _
                                  " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                  "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multi user check
                        Query += " And A.REC_ID = '" & param.Party_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Profile_Membership
                    If param.Use_Rec_ID = True Then
                        param.SearchCondition = " ( A.C_ORG_REC_ID LIKE '%" & param.SearchStr & "%' AND C_COD_YEAR_ID = " & inBasicParam.openYearID & ")"
                    Else
                        param.SearchCondition = " ( A.C_NAME LIKE '%" & param.SearchStr & "%' " & _
                                          "   OR  CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END LIKE '%" & param.SearchStr & "%' " & _
                                          "   OR  CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END LIKE '%" & param.SearchStr & "%' " & _
                                          "  ) "
                    End If
                    Query = " SELECT A.C_NAME + CASE WHEN A.C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(A.C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  C_NAME,A.C_R_ADD1,A.C_R_ADD2,A.C_R_ADD3,A.C_R_ADD4,A.C_R_PINCODE,CT.CI_NAME, ST.ST_NAME, DI.DI_NAME, CO.CO_NAME,A.C_EDUCATION,MI.MISC_NAME  AS C_OCCUPATION,CAST(A.C_DOB_L AS DATE) AS C_DOB, ROUND ( (DATEDIFF(dd,CAST(GETDATE()AS DATE),A.C_DOB_L)/365),0) AS C_AGE  " & _
                                          " ,CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0 THEN A.C_TEL_NO_R_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0  AND  LEN(COALESCE(A.C_TEL_NO_R_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(coalesce(A.C_TEL_NO_R_2,'')) > 0 THEN A.C_TEL_NO_R_2 ELSE '' END  AS TEL_NOS " & _
                                          " ,CASE WHEN LEN(COALESCE(A.C_MOB_NO_1  ,'')) > 0 THEN A.C_MOB_NO_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_MOB_NO_1 ,'')) > 0  AND  LEN(COALESCE(A.C_MOB_NO_2  ,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_MOB_NO_2 ,'')) > 0 THEN A.C_MOB_NO_2  ELSE '' END  AS MOB_NOS  " & _
                                          " ,CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0 THEN A.C_EMAIL_ID_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0  AND  LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0 THEN A.C_EMAIL_ID_2 ELSE '' END  AS EMAILS   " & _
                                          " ,A.C_CEN_CATEGORY,A.C_CLASS_CEN_ID,CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END AS 'CEN_NAME',CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END AS 'CEN_UID',A.REC_ID AS C_ID,A.C_ORG_REC_ID AS C_ORG_ID, A.REC_EDIT_ON AS C_REC_EDIT_ON  " & _
                                          " FROM ADDRESS_BOOK               AS A    " & _
                                          " LEFT JOIN CENTRE_INFO           AS A_C    ON (A_C.CEN_ID          = A.C_CEN_ID AND A_C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN CENTRE_INFO           AS C    ON (C.CEN_ID          = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN OVERSEAS_CENTRE_INFO  AS O    ON (O.CEN_ID          = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID           AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MAP_DISTRICT_INFO     AS DI 	ON (A.C_R_DISTRICT_ID = DI.REC_ID           AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (A.C_R_COUNTRY_ID  = CO.REC_ID           AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MISC_INFO             AS MI   ON (A.C_OCCUPATION_ID = MI.REC_ID           AND MI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                          "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND  (A.C_STATUS='B.K.' OR A_C.CEN_INS_ID = '00008')  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & _
                                          "   AND " & param.SearchCondition & " " & _
                                          " ORDER BY A.C_NAME "
                    If Not param.Member_Rec_Id Is Nothing Then
                        Query = " SELECT A.C_NAME  + CASE WHEN A.C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(A. C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  C_NAME,A.C_R_ADD1,A.C_R_ADD2,A.C_R_ADD3,A.C_R_ADD4,A.C_R_PINCODE,CT.CI_NAME, ST.ST_NAME, DI.DI_NAME, CO.CO_NAME,A.C_EDUCATION,MI.MISC_NAME  AS C_OCCUPATION,CAST(A.C_DOB_L AS DATE) AS C_DOB, ROUND ( (DATEDIFF(dd,CAST(GETDATE()AS DATE),A.C_DOB_L)/365),0) AS C_AGE  " & _
                                          " ,CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0 THEN A.C_TEL_NO_R_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0  AND  LEN(COALESCE(A.C_TEL_NO_R_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(coalesce(A.C_TEL_NO_R_2,'')) > 0 THEN A.C_TEL_NO_R_2 ELSE '' END  AS TEL_NOS " & _
                                          " ,CASE WHEN LEN(COALESCE(A.C_MOB_NO_1  ,'')) > 0 THEN A.C_MOB_NO_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_MOB_NO_1 ,'')) > 0  AND  LEN(COALESCE(A.C_MOB_NO_2  ,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_MOB_NO_2 ,'')) > 0 THEN A.C_MOB_NO_2  ELSE '' END  AS MOB_NOS  " & _
                                          " ,CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0 THEN A.C_EMAIL_ID_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0  AND  LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0 THEN A.C_EMAIL_ID_2 ELSE '' END  AS EMAILS   " & _
                                          " ,A.C_CEN_CATEGORY,A.C_CLASS_CEN_ID,CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END AS 'CEN_NAME',CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END AS 'CEN_UID',A.REC_ID AS C_ID,A.C_ORG_REC_ID AS C_ORG_ID, A.REC_EDIT_ON AS C_REC_EDIT_ON  " & _
                                          " FROM ADDRESS_BOOK               AS A    " & _
                                          " LEFT JOIN CENTRE_INFO           AS A_C    ON (A_C.CEN_ID          = A.C_CEN_ID AND A_C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN CENTRE_INFO           AS C    ON (C.CEN_ID          = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN OVERSEAS_CENTRE_INFO  AS O    ON (O.CEN_ID          = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID           AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MAP_DISTRICT_INFO     AS DI 	ON (A.C_R_DISTRICT_ID = DI.REC_ID           AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (A.C_R_COUNTRY_ID  = CO.REC_ID           AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " LEFT JOIN MISC_INFO             AS MI   ON (A.C_OCCUPATION_ID = MI.REC_ID           AND MI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                          " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                          "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND  (A.C_STATUS='B.K.' OR A_C.CEN_INS_ID = '00008')  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & _
                                          "   AND " & param.SearchCondition & " And A.C_ORG_REC_ID  = '" & param.Member_Rec_Id & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    If param.Type.ToUpper() = "PARTY" Then
                        Query = " SELECT A.C_NAME  + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  C_NAME ,A.C_PAN_NO, A.C_UID_NO AS C_AADHAR_NO,CT.CI_NAME as 'C_CITY',COALESCE(A.C_CATEGORY,'') CATEGORY, A.REC_ID AS C_ID,A.REC_EDIT_ON " &
                                  " FROM ADDRESS_BOOK               AS A    " &
                                  " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                  " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" &
                                  "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                        If Not param.Party_Rec_ID Is Nothing Then 'added for multiuser check
                            Query += " AND A.REC_ID = '" & param.Party_Rec_ID & "' "
                        End If
                    Else
                        Query = " SELECT C_NAME  + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  as " & param.NameColHead & ", C_PAN_NO as PAN, C_UID_NO AS Aadhar,REC_ID AS " & param.IdColHead & " FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & "   AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Query = " SELECT C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  as " & param.NameColHead & ", C_PAN_NO as PAN,C_UID_NO,C_GST_TIN_NO as GST,CT.CI_NAME as City, ST.ST_NAME as State,A.REC_ID AS " & param.IdColHead & ", A.REC_EDIT_ON," &
                        " COALESCE(NULLIF(C_CATEGORY,''),'') AS C_CATEGORY," &
                     " CASE WHEN LEN(COALESCE(C_PASSPORT_NO,'')) > 0 THEN C_PASSPORT_NO " &
                                " WHEN LEN(COALESCE(C_DL_NO,'')) > 0 THEN C_DL_NO " &
                                " WHEN LEN(COALESCE(C_VOTER_ID_NO,'')) > 0 THEN C_VOTER_ID_NO" &
                                " WHEN LEN(COALESCE(C_RATION_NO,'')) > 0 THEN C_RATION_NO " &
                                " ELSE '' END AS C_OTHER_ID," &
                                " CASE WHEN LEN(COALESCE(C_PASSPORT_NO,'')) > 0 THEN 'Passport No.:' " &
                                " WHEN LEN(COALESCE(C_DL_NO,'')) > 0 THEN 'DL No.:' " &
                                " WHEN LEN(COALESCE(C_VOTER_ID_NO,'')) > 0 THEN 'Voter ID:'" &
                                " WHEN LEN(COALESCE(C_RATION_NO,'')) > 0 THEN 'Ration No.:' " &
                                " ELSE 'Passport No.:' END AS C_OTHER_ID_LABEL, " &
                                " CASE WHEN LEN(COALESCE(A.C_MOB_NO_1,'')) > 0 THEN A.C_MOB_NO_1 WHEN LEN(COALESCE(A.C_MOB_NO_2 ,'')) > 0 THEN A.C_MOB_NO_2 ELSE '' END as Mobile," &
                                " CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0 THEN A.C_EMAIL_ID_1 WHEN LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0 THEN A.C_EMAIL_ID_2 ELSE '' END as Email " &
                    "FROM ADDRESS_BOOK AS A " &
                    "LEFT JOIN MAP_CITY_INFO  AS CT ON A.C_R_CITY_ID = CT.REC_ID " &
                    "LEFT JOIN MAP_STATE_INFO AS ST ON A.C_R_STATE_ID    = ST.REC_ID " &
                    "WHERE  A.REC_STATUS IN (0,1,2) And C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  And C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "  " & AccPartyOnly & ""
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " And A.REC_ID= '" & param.Party_Rec_ID & "'"
                    End If
                Case ConnectOneWS.ClientScreen.Report_Collection_Box
                    Query = "Select COALESCE(ab1.C_TITLE,'') + ' ' + ab1.C_NAME AS name, REC_ID from ADDRESS_BOOK ab1 where C_CEN_ID =" & inBasicParam.openCenID.ToString & " AND  REC_STATUS IN (0,1,2) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                Case ConnectOneWS.ClientScreen.Report_Collection_Box_Voucher
                    Query = "Select COALESCE(ab1.C_TITLE,'') + ' ' + ab1.C_NAME AS name, REC_ID from ADDRESS_BOOK ab1 where REC_STATUS IN (0,1,2) AND REC_ID IN (" & param.AB_IDs & ") AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    Query = " SELECT A.C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END as C_NAME,A.C_PAN_NO,CT.CI_NAME as 'C_CITY',A.REC_ID AS C_ID,A.REC_EDIT_ON " & _
                                  " FROM ADDRESS_BOOK               AS A    " & _
                                  " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                  " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                  "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                    If Not param.Party_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND A.REC_ID = '" & param.Party_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Stock_Personnel_Master
                    Query = "Select CONCAT(NULLIF(ab1.C_TITLE + ' ','') ,ab1.C_NAME)  AS Name, C_GENDER as Gender, C_DOB_L as DOB, C_UID_NO as Aadhar_No, C_PAN_NO as Pan_No, REC_ID AS ID, CASE WHEN LEN(COALESCE(ab1.C_MOB_NO_1  ,'')) > 0 THEN ab1.C_MOB_NO_1 ELSE '' END + CASE WHEN LEN(COALESCE(ab1.C_MOB_NO_1 ,'')) > 0  AND  LEN(COALESCE(ab1.C_MOB_NO_2  ,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(ab1.C_MOB_NO_2 ,'')) > 0 THEN ab1.C_MOB_NO_2  ELSE '' END  AS Mobile_No   from ADDRESS_BOOK ab1 where C_CEN_ID =" & inBasicParam.openCenID.ToString & " AND  REC_STATUS IN (0,1,2) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " "
                Case ConnectOneWS.ClientScreen.Stock_Supplier_Master
                    Query = "SELECT CONCAT(NULLIF(A.C_TITLE + ' ',''), A.C_NAME) + CASE WHEN A.C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(A.C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END Name ,A.C_PAN_NO Pan_No,COALESCE(A.C_R_ADD1 ,'')  + CASE WHEN LEN(COALESCE(A.C_R_ADD2,'')+COALESCE(A.C_R_ADD3,'')+COALESCE(A.C_R_ADD4,''))>0 THEN ', ' ELSE '' END	+ COALESCE(A.C_R_ADD2,'') + CASE WHEN LEN(COALESCE(A.C_R_ADD3,'')+COALESCE(A.C_R_ADD4,''))>0 THEN ', ' ELSE '' END 	+ COALESCE(A.C_R_ADD3,'') + CASE WHEN LEN(COALESCE(A.C_R_ADD4,''))>0 THEN ', ' ELSE '' END 	+ COALESCE(A.C_R_ADD4,'') AS Address,CT.CI_NAME City, ST.ST_NAME State, CO.CO_NAME as Country,A.REC_ID AS ID " & " ,CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0 THEN A.C_TEL_NO_R_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0  AND  LEN(COALESCE(A.C_TEL_NO_R_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(coalesce(A.C_TEL_NO_R_2,'')) > 0 THEN A.C_TEL_NO_R_2 ELSE '' END  + ',' " &
                            " ,CASE WHEN LEN(COALESCE(A.C_MOB_NO_1  ,'')) > 0 THEN A.C_MOB_NO_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_MOB_NO_1 ,'')) > 0  AND  LEN(COALESCE(A.C_MOB_NO_2  ,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_MOB_NO_2 ,'')) > 0 THEN A.C_MOB_NO_2  ELSE '' END  AS ContactNo  " &
                            " ,CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0 THEN A.C_EMAIL_ID_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0  AND  LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0 THEN A.C_EMAIL_ID_2 ELSE '' END  AS Email   " &
                                " FROM ADDRESS_BOOK               AS A    " &
                                " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID           AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_DISTRICT_INFO     AS DI 	ON (A.C_R_DISTRICT_ID = DI.REC_ID           AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (A.C_R_COUNTRY_ID  = CO.REC_ID           AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                                " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" &
                                "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly & ""
                Case ConnectOneWS.ClientScreen.Facility_ServiceReport
                    Query = "select  CONCAT(NULLIF(C_TITLE + ' ',''), C_NAME) + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS NAME,A.REC_ID AS ID, A.REC_EDIT_ON,C_ORG_NAME" &
                            " ,CASE WHEN LEN(COALESCE(A.C_MOB_NO_1  ,'')) > 0 THEN A.C_MOB_NO_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_MOB_NO_1 ,'')) > 0  AND  LEN(COALESCE(A.C_MOB_NO_2  ,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_MOB_NO_2 ,'')) > 0 THEN A.C_MOB_NO_2  ELSE '' END  AS ContactNo  " &
                            " ,CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0 THEN A.C_EMAIL_ID_1 ELSE '' END + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_1,'')) > 0  AND  LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0  THEN ', ' ELSE '' END  + CASE WHEN LEN(COALESCE(A.C_EMAIL_ID_2,'')) > 0 THEN A.C_EMAIL_ID_2 ELSE '' END  AS Email   " &
                            " ,CASE WHEN LEN(COALESCE(A.C_REMARKS_1,'')) > 0 THEN A.C_REMARKS_1  WHEN LEN(COALESCE(A.C_REMARKS_2,'')) > 0  THEN A.C_REMARKS_2 ELSE '' END AS Remarks  " &
                            " ,CT.CI_NAME as 'C_CITY' " &
                    "from ADDRESS_BOOK AS A LEFT JOIN MAP_CITY_INFO AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID           AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" &
                    "WHERE  A.REC_STATUS IN (0,1,2) AND C_CEN_ID = " & inBasicParam.openCenID.ToString & " AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & AccPartyOnly
                    If String.IsNullOrWhiteSpace(param.Type) = False Then
                        If param.Type = "FACULTY" Then
                            Query = Query & " AND C_CATEGORY='FACULTY'"
                        End If
                        If param.Type = "EVENT ORGANIZER" Then
                            Query = Query & " AND C_CATEGORY='EVENT ORGANIZER'"
                        End If
                    End If

                    Query = Query & " ORDER BY NAME "
            End Select
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
            Return Nothing
        End Function

        Public Shared Function GetOrgList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = " SELECT DISTINCT C_ORG_NAME FROM address_book WHERE C_CEN_ID = " & inBasicParam.openCenID.ToString & " AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "" & _
                          " ORDER BY C_ORG_NAME"
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, OnlineQuery, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Address list
        ''' </summary>
        ''' <param name="Date_Format_Current"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetList</remarks>
        ''' This function is replaced by the function GetAddressBookListing in Common Project. To be deleted later. 
        Public Shared Function GetList(ShowAccPartyOnly As Boolean?, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim OnlineQuery As String = ""
            'Dim QueryAccParty As String = ""
            'If ShowAccPartyOnly.HasValue Then
            '    QueryAccParty = " AND COALESCE(C_ACC_PARTY,'FALSE') = '" & ShowAccPartyOnly.ToString & "' "
            'End If

            'OnlineQuery = " Select  A.C_TITLE AS Title,A.C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END AS Name,A.C_ORG_NAME  AS Organization,A.C_DESIGNATION AS Designation,A.C_EDUCATION AS Education, A.C_PASSPORT_NO AS 'Passport No.',A.C_PAN_NO AS 'PAN No.',C_VAT_TIN_NO as 'VAT TIN No.',C_GST_TIN_NO AS 'GSTIN No.',C_CST_TIN_NO AS 'CST TIN No.',A.C_TAN_NO AS 'TAN No.',A.C_UID_NO AS 'UID No.',A.C_STR_NO  as 'Service Tax Reg. No.'," &
            '                  " A.C_R_ADD1 AS 'Address Line.1',A.C_R_ADD2 AS 'Address Line.2',A.C_R_ADD3 AS 'Address Line.3',A.C_R_ADD4 AS 'Address Line.4',A.C_R_PINCODE AS 'Pincode',COALESCE(CT.CI_NAME,A.C_R_CITY) as 'City', ST.ST_NAME as 'State', DI.DI_NAME as 'District', CO.CO_NAME as 'Country'," &
            '                  " A.C_TEL_NO_R_1  + ' ' + A.C_TEL_NO_R_2 as 'Resi.Tel.No(s)', A.C_TEL_NO_O_1 + ' ' + A.C_TEL_NO_O_2  as 'Office Tel.No(s)',A.C_FAX_NO_O_1  + ' ' + A.C_FAX_NO_O_2 as 'Office Fax No(s)',A.C_FAX_NO_R_1  + ' ' + A.C_FAX_NO_R_2 as 'Resi.Fax No(s)',  A.C_MOB_NO_1  + ' ' + A.C_MOB_NO_2  as  'Mobile No(s)',A.C_EMAIL_ID_1 + ' ' + A.C_EMAIL_ID_2 as Email,A.C_WEBSITE as 'Website', " &
            '                  " A.C_DOB_L AS 'Date of Birth (Lokik)',A.C_BLOODGROUP AS 'Blood Group', CASE WHEN A.C_CON_DT IS NULL THEN A.C_STATUS ELSE A.C_STATUS + ' (Fr.: '+dbo.fn_FormatDate(C_CON_DT , 'dd-MM-yyyy')  + ')' END as Status,A.C_DOB_A AS 'Date of Birht (Alokik)',A.C_BK_TITLE AS 'BK Title',A.C_BK_PAD_NO AS 'BK PAD No.',A.C_CLASS_AT as 'Class At', " &
            '                  " CASE WHEN A.C_CEN_CATEGORY=0 Or COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN 'Indian Centre' ELSE 'Overseas Rajyoga Training Centre' END AS 'Centre Category',CASE WHEN COALESCE(A.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END AS 'Centre Name' , A.C_CATEGORY as Category,A.C_REF as Referene,A.C_REMARKS_1 +' | '+ A.C_REMARKS_2 as Remarks,(SUBSTRING(( select distinct ',' + ev.MISC_NAME  from address_book_events_info ce inner join misc_info as ev on ce.C_MISC_REC_ID = ev.rec_id and ce.c_rec_id = A.REC_ID FOR XML PATH('')),2,200000)) AS Events,  A.REC_ID AS ID  ," & Common.Rec_Detail("A") &
            '                  " From ADDRESS_BOOK               AS A    " &
            '                  " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID AND CT.REC_STATUS IN (0,1,2) ) " &
            '                  " LEFT JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID AND ST.REC_STATUS IN (0,1,2) ) " &
            '                  " LEFT JOIN MAP_DISTRICT_INFO     AS DI 	ON (A.C_R_DISTRICT_ID = DI.REC_ID AND DI.REC_STATUS IN (0,1,2) ) " &
            '                  " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (A.C_R_COUNTRY_ID  = CO.REC_ID AND CO.REC_STATUS IN (0,1,2) ) " &
            '                  " LEFT JOIN CENTRE_INFO           AS C    ON (A.C_CLASS_CEN_ID  = C.CEN_ID  AND A.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (0,1,2) )" &
            '                  " LEFT JOIN OVERSEAS_CENTRE_INFO  AS O    ON (A.C_CLASS_CEN_ID  = O.CEN_ID  AND A.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (0,1,2) )" &
            '                  " WHERE  A.REC_STATUS  IN (0,1,2) AND A.C_CEN_ID = " & inBasicParam.openCenID.ToString & " AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & QueryAccParty & " ORDER BY C_NAME"
            'Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, OnlineQuery, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)

            Dim SPName As String = "sp_get_Address_Book_Listing"
            Dim params() As String = {"CENID", "YEARID", "@ACC_PARTY", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, IIf(ShowAccPartyOnly.HasValue, True, False), inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 1, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ADDRESS_BOOK, SPName, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), params, values, dbTypes, lengths, inBasicParam)

        End Function

        ''' <summary>
        ''' Gets Duplicate count on basis of defined parameters 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetAddressUsageCount</remarks>
        Public Shared Function GetAddressUsageCount(ByVal Param As Param_GetAddressUsageCount, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim Query As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OrgABID As String = GetOrgAddressRecID(Param.AB_Rec_ID, inBasicParam)
            Select Case Param.TableName
                Case ConnectOneWS.Tables.TRANSACTION_INFO
                    Query = "SELECT CASE WHEN TR_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " THEN 0 ELSE 1 END SR, TR_COD_YEAR_ID AS YEARID, COUNT(REC_ID) AS CNT FROM TRANSACTION_INFO  WHERE REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( TR_AB_ID_1 IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') OR  TR_AB_ID_2  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') ) GROUP BY TR_COD_YEAR_ID  ORDER BY SR"
                Case ConnectOneWS.Tables.ADVANCES_INFO
                    Query = "SELECT CASE WHEN AI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " THEN 0 ELSE 1 END SR,  AI_COD_YEAR_ID AS YEARID, COUNT(REC_ID) AS CNT  FROM Advances_Info  WHERE REC_STATUS  IN (0,1,2) AND AI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( AI_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') )  GROUP BY AI_COD_YEAR_ID  ORDER BY SR"
                Case ConnectOneWS.Tables.DEPOSITS_INFO
                    Query = "SELECT CASE WHEN DI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " THEN 0 ELSE 1 END SR,  DI_COD_YEAR_ID AS YEARID, COUNT(REC_ID) AS CNT  FROM Deposits_Info  WHERE REC_STATUS  IN (0,1,2) AND DI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( DI_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') )  GROUP BY DI_COD_YEAR_ID  ORDER BY SR"
                Case ConnectOneWS.Tables.LIABILITIES_INFO
                    Query = "SELECT CASE WHEN LI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " THEN 0 ELSE 1 END SR,  LI_COD_YEAR_ID AS YEARID, COUNT(REC_ID) AS CNT  FROM Liabilities_Info  WHERE REC_STATUS  IN (0,1,2) AND LI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( LI_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') )  GROUP BY LI_COD_YEAR_ID  ORDER BY SR"
                Case ConnectOneWS.Tables.VEHICLES_INFO
                    Query = "SELECT CASE WHEN VI_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " THEN 0 ELSE 1 END SR,  VI_COD_YEAR_ID AS YEARID, COUNT(REC_ID) AS CNT  FROM Vehicles_Info  WHERE REC_STATUS  IN (0,1,2) AND VI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( VI_OWNERSHIP_AB_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') )  GROUP BY VI_COD_YEAR_ID  ORDER BY SR"
                Case ConnectOneWS.Tables.BANK_ACCOUNT_INFO
                    Query = "SELECT CASE WHEN BA_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " THEN 0 ELSE 1 END SR,  BA_COD_YEAR_ID AS YEARID, COUNT(REC_ID) AS CNT  FROM Bank_Account_Info  WHERE REC_STATUS  IN (0,1,2) AND BA_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( BA_SIGN_AB_ID_1  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') or  BA_SIGN_AB_ID_2 IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') or  BA_SIGN_AB_ID_3  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') )  GROUP BY BA_COD_YEAR_ID  ORDER BY SR"
                Case ConnectOneWS.Tables.CENTRE_SUPPORT_INFO
                    Query = "SELECT COUNT(REC_ID) AS CNT FROM Centre_Support_Info  WHERE REC_STATUS  IN (0,1,2) AND CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( CEN_ACC_RES_PERSON_AB_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "')  ) "
                Case ConnectOneWS.Tables.LAND_BUILDING_INFO
                    Query = "SELECT CASE WHEN LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " THEN 0 ELSE 1 END SR,  LB_COD_YEAR_ID AS YEARID, COUNT(REC_ID) AS CNT  FROM Land_Building_Info  WHERE REC_STATUS  IN (0,1,2) AND LB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( LB_OWNERSHIP_PARTY_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "')  )  GROUP BY LB_COD_YEAR_ID  ORDER BY SR"
                Case ConnectOneWS.Tables.SERVICE_PLACE_INFO
                    Query = "SELECT CASE WHEN SP_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " THEN 0 ELSE 1 END SR,  SP_COD_YEAR_ID AS YEARID, COUNT(REC_ID) AS CNT  FROM Service_Place_Info  WHERE REC_STATUS  IN (0,1,2) AND SP_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( SP_PLACEAT_AB_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "') OR SP_RESPONSIBLE_PERSON_AB_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "')   )  GROUP BY SP_COD_YEAR_ID  ORDER BY SR"
                Case ConnectOneWS.Tables.MEMBERSHIP_INFO
                    Query = "SELECT COUNT(REC_ID) AS CNT FROM MEMBERSHIP_INFO  WHERE REC_STATUS  IN (0,1,2) AND MS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( MS_AB_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "')) "
                Case ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO
                    Query = "SELECT COUNT(REC_ID) AS CNT FROM MAGAZINE_MEMBERSHIP_INFO  WHERE REC_STATUS  IN (0,1,2) AND MM_CEN_ID=" & inBasicParam.openCenID.ToString & " AND ( MM_MEMBER_ID  IN (SELECT REC_ID FROM ADDRESS_BOOK  WHERE C_ORG_REC_ID = '" & OrgABID & "')) "

            End Select
            Return dbService.List(Param.TableName, Query, Param.TableName.ToString(), inBasicParam)
        End Function

        Public Shared Function GetAddressRecID(ByVal inParam As Param_GetAddressRecID, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim Query As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OrgRecID As String = GetOrgAddressRecID(inParam.AB_RecID, inBasicParam)
            Query = "SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = '" & OrgRecID & "' AND C_COD_YEAR_ID = " & inParam.YearID.ToString & " AND REC_STATUS IN (0,1,2)"
            Return dbService.GetScalar(ConnectOneWS.Tables.ADDRESS_BOOK, Query, "ADDRESS_BOOK", inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Original RecID for Contact RecID Specified 
        ''' </summary>
        ''' <param name="Ab_ID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetOrgAddressRecID(ByVal Ab_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Return dbService.GetScalar(ConnectOneWS.Tables.ADDRESS_BOOK, "SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID = '" & Ab_ID & "' AND REC_STATUS IN (0,1,2)", "ADDRESS_BOOK", inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Contact RecID from Original RecID for CurrentYear
        ''' </summary>
        ''' <param name="Ab_ID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetAddressRecID_from_Org_ID(ByVal Ab_Org_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Return dbService.GetScalar(ConnectOneWS.Tables.ADDRESS_BOOK, "SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = '" & Ab_Org_ID & "' AND C_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " AND REC_STATUS IN (0,1,2)", "ADDRESS_BOOK", inBasicParam)
        End Function

        Public Shared Function GetAddressRecIDs_ForAllYears(ByVal AB_RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim Query As String = "SELECT C_ORG_REC_ID FROM ADDRESS_BOOK WHERE REC_ID = '" & AB_RecID & "' AND REC_STATUS IN (0,1,2)"
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OrgRecID As String = dbService.GetScalar(ConnectOneWS.Tables.ADDRESS_BOOK, Query, "ADDRESS_BOOK", inBasicParam)
            Query = "SELECT REC_ID FROM ADDRESS_BOOK WHERE C_ORG_REC_ID = '" & OrgRecID & "' AND REC_STATUS IN (0,1,2)"
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, "ADDRESS_BOOK", inBasicParam)
        End Function

        Public Shared Function GetDuplicateCount(ByVal inParam As Param_Get_Duplicates, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim param As Object = Nothing
            If Not inParam.insertPAram Is Nothing Then param = inParam.insertPAram
            If Not inParam.updatePAram Is Nothing Then param = inParam.updatePAram
            Dim Query As String = "select C_NAME AS 'Contact', " &
                                " CASE WHEN C_NAME LIKE '%" & param.Name & "%' THEN 'Name : " + param.Name + "'   " &
                                " WHEN (LEN(RTRIM(LTRIM(C_PAN_NO)))> 0 and C_PAN_NO='" + param.PANNo + "') THEN 'PAN ( " + param.PANNo + "  )' " + "  " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Res_Add1 & "')))> 0 and UPPER(RTRIM(LTRIM(C_R_ADD1))) + UPPER(RTRIM(LTRIM(C_R_ADD2))) + UPPER(RTRIM(LTRIM(C_R_ADD3))) + UPPER(RTRIM(LTRIM(C_R_ADD4))) LIKE UPPER('%" & param.Res_Add1 & "%')) THEN 'R.Address1 ( " + param.Res_Add1 + " " + ")'  " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Res_Add2 & "')))> 0 and UPPER(RTRIM(LTRIM(C_R_ADD1))) + UPPER(RTRIM(LTRIM(C_R_ADD2))) + UPPER(RTRIM(LTRIM(C_R_ADD3))) + UPPER(RTRIM(LTRIM(C_R_ADD4))) LIKE UPPER('%" & param.Res_Add2 & "%')) THEN 'R.Address2 ( " + param.Res_Add2 + " " + ")'  " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Res_Add3 & "')))> 0 and UPPER(RTRIM(LTRIM(C_R_ADD1))) + UPPER(RTRIM(LTRIM(C_R_ADD2))) + UPPER(RTRIM(LTRIM(C_R_ADD3))) + UPPER(RTRIM(LTRIM(C_R_ADD4))) LIKE UPPER('%" & param.Res_Add3 & "%')) THEN 'R.Address3 ( " + param.Res_Add3 + " " + ")'  " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Res_Add4 & "')))> 0 and UPPER(RTRIM(LTRIM(C_R_ADD1))) + UPPER(RTRIM(LTRIM(C_R_ADD2))) + UPPER(RTRIM(LTRIM(C_R_ADD3))) + UPPER(RTRIM(LTRIM(C_R_ADD4))) LIKE UPPER('%" & param.Res_Add4 & "%')) THEN 'R.Address4 ( " + param.Res_Add4 + " " + ")'  " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Off_Add1 & "')))> 0 and UPPER(RTRIM(LTRIM(C_O_ADD1))) + UPPER(RTRIM(LTRIM(C_O_ADD2))) + UPPER(RTRIM(LTRIM(C_O_ADD3))) + UPPER(RTRIM(LTRIM(C_O_ADD4))) LIKE UPPER('%" & param.Off_Add1 & "%')) THEN 'O.Address1 ( " + param.Off_Add1 + " " + ")'  " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Off_Add2 & "')))> 0 and UPPER(RTRIM(LTRIM(C_O_ADD1))) + UPPER(RTRIM(LTRIM(C_O_ADD2))) + UPPER(RTRIM(LTRIM(C_O_ADD3))) + UPPER(RTRIM(LTRIM(C_O_ADD4))) LIKE UPPER('%" & param.Off_Add2 & "%')) THEN 'O.Address2 ( " + param.Off_Add2 + " " + ")'  " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Off_Add3 & "')))> 0 and UPPER(RTRIM(LTRIM(C_O_ADD1))) + UPPER(RTRIM(LTRIM(C_O_ADD2))) + UPPER(RTRIM(LTRIM(C_O_ADD3))) + UPPER(RTRIM(LTRIM(C_O_ADD4))) LIKE UPPER('%" & param.Off_Add3 & "%')) THEN 'O.Address3 ( " + param.Off_Add3 + " " + ")'  " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Off_Add4 & "')))> 0 and UPPER(RTRIM(LTRIM(C_O_ADD1))) + UPPER(RTRIM(LTRIM(C_O_ADD2))) + UPPER(RTRIM(LTRIM(C_O_ADD3))) + UPPER(RTRIM(LTRIM(C_O_ADD4))) LIKE UPPER('%" & param.Off_Add4 & "%')) THEN 'O.Address4 ( " + param.Off_Add4 + " " + ")' " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.ResTel1 & "')))> 0 and (C_TEL_NO_R_1='" & param.ResTel1 & "' OR C_TEL_NO_R_2='" & param.ResTel1 & "' OR C_TEL_NO_O_1='" & param.ResTel1 & "' OR C_TEL_NO_O_2='" & param.ResTel1 & "' OR C_MOB_NO_1='" & param.ResTel1 & "' OR C_MOB_NO_2='" & param.ResTel1 & "')) THEN 'R.Tel1 ( " + param.ResTel1 + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.ResTel2 & "')))> 0 and (C_TEL_NO_R_1='" & param.ResTel2 & "' OR C_TEL_NO_R_2='" & param.ResTel2 & "' OR C_TEL_NO_O_1='" & param.ResTel2 & "' OR C_TEL_NO_O_2='" & param.ResTel2 & "' OR C_MOB_NO_1='" & param.ResTel2 & "' OR C_MOB_NO_2='" & param.ResTel2 & "')) THEN 'R.Tel2 ( " + param.ResTel2 + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.OffTel1 & "')))> 0 and (C_TEL_NO_R_1='" & param.OffTel1 & "' OR C_TEL_NO_R_2='" & param.OffTel1 & "' OR C_TEL_NO_O_1='" & param.OffTel1 & "' OR C_TEL_NO_O_2='" & param.OffTel1 & "' OR C_MOB_NO_1='" & param.OffTel1 & "' OR C_MOB_NO_2='" & param.OffTel1 & "')) THEN 'O.Tel1 ( " + param.OffTel1 + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.OffTel2 & "')))> 0 and (C_TEL_NO_R_1='" & param.OffTel2 & "' OR C_TEL_NO_R_2='" & param.OffTel2 & "' OR C_TEL_NO_O_1='" & param.OffTel2 & "' OR C_TEL_NO_O_2='" & param.OffTel2 & "' OR C_MOB_NO_1='" & param.OffTel2 & "' OR C_MOB_NO_2='" & param.OffTel2 & "')) THEN 'O.Tel2 ( " + param.OffTel2 + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Mob1 & "')))> 0 and (C_TEL_NO_R_1='" & param.Mob1 & "' OR C_TEL_NO_R_2='" & param.Mob1 & "' OR C_TEL_NO_O_1='" & param.Mob1 & "' OR C_TEL_NO_O_2='" & param.Mob1 & "' OR C_MOB_NO_1='" & param.Mob1 & "' OR C_MOB_NO_2='" & param.Mob1 & "')) THEN 'Mob1 ( " + param.Mob1 + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Mob2 & "')))> 0 and (C_TEL_NO_R_1='" & param.Mob2 & "' OR C_TEL_NO_R_2='" & param.Mob2 & "' OR C_TEL_NO_O_1='" & param.Mob2 & "' OR C_TEL_NO_O_2='" & param.Mob2 & "' OR C_MOB_NO_1='" & param.Mob2 & "' OR C_MOB_NO_2='" & param.Mob2 & "')) THEN 'Mob2 ( " + param.Mob2 + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Email1 & "')))> 0 and (UPPER(C_EMAIL_ID_1)=UPPER('" & param.Email1 & "') OR UPPER(C_EMAIL_ID_2)='" & param.Email1 & "')) THEN 'Email1 ( " + param.Email1 + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM('" & param.Email2 & "')))> 0 and (UPPER(C_EMAIL_ID_1)=UPPER('" & param.Email2 & "') OR UPPER(C_EMAIL_ID_2)='" & param.Email2 & "')) THEN 'Email2 ( " + param.Email2 + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_PASSPORT_NO)))> 0 and C_PASSPORT_NO ='" & param.PassportNo & "') THEN 'Passport ( " + param.PassportNo + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_VAT_TIN_NO)))> 0 and C_VAT_TIN_NO ='" & param.VAT_TIN & "') THEN 'VAT TIN ( " + param.VAT_TIN + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_CST_TIN_NO)))> 0 and C_CST_TIN_NO='" & param.CST_TIN & "') THEN 'CST TIN ( " + param.CST_TIN + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_TAN_NO)))> 0 and C_TAN_NO='" & param.TAN & "') THEN 'TAN ( " + param.TAN + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_UID_NO)))> 0 and C_UID_NO='" & param.UID & "') THEN 'Aadhaar No ( " + param.UID + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_DL_NO)))> 0 and C_DL_NO='" & param.DLNo & "') THEN 'DLNo ( " + param.DLNo + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_RATION_NO)))> 0 and C_RATION_NO='" & param.RationCardNo & "') THEN 'RationCardNo ( " + param.RationCardNo + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_VOTER_ID_NO)))> 0 and C_VOTER_ID_NO='" & param.VoterID & "') THEN 'VoterID ( " + param.VoterID + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_TAX_ID_NO)))> 0 and C_TAX_ID_NO='" & param.TaxpayerID & "') THEN 'TaxpayerID ( " + param.TaxpayerID + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_GST_TIN_NO)))> 0 and C_GST_TIN_NO='" & param.GST_TIN & "') THEN 'GST TIN ( " + param.GST_TIN + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_WEBSITE)))> 0 and C_WEBSITE='" & param.Website & "') THEN 'Website ( " + param.Website + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_STR_NO)))> 0 and C_STR_NO='" & param.STRNo & "') THEN 'STR_NO ( " + param.STRNo + " " + " " + ") ' " &
                                " WHEN (LEN(RTRIM(LTRIM(C_SKYPE_ID)))> 0 and C_SKYPE_ID='" & param.SkypeID & "') THEN 'SkypeID ( " + param.SkypeID + " " + ") ' " &
                                " WHEN  (LEN(RTRIM(LTRIM(C_FACEBOOK_ID)))> 0 and C_FACEBOOK_ID='" & param.FaceBookID & "') THEN 'FaceBookID ( " + param.FaceBookID + " " + ") ' " &
                                " WHEN  (LEN(RTRIM(LTRIM(C_TWITTER_ID)))> 0 and C_TWITTER_ID='" & param.TwitterID & "') THEN 'TwitterID ( " + param.TwitterID + " " + ") ' " &
                                " WHEN  (LEN(RTRIM(LTRIM(C_GTALK_ID)))> 0 and C_GTALK_ID='" & param.GtalkID & "') THEN 'GtalkID ( " + param.GtalkID + " " + ") ' " &
                                " END as Duplicate" &
                                 " from address_book where ((C_NAME LIKE '%" & param.Name & "%' " &
                                 "OR (LEN(RTRIM(LTRIM(C_PAN_NO)))> 0 and C_PAN_NO='" & param.PANNo & "') " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Res_Add1 & "')))> 0 and UPPER(RTRIM(LTRIM(C_R_ADD1))) + UPPER(RTRIM(LTRIM(C_R_ADD2))) + UPPER(RTRIM(LTRIM(C_R_ADD3))) + UPPER(RTRIM(LTRIM(C_R_ADD4))) LIKE UPPER('%" & param.Res_Add1 & "%')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Res_Add2 & "')))> 0 and UPPER(RTRIM(LTRIM(C_R_ADD1))) + UPPER(RTRIM(LTRIM(C_R_ADD2))) + UPPER(RTRIM(LTRIM(C_R_ADD3))) + UPPER(RTRIM(LTRIM(C_R_ADD4))) LIKE UPPER('%" & param.Res_Add2 & "%')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Res_Add3 & "')))> 0 and UPPER(RTRIM(LTRIM(C_R_ADD1))) + UPPER(RTRIM(LTRIM(C_R_ADD2))) + UPPER(RTRIM(LTRIM(C_R_ADD3))) + UPPER(RTRIM(LTRIM(C_R_ADD4))) LIKE UPPER('%" & param.Res_Add3 & "%')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Res_Add4 & "')))> 0 and UPPER(RTRIM(LTRIM(C_R_ADD1))) + UPPER(RTRIM(LTRIM(C_R_ADD2))) + UPPER(RTRIM(LTRIM(C_R_ADD3))) + UPPER(RTRIM(LTRIM(C_R_ADD4))) LIKE UPPER('%" & param.Res_Add4 & "%')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Off_Add1 & "')))> 0 and UPPER(RTRIM(LTRIM(C_O_ADD1))) + UPPER(RTRIM(LTRIM(C_O_ADD2))) + UPPER(RTRIM(LTRIM(C_O_ADD3))) + UPPER(RTRIM(LTRIM(C_O_ADD4))) LIKE UPPER('%" & param.Off_Add1 & "%')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Off_Add2 & "')))> 0 and UPPER(RTRIM(LTRIM(C_O_ADD1))) + UPPER(RTRIM(LTRIM(C_O_ADD2))) + UPPER(RTRIM(LTRIM(C_O_ADD3))) + UPPER(RTRIM(LTRIM(C_O_ADD4))) LIKE UPPER('%" & param.Off_Add2 & "%')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Off_Add3 & "')))> 0 and UPPER(RTRIM(LTRIM(C_O_ADD1))) + UPPER(RTRIM(LTRIM(C_O_ADD2))) + UPPER(RTRIM(LTRIM(C_O_ADD3))) + UPPER(RTRIM(LTRIM(C_O_ADD4))) LIKE UPPER('%" & param.Off_Add3 & "%')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Off_Add4 & "')))> 0 and UPPER(RTRIM(LTRIM(C_O_ADD1))) + UPPER(RTRIM(LTRIM(C_O_ADD2))) + UPPER(RTRIM(LTRIM(C_O_ADD3))) + UPPER(RTRIM(LTRIM(C_O_ADD4))) LIKE UPPER('%" & param.Off_Add4 & "%')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.ResTel1 & "')))> 0 and (C_TEL_NO_R_1='" & param.ResTel1 & "' OR C_TEL_NO_R_2='" & param.ResTel1 & "' OR C_TEL_NO_O_1='" & param.ResTel1 & "' OR C_TEL_NO_O_2='" & param.ResTel1 & "' OR C_MOB_NO_1='" & param.ResTel1 & "' OR C_MOB_NO_2='" & param.ResTel1 & "')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.ResTel2 & "')))> 0 and (C_TEL_NO_R_1='" & param.ResTel2 & "' OR C_TEL_NO_R_2='" & param.ResTel2 & "' OR C_TEL_NO_O_1='" & param.ResTel2 & "' OR C_TEL_NO_O_2='" & param.ResTel2 & "' OR C_MOB_NO_1='" & param.ResTel2 & "' OR C_MOB_NO_2='" & param.ResTel2 & "')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.OffTel1 & "')))> 0 and (C_TEL_NO_R_1='" & param.OffTel1 & "' OR C_TEL_NO_R_2='" & param.OffTel1 & "' OR C_TEL_NO_O_1='" & param.OffTel1 & "' OR C_TEL_NO_O_2='" & param.OffTel1 & "' OR C_MOB_NO_1='" & param.OffTel1 & "' OR C_MOB_NO_2='" & param.OffTel1 & "')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.OffTel2 & "')))> 0 and (C_TEL_NO_R_1='" & param.OffTel2 & "' OR C_TEL_NO_R_2='" & param.OffTel2 & "' OR C_TEL_NO_O_1='" & param.OffTel2 & "' OR C_TEL_NO_O_2='" & param.OffTel2 & "' OR C_MOB_NO_1='" & param.OffTel2 & "' OR C_MOB_NO_2='" & param.OffTel2 & "')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Mob1 & "')))> 0 and (C_TEL_NO_R_1='" & param.Mob1 & "' OR C_TEL_NO_R_2='" & param.Mob1 & "' OR C_TEL_NO_O_1='" & param.Mob1 & "' OR C_TEL_NO_O_2='" & param.Mob1 & "' OR C_MOB_NO_1='" & param.Mob1 & "' OR C_MOB_NO_2='" & param.Mob1 & "')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Mob2 & "')))> 0 and (C_TEL_NO_R_1='" & param.Mob2 & "' OR C_TEL_NO_R_2='" & param.Mob2 & "' OR C_TEL_NO_O_1='" & param.Mob2 & "' OR C_TEL_NO_O_2='" & param.Mob2 & "' OR C_MOB_NO_1='" & param.Mob2 & "' OR C_MOB_NO_2='" & param.Mob2 & "')) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Email1 & "')))> 0 and (UPPER(C_EMAIL_ID_1)=UPPER('" & param.Email1 & "') OR UPPER(C_EMAIL_ID_2)=UPPER('" & param.Email1 & "'))) " &
                                 "OR (LEN(RTRIM(LTRIM('" & param.Email2 & "')))> 0 and (UPPER(C_EMAIL_ID_1)=UPPER('" & param.Email2 & "') OR UPPER(C_EMAIL_ID_2)=UPPER('" & param.Email2 & "'))) " &
                                 "OR (LEN(RTRIM(LTRIM(C_PASSPORT_NO)))> 0 and C_PASSPORT_NO ='" & param.PassportNo & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_VAT_TIN_NO)))> 0 and C_VAT_TIN_NO='" & param.VAT_TIN & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_CST_TIN_NO)))> 0 and C_CST_TIN_NO='" & param.CST_TIN & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_TAN_NO)))> 0 and C_TAN_NO='" & param.TAN & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_UID_NO)))> 0 and C_UID_NO='" & param.UID & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_VOTER_ID_NO)))> 0 and C_VOTER_ID_NO='" & param.VoterID & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_RATION_NO)))> 0 and C_RATION_NO='" & param.RationCardNo & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_DL_NO)))> 0 and C_DL_NO='" & param.DLNo & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_TAX_ID_NO)))> 0 and C_TAX_ID_NO='" & param.TaxpayerID & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_STR_NO)))> 0 and C_STR_NO='" & param.STRNo & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_GST_TIN_NO)))> 0 and C_GST_TIN_NO='" & param.GST_TIN & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_WEBSITE)))> 0 and C_WEBSITE='" & param.Website & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_SKYPE_ID)))> 0 and C_SKYPE_ID='" & param.SkypeID & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_FACEBOOK_ID)))> 0 and C_FACEBOOK_ID='" & param.FaceBookID & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_TWITTER_ID)))> 0 and C_TWITTER_ID='" & param.TwitterID & "') " &
                                 "OR (LEN(RTRIM(LTRIM(C_GTALK_ID)))> 0 and C_GTALK_ID='" & param.GtalkID & "')) " &
                                 "AND C_CEN_ID =" & inBasicParam.openCenID.ToString & "	" &
                                 "AND REC_STATUS IN (0,1,2) " &
                                 "AND C_ORG_REC_ID <> '" & GetOrgAddressRecID(param.Rec_ID, inBasicParam) & "' " &
                                 "AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesList</remarks>
        Public Shared Function GetPartiesList(inBasicParam As ConnectOneWS.Basic_Param, Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END AS Name,C_OCCUPATION as Occupation,C_STATUS as Status, REC_ID AS ID, REC_EDIT_ON FROM ADDRESS_BOOK WHERE  REC_STATUS  IN (0,1,2) AND C_CEN_ID = " & inBasicParam.openCenID.ToString & " AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " ORDER BY C_NAME"
            If Not Party_Rec_ID Is Nothing Then 'added for multiuser check
                Query = "SELECT C_NAME + CASE WHEN C_NAME_DUP_ID IS NOT NULL THEN '('+CAST(C_NAME_DUP_ID AS VARCHAR)+')' ELSE '' END  AS Name,C_OCCUPATION as Occupation,C_ORG_NAME  AS Organization,C_STATUS as Status, REC_ID AS ID, REC_EDIT_ON FROM ADDRESS_BOOK WHERE  REC_STATUS  IN (0,1,2) AND C_CEN_ID = " & inBasicParam.openCenID.ToString & " and REC_ID = '" & Party_Rec_ID & "' ORDER BY C_NAME"
            End If
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook for specified RecIDs
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesListForSpecifiedRecIds</remarks>
        Public Shared Function GetPartiesList(ByVal RecIDs As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  C_NAME,REC_ID AS ID  FROM Address_Book Where   REC_STATUS  IN (0,1,2) AND REC_ID IN (" & RecIDs & ") AND C_CEN_ID = " & inBasicParam.openCenID.ToString & "  AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " ORDER BY C_NAME"
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Centre List
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetCenterList</remarks>
        Public Shared Function GetCenterList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT C.CEN_NAME ,C.CEN_BK_PAD_NO ,   M.CEN_INCHARGE , M.CEN_ZONE_ID , C.CEN_ID  " & _
                                        " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " & _
                                        " Where   C.REC_STATUS  IN (0,1,2) AND M.REC_STATUS  IN (0,1,2) AND  M.CEN_MAIN=1 AND C.CEN_INS_ID='00001' "
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Overseas Centre List
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetOverseasCenterList</remarks>
        Public Shared Function GetOverseasCenterList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT C.CEN_NAME ,C.CEN_BK_PAD_NO ,   C.CEN_INCHARGE , C.CEN_ZONE_ID , C.CEN_ID  " & _
                                        " FROM OVERSEAS_CENTRE_INFO AS C  " & _
                                        " Where   C.REC_STATUS  IN (0,1,2) "
            Return dbService.List(ConnectOneWS.Tables.OVERSEAS_CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.OVERSEAS_CENTRE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetAddressesForLabels(param As Param_GetAddressesForLabels, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim _SqlCityStyle As String = ""
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If param.rbt_CityStyle1_Checked Then
                _SqlCityStyle = " LTRIM(RTRIM(ISNULL(CT.CI_NAME,''))) "
            End If

            If param.rbt_CityStyle2_Checked Then
                _SqlCityStyle = " CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 THEN LTRIM(RTRIM(ISNULL(CT.CI_NAME,''))) ELSE '' END " & _
                                " +  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 AND LEN(LTRIM(RTRIM(ISNULL(A.C_R_PINCODE,'')))) > 0 THEN ' - ' ELSE '' END " & _
                                " +  LTRIM(RTRIM(ISNULL(A.C_R_PINCODE,''))) "
            End If

            If param.rbt_CityStyle3_Checked Then
                _SqlCityStyle = " CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 THEN LTRIM(RTRIM(ISNULL(CT.CI_NAME,''))) ELSE '' END " & _
                                " +  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 AND LEN(LTRIM(RTRIM(ISNULL(A.C_R_PINCODE,'')))) > 0 THEN ' - ' ELSE '' END " & _
                                " +  LTRIM(RTRIM(ISNULL(A.C_R_PINCODE,''))) " & _
                                " +  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(CT.CI_NAME,'')))) > 0 OR LEN(LTRIM(RTRIM(ISNULL(A.C_R_PINCODE,'')))) > 0 OR LEN(LTRIM(RTRIM(ISNULL(ST.ST_NAME,'')))) > 0 THEN ', '  ELSE '' END  + LTRIM(RTRIM(ISNULL(ST.ST_NAME,'')))   " & _
                                " +  CASE WHEN LEN(LTRIM(RTRIM(ISNULL(ST.ST_NAME,'')))) > 0 THEN ' - ' ELSE '' END  + LTRIM(RTRIM(ISNULL(CO.CO_NAME,'')))  " & _
                                " +  CASE WHEN LEN(COALESCE(A.C_TEL_NO_R_1,'')) > 0 THEN ', Tel: ' + A.C_TEL_NO_R_1 ELSE '' END " & _
                                " +  CASE WHEN LEN(COALESCE(A.C_MOB_NO_1,'')) > 0 THEN ', Mob: ' + A.C_MOB_NO_1 ELSE '' END "
            End If


            Dim _query = " SELECT '' AS NO,                              " &
                        "  RTRIM(LTRIM(A.C_NAME)) AS NAME,              " &
                        "  RTRIM(LTRIM(ISNULL(A.C_R_ADD1,''))) AS ADD1, " &
                        "  RTRIM(LTRIM(ISNULL(A.C_R_ADD2,''))) AS ADD2, " &
                        "  RTRIM(LTRIM(ISNULL(A.C_R_ADD3,''))) AS ADD3, " &
                        "  RTRIM(LTRIM(ISNULL(A.C_R_ADD4,''))) AS ADD4, " &
                        "  " & _SqlCityStyle & " AS ADD5,               " &
                        "  A.REC_ID AS ID                               " &
                        "  FROM      ADDRESS_BOOK       AS A    " &
                        "  LEFT JOIN MAP_CITY_INFO      AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID   AND     CT.REC_STATUS IN (0,1,2) ) " &
                        "  LEFT JOIN MAP_STATE_INFO     AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID   AND     ST.REC_STATUS IN (0,1,2) ) " &
                        "  LEFT JOIN MAP_DISTRICT_INFO  AS DI 	ON (A.C_R_DISTRICT_ID = DI.REC_ID   AND     DI.REC_STATUS IN (0,1,2) ) " &
                        "  LEFT JOIN MAP_COUNTRY_INFO   AS CO 	ON (A.C_R_COUNTRY_ID  = CO.REC_ID   AND     CO.REC_STATUS IN (0,1,2) ) " &
                        "  WHERE A.REC_STATUS IN (0,1,2) AND A.C_CEN_ID =" & inBasicParam.openCenID.ToString & " AND A.C_COD_YEAR_ID=(SELECT MAX(COD_YEAR_ID) YEAR_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID.ToString & ") " &
                        "  AND CT.CI_NAME LIKE CASE WHEN '" & param.FilterBy & "' = 'a) City' THEN '%" & param.FilterValue & "%' ELSE CT.CI_NAME END " &
                        "  AND ST.ST_NAME LIKE CASE WHEN '" & param.FilterBy & "' = 'b) State' THEN '%" & param.FilterValue & "%' ELSE ST.ST_NAME END " &
                        "  AND A.C_NAME LIKE CASE WHEN '" & param.FilterBy & "' = 'd) Name (Contains)' THEN '%" & param.FilterValue & "%' ELSE A.C_NAME END " &
                        "  AND A.C_R_PINCODE LIKE CASE WHEN '" & param.FilterBy & "' = 'e) Pincode' THEN '%" & param.FilterValue & "%' ELSE A.C_R_PINCODE END " &
                        "  ORDER BY RTRIM(LTRIM(A.C_NAME)) "
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, _query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Inserts Address
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Addresses, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.YearID = Nothing Then
                InParam.YearID = inBasicParam.openYearID
            End If
            Dim SPName As String = "sp_Insert_Address_Book"
            Dim params() As String = {"@cen_id", "@YEARID", "@Title", "@Name", "@Gender", "@OrgName", "@Designation", "@Education", "@OccupationID", "@Reference", "@Remarks1", "@Remarks2", "@LokikDob", "@BloodGroup", "@ContactModeID", "@PANNo", "@VAT_TIN", "@CST_TIN", "@TAN", "@UID", "@STRNo", "@PassportNo", "@Magazine", "@Res_Add1", "@Res_Add2", "@Res_Add3", "@Res_Add4", "@Res_cityID", "@Res_city", "@Res_StateID", "@Res_DisttID", "@Res_CountryID", "@Res_PinCode", "@Off_Add1", "@Off_Add2", "@Off_Add3", "@Off_Add4", "@Off_CityID", "@Off_StateID", "@Off_DistID", "@Off_CountryID", "@Off_PinCode", "@ResTel1", "@ResTel2", "@ResFax1", "@ResFax2", "@OffTel1", "@OffTel2", "@OffFax1", "@OffFax2", "@Mob1", "@Mob2", "@Email1", "@Email2", "@Website", "@SkypeID", "@FaceBookID", "@TwitterID", "@GtalkID", "@Status", "@ContactDate", "@AlokikDOB", "@BKTitle", "@PADNo", "@ClassAt", "@CenCategory", "@ClassCID", "@ClassAdd1", "@Category", "@Category_Other", "@WingsMember", "@Specialities", "@Special_Other", "@Events", "@EventsOther", "@UserID", "@Status_Action", "@Rec_ID", "@OrgAB_RecId", "@SubCityID", "@D_O_SURR", "@ACC_PARTY", "@GST_TIN", "@DLNo", "@RationNo", "@VoterID", "@TaxID"}
            Dim values() As Object = {inBasicParam.openCenID, InParam.YearID, InParam.Title, InParam.Name, InParam.Gender, InParam.OrgName, InParam.Designation, InParam.Education, InParam.OccupationID, InParam.Reference, InParam.Remarks1, InParam.Remarks2, InParam.LokikDob, InParam.BloodGroup, InParam.ContactModeID, InParam.PANNo, InParam.VAT_TIN, InParam.CST_TIN, InParam.TAN, InParam.UID, InParam.STRNo, InParam.PassportNo, InParam.Magazine, InParam.Res_Add1, InParam.Res_Add2, InParam.Res_Add3, InParam.Res_Add4, InParam.Res_cityID, InParam.Res_city, InParam.Res_StateID, InParam.Res_DisttID, InParam.Res_CountryID, InParam.Res_PinCode, InParam.Off_Add1, InParam.Off_Add2, InParam.Off_Add3, InParam.Off_Add4, InParam.Off_CityID, InParam.Off_StateID, InParam.Off_DistID, InParam.Off_CountryID, InParam.Off_PinCode, InParam.ResTel1, InParam.ResTel2, InParam.ResFax1, InParam.ResFax2, InParam.OffTel1, InParam.OffTel2, InParam.OffFax1, InParam.OffFax2, InParam.Mob1, InParam.Mob2, InParam.Email1, InParam.Email2, InParam.Website, InParam.SkypeID, InParam.FaceBookID, InParam.TwitterID, InParam.GtalkID, InParam.Status, InParam.ContactDate, InParam.AlokikDOB, InParam.BKTitle, InParam.PADNo, InParam.ClassAt, InParam.CenCategory, InParam.ClassCID, InParam.ClassAdd1, InParam.Category, InParam.Category_Other, InParam.WingsMember, InParam.Specialities, InParam.Special_Other, InParam.Events, InParam.EventsOther, inBasicParam.openUserID, InParam.Status_Action, InParam.Rec_ID, InParam.OrgAB_RecId, InParam.SubCityID, InParam.DateOfSurr, InParam.AccountingParty, InParam.GST_TIN, InParam.DLNo, InParam.RationCardNo, InParam.VoterID, InParam.TaxpayerID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 100, 250, 255, 250, 255, 255, 36, 100, 250, 250, 8, 255, 36, 255, 255, 255, 255, 255, 255, 255, -1, 100, 100, 100, 100, 36, 100, 36, 36, 36, 100, 100, 100, 100, 100, 36, 36, 36, 36, 100, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 100, 100, 100, 255, 255, 255, 255, 10, 8, 8, 255, 255, 255, 100, 5, 255, 100, 255, -1, -1, -1, -1, -1, 255, 4, 36, 36, 36, 8, 1, 15, 255, 255, 255, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.ADDRESS_BOOK, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True

            'Dim OnlineQuery As String = "INSERT INTO ADDRESS_BOOK(C_CEN_ID,C_TITLE,C_NAME,C_GENDER,C_ORG_NAME,C_DESIGNATION,C_EDUCATION,C_OCCUPATION_ID,C_REF,C_REMARKS_1,C_REMARKS_2," & _
            '                                        "C_DOB_L,C_BLOODGROUP,C_CONTACT_MODE_ID,C_PAN_NO,C_VAT_TIN_NO,C_CST_TIN_NO,C_TAN_NO,C_UID_NO,C_STR_NO,C_PASSPORT_NO,C_MAGAZINE," & _
            '                                        "C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_R_CITY,C_R_STATE_ID,C_R_DISTRICT_ID,C_R_COUNTRY_ID,C_R_PINCODE,C_O_ADD1,C_O_ADD2,C_O_ADD3," & _
            '                                        "C_O_ADD4,C_O_CITY_ID,C_O_STATE_ID,C_O_DISTRICT_ID,C_O_COUNTRY_ID,C_O_PINCODE,C_TEL_NO_R_1,C_TEL_NO_R_2,C_FAX_NO_R_1,C_FAX_NO_R_2,C_TEL_NO_O_1," & _
            '                                        "C_TEL_NO_O_2,C_FAX_NO_O_1,C_FAX_NO_O_2,C_MOB_NO_1,C_MOB_NO_2,C_EMAIL_ID_1,C_EMAIL_ID_2,C_WEBSITE,C_SKYPE_ID,C_FACEBOOK_ID,C_TWITTER_ID,C_GTALK_ID," & _
            '                                        "C_STATUS,C_CON_DT,C_DOB_A,C_BK_TITLE,C_BK_PAD_NO,C_CLASS_AT,C_CEN_CATEGORY,C_CLASS_CEN_ID,C_CLASS_ADD1,C_CATEGORY,C_CATEGORY_OTHER," & _
            '                                        "C_WINGS_MEMBER,C_SPECIALTIES,C_SPECIAL_OTHER,C_EVENTS,C_EVENTS_OTHER,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID, C_ORG_REC_ID, C_COD_YEAR_ID,C_R_SUB_CITY_ID " & _
            '                                        ") VALUES(" & _
            '                                       "'" & inBasicParam.openCenID & "','" & _
            '                                       InParam.Title & "', " & _
            '                                        "'" & InParam.Name & "','" & InParam.Gender & "','" & InParam.OrgName & "','" & InParam.Designation & "','" & InParam.Education & "'," & IIf(InParam.OccupationID Is Nothing, "NULL", "'" & InParam.OccupationID & "'") & _
            '                                        ",'" & InParam.Reference & "','" & InParam.Remarks1 & "','" & InParam.Remarks2 & "'," & _
            '                                        If(IsDate(InParam.LokikDob), "'" & Convert.ToDateTime(InParam.LokikDob).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & _
            '                                        ",'" & InParam.BloodGroup & "'," & IIf(InParam.ContactModeID Is Nothing, "NULL", "'" & InParam.ContactModeID & "'") & ",'" & InParam.PANNo & "','" & InParam.VAT_TIN & "','" & InParam.CST_TIN & "','" & InParam.TAN & "','" & _
            '                                        InParam.UID & "','" & InParam.STRNo & "','" & InParam.PassportNo & "','" & InParam.Magazine & "'," & _
            '                                        "'" & InParam.Res_Add1 & "','" & InParam.Res_Add2 & "','" & InParam.Res_Add3 & "','" & InParam.Res_Add4 & "'," & IIf(InParam.Res_cityID Is Nothing, "NULL", "'" & InParam.Res_cityID & "'") & "," & IIf(InParam.Res_city Is Nothing, "NULL", "'" & InParam.Res_city & "'") & "," & IIf(InParam.Res_StateID Is Nothing, "NULL", "'" & InParam.Res_StateID & "'") & "," & IIf(InParam.Res_DisttID Is Nothing, "NULL", "'" & InParam.Res_DisttID & "'") & _
            '                                        "," & IIf(InParam.Res_CountryID Is Nothing, "NULL", "'" & InParam.Res_CountryID & "'") & ",'" & InParam.Res_PinCode & "','" & InParam.Off_Add1 & "','" & InParam.Off_Add2 & "','" & InParam.Off_Add3 & "','" & InParam.Off_Add4 & "'," & _
            '                                        IIf(InParam.Off_CityID Is Nothing, "NULL", "'" & InParam.Off_CityID & "'") & "," & IIf(InParam.Off_StateID Is Nothing, "NULL", "'" & InParam.Off_StateID & "'") & "," & IIf(InParam.Off_DistID Is Nothing, "NULL", "'" & InParam.Off_DistID & "'") & "," & IIf(InParam.Off_CountryID Is Nothing, "NULL", "'" & InParam.Off_CountryID & "'") & ",'" & InParam.Off_PinCode & "','" & InParam.ResTel1 & "','" & InParam.ResTel2 & _
            '                                        "','" & InParam.ResFax1 & "','" & InParam.ResFax2 & "','" & InParam.OffTel1 & "','" & InParam.OffTel2 & "','" & InParam.OffFax1 & "','" & InParam.OffFax2 & "','" & InParam.Mob1 & "','" & InParam.Mob2 & _
            '                                        "','" & InParam.Email1 & "','" & InParam.Email2 & "','" & InParam.Website & "','" & InParam.SkypeID & "','" & InParam.FaceBookID & "','" & InParam.TwitterID & "','" & InParam.GtalkID & "'," & _
            '                                        "'" & InParam.Status & "'," & If(IsDate(InParam.ContactDate), "'" & Convert.ToDateTime(InParam.ContactDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & "," & _
            '                                        If(IsDate(InParam.AlokikDOB), "'" & Convert.ToDateTime(InParam.AlokikDOB).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ",'" & _
            '                                        InParam.BKTitle & "','" & InParam.PADNo & "','" & InParam.ClassAt & "'," & InParam.CenCategory & "," & IIf(InParam.ClassCID Is Nothing, "NULL", "'" & InParam.ClassCID & "'") & ",'" & InParam.ClassAdd1 & "','" & InParam.Category & "','" & InParam.Category_Other & "'," & _
            '                                        "'" & InParam.WingsMember & "','" & InParam.Specialities & "','" & InParam.Special_Other & "','" & InParam.Events & "','" & InParam.EventsOther & "'," & _
            '                                      "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.Rec_ID & "'" & ", '" & InParam.OrgAB_RecId & "', '" & InParam.YearID & "'," & IIf(InParam.SubCityID Is Nothing, "NULL", "'" & InParam.SubCityID & "'") & ")"
            'dbService.Insert(ConnectOneWS.Tables.ADDRESS_BOOK, OnlineQuery, inBasicParam, InParam.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Updates Address
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Addresses, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            If UpParam.Status.ToLower() = "wellwisher" Then 'Well Wisher
                UpParam.AlokikDOB = Nothing
                UpParam.BKTitle = Nothing
                UpParam.PADNo = Nothing
                UpParam.ClassAt = Nothing
                UpParam.CenCategory = Nothing
                UpParam.ClassCID = Nothing
                UpParam.ClassAdd1 = Nothing
            Else 'BK
                UpParam.ContactDate = Nothing
            End If

            If UpParam.ReplicationUpdate Then
                Dim OrgRecID As String = GetOrgAddressRecID(UpParam.Rec_ID, inBasicParam)
                UpParam.OrgRecID = OrgRecID
                'OnlineQuery = OnlineQuery & "  WHERE C_ORG_REC_ID    ='" & OrgRecID & "' AND C_COD_YEAR_ID = '" & UpParam.YearID & "'"
                'Else
                'OnlineQuery = OnlineQuery & "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            End If

            If UpParam.YearID = Nothing Then
                UpParam.YearID = inBasicParam.openYearID
            End If

            dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & UpParam.Rec_ID & "' ", inBasicParam)

            Dim SPName As String = "sp_Update_Address_Book"
            Dim params() As String = {"@cen_id", "@YEARID", "@Title", "@Name", "@Gender", "@OrgName", "@Designation", "@Education", "@OccupationID", "@Reference", "@Remarks1", "@Remarks2", "@LokikDob", "@BloodGroup", "@ContactModeID", "@PANNo", "@VAT_TIN", "@CST_TIN", "@TAN", "@UID", "@STRNo", "@PassportNo", "@Magazine", "@Res_Add1", "@Res_Add2", "@Res_Add3", "@Res_Add4", "@Res_cityID", "@Res_city", "@Res_StateID", "@Res_DisttID", "@Res_CountryID", "@Res_PinCode", "@Off_Add1", "@Off_Add2", "@Off_Add3", "@Off_Add4", "@Off_CityID", "@Off_StateID", "@Off_DistID", "@Off_CountryID", "@Off_PinCode", "@ResTel1", "@ResTel2", "@ResFax1", "@ResFax2", "@OffTel1", "@OffTel2", "@OffFax1", "@OffFax2", "@Mob1", "@Mob2", "@Email1", "@Email2", "@Website", "@SkypeID", "@FaceBookID", "@TwitterID", "@GtalkID", "@Status", "@ContactDate", "@AlokikDOB", "@BKTitle", "@PADNo", "@ClassAt", "@CenCategory", "@ClassCID", "@ClassAdd1", "@Category", "@Category_Other", "@WingsMember", "@Specialities", "@Special_Other", "@Events", "@EventsOther", "@UserID", "@Status_Action", "@Rec_ID", "@OrgAB_RecId", "@SubCityID", "@D_O_SURR", "@ACC_PARTY", "@GST_TIN", "@DLNo", "@RationNo", "@VoterID", "@TaxID"}
            Dim values() As Object = {inBasicParam.openCenID, UpParam.YearID, UpParam.Title, UpParam.Name, UpParam.Gender, UpParam.OrgName, UpParam.Designation, UpParam.Education, UpParam.OccupationID, UpParam.Reference, UpParam.Remarks1, UpParam.Remarks2, UpParam.LokikDob, UpParam.BloodGroup, UpParam.ContactModeID, UpParam.PANNo, UpParam.VAT_TIN, UpParam.CST_TIN, UpParam.TAN, UpParam.UID, UpParam.STRNo, UpParam.PassportNo, UpParam.Magazine, UpParam.Res_Add1, UpParam.Res_Add2, UpParam.Res_Add3, UpParam.Res_Add4, UpParam.Res_cityID, UpParam.Res_city, UpParam.Res_StateID, UpParam.Res_DisttID, UpParam.Res_CountryID, UpParam.Res_PinCode, UpParam.Off_Add1, UpParam.Off_Add2, UpParam.Off_Add3, UpParam.Off_Add4, UpParam.Off_CityID, UpParam.Off_StateID, UpParam.Off_DistID, UpParam.Off_CountryID, UpParam.Off_PinCode, UpParam.ResTel1, UpParam.ResTel2, UpParam.ResFax1, UpParam.ResFax2, UpParam.OffTel1, UpParam.OffTel2, UpParam.OffFax1, UpParam.OffFax2, UpParam.Mob1, UpParam.Mob2, UpParam.Email1, UpParam.Email2, UpParam.Website, UpParam.SkypeID, UpParam.FaceBookID, UpParam.TwitterID, UpParam.GtalkID, UpParam.Status, UpParam.ContactDate, UpParam.AlokikDOB, UpParam.BKTitle, UpParam.PADNo, UpParam.ClassAt, UpParam.CenCategory, UpParam.ClassCID, UpParam.ClassAdd1, UpParam.Category, UpParam.Category_Other, UpParam.WingsMember, UpParam.Specialities, UpParam.Special_Other, UpParam.Events, UpParam.EventsOther, inBasicParam.openUserID, 1, UpParam.Rec_ID, UpParam.OrgRecID, UpParam.SubCityID, UpParam.DateOfSurr, UpParam.AccountingParty, UpParam.GST_TIN, UpParam.DLNo, UpParam.RationCardNo, UpParam.VoterID, UpParam.TaxpayerID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 100, 250, 255, 250, 255, 255, 36, 100, 250, 250, 8, 255, 36, 255, 255, 255, 255, 255, 255, 255, -1, 100, 100, 100, 100, 36, 100, 36, 36, 36, 100, 100, 100, 100, 100, 36, 36, 36, 36, 100, 15, 15, 15, 15, 15, 15, 15, 15, 15, 15, 100, 100, 100, 255, 255, 255, 255, 10, 8, 8, 255, 255, 255, 100, 5, 255, 100, 255, -1, -1, -1, -1, -1, 255, 4, 36, 36, 36, 8, 1, 15, 255, 255, 255, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.ADDRESS_BOOK, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True

            'Dim OnlineQuery As String = " UPDATE ADDRESS_BOOK SET " & _
            '                             " C_TITLE       = '" & UpParam.Title & "', " & _
            '                             " C_NAME        = '" & UpParam.Name & "', " & _
            '                             " C_ORG_NAME    = '" & UpParam.OrgName & "', " & _
            '                             " C_DESIGNATION = '" & UpParam.Designation & "', " & _
            '                             " C_OCCUPATION_ID  = " & IIf(UpParam.OccupationID Is Nothing, "NULL", "'" & UpParam.OccupationID & "'") & " , " & _
            '                             " C_R_ADD1        ='" & UpParam.Res_Add1 & "', " & _
            '                             " C_R_ADD2        ='" & UpParam.Res_Add2 & "', " & _
            '                             " C_R_ADD3        ='" & UpParam.Res_Add3 & "', " & _
            '                             " C_R_ADD4        ='" & UpParam.Res_Add4 & "', " & _
            '                             " C_R_CITY_ID        = " & IIf(UpParam.Res_cityID Is Nothing, "NULL", "'" & UpParam.Res_cityID & "'") & " , " & _
            '                             " C_R_CITY        = " & IIf(UpParam.Res_city Is Nothing, "NULL", "'" & UpParam.Res_city & "'") & " , " & _
            '                             " C_R_STATE_ID       =" & IIf(UpParam.Res_StateID Is Nothing, "NULL", "'" & UpParam.Res_StateID & "'") & ", " & _
            '                             " C_R_DISTRICT_ID    =" & IIf(UpParam.Res_DisttID Is Nothing, "NULL", "'" & UpParam.Res_DisttID & "'") & ", " & _
            '                             " C_R_COUNTRY_ID     =" & IIf(UpParam.Res_CountryID Is Nothing, "NULL", "'" & UpParam.Res_CountryID & "'") & ", " & _
            '                             " C_R_PINCODE        ='" & UpParam.Res_PinCode & "', " & _
            '                             " C_REMARKS_1     ='" & UpParam.Remarks1 & "', " & _
            '                             " C_REMARKS_2     ='" & UpParam.Remarks2 & "', " & _
            '                             " C_REF        ='" & UpParam.Reference & "', " & _
            '                             " C_GENDER        ='" & UpParam.Gender & "', " & _
            '                             " C_EDUCATION        ='" & UpParam.Education & "', " & _
            '                             " C_DOB_L        =" & If(IsDate(UpParam.LokikDob), "'" & Convert.ToDateTime(UpParam.LokikDob).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
            '                             " C_BLOODGROUP        ='" & UpParam.BloodGroup & "', " & _
            '                             " C_CONTACT_MODE_ID        =" & IIf(UpParam.ContactModeID Is Nothing, "NULL", "'" & UpParam.ContactModeID & "'") & ", " & _
            '                             " C_PAN_NO        ='" & UpParam.PANNo & "', " & _
            '                             " C_VAT_TIN_NO        ='" & UpParam.VAT_TIN & "', " & _
            '                             " C_CST_TIN_NO        ='" & UpParam.CST_TIN & "', " & _
            '                             " C_TAN_NO        ='" & UpParam.TAN & "', " & _
            '                             " C_UID_NO        ='" & UpParam.UID & "', " & _
            '                             " C_STR_NO        ='" & UpParam.STRNo & "', " & _
            '                             " C_PASSPORT_NO   ='" & UpParam.PassportNo & "', " & _
            '                             " C_MAGAZINE        ='" & UpParam.Magazine & "', " & _
            '                             " C_O_ADD1        ='" & UpParam.Off_Add1 & "', " & _
            '                             " C_O_ADD2        ='" & UpParam.Off_Add2 & "', " & _
            '                             " C_O_ADD3        ='" & UpParam.Off_Add3 & "', " & _
            '                             " C_O_ADD4        ='" & UpParam.Off_Add4 & "', " & _
            '                             " C_O_CITY_ID        = " & IIf(UpParam.Off_CityID Is Nothing, "NULL", "'" & UpParam.Off_CityID & "'") & " , " & _
            '                             " C_O_STATE_ID       = " & IIf(UpParam.Off_StateID Is Nothing, "NULL", "'" & UpParam.Off_StateID & "'") & " , " & _
            '                             " C_O_DISTRICT_ID    = " & IIf(UpParam.Off_DistID Is Nothing, "NULL", "'" & UpParam.Off_DistID & "'") & " , " & _
            '                             " C_O_COUNTRY_ID     = " & IIf(UpParam.Off_CountryID Is Nothing, "NULL", "'" & UpParam.Off_CountryID & "'") & " , " & _
            '                             " C_R_SUB_CITY_ID     = " & IIf(UpParam.SubCityID Is Nothing, "NULL", "'" & UpParam.SubCityID & "'") & " , " & _
            '                             " C_O_PINCODE        ='" & UpParam.Off_PinCode & "', " & _
            '                             " C_TEL_NO_R_1        ='" & UpParam.ResTel1 & "', " & _
            '                             " C_TEL_NO_R_2        ='" & UpParam.ResTel2 & "', " & _
            '                             " C_FAX_NO_R_1        ='" & UpParam.ResFax1 & "', " & _
            '                             " C_FAX_NO_R_2        ='" & UpParam.ResFax2 & "', " & _
            '                             " C_TEL_NO_O_1        ='" & UpParam.OffTel1 & "', " & _
            '                             " C_TEL_NO_O_2        ='" & UpParam.OffTel2 & "', " & _
            '                             " C_FAX_NO_O_1        ='" & UpParam.OffFax1 & "', " & _
            '                             " C_FAX_NO_O_2        ='" & UpParam.OffFax2 & "', " & _
            '                             " C_MOB_NO_1        ='" & UpParam.Mob1 & "', " & _
            '                             " C_MOB_NO_2        ='" & UpParam.Mob2 & "', " & _
            '                             " C_EMAIL_ID_1        ='" & UpParam.Email1 & "', " & _
            '                             " C_EMAIL_ID_2        ='" & UpParam.Email2 & "', " & _
            '                             " C_WEBSITE        ='" & UpParam.Website & "', " & _
            '                             " C_SKYPE_ID        ='" & UpParam.SkypeID & "', " & _
            '                             " C_FACEBOOK_ID        ='" & UpParam.FaceBookID & "', " & _
            '                             " C_TWITTER_ID        ='" & UpParam.TwitterID & "', " & _
            '                             " C_GTALK_ID        ='" & UpParam.GtalkID & "', " & _
            '                             " C_STATUS        ='" & UpParam.Status & "', "
            'If UpParam.Status.ToLower() = "wellwisher" Then 'Well Wisher
            '    OnlineQuery = OnlineQuery & " C_CON_DT        =" & If(IsDate(UpParam.ContactDate), "'" & Convert.ToDateTime(UpParam.ContactDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
            '                    " C_DOB_A            = NULL, " & _
            '                    " C_BK_TITLE         = NULL, " & _
            '                    " C_BK_PAD_NO        = NULL, " & _
            '                    " C_CLASS_AT         = NULL, " & _
            '                    " C_CEN_CATEGORY     = NULL, " & _
            '                    " C_CLASS_CEN_ID     = NULL, " & _
            '                    " C_CLASS_ADD1       = NULL, "
            'Else 'BK
            '    OnlineQuery = OnlineQuery & " C_DOB_A        =" & If(IsDate(UpParam.AlokikDOB), "'" & Convert.ToDateTime(UpParam.AlokikDOB).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
            '                    " C_BK_TITLE         ='" & UpParam.BKTitle & "', " & _
            '                    " C_BK_PAD_NO        ='" & UpParam.PADNo & "', " & _
            '                    " C_CLASS_AT         ='" & UpParam.ClassAt & "', " & _
            '                    " C_CEN_CATEGORY     = " & UpParam.CenCategory & " , " & _
            '                    " C_CLASS_CEN_ID     = " & IIf(UpParam.ClassCID Is Nothing, "NULL", "'" & UpParam.ClassCID & "'") & " , " & _
            '                    " C_CLASS_ADD1       ='" & UpParam.ClassAdd1 & "', " & _
            '                    " C_CON_DT           = NULL, "
            'End If
            'OnlineQuery = OnlineQuery & " C_CATEGORY     ='" & UpParam.Category & "', " & _
            '                    " C_CATEGORY_OTHER       ='" & UpParam.Category_Other & "', " & _
            '                    " C_WINGS_MEMBER         ='" & UpParam.WingsMember & "', " & _
            '                    " C_SPECIALTIES          ='" & UpParam.Specialities & "', " & _
            '                    " C_SPECIAL_OTHER        ='" & UpParam.Special_Other & "', " & _
            '                    " C_EVENTS               ='" & UpParam.Events & "', " & _
            '                    " C_EVENTS_OTHER         ='" & UpParam.EventsOther & "', " & _
            '                     " " & _
            '                     "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
            '                     "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'  "
            'If UpParam.ReplicationUpdate Then
            '    Dim OrgRecID As String = GetOrgAddressRecID(UpParam.Rec_ID, inBasicParam)
            '    OnlineQuery = OnlineQuery & "  WHERE C_ORG_REC_ID    ='" & OrgRecID & "' AND C_COD_YEAR_ID = '" & UpParam.YearID & "'"
            'Else
            '    OnlineQuery = OnlineQuery & "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            'End If

            'dbService.Update(ConnectOneWS.Tables.ADDRESS_BOOK, OnlineQuery, inBasicParam, EditTime)
            'Return True
        End Function

        ''' <summary>
        ''' Insert Magazine
        ''' </summary>
        ''' <param name="InMagParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_InsertMagazine</remarks>
        Public Shared Function InsertMagazine(ByVal InMagParam As Parameter_InsertMagazine_Addresses, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InMagParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Address_Book_Magazine_Info (C_CEN_ID,C_REC_ID,C_MISC_REC_ID," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "'" & InMagParam.AB_Rec_ID & "'," & _
                                        "'" & InMagParam.Magazine_Misc_ID & "'," & _
                                        "" & Str & "   '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InMagParam.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.ADDRESS_BOOK_MAGAZINE_INFO, OnlineQuery, inBasicParam, InMagParam.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Inserts Wings Information
        ''' </summary>
        ''' <param name="InWinParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_InsertWings</remarks>
        Public Shared Function InsertWings(ByVal InWinParam As Parameter_InsertWings_Addresses, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InWinParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Address_Book_Wing_Info (C_CEN_ID,C_REC_ID,C_WING_ID," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "'" & InWinParam.AB_Rec_ID & "'," & _
                                        "'" & InWinParam.Wings_Misc_ID & "'," & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InWinParam.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.ADDRESS_BOOK_WING_INFO, OnlineQuery, inBasicParam, InWinParam.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Inserts Specialities Info
        ''' </summary>
        ''' <param name="InSpeParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_InsertSpecialities</remarks>
        Public Shared Function InsertSpecialities(ByVal InSpeParam As Parameter_InsertSpecialities_Addresses, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InSpeParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Address_Book_Special_Info (C_CEN_ID,C_REC_ID,C_MISC_REC_ID," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                        ") VALUES(" &
                                        "" & inBasicParam.openCenID.ToString & "," &
                                        "'" & InSpeParam.AB_Rec_ID & "'," &
                                        "'" & InSpeParam.Specialities_Misc_ID & "'," &
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InSpeParam.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.ADDRESS_BOOK_SPECIAL_INFO, OnlineQuery, inBasicParam, InSpeParam.Rec_ID, Nothing, AddTime)
            Return True
        End Function
        Public Shared Function InsertAdditionalParameters(inParam As Param_Insert_additional_info_address, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "','" & Common_Lib.Common.Record_Status._Completed & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"

            Dim OnlineQuery As String = "INSERT INTO address_book_additional_info (C_CEN_ID,C_REC_ID,C_MOB_NO_3,C_MOB_NO_4,C_MOB_NO_5,C_EMAIL_ID_3,C_EMAIL_ID_4,C_EMAIL_ID_5," &
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,C_PROFILE_PIC_FILENAME,REC_ID" &
                                        ") VALUES(" &
                                        "" & inBasicParam.openCenID.ToString & "," &
                                        "'" & inParam.AB_Rec_ID & "'," &
                                        "'" & inParam.Mobile3 & "'," &
                                        "'" & inParam.Mobile4 & "'," &
                                        "'" & inParam.Mobile5 & "'," &
                                        "'" & inParam.Email3 & "'," &
                                        "'" & inParam.Email4 & "'," &
                                        "'" & inParam.Email5 & "'," &
                                        "" & Str & " '" & Common.DateTimePlaceHolder &
                                        "', '" & inBasicParam.openUserID & "', '" & inParam.File_Name & "', '" & inParam.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.ADDRESS_BOOK_ADDITIONAL_INFO, OnlineQuery, inBasicParam, inParam.Rec_ID, Nothing, AddTime)
            If inParam.File IsNot Nothing Then
                If inParam.File.Length > 0 Then
                    'Attachments.UploadFile(inParam.File, inParam.File_Name, inParam.AB_Rec_ID) add File Upload code here
                End If
            End If
            Return True
        End Function

        ''' <summary>
        ''' Inserts Events Info
        ''' </summary>
        ''' <param name="InEvParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_InsertEvents</remarks>
        Public Shared Function InsertEvents(ByVal InEvParam As Parameter_InsertEvents_Addresses, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InEvParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Address_Book_Events_Info (C_CEN_ID,C_REC_ID,C_MISC_REC_ID," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "'" & InEvParam.AB_Rec_ID & "'," & _
                                        "'" & InEvParam.Events_Misc_ID & "'," & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InEvParam.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.ADDRESS_BOOK_EVENTS_INFO, OnlineQuery, inBasicParam, InEvParam.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function InsertAddresses_Txn(inParam As Param_Txn_Insert_Addresses, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            'Main Address
            If Not inParam.param_InsertAddresses Is Nothing Then
                If Not Insert(inParam.param_InsertAddresses, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_InsertAddresses_NextYear Is Nothing Then
                If Not Insert(inParam.param_InsertAddresses_NextYear, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'Magazines
            For Each Param As Parameter_InsertMagazine_Addresses In inParam.InsertMagazine
                If Not Param Is Nothing Then InsertMagazine(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertMagazine_Addresses In inParam.InsertMagazine_NextYear
                If Not Param Is Nothing Then InsertMagazine(Param, inBasicParam, RequestTime)
            Next
            'Specialities
            For Each Param As Parameter_InsertSpecialities_Addresses In inParam.InsertSpecialities
                If Not Param Is Nothing Then InsertSpecialities(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertSpecialities_Addresses In inParam.InsertSpecialities_NextYear
                If Not Param Is Nothing Then InsertSpecialities(Param, inBasicParam, RequestTime)
            Next
            'Wings
            For Each Param As Parameter_InsertWings_Addresses In inParam.InsertWings
                If Not Param Is Nothing Then InsertWings(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertWings_Addresses In inParam.InsertWings_NextYear
                If Not Param Is Nothing Then InsertWings(Param, inBasicParam, RequestTime)
            Next
            'Events
            For Each Param As Parameter_InsertEvents_Addresses In inParam.InsertEvents
                If Not Param Is Nothing Then InsertEvents(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertEvents_Addresses In inParam.InsertEvents_NextYear
                If Not Param Is Nothing Then InsertEvents(Param, inBasicParam, RequestTime)
            Next
            '  End Using
            '  txn.Complete()
            '   End Using
            Return True
        End Function

        Public Shared Function UpdateAddresses_Txn(upParam As Param_Txn_Update_Addresses, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.ADDRESS_BOOK, upParam.param_UpdateAddresses.Rec_ID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateAddresses Is Nothing Then
                If Not Update(upParam.param_UpdateAddresses, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdateAddresses_NextYear Is Nothing Then
                If Not Update(upParam.param_UpdateAddresses_NextYear, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'Magazine 
            If Not upParam.RecID_DeleteMagazine Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_MAGAZINE_INFO, "C_REC_ID    ='" & upParam.RecID_DeleteMagazine & "'", inBasicParam)
            End If
            If Not upParam.RecID_DeleteMagazine_NextYear Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_MAGAZINE_INFO, "C_REC_ID    ='" & upParam.RecID_DeleteMagazine_NextYear & "'", inBasicParam)
            End If
            If Not upParam.InsertMagazine Is Nothing Then
                For Each Param As Parameter_InsertMagazine_Addresses In upParam.InsertMagazine
                    If Not Param Is Nothing Then InsertMagazine(Param, inBasicParam, RequestTime, CommonParam)
                Next
            End If
            If Not upParam.InsertMagazine_NextYear Is Nothing Then
                For Each Param As Parameter_InsertMagazine_Addresses In upParam.InsertMagazine_NextYear
                    If Not Param Is Nothing Then InsertMagazine(Param, inBasicParam, RequestTime, CommonParam)
                Next
            End If
            'Wings
            If Not upParam.RecID_DelteWings Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_WING_INFO, "C_REC_ID    ='" & upParam.RecID_DelteWings & "'", inBasicParam)
            End If
            If Not upParam.RecID_DelteWings_NextYear Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_WING_INFO, "C_REC_ID    ='" & upParam.RecID_DelteWings_NextYear & "'", inBasicParam)
            End If
            If Not upParam.InsertWings Is Nothing Then
                For Each Param As Parameter_InsertWings_Addresses In upParam.InsertWings
                    If Not Param Is Nothing Then InsertWings(Param, inBasicParam, RequestTime, CommonParam)
                Next
            End If
            If Not upParam.InsertWings_NextYear Is Nothing Then
                For Each Param As Parameter_InsertWings_Addresses In upParam.InsertWings_NextYear
                    If Not Param Is Nothing Then InsertWings(Param, inBasicParam, RequestTime, CommonParam)
                Next
            End If
            'Speciality
            If Not upParam.RecID_DeleteSpeciality Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_SPECIAL_INFO, "C_REC_ID    ='" & upParam.RecID_DeleteSpeciality & "'", inBasicParam)
            End If
            If Not upParam.RecID_DeleteSpeciality_NextYear Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_SPECIAL_INFO, "C_REC_ID    ='" & upParam.RecID_DeleteSpeciality_NextYear & "'", inBasicParam)
            End If
            If Not upParam.InsertSpecialities Is Nothing Then
                For Each Param As Parameter_InsertSpecialities_Addresses In upParam.InsertSpecialities
                    If Not Param Is Nothing Then InsertSpecialities(Param, inBasicParam, RequestTime, CommonParam)
                Next
            End If
            If Not upParam.InsertSpecialities_NextYear Is Nothing Then
                For Each Param As Parameter_InsertSpecialities_Addresses In upParam.InsertSpecialities_NextYear
                    If Not Param Is Nothing Then InsertSpecialities(Param, inBasicParam, RequestTime, CommonParam)
                Next
            End If
            If Not upParam.RecID_DeleteAdditionalAddress Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_ADDITIONAL_INFO, "C_REC_ID    ='" & upParam.RecID_DeleteAdditionalAddress & "'", inBasicParam)
            End If
            If Not upParam.RecID_DeleteAdditionalAddress_NextYear Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_ADDITIONAL_INFO, "C_REC_ID    ='" & upParam.RecID_DeleteAdditionalAddress_NextYear & "'", inBasicParam)
            End If
            If Not upParam.param_AdditionalAddress Is Nothing Then
                InsertAdditionalParameters(upParam.param_AdditionalAddress, inBasicParam, RequestTime)
            End If
            If Not upParam.param_AdditionalAddress_NextYear Is Nothing Then
                InsertAdditionalParameters(upParam.param_AdditionalAddress_NextYear, inBasicParam, RequestTime)
            End If
            'Events
            If Not upParam.RecID_DeleteEvents Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_EVENTS_INFO, "C_REC_ID    ='" & upParam.RecID_DeleteEvents & "'", inBasicParam)
            End If
            If Not upParam.RecID_DeleteEvents_NextYear Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_EVENTS_INFO, "C_REC_ID    ='" & upParam.RecID_DeleteEvents_NextYear & "'", inBasicParam)
            End If
            If Not upParam.InsertEvents Is Nothing Then
                For Each Param As Parameter_InsertEvents_Addresses In upParam.InsertEvents
                    If Not Param Is Nothing Then InsertEvents(Param, inBasicParam, RequestTime, CommonParam)
                Next
            End If
            If Not upParam.InsertEvents_NextYear Is Nothing Then
                For Each Param As Parameter_InsertEvents_Addresses In upParam.InsertEvents_NextYear
                    If Not Param Is Nothing Then InsertEvents(Param, inBasicParam, RequestTime, CommonParam)
                Next
            End If
            ' End Using
            ' txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function DeleteAddresses_Txn(DelParam As Param_Txn_Delete_Addresses, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            'Using txn
            'Using Common.GetConnectionScope()
            For Each DelSet As Param_Txn_Delete_AddressSet In DelParam.DeleteAddressSets
                If Not DelSet Is Nothing Then
                    '    If Not DelSet.RecID_DeleteMagazine Is Nothing Then
                    '        dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_MAGAZINE_INFO, "C_REC_ID    ='" & DelSet.RecID_DeleteMagazine & "'", inBasicParam)
                    '    End If
                    '    If Not DelSet.RecID_DelteWings Is Nothing Then
                    '        dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_WING_INFO, "C_REC_ID    ='" & DelSet.RecID_DelteWings & "'", inBasicParam)
                    '    End If
                    '    If Not DelSet.RecID_DeleteSpeciality Is Nothing Then
                    '        dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_SPECIAL_INFO, "C_REC_ID    ='" & DelSet.RecID_DeleteSpeciality & "'", inBasicParam)
                    '    End If
                    '    If Not DelSet.RecID_DeleteEvents Is Nothing Then
                    '        dbService.DeleteByCondition(ConnectOneWS.Tables.ADDRESS_BOOK_EVENTS_INFO, "C_REC_ID    ='" & DelSet.RecID_DeleteEvents & "'", inBasicParam)
                    '    End If
                    '    If Not DelSet.RecID_Delete Is Nothing Then
                    '        dbService.Delete(ConnectOneWS.Tables.ADDRESS_BOOK, DelSet.RecID_Delete, inBasicParam)
                    '    End If

                    dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & DelSet.RecID_Delete & "' ", inBasicParam)

                    Dim SPName As String = "sp_Delete_Address_Book"
                    Dim params() As String = {"@cen_id", "@YEARID", "@REC_ID"}
                    Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, DelSet.RecID_Delete}
                    Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
                    Dim lengths() As Integer = {4, 4, 36}
                    dbService.DeleteFromSP(ConnectOneWS.Tables.ADDRESS_BOOK, SPName, params, values, dbTypes, lengths, inBasicParam)
                End If
            Next

           
            Return True
            '  End Using
            '  txn.Complete()
            ' End Using
            Return True
        End Function

        'This function is for inserting the query of addressbook to upload excel data of addresses.
        Public Shared Function Excel_Insert_Query(ByVal InsertQueryParam As Parameter_InsertQuery_ExcelRawDataUpload, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Select Case (InsertQueryParam.table_name.ToUpper())
                Case "ADDRESS_BOOK"
                    dbService.Insert(ConnectOneWS.Tables.ADDRESS_BOOK, InsertQueryParam.query, inBasicParam, InsertQueryParam.RecID, Nothing)
                Case "ACTION_ITEM_INFO"
                    dbService.Insert(ConnectOneWS.Tables.ACTION_ITEM_INFO, InsertQueryParam.query, inBasicParam, InsertQueryParam.RecID, Nothing)
            End Select

            Return True
        End Function

    End Class
#End Region

End Namespace

