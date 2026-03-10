Imports System.Data

Namespace Real
    <Serializable>
    Public Class Action_Items

#Region "Get/Input/Update Parameter Classes"
        Public Class Parameter_Insert_Action_Items
            Public Status As String
            Public Remarks As String
            Public Type As String
            Public Title As String
            Public DueType As String
            Public DueOn As String
            Public ContactName As String
            Public ContactNo As String
            Public Ref_Table As String
            Public Ref_Screen As String
            Public Ref_Rec_ID As String
            Public RecID As String
        End Class
        Public Class Parameter_Update_Action_Items
            Public Status As String
            Public Remarks As String
            Public Type As String
            Public Title As String
            Public DueType As String
            Public DueOn As String
            Public ContactName As String
            Public ContactNo As String
            Public Ref_Table As String
            Public Ref_Screen As String
            Public Ref_Rec_ID As String
            Public CentreRemarks As String
            Public RecID As String
        End Class
        Public Class Parameter_Close_Action_Items
            Public closeRemarks As String
            Public RecId As String
        End Class
        Public Class Param_Action_Items_GetList
            Public RefRecId As String
            Public RefTable As String
        End Class
        Public Class Param_GetOpenActions_Common
            Public RefID As String
            Public Tablename As String
        End Class
        Public Class Param_GetRemarksStatus
            Public RecID As String
            Public TableName As String
        End Class
        Public Class Param_Txn_Insert_ActionItems
            Public param_InsertActionItems As Parameter_Insert_Action_Items
            Public param_CloseActionItems As Parameter_Close_Action_Items = Nothing
        End Class
        Public Class Param_Txn_Update_ActionItems
            Public param_UpdateActionItems As Parameter_Update_Action_Items
            Public param_CloseActionItems As Parameter_Close_Action_Items = Nothing
        End Class
