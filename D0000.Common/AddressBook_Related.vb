'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Profile--"
    <Serializable>
    Public Class ServiceableSouls
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Gets List of Souls, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_ServiceableSouls_GetListOfSouls</remarks>
        Public Function GetListOfSouls() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_ServiceableSouls_GetListOfSouls, ClientScreen.Profile_ServicableSouls)
        End Function
    End Class
    <Serializable>
    Public Class Students
        Inherits SharedVariables

#Region "Parameter Classes"
        <Serializable>
        Public Class Return_StudentList
            Public Property Name As String
            ''' <summary>
            ''' Original Column Name is BK Title
            ''' </summary>
            ''' <returns></returns>
            Public Property BK_Title As String
            Public Property Occupation As String
            ''' <summary>
            ''' Original Field Name is Lokik Date of Birth
            ''' </summary>
            ''' <returns></returns>
            Public Property Lokik_Date_Of_Birth As String
            ''' <summary>
            ''' Original Field Name is Alokik Date of Birth
            ''' </summary>
            ''' <returns></returns>
            Public Property Alokik_Date_Of_Birth As String
            ''' <summary>
            ''' Original Field name is Mobile No
            ''' </summary>
            ''' <returns></returns>
            Public Property MobileNo As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Gets List of Students, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Students_GetListOfStudents</remarks>
        Public Function GetListOfStudents() As List(Of Return_StudentList)
            Dim _Data As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Students_GetListOfStudents, ClientScreen.Profile_ServicableSouls)
            Dim _StudentList As List(Of Return_StudentList) = New List(Of Return_StudentList)
            If (Not (_Data) Is Nothing) Then
                For Each row As DataRow In _Data.Rows
                    Dim newdata = New Return_StudentList
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Occupation = row.Field(Of String)("Occupation")
                    newdata.BK_Title = row.Field(Of String)("BK Title")
                    newdata.Lokik_Date_Of_Birth = row.Field(Of String)("Lokik Date of Birth")
                    newdata.Alokik_Date_Of_Birth = row.Field(Of String)("Alokik Date of Birth")
                    newdata.MobileNo = row.Field(Of String)("Mobile No")
                    _StudentList.Add(newdata)
                Next
            End If
            Return _StudentList
        End Function
    End Class
#End Region

