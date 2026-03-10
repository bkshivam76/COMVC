Imports System.Data

Namespace Real
#Region "Facility"
    <Serializable>
    Public Class ServiceMaterial
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_ServiceMaterial
            Public Title As String
            Public ProjectID As String
            Public Material_Type As String
            Public PublishDate As String
            Public OnlineLink As String
            Public Brief_Summary As String
            Public Speaker_Author_Publisher As String
            Public CenID As String
            Public Rec_ID As String
            Public PreviewImagePath As String
            Public MaterialSubCategory As String
            Public Is_Private As Boolean = False
        End Class
        <Serializable>
        Public Class Parameter_InsertWings_ServiceMaterial
            Public Sr_ID As String
            Public WingID As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_ServiceMaterial
            Public Title As String
            Public ProjectID As String
            Public Material_Type As String
            Public PublishDate As String
            Public OnlineLink As String
            Public Brief_Summary As String
            Public Speaker_Author_Publisher As String
            Public CenID As String
            Public Rec_ID As String
            Public PreviewImagePath As String
            Public MaterialSubCategory As String
            Public Is_Private As Boolean = False
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_ServiceMaterial
            Public param_Insert_ServiceMaterial As Parameter_Insert_ServiceMaterial
            Public InsertWings() As Parameter_InsertWings_ServiceMaterial = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Update_ServiceMaterial
            Public param_Update_ServiceMaterial As Parameter_Update_ServiceMaterial
            Public RecID_DeleteWing As String = Nothing
            Public InsertWings() As Parameter_InsertWings_ServiceMaterial = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_ServiceMaterial
            Public RecID_DeleteWing As String = Nothing
            Public RecID_Delete As String = Nothing
        End Class
