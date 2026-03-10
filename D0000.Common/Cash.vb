'SQL, Shifted
Imports Common_Lib.RealTimeService
Partial Public Class DbOperations
    <Serializable>
    Public Class Cash
        Inherits SharedVariables

#Region "Report Classes"
        <Serializable>
        Public Class Return_CashProfile
            Inherits CommonReturnFields
            ''' <summary>
            ''' Original Column name is Date
            ''' </summary>
            ''' <returns></returns>
            Public Property BalDate As DateTime
            Public Property OP_AMOUNT As Decimal
            Public Property RemarkCount As Integer
            Public Property RemarkStatus As String
            Public Property OpenActions As Integer
            Public Property CrossedTimeLimit As Integer
            Public Property Remarks As Boolean
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
            Public Property iIcon As String
            Public Property Special_Ref As String
        End Class
#End Region
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetList() As List(Of Return_CashProfile)
            Dim LocalQuery As String = " SELECT '" & Format(DateAdd(DateInterval.Day, -1, cBase._open_Year_Sdt), cBase._Date_Format_Current) & "' as Date,OP_AMOUNT,REC_ID AS ID  ," & cBase.Remarks_Detail("Opening_Balances_Info", True, GetCurrentDateTime(ClientScreen.Profile_Cash)) & "," & cBase.Rec_Detail("Opening_Balances_Info", Common.DbConnectionMode.Local) & "" &
                                 " FROM Opening_Balances_Info " &
                                 " Where   REC_STATUS IN (0,1,2) AND OP_CEN_ID='" & cBase._open_Cen_ID & "'  AND  REC_ID LIKE '%CASH%' ; "
            Dim inParam As Param_GetOpeningBalance_Common = New Param_GetOpeningBalance_Common()
            inParam.Date_Format_Current = cBase._Date_Format_Current
            inParam.open_Year_Sdt = cBase._open_Year_Sdt
            Dim CashTable As DataTable = GetOpeningBalance(ClientScreen.Profile_Cash, inParam)

            Dim Remarks As Boolean
            CashTable.Columns.Add("Remarks", Remarks.GetType)
            For Each XRow In CashTable.Rows : XRow("Remarks") = IIf(XRow("RemarkCount") > 0, True, False) : Next

            Dim _CashBalances As List(Of Return_CashProfile) = New List(Of Return_CashProfile)
            If (Not (CashTable) Is Nothing) Then
                For Each row As DataRow In CashTable.Rows
                    Dim newdata = New Return_CashProfile
                    newdata.BalDate = row.Field(Of DateTime)("Date") 'redmine bug 132955 fixed
                    newdata.OP_AMOUNT = row.Field(Of Decimal)("OP_AMOUNT")
                    newdata.RemarkCount = row.Field(Of Integer)("RemarkCount")
                    newdata.RemarkStatus = row.Field(Of String)("RemarkStatus")
                    newdata.OpenActions = row.Field(Of Integer)("OpenActions")
                    newdata.CrossedTimeLimit = row.Field(Of Integer)("CrossedTimeLimit")
                    newdata.Remarks = row.Field(Of Boolean)("Remarks")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Add_Date = row.Field(Of DateTime)("Add Date")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit Date")
                    newdata.Action_Date = row.Field(Of DateTime)("Action Date")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.REQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    newdata.COMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    newdata.RESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    newdata.REJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
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
                    _CashBalances.Add(newdata)
                Next
            End If
            Return _CashBalances
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Cash, RealTimeService.Tables.OPENING_BALANCES_INFO, Common.ClientDBFolderCode.DATA, "BA_CEN_ID")
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Cash, RealTimeService.Tables.OPENING_BALANCES_INFO, "OP_CEN_ID")
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="Cash_Acc_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Cash_AddDefault</remarks>
        Public Function AddDefault(ByVal Cash_Acc_ID As String) As Boolean
            Dim Param As Parameter_AddDefault_Cash = New Parameter_AddDefault_Cash()
            Param.Cash_Acc_ID = Cash_Acc_ID
            Param.openYearID = cBase._open_Year_ID
            Return InsertRecord(RealTimeService.RealServiceFunctions.Cash_AddDefault, ClientScreen.Profile_Cash, Param)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Cash_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Cash) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Cash)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Cash_Update, ClientScreen.Profile_Cash, UpParam)
        End Function

        ''' <summary>
        ''' Get Cash Opening Balance, Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Cash_GetCashOpeningBalance</remarks>
        Public Function GetCashOpeningBalance(ByVal RecID) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Cash_GetCashOpeningBalance, ClientScreen.Profile_Cash, RecID) 'step:2
        End Function

        Public Function CheckCashOpeningBalanceRowCount(ByVal RecID) As Object
            Return GetOpeningBalanceRowCount(ClientScreen.Profile_Cash, RecID)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.OPENING_BALANCES_INFO, ClientScreen.Profile_Cash)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.OPENING_BALANCES_INFO, ClientScreen.Profile_Cash)
            Return Locked
        End Function


    End Class
End Class
