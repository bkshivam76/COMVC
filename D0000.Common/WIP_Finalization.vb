
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class Return_WIP_Ledger
        Public WIP_LED_ID As String
        Public WIP_LEDGER As String
        Public Txn_Cr_ItemId As String
        Public Txn_Cr_ItemName As String
    End Class
    <Serializable>
    Public Class Return_ReferenceType
        Public Property SNo As Long
        Public Property LED_ID As String
        Public Property WIP_Ledger As String
        Public Property Reference As String
        Public Property Opening As Decimal
        Public Property Addition As Decimal
        Public Property Deduction As Decimal
        Public Property Closing As Decimal
        Public Property Next_Year_Closing_Value As Decimal
        Public Property Date_of_Creation As Date?
        Public Property YearID As Integer
        Public Property TR_ID As String
        Public Property Entry_Type As String
        Public Property RemarkCount As Integer
        Public Property RemarkStatus As String
        Public Property OpenActions As Integer
        Public Property CrossedTimeLimit As Integer
        Public Property Add_By As String
        Public Property Add_Date As Date
        Public Property Edit_By As String
        Public Property Edit_Date As Date
        Public Property Action_Status As String
        Public Property Action_By As String
        Public Property Action_Date As Date
        Public Property ID As String
        Public Property REQ_ATTACH_COUNT As Int32?
        Public Property COMPLETE_ATTACH_COUNT As Int32?
        Public Property RESPONDED_COUNT As Int32?
        Public Property REJECTED_COUNT As Int32?
        Public Property OTHER_ATTACH_CNT As Int32?
        Public Property ALL_ATTACH_CNT As Int32?
        Public Property VOUCHING_ACCEPTED_COUNT As Int32?
        Public Property VOUCHING_PENDING_COUNT As Int32?
        Public Property VOUCHING_ACCEPTED_WITH_REMARKS_COUNT As Int32?
        Public Property VOUCHING_REJECTED_COUNT As Int32?
        Public Property VOUCHING_TOTAL_COUNT As Int32?
        Public Property AUDIT_PENDING_COUNT As Int32?
        Public Property AUDIT_ACCEPTED_COUNT As Int32?
        Public Property AUDIT_ACCEPTED_WITH_REMARKS_COUNT As Int32?
        Public Property AUDIT_REJECTED_COUNT As Int32?
        Public Property AUDIT_TOTAL_COUNT As Int32?
        'Added for Audit Icon Filter
        Public Property iIcon As String
        Public Property Special_Ref As String
        Public Property Remarks As Boolean?
    End Class
    <Serializable>
    Public Class WIP_Profile
            Inherits SharedVariables

            Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub
        Public Function GetProfileListing_WIP(Param As Param_GetProfileListing_WIP) As List(Of Return_ReferenceType)
            Dim ret_table As DataTable = GetListOfRecordsBySP(RealServiceFunctions.WIP_Profile_GetProfileListing, ClientScreen.Profile_WIP, Param)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim profilelist = New List(Of Return_ReferenceType)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_ReferenceType
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Date = row.Field(Of Date)("Action Date")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.Addition = row.Field(Of Decimal)("Addition")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Add_Date = row.Field(Of Date)("Add Date")
                    newdata.Closing = row.Field(Of Decimal)("Closing")
                    newdata.CrossedTimeLimit = row.Field(Of Integer)("CrossedTimeLimit")
                    newdata.Date_of_Creation = row.Field(Of Date?)("Date of Creation")
                    newdata.Deduction = row.Field(Of Decimal)("Deduction")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Edit_Date = row.Field(Of Date)("Edit Date")
                    newdata.Entry_Type = row.Field(Of String)("Entry Type")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.LED_ID = row.Field(Of String)("LED_ID")
                    newdata.Next_Year_Closing_Value = row.Field(Of Decimal)("Next Year Closing Value")
                    newdata.OpenActions = row.Field(Of Integer)("OpenActions")
                    newdata.Opening = row.Field(Of Decimal)("Opening")
                    newdata.Reference = row.Field(Of String)("Reference")
                    newdata.RemarkCount = row.Field(Of Integer)("RemarkCount")
                    newdata.RemarkStatus = row.Field(Of String)("RemarkStatus")
                    newdata.SNo = row.Field(Of Long)("SNo.")
                    newdata.TR_ID = row.Field(Of String)("TR_ID")
                    newdata.WIP_Ledger = row.Field(Of String)("WIP Ledger")
                    newdata.YearID = row.Field(Of Integer)("YearID")
                    newdata.REQ_ATTACH_COUNT = row.Field(Of Integer?)("REQ_ATTACH_COUNT")
                    newdata.COMPLETE_ATTACH_COUNT = row.Field(Of Integer?)("COMPLETE_ATTACH_COUNT")
                    newdata.RESPONDED_COUNT = row.Field(Of Integer?)("RESPONDED_COUNT")
                    newdata.REJECTED_COUNT = row.Field(Of Integer?)("REJECTED_COUNT")
                    newdata.OTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    newdata.ALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")
                    newdata.VOUCHING_PENDING_COUNT = row.Field(Of Int32?)("VOUCHING_PENDING_COUNT")
                    newdata.VOUCHING_ACCEPTED_COUNT = row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT")
                    newdata.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT")
                    newdata.VOUCHING_REJECTED_COUNT = row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT")
                    newdata.VOUCHING_TOTAL_COUNT = row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT")
                    newdata.AUDIT_PENDING_COUNT = row.Field(Of Int32?)("AUDIT_PENDING_COUNT")
                    newdata.AUDIT_ACCEPTED_COUNT = row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT")
                    newdata.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT")
                    newdata.AUDIT_REJECTED_COUNT = row.Field(Of Int32?)("AUDIT_REJECTED_COUNT")
                    newdata.AUDIT_TOTAL_COUNT = row.Field(Of Int32?)("AUDIT_TOTAL_COUNT")
                    newdata.Special_Ref = row.Field(Of String)("Special_Ref")

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

                    If (If(row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"), 0) = 0 And row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT") > 0) Then newdata.iIcon += "VouchingAccepted|"
                    If (If(row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT"), 0) > 0) Then newdata.iIcon += "VouchingReject|"
                    If (If(row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"), 0) > 0) Then newdata.iIcon += "VouchingAcceptWithRemarks|"
                    If (If(row.Field(Of Int32?)("VOUCHING_PENDING_COUNT"), 0) > 0 And (If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) > 0 Or If(row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT"), 0) > 0)) Then newdata.iIcon += "VouchingPartial|"
                    If (If(row.Field(Of Int32?)("AUDIT_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"), 0) = 0 And row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT") > 0) Then newdata.iIcon += "AuditAccepted|"
                    If (If(row.Field(Of Int32?)("AUDIT_REJECTED_COUNT"), 0) > 0) Then newdata.iIcon += "AuditReject|"
                    If (If(row.Field(Of Int32?)("AUDIT_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"), 0) > 0) Then newdata.iIcon += "AuditAcceptWithRemarks|"
                    If (If(row.Field(Of Int32?)("AUDIT_PENDING_COUNT"), 0) > 0 And (If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) > 0 Or If(row.Field(Of Int32?)("AUDIT_REJECTED_COUNT"), 0) > 0)) Then newdata.iIcon += "AuditPartial|"
                    If ((row.Field(Of Int32?)("IS_AUTOVOUCHING")) > 0) Then newdata.iIcon += "AutoVouching|"
                    If ((row.Field(Of Int32?)("IS_CORRECTED_ENTRY")) > 0) Then newdata.iIcon += "CorrectedEntry|"
                    If newdata.RemarkCount > 0 Then newdata.Remarks = True Else newdata.Remarks = False
                    profilelist.Add(newdata)
                Next
                Return profilelist
            End If
        End Function
        Public Function GetStatus(ByVal Rec_Id As String) As Object
                Return GetRecordStatus(Rec_Id, ClientScreen.Profile_WIP, RealTimeService.Tables.WIP_INFO, "WIP_CEN_ID")
            End Function
            Public Function Get_WIP_List(ByVal screen As ClientScreen, Optional ByVal parameter As Object = Nothing) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.WIP_Profile_GetList_Common, screen, parameter)
            End Function
            Public Function IsTBImportedCentre() As Boolean
                Return GetSingleValue_Data(RealServiceFunctions.Data_IsTBImportedCentre, ClientScreen.Profile_WIP)
            End Function
            Public Function IsWIPCarriedForward(ByVal Rec_ID As String, ByVal recYearID As Integer) As Boolean?
                Return IsRecordCarriedForward(Rec_ID, recYearID, ClientScreen.Profile_WIP, Tables.WIP_INFO)
            End Function
            Public Function GetTxn_Report(ByVal inParam As Param_GetTxnReport) As DataTable
                Return GetListOfRecordsBySP(RealServiceFunctions.WIP_Profile_GetTxn_Report, ClientScreen.Profile_WIP, inParam)
            End Function

            Public Function GetRecord(ByVal Rec_ID As String) As DataTable
                Return GetRecordByID(Rec_ID, ClientScreen.Profile_WIP, RealTimeService.Tables.WIP_INFO, Common.ClientDBFolderCode.DATA)
            End Function

        Public Function Insert(ByVal InParam As Param_Insert_WIP_Profile) As Boolean
            Return InsertRecord(RealServiceFunctions.WIP_Profile_Insert, ClientScreen.Profile_WIP, InParam)
        End Function
        Public Function Insert(ByVal InParam As Param_InsertTRIDAndTRSrNo_WIP_Profile) As Boolean
                Return InsertRecord(RealServiceFunctions.WIP_Profile_InsertTRIDAndTRSrNo, ClientScreen.Profile_WIP, InParam)
            End Function
        Public Function Update(ByVal UpParam As Parameter_Update_WIP_Profile) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Profile_WIP)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.WIP_Profile_Update, ClientScreen.Profile_WIP, UpParam)

        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_WIP)

            Return DeleteRecord(Rec_Id, Tables.WIP_INFO, ClientScreen.Profile_WIP)
        End Function
        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_WIP)

            Return DeleteByCondition("WIP_TR_ID    ='" & Rec_Id & "'", Tables.WIP_INFO, Screen)
        End Function
        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.WIP_INFO, ClientScreen.Profile_WIP)
            Return Locked
        End Function
        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.WIP_INFO, ClientScreen.Profile_WIP)
        End Function

    End Class
    <Serializable>
    Public Class WIP_Creation_Vouchers
            Inherits SharedVariables
            Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub
            Public Function GetCountOfReferencesByLedID(ByVal Led_ID As String) As Object
                Return GetSingleValue_Data(RealServiceFunctions.WIP_Creation_Vouchers_GetCountOfReferencesByLedID, ClientScreen.Accounts_Voucher_Payment, Led_ID)
            End Function

            Public Function GetDuplicateReferenceCount(Param As Param_GetDuplicateReferenceCount) As Object
                Return GetSingleValue_Data(RealServiceFunctions.WIP_Creation_Vouchers_GetDuplicateReferenceCount, ClientScreen.Accounts_Voucher_Payment, Param)
            End Function

            Public Function GetRefCreationDateByWIPID(WIP_ID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.WIP_Creation_Vouchers_GetWIP_GetRefCreationDateByWIPID, ClientScreen.Accounts_Voucher_Payment, WIP_ID)
            End Function

            Public Function GetWIP_Dependencies(ByVal TR_M_ID As String, Optional WIP_ID As String = "") As DataTable
                Dim param As Param_GetWIP_Dependencies = New Param_GetWIP_Dependencies
                param.Txn_M_ID = TR_M_ID
                param.WIP_ID = WIP_ID
                param.Next_YearID = cBase._next_Unaudited_YearID
                Return GetListOfRecordsBySP(RealServiceFunctions.WIP_Creation_Vouchers_GetWIP_Dependencies, ClientScreen.Accounts_Voucher_WIP_Finalization, param)
            End Function
        End Class
    <Serializable>
    Public Class WIP_Finalization
            Inherits SharedVariables
            Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub

            'Public Function GetListOfReferences(WIP_Led_ID As String) As DataTable
            '    Return GetDataListOfRecords(RealServiceFunctions.Voucher_WIP_Finalization_GetListOfReferences, ClientScreen.Accounts_Voucher_WIP_Finalization, WIP_Led_ID)
            'End Function

            Public Function GetRecord(ByVal Rec_ID As String) As DataTable
                Return GetRecordByColumn("TR_M_ID", Rec_ID, ClientScreen.Accounts_Voucher_WIP_Finalization, RealTimeService.Tables.TRANSACTION_INFO)
            End Function

        Public Function GetPurposeID(ByVal Rec_ID As String) As String
            Dim _Table As DataTable = GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_WIP_Finalization, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
            If _Table.Rows.Count > 0 Then
                Return _Table.Rows(0)("TR_PURPOSE_MISC_ID")
            End If
            Return ""
        End Function

        Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
                Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_WIP_Finalization, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
            End Function

        Public Function GetListOfWIPLedgers(ByVal Is_HQ_Centre As Boolean) As List(Of Return_WIP_Ledger)
            'Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Voucher_WIP_Finalization_GetListOfWIPLedgers, ClientScreen.Accounts_Voucher_WIP_Finalization)
            Dim ITEM_APPLICABLE As String = "" : If Is_HQ_Centre Then ITEM_APPLICABLE = "'GENERAL','H.Q.'" Else ITEM_APPLICABLE = "'GENERAL','CENTRE'"
            Dim LocalQuery As String = "SELECT i.rec_id    AS Txn_Cr_ItemId, i.item_name AS 'Txn_Cr_ItemName', acc.led_id as WIP_LED_ID, acc.led_name as 'WIP LEDGER' " &
                " FROM   acc_ledger_info AS acc  LEFT OUTER JOIN item_info AS i ON i.item_led_id = acc.led_id WHERE acc.LED_SG_ID  = '00050' AND i.item_name LIKE '%(WIP) Finalization%'  AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") "
            Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
            OnlineParam.ItemApplicable = ITEM_APPLICABLE
            Dim ret_table As DataTable = GetItems_Ledger(ClientScreen.Accounts_Voucher_WIP_Finalization, LocalQuery, OnlineParam)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim ledgerlist = New List(Of Return_WIP_Ledger)
                For Each row As DataRow In ret_table.Rows
                    Dim newdata = New Return_WIP_Ledger
                    newdata.WIP_LEDGER = row.Field(Of String)("WIP LEDGER")
                    newdata.WIP_LED_ID = row.Field(Of String)("WIP_LED_ID")
                    newdata.Txn_Cr_ItemId = row.Field(Of String)("Txn_Cr_ItemId")
                    newdata.Txn_Cr_ItemName = row.Field(Of String)("Txn_Cr_ItemName")
                    ledgerlist.Add(newdata)
                Next
                Return ledgerlist
            End If
        End Function

        Public Function GetListOfFinalizedAssets(Curr_InsID As String, WIP_Led_ID As String) As DataTable
                Dim Param As Param_GetListOfFinalizedAssets = New Param_GetListOfFinalizedAssets
                Param.Curr_Ins_Id = Curr_InsID
                Param.WIP_Led_ID = WIP_Led_ID
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Voucher_WIP_Finalization_GetListOfFinalizedAssets, ClientScreen.Accounts_Voucher_WIP_Finalization, Param)
            End Function

            Public Function GetExistingAssetListing(Param As Param_GetExistingAssetListing) As DataTable
                Return GetListOfRecordsBySP(RealServiceFunctions.Voucher_WIP_Finalization_GetExistingAssetListing, ClientScreen.Accounts_Voucher_WIP_Finalization, Param)
            End Function

            Public Function Get_WIP_Outstanding_References(Param As Param_Get_WIP_Outstanding_References) As DataTable
                Return GetListOfRecordsBySP(RealServiceFunctions.Voucher_WIP_Finalization_Get_WIP_Outstanding_References, ClientScreen.Accounts_Voucher_WIP_Finalization, Param)
            End Function

            'Public Function InsertMasterInfo(ByVal InMInfo As Param_InsertMasterInfo_Voucher_WIPFinalization) As Boolean
            '    Return InsertRecord(RealServiceFunctions.Voucher_WIP_Finalization_InsertMasterInfo, ClientScreen.Accounts_Voucher_WIP_Finalization, InMInfo)
            'End Function
            Public Function GetFinalizedAmounts(TR_M_ID As String) As DataTable
                Return GetDataListOfRecords(RealServiceFunctions.Voucher_WIP_Finalization_GetFinalizedAmounts, ClientScreen.Accounts_Voucher_WIP_Finalization, TR_M_ID)
            End Function

            Public Function GetAssetList(ByVal RecID As String) As DataTable
                Dim _Assets As Assets = New Assets(cBase)
                Return _Assets.GetList(ClientScreen.Accounts_Voucher_WIP_Finalization, RecID)
            End Function

            'Public Function Insert(InParam As Param_Insert_Voucher_WIPFinalization) As Boolean
            '    Return InsertRecord(RealServiceFunctions.Voucher_WIP_Finalization_Insert, ClientScreen.Accounts_Voucher_WIP_Finalization, InParam)
            'End Function

            Public Function Insert_WIP_Finalization_Txn(InParam As Param_Txn_Insert_Voucher_WIPFinalization) As Boolean
                Return ExecuteGroup(RealTimeService.RealServiceFunctions.Voucher_WIP_Finalization_Insert_WIP_Finalization_Txn, ClientScreen.Accounts_Voucher_WIP_Finalization, InParam)
            End Function

        Public Function Update_WIP_Finalization_Txn(UpParam As Param_Txn_Update_Voucher_WIPFinalization) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.MID_Delete, ClientScreen.Accounts_Voucher_AssetTransfer)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Voucher_WIP_Finalization_Update_WIP_Finalization_Txn, ClientScreen.Accounts_Voucher_WIP_Finalization, UpParam)
        End Function

        Public Function Delete_WIP_Finalization_Txn(DelParam As Param_Txn_Delete_Voucher_WIPFinalization) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.MID_Delete, ClientScreen.Accounts_Voucher_AssetTransfer)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + DelParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Voucher_WIP_Finalization_Delete_WIP_Finalization_Txn, ClientScreen.Accounts_Voucher_WIP_Finalization, DelParam)
        End Function
    End Class

    End Class