#End Region




        ''' <summary>
        ''' Get List
        ''' </summary>
        '''' <param name="DateFormatCurrent"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_GetList</remarks>
        'Public Shared Function GetList(ByVal DateFormatCurrent As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim Current_Format As String = "'"
        '    For Each DatePart As String In DateFormatCurrent.Split("/")
        '        If DatePart.ToLower().Contains("y") Then
        '            If (DatePart.Length > 2) Then
        '                Current_Format = Current_Format & "%Y/"
        '            Else
        '                Current_Format = Current_Format & "%y/"
        '            End If
        '        Else
        '            Current_Format = Current_Format & "%" & DatePart.Substring(0, 1) & "/"
        '        End If

        '    Next
        '    Current_Format = Current_Format.Substring(0, Current_Format.Length - 1) & "'"

        '    'Dim OnlineQuery As String = "SELECT SR_PROG_NAME ,SR_PROG_VENUE, " & _
        '    '                        "dbo.fn_FORMATDATE(SR_PROG_FR_DATE , 'dd-MON-yyyy')  + '  to  ' +  dbo.fn_FORMATDATE(SR_PROG_TO_DATE , 'dd-MON-yyyy') AS  SR_DATE," & _
        '    '                        "DATEDIFF (dd, SR_PROG_FR_DATE , SR_PROG_TO_DATE )+1 AS SR_PERIOD," & _
        '    '                        "SR_PROG_FR_TIME + '  to  ' + SR_PROG_TO_TIME AS SR_TIME," & _
        '    '                        "SR_PROG_PD_TIME AS SR_TIME_PER," & _
        '    '                        "SR_PROG_BRIEF AS SR_BRIEF," & _
        '    '                        "SR_PROG_SPEAKER AS SR_SPEAKER," & _
        '    '                        "SR_PROG_SPL AS SR_SPL," & _
        '    '                        "SR_PROG_BENEFIT AS SR_BENEFIT," & _
        '    '                        "SR_PROG_FOLLOWUP AS SR_FOLLOW," & _
        '    '                        "SR_PROG_FEEDBACK AS SR_FEEDBACK," & _
        '    '                        "SR_PROJ_ID  AS 'Project ID'," & _
        '    '                        "REC_ID AS ID  ," & Common.Rec_Detail("SERVICE_REPORT_INFO") & "   " & _
        '    '                        "FROM SERVICE_REPORT_INFO " & _
        '    '                   " Where   REC_STATUS IN (0,1,2) AND SR_CEN_ID = " & inBasicParam.openCenID.ToString & " ; "
        '    'Return dbService.List(ConnectOneWS.Tables.SERVICE_REPORT_INFO, OnlineQuery, ConnectOneWS.Tables.SERVICE_REPORT_INFO.ToString(), inBasicParam)

        '    Dim SPName As String = "[sp_get_Service_Report_Listing]"
        '    Dim params() As String = {"@CENID", "@YEARID", "@UserID"}
        '    Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
        '    Dim lengths() As Integer = {5, 4, 255}
        '    Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        'End Function

        ''' <summary>
        ''' Get Wings Record
        ''' </summary>
        ''' <param name="SM_REC_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_GetWingsRecord</remarks>
        Public Shared Function GetWingsRecord(ByVal SM_REC_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim query As String = "Select SM_WING_ID,WI.WING_NAME  from SERVICE_MATERIAL_WING_INFO SRWI INNER JOIN WINGS_INFO WI ON SM_WING_ID=WI.WING_ID where  SRWI.REC_STATUS IN (0,1,2) AND SM_REC_ID= '" & SM_REC_ID & "'"
            Return dbService.List(ConnectOneWS.Tables.SERVICE_MATERIAL_WING_INFO, query, ConnectOneWS.Tables.SERVICE_MATERIAL_WING_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_ServiceMaterial, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO GODLY_SERVICE_MATERIAL_INFO(GSM_TITLE,GSM_PROJ_ID,GSM_Material_Type,GSM_DATE_OF_PUBLISH,GSM_ONLINE_LINK,GSM_BRIEF_SUMMARY,GSM_SPEAKER_AUTHOR_PUBLISHER,GSM_CEN_ID,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,GSM_PREVIEW_IMAGE_PATH,GSM_MATERIAL_SUB_CATEGORY,GSM_PRIVATE) VALUES(" &
                                                "N'" & InParam.Title & "'," &
                                                " " & IIf(InParam.ProjectID = Nothing, " NULL ", "'" & InParam.ProjectID & "'") & "," &
                                                "N'" & InParam.Material_Type & "'," &
                                                " " & If(IsDate(InParam.PublishDate), "'" & Convert.ToDateTime(InParam.PublishDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                "N'" & InParam.OnlineLink & "'," &
                                                "N'" & InParam.Brief_Summary & "', " &
                                                "N'" & InParam.Speaker_Author_Publisher & "', " &
                                                "" & InParam.CenID & ", " &
                                                "'" & Common.DateTimePlaceHolder & "', '" &
                                                inBasicParam.openUserID & "', '" &
                                                Common.DateTimePlaceHolder & "', '" &
                                                inBasicParam.openUserID & "', " &
                                                "'" & InParam.Rec_ID & "'," &
                                                "" & Common_Lib.Common.Record_Status._Completed &
                                                ", '" & Common.DateTimePlaceHolder & "', '" &
                                                    inBasicParam.openUserID & "'," &
                                               If(String.IsNullOrWhiteSpace(InParam.PreviewImagePath) = False, "'" & InParam.PreviewImagePath & "'", "NULL") & " ," &
                                               If(String.IsNullOrWhiteSpace(InParam.MaterialSubCategory) = False, "N'" & InParam.MaterialSubCategory & "'", "NULL") & " ," &
                                               If(InParam.Is_Private = False, 0, 1) & " )"

            dbService.Insert(ConnectOneWS.Tables.GODLY_SERVICE_MATERIAL_INFO, OnlineQuery, inBasicParam, InParam.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Wings
        ''' </summary>
        ''' <param name="InWings"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_InsertWings</remarks>
        Public Shared Function InsertWings(ByVal InWings As Parameter_InsertWings_ServiceMaterial, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO SERVICE_MATERIAL_WING_INFO(SM_CEN_ID,SM_REC_ID,SM_WING_ID," &
                                                 "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                 ") VALUES(" &
                                                    "" & inBasicParam.openCenID.ToString & "," &
                                                    "'" & InWings.Sr_ID & "'," &
                                                    "'" & InWings.WingID & "'," &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InWings.Rec_ID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.SERVICE_MATERIAL_WING_INFO, OnlineQuery, inBasicParam, InWings.Rec_ID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ServiceReport_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_ServiceMaterial, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " UPDATE GODLY_SERVICE_MATERIAL_INFO SET " &
                                        "GSM_TITLE        =N'" & UpParam.Title & "', " &
                                        "GSM_PROJ_ID       =" & IIf(UpParam.ProjectID = Nothing, " NULL ", "'" & UpParam.ProjectID & "'") & ", " &
                                        "GSM_Material_Type     =N'" & UpParam.Material_Type & "', " &
                                        "GSM_DATE_OF_PUBLISH     =" & If(IsDate(UpParam.PublishDate), "'" & Convert.ToDateTime(UpParam.PublishDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                        "GSM_ONLINE_LINK           =N'" & UpParam.OnlineLink & "', " &
                                        "GSM_BRIEF_SUMMARY           =N'" & UpParam.Brief_Summary & "',  " &
                                        "GSM_SPEAKER_AUTHOR_PUBLISHER =N'" & UpParam.Speaker_Author_Publisher & "',  " &
                                        "GSM_CEN_ID         =" & UpParam.CenID & ",  " &
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "'," &
                                         "REC_STATUS        =" & Convert.ToInt32(Common_Lib.Common.Record_Status._Completed) & "," &
                                         "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                         "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'," &
                                         "GSM_PREVIEW_IMAGE_PATH     ='" & UpParam.PreviewImagePath & "'," &
                                         "GSM_MATERIAL_SUB_CATEGORY  =" & If(String.IsNullOrWhiteSpace(UpParam.MaterialSubCategory) = False, "N'" & UpParam.MaterialSubCategory & "'", "NULL") & "," &
                                         "GSM_PRIVATE     =" & If(UpParam.Is_Private = False, 0, 1) &
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.GODLY_SERVICE_MATERIAL_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function InsertServiceMaterial_Txn(inParam As Param_Txn_Insert_ServiceMaterial, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '   Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_Insert_ServiceMaterial Is Nothing Then
                If Not Insert(inParam.param_Insert_ServiceMaterial, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_InsertWings_ServiceMaterial In inParam.InsertWings
                If Not Param Is Nothing Then InsertWings(Param, inBasicParam, RequestTime)
            Next
            '  End Using
            '  txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function UpdateServiceMaterial_Txn(UpParam As Param_Txn_Update_ServiceMaterial, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.GODLY_SERVICE_MATERIAL_INFO, UpParam.param_Update_ServiceMaterial.Rec_ID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not UpParam.param_Update_ServiceMaterial Is Nothing Then
                If Not Update(UpParam.param_Update_ServiceMaterial, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not UpParam.RecID_DeleteWing Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_MATERIAL_WING_INFO, "SM_REC_ID    ='" & UpParam.RecID_DeleteWing & "'", inBasicParam)
            End If
            For Each inWings_Update As Parameter_InsertWings_ServiceMaterial In UpParam.InsertWings
                If Not inWings_Update Is Nothing Then InsertWings(inWings_Update, inBasicParam, RequestTime)
            Next
            ' End Using
            '  txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function DeleteServiceMaterial_Txn(DelParam As Param_Txn_Delete_ServiceMaterial, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            'Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not DelParam.RecID_DeleteWing Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.SERVICE_MATERIAL_WING_INFO, "SM_REC_ID    ='" & DelParam.RecID_DeleteWing & "'", inBasicParam)
            End If
            If Not DelParam.RecID_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.GODLY_SERVICE_MATERIAL_INFO, DelParam.RecID_Delete, inBasicParam)
            End If
            '  End Using
            ' txn.Complete()
            '  End Using
            Return True
        End Function

    End Class
#End Region
End Namespace
