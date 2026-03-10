'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "Accounts"
    <Serializable>
    Public Class Return_InternalTransferRegister_Posted
        Public Property Status As String
        Public Property Centre_Name As String
        Public Property Center_UID As String
        Public Property No As String
        Public Property Zone As String
        Public Property Sub_Zone As String
        Public Property Description As String
        Public Property xDate As Date?
        Public Property Mode As String
        Public Property Amount As Decimal?
        Public Property Bank_Name As String
        Public Property Branch_Name As String
        Public Property Bank_AC_No As String
        Public Property Bank_2 As String
        Public Property RefNo As String
        Public Property RefDate As Date?
        Public Property Purpose As String
        Public Property ID As String
        Public Property MID As String
        Public Property AddBy As String
        Public Property AddDate As Date
        Public Property EditBy As String
        Public Property EditDate As Date
        Public Property ActionStatus As String
        Public Property ActionBy As String
        Public Property ActionDate As Date
        Public Property TR_TYPE As String
        Public Property TR_ITEM_ID As String
        Public Property TR_SR_NO As Int32
        Public Property iREQ_ATTACH_COUNT As Int32?
        Public Property iCOMPLETE_ATTACH_COUNT As Int32?
        Public Property iRESPONDED_COUNT As Int32?
        Public Property iREJECTED_COUNT As Int32?

        Public Property iOTHER_ATTACH_CNT As Int32?
        Public Property iALL_ATTACH_CNT As Int32?

        'Added for Audit Icon Filter
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
        Public Property iIcon As String

    End Class
    <Serializable>
    Public Class Voucher_Internal_Transfer
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_Internal_Transfer, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Master_Rec_ID As String, ByVal SrNo As Int16) As DataTable
            Return GetRecordByColumn("TR_M_ID", Master_Rec_ID, "TR_SR_NO", SrNo.ToString, ClientScreen.Accounts_Voucher_Internal_Transfer, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        Public Function GetPurposeRecord(ByVal TxnID As String) As DataTable
            Return GetRecordByColumn("TR_REC_ID", TxnID, ClientScreen.Accounts_Voucher_Internal_Transfer, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
        End Function

        'Shifted
        Public Function GetItemsByID(ByVal Item_Rec_Id As String) As DataTable
            Dim Query As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID  FROM ITEM_INFO WHERE  REC_STATUS IN (0,1,2) AND REC_ID='" & Item_Rec_Id & "' AND UCASE(ITEM_TRANS_TYPE)='DEBIT' "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.ItemIDs = Item_Rec_Id
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_Internal_Transfer, inParam)
        End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetList</remarks>
        Public Function GetList() As List(Of Return_InternalTransferRegister_Posted)
            Dim ret_table As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.InternalTransfer_GetList, ClientScreen.Accounts_Voucher_Internal_Transfer)
            If ret_table Is Nothing Then
                Return Nothing
            Else
                Dim data = New List(Of Return_InternalTransferRegister_Posted)
                For Each row As DataRow In ret_table.Rows
                    Dim newrow = New Return_InternalTransferRegister_Posted
                    newrow.Status = row.Field(Of String)("Status")
                    newrow.Centre_Name = row.Field(Of String)("Centre Name")
                    newrow.Center_UID = row.Field(Of String)("Centre UID")
                    newrow.No = row.Field(Of String)("No.")
                    newrow.Zone = row.Field(Of String)("Zone")
                    newrow.Sub_Zone = row.Field(Of String)("Sub Zone")
                    newrow.Description = row.Field(Of String)("Description")
                    newrow.xDate = row.Field(Of Date?)("Date")
                    newrow.Mode = row.Field(Of String)("Mode")
                    newrow.Amount = row.Field(Of Decimal?)("Amount")
                    newrow.Bank_Name = row.Field(Of String)("Bank Name")
                    newrow.Branch_Name = row.Field(Of String)("Branch Name")
                    newrow.Bank_AC_No = row.Field(Of String)("Bank A/C. No.")
                    newrow.Bank_2 = row.Field(Of String)("Bank 2")
                    newrow.RefNo = row.Field(Of String)("Ref.No.")
                    newrow.RefDate = row.Field(Of Date?)("Ref.Date")
                    newrow.Purpose = row.Field(Of String)("Purpose")
                    newrow.ID = row.Field(Of String)("ID")
                    newrow.MID = row.Field(Of String)("M.ID")
                    newrow.AddBy = row.Field(Of String)("Add By")
                    newrow.AddDate = row.Field(Of Date)("Add Date")
                    newrow.EditBy = row.Field(Of String)("Edit By")
                    newrow.EditDate = row.Field(Of Date)("Edit Date")
                    newrow.ActionStatus = row.Field(Of String)("Action Status")
                    newrow.ActionBy = row.Field(Of String)("Action By")
                    newrow.ActionDate = row.Field(Of Date)("Action Date")
                    newrow.TR_TYPE = row.Field(Of String)("TR_TYPE")
                    newrow.TR_ITEM_ID = row.Field(Of String)("TR_ITEM_ID")
                    newrow.TR_SR_NO = row.Field(Of Int32)("TR_SR_NO")
                    newrow.iREQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    newrow.iCOMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    newrow.iRESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    newrow.iREJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
                    newrow.iOTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    newrow.iALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")

                    newrow.VOUCHING_PENDING_COUNT = row.Field(Of Int32?)("VOUCHING_PENDING_COUNT")
                    newrow.VOUCHING_ACCEPTED_COUNT = row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT")
                    newrow.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT")
                    newrow.VOUCHING_REJECTED_COUNT = row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT")
                    newrow.VOUCHING_TOTAL_COUNT = row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT")
                    newrow.AUDIT_PENDING_COUNT = row.Field(Of Int32?)("AUDIT_PENDING_COUNT")
                    newrow.AUDIT_ACCEPTED_COUNT = row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT")
                    newrow.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT")
                    newrow.AUDIT_REJECTED_COUNT = row.Field(Of Int32?)("AUDIT_REJECTED_COUNT")
                    newrow.AUDIT_TOTAL_COUNT = row.Field(Of Int32?)("AUDIT_TOTAL_COUNT")

                    newrow.iIcon = ""

                    If (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) = 0 AndAlso (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) Then
                        newrow.iIcon += "RedShield|"
                    ElseIf ((((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) >= (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0))) AndAlso ((If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) AndAlso ((If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0)) = 0)) Then
                        newrow.iIcon += "GreenShield|"
                    ElseIf (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) > 0 AndAlso (((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) < (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)))) Then
                        newrow.iIcon += "YellowShield|"
                    ElseIf ((((If(row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT"), 0)) + (If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0))) >= (If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0))) AndAlso ((If(row.Field(Of Int32?)("REQ_ATTACH_COUNT"), 0)) > 0) AndAlso ((If(row.Field(Of Int32?)("RESPONDED_COUNT"), 0)) > 0)) Then
                        newrow.iIcon += "BlueShield|"
                    End If

                    If ((If(row.Field(Of Int32?)("REJECTED_COUNT"), 0)) > 0) Then
                        newrow.iIcon += "RedFlag|"
                    End If

                    If (((If(row.Field(Of Int32?)("ALL_ATTACH_CNT"), 0)) > 0) AndAlso (If(row.Field(Of Int32?)("OTHER_ATTACH_CNT"), 0)) = 0) Then
                        newrow.iIcon += "RequiredAttachment|"
                    ElseIf (((If(row.Field(Of Int32?)("ALL_ATTACH_CNT"), 0)) > 0) AndAlso (If(row.Field(Of Int32?)("OTHER_ATTACH_CNT"), 0)) <> 0) Then
                        newrow.iIcon += "AdditionalAttachment|"
                    End If

                    If (If(row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"), 0) = 0 And row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT") > 0) Then newrow.iIcon += "VouchingAccepted|"
                    If (If(row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT"), 0) > 0) Then newrow.iIcon += "VouchingReject|"
                    If (If(row.Field(Of Int32?)("VOUCHING_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT"), 0) > 0) Then newrow.iIcon += "VouchingAcceptWithRemarks|"
                    If (If(row.Field(Of Int32?)("VOUCHING_PENDING_COUNT"), 0) > 0 And (If(row.Field(Of Int32?)("VOUCHING_ACCEPTED_COUNT"), 0) > 0 Or If(row.Field(Of Int32?)("VOUCHING_REJECTED_COUNT"), 0) > 0)) Then newrow.iIcon += "VouchingPartial|"
                    If (If(row.Field(Of Int32?)("AUDIT_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"), 0) = 0 And row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT") > 0) Then newrow.iIcon += "AuditAccepted|"
                    If (If(row.Field(Of Int32?)("AUDIT_REJECTED_COUNT"), 0) > 0) Then newrow.iIcon += "AuditReject|"
                    If (If(row.Field(Of Int32?)("AUDIT_TOTAL_COUNT"), 0) = If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) And If(row.Field(Of Int32?)("AUDIT_ACCEPTED_WITH_REMARKS_COUNT"), 0) > 0) Then newrow.iIcon += "AuditAcceptWithRemarks|"
                    If (If(row.Field(Of Int32?)("AUDIT_PENDING_COUNT"), 0) > 0 And (If(row.Field(Of Int32?)("AUDIT_ACCEPTED_COUNT"), 0) > 0 Or If(row.Field(Of Int32?)("AUDIT_REJECTED_COUNT"), 0) > 0)) Then newrow.iIcon += "AuditPartial|"
                    If ((row.Field(Of Int32?)("IS_AUTOVOUCHING")) > 0) Then newrow.iIcon += "AutoVouching|"
                    If ((row.Field(Of Int32?)("IS_CORRECTED_ENTRY")) > 0) Then newrow.iIcon += "CorrectedEntry|"
                    data.Add(newrow)
                Next
                Return data
            End If
        End Function

        ''' <summary>
        ''' GetList With Multiple Params, Shifted
        ''' </summary>
        ''' <param name="To_Match_RecID"></param>
        ''' <param name="To_Cen_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetListWithTwoParams</remarks>
        Public Function GetList(ByVal To_Match_RecID As String, ByVal To_Cen_ID As Integer) As DataTable
            Dim Param As Param_Voucher_Internal_Transfer_GetList = New Param_Voucher_Internal_Transfer_GetList()
            Param.openCenRecID = cBase._open_Cen_Rec_ID
            Param.To_Cen_ID = To_Cen_ID
            Param.To_Match_RecID = To_Match_RecID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.InternalTransfer_GetListWithTwoParams, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
        End Function

        ''' <summary>
        ''' GetListWithMultipleParams, Shifted
        ''' </summary>
        ''' <param name="Mode"></param>
        ''' <param name="Amount"></param>
        ''' <param name="RefNo"></param>
        ''' <param name="TRDate"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetListWithMultipleParams</remarks>
        Public Function GetList(ByVal Mode As String, ByVal Amount As Double, ByVal RefNo As String, ByVal TRDate As Date) As DataTable
            Dim Param As Param_Voucher_Internal_Transfer_GetListWithMultipleParams = New Param_Voucher_Internal_Transfer_GetListWithMultipleParams()
            Param.Amount = Amount
            Param.Mode = Mode
            Param.openCenRecID = cBase._open_Cen_Rec_ID
            Param.RefNo = RefNo
            Param.TRDate = TRDate
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.InternalTransfer_GetListWithMultipleParams, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
        End Function

        ''' <summary>
        ''' Gets UnMatched List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetUnMatchedList</remarks>
        Public Function GetUnMatchedList(ByVal RowCount As Int32, ByVal CenID As Integer?) As DataSet
            Dim param As Param_Voucher_Internal_Transfer_GetUnMatchedList_LimitedCount = New Param_Voucher_Internal_Transfer_GetUnMatchedList_LimitedCount
            param.OpenCenRecID = cBase._open_Cen_Rec_ID
            param.TopCount = RowCount
            param.EnteringCenID = CenID
            Return (GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.InternalTransfer_GetUnMatchedList_LimitedCount, ClientScreen.Accounts_Voucher_Internal_Transfer, param))
        End Function

        ''' <summary>
        ''' Gets Items Required on Internal TF screen as per the open Center and Tasks assigned, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemList(ByVal ITEM_APPLICABLE As String) As DataTable
            Dim LocalQuery As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                    " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND UCASE(I.ITEM_APPLICABLE) IN(" & ITEM_APPLICABLE & ") "
            Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
            OnlineParam.ItemApplicable = ITEM_APPLICABLE
            Return GetItems_Ledger(ClientScreen.Accounts_Voucher_Internal_Transfer, LocalQuery, OnlineParam)
        End Function

        ''' <summary>
        ''' Get Fr Center List, Shifted
        ''' </summary>
        ''' <param name="IncludeCenIDs"></param>
        ''' <param name="ExcludeCenIDs"></param>
        ''' <param name="IncludeRecIDs"></param>
        ''' <param name="ExcludeRecIDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetFrCenterList</remarks>
        Public Function GetFrCenterList(ByVal IncludeCenIDs As String, ByVal ExcludeCenIDs As String, ByVal IncludeRecIDs As String, ByVal ExcludeRecIDs As String) As DataTable
            Dim LocalQuery As String = " SELECT IIF( LEN(IIF(ISNULL(C.CEN_NAME),'',C.CEN_NAME)) > 0, C.CEN_NAME, M.CEN_NAME) AS [FR_CEN_NAME],C.CEN_PAD_NO AS [FR_PAD_NO], C.CEN_UID AS [FR_UID],   M.CEN_INCHARGE AS [FR_INCHARGE], M.CEN_ZONE_ID AS [FR_ZONE], C.CEN_ID AS [FR_CEN_ID], C.REC_ID AS [FR_ID], " &
                                    " IIF( LEN(IIF(ISNULL(M.CEN_TEL_NO_1),'',M.CEN_TEL_NO_1)) > 0 ,M.CEN_TEL_NO_1,'') +  IIF( LEN(IIF(ISNULL(M.CEN_TEL_NO_1),'',M.CEN_TEL_NO_1)) > 0 AND LEN(IIF(ISNULL(M.CEN_MOB_NO_1),'',M.CEN_MOB_NO_1)) > 0 , ', ','') + IIF( LEN(IIF(ISNULL(M.CEN_MOB_NO_1),'',M.CEN_MOB_NO_1)) > 0 ,M.CEN_MOB_NO_1,'') AS [FR_TEL_NO]" &
                                    " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " &
                                    " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=TRUE AND C.[CEN_INS_ID]='" & cBase._open_Ins_ID & "' AND C.CEN_CANCELLATION_DATE IS NULL"

            If ExcludeRecIDs.Length > 0 Then LocalQuery += " AND C.REC_ID NOT IN (" & ExcludeRecIDs & ")"
            If IncludeRecIDs.Length > 0 Then LocalQuery += " AND C.REC_ID IN (" & IncludeRecIDs & ")"
            If ExcludeCenIDs.Length > 0 Then LocalQuery += " AND C.CEN_ID NOT IN (" & ExcludeCenIDs & ")"
            If IncludeCenIDs.Length > 0 Then LocalQuery += " AND C.CEN_ID IN (" & IncludeCenIDs & ")"

            Dim Param As Param_Voucher_Internal_Transfer_GetFrCenterList = New Param_Voucher_Internal_Transfer_GetFrCenterList()
            Param.ExcludeCenIDs = ExcludeCenIDs
            Param.ExcludeRecIDs = ExcludeRecIDs
            Param.IncludeCenIDs = IncludeCenIDs
            Param.IncludeRecIDs = IncludeRecIDs
            Param.openInsID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.InternalTransfer_GetFrCenterList, LocalQuery, ClientScreen.Accounts_Voucher_Internal_Transfer, Tables.CENTRE_INFO, Param)
        End Function

        ''' <summary>
        ''' Get To Center List, Shifted
        ''' </summary>
        ''' <param name="IncludeCenIDs"></param>
        ''' <param name="ExcludeCenIDs"></param>
        ''' <param name="IncludeRecIDs"></param>
        ''' <param name="ExcludeRecIDs"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetToCenterList</remarks>
        Public Function GetToCenterList(ByVal IncludeCenIDs As String, ByVal ExcludeCenIDs As String, ByVal IncludeRecIDs As String, ByVal ExcludeRecIDs As String) As DataTable
            Dim LocalQuery As String = " SELECT IIF( LEN(IIF(ISNULL(C.CEN_NAME),'',C.CEN_NAME)) > 0, C.CEN_NAME, M.CEN_NAME) AS [TO_CEN_NAME],C.CEN_PAD_NO AS [TO_PAD_NO], C.CEN_UID AS [TO_UID],   M.CEN_INCHARGE AS [TO_INCHARGE], M.CEN_ZONE_ID AS [TO_ZONE], C.CEN_ID AS [TO_CEN_ID], C.REC_ID AS [TO_ID], " &
                                    " IIF( LEN(IIF(ISNULL(M.CEN_TEL_NO_1),'',M.CEN_TEL_NO_1)) > 0 ,M.CEN_TEL_NO_1,'') +  IIF( LEN(IIF(ISNULL(M.CEN_TEL_NO_1),'',M.CEN_TEL_NO_1)) > 0 AND LEN(IIF(ISNULL(M.CEN_MOB_NO_1),'',M.CEN_MOB_NO_1)) > 0 , ', ','') + IIF( LEN(IIF(ISNULL(M.CEN_MOB_NO_1),'',M.CEN_MOB_NO_1)) > 0 ,M.CEN_MOB_NO_1,'') AS [TO_TEL_NO]" &
                                    " FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " &
                                    " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN=TRUE AND C.[CEN_INS_ID]='" & cBase._open_Ins_ID & "'  AND C.CEN_CANCELLATION_DATE IS NULL"

            If ExcludeRecIDs.Length > 0 Then LocalQuery += " AND C.REC_ID NOT IN (" & ExcludeRecIDs & ")"
            If IncludeRecIDs.Length > 0 Then LocalQuery += " AND C.REC_ID IN (" & IncludeRecIDs & ")"
            If ExcludeCenIDs.Length > 0 Then LocalQuery += " AND C.CEN_ID NOT IN (" & ExcludeCenIDs & ")"
            If IncludeCenIDs.Length > 0 Then LocalQuery += " AND C.CEN_ID IN (" & IncludeCenIDs & ")"
            Dim Param As Param_Voucher_InternalTransfer_GetToCenterList = New Param_Voucher_InternalTransfer_GetToCenterList()
            Param.ExcludeCenIDs = ExcludeCenIDs
            Param.ExcludeRecIDs = ExcludeRecIDs
            Param.IncludeCenIDs = IncludeCenIDs
            Param.IncludeRecIDs = IncludeRecIDs
            Param.openInsID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.InternalTransfer_GetToCenterList, LocalQuery, ClientScreen.Accounts_Voucher_Internal_Transfer, Tables.CENTRE_INFO, Param)
        End Function

        ''' <summary>
        ''' Get Txn Count, Shifted
        ''' </summary>
        ''' <param name="VoucherTxnCode"></param>
        ''' <param name="ItemID"></param>
        ''' <param name="AB_ID_1"></param>
        ''' <param name="AB_ID_2"></param>
        ''' <param name="VoucherDate"></param>
        ''' <param name="IsEdit"></param>
        ''' <param name="Master_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_GetTxnCount</remarks>
        Public Function GetCashTxnCount(ByVal VoucherTxnCode As String, ByVal ItemID As String, ByVal AB_ID_1 As String, ByVal AB_ID_2 As String, ByVal VoucherDate As String, ByVal IsEdit As Boolean, ByVal Master_Rec_ID As String) As Object
            Dim Param As Param_Voucher_InternalTransfer_GetTxnCount = New Param_Voucher_InternalTransfer_GetTxnCount()
            Param.VoucherDate = VoucherDate
            Param.VoucherTxnCode = VoucherTxnCode
            Param.Master_Rec_ID = Master_Rec_ID
            Param.ItemID = ItemID
            Param.IsEdit = IsEdit
            Param.AB_ID_2 = AB_ID_2
            Param.AB_ID_1 = AB_ID_1
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.InternalTransfer_GetCashTxnCount, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
        End Function

        Public Function GetCashPendingTxnCount(ByVal VoucherTxnCode As String, ByVal ItemID As String, ByVal AB_ID_1 As String, ByVal AB_ID_2 As String, ByVal VoucherDate As String, ByVal IsEdit As Boolean, ByVal Master_Rec_ID As String) As Object
            Dim Param As Param_Voucher_InternalTransfer_GetTxnCount = New Param_Voucher_InternalTransfer_GetTxnCount()
            Param.VoucherDate = VoucherDate
            Param.VoucherTxnCode = VoucherTxnCode
            Param.Master_Rec_ID = Master_Rec_ID
            Param.ItemID = ItemID
            Param.IsEdit = IsEdit
            Param.AB_ID_2 = AB_ID_2
            Param.AB_ID_1 = AB_ID_1
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.InternalTransfer_GetCashPendingTxnCount, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
        End Function

        Public Function GetNonCashTxnCount(ByVal VoucherTxnCode As String, ByVal ItemID As String, ByVal AB_ID_1 As String, ByVal AB_ID_2 As String, ByVal VoucherDate As String, ByVal Mode As String, ByVal BankRefID As String, ByVal RefNo As String, ByVal IsEdit As Boolean, ByVal Master_Rec_ID As String) As Object
            Dim Param As Param_Voucher_InternalTransfer_GetTxnCount = New Param_Voucher_InternalTransfer_GetTxnCount()
            Param.VoucherDate = VoucherDate
            Param.VoucherTxnCode = VoucherTxnCode
            Param.Master_Rec_ID = Master_Rec_ID
            Param.ItemID = ItemID
            Param.IsEdit = IsEdit
            Param.AB_ID_2 = AB_ID_2
            Param.AB_ID_1 = AB_ID_1
            Param.Mode = Mode
            Param.BankRefID = BankRefID
            Param.RefNo = RefNo

            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.InternalTransfer_GetNonCashTxnCount, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
        End Function

        Public Function GetNonCashPendingTxnCount(ByVal VoucherTxnCode As String, ByVal ItemID As String, ByVal AB_ID_1 As String, ByVal AB_ID_2 As String, ByVal VoucherDate As String, ByVal Mode As String, ByVal BankRefID As String, ByVal RefNo As String, ByVal IsEdit As Boolean, ByVal Master_Rec_ID As String) As Object
            Dim Param As Param_Voucher_InternalTransfer_GetTxnCount = New Param_Voucher_InternalTransfer_GetTxnCount()
            Param.VoucherDate = VoucherDate
            Param.VoucherTxnCode = VoucherTxnCode
            Param.Master_Rec_ID = Master_Rec_ID
            Param.ItemID = ItemID
            Param.IsEdit = IsEdit
            Param.AB_ID_2 = AB_ID_2
            Param.AB_ID_1 = AB_ID_1
            Param.Mode = Mode
            Param.BankRefID = BankRefID
            Param.RefNo = RefNo

            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.InternalTransfer_GetNonCashPendingTxnCount, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
        End Function

        'Shifted
        Public Function GetHQCenters() As DataTable
            Return GetHQCentersForCurrInstt(ClientScreen.Accounts_Voucher_Internal_Transfer)
        End Function

        'Shifted
        Public Function GetCenterToCentreUnmatchedCount() As Integer
            Dim InParam As Param_GetUnmatchedEntriesCentreToCentreCount = New Param_GetUnmatchedEntriesCentreToCentreCount
            InParam.CurrInsttID = cBase._open_Ins_ID
            InParam.OpenCenRecID = cBase._open_Cen_Rec_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.InternalTransfer_GetUnmatchedEntriesCentreToCentreCount, ClientScreen.Home_StartUp, InParam)
        End Function

        Public Function GetCenterToCentreUnmatchedCount(fromDate As DateTime, ToDate As DateTime) As Integer
            Dim InParam As Param_GetUnmatchedEntriesCentreToCentreCount = New Param_GetUnmatchedEntriesCentreToCentreCount
            InParam.CurrInsttID = cBase._open_Ins_ID
            InParam.OpenCenRecID = cBase._open_Cen_Rec_ID
            InParam.FromDate = fromDate
            InParam.ToDate = ToDate
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.InternalTransfer_GetUnmatchedEntriesCentreToCentreCount, ClientScreen.Home_StartUp, InParam)
        End Function

        Public Function GetCenterID(ByVal Cen_Rec_ID As String) As Object
            Return GetCenterIDByCenRecID(ClientScreen.Accounts_Voucher_Internal_Transfer, Cen_Rec_ID)
        End Function

        'Shifted
        Public Function GetPurposes() As DataTable
            ' Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Internal_Transfer, "PUR_NAME", "PUR_ID")
            Dim InParam As Param_GetMisc = New Param_GetMisc()
            '  InParam.MiscId = MiscId
            InParam.MiscNameColumnHead = "PUR_NAME"
            InParam.RecIDColumnHead = "PUR_ID"
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Accounts_Voucher_Internal_Transfer, InParam)
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional Bank_Account_Rec_ID As String = Nothing) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common
            Param.Bank_Account_Rec_ID = Bank_Account_Rec_ID
            Return _BA.GetList(ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
        End Function

        'Shifted
        Public Function GetBranches(ByVal BranchIDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(BranchIDs, ClientScreen.Accounts_Voucher_Internal_Transfer, "B.BI_BANK_NAME AS Name,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as Branch, A.REC_ID AS BB_BRANCH_ID,B.REC_ID as BANK_ID ")
        End Function

        ''' <summary>
        ''' Get_Tf_Banks, Shifted
        ''' </summary>
        ''' <param name="CentreWise"></param>
        ''' <param name="Cen_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_Get_Tf_Banks</remarks>
        Public Function Get_Tf_Banks(ByVal CentreWise As Boolean, Optional ByVal Cen_ID As String = "", Optional TrfBank_Account_Acc_No As String = Nothing) As DataTable
            Dim Query As String = ""
            If CentreWise Then
                Dim Param As Param_Voucher_InternalTransferGet_Tf_Banks = New Param_Voucher_InternalTransferGet_Tf_Banks
                Param.CenId = Cen_ID
                Param.TrfBank_Account_Acc_No = TrfBank_Account_Acc_No
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.InternalTransfer_Get_Tf_Banks, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
            Else
                Query = " SELECT BI_BANK_NAME as TRF_BI_BANK_NAME ,BI_SHORT_NAME AS TRF_BI_SHORT_NAME,REC_ID as TRF_BI_ID " &
                        " From   BANK_INFO     " &
                        " Where  REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
                Return GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_Internal_Transfer) 'Shifted
            End If
        End Function

        ''' <summary>
        ''' Insert Master, Shifted
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_InsertMaster</remarks>
        Public Function InsertMaster(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherInternalTransfer) As Boolean
            'InMInfo.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead
            Return InsertRecord(RealTimeService.RealServiceFunctions.InternalTransfer_InsertMaster, ClientScreen.Accounts_Voucher_Internal_Transfer, InMInfo)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherInternalTransfer) As Boolean
            'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead
            Return InsertRecord(RealTimeService.RealServiceFunctions.InternalTransfer_Insert, ClientScreen.Accounts_Voucher_Internal_Transfer, InParam)
        End Function

        ''' <summary>
        ''' Insert Purpose, Shifted
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_InsertPurpose</remarks>
        Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherInternalTransfer) As Boolean
            'InPurpose.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead
            Return InsertRecord(RealTimeService.RealServiceFunctions.InternalTransfer_InsertPurpose, ClientScreen.Accounts_Voucher_Internal_Transfer, InPurpose)
        End Function

        ''' <summary>
        ''' Update Master, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_UpdateMaster</remarks>
        Public Function UpdateMaster(ByVal UpParam As Parameter_UpdateMasterInfo_VoucherInternalTransfer) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Internal_Transfer)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.InternalTransfer_UpdateMaster, ClientScreen.Accounts_Voucher_Internal_Transfer, UpParam)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_VoucherInternalTransfer) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Internal_Transfer)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.InternalTransfer_Update, ClientScreen.Accounts_Voucher_Internal_Transfer, UpParam)
        End Function

        ' ''' <summary>
        ' ''' Update CrossReference,shifted
        ' ''' </summary>
        ' ''' <param name="Cross_Ref_ID"></param>
        ' ''' <param name="RecID"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.InternalTransfer_Update_CrossReference</remarks>
        'Public Function Update_CrossReference(ByVal Cross_Ref_ID As String, ByVal RecID As String) As Boolean
        '    Dim Param As Param_Voucher_InternalTransfer_Update_CrossReference = New Param_Voucher_InternalTransfer_Update_CrossReference()
        '    Param.Cross_Ref_ID = Cross_Ref_ID
        '    Param.RecID = RecID
        '    Return UpdateRecord(RealTimeService.RealServiceFunctions.InternalTransfer_Update_CrossReference, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
        'End Function

        ''' <summary>
        ''' Match Transfers, Shifted
        ''' </summary>
        ''' <param name="MatchingRecID"></param>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_MatchTransfers</remarks>
        Public Function MatchTransfers(ByVal MatchingRecID As String, ByVal RecID As String) As Boolean
            Dim Result As Boolean = True
            Dim Param As Param_Voucher_InternalTransfer_MatchTransfers = New Param_Voucher_InternalTransfer_MatchTransfers()
            Param.MatchingRecID = MatchingRecID
            Param.RecID = RecID
            Result = UpdateRecord(RealTimeService.RealServiceFunctions.InternalTransfer_MatchTransfers, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
            Return Result
        End Function

        ''' <summary>
        ''' UnMatch Transfers, Shifted
        ''' </summary>
        ''' <param name="MatchingRecID"></param>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_UnMatchTransfers</remarks>
        Public Function UnMatchTransfers(ByVal MatchingRecID As String, ByVal RecID As String, TxnDate As DateTime) As Boolean
            Dim Result As Boolean = True
            Dim Param As Param_Voucher_InternalTransfer_UnMatchTransfers = New Param_Voucher_InternalTransfer_UnMatchTransfers()
            Param.MatchingRecID = MatchingRecID
            Param.RecID = RecID
            Param.TxnDate = TxnDate
            Result = UpdateRecord(RealTimeService.RealServiceFunctions.InternalTransfer_UnMatchTransfers, ClientScreen.Accounts_Voucher_Internal_Transfer, Param)
            Return Result
        End Function

        ''' <summary>
        ''' Update Purpose, Shifted
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.InternalTransfer_UpdatePurpose</remarks>
        Public Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_VoucherInternalTransfer) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.InternalTransfer_UpdatePurpose, ClientScreen.Accounts_Voucher_Internal_Transfer, UpPurpose)
        End Function

        Public Overloads Function DeleteByMasterID(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Internal_Transfer)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Internal_Transfer)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_Internal_Transfer)
        End Function

        Public Overloads Function DeletePurpose(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Internal_Transfer)
            Return DeleteByCondition("TR_REC_ID ='" & Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_Internal_Transfer)
        End Function

        Public Overloads Function DeleteMaster(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Internal_Transfer)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_Internal_Transfer)
        End Function

        Public Function Insert_InternalTransfer_Txn(InParam As Param_Txn_Insert_InternalTransfer)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.InternalTransfer_Insert_InternalTransfer_Txn, ClientScreen.Accounts_Voucher_Internal_Transfer, InParam)
        End Function

        Public Function Update_InternalTransfer_Txn(UpParam As Param_Txn_Update_InternalTransfer)
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.param_UpdateMaster.RecID, ClientScreen.Accounts_Voucher_Internal_Transfer)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.InternalTransfer_Update_InternalTransfer_Txn, ClientScreen.Accounts_Voucher_Internal_Transfer, UpParam)
        End Function

        Public Function Delete_InternalTransfer_Txn(DelParam As Param_Txn_Delete_InternalTransfer)
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.MID_DeleteMaster, ClientScreen.Accounts_Voucher_Internal_Transfer)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + DelParam.MID_DeleteMaster + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.InternalTransfer_Delete_InternalTransfer_Txn, ClientScreen.Accounts_Voucher_Internal_Transfer, DelParam)
        End Function
    End Class
#End Region
End Class
