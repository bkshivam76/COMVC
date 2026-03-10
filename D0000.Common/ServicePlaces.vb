'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Profile--"
    <Serializable>
    Public Class ServicePlaces
        Inherits SharedVariables

#Region "Parameter Classes"
        <Serializable>
        Public Class Return_ServicePlace
            Inherits CommonReturnFields
            ''' <summary>
            ''' Actual Field Name is Service Place Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Service_Place_Name As String
            Public Property Type As String
            ''' <summary>
            ''' Actual Field Name is Start Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Start_Date As DateTime
            Public Property Weekdays As String
            Public Property Timing As String
            Public Property Status As String
            ''' <summary>
            ''' Actual Field name is Place Owner
            ''' </summary>
            ''' <returns></returns>
            Public Property Place_Owner As String
            ''' <summary>
            ''' Actual field name is Place Address
            ''' </summary>
            ''' <returns></returns>
            Public Property Place_Address As String
            Public Property CITYID As String
            Public Property R_City As String
            ''' <summary>
            ''' Actual Field name is Place Contact No
            ''' </summary>
            ''' <returns></returns>
            Public Property Place_Contact_No As String
            ''' <summary>
            ''' Actual Field name is Responsible Person
            ''' </summary>
            ''' <returns></returns>
            Public Property Responsible_Person As String
            Public Property Contact_No As String
            Public Property Other_Details As String
            Public Property YearID As Integer
            Public Property ORG_REC As String
            Public Property LOC_ID As String
            Public Property iREQ_ATTACH_COUNT As Int32?
            Public Property iCOMPLETE_ATTACH_COUNT As Int32?
            Public Property iRESPONDED_COUNT As Int32?
            Public Property iREJECTED_COUNT As Int32?
            Public Property iOTHER_ATTACH_CNT As Int32?
            Public Property iALL_ATTACH_CNT As Int32?

            'Added for Audit Icon Filter
            Public Property iIcon As String

        End Class
