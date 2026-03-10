Imports Common_Lib.RealTimeService

Partial Public Class DbOperations
    <Serializable>
    Public Class Complexes
        Inherits SharedVariables

#Region "Return Classes"
        <Serializable>
        Public Class Return_Complex_GetList
            Public Complex_Name As String
            Public Address As String
            Public City As String
            Public State As String
            Public District As String
            Public Pincode As String
            Public Country As String
            Public Remarks As String
            Public YEAR_ID As String
            Public ID As String
            Public Add_By As String
            Public Add_Date As DateTime?
            Public Edit_By As String
            Public Edit_date As DateTime?
            Public Action_Status As String
            Public Action_By As String
            Public Action_Date As DateTime?
            Public Building_Count As Integer?
            Public RemarkCount As Integer?
            Public RemarkStatus As String
            Public OpenActions As Integer?
            Public CrossedTimeLimit As Integer?
            Public Max_Edit_on As DateTime?
        End Class
        <Serializable>
        Public Class Return_Complex_GetInstList
            Public Complex_Name As String
            Public City As String
            Public ID As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetList(Cen_BK_PAD_NO As String, Optional Prev_Year_ID As Integer = Nothing) As List(Of Return_Complex_GetList)
            Dim Param As Param_Get_LB_Listing_Profile = New Param_Get_LB_Listing_Profile
            Param.Cen_BK_PAD_NO = Cen_BK_PAD_NO
            Param.Prev_Year_ID = Prev_Year_ID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Complexes_GetList, ClientScreen.Profile_Complexes, Param)
            Dim _Complex_data As List(Of Return_Complex_GetList) = New List(Of Return_Complex_GetList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_Complex_GetList
                    newdata.Complex_Name = row.Field(Of String)("Complex Name")
                    newdata.Address = row.Field(Of String)("Address")
                    newdata.City = row.Field(Of String)("City")
                    newdata.State = row.Field(Of String)("State")
                    newdata.District = row.Field(Of String)("District")
                    newdata.Pincode = row.Field(Of String)("Pincode")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.YEAR_ID = row.Field(Of Integer)("YEAR_ID")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Add_Date = row.Field(Of DateTime?)("Add Date")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Edit_date = row.Field(Of DateTime?)("Edit Date")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Date = row.Field(Of DateTime?)("Action Date")
                    newdata.Building_Count = row.Field(Of Integer?)("Building_Count")
                    newdata.RemarkCount = row.Field(Of Integer?)("RemarkCount")
                    newdata.RemarkStatus = row.Field(Of String)("RemarkStatus")
                    newdata.OpenActions = row.Field(Of Integer?)("OpenActions")
                    newdata.CrossedTimeLimit = row.Field(Of Integer?)("CrossedTimeLimit")
                    newdata.Max_Edit_on = row.Field(Of DateTime?)("Max_Edit_on")
                    _Complex_data.Add(newdata)
                Next
            End If
            Return _Complex_data
        End Function
        Public Function GetInstList() As List(Of Return_Complex_GetInstList)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Complexes_GetInstList, ClientScreen.Profile_Complexes)
            Dim _Complex_data As List(Of Return_Complex_GetInstList) = New List(Of Return_Complex_GetInstList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_Complex_GetInstList
                    newdata.Complex_Name = row.Field(Of String)("Complex")
                    newdata.City = row.Field(Of String)("City")
                    newdata.ID = row.Field(Of String)("Complex_ID")
                    _Complex_data.Add(newdata)
                Next
            End If
            Return _Complex_data
        End Function
        Public Function GetBuildingList(Cen_BK_PAD_NO As String, ComplexID As String, Optional Prev_Year_ID As Integer = Nothing) As DataTable
            Dim Param As Param_GetBuildingList_Complexes = New Param_GetBuildingList_Complexes
            Param.Cen_BK_PAD_NO = Cen_BK_PAD_NO
            Param.ComplexID = ComplexID
            Param.Prev_Year_ID = Prev_Year_ID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Complexes_GetBuildingList, ClientScreen.Profile_Complexes, Param)
        End Function

        Public Function GetAllComplexBuildings(Cen_BK_PAD_NO As String, Optional Prev_Year_ID As Integer = Nothing) As DataTable
            Dim Param As Param_Get_LB_Listing_Profile = New Param_Get_LB_Listing_Profile
            Param.Cen_BK_PAD_NO = Cen_BK_PAD_NO
            Param.Prev_Year_ID = Prev_Year_ID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Complexes_GetAllComplexBuildings, ClientScreen.Profile_Complexes, Param)
        End Function

        Public Function Get_LB_Listing_Profile(Cen_BK_PAD_NO As String, Optional Prev_YearId As Integer = Nothing) As DataTable
            Dim Param As Param_Get_LB_Listing_Profile = New Param_Get_LB_Listing_Profile
            Param.Cen_BK_PAD_NO = Cen_BK_PAD_NO
            Param.Prev_Year_ID = Prev_YearId
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Complexes_Get_LB_Listing_Profile, ClientScreen.Profile_Complexes, Param)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Complexes, RealTimeService.Tables.COMPLEX_INFO, "CM_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Complexes, RealTimeService.Tables.COMPLEX_INFO, Common.ClientDBFolderCode.SYS)
            'Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Property_GetRecord, ClientScreen.Profile_LandAndBuilding, Rec_ID)
        End Function


        Public Function GetRecordCountByName(ByVal ComplexName As String, Optional CM_ID As String = Nothing) As Object
            Dim Param As Param_GetRecordCountByName_Complexes = New Param_GetRecordCountByName_Complexes
            Param.Name = ComplexName
            Param.ID = CM_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Complexes_GetRecordCountByName, ClientScreen.Profile_Complexes, Param)
        End Function

        Public Function GetMaxEditOn(ByVal ComplexId As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Complexes_GetMaxEditOn, ClientScreen.Profile_Complexes, ComplexId)
        End Function

        Public Function IsPropertyAlreadyMapped(PropertyId As String, Optional ComplexId As String = "", Optional IsManageBuildings As Boolean = False) As Boolean
            Dim Param As Param_IsPropertyAlreadyMapped = New Param_IsPropertyAlreadyMapped
            Param.ComplexID = ComplexId
            Param.PropertyID = PropertyId
            Param.IsManageBuildings = IsManageBuildings
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Complexes_IsPropertyAlreadyMapped, ClientScreen.Profile_Complexes, Param)
        End Function

        Public Function IsComplexCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_Complexes, Tables.COMPLEX_INFO)
        End Function

        Public Function Insert(ByVal InParam As Param_Insert_Complexes) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Complexes_Insert, ClientScreen.Profile_Complexes, InParam)
        End Function

        Public Function Insert_Building(InParam As Param_InsertBuilding_Complexes) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Complexes_Insert_Building, ClientScreen.Profile_Complexes, InParam)
        End Function

        Public Function Update(UpParam As Param_Update_Complexes) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Complexes_Update, ClientScreen.Profile_Complexes, UpParam)
        End Function

        Public Function Delete(ByVal Rec_Id As String) As Boolean
            Return DeleteRecord(Rec_Id, Tables.COMPLEX_INFO, ClientScreen.Profile_Complexes)
        End Function

        Public Function Delete_Building(ByVal Rec_Id As String) As Boolean
            Return DeleteByCondition("CB_COMPLEX_ID   ='" & Rec_Id & "'", Tables.COMPLEX_BUILDING_INFO, ClientScreen.Profile_Complexes)
        End Function

        Public Function Insert_Complexes_Txn(inParam As Param_Txn_Insert_Complexes) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Complexes_Insert_Complexes_Txn, ClientScreen.Profile_Complexes, inParam)
        End Function

        Public Function DeleteComplexBuilding_Txn(ByVal DelParam As Param_Txn_Delete_Complexes) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Complexes_DeleteComplexBuilding_Txn, ClientScreen.Profile_Complexes, DelParam)
        End Function

        Public Function Update_ManageBuildings_Txn(inParam As Param_Update_ManageBuildings_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Complexes_Update_ManageBuildings_Txn, ClientScreen.Profile_Complexes, inParam)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Dim Result As Boolean = True
            If Not MyBase.MarkAsComplete(Rec_Id, Tables.COMPLEX_INFO, ClientScreen.Profile_Complexes) Then Result = False
            If Not MyBase.MarkAsComplete("CB_COMPLEX_ID", Rec_Id, Tables.COMPLEX_BUILDING_INFO, ClientScreen.Profile_Complexes) Then Result = False
            Return Result
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.COMPLEX_INFO, ClientScreen.Profile_Complexes, cBase._data_ConStr_Sys)
            If Locked Then
                Locked = MyBase.MarkAsLocked("CB_COMPLEX_ID", Rec_Id, Tables.COMPLEX_BUILDING_INFO, ClientScreen.Profile_Complexes)
            End If
            Return Locked
        End Function

    End Class
End Class

