Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class OpeningBalances
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
 
        Public Function GetList() As DataTable
            Dim param As Parameter_OpeningBalances_GetList = New Parameter_OpeningBalances_GetList
            param.CurrInsttID = cBase._open_Ins_ID
            param.YearID = cBase._open_Year_ID
            param.PrevYearID = cBase._prev_Unaudited_YearID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.OpeningBalances_GetList, ClientScreen.Profile_OpeningBalances, param)
        End Function

        'Public Function GetList(ByVal screen As ClientScreen) As DataTable
        '    Dim param As Parameter_OpeningBalances_GetList = New Parameter_OpeningBalances_GetList
        '    param.CurrInsttID = cBase._open_Ins_ID
        '    param.YearID = cBase._open_Year_ID
        '    Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.OpeningBalances_GetList, screen, param)
        'End Function

        Public Function GetDuplicateCount(ByVal ItemID As String) As Object
            Dim param As Parameter_OpeningBalances_GetList = New Parameter_OpeningBalances_GetList
            param.CurrInsttID = cBase._open_Ins_ID
            param.YearID = cBase._open_Year_ID
            param.ItemID = ItemID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.OpeningBalances_GetDuplicateCount, ClientScreen.Profile_OpeningBalances, param)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_OpeningBalances, RealTimeService.Tables.OTHER_PROFILE_INFO, "OP_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_OpeningBalances, RealTimeService.Tables.OTHER_PROFILE_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetOpeningBalancesItems() As DataTable
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Profile = "OPENING"
            inParam.Type = "1"
            inParam.currInsttID = cBase._open_Ins_ID
            Return GetItemsByQuery_Common("", ClientScreen.Profile_OpeningBalances, inParam)
        End Function

        Public Function IsOpBalanceCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
            Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_OpeningBalances, Tables.OTHER_PROFILE_INFO)
        End Function

        Public Function Insert(ByVal InParam As Parameter_Insert_OpeningBalances) As Boolean
            InParam.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.OpeningBalances_Insert, ClientScreen.Profile_OpeningBalances, InParam)
        End Function

        Public Function Update(ByVal UpParam As Parameter_Update_OpeningBalances) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_OpeningBalances)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.OpeningBalances_Update, ClientScreen.Profile_OpeningBalances, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_OpeningBalances)

            Return DeleteRecord(Rec_Id, Tables.OTHER_PROFILE_INFO, ClientScreen.Profile_OpeningBalances)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.OTHER_PROFILE_INFO, ClientScreen.Profile_OpeningBalances)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.OTHER_PROFILE_INFO, ClientScreen.Profile_OpeningBalances)
            Return Locked
        End Function
    End Class
End Class
