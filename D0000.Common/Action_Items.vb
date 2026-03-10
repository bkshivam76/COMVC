'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Help"
    <Serializable>
    Public Class Action_Items
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        ''' <summary>
        ''' Gets HO Events , Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetEvents() As DataTable
            Return GetHOEvents(ClientScreen.Help_Action_Items, False)
        End Function

        Public Function GetServerDateTime() As Object
            Return GetCurrentDateTime(ClientScreen.Help_Action_Items)
        End Function

        ''' <summary>
        ''' Get Over dues Count, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>ActionItems_GetOverDueCount</remarks>
        Public Function GetOverDueCount(ByVal screen As Common_Lib.RealTimeService.ClientScreen) As Object
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM action_item_info WHERE  REC_STATUS IN (0,1,2) AND UPPER(AI_STATUS) <> 'CLOSED' AND  AI_DUE_ON < '" & Convert.ToDateTime(GetServerDateTime()).ToString(cBase._Server_Date_Format_Short) & "' AND AI_CEN_ID = '" & cBase._open_Cen_ID & "'"
            Return GetSingleValue_Data(RealServiceFunctions.ActionItems_GetOverDueCount, screen)
        End Function

        Public Function GetPendingCentreRemarkCount(ByVal screen As Common_Lib.RealTimeService.ClientScreen) As Object
            Return GetSingleValue_Data(RealServiceFunctions.ActionItems_GetPendingCentreRemarkCount, screen)
        End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <param name="RefRecId"></param>
        ''' <param name="RefTable"></param>
        ''' <returns></returns>
        ''' <remarks>ActionItems_GetList</remarks>
        Public Function GetList(Optional ByVal RefRecId As String = "", Optional ByVal RefTable As String = "") As DataTable
            Dim param As Param_Action_Items_GetList = New Param_Action_Items_GetList()
            param.RefRecId = RefRecId
            param.RefTable = RefTable
            Return GetDataListOfRecords(Common_Lib.RealTimeService.RealServiceFunctions.ActionItems_GetList, ClientScreen.Help_Action_Items, param)
        End Function

        Public Function GetOpenActions(ByVal RefRecId As String, ByVal Table As String) As Object
            Dim Param As Param_GetOpenActions_Common = New Param_GetOpenActions_Common
            Param.RefID = RefRecId
            Param.Tablename = Table
            Return GetSingleValue_Data(Common_Lib.RealTimeService.RealServiceFunctions.ActionItems_GetOpenActions_Common, ClientScreen.Help_Action_Items, Param)
        End Function

        Public Function GetRemarksStatus(TableName As Common_Lib.RealTimeService.Tables, RecID As String) As Object
            Dim Param As Param_GetRemarksStatus = New Param_GetRemarksStatus
            Param.TableName = TableName.ToString.ToUpper
            Param.RecID = RecID
            Return GetSingleValue_Data(Common_Lib.RealTimeService.RealServiceFunctions.ActionItems_GetRemarksStatus, ClientScreen.Help_Action_Items, Param)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="RecIdColumnHead"></param>
        ''' <param name="NameColumnHead"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetActionTitles(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
            Return GetMisc("ACTION TITLES", ClientScreen.Help_Action_Items, NameColumnHead, RecIdColumnHead)
        End Function

        ''' <summary>
        ''' Insert, shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>ActionItems_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Action_Items) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.ActionItems_Insert, ClientScreen.Help_Action_Items, InParam)
        End Function

        ''' <summary>
        ''' Update , shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>ActionItems_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Action_Items) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.ActionItems_Update, ClientScreen.Help_Action_Items, UpParam)
        End Function

        Public Function UpdateCentreRemarks(ByVal UpParam As Parameter_Update_Action_Items) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.ActionItems_UpdateCentreRemarks, ClientScreen.Help_Action_Items, UpParam)
        End Function

        ''' <summary>
        ''' Closes action Item, Shifted
        ''' </summary>
        ''' <param name="Cls"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overloads Function Close(ByVal Cls As Parameter_Close_Action_Items) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.ActionItems_Close, ClientScreen.Help_Action_Items, Cls)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Help_Action_Items, RealTimeService.Tables.ACTION_ITEM_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function InsertActionItems_Txn(InParam As Param_Txn_Insert_ActionItems) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.ActionItems_InsertActionItems_Txn, ClientScreen.Help_Action_Items, InParam)
        End Function

        Public Function UpdateActionItems_Txn(UpParam As Param_Txn_Update_ActionItems) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.ActionItems_UpdateActionItems_Txn, ClientScreen.Help_Action_Items, UpParam)
        End Function

        Public Function GetAuditExceptions(Optional ShowBlockRegistrationOnly As Boolean = False) As Object
            Return GetDatasetOfRecordsBySP(Common_Lib.RealTimeService.RealServiceFunctions.Audit_GetExceptions, ClientScreen.Home_StartUp, ShowBlockRegistrationOnly)
        End Function
    End Class
#End Region
End Class