#End Region
        <Serializable>
        Public Class Return_GetAllServicePlaceList

            Public SP_ID As String
            Public Institute As String
            Public Centre_No As String
            Public UID As String
            Public No As String
            Public Service_Place_Name As String
            Public Place_Type As String
            Public REC_EDIT_ON As DateTime

        End Class
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServicePlaces_GetList</remarks>
        Public Function GetList() As List(Of Return_ServicePlace)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServicePlaces_GetList, ClientScreen.Profile_ServicePlaces)
            Dim _SP As List(Of Return_ServicePlace) = New List(Of Return_ServicePlace)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_ServicePlace
                    newdata.Service_Place_Name = row.Field(Of String)("Service Place Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Start_Date = row.Field(Of DateTime)("Start Date")
                    newdata.Weekdays = row.Field(Of String)("Weekdays")
                    newdata.Timing = row.Field(Of String)("Timing")
                    newdata.Status = row.Field(Of String)("Status")
                    newdata.Place_Owner = row.Field(Of String)("Place Owner")
                    newdata.Place_Address = row.Field(Of String)("Place Address")
                    newdata.CITYID = row.Field(Of String)("CITYID")
                    newdata.R_City = row.Field(Of String)("R_City")
                    newdata.Place_Contact_No = row.Field(Of String)("Place Contact No")
                    newdata.Responsible_Person = row.Field(Of String)("Responsible Person")
                    newdata.Contact_No = row.Field(Of String)("Contact No")
                    newdata.Other_Details = row.Field(Of String)("Other Details")
                    newdata.YearID = row.Field(Of Integer)("YearID")
                    newdata.ORG_REC = row.Field(Of String)("ORG_REC")
                    newdata.LOC_ID = row.Field(Of String)("LOC_ID")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Add_Date = row.Field(Of DateTime)("Add Date")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit Date")
                    newdata.Action_Date = row.Field(Of DateTime)("Action Date")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.iREQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    newdata.iCOMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    newdata.iRESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    newdata.iREJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
                    newdata.iOTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    newdata.iALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")
                    newdata.iIcon = ""

                    If (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) = 0 AndAlso (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) Then
                        newdata.iIcon += "RedShield|"
                    ElseIf ((((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) >= (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0))) AndAlso ((If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) AndAlso ((If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0)) = 0)) Then
                        newdata.iIcon += "GreenShield|"
                    ElseIf (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) > 0 AndAlso (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) < (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)))) Then
                        newdata.iIcon += "YellowShield|"
                    ElseIf ((((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) >= (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0))) AndAlso ((If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) AndAlso ((If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0)) > 0)) Then
                        newdata.iIcon += "BlueShield|"
                    End If

                    If ((If(row.Field(Of Int32?)("REJECTED_COUNT"), 0)) > 0) Then
                        newdata.iIcon += "RedFlag|"
                    End If

                    If (((If(row.Field(Of Int32?)("ALL_ATTACH_CNT"), 0)) > 0) AndAlso (If(row.Field(Of Int32?)("OTHER_ATTACH_CNT"), 0)) = 0) Then
                        newdata.iIcon += "RequiredAttachment|"
                    ElseIf (((If(row.Field(Of Int32?)("ALL_ATTACH_CNT"), 0)) > 0) AndAlso (If(row.Field(Of Int32?)("OTHER_ATTACH_CNT"), 0)) <> 0) Then
                        newdata.iIcon += "AdditionalAttachment|"
                    End If

                    _SP.Add(newdata)
                Next
            End If
            Return _SP
        End Function

        Public Function GetAllServicePlaceList(ByVal screen As ClientScreen) As List(Of Return_GetAllServicePlaceList)
            Dim ret_table As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServicePlaces_GetAllServicePlaceList, screen, cBase._open_PAD_No_Main)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim servicelist = New List(Of Return_GetAllServicePlaceList)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_GetAllServicePlaceList()
                    newdata.SP_ID = row.Field(Of String)("SP_ID")
                    newdata.Institute = row.Field(Of String)("Institute")
                    newdata.Centre_No = row.Field(Of String)("Centre No.")
                    newdata.UID = row.Field(Of String)("UID")
                    newdata.No = row.Field(Of String)("No.")
                    newdata.Service_Place_Name = row.Field(Of String)("Service Place Name")
                    newdata.Place_Type = row.Field(Of String)("Place Type")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    servicelist.Add(newdata)
                Next
                Return servicelist
            End If
        End Function

        'Shifted
        Public Function GetCityList() As DataTable
            Return GetCities(ClientScreen.Profile_ServicePlaces)
        End Function

        'Shifted
        Public Function GetAddresses(Optional Party_Rec_ID As String = Nothing) As DataTable
            Dim _address As Addresses = New Addresses(cBase)
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common
            Param.Party_Rec_ID = Party_Rec_ID
            Return _address.GetList(ClientScreen.Profile_ServicePlaces, Param)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_ServicePlaces, RealTimeService.Tables.SERVICE_PLACE_INFO, "SP_CEN_ID")
        End Function

        ''' <summary>
        ''' Get Count By Place Name, Shifted
        ''' </summary>
        ''' <param name="Name"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServicePlaces_GetCountByPlaceName</remarks>
        Public Function GetCountByPlaceName(ByVal Name As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.ServicePlaces_GetCountByPlaceName, ClientScreen.Profile_ServicePlaces, Name)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            ' Return GetRecordByID(Rec_ID, ClientScreen.Profile_ServicePlaces, RealTimeService.Tables.SERVICE_PLACE_INFO, Common.ClientDBFolderCode.SYS)
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServicePlaces_GetRecord, ClientScreen.Profile_ServicePlaces, Rec_ID)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.SERVICE_PLACE_INFO, ClientScreen.Profile_ServicePlaces)
        End Function

        Public Function IsServicePlaceCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_ServicePlaces, Tables.SERVICE_PLACE_INFO)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServicePlaces_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_ServicePlaces) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.ServicePlaces_Insert, ClientScreen.Profile_ServicePlaces, InParam)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServicePlaces_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_ServicePlaces) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.ServicePlaces_Update, ClientScreen.Profile_ServicePlaces, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            Return DeleteRecord(Rec_Id, Tables.SERVICE_PLACE_INFO, ClientScreen.Profile_ServicePlaces)
        End Function

        Public Function InsertServicePlaces_Txn(inParam As Param_Txn_InsertServicePlaces) As Boolean
            Return ExecuteGroup(RealServiceFunctions.ServicePlaces_InsertServicePlaces_Txn, ClientScreen.Profile_ServicePlaces, inParam)
        End Function

        Public Function UpdateServicePlaces_Txn(upParam As Param_Txn_UpdateServicePlaces) As Boolean
            Return ExecuteGroup(RealServiceFunctions.ServicePlaces_UpdateServicePlaces_Txn, ClientScreen.Profile_ServicePlaces, upParam)
        End Function

        Public Function DeleteServicePlaces_Txn(delParam As Param_Txn_DeleteServicePlaces) As Boolean
            Return ExecuteGroup(RealServiceFunctions.ServicePlaces_DeleteServicePlaces_Txn, ClientScreen.Profile_ServicePlaces, delParam)
        End Function
    End Class
#End Region
End Class