#End Region

        ''' <summary>
        ''' Get Over dues Count
        ''' </summary>
        ''' <remarks>ActionItems_GetOverDueCount</remarks>
        Public Shared Function GetOverDueCount(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CurrentDateTime As DateTime = DataFunctions.GetCurrentDateTime(inBasicParam)
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM action_item_info WHERE  REC_STATUS IN (0,1,2) AND UPPER(AI_STATUS) <> 'CLOSED' AND  AI_DUE_ON < '" & CurrentDateTime.ToString(Common.Server_Date_Format_Short) & "' AND AI_CEN_ID = " & inBasicParam.openCenID.ToString
            Return dbService.GetScalar(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, ConnectOneWS.Tables.ACTION_ITEM_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetPendingCentreRemarkCount(inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim CurrentDateTime As DateTime = DataFunctions.GetCurrentDateTime(inBasicParam)
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM action_item_info WHERE  REC_STATUS IN (0,1,2) AND UPPER(AI_STATUS) in('OPEN','REOPENED') AND  LEN(COALESCE(AI_CEN_REMARKS,''))<=0  AND AI_CEN_ID = " & inBasicParam.openCenID.ToString
            Return dbService.GetScalar(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, ConnectOneWS.Tables.ACTION_ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>ActionItems_GetList</remarks>
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Param_Action_Items_GetList = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim CurrentDateTime As DateTime = DataFunctions.GetCurrentDateTime(inBasicParam)
            'Dim OnlineQuery As String = ""
            'OnlineQuery = "SELECT AI_TYPE as Type , AI_STATUS as Status, A.REC_ADD_ON AS Date, A.REC_ADD_BY AS Auditor, AI_TITLE AS Title, AI_REMARKS AS Description, CASE WHEN CAST(AI_DUE_TYPE as VARCHAR)= 'SPECIFIC DATE' THEN CAST(AI_DUE_ON as VARCHAR) ELSE CAST(AI_DUE_TYPE as VARCHAR) END AS 'Due On',AI_CEN_REMARKS as 'Centre Remarks', AI_CLOSURE_REMARKS as 'Close Remarks',AI_CLOSURE_ON as 'Closed On',AI_CLOSURE_BY as 'Closed By', REC_ID AS ID,CASE WHEN  AI_DUE_ON < '" & CurrentDateTime.ToString(Common.Server_Date_Format_Short) & "' AND UPPER(AI_STATUS) <> 'CLOSED' THEN 1 ELSE 0 END AS CrossedTimeLimit, COALESCE(ATTACHMENT.ATTACHMENT_COUNT,0) AS Attachments, " & Common.Rec_Detail("A") &
            '                      " FROM ACTION_ITEM_INFO A OUTER APPLY ( SELECT * FROM fn_get_Attachment_Count(AI_CEN_ID,NULL,'Audit_Action') AS ABC WHERE A.REC_ID = COALESCE(ABC.REF_ID,A.REC_ID) ) AS ATTACHMENT WHERE REC_STATUS IN (0,1,2) AND AI_CEN_ID = " & inBasicParam.openCenID.ToString

            'If Param.RefRecId.Length > 0 Then
            '    OnlineQuery += " AND AI_REF_REC_ID='" & Param.RefRecId & "' AND AI_REF_TABLE='" & Param.RefTable & "'"
            'End If

            'OnlineQuery += " ORDER BY REC_ADD_ON DESC"
            'Return dbService.List(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, ConnectOneWS.Tables.ACTION_ITEM_INFO.ToString(), inBasicParam)

            Dim paramters As String() = {"@CENID", "@RefRecId", "@RefTable"}
            Dim values() As Object = {inBasicParam.openCenID, Param.RefRecId, Param.RefTable}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 36, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.BANK_ACCOUNT_INFO, "[sp_get_Audit_Action_Profile]", ConnectOneWS.Tables.BANK_ACCOUNT_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)

        End Function

        Public Shared Function GetOpenActions_Common(ByVal Param As Param_GetOpenActions_Common, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = "SELECT COUNT(REC_ID) FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & Param.Tablename & "' AND AI_REF_REC_ID= '" & Param.RefID & "' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED')"
            Return dbService.GetScalar(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, ConnectOneWS.Tables.ACTION_ITEM_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetRemarksStatus(Param As Param_GetRemarksStatus, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            OnlineQuery = "(SELECT TOP 1 COALESCE(UPPER(AI_STATUS),'') as 'Remark Status' FROM ACTION_ITEM_INFO WHERE UPPER(AI_REF_TABLE)='" & Param.TableName & "' AND AI_REF_REC_ID= '" & Param.RecID & "' AND UPPER(AI_DUE_TYPE) = 'CURRENT AUDIT' AND (UPPER(AI_STATUS)='OPEN' OR UPPER(AI_STATUS)='REOPENED'))"
            Return dbService.GetScalar(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, ConnectOneWS.Tables.ACTION_ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Inserts Action Items 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>ActionItems_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Action_Items, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO ACTION_ITEM_INFO(AI_CEN_ID,AI_TYPE,AI_STATUS,AI_TITLE,AI_REMARKS,AI_DUE_TYPE,AI_DUE_ON,AI_CONTACT_NAME,AI_CONTACT_NUMBER,AI_REF_TABLE,AI_REF_SCREEN,AI_REF_REC_ID, " &
                                            "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "'" & InParam.Type & "', " &
                                                  "'" & InParam.Status & "', " &
                                                  "N'" & InParam.Title & "', " &
                                                  "N'" & InParam.Remarks & "', " &
                                                  "'" & InParam.DueType & "', " &
                                                  " " & If(IsDate(InParam.DueOn), "'" & Convert.ToDateTime(InParam.DueOn).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                  "'" & InParam.ContactName & "', " &
                                                  "'" & InParam.ContactNo & "', " &
                                                  "'" & InParam.Ref_Table & "', " &
                                                  "'" & InParam.Ref_Screen & "', " &
                                                  "'" & InParam.Ref_Rec_ID & "', " &
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Locked & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, inBasicParam, InParam.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Updates action Items 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>ActionItems_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Action_Items, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE ACTION_ITEM_INFO SET " &
                                         "AI_STATUS             ='" & UpParam.Status & "', " &
                                         "AI_REMARKS             =N'" & UpParam.Remarks & "', " &
                                         "AI_TYPE             ='" & UpParam.Type & "', " &
                                         "AI_DUE_TYPE             ='" & UpParam.DueType & "', " &
                                         "AI_TITLE             =N'" & UpParam.Title & "', " &
                                         "AI_DUE_ON             =" & If(IsDate(UpParam.DueOn), "'" & Convert.ToDateTime(UpParam.DueOn).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                         "AI_CONTACT_NAME             ='" & UpParam.ContactName & "', " &
                                         "AI_CONTACT_NUMBER             ='" & UpParam.ContactNo & "', " &
                                         "AI_REF_TABLE             ='" & UpParam.Ref_Table & "', " &
                                         "AI_REF_SCREEN             ='" & UpParam.Ref_Screen & "', " &
                                         "AI_REF_REC_ID             ='" & UpParam.Ref_Rec_ID & "', " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE REC_ID    ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function UpdateCentreRemarks(ByVal UpParam As Parameter_Update_Action_Items, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE ACTION_ITEM_INFO SET " &
                                        " AI_CEN_REMARKS    =N'" & UpParam.CentreRemarks & "', " &
                                        " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        " REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        " WHERE REC_ID      ='" & UpParam.RecID & "'"
            dbService.Update(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Marks action item as closed 
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <param name="Cls"></param>
        ''' <returns></returns>
        ''' <remarks>ActionItems_Close</remarks>
        Public Overloads Shared Function Close(ByVal Cls As Parameter_Close_Action_Items, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE ACTION_ITEM_INFO SET " &
                                         "AI_STATUS             ='Closed', " &
                                         "AI_CLOSURE_REMARKS             =N'" & Cls.closeRemarks & "', " &
                                         "AI_CLOSURE_BY             ='" & inBasicParam.openUserID & "', " &
                                         "AI_CLOSURE_ON             ='" & Common.DateTimePlaceHolder & "', " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " &
                                         "REC_STATUS        =" & Common_Lib.Common.Record_Status._Locked & "," &
                                         "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " &
                                         "  WHERE REC_ID    ='" & Cls.RecId & "'"
            dbService.Update(ConnectOneWS.Tables.ACTION_ITEM_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function InsertActionItems_Txn(inParam As Param_Txn_Insert_ActionItems, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertActionItems Is Nothing Then
                If Not Insert(inParam.param_InsertActionItems, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.param_CloseActionItems Is Nothing Then
                If Not Close(inParam.param_CloseActionItems, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            'txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function UpdateActionItems_Txn(upParam As Param_Txn_Update_ActionItems, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.param_UpdateActionItems Is Nothing Then
                If Not Update(upParam.param_UpdateActionItems, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_CloseActionItems Is Nothing Then
                If Not Close(upParam.param_CloseActionItems, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            '   txn.Complete()
            '   End Using
            Return True
        End Function


    End Class
End Namespace
