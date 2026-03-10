Imports Common_Lib.RealTimeService

Partial Public Class DbOperations
    <Serializable>
    Public Class Audit
        Inherits SharedVariables
        <Serializable>
        Public Class Return_GetDocumentMapping_With_AdditionalInfo
            Public Property DocumentMapping As List(Of Return_GetDocumentMapping)
            Public Property AdditionalInfo As List(Of Return_GetEntry_AdditionalInfo)
        End Class
        <Serializable>
        Public Class Return_GetEntry_AdditionalInfo
            Public Property Col1 As String
            Public Property Col2 As String
        End Class
        <Serializable>
        Public Class Return_GetDocumentMapping
            Public Property Item_Name As String
            Public Property Document_Name As String
            Public Property Reason As String
            Public Property FromDate As DateTime?
            Public Property ToDate As DateTime?
            Public Property Description As String
            Public Property TxnID As String
            Public Property TxnMID As String
            Public Property Has_doc As Boolean?
            Public Property Doc_Status As String
            Public Property Params_Mandatory As Int32
            Public Property LABEL_FROM_DATE As String
            Public Property LABEL_TO_DATE As String
            Public Property LABEL_DESCRIPTION As String
            Public Property ATTACH_ID As String
            Public Property Document_Category As String
            Public Property Document_ID As String
            Public Property Tr_Code As Int32?
            Public Property MAP_ID As Int32?
            Public Property UniqueID As String
            Public Property ATTACH_FILE_NAME As String
            Public Property Doc_Checking_Status As String
            Public Property Rejection_Reason As String
            Public Property Attachment_Action_Status As String
            Public Property Vouching_Status As String
            Public Property Vouching_Auditor As String
            Public Property Vouching_Remarks As String
            Public Property Vouching_Details As String
            Public Property Vouching_History As String
            Public Property Vouching_During_Audit As Boolean?
            Public Property ReasonID As Int32?
        End Class
        <Serializable>
        Public Class Return_GetDocumentChecking
            Public Property Institute_Name As String
            Public Property UID As String
            Public Property Document_Category As String
            Public Property Document_Name As String
            Public Property Document_File_Name As String
            Public Property From_Date As DateTime?
            Public Property To_Date As DateTime?
            Public Property Decument_Description As String
            Public Property Document_Attachment_ID As String
            Public Property Document_Rejection_Reason As String
            Public Property Document_Params_Mandatory As Int32
            Public Property Document_LABEL_FROM_DATE As String
            Public Property Document_LABEL_TO_DATE As String
            Public Property Document_LABEL_DESCRIPTION As String
            Public Property Attachment_Action_Status As String
            Public Property Document_Last_Action_By As String
            Public Property Document_Last_Action_On As DateTime
            Public Property Document_Added_On As DateTime
            Public Property Document_Checking_Status As String
        End Class
        <Serializable>
        Public Class Return_GetItemList
            Public Property ItemID As String
            Public Property ItemName As String
            Public Property LedgerID As String
            Public Property Ledger As String
        End Class
        <Serializable>
        Public Class Return_GetPurpose
            Public ID As String
            Public Purpose As String
        End Class
        <Serializable>
        Public Class Param_GetCashBookVouching
            Public Vouching_Status As String = ""
            Public Vouched_by As String = ""
            ''' <summary>
            ''' comma separated zone id list
            ''' </summary>
            Public ZoneIDs As String = ""
            ''' <summary>
            ''' comma separated sub zone id list
            ''' </summary>
            Public SubZoneIDs As String = ""
            ''' <summary>
            ''' comma separated state id list
            ''' </summary>
            Public StateIDs As String = ""
            ''' <summary>
            ''' comma separated Cen id list
            ''' </summary>
            Public CenIDs As String = ""
            ''' <summary>
            ''' comma separated Tr_Code list
            ''' </summary>
            Public Tr_Codes As String = ""
            ''' <summary>
            ''' comma separated Ledger id list
            ''' </summary>
            Public LedgerIDs As String = ""
            ''' <summary>
            ''' comma separated Item id list
            ''' </summary>
            Public ItemRecIDs As String = ""
            Public AmountFrom As Decimal = -1
            Public AmountTo As Decimal = -1
            ''' <summary>
            ''' hard coded as Cash/Bank/JV 
            ''' </summary>
            Public Mode As String = ""
            ''' <summary>
            ''' hard coded as Receipt / Payment 
            ''' </summary>
            Public Type As String = ""
            ''' <summary>
            ''' comma separated Purpose id list
            ''' </summary>
            Public PurposeIDs As String = ""
            Public Narration As String = ""
            Public Rejection_Reason As String = ""
            Public Reviewed_By As String = ""
            Public Review_Count_From As Int32 = -1
            Public Review_Count_To As Int32 = -1
            ''' <summary>
            ''' comma separated Attached Document id list, Entries containing ANY of the selected documents attached
            ''' </summary>
            Public Document_IDs As String = ""
            ''' <summary>
            ''' Exclusive  / All 
            ''' </summary>
            Public Selection_Pool As String = "Exclusive"
            ''' <summary>
            ''' All / Top 100
            ''' </summary>
            Public Record_Pool_Size As String = "All"
            Public AuditorID As String
            Public InsttID As String
            Public YearID As Int32
            Public FreshData As Boolean
            Public DocumentCategory As String = ""
            Public DocumentDescription As String = ""
            Public DocumentFromDate As DateTime?
            Public DocumentToDate As DateTime?
            Public VouchingCategory As String = ""
            Public Skip_Audited_Period As Boolean = True
        End Class
        <Serializable>
        Public Class Return_GetCashBookVouching
            Public Property iTR_VNO As String
            Public Property iTR_DATE As DateTime?
            Public Property iTR_ITEM_ID As String
            Public Property iTR_ITEM As String
            Public Property iLED_ID As String
            Public Property iTR_HEAD As String
            Public Property iTR_SUB_ID As String
            Public Property iTR_AB_ID_1 As String
            Public Property iTR_PARTY_1 As String
            Public Property iTR_CR_ID As String
            Public Property iTR_CR_NAME As String
            Public Property iTR_DATE_SERIAL As Integer?
            Public Property iTR_DATE_SHOW As String
            Public Property iTR_ENTRY As String
            Public Property iTR_REC_CASH As Decimal?
            Public Property iTR_REC_BANK As Decimal?
            Public Property iTR_REC_JOURNAL As Decimal?
            Public Property iTR_REC_TOTAL As Decimal?
            Public Property iTR_PAY_CASH As Decimal?
            Public Property iTR_PAY_BANK As Decimal?
            Public Property iTR_PAY_JOURNAL As Decimal?
            Public Property iTR_PAY_TOTAL As Decimal?
            Public Property iTR_NARRATION As String
            Public Property iTR_ROW_POS As String
            Public Property iTR_TYPE As String
            Public Property iTR_CODE As Integer?
            Public Property iREC_ID As String
            Public Property iTR_M_ID As String
            Public Property iTR_SR_NO As Integer?
            Public Property iTR_SORT As String
            Public Property iREC_ADD_ON As DateTime?
            Public Property iTR_TEMP_ID As String
            Public Property iTR_REF_NO As Integer?
            Public Property iACTION_STATUS As String
            Public Property iREC_EDIT_ON As DateTime?
            Public Property iREC_STATUS_ON As DateTime?
            Public Property iREC_ADD_BY As String
            Public Property iREC_EDIT_BY As String
            Public Property iREC_STATUS_BY As String
            Public Property iCross_Ref_ID As String
            Public Property iRef_no As String
            Public Property Attachment_IDs As String
            Public Property iPurpose As String
            Public Property Advanced_Filter As String
            Public Property Sr As Integer
            Public Property iREQ_ATTACH_COUNT As Integer?
            Public Property iCOMPLETE_ATTACH_COUNT As Integer?
            Public Property iRESPONDED_COUNT As Integer?
            Public Property iREJECTED_COUNT As Integer?
            Public Property iOTHER_ATTACH_CNT As Integer?
            Public Property iALL_ATTACH_CNT As Integer?
            Public Property Grid_PK As String
            Public Property UID As String
            'Public Property VOUCHING_STATUS As String
            'Public Property Vouching_Remarks As String
            Public Property Review_Count As Int32
            Public Property Reviewed_By As String
            Public Property CEN_ID As Int32
            Public Property VOUCHING_CATEGORY As String
        End Class
        <Serializable>
        Public Class Return_AuditStatusPage_Public
            Public Property EventName As String
            Public Property Fin_Year As Integer
            Public Property UID As String
            Public Property Status As String
            Public Property Auditor As String
            Public Property Reg_No_ As Integer?
        End Class
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetDocumentMapping(ByVal REC_ID As String, ByVal TR_M_ID As String, Screen As ClientScreen, Optional Show_New_Others_Option As Boolean = True, Optional CEN_ID As Integer = 0) As List(Of Return_GetDocumentMapping)
            Dim param As Param_GetDocumentMapping = New Param_GetDocumentMapping()
            param.REC_ID = REC_ID
            param.TR_M_ID = TR_M_ID
            param.SCREEN = Screen
            param.SHOW_NEW_OTHERS = Show_New_Others_Option
            param.CenID = CEN_ID
            Dim _dataset = GetDatasetOfRecordsBySP(Common_Lib.RealTimeService.RealServiceFunctions.Audit_GetDocumentMapping, Screen, param)
            Dim _docList As List(Of Return_GetDocumentMapping) = New List(Of Return_GetDocumentMapping)()
            Dim _Table As DataTable = _dataset.Tables(0)
            For Each row As DataRow In _Table.Rows
                Dim newdata = New Return_GetDocumentMapping()
                newdata.Item_Name = row.Field(Of String)("Item_Name")
                newdata.Document_Name = row.Field(Of String)("Document_Name")
                newdata.Reason = row.Field(Of String)("Reason")
                newdata.FromDate = row.Field(Of DateTime?)("FromDate")
                newdata.ToDate = row.Field(Of DateTime?)("ToDate")
                newdata.Description = row.Field(Of String)("Description")
                newdata.TxnID = row.Field(Of String)("TxnID")
                newdata.TxnMID = row.Field(Of String)("TxnMID")
                newdata.Has_doc = row.Field(Of Boolean?)("Has_doc")
                newdata.Doc_Status = row.Field(Of String)("Doc_Status")
                newdata.Params_Mandatory = row.Field(Of Int32)("Params_Mandatory")
                newdata.LABEL_FROM_DATE = row.Field(Of String)("LABEL_FROM_DATE")
                newdata.LABEL_TO_DATE = row.Field(Of String)("LABEL_TO_DATE")
                newdata.LABEL_DESCRIPTION = row.Field(Of String)("LABEL_DESCRIPTION")
                newdata.ATTACH_ID = row.Field(Of String)("ATTACH_ID")
                newdata.Document_Category = row.Field(Of String)("Document_Category")
                newdata.Document_ID = row.Field(Of String)("Document_ID")
                newdata.Tr_Code = row.Field(Of Int32?)("Tr_Code")
                newdata.MAP_ID = row.Field(Of Int32?)("MAP_ID")
                newdata.UniqueID = row.Field(Of String)("UniqueID")
                newdata.ATTACH_FILE_NAME = row.Field(Of String)("ATTACH_FILE_NAME")
                newdata.Doc_Checking_Status = row.Field(Of String)("Doc_Checking_Status")
                newdata.Rejection_Reason = row.Field(Of String)("Rejection_Reason")
                newdata.Attachment_Action_Status = row.Field(Of String)("ATTACH_REC_STATUS")
                newdata.Vouching_Status = row.Field(Of String)("Vouching_Status")
                newdata.Vouching_Auditor = row.Field(Of String)("Vouching_Auditor")
                newdata.Vouching_Remarks = row.Field(Of String)("Vouching_Remarks")
                newdata.Vouching_Details = row.Field(Of String)("Vouching_Details")
                newdata.Vouching_History = row.Field(Of String)("Vouching_History")
                newdata.Vouching_During_Audit = row.Field(Of Boolean?)("Vouching_During_Audit")
                newdata.ReasonID = row.Field(Of Int32?)("ReasonID")
                _docList.Add(newdata)
            Next
            Return _docList
        End Function
        ''' <summary>
        ''' Used in Vouching Screen 
        ''' </summary>
        ''' <param name="REC_ID"></param>
        ''' <param name="TR_M_ID"></param>
        ''' <param name="CEN_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="Show_New_Others_Option"></param>
        ''' <returns></returns>
        Public Function GetDocumentMapping_With_Additional_Info(ByVal REC_ID As String, ByVal TR_M_ID As String, ByVal CEN_ID As Int32, Screen As ClientScreen, Optional Show_New_Others_Option As Boolean = True) As Return_GetDocumentMapping_With_AdditionalInfo
            Dim param As Param_GetDocumentMapping = New Param_GetDocumentMapping()
            param.REC_ID = REC_ID
            param.TR_M_ID = TR_M_ID
            param.SCREEN = Screen
            param.CenID = CEN_ID
            param.SHOW_NEW_OTHERS = Show_New_Others_Option
            param.ShowAdditionalInfo = True
            Dim _dataset = GetDatasetOfRecordsBySP(Common_Lib.RealTimeService.RealServiceFunctions.Audit_GetDocumentMapping, Screen, param)

            Dim _RetObj As New Return_GetDocumentMapping_With_AdditionalInfo

            Dim _docList As List(Of Return_GetDocumentMapping) = New List(Of Return_GetDocumentMapping)()
            Dim _Table As DataTable = _dataset.Tables(0)
            For Each row As DataRow In _Table.Rows
                Dim newdata = New Return_GetDocumentMapping()
                newdata.Item_Name = row.Field(Of String)("Item_Name")
                newdata.Document_Name = row.Field(Of String)("Document_Name")
                newdata.Reason = row.Field(Of String)("Reason")
                newdata.FromDate = row.Field(Of DateTime?)("FromDate")
                newdata.ToDate = row.Field(Of DateTime?)("ToDate")
                newdata.Description = row.Field(Of String)("Description")
                newdata.TxnID = row.Field(Of String)("TxnID")
                newdata.TxnMID = row.Field(Of String)("TxnMID")
                newdata.Has_doc = row.Field(Of Boolean?)("Has_doc")
                newdata.Doc_Status = row.Field(Of String)("Doc_Status")
                newdata.Params_Mandatory = row.Field(Of Int32)("Params_Mandatory")
                newdata.LABEL_FROM_DATE = row.Field(Of String)("LABEL_FROM_DATE")
                newdata.LABEL_TO_DATE = row.Field(Of String)("LABEL_TO_DATE")
                newdata.LABEL_DESCRIPTION = row.Field(Of String)("LABEL_DESCRIPTION")
                newdata.ATTACH_ID = row.Field(Of String)("ATTACH_ID")
                newdata.Document_Category = row.Field(Of String)("Document_Category")
                newdata.Document_ID = row.Field(Of String)("Document_ID")
                newdata.Tr_Code = row.Field(Of Int32?)("Tr_Code")
                newdata.MAP_ID = row.Field(Of Int32?)("MAP_ID")
                newdata.UniqueID = row.Field(Of String)("UniqueID")
                newdata.ATTACH_FILE_NAME = row.Field(Of String)("ATTACH_FILE_NAME")
                newdata.Doc_Checking_Status = row.Field(Of String)("Doc_Checking_Status")
                newdata.Rejection_Reason = row.Field(Of String)("Rejection_Reason")
                newdata.Attachment_Action_Status = row.Field(Of String)("ATTACH_REC_STATUS")
                newdata.Vouching_Status = row.Field(Of String)("Vouching_Status")
                newdata.Vouching_Auditor = row.Field(Of String)("Vouching_Auditor")
                newdata.Vouching_Remarks = row.Field(Of String)("Vouching_Remarks")
                newdata.Vouching_Details = row.Field(Of String)("Vouching_Details")
                newdata.Vouching_History = row.Field(Of String)("Vouching_History")
                newdata.Vouching_During_Audit = row.Field(Of Boolean?)("Vouching_During_Audit")
                newdata.ReasonID = row.Field(Of Int32?)("ReasonID")
                _docList.Add(newdata)
            Next
            _Table = _dataset.Tables(1)
            Dim _addList As List(Of Return_GetEntry_AdditionalInfo) = New List(Of Return_GetEntry_AdditionalInfo)()
            For Each row As DataRow In _Table.Rows
                Dim newdata = New Return_GetEntry_AdditionalInfo()
                newdata.Col1 = row.Field(Of String)("COL1")
                newdata.Col2 = row.Field(Of String)("COL2")
                _addList.Add(newdata)
            Next

            _RetObj.DocumentMapping = _docList
            _RetObj.AdditionalInfo = _addList

            Return _RetObj
        End Function
        Public Function GetAdditionalInfo(ByVal REC_ID As String, ByVal TR_M_ID As String, ByVal CEN_ID As Int32, Screen As ClientScreen) As List(Of Return_GetEntry_AdditionalInfo)
            Dim param As Param_GetAdditionalInfo = New Param_GetAdditionalInfo()
            param.REC_ID = REC_ID
            param.TR_M_ID = TR_M_ID
            param.SCREEN = Screen
            param.CenID = CEN_ID
            Dim _Table = GetListOfRecordsBySP(Common_Lib.RealTimeService.RealServiceFunctions.Audit_GetDoc_AdditionalInfo, Screen, param)
            Dim AdditionalInfo As List(Of Return_GetEntry_AdditionalInfo) = New List(Of Return_GetEntry_AdditionalInfo)()
            For Each row As DataRow In _Table.Rows
                Dim newdata = New Return_GetEntry_AdditionalInfo()
                newdata.Col1 = row.Field(Of String)("COL1")
                newdata.Col2 = row.Field(Of String)("COL2")
                AdditionalInfo.Add(newdata)
            Next
            Return AdditionalInfo
        End Function
        Public Function GetDocumentChecking(Audit_Status As Audited_Status_Options, Data_Scope As Data_Scope_Options, Optional ByVal Checking_Status As String = Nothing, Optional ByVal Document_ID As String = Nothing, Optional ByVal Document_Category As String = Nothing, Optional ByVal CheckedBy As String = "") As List(Of Return_GetDocumentChecking)
            Dim param As Param_GetDocumentChecking = New Param_GetDocumentChecking()
            param.Status = Checking_Status
            param.Document_ID = Document_ID
            param.Document_Category = Document_Category
            param.Audited_Status = Audit_Status
            param.Data_Scope = Data_Scope
            param.CheckedBy = CheckedBy
            Dim _Table = GetDataListOfRecords(Common_Lib.RealTimeService.RealServiceFunctions.Audit_GetDocumentchecking, ClientScreen.Accounts_CashBook, param)
            Dim _docList As List(Of Return_GetDocumentChecking) = New List(Of Return_GetDocumentChecking)()

            For Each row As DataRow In _Table.Rows
                Dim newdata = New Return_GetDocumentChecking()
                newdata.Institute_Name = row.Field(Of String)("INS")
                newdata.UID = row.Field(Of String)("UID")
                newdata.Document_Category = row.Field(Of String)("Category")
                newdata.Document_Name = row.Field(Of String)("Document")
                newdata.Document_File_Name = row.Field(Of String)("File Name")
                If row.Table.Columns.Contains("From Date") Then
                    newdata.From_Date = row.Field(Of DateTime?)("From Date")
                End If
                If row.Table.Columns.Contains("To Date") Then
                    newdata.To_Date = row.Field(Of DateTime?)("To Date")
                End If
                newdata.Decument_Description = row.Field(Of String)("Description")
                newdata.Document_Attachment_ID = row.Field(Of String)("ATTACHMENT_ID")
                newdata.Document_Rejection_Reason = row.Field(Of String)("Rejection_Reason")
                newdata.Document_Params_Mandatory = row.Field(Of Int32)("Params_Mandatory")
                newdata.Document_LABEL_FROM_DATE = row.Field(Of String)("LABEL_FROM_DATE")
                newdata.Document_LABEL_TO_DATE = row.Field(Of String)("LABEL_TO_DATE")
                newdata.Document_LABEL_DESCRIPTION = row.Field(Of String)("LABEL_DESCRIPTION")
                newdata.Attachment_Action_Status = row.Field(Of String)("ATTACH_REC_STATUS")
                newdata.Document_Added_On = row.Field(Of DateTime)("Added_On")
                newdata.Document_Last_Action_By = row.Field(Of String)("Last_Action_By")
                newdata.Document_Last_Action_On = row.Field(Of DateTime)("Last_Action_On")
                newdata.Document_Checking_Status = row.Field(Of String)("Doc_Checking_Status")
                _docList.Add(newdata)
            Next
            Return _docList
        End Function
        'Public Function AttachmentAdded(ByVal RefRec_ID As String, ByVal Mapping_ID As Int32, ByVal Screen As ClientScreen) As Boolean
        '    Dim InParam As New Param_InsertDocumentMapResponse
        '    InParam.Attachment_ID = Attachment_ID
        '    InParam.Document_Present = True
        '    InParam.MAP_ID = Mapping_ID
        '    InParam.RefRec_ID = RefRec_ID
        '    Return InsertRecord(RealTimeService.RealServiceFunctions.Audit_InsertDocumentMapResponse, Screen, InParam)
        'End Function
        Public Function DocumentAbsentReasonAdded(ByVal RefRec_ID As String, ByVal Reason As String, ByVal Mapping_ID As Int32, ByVal Screen As ClientScreen) As Boolean
            Dim InParam As New Param_InsertDocumentMapResponse
            InParam.REASON = Reason
            InParam.MAP_ID = Mapping_ID
            InParam.RefRec_ID = RefRec_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Audit_InsertDocumentMapResponse, Screen, InParam)
        End Function
        Public Function DocumentAbsentReasonUpdated(ByVal RefRec_ID As String, ByVal Reason As String, ByVal Mapping_ID As Int32, ByVal Screen As ClientScreen) As Boolean
            Dim InParam As New Param_InsertDocumentMapResponse
            InParam.REASON = Reason
            InParam.MAP_ID = Mapping_ID
            InParam.RefRec_ID = RefRec_ID
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Audit_UpdtaeDocumentMapResponse, Screen, InParam)
        End Function
        'Public Function UnVouchEntryByReference(ByVal RefEntryID As String, screen As ClientScreen) As Boolean
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim paramters As String() = {"@EntryID", "@UserID"}
        '    Dim values() As Object = {RefEntryID, cBase._open_user_id}
        '    Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String}
        '    Dim lengths() As Integer = {36, 255}
        '    Return InsertBySPPublic(Tables.VOUCHING_AUDIT, "[sp_Insert_Entry_Unvouched]", paramters, values, dbTypes, lengths, screen, _RealService)
        'End Function
        Public Function UnVouchEntryByReference(ByVal RefEntryID As String, screen As ClientScreen, Optional AttachmentID As String = Nothing, Optional AttachmentResponseID As Int32 = Nothing) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@EntryID", "@UserID", "@Attachment_ID", "@ResponseID"}
            Dim values() As Object = {RefEntryID, cBase._open_user_id, AttachmentID, AttachmentResponseID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.Int32}
            Dim lengths() As Integer = {36, 255, 36, 4}
            'dbService.UpdateBySP(ConnectOneWS.Tables.VOUCHING_AUDIT, "[sp_Insert_Entry_Unvouched]", paramters, values, dbTypes, lengths, inBasicParam)
            Return InsertBySPPublic(Tables.VOUCHING_AUDIT, "[sp_Insert_Entry_Unvouched]", paramters, values, dbTypes, lengths, screen, _RealService)
            Return True
        End Function
        Public Overloads Function DeleteAllVouchingAgainstReference(ByVal RefEntryID As String, screen As ClientScreen) As Boolean
            Return ExecuteGroup(RealServiceFunctions.Audit_DeleteAllVouchingAgainstReference, screen, RefEntryID)
        End Function
        Public Overloads Function DeleteAllVouchingAgainstEntry(ByVal RefEntryID As String, screen As ClientScreen) As Boolean
            Return ExecuteGroup(RealServiceFunctions.Audit_DeleteAllVouchingAgainstEntry, screen, RefEntryID)
        End Function
        Public Overloads Function DocumentAbsentReasonDelete(ByVal RefRec_ID As String, ByVal Mapping_ID As Int32) As Boolean
            'DeleteAllVouchingAgainstEntry(RefRec_ID, ClientScreen.CommonFunctions)
            'UnVouchEntryByReference(RefRec_ID, ClientScreen.CommonFunctions, Nothing, Mapping_ID)
            DeleteByCondition("COALESCE(VA_ATTACHMENT_RESP_ID,0) = " & Mapping_ID.ToString() & "", Tables.VOUCHING_AUDIT, ClientScreen.Accounts_CashBook)
            Return DeleteByCondition("TR_DOC_RESP_MAP_ID = " & Mapping_ID.ToString() & " And TR_DOC_RESP_REF_REC_ID = '" & RefRec_ID & "'", Tables.TRANSACTION_DOC_MAPPING_RESPONSE, ClientScreen.Accounts_CashBook)
        End Function
        Public Function EntryVouchingAccepted(ByVal EntryID As String, ByVal Remarks As String, ByVal EntryCenID As Int32, ByVal Screen As ClientScreen, Optional ByVal VouchingCategory As String = "CASHBOOK", Optional ByVal AttachmentID As String = Nothing, Optional ByVal TxnDocRespID As Int32? = Nothing) As Boolean
            Dim Inparam As New Param_InsertVouchingStatus
            Inparam.EntryID = EntryID
            Inparam.Remarks = Remarks
            Inparam.VouchingCategory = VouchingCategory
            Inparam.Status = "ACCEPTED"
            Inparam.EntryCenID = EntryCenID
            Inparam.AttachmentID = AttachmentID
            Inparam.TxnDocRespID = TxnDocRespID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Audit_InsertVouchingStatus, Screen, Inparam)
        End Function
        Public Function EntryVouchingRejected(ByVal AuditorID As String, ByVal EntryID As String, ByVal Remarks As String, ByVal EntryCenID As Int32, ByVal Screen As ClientScreen, Optional ByVal VouchingCategory As String = "CASHBOOK", Optional ByVal AttachmentID As String = Nothing, Optional ByVal TxnDocRespID As Int32? = Nothing) As Boolean
            Dim Inparam As New Param_InsertVouchingStatus
            Inparam.EntryID = EntryID
            Inparam.Remarks = Remarks
            Inparam.VouchingCategory = VouchingCategory
            Inparam.Status = "REJECTED"
            Inparam.EntryCenID = EntryCenID
            Inparam.AttachmentID = AttachmentID
            Inparam.TxnDocRespID = TxnDocRespID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Audit_InsertVouchingStatus, Screen, Inparam)
        End Function
        Public Function EntryVouchingSkip(ByVal AuditorID As String, ByVal EntryID As String, ByVal Screen As ClientScreen) As Boolean
            DeleteByCondition("USER_ID = '" + AuditorID + "' AND ENTRY_ID ='" + EntryID + "'", Tables.VOUCHING_AUDIT_ALLOTTMENT, Screen)
            Return True
        End Function
        ''' <summary>
        ''' Gives Item list for specified LedgerIDs. If nothing specific then gives all Items
        ''' </summary>
        ''' <param name="LedID">String Array of possible multiple Ledger Ids for which items are to be returned</param>
        ''' <returns></returns>
        Public Function GetItemList(Optional LedIDs As String() = Nothing) As List(Of Return_GetItemList)
            Dim _itemList As List(Of Return_GetItemList) = New List(Of Return_GetItemList)()
            If LedIDs Is Nothing Then
                Dim param As New Param_GetItemsLedgerCommon
                param.LedgerID = ""
                Dim _Table As DataTable = GetItems_Ledger(ClientScreen.Account_CashbookAuditor, "", param)
                For Each row As DataRow In _Table.Rows
                    Dim newdata = New Return_GetItemList()
                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.ItemID = row.Field(Of String)("ItemID")
                    newdata.LedgerID = row.Field(Of String)("LED_ID")
                    newdata.Ledger = row.Field(Of String)("LEDGER")
                    _itemList.Add(newdata)
                Next
            Else
                For Each LedID As String In LedIDs
                    Dim param As New Param_GetItemsLedgerCommon
                    param.LedgerID = LedID
                    Dim _Table As DataTable = GetItems_Ledger(ClientScreen.Account_CashbookAuditor, "", param)
                    For Each row As DataRow In _Table.Rows
                        Dim newdata = New Return_GetItemList()
                        newdata.ItemName = row.Field(Of String)("ItemName")
                        newdata.ItemID = row.Field(Of String)("ItemID")
                        newdata.LedgerID = row.Field(Of String)("LED_ID")
                        newdata.Ledger = row.Field(Of String)("LEDGER")
                        _itemList.Add(newdata)
                    Next
                Next
            End If
            Return _itemList
        End Function
        ''' <summary>
        ''' Gives joint list of all Global as well as Center Specific Purpose
        ''' </summary>
        ''' <returns></returns>
        Public Function GetPurpose() As List(Of Return_GetPurpose)
            Dim InParam As Param_GetMisc = New Param_GetMisc()
            '  InParam.MiscId = MiscId
            InParam.MiscNameColumnHead = "Purpose"
            InParam.RecIDColumnHead = "ID"
            Dim ret_table As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Account_CashbookAuditor, InParam)
            Dim purposelist As List(Of Return_GetPurpose) = New List(Of Return_GetPurpose)
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                For Each row As DataRow In ret_table.Rows
                    Dim newdata As Return_GetPurpose = New Return_GetPurpose()
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Purpose = row.Field(Of String)("Purpose")
                    purposelist.Add(newdata)
                Next
                Return purposelist
            End If
        End Function
        Public Function GetCashBookVouching(inParam As Param_GetCashBookVouching) As List(Of Return_GetCashBookVouching)
            ' Dim Counter As Int32
            Dim ZoneIDs As String() = inParam.ZoneIDs.Split(",")
            Dim SubZoneIDs As String() = inParam.SubZoneIDs.Split(",")
            Dim StateIDs As String() = inParam.StateIDs.Split(",")
            Dim CenIDs As String() = inParam.CenIDs.Split(",")
            Dim Tr_Codes As String() = inParam.Tr_Codes.Split(",")
            Dim LedgerIDs As String() = inParam.LedgerIDs.Split(",")
            Dim ItemRecIDs As String() = inParam.ItemRecIDs.Split(",")
            Dim PurposeIDs As String() = inParam.PurposeIDs.Split(",")
            Dim Document_IDs As String() = inParam.Document_IDs.Split(",")
            Dim _ZoneIDs As String = inParam.ZoneIDs, _SubZoneIDs = inParam.SubZoneIDs, _StateIDs = inParam.StateIDs, _CenIDs = inParam.CenIDs, _Tr_Codes = inParam.Tr_Codes, _LedgerIDs = inParam.LedgerIDs, _ItemRecIDs = inParam.ItemRecIDs, _PurposeIDs = inParam.PurposeIDs, _Document_IDs = inParam.Document_IDs

            'Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@Vouching_Status", "@Vouched_by", "@ZoneIDs", "@SubZoneIDs", "@StateIDs", "@CenIDs", "@Tr_Codes", "@LedgerIDs", "@ItemRecIDs", "@AmountFrom", "@AmountTo", "@Mode", "@Type", "@PurposeIDs", "@Narration", "@Rejection_Reason", "@Reviewed_By", "@Review_Count_From", "@Review_Count_To", "@Document_IDs", "@Selection_Pool", "@Record_Pool_Size", "@AuditorID", "@InsttID", "@YearID", "@FreshData", "@DocumentCategory", "@DocumentFromDate", "@DocumentToDate", "@DocumentDescription", "@VouchingCategory", "@CEN_ID", "@SKIP_AUDITED_PERIOD"}
            Dim values() As Object = {inParam.Vouching_Status, inParam.Vouched_by, _ZoneIDs, _SubZoneIDs, _StateIDs, _CenIDs, _Tr_Codes, _LedgerIDs, _ItemRecIDs, inParam.AmountFrom, inParam.AmountTo, inParam.Mode, inParam.Type, _PurposeIDs, inParam.Narration, inParam.Rejection_Reason, inParam.Reviewed_By, inParam.Review_Count_From, inParam.Review_Count_To, _Document_IDs, inParam.Selection_Pool, inParam.Record_Pool_Size, inParam.AuditorID, inParam.InsttID, inParam.YearID, inParam.FreshData, inParam.DocumentCategory, inParam.DocumentFromDate, inParam.DocumentToDate, inParam.DocumentDescription, inParam.VouchingCategory, cBase._open_Cen_ID, inParam.Skip_Audited_Period}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Decimal, DbType.Decimal, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.Boolean, DbType.String, DbType.DateTime2, DbType.DateTime2, DbType.String, DbType.String, DbType.Int32, DbType.Boolean}
            Dim lengths() As Integer = {50, 255, 8000, 8000, 8000, 8000, 8000, 8000, 8000, 19, 19, 20, 20, 8000, 8000, 8000, 255, 4, 4, 8000, 50, 50, 255, 255, 4, 2, 255, 12, 12, 8000, 50, 4, 2}
            Dim ret_table As DataTable = _RealService.ListFromSP(Tables.TRANSACTION_INFO, "[get_Cashbook_Vouching]", Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))

            Dim purposelist As List(Of Return_GetCashBookVouching) = New List(Of Return_GetCashBookVouching)
            Dim Sr = 1
            If (ret_table Is Nothing) Then
                Return Nothing
            Else
                For Each row As DataRow In ret_table.Rows
                    Dim newdata As Return_GetCashBookVouching = New Return_GetCashBookVouching()
                    'newdata.ID = row.Field(Of String)("ID")
                    newdata.iTR_VNO = row.Field(Of String)("iTR_VNO")
                    newdata.iTR_DATE = row.Field(Of DateTime?)("iTR_DATE")
                    newdata.iTR_ITEM_ID = row.Field(Of String)("iTR_ITEM_ID")
                    newdata.iTR_ITEM = row.Field(Of String)("iTR_ITEM")
                    newdata.iLED_ID = row.Field(Of String)("iLED_ID")
                    newdata.iTR_HEAD = row.Field(Of String)("iTR_HEAD")
                    newdata.iTR_SUB_ID = row.Field(Of String)("iTR_SUB_ID")
                    newdata.iTR_AB_ID_1 = row.Field(Of String)("iTR_AB_ID_1")
                    newdata.iTR_PARTY_1 = row.Field(Of String)("iTR_PARTY_1")
                    newdata.iTR_CR_ID = row.Field(Of String)("iTR_CR_ID")
                    newdata.iTR_CR_NAME = row.Field(Of String)("iTR_CR_NAME")
                    newdata.iTR_DATE_SERIAL = row.Field(Of Integer?)("iTR_DATE_SERIAL")
                    newdata.iTR_DATE_SHOW = row.Field(Of String)("iTR_DATE_SHOW")
                    newdata.iTR_ENTRY = row.Field(Of String)("iTR_ENTRY")
                    newdata.iTR_REC_CASH = row.Field(Of Decimal?)("iTR_REC_CASH")
                    newdata.iTR_REC_BANK = row.Field(Of Decimal?)("iTR_REC_BANK")
                    newdata.iTR_REC_JOURNAL = row.Field(Of Decimal?)("iTR_REC_JOURNAL")
                    newdata.iTR_REC_TOTAL = row.Field(Of Decimal?)("iTR_REC_TOTAL")
                    newdata.iTR_PAY_CASH = row.Field(Of Decimal?)("iTR_PAY_CASH")
                    newdata.iTR_PAY_BANK = row.Field(Of Decimal?)("iTR_PAY_BANK")
                    newdata.iTR_PAY_JOURNAL = row.Field(Of Decimal?)("iTR_PAY_JOURNAL")
                    newdata.iTR_PAY_TOTAL = row.Field(Of Decimal?)("iTR_PAY_TOTAL")
                    newdata.iTR_NARRATION = row.Field(Of String)("iTR_NARRATION")
                    newdata.iTR_ROW_POS = row.Field(Of String)("iTR_ROW_POS")
                    newdata.iTR_TYPE = row.Field(Of String)("iTR_TYPE")
                    newdata.iTR_CODE = row.Field(Of Integer?)("iTR_CODE")
                    newdata.iREC_ID = row.Field(Of String)("iREC_ID")
                    newdata.iTR_M_ID = row.Field(Of String)("iTR_M_ID")
                    newdata.iTR_SR_NO = row.Field(Of Integer?)("iTR_SR_NO")
                    newdata.iTR_SORT = row.Field(Of String)("iTR_SORT")
                    newdata.iREC_ADD_ON = row.Field(Of DateTime?)("iREC_ADD_ON")
                    newdata.iTR_TEMP_ID = row.Field(Of String)("iTR_TEMP_ID")
                    newdata.iTR_REF_NO = row.Field(Of Integer?)("iTR_REF_NO")
                    newdata.iACTION_STATUS = row.Field(Of String)("iACTION_STATUS")
                    newdata.iREC_EDIT_ON = row.Field(Of DateTime?)("iREC_EDIT_ON")
                    newdata.iREC_STATUS_ON = row.Field(Of DateTime?)("iREC_STATUS_ON")
                    newdata.iREC_ADD_BY = row.Field(Of String)("iREC_ADD_BY")
                    newdata.iREC_EDIT_BY = row.Field(Of String)("iREC_EDIT_BY")
                    newdata.iREC_STATUS_BY = row.Field(Of String)("iREC_STATUS_BY")
                    newdata.iCross_Ref_ID = row.Field(Of String)("iCross_Ref_ID")
                    newdata.iRef_no = row.Field(Of String)("iRef_no")
                    newdata.iPurpose = row.Field(Of String)("iPurpose")
                    newdata.Grid_PK = If(String.IsNullOrEmpty(row.Field(Of String)("iTR_M_ID")), "Null", row.Field(Of String)("iTR_M_ID")) + If(String.IsNullOrEmpty(row.Field(Of String)("iREC_ID")), "Null", row.Field(Of String)("iREC_ID"))
                    newdata.UID = row.Field(Of String)("UID")
                    'newdata.VOUCHING_STATUS = row.Field(Of String)("VOUCHING_STATUS")
                    ' newdata.Vouching_Remarks = row.Field(Of String)("Vouching_Remarks")
                    newdata.Review_Count = row.Field(Of Int32)("Review_Count")
                    newdata.Reviewed_By = row.Field(Of String)("Reviewed_By")
                    newdata.CEN_ID = row.Field(Of Int32)("CEN_ID")
                    newdata.VOUCHING_CATEGORY = row.Field(Of String)("VOUCHING_CATEGORY")
                    newdata.Sr = Sr
                    Sr = Sr + 1

                    purposelist.Add(newdata)

                Next
            End If
            Return purposelist
        End Function

        Public Function GetAllPendingAttachments() As DataSet
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@SCREEN", "@CEN_ID", "@YEAR_ID", "@PREV_YEARID", "@TYPE", "@USER_ID"}
            Dim values() As Object = {"ALL", cBase._open_Cen_ID, cBase._open_Year_ID, cBase._prev_Unaudited_YearID, "PENDING", cBase._open_User_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.Int32, DbType.Int32, DbType.Int32, DbType.String, DbType.String}
            Dim lengths() As Integer = {255, 8, 8, 8, 255, 255}
            Return _RealService.ListDatasetFromSP(Tables.ATTACHMENT_INFO, "[sp_get_Document_stats]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function

        Public Function RegisterForAudit(Optional ConfirmationTaken As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@YEAR_ID", "@CONFIRMATION_TAKEN"}
            Dim values() As Object = {cBase._open_Cen_ID, cBase._open_Year_ID, ConfirmationTaken}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.Boolean}
            Dim lengths() As Integer = {8, 8, 2}
            Return _RealService.ListFromSP(Tables.SO_CENTER_AUDIT_STATS, "[sp_update_Register_For_Audit]", Tables.SO_CENTER_AUDIT_STATS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function

        Public Function CancelAuditRegistration(Optional ConfirmationTaken As Boolean = False) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@YEAR_ID", "@CONFIRMATION_TAKEN"}
            Dim values() As Object = {cBase._open_Cen_ID, cBase._open_Year_ID, ConfirmationTaken}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.Boolean}
            Dim lengths() As Integer = {8, 8, 2}
            Return _RealService.ListFromSP(Tables.SO_CENTER_AUDIT_STATS, "[sp_update_Audit_Registration_Cancellation]", Tables.SO_CENTER_AUDIT_STATS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function

        Public Function AuditStatusPage_Public(CertificateNo As Integer) As List(Of Return_AuditStatusPage_Public)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim OnlineQuery As String = "SELECT EVE.HE_NAME AS Event, EVE.HE_COD_YEAR_ID As 'Fin.Year',ci.Cen_uid UID, COALESCE(MI.MISC_NAME,'NOT AVAILABLE') AS Status,
                                         CAS_AUDITOR_ID AS Auditor, CE.EVENT_REG_NO AS 'Reg.No.'
                                         From so_center_audit_stats CAS
                                         INNER JOIN centre_info AS CI ON CAS_CEN_ID = CEN_ID
                                         INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO = MAIN.CEN_BK_PAD_NO And MAIN.CEN_MAIN = 1
                                         INNER JOIN so_ho_event_info AS EVE ON CAS_EVENT_ID = EVE.HE_EVENT_ID
                                         LEFT JOIN Centre_Events AS CE ON CI.CEN_ID = CE.CEN_ID And EVE.HE_EVENT_ID = CE.EVENTID
                                         LEFT OUTER JOIN misc_info AS MI ON CAS_AUDIT_STATUS_ID = MI.REC_ID
                                         WHERE CAS_STATUS = 1 And MAIN.CEN_BK_PAD_NO = " & CertificateNo &
                                         " ORDER BY EVE.HE_FROM DESC"
            Dim RetTable As DataTable = _RealService.List(Tables.SO_CENTER_AUDIT_STATS, OnlineQuery, Tables.SO_CENTER_AUDIT_STATS.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Dim _AuditStatusList As List(Of Return_AuditStatusPage_Public) = New List(Of Return_AuditStatusPage_Public)()
            For Each row As DataRow In RetTable.Rows
                Dim newdata = New Return_AuditStatusPage_Public()
                newdata.EventName = row.Field(Of String)("Event")
                newdata.Fin_Year = row.Field(Of Integer)("Fin.Year")
                newdata.UID = row.Field(Of String)("UID")
                newdata.Status = row.Field(Of String)("Status")
                newdata.Auditor = row.Field(Of String)("Auditor")
                newdata.Reg_No_ = row.Field(Of Integer?)("Reg.No.")
                _AuditStatusList.Add(newdata)
            Next
            Return _AuditStatusList
        End Function

        Public Function GetPANDatabase(PAN As String, DeductionDate As DateTime) As DataSet

            Dim DeductionDate_Str As String = DeductionDate.ToString("yyyy-MM-dd HH:mm:ss")
            Dim IndRateChart_Query As String = "Select * from IndRateChart$ where '" + DeductionDate_Str + "' between ISNULL(from_date, '" + DeductionDate_Str + "') AND ISNULL(to_date, '" + DeductionDate_Str + "')"
            Dim OthRateChart_Query As String = "Select * from OthRateChart$ where '" + DeductionDate_Str + "' between ISNULL(from_date, '" + DeductionDate_Str + "') AND ISNULL(to_date, '" + DeductionDate_Str + "')"

            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim DataBase_xls As DataTable = _RealService.List(Tables.DataBase_xls, "Select * from pan_tds_data where PAN ='" + PAN + "' AND  YEARID ='" + cBase._open_Year_ID.ToString() + "'", Tables.DataBase_xls.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Dim IndRateChart_xls As DataTable = _RealService.List(Tables.IndRateChart_xls, IndRateChart_Query, Tables.IndRateChart_xls.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Dim OthRateChart_xls As DataTable = _RealService.List(Tables.OthRateChart_xls, OthRateChart_Query, Tables.OthRateChart_xls.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Dim LastDate_xls As DataTable = _RealService.List(Tables.LastDate_xls, "Select * from LastDate$", Tables.LastDate_xls.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Dim PersonStatus_xls As DataTable = _RealService.List(Tables.PersonStatus_xls, "Select * from PersonStatus$", Tables.PersonStatus_xls.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Dim TermsNConditions_xls As DataTable = _RealService.List(Tables.TermsNConditions_xls, "Select * from TermsNConditions$ where YEARID ='" + cBase._open_Year_ID.ToString() + "'", Tables.TermsNConditions_xls.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))

            Dim dSet As DataSet = New DataSet()
            dSet.Tables.Add(DataBase_xls)
            dSet.Tables.Add(IndRateChart_xls)
            dSet.Tables.Add(OthRateChart_xls)
            dSet.Tables.Add(LastDate_xls)
            dSet.Tables.Add(PersonStatus_xls)
            dSet.Tables.Add(TermsNConditions_xls)

            Return dSet
        End Function

    End Class
End Class