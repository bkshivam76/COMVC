Imports Microsoft.VisualBasic
Imports System.Data

Namespace Real
    <Serializable>
    Public Class Audit
        <Serializable>
        Public Class Param_GetDocumentMapping
            Public REC_ID As String
            Public TR_M_ID As String
            Public SHOW_NEW_OTHERS As Boolean = True
            Public SCREEN As ConnectOneWS.ClientScreen
            Public CenID As Int32 = 0
            Public ShowAdditionalInfo As Boolean = False
        End Class
        <Serializable>
        Public Class Param_GetAdditionalInfo
            Public REC_ID As String
            Public TR_M_ID As String
            Public SCREEN As ConnectOneWS.ClientScreen
            Public CenID As Int32 = 0
        End Class
        <Serializable>
        Public Class Param_InsertDocumentMapResponse
            Public MAP_ID As Int32
            Public REASON As String
            Public RefRec_ID As String
        End Class
        <Serializable>
        Public Class Param_InsertVouchingStatus
            Public EntryID As String
            Public AttachmentID As String
            Public Remarks As String
            Public VouchingCategory As String
            Public Status As String
            Public EntryCenID As Int32 = 0
            Public TxnDocRespID As Int32? = Nothing
        End Class
        <Serializable>
        Public Enum Audited_Status_Options
            All
            Audited
            Unaudited
        End Enum
        <Serializable>
        Public Enum Data_Scope_Options
            All
            Exclusive
        End Enum
        ''' <summary>
        ''' Serialized
        ''' </summary>
        <Serializable>
        Public Class Param_GetDocumentChecking
            Public Status As String
            Public Document_Category As String
            Public Document_ID As String
            Public Audited_Status As Audited_Status_Options
            Public Data_Scope As Data_Scope_Options
            Public CheckedBy As String = ""
        End Class
        Public Shared Function GetDocumentMapping(ByVal inParam As Param_GetDocumentMapping, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"@REC_ID", "@TR_M_ID", "@SCREEN", "@CEN_ID", "@SHOW_NEW_OTHERS", "@YEAR_ID", "@USER_ID", "@SHOW_ADDITIONAL_INFO"}
            Dim values() As Object = {inParam.REC_ID, inParam.TR_M_ID, inParam.SCREEN, IIf(inParam.CenID = 0, inBasicParam.openCenID, inParam.CenID), inParam.SHOW_NEW_OTHERS, inBasicParam.openYearID, inBasicParam.openUserID, inParam.ShowAdditionalInfo}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.Boolean, DbType.Int32, DbType.String, DbType.Boolean}
            Dim lengths() As Integer = {36, 36, 255, 4, 1, 4, 255, 1}
            Dim Data As DataSet = dbService.ListDatasetFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "[sp_get_Doc_Mapping]", paramters, values, dbTypes, lengths, inBasicParam)
            Return Data
        End Function
        Public Shared Function GetAdditionalInfo(ByVal inParam As Param_GetAdditionalInfo, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"@REC_ID", "@TR_M_ID", "@SCREEN", "@CEN_ID", "@YEAR_ID", "@USER_ID"}
            Dim values() As Object = {inParam.REC_ID, inParam.TR_M_ID, inParam.SCREEN, IIf(inParam.CenID = 0, inBasicParam.openCenID, inParam.CenID), inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.Int32, DbType.String}
            Dim lengths() As Integer = {36, 36, 255, 4, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, "[sp_get_Doc_Additional_Info]", ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetDocumentChecking(ByVal inParam As Param_GetDocumentChecking, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"@User_ID", "@Status", "@Document_ID", "@Document_Category", "@Audited_Status", "@Data_Scope", "@Checked_By", "@YEARID", "@CENID"}
            Dim values() As Object = {inBasicParam.openUserID, IIf(inParam.Status <> Nothing, inParam.Status, DBNull.Value), IIf(inParam.Document_ID <> Nothing, inParam.Document_ID, DBNull.Value), IIf(inParam.Document_Category <> Nothing, inParam.Document_Category, DBNull.Value), inParam.Audited_Status.ToString(), inParam.Data_Scope.ToString(), inParam.CheckedBy, inBasicParam.openYearID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {36, 36, 36, 255, 50, 50, 255, 4, 4}
            Dim Data As DataTable = dbService.ListFromSP(ConnectOneWS.Tables.ATTACHMENT_INFO, "[sp_get_Document_Checking]", ConnectOneWS.Tables.ATTACHMENT_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
            Return Data
        End Function


        Public Shared Function DeleteAllVouchingAgainstEntry(ByVal RefEntryID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            'Remove vouching posted against referred entry 
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & RefEntryID & "' ", inBasicParam)
        End Function
        Public Shared Function DeleteAllVouchingAgainstReference(ByVal RefEntryID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            'Remove vouching posted against referred entry 
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[sp_Delete_Vouching_References]"
            Dim params() As String = {"@EntryID"}
            Dim values() As Object = {RefEntryID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            dbService.DeleteFromSP(ConnectOneWS.Tables.VOUCHING_AUDIT, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertVouchingStatus(ByVal InParam As Param_InsertVouchingStatus, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            If inBasicParam.openYearID = 0 Then
                Throw New Exception("Session Logged Out!!")
            End If
            Dim CenID As Int32 = InParam.EntryCenID
            If CenID = 0 Then
                CenID = inBasicParam.openCenID
            End If
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_Vouching_Status"
            Dim params() As String = {"@AuditorID", "@EntryID", "@Remarks", "@VouchingCategory", "@Status", "@CenID", "@YEARID", "@AttachmentID", "@TxnDocRespID"}
            Dim values() As Object = {inBasicParam.openUserID, InParam.EntryID, InParam.Remarks, InParam.VouchingCategory, InParam.Status, CenID, inBasicParam.openYearID, IIf([String].IsNullOrEmpty(InParam.AttachmentID), Nothing, InParam.AttachmentID), InParam.TxnDocRespID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 36, 4000, 255, 20, 4, 4, 36, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.VOUCHING_AUDIT, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertDocumentMapResponse(ByVal InParam1 As Param_InsertDocumentMapResponse, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO [transaction_doc_mapping_Response] ([TR_DOC_RESP_MAP_ID],[TR_DOC_RESP_REASON],TR_DOC_RESP_REF_REC_ID,[TR_DOC_RESP_BY_ID],[TR_DOC_RESP_CEN_ID]) VALUES(" &
                                              "" & InParam1.MAP_ID.ToString() & ", " &
                                              "N'" & InParam1.REASON & "', " &
                                              "'" & InParam1.RefRec_ID & "', " &
                                              "'" & inBasicParam.openUserID & "'," &
                                              "" & inBasicParam.openCenID.ToString() & "" &
                                              ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE, OnlineQuery, inBasicParam, InParam1.RefRec_ID, Nothing, DateTime.Now)

            'Remove vouching posted against referred entry 
            'dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & InParam1.RefRec_ID & "' ", inBasicParam)

            Return True
        End Function
        Public Shared Function UpdtaeDocumentMapResponse(ByVal InParam1 As Param_InsertDocumentMapResponse, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "UPDATE [transaction_doc_mapping_Response] " &
                                        "SET [TR_DOC_RESP_REASON] = N'" & InParam1.REASON.ToString() & "'" &
                                        ",[TR_DOC_RESP_BY_ID] = '" & inBasicParam.openUserID & "'" &
                                        " WHERE [TR_DOC_RESP_MAP_ID] = " & InParam1.MAP_ID.ToString() &
                                        " AND [TR_DOC_RESP_REF_REC_ID] = '" & InParam1.RefRec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE, OnlineQuery, inBasicParam)

            OnlineQuery = "select TR_DOC_RESP_ID from transaction_doc_mapping_Response WHERE [TR_DOC_RESP_MAP_ID] = " & InParam1.MAP_ID.ToString() + " AND [TR_DOC_RESP_REF_REC_ID] = '" & InParam1.RefRec_ID & "'"
            Dim RespID As String = dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING_RESPONSE.ToString(), inBasicParam)

            'Remove vouching posted against referred entry 
            'dbService.DeleteByCondition(ConnectOneWS.Tables.VOUCHING_AUDIT, "VA_ENTRY_ID = '" & InParam1.RefRec_ID & "' ", inBasicParam)
            Audit.UnVouchEntryByReference(InParam1.RefRec_ID, inBasicParam, Nothing, RespID)
            Return True
        End Function
        Public Shared Function UnVouchEntryByReference(ByVal RefEntryID As String, inBasicParam As ConnectOneWS.Basic_Param, Optional AttachmentID As String = Nothing, Optional AttachmentResponseID As Int32 = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"@EntryID", "@UserID", "@Attachment_ID", "@ResponseID"}
            Dim values() As Object = {RefEntryID, inBasicParam.openUserID, AttachmentID, AttachmentResponseID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.Int32}
            Dim lengths() As Integer = {36, 255, 36, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.VOUCHING_AUDIT, "[sp_Insert_Entry_Unvouched]", paramters, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
    End Class
End Namespace