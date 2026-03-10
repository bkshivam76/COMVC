'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Facility"
    <Serializable>
    Public Class GodlyServiceMaterial
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetList() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@USERID", "@YEARID", "@CENID"}
            Dim values() As Object = {cBase._open_User_ID, cBase._open_Year_ID, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4}
            Return _RealService.ListFromSP(Tables.GODLY_SERVICE_MATERIAL_INFO, "[sp_Get_GodlyServiceMaterialInfo]", Tables.GODLY_SERVICE_MATERIAL_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_GodlyServiceMaterial))
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Facility_GodlyServiceMaterial, RealTimeService.Tables.GODLY_SERVICE_MATERIAL_INFO, Common.ClientDBFolderCode.SYS)
        End Function

        ''' <summary>
        ''' Get Wings Record,
        ''' </summary>
        ''' <param name="SM_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceMaterial_GetWingsRecord</remarks>
        Public Function GetWingsRecord(ByVal SM_Rec_ID As String) As DataTable
            Dim query As String = "Select SM_WING_ID  from SERVICE_MATERIAL_WING_INFO where  REC_STATUS IN (0,1,2) AND SM_REC_ID= '" & SM_Rec_ID & "'"
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServiceMaterial_GetWingsRecord, ClientScreen.Facility_GodlyServiceMaterial, SM_Rec_ID)
        End Function
        Public Function GetProjects_Occasions(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String, Optional Remark2Filter As String = "") As DataTable
            Return GetMiscDetails("'GODLY SERVICE PROJECTS','GODLY SERVICE PROGRAM OCCASION'", ClientScreen.Facility_GodlyServiceMaterial, MiscNameColumnHead, RecIDColumnHead)
            'Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Facility_ServiceReport, MiscNameColumnHead, RecIDColumnHead, Remark2Filter)
        End Function
        Public Function GetMaterialCategory() As DataTable
            Dim query As String = "Select Distinct GSM_MATERIAL_TYPE as 'MaterialType' FROM Godly_Service_material_info WHERE len(coalesce(GSM_MATERIAL_TYPE,''))>0"
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Return _RealService.List(Tables.GODLY_SERVICE_MATERIAL_INFO, query, Tables.GODLY_SERVICE_MATERIAL_INFO.ToString, GetBaseParams(ClientScreen.Facility_GodlyServiceMaterial))
        End Function
        Public Function GetMaterialSubCategory() As DataTable
            Dim query As String = "Select Distinct GSM_MATERIAL_SUB_CATEGORY as 'MaterialSubCategory' FROM Godly_Service_material_info WHERE len(coalesce(GSM_MATERIAL_SUB_CATEGORY,''))>0"
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Return _RealService.List(Tables.GODLY_SERVICE_MATERIAL_INFO, query, Tables.GODLY_SERVICE_MATERIAL_INFO.ToString, GetBaseParams(ClientScreen.Facility_GodlyServiceMaterial))
        End Function
        'Shifted
        'Public Function GetProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String, Optional Remark2Filter As String = "") As DataTable
        '    Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Facility_ServiceReport, MiscNameColumnHead, RecIDColumnHead, Remark2Filter)
        'End Function
        'Public Function GetMasterProjects(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String, Optional Remark2Filter As String = "") As DataTable
        '    Return GetMisc("MASTER GODLY SERVICE PROJECTS", ClientScreen.Facility_ServiceReport, MiscNameColumnHead, RecIDColumnHead, Remark2Filter)
        'End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_GetList</remarks>
        'Public Function GetList() As DataTable
        '    Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServiceReport_GetList, ClientScreen.Facility_ServiceReport, cBase._Date_Format_Current)
        'End Function

        'Shifted
        'Public Function GetWings() As DataTable
        '    Return GetWingsList(ClientScreen.Facility_ServiceReport)
        'End Function

        'Public Function GetRecord(ByVal Rec_ID As String) As DataTable
        '    Return GetRecordByID(Rec_ID, ClientScreen.Facility_ServiceReport, RealTimeService.Tables.SERVICE_REPORT_INFO, Common.ClientDBFolderCode.SYS)
        'End Function

        ''' <summary>
        ''' Get Wings Record, Shifted
        ''' </summary>
        ''' <param name="SM_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_GetWingsRecord</remarks>
        'Public Function GetWingsRecord(ByVal SR_Rec_ID As String) As DataTable
        '    Dim query As String = "Select SR_WING_ID  from SERVICE_REPORT_WING_INFO where  REC_STATUS IN (0,1,2) AND SR_REC_ID= '" & SR_Rec_ID & "'"
        '    Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ServiceReport_GetWingsRecord, ClientScreen.Facility_ServiceReport, SR_Rec_ID)
        'End Function

        '' <summary>
        '' Insert, Shifted
        '' </summary>
        ''' <param name="InParam"></param>
        '' <returns></returns>
        '' <remarks>RealServiceFunctions.ServiceMaterial_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_ServiceMaterial) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.ServiceMaterial_Insert, ClientScreen.Facility_GodlyServiceMaterial, InParam)
        End Function

        ''' <summary>
        ''' Insert Wings, Shifted
        ''' </summary>
        '''' <param name="InWings"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_InsertWings</remarks>
        'Public Function InsertWings(ByVal InWings As Parameter_InsertWings_ServiceReport) As Boolean
        '    Return InsertRecord(RealTimeService.RealServiceFunctions.ServiceReport_InsertWings, ClientScreen.Facility_ServiceReport, InWings)
        'End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        '''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_Update</remarks>
        'Public Function Update(ByVal UpParam As Parameter_Update_ServiceReport) As Boolean
        '    Return UpdateRecord(RealTimeService.RealServiceFunctions.ServiceReport_Update, ClientScreen.Facility_ServiceReport, UpParam)
        'End Function

        'Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
        '    Return DeleteRecord(Rec_Id, Tables.SERVICE_REPORT_INFO, ClientScreen.Facility_ServiceReport)
        'End Function

        'Public Function DeleteWing(ByVal SR_Rec_Id As String) As Boolean
        '    Return DeleteByCondition("SR_REC_ID    ='" & SR_Rec_Id & "'", Tables.SERVICE_REPORT_WING_INFO, ClientScreen.Facility_ServiceReport)
        'End Function

        Public Function InsertServiceMaterial_Txn(InParam As Param_Txn_Insert_ServiceMaterial) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.ServiceMaterial_InsertServiceMaterial_Txn, ClientScreen.Facility_GodlyServiceMaterial, InParam)
        End Function

        Public Function UpdateServiceMaterial_Txn(UpParam As Param_Txn_Update_ServiceMaterial) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.ServiceMaterial_UpdateServiceMaterial_Txn, ClientScreen.Facility_GodlyServiceMaterial, UpParam)
        End Function

        Public Function DeleteServiceMaterial_Txn(DelParam As Param_Txn_Delete_ServiceMaterial) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.ServiceMaterial_DeleteServiceMaterial_Txn, ClientScreen.Facility_GodlyServiceMaterial, DelParam)
        End Function

    End Class
#End Region
End Class
