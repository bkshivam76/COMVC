
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Profile--"
    <Serializable>
    Public Class Return_PropertyData
        Public Property REC_ID As String
        Public Property Name As String
        Public Property Category As String
        Public Property Use As String
        Public Property Type As String
        Public Property Address As String
        Public Property REC_EDIT_ON As Date
        Public Property Final_Amount As Decimal

    End Class
    <Serializable>
    Public Class Return_LB_Documents
        Public ID As String
        Public Name As String
    End Class
    <Serializable>
    Public Class Return_LB_Owners
        Public Name As String
        Public Organization As String
        Public Status As String
        Public ID As String
        Public REC_EDIT_ON As DateTime
    End Class
    <Serializable>
    Public Class Return_LB_Institution
        Public Name As String
        Public ID As String
        Public Short_Name As String
    End Class
    <Serializable>
    Public Class Return_GetAllPropertyList

        Public LB_ID As String
        Public Institute As String
        Public TYPE As String
        Public PROP_NAME As String
        Public CATEGORY As String
        Public LED_ID As String
        Public CURR_VALUE As Decimal
        Public OWNERSHIP As String
        Public USE_OF_PROPERTY As String
        Public REC_EDIT_ON As DateTime

    End Class
    <Serializable>
    Public Class LandAndBuilding
        Inherits SharedVariables
        <Serializable>
        Public Class Param_Insert_PropertTypeChange
            Public Property LB_REC_ID As String
            Public Property LB_ORG_REC_ID As String
            Public Property TYPE_FROM As String
            Public Property TYPE_TO As String
        End Class
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Get List, Shifted/Replaced
        ''' </summary>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <param name="opt"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal opt As Boolean) As DataTable
            Dim Param As Param_LandAndBuilding_GetList = New Param_LandAndBuilding_GetList()
            Param.ProfileEntry = ProfileEntry
            Param.VoucherEntry = VoucherEntry
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetList, ClientScreen.Profile_LandAndBuilding, Param)
        End Function

        ''' <summary>
        ''' Gets Property Profile Listing 
        ''' </summary>
        ''' <param name="parameter"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetProfileListing(Optional ByVal parameter As RealTimeService.Param_GetProfileListing = Nothing) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Property_GetProfileListing, ClientScreen.Profile_LandAndBuilding, parameter)
        End Function

        Public Function Get_Property_Closing(Optional ByVal parameter As Param_Get_Property_Closing = Nothing) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Property_Get_Property_Closing, ClientScreen.Profile_LandAndBuilding, parameter)
        End Function

        Public Function GetAllPropertyList(ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetAllPropertyList, screen, cBase._open_PAD_No_Main)
        End Function

        Public Function Get_PropertyListingBySP(ByVal YearID As Integer, ByVal Prev_YearID As Integer, Optional ByVal Cen_ID As Integer = Nothing, Optional ByVal LB_Rec_ID As String = Nothing) As List(Of Return_GetAllPropertyList)
            Dim Param As Param_LandAndBuilding_GetPropertyListingBySP = New Param_LandAndBuilding_GetPropertyListingBySP
            Param.Cen_ID = Cen_ID
            'Param.Instt_ID = Instt_ID
            Param.Prev_YearID = Prev_YearID
            Param.YearID = YearID
            Param.LB_Rec_ID = LB_Rec_ID
            Dim ret_table As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Property_Get_PropertyListingBySP, ClientScreen.Profile_LandAndBuilding, Param)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim propertylist = New List(Of Return_GetAllPropertyList)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_GetAllPropertyList()
                    newdata.LB_ID = row.Field(Of String)("LB_ID")
                    newdata.Institute = row.Field(Of String)("Institute")
                    newdata.TYPE = row.Field(Of String)("TYPE")
                    newdata.PROP_NAME = row.Field(Of String)("PROP_NAME")
                    newdata.CATEGORY = row.Field(Of String)("CATEGORY")
                    newdata.LED_ID = row.Field(Of String)("LED_ID")
                    newdata.CURR_VALUE = row.Field(Of Decimal)("CURR_VALUE")
                    newdata.OWNERSHIP = row.Field(Of String)("OWNERSHIP")
                    newdata.USE_OF_PROPERTY = row.Field(Of String)("USE OF PROPERTY")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    propertylist.Add(newdata)
                Next
                Return propertylist
            End If
        End Function

        Public Function Get_Location_Property_ListingBySP(Param As Param_LandAndBuilding_Get_Location_Property_ListingBySP) As List(Of Return_GetAllPropertyList)
            Dim ret_table As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Property_Get_Location_Property_ListingBySP, ClientScreen.Profile_Core, Param)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim propertylist = New List(Of Return_GetAllPropertyList)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_GetAllPropertyList()
                    newdata.LB_ID = row.Field(Of String)("LB_ID")
                    newdata.Institute = row.Field(Of String)("Institute")
                    newdata.TYPE = row.Field(Of String)("TYPE")
                    newdata.PROP_NAME = row.Field(Of String)("PROP_NAME")
                    newdata.CATEGORY = row.Field(Of String)("CATEGORY")
                    newdata.LED_ID = row.Field(Of String)("LED_ID")
                    newdata.CURR_VALUE = row.Field(Of Decimal)("CURR_VALUE")
                    newdata.OWNERSHIP = row.Field(Of String)("OWNERSHIP")
                    newdata.USE_OF_PROPERTY = row.Field(Of String)("USE OF PROPERTY")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    propertylist.Add(newdata)
                Next
                Return propertylist
            End If
        End Function


        ''' <summary>
        ''' Gets List For Expenses, Shifted
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetListForExpenses</remarks>
        Public Function GetListForExpenses(ByVal MasterID As String, ByVal screen As ClientScreen, Optional LB_Rec_Id As String = Nothing) As List(Of Return_PropertyData)
            Dim Param As Param_LandAndBuilding_GetListForExpenses = New Param_LandAndBuilding_GetListForExpenses
            Param.MasterID = MasterID
            Param.LB_Rec_ID = LB_Rec_Id
            Param.Next_Year_ID = cBase._next_Unaudited_YearID
            Param.Prev_Year_ID = cBase._prev_Unaudited_YearID
            Dim ret_table As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetListForExpenses, screen, Param)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim propertyList = New List(Of Return_PropertyData)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_PropertyData()
                    newdata.Address = row.Field(Of String)("Address")
                    newdata.Category = row.Field(Of String)("Category")
                    newdata.Final_Amount = row.Field(Of Decimal)("Final_Amount")
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.REC_EDIT_ON = row.Field(Of Date)("REC_EDIT_ON")
                    newdata.REC_ID = row.Field(Of String)("REC_ID")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Use = row.Field(Of String)("Use")
                    propertyList.Add(newdata)
                Next
                Return propertyList
            End If
        End Function

        ''' <summary>
        ''' Gets List For MOU, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetListForExpenses</remarks>
        Public Function GetListForMOU(ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetListForMOU, screen, cBase._next_Unaudited_YearID)
        End Function

        ''' <summary>
        ''' Gets Extension Details, Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetExtensionDetails</remarks>
        Public Function GetExtensionDetails(ByVal RecID As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetExtensionDetails, screen, RecID)
        End Function

        ''' <summary>
        ''' Gets Building Documents Info, Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetDocsDetails</remarks>
        Public Function GetDocsDetails(ByVal RecID As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetDocsDetails, screen, RecID)
        End Function

        ''' <summary>
        ''' Get List By Condition, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="Cur_Ins_Short_Name"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetListByCondition</remarks>
        Public Function GetListByCondition(ByVal Screen As ClientScreen, ByVal Cur_Ins_Short_Name As String, ByVal OtherCondition As String) As DataTable
            Dim OnlineParam As Param_LandAndBuilding_GetListByCondition = New Param_LandAndBuilding_GetListByCondition()
            OnlineParam.Cur_Ins_Short_Name = Cur_Ins_Short_Name
            OnlineParam.OtherCondition = OtherCondition
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetListByCondition, Screen, OnlineParam)
        End Function

        ''' <summary>
        ''' Manipulated, Common Function, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetListByQuery(ByVal Screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetListByQuery_Common, Screen, parameter)
        End Function

        ''' <summary>
        ''' Gets Transaction Details,Shifted
        ''' </summary>
        ''' <param name="Rec_IDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetTransactions</remarks>
        Public Function GetTransactions(ByVal Rec_IDs As String, YearID As Integer) As DataTable
            Dim oParam As Param_Veh_GetTransactions = New Param_Veh_GetTransactions
            oParam.Rec_IDs = Rec_IDs
            oParam.YearID = YearID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetTransactions, ClientScreen.Profile_GoldSilver, oParam)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_LandAndBuilding, RealTimeService.Tables.LAND_BUILDING_INFO, "LB_CEN_ID")
        End Function

        ' ''' <summary>
        ' ''' Gets Centre Count By Name, Shifted
        ' ''' </summary>
        ' ''' <param name="Name"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.Property_GetCenterCountByName</remarks>
        'Public Function GetCenterCountByName(ByVal Name As String) As Object
        '    Dim Query As String = "SELECT COUNT(REC_ID) FROM Land_Building_Info  WHERE REC_STATUS IN (0,1,2) AND LB_CEN_ID='" & cBase._open_Cen_ID & "' AND UCASE(LB_PRO_NAME)  = '" & Name.ToUpper & "' "
        '    Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Property_GetCenterCountByName, ClientScreen.Profile_LandAndBuilding, Name)
        'End Function

        ''' <summary>
        ''' Gets Center Count By Name And Id, Shifted
        ''' </summary>
        ''' <param name="Name"></param>
        ''' <param name="ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetCenterCountByNameAndId, uses Param_LandAndBuilding_GetCenterCountByName</remarks>
        Public Function GetPropertyrByName(ByVal Name As String, Optional ID As String = "") As Object
            Dim OnlineParam As Param_LandAndBuilding_GetPropertyByName = New Param_LandAndBuilding_GetPropertyByName()
            OnlineParam.Name = Name
            OnlineParam.ID = ID
            OnlineParam.Next_YearId = cBase._next_Unaudited_YearID
            OnlineParam.Prev_YearId = cBase._prev_Unaudited_YearID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Property_GetPropertyByName, ClientScreen.Profile_LandAndBuilding, OnlineParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetMainCenterCountByRecID</remarks>
        Public Function GetMainCenters(Optional RecID As String = "") As DataTable
            Dim PAram As Param_GetMainCenters = New Param_GetMainCenters
            PAram.Asset_RecID = RecID
            PAram.BKPADNo = cBase._open_PAD_No_Main
            PAram.Prev_YearId = cBase._prev_Unaudited_YearID
            PAram.Next_YearId = cBase._next_Unaudited_YearID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Property_GetMainCenters, ClientScreen.Profile_LandAndBuilding, PAram)
        End Function

        'Shifted
        Public Function GetCenterDetails()
            Dim LocalQuery As String = " SELECT IIF(ISNULL(CEN_B_NAME),'',CEN_B_NAME) AS CEN_B_NAME, " &
                                  "        IIF(ISNULL(CEN_ADD1),'',CEN_ADD1) AS CEN_ADD1, " &
                                  "        IIF(ISNULL(CEN_ADD2),'',CEN_ADD2) AS CEN_ADD2, " &
                                  "        IIF(ISNULL(CEN_ADD3),'',CEN_ADD3) AS CEN_ADD3, " &
                                  "        IIF(ISNULL(CEN_ADD4),'',CEN_ADD4) AS CEN_ADD4, " &
                                  "        IIF(ISNULL(CEN_CITY),'',CEN_CITY) AS CEN_CITY, " &
                                  "        IIF(ISNULL(CEN_STATE),'',CEN_STATE) AS CEN_STATE, " &
                                  "        IIF(ISNULL(CEN_DISTRICT),'',CEN_DISTRICT) AS CEN_DISTRICT, " &
                                  "        IIF(ISNULL(CEN_COUNTRY),'',CEN_COUNTRY) AS CEN_COUNTRY,  " &
                                  "        IIF(ISNULL(CEN_PINCODE),'',CEN_PINCODE) AS CEN_PINCODE " &
                                  " FROM CENTRE_INFO WHERE  REC_STATUS IN (0,1,2) AND CEN_ID= '" & cBase._open_Cen_ID_Main & "'"
            Return GetCenterDetailsByQuery(LocalQuery, ClientScreen.Profile_LandAndBuilding, cBase._open_Cen_ID_Main)
        End Function

        'Shifted
        Public Function GetDocumentsList(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String) As List(Of Return_LB_Documents)
            Dim ret_table As DataTable = GetMisc("DOCUMENTS FOR LAND AND BUILDINGS", ClientScreen.Profile_LandAndBuilding, NameColumnHead, RecIdColumnHead)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim doclist = New List(Of Return_LB_Documents)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_LB_Documents()
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Name = row.Field(Of String)("Name")
                    doclist.Add(newdata)
                Next
                Return doclist
            End If
        End Function

        ''' <summary>
        ''' Returns List of Names, Organization and Status, Rec_Id from AddressBook, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetOwners(Optional Owner_Rec_ID As String = Nothing) As List(Of Return_LB_Owners)
            Dim _addresses As Addresses = New Addresses(cBase)
            Dim ret_table As DataTable = _addresses.GetPartiesList(ClientScreen.Profile_LandAndBuilding, Owner_Rec_ID)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim ownerlist = New List(Of Return_LB_Owners)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_LB_Owners()
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Organization = row.Field(Of String)("Organization")
                    newdata.Status = row.Field(Of String)("Status")
                    newdata.REC_EDIT_ON = row.Field(Of Date)("REC_EDIT_ON")
                    ownerlist.Add(newdata)
                Next
                Return ownerlist
            End If
        End Function

        'Shifted
        Public Function GetInstitutes() As DataTable
            Return GetInstituteList(ClientScreen.Profile_LandAndBuilding)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            'Return GetRecordByID(Rec_ID, ClientScreen.Profile_LandAndBuilding, RealTimeService.Tables.LAND_BUILDING_INFO, Common.ClientDBFolderCode.SYS)
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetRecord, ClientScreen.Profile_LandAndBuilding, Rec_ID)
        End Function

        Public Function GetExtendedRecord(ByVal LB_Rec_ID As String) As DataTable
            Return GetRecordByColumn("LB_REC_ID", LB_Rec_ID, ClientScreen.Profile_LandAndBuilding, RealTimeService.Tables.LAND_BUILDING_EXTENDED_INFO)
        End Function

        Public Function GetExtendedRecord(ByVal LB_Rec_ID As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Return GetRecordByColumn("LB_REC_ID", LB_Rec_ID, screen, RealTimeService.Tables.LAND_BUILDING_EXTENDED_INFO)
        End Function

        Public Function GetDocumentRecord(ByVal LB_Rec_ID As String) As DataTable
            Return GetRecordByColumn("LB_REC_ID", LB_Rec_ID, ClientScreen.Profile_LandAndBuilding, RealTimeService.Tables.LAND_BUILDING_DOCUMENTS_INFO)
        End Function

        Public Function GetDocumentRecord(ByVal LB_Rec_ID As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As DataTable
            Return GetRecordByColumn("LB_REC_ID", LB_Rec_ID, screen, RealTimeService.Tables.LAND_BUILDING_DOCUMENTS_INFO)
        End Function

        ''' <summary>
        ''' Gets Transaction Count,Shifted
        ''' </summary>
        ''' <param name="LB_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetTransactionCount</remarks>
        Public Function GetTransactionCount(ByVal LB_ID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Property_GetTransactionCount, ClientScreen.Profile_LandAndBuilding, LB_ID)
        End Function

        ''' <summary>
        ''' Gets IDs By TransactionID, Shifted
        ''' </summary>
        ''' <param name="TxnID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetIDsBytxnID</remarks>
        Public Function GetIDsBytxnID(ByVal TxnID As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetIDsBytxnID, screen, TxnID)
        End Function

        Public Function GetPendingTfs_LocNames(ByVal Cen_Rec_Id As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetPendingTfs_LocNames, ClientScreen.Profile_LandAndBuilding, Cen_Rec_Id)
        End Function

        Public Function GetInsuranceRegisterData(YearId As Integer) As DataTable
            Dim param As Param_GetInsuranceLetterData = New Param_GetInsuranceLetterData
            param.YearID = YearId
            param.BK_PAD_NO = cBase._open_PAD_No_Main
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Report_GetInsuranceLetterData, ClientScreen.Report_InsuranceLetter, param)
        End Function

        Public Function IsPropertyCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_LandAndBuilding, Tables.LAND_BUILDING_INFO)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_LandAndBuilding, Optional Screen As Common_Lib.RealTimeService.ClientScreen = Nothing) As Boolean
            If Not Screen = Nothing Then Screen = ClientScreen.Profile_LandAndBuilding
            Return InsertRecord(RealTimeService.RealServiceFunctions.Property_Insert, Screen, InParam)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_InsertMasterIdSrNo</remarks>
        Public Function InsertWithVoucher(ByVal InParam1 As Parameter_InsertMasterIDAndSrNo_LandAndBuilding, Screen As Common_Lib.RealTimeService.ClientScreen) As Boolean
            'InParam1.YearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Property_InsertMasterIdSrNo, Screen, InParam1)
        End Function

        ''' <summary>
        ''' Inserts Building Extended Info, Shifted
        ''' </summary>
        ''' <param name="InEInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_InsertExtendedInfo</remarks>
        Public Function InsertExtendedInfo(ByVal InEInfo As Parameter_InsertExtendedInfo_LandAndBuilding) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Property_InsertExtendedInfo, ClientScreen.Profile_LandAndBuilding, InEInfo)
        End Function

        ''' <summary>
        ''' Inserts Documents Info, Shifted
        ''' </summary>
        ''' <param name="InDocInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_InsertDocumentsInfo</remarks>
        Public Function InsertDocumentsInfo(ByVal InDocInfo As Parameter_InsertDocInfo_LandAndBuilding) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Property_InsertDocumentsInfo, ClientScreen.Profile_LandAndBuilding, InDocInfo)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_LandAndBuilding) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Profile_LandAndBuilding)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Property_Update, ClientScreen.Profile_LandAndBuilding, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_LandAndBuilding)

            Return DeleteRecord(Rec_Id, Tables.LAND_BUILDING_INFO, ClientScreen.Profile_LandAndBuilding)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal screen As Common_Lib.RealTimeService.ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_LandAndBuilding)

            Return DeleteByCondition("LB_TR_ID    ='" & Rec_Id & "'", Tables.LAND_BUILDING_INFO, screen)
        End Function

        Public Overloads Function DeleteExtendedInfo(ByVal Lb_Rec_Id As String) As Boolean
            Return DeleteByCondition(" LB_REC_ID    ='" & Lb_Rec_Id & "'", Tables.LAND_BUILDING_EXTENDED_INFO, ClientScreen.Profile_LandAndBuilding)
        End Function

        Public Overloads Function DeleteDocumentInfo(ByVal Lb_Rec_Id As String) As Boolean
            Return DeleteByCondition(" LB_REC_ID    ='" & Lb_Rec_Id & "'", Tables.LAND_BUILDING_DOCUMENTS_INFO, ClientScreen.Profile_LandAndBuilding)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Dim Result As Boolean = True
            If Not MyBase.MarkAsComplete(Rec_Id, Tables.LAND_BUILDING_INFO, ClientScreen.Profile_LandAndBuilding) Then Result = False
            If Not MyBase.MarkAsComplete("LB_REC_ID", Rec_Id, Tables.LAND_BUILDING_EXTENDED_INFO, ClientScreen.Profile_LandAndBuilding) Then Result = False
            If Not MyBase.MarkAsComplete("LB_REC_ID", Rec_Id, Tables.LAND_BUILDING_DOCUMENTS_INFO, ClientScreen.Profile_LandAndBuilding) Then Result = False
            Return Result
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.LAND_BUILDING_INFO, ClientScreen.Profile_LandAndBuilding, cBase._data_ConStr_Sys)
            If Locked Then
                Locked = MyBase.MarkAsLocked("LB_REC_ID", Rec_Id, Tables.LAND_BUILDING_EXTENDED_INFO, ClientScreen.Profile_LandAndBuilding)
            End If
            If Locked Then
                Locked = MyBase.MarkAsLocked("LB_REC_ID", Rec_Id, Tables.LAND_BUILDING_DOCUMENTS_INFO, ClientScreen.Profile_LandAndBuilding)
            End If
            Return Locked
        End Function

        Public Function InsertProperty_Txn(ByVal InParam As Param_Txn_InsertProperty_LandAndBuilding) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Property_InsertProperty_Txn, ClientScreen.Profile_LandAndBuilding, InParam)
        End Function

        Public Function UpdateProperty_Txn(ByVal UpParam As Param_Txn_UpdateProperty_LandAndBuilding) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.param_UpdateLandAndBuilding.RecID, ClientScreen.Profile_LandAndBuilding)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Property_UpdateProperty_Txn, ClientScreen.Profile_LandAndBuilding, UpParam)
        End Function
        Public Function UpdateRentDetails_Property_Txn(ByVal UpParam As Parameter_Update_Property_RentDetails) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Property_UpdateRentDetails_Txn, ClientScreen.Profile_LandAndBuilding, UpParam)
        End Function

        Public Function DeleteProperty_Txn(ByVal DelParam As Param_Txn_DeleteProperty_LandAndBuilding) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & DelParam.RecID_Delete & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_LandAndBuilding)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Property_DeleteProperty_Txn, ClientScreen.Profile_LandAndBuilding, DelParam)
        End Function

        Public Function UpdateInsuranceRegister(ByVal InParam As Parameter_Update_Insurance_Register) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Reports_Update_Insurance_Register, ClientScreen.Profile_LandAndBuilding, InParam)
        End Function

        Public Function InsertPropertTypeChangeLog(Inparam As Param_Insert_PropertTypeChange) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_insert_PropertTypeChangeLog]"
            Dim params() As String = {"@LB_REC_ID", "@LB_ORG_REC_ID", "@PTCL_TYPE_FROM", "@PTCL_TYPE_TO", "@REC_ADD_BY"}
            Dim values() As Object = {Inparam.LB_REC_ID, Inparam.LB_ORG_REC_ID, Inparam.TYPE_FROM, Inparam.TYPE_TO, cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.String}
            Dim lengths() As Integer = {36, 36, 255, 255, 255}
            _RealService.InsertBySPPublic(Tables.PROPERTY_TYPE_CHANGE_LOG, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Profile_LandAndBuilding))
            Return True
        End Function
    End Class
#End Region

#Region "--Accounts--"
    <Serializable>
        Public Class Voucher_Property
            Inherits SharedVariables
            Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub

            ' ''' <summary>
            ' ''' Shifted
            ' ''' </summary>
            ' ''' <param name="tag"></param>
            ' ''' <param name="RecID"></param>
            ' ''' <returns></returns>
            ' ''' <remarks>RealServiceFunctions.Property_CheckDuplicateMainCenter</remarks>
            'Public Function CheckDuplicateMainCenter(ByVal tag As Integer, ByVal RecID As String) As Object
            '    Dim Param As Param_Voucher_Property_CheckDuplicateMainCenter = New Param_Voucher_Property_CheckDuplicateMainCenter()
            '    Param.RecID = RecID
            '    Param.tag = tag
            '    Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Property_CheckDuplicateMainCenter, ClientScreen.Accounts_Voucher_Property, Param)
            'End Function

            ''' <summary>
            ''' Check Duplicate Property Names, Shifted
            ''' </summary>
            ''' <param name="tag"></param>
            ''' <param name="RecID"></param>
            ''' <param name="PropertyName"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Property_CheckDuplicatePropertyName</remarks>
            Public Function CheckDuplicatePropertyName(ByVal tag As Integer, ByVal RecID As String, ByVal PropertyName As String, Optional CenID As Integer = Nothing) As Object
                Dim Param As Param_Voucher_Property_CheckDuplicatePropertyName = New Param_Voucher_Property_CheckDuplicatePropertyName()
                Param.PropertyName = PropertyName
                Param.RecID = RecID
                Param.tag = tag
                Param.CenID = CenID
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Property_CheckDuplicatePropertyName, ClientScreen.Accounts_Voucher_Property, Param)
            End Function

            'Shifted
            Public Function GetMainCenterAdd() As DataTable
                Return GetMainCenterAddress(ClientScreen.Accounts_Voucher_Property)
            End Function

            'Shifted
            Public Function GetDocuments() As List(Of Return_LB_Documents)
                Dim ret_table As DataTable = GetMisc("DOCUMENTS FOR LAND AND BUILDINGS", ClientScreen.Accounts_Voucher_Property, "Name", "ID")
                If ret_table Is Nothing Then
                    Return Nothing
                Else
                    Dim doclist = New List(Of Return_LB_Documents)
                    For Each row As DataRow In ret_table.Rows
                        Dim newdata = New Return_LB_Documents()
                        newdata.ID = row.Field(Of String)("ID")
                        newdata.Name = row.Field(Of String)("Name")
                        doclist.Add(newdata)
                    Next
                    Return doclist
                End If
            End Function

            'Shifted
            Public Function GetOwners() As List(Of Return_LB_Owners)
                Dim _addresses As Addresses = New Addresses(cBase)
                Dim ret_table As DataTable = _addresses.GetList(ClientScreen.Accounts_Voucher_Property)
                If ret_table Is Nothing Then
                    Return Nothing
                Else
                    Dim ownerlist = New List(Of Return_LB_Owners)
                    For Each row As DataRow In ret_table.Rows
                        Dim newdata = New Return_LB_Owners()
                        newdata.ID = row.Field(Of String)("ID")
                        newdata.Name = row.Field(Of String)("Name")
                        newdata.Organization = row.Field(Of String)("Organization")
                        newdata.Status = row.Field(Of String)("Status")
                        ownerlist.Add(newdata)
                    Next
                    Return ownerlist
                End If
            End Function

            'Shifted
            Public Function GetInstt(ByVal NameColHead As String, ByVal IDColHead As String, ByVal SNameColHead As String) As DataTable
                Return GetInstituteList(ClientScreen.Accounts_Voucher_Property, NameColHead, IDColHead, SNameColHead)
            End Function

            'Shifted
            Public Function GetInstt() As List(Of Return_LB_Institution)
                Dim ret_table = GetInstituteList(ClientScreen.Accounts_Voucher_Property, "Name", "ID", "Short Name")
                If ret_table Is Nothing Then
                    Return Nothing
                Else
                    Dim inslist = New List(Of Return_LB_Institution)
                    For Each row As DataRow In ret_table.Rows
                        Dim newdata = New Return_LB_Institution()
                        newdata.ID = row.Field(Of String)("ID")
                        newdata.Name = row.Field(Of String)("Name")
                        newdata.Short_Name = row.Field(Of String)("Short Name")
                        inslist.Add(newdata)
                    Next
                    Return inslist
                End If
            End Function
        End Class
#End Region
    End Class