#Region "--Facility--"
    <Serializable>
    Public Class Return_StateList
        Public R_ST_REC_ID As String
        Public R_ST_NAME As String
        Public R_ST_CODE As String
    End Class
    <Serializable>
    Public Class Return_DistrictList
        Public R_DI_REC_ID As String
        Public R_DI_NAME As String
        Public R_DI_CODE As String
    End Class
    <Serializable>
    Public Class Return_CityList
        Public R_CI_REC_ID As String
        Public R_CI_NAME As String
        Public R_CI_CODE As String
    End Class
    <Serializable>
    Public Class Return_CityList_With_StateandCountryCode
        Public R_CI_REC_ID As String
        Public CITY_NAME As String
        Public CITY_CODE As String
        Public STATE_CODE As Int32?
        Public COUNTRY_CODE As String
        Public COUNTRY_NAME As String
        Public STATE_NAME As String
    End Class
    <Serializable>
    Public Class Addresses
            Inherits SharedVariables
            Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub

            ''' <summary>
            ''' Manipulated, Common Function, Shifted
            ''' </summary>
            ''' <param name="screen"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetList_Common</remarks>
            Public Function GetList(ByVal screen As ClientScreen, Optional ByVal parameter As Parameter_Addresses_GetList_Common = Nothing) As DataTable
                If Not cBase._prefer_show_acc_party_only Is Nothing Then parameter.ShowAccountingPartyOnly = cBase._prefer_show_acc_party_only
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetList_Common, screen, parameter)
            End Function

        ''' <summary>
        ''' Returns Address List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetList</remarks>
        ''' ''' This function is replaced by the function GetAddressBookListing. To be deleted later.
        Public Function GetList() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetList, ClientScreen.Facility_AddressBook, cBase._prefer_show_acc_party_only)
        End Function
        Public Function GetAddressBookListing(Optional ShowAttachmentIndicator As Boolean = False, Optional ShowVouchingIndicator As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_get_Address_Book_Listing"
            Dim params() As String = {"CENID", "YEARID", "@ACC_PARTY", "@UserID", "@SHOW_ATTACHMENT_STATUS", "@SHOW_VOUCH_STATUS"}
            Dim values() As Object = {cBase._open_Cen_ID, cBase._open_Year_ID, IIf(cBase._prefer_show_acc_party_only.HasValue, True, False), cBase._open_User_ID, ShowAttachmentIndicator, ShowVouchingIndicator}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.String, Data.DbType.Boolean, Data.DbType.Boolean}
            Dim lengths() As Integer = {4, 4, 1, 255, 1, 1}
            Return _RealService.ListFromSP(Tables.ADDRESS_BOOK, SPName, Tables.ADDRESS_BOOK.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_AddressBook))
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
                Return GetRecordStatus(Rec_Id, ClientScreen.Facility_AddressBook, RealTimeService.Tables.ADDRESS_BOOK, "C_CEN_ID")
            End Function

            ''' <summary>
            ''' Gets Address Usage Count, Shifted
            ''' </summary>
            ''' <param name="TableName"></param>
            ''' <param name="AB_Rec_ID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetAddressUsageCount</remarks>
            Public Function GetAddressUsageCount(ByVal TableName As Tables, ByVal AB_Rec_ID As String) As DataTable
                Dim Param As Param_GetAddressUsageCount = New Param_GetAddressUsageCount
                Param.AB_Rec_ID = AB_Rec_ID
                Param.TableName = TableName
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetAddressUsageCount, ClientScreen.Facility_AddressBook, Param)
            End Function

            ''' <summary>
            ''' Gets Original Address RecId
            ''' </summary>
            ''' <param name="AB_Rec_ID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetOriginalAddressRecID</remarks>
            Public Function GetAddressRecID(ByVal AB_Rec_ID As String, yearID As Integer) As Object
                Dim Param As Param_GetAddressRecID = New Param_GetAddressRecID
                Param.AB_RecID = AB_Rec_ID
                Param.YearID = yearID
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetAddressRecID, ClientScreen.Facility_AddressBook, Param)
            End Function

            ''' <summary>
            ''' Gets RecIDs for all years for provided party rec ID 
            ''' </summary>
            ''' <param name="AB_Rec_ID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetAddressRecIDs_ForAllYears(ByVal AB_Rec_ID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetAddressRecIDs_ForAllYears, ClientScreen.Facility_AddressBook, AB_Rec_ID)
            End Function

            ''' <summary>
            ''' Gets RecIDs for current year for provided party Org rec ID 
            ''' </summary>
            ''' <param name="AB_Org_ID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetAddressRecID_ForOrgID(ByVal AB_Org_ID As String) As String
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetAddressRecID_FromOrgID, ClientScreen.Facility_AddressBook, AB_Org_ID)
            End Function
        Public Function GetAddressBookServiceUserDetails(ABID As String) As DataTable
            Dim query As String = "Select * from SERVICE_USERS_INFO where ab_id='" & ABID & "'"
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_AddressBook)
            Return _RealService.List(Tables.SERVICE_USERS_INFO, query, Tables.SERVICE_USERS_INFO.ToString, inbasicparam)
        End Function
        Public Function Get_service_users_From_Contact_Details(Mob1 As String, Mob2 As String, Email1 As String, Email2 As String, Optional Mob3 As String = Nothing, Optional Mob4 As String = Nothing, Optional Mob5 As String = Nothing, Optional Email3 As String = Nothing, Optional Email4 As String = Nothing, Optional Email5 As String = Nothing) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@Mob1", "@Mob2", "@Mob3", "@Mob4", "@Mob5", "@Email1", "@Email2", "@Email3", "@Email4", "@Email5"}
            Dim values() As Object = {Mob1, Mob2, Mob3, Mob4, Mob5, Email1, Email2, Email3, Email4, Email5}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {15, 15, 15, 15, 15, 100, 100, 100, 100, 100}
            Return _RealService.ListFromSP(Tables.ADDRESS_BOOK, "sp_get_service_users_From_UserProfile_Details", Tables.ADDRESS_BOOK.ToString(),
                                                              paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ServiceReport))
        End Function
        ''' <summary>
        ''' Gets Duplicates Count by Params provided 
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetAddressUsageCount</remarks>
        Public Function GetDuplicateColumnMsg(ByVal Param As Param_Get_Duplicates) As String
                Dim DTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetDuplicateCount, ClientScreen.Facility_AddressBook, Param)
                If DTable Is Nothing Then Return ""
                If DTable.Rows.Count = 0 Then Return ""
                Dim Msg As String = ""
                Dim Ctr As Int16 = 1
                For Each cRow As DataRow In DTable.Rows
                If Msg = "" Then Msg = "Sorry! Some Fields are seemingly present In below given records!!" & "<br>"
                Msg = Msg & Ctr.ToString & ". Contact Name : " & cRow("Contact") & ", Duplicate Data Field : " & cRow("Duplicate") & "<br>"
                Ctr += 1
                Next
                'If DTable.Rows(0)("C_PAN_NO").ToString.Length > 0 And DTable.Rows(0)("C_PAN_NO").ToString = Param.PAN Then Return "Sorry! Contact " & DTable.Rows(0)("C_NAME").ToString & " already uses same PAN No!!"
                'If DTable.Rows(0)("C_PASSPORT_NO").ToString.Length > 0 And DTable.Rows(0)("C_PASSPORT_NO").ToString = Param.Passport Then Return "Sorry! Contact " & DTable.Rows(0)("C_NAME").ToString & " already uses same Passport No!!"
                'If DTable.Rows(0)("C_VAT_TIN_NO").ToString.Length > 0 And DTable.Rows(0)("C_VAT_TIN_NO").ToString = Param.VAT_TIN Then Return "Sorry! Contact " & DTable.Rows(0)("C_NAME").ToString & " already uses same VAT TIN No!!"
                'If DTable.Rows(0)("C_CST_TIN_NO").ToString.Length > 0 And DTable.Rows(0)("C_CST_TIN_NO").ToString = Param.CST_TIN Then Return "Sorry! Contact " & DTable.Rows(0)("C_NAME").ToString & " already uses same CST TIN No!!"
                'If DTable.Rows(0)("C_TAN_NO").ToString.Length > 0 And DTable.Rows(0)("C_TAN_NO").ToString = Param.TAN Then Return "Sorry! Contact " & DTable.Rows(0)("C_NAME").ToString & " already uses same TAN No!!"
                'If DTable.Rows(0)("C_UID_NO").ToString.Length > 0 And DTable.Rows(0)("C_UID_NO").ToString = Param.UID Then Return "Sorry! Contact " & DTable.Rows(0)("C_NAME").ToString & " already uses same UID No!!"
                'If DTable.Rows(0)("C_STR_NO").ToString.Length > 0 And DTable.Rows(0)("C_STR_NO").ToString = Param.STR_NO Then Return "Sorry! Contact " & DTable.Rows(0)("C_NAME").ToString & " already uses same STR No!!"
                'If DTable.Rows(0)("C_NAME").ToString.Length > 0 Then Return "Sorry! There Already Exists Contact with same Name!!"
                Return Msg
            End Function

            ''' <summary>
            ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesList</remarks>
            Public Function GetPartiesList(ByVal screen As ClientScreen, Optional Party_Rec_ID As String = Nothing) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesList, screen, Party_Rec_ID)
            End Function

            ''' <summary>
            ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook for specified RecIDs, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesListForSpecifiedRecIds</remarks>
            Public Function GetPartiesListByRecIDs(ByVal screen As ClientScreen, ByVal RecIDs As String) As DataTable
                'Screen :ClientScreen.Profile_Report
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetPartiesListForSpecifiedRecIds, screen, RecIDs)
            End Function

            ''' <summary>
            ''' Shifted
            ''' </summary>
            ''' <param name="MiscNameColumnHead"></param>
            ''' <param name="RecIDColumnHead"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetAllMasters(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
                Return GetMiscDetails("'TITLE','OCCUPATION','STATUS CATEGORY','CONTACT MODE','EVENTS','MAGAZINE','SPECIALTIES','DESGINATIONS','QUALIFICATIONS','BLANK'", ClientScreen.Facility_AddressBook, MiscNameColumnHead, RecIDColumnHead)
            End Function

            'AddressBookRelated_GetOrgList
            Public Function GetOrgList() As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_GetOrgList, ClientScreen.Facility_AddressBook)
            End Function

            ''' <summary>
            ''' Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetWings() As DataTable
                Return GetWingsList(ClientScreen.Facility_AddressBook)
            End Function

            Public Function GetRecord(ByVal Rec_ID As String) As DataTable
                Return GetRecordByID(Rec_ID, ClientScreen.Facility_AddressBook, RealTimeService.Tables.ADDRESS_BOOK, Common.ClientDBFolderCode.SYS)
            End Function

            ''' <summary>
            ''' Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetLastEditTime(ByVal Rec_ID As String, ByVal Screen As Common_Lib.RealTimeService.ClientScreen) As Object
                Return GetLastEditOn(Rec_ID, Screen, RealTimeService.Tables.ADDRESS_BOOK, cBase._data_ConStr_Sys)
            End Function

            Public Function GetMagazineRecords(ByVal AB_Rec_ID As String) As DataTable
                Return GetRecordByColumn("C_REC_ID", AB_Rec_ID, ClientScreen.Facility_AddressBook, RealTimeService.Tables.ADDRESS_BOOK_MAGAZINE_INFO)
            End Function

            Public Function GetEventRecords(ByVal AB_Rec_ID As String) As DataTable
                Return GetRecordByColumn("C_REC_ID", AB_Rec_ID, ClientScreen.Facility_AddressBook, RealTimeService.Tables.ADDRESS_BOOK_EVENTS_INFO)
            End Function

            Public Function GetSpecialityRecords(ByVal AB_Rec_ID As String) As DataTable
                Return GetRecordByColumn("C_REC_ID", AB_Rec_ID, ClientScreen.Facility_AddressBook, RealTimeService.Tables.ADDRESS_BOOK_SPECIAL_INFO)
            End Function

            Public Function GetWingRecords(ByVal AB_Rec_ID As String) As DataTable
                Return GetRecordByColumn("C_REC_ID", AB_Rec_ID, ClientScreen.Facility_AddressBook, RealTimeService.Tables.ADDRESS_BOOK_WING_INFO)
            End Function

            ''' <summary>
            ''' Gets Countries List, Shifted
            ''' </summary>
            ''' <param name="NameColumnHead"></param>
            ''' <param name="CodeColumnHead"></param>
            ''' <param name="IDColumnHead"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetCountries(ByVal NameColumnHead As String, ByVal CodeColumnHead As String, ByVal IDColumnHead As String) As DataTable
                Return GetCountriesList(ClientScreen.Facility_AddressBook, NameColumnHead, CodeColumnHead, IDColumnHead)
            End Function

        ''' <summary>
        ''' Gets States, Shifted
        ''' </summary>
        ''' <param name="CountryCode"></param>
        ''' <param name="NameColumnHead"></param>
        ''' <param name="CodeColumnHead"></param>
        ''' <param name="IDColumnHead"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>Get
        Public Function GetAllState() As DataTable
            Dim Query As String = " SELECT ST_NAME, ST_CODE, CO_CODE, REC_ID FROM Map_State_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " order by CO_CODE, ST_NAME"
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartInfo)
            Return _RealService.List(Tables.MAP_STATE_INFO, Query, Tables.MAP_STATE_INFO.ToString, inbasicparam)
        End Function
        Public Function GetStates(ByVal CountryCode As String, ByVal NameColumnHead As String, ByVal CodeColumnHead As String, ByVal IDColumnHead As String) As List(Of Return_StateList)
            Dim ret_table As DataTable = GetStatesList(ClientScreen.Facility_AddressBook, CountryCode, NameColumnHead, CodeColumnHead, IDColumnHead)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim statelist = New List(Of Return_StateList)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_StateList()
                    If ret_table.Columns.Contains("R_ST_REC_ID") Then
                        newdata.R_ST_REC_ID = row.Field(Of String)("R_ST_REC_ID")
                    Else
                        newdata.R_ST_REC_ID = row.Field(Of String)("O_ST_REC_ID")
                    End If
                    If ret_table.Columns.Contains("R_ST_NAME") Then
                        newdata.R_ST_NAME = row.Field(Of String)("R_ST_NAME")
                    Else
                        newdata.R_ST_NAME = row.Field(Of String)("O_ST_NAME")
                    End If
                    If ret_table.Columns.Contains("R_ST_CODE") Then
                        newdata.R_ST_CODE = row.Field(Of Int32)("R_ST_CODE")
                    Else
                        newdata.R_ST_CODE = row.Field(Of Int32)("O_ST_CODE")
                    End If
                    statelist.Add(newdata)
                Next
                Return statelist
            End If
        End Function

        ''' <summary>
        ''' Gets Districts, Shifted
        ''' </summary>
        ''' <param name="CountryCode"></param>
        ''' <param name="StateCode"></param>
        ''' <param name="NameColumnHead"></param>
        ''' <param name="IDColumnHead"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetDistricts(ByVal CountryCode As String, ByVal StateCode As String, ByVal NameColumnHead As String, ByVal IDColumnHead As String) As List(Of Return_DistrictList)
                Dim ret_table As DataTable = GetDistrictsList(ClientScreen.Facility_AddressBook, CountryCode, StateCode, NameColumnHead, IDColumnHead)
                If ret_table Is Nothing Then
                    Return Nothing
                Else
                    Dim districtlist = New List(Of Return_DistrictList)
                    For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_DistrictList()
                    If ret_table.Columns.Contains("R_DI_REC_ID") Then
                        newdata.R_DI_REC_ID = row.Field(Of String)("R_DI_REC_ID")
                    Else
                        newdata.R_DI_REC_ID = row.Field(Of String)("O_DI_REC_ID")
                    End If
                    If ret_table.Columns.Contains("R_DI_NAME") Then
                            newdata.R_DI_NAME = row.Field(Of String)("R_DI_NAME")
                        Else
                            newdata.R_DI_NAME = row.Field(Of String)("O_DI_NAME")
                        End If

                    districtlist.Add(newdata)
                    Next
                    Return districtlist
                End If
            End Function

        ''' <summary>
        ''' Gets Cities  by State and Country Code, Shifted
        ''' </summary>
        ''' <param name="CountryCode"></param>
        ''' <param name="StateCode"></param>
        ''' <param name="NameColumnHead"></param>
        ''' <param name="IDColumnHead"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCitiesBySt_Co_Code(ByVal CountryCode As String, ByVal StateCode As String, ByVal NameColumnHead As String, ByVal IDColumnHead As String) As List(Of Return_CityList)
            Dim ret_table As DataTable = GetCitiesList(ClientScreen.Facility_AddressBook, CountryCode, StateCode, NameColumnHead, IDColumnHead)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim citylist = New List(Of Return_CityList)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_CityList()
                    If ret_table.Columns.Contains("R_CI_REC_ID") Then
                        newdata.R_CI_REC_ID = row.Field(Of String)("R_CI_REC_ID")
                    Else
                        newdata.R_CI_REC_ID = row.Field(Of String)("O_CI_REC_ID")
                    End If
                    If ret_table.Columns.Contains("R_CI_NAME") Then
                        newdata.R_CI_NAME = row.Field(Of String)("R_CI_NAME")
                    Else
                        newdata.R_CI_NAME = row.Field(Of String)("O_CI_NAME")
                    End If
                    citylist.Add(newdata)
                Next
                Return citylist
            End If
        End Function

        ''' <summary>
        ''' Gets Cities by Country Code, Shifted
        ''' </summary>
        ''' <param name="CountryCode"></param>
        ''' <param name="NameColumnHead"></param>
        ''' <param name="IDColumnHead"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCitiesByCO_Code(ByVal CountryCode As String, ByVal NameColumnHead As String, ByVal IDColumnHead As String) As List(Of Return_CityList)
            Dim ret_table = GetCitiesList(ClientScreen.Facility_AddressBook, CountryCode, NameColumnHead, IDColumnHead)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim citylist = New List(Of Return_CityList)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_CityList()
                    newdata.R_CI_REC_ID = row.Field(Of String)("R_CI_REC_ID")
                    newdata.R_CI_NAME = row.Field(Of String)("R_CI_NAME")
                    newdata.R_CI_CODE = row.Field(Of String)("R_CI_CODE")
                    citylist.Add(newdata)
                Next
                Return citylist
            End If
        End Function
        Public Function GetCitiesWithSTandCO_Code(ByVal CountryCode As String, ByVal StateCode As String, ByVal NameColumnHead As String, ByVal IDColumnHead As String) As List(Of Return_CityList_With_StateandCountryCode)

            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim query As String = Nothing
            Dim inbasicparam As RealTimeService.Basic_Param = GetBaseParams(ClientScreen.Facility_ChartInfo)
            If String.IsNullOrWhiteSpace(CountryCode) Then
                query = "SELECT city.CI_NAME AS CITY_NAME, city.REC_ID as R_CI_REC_ID, city.ST_CODE as STATE_CODE, city.CO_CODE as COUNTRY_CODE, country.CO_NAME as COUNTRY_NAME, states.ST_NAME as STATE_NAME FROM Map_City_Info city LEFT JOIN map_state_info states ON city.ST_CODE = states.ST_CODE LEFT JOIN map_country_info country ON city.CO_CODE = country.CO_CODE WHERE  city.REC_STATUS <> -1 order by city.CI_NAME"
            ElseIf String.IsNullOrWhiteSpace(StateCode) Then
                query = "SELECT city.CI_NAME AS CITY_NAME, city.REC_ID as R_CI_REC_ID, city.ST_CODE as STATE_CODE, city.CO_CODE as COUNTRY_CODE, country.CO_NAME as COUNTRY_NAME, states.ST_NAME as STATE_NAME FROM Map_City_Info city LEFT JOIN map_state_info states ON city.ST_CODE = states.ST_CODE LEFT JOIN map_country_info country ON city.CO_CODE = country.CO_CODE WHERE  city.REC_STATUS <> -1 AND city.CO_CODE='" & CountryCode & "' order by city.CI_NAME"
            Else
                query = "SELECT city.CI_NAME AS CITY_NAME, city.REC_ID as R_CI_REC_ID, city.ST_CODE as STATE_CODE, city.CO_CODE as COUNTRY_CODE, country.CO_NAME as COUNTRY_NAME, states.ST_NAME as STATE_NAME FROM Map_City_Info city LEFT JOIN map_state_info states ON city.ST_CODE = states.ST_CODE LEFT JOIN map_country_info country ON city.CO_CODE = country.CO_CODE WHERE  city.REC_STATUS <> -1 AND city.CO_CODE='" & CountryCode & "' AND city.ST_CODE='" & StateCode & "' order by city.CI_NAME"
            End If
            Dim ret_table = _RealService.List(Tables.MAP_CITY_INFO, query, Tables.MAP_CITY_INFO.ToString, inbasicparam)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim citylist = New List(Of Return_CityList_With_StateandCountryCode)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_CityList_With_StateandCountryCode()
                    newdata.R_CI_REC_ID = If(IsDBNull(row("R_CI_REC_ID")), String.Empty, row.Field(Of String)("R_CI_REC_ID"))
                    newdata.CITY_NAME = If(IsDBNull(row("CITY_NAME")), String.Empty, row.Field(Of String)("CITY_NAME"))
                    newdata.STATE_CODE = If(IsDBNull(row("STATE_CODE")), Nothing, row.Field(Of System.Int32)("STATE_CODE"))
                    newdata.COUNTRY_CODE = If(IsDBNull(row("COUNTRY_CODE")), String.Empty, row.Field(Of String)("COUNTRY_CODE"))
                    newdata.COUNTRY_NAME = If(IsDBNull(row("COUNTRY_NAME")), String.Empty, row.Field(Of String)("COUNTRY_NAME"))
                    newdata.STATE_NAME = If(IsDBNull(row("STATE_NAME")), String.Empty, row.Field(Of String)("STATE_NAME"))

                    ' newdata.R_CI_CODE = If(IsDBNull(row("R_CI_CODE")), String.Empty, row.Field(Of String)("R_CI_CODE"))
                    citylist.Add(newdata)
                Next
                Return citylist
            End If
        End Function

        ''' <summary>
        ''' Gets Main Centre List with InsId=00001 filter, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetCenterList</remarks>
        Public Function GetCenterList() As DataTable
                Dim LocalQuery As String = "  SELECT M.CEN_NAME ,C.CEN_BK_PAD_NO ,   M.CEN_INCHARGE , M.CEN_ZONE_ID , C.CEN_ID  " &
                                       "  FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " &
                                       "  Where   C.REC_STATUS  IN (0,1,2) AND M.REC_STATUS  IN (0,1,2) AND  M.CEN_MAIN=TRUE AND C.[CEN_INS_ID]='00001' "

                Dim OnlineQuery As String = " SELECT M.CEN_NAME ,C.CEN_BK_PAD_NO ,   M.CEN_INCHARGE , M.CEN_ZONE_ID , C.CEN_ID  " &
                                        " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " &
                                        " Where   C.REC_STATUS  IN (0,1,2) AND M.REC_STATUS  IN (0,1,2) AND  M.CEN_MAIN=1 AND C.CEN_INS_ID='00001' "
                Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetCenterList, LocalQuery, ClientScreen.Facility_AddressBook, Tables.CENTRE_INFO)
            End Function

            ''' <summary>
            ''' Gets Overseas Centre List, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_GetOverseasCenterList</remarks>
            Public Function GetOverseasCenterList() As DataTable
                Dim LocalQuery As String = "  SELECT C.CEN_NAME ,C.CEN_BK_PAD_NO ,   C.CEN_INCHARGE , C.CEN_ZONE_ID , C.CEN_ID  " &
                                       "  FROM OVERSEAS_CENTRE_INFO AS C " &
                                       "  Where   C.REC_STATUS  IN (0,1,2) "
                Dim OnlineQuery As String = " SELECT C.CEN_NAME ,C.CEN_BK_PAD_NO ,   C.CEN_INCHARGE , C.CEN_ZONE_ID , C.CEN_ID  " &
                                        " FROM OVERSEAS_CENTRE_INFO AS C  " &
                                        " Where   C.REC_STATUS  IN (0,1,2) "
                Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_GetOverseasCenterList, LocalQuery, ClientScreen.Facility_AddressBook, RealTimeService.Tables.OVERSEAS_CENTRE_INFO)
            End Function

            ''' <summary>
            ''' Inserts Address, Shifted
            ''' </summary>
            ''' <param name="InParam"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_Insert</remarks>
            Public Function Insert(ByVal InParam As Parameter_Insert_Addresses) As Boolean
                Return InsertRecord(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_Insert, ClientScreen.Facility_AddressBook, InParam)
            End Function

            ''' <summary>
            ''' Updates Address, Shifted
            ''' </summary>
            ''' <param name="UpParam"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_Update</remarks>
            Public Function Update(ByVal UpParam As Parameter_Update_Addresses) As Boolean
                Return UpdateRecord(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_Update, ClientScreen.Facility_AddressBook, UpParam)
            End Function

        ''' <summary>
        ''' Insert Magazine,Shifted
        ''' </summary>
        ''' <param name="InMagParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_InsertMagazine</remarks>
        Public Function InsertMagazine(ByVal InMagParam As Parameter_InsertMagazine_Addresses) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_InsertMagazine, ClientScreen.Facility_AddressBook, InMagParam)
        End Function
        ''' <summary>
        ''' Insert Addressess from excel file to address_book table through insert query
        ''' </summary>
        ''' <param name="InsertQueryParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Excel_Insert_Query</remarks>
        Public Function Excel_Insert_Query(ByVal InsertQueryParam As Parameter_InsertQuery_ExcelRawDataUpload) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.AddressBookRelated_Excel_Insert_Query, ClientScreen.Facility_AddressBook, InsertQueryParam)
        End Function

        ''' <summary>
        ''' Inserts Wings Information, Shifted
        ''' </summary>
        ''' <param name="InWinParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_InsertWings</remarks>
        Public Function InsertWings(ByVal InWinParam As Parameter_InsertWings_Addresses) As Boolean
                Return InsertRecord(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_InsertWings, ClientScreen.Facility_AddressBook, InWinParam)
            End Function

            ''' <summary>
            ''' Inserts Specialities Info, Shifted
            ''' </summary>
            ''' <param name="InSpeParam"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_InsertSpecialities</remarks>
            Public Function InsertSpecialities(ByVal InSpeParam As Parameter_InsertSpecialities_Addresses) As Boolean
                Return InsertRecord(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_InsertSpecialities, ClientScreen.Facility_AddressBook, InSpeParam)
            End Function

            ''' <summary>
            ''' Inserts Events Info, Shifted
            ''' </summary>
            ''' <param name="InEvParam"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.AddressBookRelated_Addresses_InsertEvents</remarks>
            Public Function InsertEvents(ByVal InEvParam As Parameter_InsertEvents_Addresses) As Boolean
                Return InsertRecord(RealTimeService.RealServiceFunctions.AddressBookRelated_Addresses_InsertEvents, ClientScreen.Facility_AddressBook, InEvParam)
            End Function

            Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
                Return DeleteRecord(Rec_Id, Tables.ADDRESS_BOOK, ClientScreen.Facility_AddressBook)
            End Function

            Public Function DeleteMagazine(ByVal Rec_Id As String) As Boolean
                Return DeleteByCondition("C_REC_ID    ='" & Rec_Id & "'", Tables.ADDRESS_BOOK_MAGAZINE_INFO, ClientScreen.Facility_AddressBook)
            End Function

            Public Function DeleteWings(ByVal Rec_Id As String) As Boolean
                Return DeleteByCondition("C_REC_ID    ='" & Rec_Id & "'", Tables.ADDRESS_BOOK_WING_INFO, ClientScreen.Facility_AddressBook)
            End Function

            Public Function DeleteSpeciality(ByVal Rec_Id As String) As Boolean
                Return DeleteByCondition("C_REC_ID    ='" & Rec_Id & "'", Tables.ADDRESS_BOOK_SPECIAL_INFO, ClientScreen.Facility_AddressBook)
            End Function

            Public Function DeleteEvents(ByVal Rec_Id As String) As Boolean
                Return DeleteByCondition("C_REC_ID    ='" & Rec_Id & "'", Tables.ADDRESS_BOOK_EVENTS_INFO, ClientScreen.Facility_AddressBook)
            End Function

            Public Function InsertAddresses_Txn(InParam As Param_Txn_Insert_Addresses) As Boolean
                Return ExecuteGroup(RealTimeService.RealServiceFunctions.Addresses_InsertAddresses_Txn, ClientScreen.Facility_AddressBook, InParam)
            End Function

            Public Function UpdateAddresses_Txn(UpParam As Param_Txn_Update_Addresses) As Boolean
                Return ExecuteGroup(RealTimeService.RealServiceFunctions.Addresses_UpdateAddresses_Txn, ClientScreen.Facility_AddressBook, UpParam)
            End Function

            Public Function DeleteAddresses_Txn(DelParam As Param_Txn_Delete_Addresses) As Boolean
                Return ExecuteGroup(RealTimeService.RealServiceFunctions.Addresses_DeleteAddresses_Txn, ClientScreen.Facility_AddressBook, DelParam)
            End Function
        Public Function MergeParties(InParam As Param_MergeParties) As String
            Return GetScalarBySP(RealTimeService.RealServiceFunctions.Addresses_MergeParties, ClientScreen.Facility_AddressBook, InParam)
        End Function
        Public Function GetEmailIDs(Reference As String, Optional StartAlphabet As String = "1", Optional EndAlphabet As String = "Z") As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim DataBase_xls As DataTable = _RealService.List(Tables.ADDRESS_BOOK, "SELECT C_EMAIL_ID_1, C_EMAIL_ID_2 FROM address_book WHERE C_REF = '" + Reference + "' AND C_CEN_ID = " + cBase._open_Cen_ID.ToString() + "  AND C_COD_YEAR_ID =" + cBase._open_Year_ID.ToString() + " AND LEN(COALESCE(C_EMAIL_ID_1,''))>0 AND LEFT(LTRIM(C_EMAIL_ID_1),1) >= '" & StartAlphabet & "' AND LEFT(LTRIM(C_EMAIL_ID_1),1) <= '" & EndAlphabet & "' ORDER BY LEFT(LTRIM(C_EMAIL_ID_1),1)", Tables.ADDRESS_BOOK.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Return DataBase_xls
        End Function

        Public Function GetEmailIDs(Optional StartAlphabet As String = "1", Optional EndAlphabet As String = "Z") As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim DataBase_xls As DataTable = _RealService.List(Tables.ADDRESS_BOOK, "SELECT C_EMAIL_ID_1, C_EMAIL_ID_2 FROM address_book WHERE C_CEN_ID = " + cBase._open_Cen_ID.ToString() + "  AND C_COD_YEAR_ID =" + cBase._open_Year_ID.ToString() + " AND LEN(COALESCE(C_EMAIL_ID_1,''))>0 AND LEFT(LTRIM(C_EMAIL_ID_1),1) >= '" & StartAlphabet & "' AND LEFT(LTRIM(C_EMAIL_ID_1),1) <= '" & EndAlphabet & "' ORDER BY LEFT(LTRIM(C_EMAIL_ID_1),1)", Tables.ADDRESS_BOOK.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Return DataBase_xls
        End Function

        Public Function Insert_LocalAddress(ByVal AB_ID As String) As Integer
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_insert_address_book_copy"
            Dim params() As String = {"@AB_ID", "@NEW_CEN_ID", "@USERNAME", "@OPEN_FY_ID"}
            Dim values() As Object = {AB_ID, cBase._open_Cen_ID, cBase._open_User_ID, cBase._open_Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {36, 4, 255, 4}
            Dim Insertion_Result As Integer = _RealService.InsertBySPPublic(Tables.ADDRESS_BOOK, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_AddressBook))
            Return Insertion_Result
            'Return _RealService.ListFromSP(Tables.ADDRESS_BOOK, SPName, Tables.ADDRESS_BOOK.ToString(), params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_AddressBook))
        End Function
    End Class
#End Region
    End Class
