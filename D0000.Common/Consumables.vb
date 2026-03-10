'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class ConsumableStock
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetConsumables(ByVal MiscNameColumnHead As String, ByVal RecIDColumnHead As String) As DataTable
            Return GetMisc("STOCK OF CONSUMABLES", ClientScreen.Profile_StockOfConsumables, MiscNameColumnHead, RecIDColumnHead)
        End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Consumables_GetList</remarks>
        Public Function GetList() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Consumables_GetList, ClientScreen.Profile_StockOfConsumables)
        End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Consumables_GetList</remarks>
        Public Function GetList(ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Consumables_GetList, screen)
        End Function

        Public Function GetList_Summary(ByVal screen As ClientScreen) As DataTable
            Dim Param As Param_GetSummary = New Param_GetSummary
            Param._YearID = cBase._open_Year_ID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Consumables_GetList_Summary, screen, Param)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_StockOfConsumables, RealTimeService.Tables.CONSUMABLES_STOCK_INFO, "CS_CEN_ID")
        End Function

        Public Function GetYearEndCount() As Object
            Dim Param As Param_GetYearEndingCount = New Param_GetYearEndingCount
            Param.BK_Pad_No = cBase._open_PAD_No_Main
            Param.Year_EDT = cBase._open_Year_Edt.ToString(cBase._Server_Date_Format_Short)
            Return GetSingleValue_Data(RealServiceFunctions.Consumables_GetYearEnding, ClientScreen.Profile_StockOfConsumables, Param)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_StockOfConsumables, RealTimeService.Tables.CONSUMABLES_STOCK_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Updates AssetLocation Where not Present: Global_Set, Shifted
        ''' </summary>
        ''' <param name="defaultLocationID"></param>
        ''' <remarks>RealServiceFunctions.Consumables_UpdateAssetLocationIfNotPresent</remarks>
        Public Function UpdateAssetLocationIfNotPresent(ByVal defaultLocationID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Consumables_UpdateAssetLocationIfNotPresent, ClientScreen.Global_Set, defaultLocationID)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Consumables_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_ConsumableStock) As Boolean
            InParam.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Consumables_Insert, ClientScreen.Profile_StockOfConsumables, InParam)
        End Function

        ''' <summary>
        ''' Updates Consumables Info, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Consumables_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_ConsumableStock) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Consumables_Update, ClientScreen.Profile_StockOfConsumables, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            Return DeleteRecord(Rec_Id, Tables.CONSUMABLES_STOCK_INFO, ClientScreen.Profile_StockOfConsumables)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.CONSUMABLES_STOCK_INFO, ClientScreen.Profile_StockOfConsumables)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.CONSUMABLES_STOCK_INFO, ClientScreen.Profile_StockOfConsumables)
            Return Locked
        End Function
    End Class
End Class
