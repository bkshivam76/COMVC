'SQL, Shifted
Imports Common_Lib.RealTimeService

Partial Public Class DbOperations
#Region "Accounts"
    <Serializable>
    Public Class DonationRegister
        Inherits SharedVariables


#Region "Parameter Classes"
        <Serializable>
        Public Class Return_DonationRegister
            Inherits CommonReturnFields
            ''' <summary>
            ''' Actual Field name is Date
            ''' </summary>
            ''' <returns></returns>
            Public Property DonationDate As DateTime
            Public Property TR_AB_ID_1 As String
            ''' <summary>
            ''' Actual Field name is Donor Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Donor_Name As String
            Public Property Address As String
            Public Property City As String
            Public Property State As String
            Public Property District As String
            Public Property Country As String
            Public Property PinCode As String
            Public Property Purpose As String
            Public Property Amount As Decimal
            Public Property Mode As String
            ''' <summary>
            ''' Actual Field name is Cheque/DD/Ref No.
            ''' </summary>
            ''' <returns></returns>
            Public Property Cheque_DD_Ref_No As String
            ''' <summary>
            ''' Actual Field name is Cheque/DD/Ref Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Cheque_DD_Ref_Date As DateTime?
            ''' <summary>
            ''' Actual Field name is Cheque/DD/Ref Bank
            ''' </summary>
            ''' <returns></returns>
            Public Property Cheque_DD_Ref_Bank As String
            ''' <summary>
            ''' Actual Field name is Dep. Bank
            ''' </summary>
            ''' <returns></returns>
            Public Property DepBank As String
            ''' <summary>
            ''' Actual Field name is Form Received
            ''' </summary>
            ''' <returns></returns>
            Public Property Form_Received As String
            ''' <summary>
            ''' Actual Field name is Form Received Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Form_Received_Date As DateTime?
            Public Property DS_STATUS_MISC_ID As String
            Public Property Status As String
            ''' <summary>
            ''' Actual Field name is Status Remarks
            ''' </summary>
            ''' <returns></returns>
            Public Property Status_Remarks As String
            ''' <summary>
            ''' actual field name is Receipt No
            ''' </summary>
            ''' <returns></returns>
            Public Property Receipt_No As String
            ''' <summary>
            ''' Actual Field name is Kind of Donation
            ''' </summary>
            ''' <returns></returns>
            Public Property Kind_of_Donation As String
            ''' <summary>
            ''' Original Field name is Amt (in Foreign Curr.)
            ''' </summary>
            ''' <returns></returns>
            Public Property Amt_in_Foreign_Curr As Decimal?
            Public Property Currency As String
            ''' <summary>
            ''' Original Field name is Curr. Rate
            ''' </summary>
            ''' <returns></returns>
            Public Property Curr_Rate As Decimal?
            Public Property Offered_for_Audit As String
            Public Property Attachment_IDs As String
            Public Property VA_Status As String
            Public Property Attached_Form As String
            Public Property Attached_PAN As String
            Public Property Attached_Aadhar As String
            Public Property PAN As String
            Public Property AADHAR As String
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
        End Class
        <Serializable>
        Public Class Return_DonationRegister_Prints
            Public Property Print As String
            ''' <summary>
            ''' Actual Field name is Print Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Print_Date As DateTime
            Public Property Location As String
            Public Property DR_TR_ID As String
            Public Property pKey As String
        End Class
        <Serializable>
        Public Class Return_DonationRegister_Dispatches
            Public Property Mode As String
            ''' <summary>
            ''' Actual Field name is Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Disp_Date As DateTime
            Public Property Reference As String
            ''' <summary>
            ''' Actual field name is Reference No
            ''' </summary>
            ''' <returns></returns>
            Public Property Reference_No As String
            Public Property Remarks As String
            Public Property Location As String
            Public Property DR_TR_ID As String
            Public Property pKey As String
        End Class
        <Serializable>
        Public Class Return_DonationRegister_Rejections
            ''' <summary>
            ''' Actual field name is Reason Of Rejection
            ''' </summary>
            ''' <returns></returns>
            Public Property Reason_Of_Rejection As String
            ''' <summary>
            ''' Actual Field name is Rejected On
            ''' </summary>
            ''' <returns></returns>
            Public Property Rejected_On As DateTime
            Public Property DS_TR_ID As String
            Public Property pKey As String
        End Class


#End Region
        Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub

            Public Function GetAddresses() As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetAddresses, ClientScreen.Accounts_DonationRegister)
            End Function

            ''' <summary>
            ''' Get Address Detail, Shifted
            ''' </summary>
            ''' <param name="ABID"></param>
            ''' <returns></returns>
            ''' <remarks> RealServiceFunctions.Donation_GetAddressDetail</remarks>
            Public Function GetAddressDetail(ByVal ABID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetAddressDetail, ClientScreen.Accounts_DonationRegister, ABID)
            End Function

            ''' <summary>
            ''' Get Office AddressDetail, Shifted
            ''' </summary>
            ''' <param name="ABID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Donation_GetOfficeAddressDetail</remarks>
            Public Function GetOfficeAddressDetail(ByVal ABID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetOfficeAddressDetail, ClientScreen.Accounts_DonationRegister, ABID)
            End Function

            ''' <summary>
            ''' Get Foreign Donation Detail, Shifted
            ''' </summary>
            ''' <param name="TxnID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Donation_GetForeignDonationDetail</remarks>
            Public Function GetForeignDonationDetail(ByVal TxnID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetForeignDonationDetail, ClientScreen.Accounts_DonationRegister, TxnID)
            End Function

            'Shifted
            Public Function GetcurrencyByID(ByVal CID As String) As DataTable
                Return GetCurrencyName(CID, ClientScreen.Accounts_DonationRegister)
            End Function

            ''' <summary>
            ''' GetAddressDetail_Form, Shifted
            ''' </summary>
            ''' <param name="IsDonationOpen"></param>
            ''' <param name="Tr_ID"></param>
            ''' <param name="ABID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Donation_GetAddressDetail_Form</remarks>
            Public Function GetAddressDetail_Form(ByVal IsDonationOpen As Boolean, Optional ByVal Tr_ID As String = "", Optional ByVal ABID As String = "") As DataTable
                Dim Query As String = ""
                Dim dTable As DataTable = Nothing
                Dim Param As Param_DonationRegister_GetAddressDetail_Form = New Param_DonationRegister_GetAddressDetail_Form()
                Param.ABID = ABID
                Param.IsDonationOpen = IsDonationOpen
                Param.Tr_ID = Tr_ID
                dTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetAddressDetail_Form, ClientScreen.Accounts_DonationRegister, Param)
                Return dTable
            End Function

            'Shifted
            Public Function GetCityList(ByVal CityIDs As String) As DataTable
                Return GetCitiesByID(ClientScreen.Accounts_DonationRegister, "CI_NAME", "REC_ID", CityIDs)
            End Function

            'Shifted
            Public Function GetStateList(ByVal StateIDs As String) As DataTable
                Return GetStatesByID(ClientScreen.Accounts_DonationRegister, "ST_NAME", "REC_ID", StateIDs)
            End Function

            'Shifted
            Public Function GetDistrictList(ByVal DistrictIDs As String) As DataTable
                Return GetDistrictsByID(ClientScreen.Accounts_DonationRegister, "DI_NAME", "REC_ID", DistrictIDs)
            End Function

            'Shifted
            Public Function GetCountryList(ByVal CountryIDs As String) As DataTable
                Return GetCountriesByID(ClientScreen.Accounts_DonationRegister, "CO_NAME", "REC_ID", CountryIDs)
            End Function

            'Shifted
            Public Function GetCenterDetails(ByVal CENID As String) As DataTable
                Dim LocalQuery As String = "SELECT MAIN.CEN_NAME,CI.CEN_INS_ID ,CI.CEN_UID,CI.CEN_PAD_NO FROM centre_info as CI INNER JOIN CENTRE_INFO AS MAIN ON (CI.CEN_BK_PAD_NO =MAIN.CEN_PAD_NO AND MAIN.CEN_MAIN = TRUE) WHERE CI.CEN_ID =" & CENID & " ;"
                Return GetCenterDetailsByQuery(LocalQuery, ClientScreen.Accounts_DonationRegister, CENID)
            End Function

            'Shifted
            Public Function GetStatuses(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
                Return GetMisc("DONATION RECEIPT STATUS", ClientScreen.Accounts_DonationRegister, NameColumnHead, RecIdColumnHead)
            End Function

        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetList</remarks>
        Public Function GetList(FromDate As DateTime, ToDate As DateTime, Optional ID As String = Nothing) As List(Of Return_DonationRegister)
            Dim _Param As New RealTimeService.Param_DonationRegister_GetList()
            _Param.FromDate = FromDate
            _Param.ToDate = ToDate
            _Param.ID = ID
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetList, ClientScreen.Accounts_DonationRegister, _Param)
            Dim _Doantions As List(Of Return_DonationRegister) = New List(Of Return_DonationRegister)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_DonationRegister
                    newdata.DonationDate = row.Field(Of DateTime)("Date")
                    newdata.TR_AB_ID_1 = row.Field(Of String)("TR_AB_ID_1")
                    newdata.Donor_Name = row.Field(Of String)("Donor Name")
                    newdata.Address = row.Field(Of String)("Address")
                    newdata.PinCode = row.Field(Of String)("PinCode")
                    newdata.City = row.Field(Of String)("City")
                    newdata.State = row.Field(Of String)("State")
                    newdata.District = row.Field(Of String)("District")
                    newdata.Country = row.Field(Of String)("Country")
                    newdata.Purpose = row.Field(Of String)("Purpose")
                    newdata.Amount = row.Field(Of Decimal)("Amount")
                    newdata.Mode = row.Field(Of String)("Mode")
                    newdata.Cheque_DD_Ref_No = row.Field(Of String)("Cheque/DD/Ref No.")
                    newdata.Cheque_DD_Ref_Date = row.Field(Of DateTime?)("Cheque/DD/Ref Date")
                    newdata.Cheque_DD_Ref_Bank = row.Field(Of String)("Cheque/DD/Ref Bank")
                    newdata.DepBank = row.Field(Of String)("Dep. Bank")
                    newdata.Form_Received = row.Field(Of String)("Form Received")
                    newdata.Form_Received_Date = row.Field(Of DateTime?)("Form Received Date")
                    newdata.DS_STATUS_MISC_ID = row.Field(Of String)("DS_STATUS_MISC_ID")
                    newdata.Status = row.Field(Of String)("Status")
                    newdata.Status_Remarks = row.Field(Of String)("Status Remarks")
                    newdata.Receipt_No = row.Field(Of String)("Receipt No")
                    newdata.Kind_of_Donation = row.Field(Of String)("Kind of Donation")
                    newdata.Amt_in_Foreign_Curr = row.Field(Of Decimal?)("Amt (in Foreign Curr.)")
                    newdata.Currency = row.Field(Of String)("Currency")
                    newdata.Curr_Rate = row.Field(Of Decimal?)("Curr. Rate")
                    newdata.Offered_for_Audit = row.Field(Of String)("Offered_for_Audit")
                    newdata.Attachment_IDs = row.Field(Of String)("Attachment_IDs")
                    newdata.VA_Status = row.Field(Of String)("VA_Status")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Add_Date = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Edit_Date = row.Field(Of DateTime)("REC_EDIT_ON")
                    newdata.Action_Date = row.Field(Of DateTime)("REC_STATUS_ON")
                    newdata.Add_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Edit_By = row.Field(Of String)("REC_EDIT_BY")
                    newdata.Action_By = row.Field(Of String)("REC_STATUS_BY")
                    newdata.Action_Status = row.Field(Of String)("ACTION_STATUS")
                    newdata.Attached_Form = row.Field(Of String)("Attached_Form")
                    newdata.Attached_PAN = row.Field(Of String)("Attached_PAN")
                    newdata.Attached_Aadhar = row.Field(Of String)("Attached_Aadhar")
                    newdata.PAN = row.Field(Of String)("PAN")
                    newdata.AADHAR = row.Field(Of String)("AADHAR")
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
                    _Doantions.Add(newdata)
                Next
            End If
            Return _Doantions
        End Function

        Public Function GetReceiptDetails(Rec_ID As String) As DataTable
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetReceiptDetails, ClientScreen.Accounts_DonationRegister, Rec_ID)
            Return _RetTable
        End Function

        ''' <summary>
        ''' Get Record Detail,Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetRecDetail</remarks>
        Public Function GetRecDetail(ByVal RecID As String) As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetRecDetail, ClientScreen.Accounts_DonationRegister, RecID)
            End Function

            ''' <summary>
            ''' shifted
            ''' </summary>
            ''' <param name="MiscID"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetMiscNameByID(ByVal MiscID As String) As Object
                Dim Query As String = "SELECT MISC_NAME from MISC_INFO where REC_ID = '" & MiscID & "' ;"
                Dim dTable As DataTable = GetMisc_Common(Query, Query, ClientScreen.Accounts_DonationRegister, MiscID)
                If dTable Is Nothing Then Return Nothing
                If dTable.Rows.Count = 0 Then Return Nothing
                Return dTable.Rows(0)(0)
            End Function

            ''' <summary>
            ''' Shifted
            ''' </summary>
            ''' <param name="BankId"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetBankNameByID(ByVal BankId As String) As Object
                Dim Query As String = "SELECT BI_BANK_NAME From  BANK_INFO     " &
                                " Where   REC_ID = '" & BankId & "';"
                Dim dTable As DataTable = GetBankInfo(Query, Query, ClientScreen.Accounts_DonationRegister, BankId)
                If dTable Is Nothing Then Return Nothing
                If dTable.Rows.Count = 0 Then Return Nothing
                Return dTable.Rows(0)(0)
            End Function

            ''' <summary>
            ''' Get Donation Status, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Donation_GetDonationStatus</remarks>
            Public Function GetDonationStatus() As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetDonationStatus, ClientScreen.Accounts_DonationRegister)
            End Function

            ''' <summary>
            '''  Get Donation Purposes, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Donation_GetDonationPurposes</remarks>
            Public Function GetDonationPurposes() As DataTable
                Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetDonationPurposes, ClientScreen.Accounts_DonationRegister)
            End Function

            'Shifted
            Public Function GetServiceProjects(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
                Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_DonationRegister, NameColumnHead, RecIdColumnHead)
            End Function

            ''' <summary>
            ''' Get Donation Prints, Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.Donation_GetDonationPrints</remarks>
            Public Function GetDonationPrints() As List(Of Return_DonationRegister_Prints)
                Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetDonationPrints, ClientScreen.Accounts_DonationRegister)
                Dim _Doantions As List(Of Return_DonationRegister_Prints) = New List(Of Return_DonationRegister_Prints)
                If (Not (_RetTable) Is Nothing) Then
                    For Each row As DataRow In _RetTable.Rows
                        Dim newdata = New Return_DonationRegister_Prints
                        newdata.Print = row.Field(Of String)("Print")
                        newdata.Print_Date = row.Field(Of DateTime)("Print Date")
                        newdata.Location = row.Field(Of String)("Location")
                        newdata.DR_TR_ID = row.Field(Of String)("DR_TR_ID")
                        newdata.pKey = Guid.NewGuid.ToString()
                        _Doantions.Add(newdata)
                    Next
                End If
                Return _Doantions
            End Function

        ''' <summary>
        ''' Get Donation Rejections, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetDonationRejections</remarks>
        Public Function GetDonationRejections() As List(Of Return_DonationRegister_Rejections)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Donation_GetDonationRejections, ClientScreen.Accounts_DonationRegister)
            Dim _Doantions As List(Of Return_DonationRegister_Rejections) = New List(Of Return_DonationRegister_Rejections)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_DonationRegister_Rejections
                    newdata.Reason_Of_Rejection = row.Field(Of String)("Reason Of Rejection")
                    newdata.Rejected_On = row.Field(Of DateTime)("Rejected On")
                    newdata.DS_TR_ID = row.Field(Of String)("DS_TR_ID")
                    newdata.pKey = Guid.NewGuid.ToString()
                    _Doantions.Add(newdata)
                Next
            End If
            Return _Doantions
        End Function

        Public Function get_PaymentGateway_Logs(ByVal cenid As Int32, ByVal openYearid As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@OPEN_YEAR"}
            Dim values() As Object = {cenid, openYearid}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {6, 6}
            Return _RealService.ListFromSP(Tables.PAYMENT_GATEWAY_LOG, "[sp_get_Payment_Gateway_Logs]", Tables.SERVICE_CHART_SRNO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_Voucher_Donation))
        End Function
        Public Function get_GatewayBankslist(ByVal cenid As Int32, ByVal openYearid As Int32) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@OPEN_YEAR"}
            Dim values() As Object = {cenid, openYearid}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32}
            Dim lengths() As Integer = {6, 6}
            Return _RealService.ListFromSP(Tables.PAYMENT_GATEWAY_LOG, "[sp_get_GatewayBankslist]", Tables.SERVICE_CHART_SRNO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_Voucher_Donation))
        End Function

        ''' <summary>
        ''' Get Donation Dispatches, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetDonationDispatches</remarks>
        Public Function GetDonationDispatches(ByVal RecID As String) As List(Of Return_DonationRegister_Dispatches)
            Dim Query As String = "Select D.DD_MODE AS 'Mode',D.DD_DATE AS 'Date',D.DD_COMPANY AS Reference,D.DD_REF_NO AS 'Reference No',D.DD_OTHER_DETAIL as Remarks,CASE WHEN COALESCE(D.DD_REF_NO,'') LIKE '80G|%' THEN '<a href=""'+DD_LOCATION+'"" target=""_blank"">Open 80G Receipt (10BE)</a>  |  <a href=""'+DD_LOCATION+'"" download>Download 80G Receipt (10BE)</a>' ELSE D.DD_LOCATION END AS Location, COALESCE(R.DR_TR_ID , D.DD_TR_ID) AS DR_TR_ID  " &
                              " From (Transaction_Info AS T LEFT JOIN Donation_Receipt_Info AS R ON T.REC_ID = R.DR_TR_ID) LEFT JOIN Donation_Receipt_Dispatch_Info AS D ON R.REC_ID = D.DD_DR_ID OR T.REC_ID = D.DD_TR_ID " &
                              " Where   D.REC_STATUS IN (0,1,2) AND T.TR_CEN_ID=" & cBase._open_Cen_ID.ToString & " AND T.TR_COD_YEAR_ID=" & cBase._open_Year_ID.ToString & " And T.REC_ID='" & RecID & "'"
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim _RetTable As DataTable = _RealService.List(Tables.DONATION_RECEIPT_DISPATCH_INFO, Query, Tables.DONATION_RECEIPT_DISPATCH_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister))
            Dim _Doantions As List(Of Return_DonationRegister_Dispatches) = New List(Of Return_DonationRegister_Dispatches)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_DonationRegister_Dispatches
                    newdata.Mode = row.Field(Of String)("Mode")
                    newdata.Disp_Date = row.Field(Of DateTime)("Date")
                    newdata.Reference = row.Field(Of String)("Reference")
                    newdata.Reference_No = row.Field(Of String)("Reference No")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.Location = row.Field(Of String)("Location")
                    newdata.DR_TR_ID = row.Field(Of String)("DR_TR_ID")
                    newdata.pKey = Guid.NewGuid.ToString()
                    _Doantions.Add(newdata)
                Next
            End If
            Return _Doantions
        End Function

        Public Function RequestAReceipt(ByVal InParam As Parameter_Request_Receipt) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Donation_RequestReceipt, ClientScreen.Accounts_DonationRegister, InParam)
        End Function

        ''' <summary>
        ''' Returns Receipt No
        ''' </summary>
        ''' <param name="Txn_REC_ID"></param>
        ''' <param name="Txn_Year_ID"></param>
        ''' <param name="UserID"></param>
        ''' <param name="DonationFormReceiveDate"></param>
        ''' <param name="DonationVoucherDate"></param>
        ''' <param name="GeneratingCentreID"></param>
        ''' <param name="RequestingCenterID"></param>
        ''' <param name="DonationType"></param>
        ''' <param name="CurrStatus"></param>
        ''' <param name="AB_Rec_Id"></param>
        ''' <returns></returns>
        Public Function GenerateReceipt(ByVal Txn_REC_ID As String, ByVal Txn_Year_ID As String, ByVal UserID As String, ByVal DonationFormReceiveDate As DateTime, ByVal DonationVoucherDate As DateTime, ByVal GeneratingCentreID As String, ByVal RequestingCenterID As String, ByVal DonationType As String, ByVal CurrStatus As String, ByVal AB_Rec_Id As String) As String
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim ReceiptNo As String '= DonationReceiptInfo.GenerateReceiptNo(Txn_REC_ID, DonationVoucherDate, GeneratingCentreID, RequestingCenterID, DonationType, UserID.ToString)

            Dim Query As String = ("SELECT COUNT(*) FROM DONATION_STATUS_INFO WHERE REC_STATUS IN(0,1,2) AND  DS_TR_ID = '" + Txn_REC_ID + "'")
            Dim isEntryAvailableInStatusInfo As Boolean = _RealService.GetScalar(Tables.DONATION_STATUS_INFO, Query, Tables.DONATION_STATUS_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister))

            If isEntryAvailableInStatusInfo = False Then

                Query = "INSERT INTO DONATION_STATUS_INFO (DS_CEN_ID, DS_COD_YEAR_ID, DS_TR_ID, DS_STATUS_MISC_ID, DS_STATUS_ON, DS_STATUS_BY_ID, DS_STATUS_REMARKS, REC_ADD_ON, REC_ADD_BY, REC_EDIT_ON, REC_EDIT_BY, REC_STATUS, REC_STATUS_ON, REC_STATUS_BY, REC_ID) VALUES("
                Query = Query + cBase._open_Cen_ID.ToString()
                Query = Query + "," + Txn_Year_ID
                Query = Query + ",'" + Txn_REC_ID + "'"
                Query = Query + "," + "(SELECT REC_ID FROM Misc_info WHERE REC_STATUS IN(0,1,2) AND misc_name = 'receipt generated')"
                Query = Query + "," + "GETDATE()"
                Query = Query + ",'" + cBase._open_User_ID.ToString() + "'"
                Query = Query + ",''"
                Query = Query + "," + "GETDATE()"
                Query = Query + ",'" + cBase._open_User_ID.ToString() + "'"
                Query = Query + "," + "GETDATE()"
                Query = Query + ",'" + cBase._open_User_ID.ToString() + "'"
                Query = Query + ",1"
                Query = Query + "," + "GETDATE()"
                Query = Query + ",'" + cBase._open_User_ID.ToString() + "'"
                Query = Query + ",'" + Guid.NewGuid().ToString() + "')"
                _RealService.List(Tables.DONATION_STATUS_INFO, Query, Tables.DONATION_STATUS_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call
            End If

            Query = ("SELECT DR_NO,DR_TR_DATE,REC_ID FROM donation_receipt_info WHERE REC_STATUS IN(0,1,2) AND  DR_TR_ID = '" + (Txn_REC_ID + "' AND DR_IS_Active =1"))
            Dim PrevReceipts As DataTable = _RealService.List(Tables.DONATION_RECEIPT_INFO, Query, Tables.DONATION_RECEIPT_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister))
            If (Not (PrevReceipts) Is Nothing) Then
                If (PrevReceipts.Rows.Count > 0) Then
                    If Convert.ToDateTime(PrevReceipts.Rows(0)("DR_TR_DATE").ToString).Equals(DonationVoucherDate) Then
                        If Not CurrStatus.ToLower().Contains("generated") Then
                            Query = "update Donation_Status_Info Set DS_STATUS_MISC_ID =(SELECT REC_ID FROM Misc_info WHERE REC_STATUS IN(0,1,2) AND misc_name = 'receipt generated'), DS_STATUS_ON = getdate(),DS_STATUS_REMARKS = '', DS_STATUS_BY_ID = '" + cBase._open_User_ID.ToString() + "' where DS_TR_ID ='" + Txn_REC_ID + "' AND REC_STATUS IN (0,1,2) "
                            _RealService.List(Tables.DONATION_STATUS_INFO, Query, Tables.DONATION_STATUS_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call
                            '            Update(Query)
                            Query = "SELECT REC_ID FROM Donation_Status_Info where DS_TR_ID ='" + Txn_REC_ID + "' AND REC_STATUS IN (0,1,2) "
                            Dim doantion_status As DataTable = _RealService.List(Tables.DONATION_STATUS_INFO, Query, Tables.DONATION_STATUS_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister))
                            _RealService.MarkAsLocked(Tables.TRANSACTION_INFO, Txn_REC_ID, GetBaseParams(ClientScreen.Accounts_DonationRegister))
                            '            FreezeNonMasters(DBOperations1936.StandardTables.DONATION.ToString, Txn_REC_ID, UserID.ToString)
                            _RealService.MarkAsLocked(Tables.DONATION_STATUS_INFO, doantion_status.Rows(0)(0).ToString(), GetBaseParams(ClientScreen.Accounts_DonationRegister))
                            '            FreezeNonMasters(Donation_Status_Info.ToString, GetList(Query).Rows(0)(0).ToString, UserID.ToString)
                        End If
                        Return PrevReceipts.Rows(0)("DR_NO").ToString
                    End If

                    'No need to  make a new Receipt Entry as the existing Receipt Entry is fine
                    'Voucher Date has been changed by the client 
                    'cancel active Receipt and generate a new one
                    Dim CancelReceipt As String = ("UPDATE donation_receipt_info SET DR_IS_Active = 0 , DR_Remarks='Cancelled, as New Receipt No. is requested , as Voucher Date has been changed' where REC_ID ='" + (PrevReceipts.Rows(0)("REC_ID").ToString + "'"))
                    _RealService.List(Tables.DONATION_RECEIPT_INFO, CancelReceipt, Tables.DONATION_RECEIPT_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call 
                    'Update(CancelReceipt)
                    ' Delete(DBOperations1936.StandardTables.DONATION_RECEIPT, PrevReceipts.Rows(0)("REC_ID").ToString, UserID)
                    _RealService.Delete(Tables.DONATION_RECEIPT_INFO, PrevReceipts.Rows(0)("REC_ID").ToString(), GetBaseParams(ClientScreen.Accounts_DonationRegister))
                End If

            End If

            If (ReceiptNo = "") Then
                ReceiptNo = (DonationType.Substring(0, 1) + ("-" _
                            + (GeneratingCentreID.PadLeft(5, "0") + ("/" _
                            + (RequestingCenterID.PadLeft(5, "0") + ("/" _
                            + (DonationVoucherDate.Year.ToString.Substring(2, 2) + ("-" _
                            + (DonationVoucherDate.Month.ToString.PadLeft(2, "0") + ("-" + DonationVoucherDate.Day.ToString.PadLeft(2, "0")))))))))))
                Dim RectNumberQuery As String = "SELECT COALESCE(MAX(DR_SR_NO),0)+1 AS SRNO FROM Donation_Receipt_Info AS DR INNER JOIN transaction_info AS TR   ON dr.DR_TR_ID = tr.rec_id INNER JOIN CENTRE_INFO AS CI ON DR_CEN_ID = CI.CEN_ID  WHERE DR.REC_STATUS IN(0,1,2) AND TR.REC_STATUS IN(0,1,2) AND CI.REC_STATUS IN(0,1,2) AND  CAST(DR_TR_DATE AS DATE) = CAST('" + DonationVoucherDate.ToString("yyyy-MM-dd") + "' AS DATE) AND CI.CEN_INS_ID = (SELECT CEN_INS_ID FROM CENTRE_INFO WHERE REC_STATUS IN(0,1,2) AND  CEN_ID =" + GeneratingCentreID + ")"
                Dim ReceiptNoForDate As DataTable = _RealService.List(Tables.DONATION_RECEIPT_INFO, RectNumberQuery, Tables.DONATION_RECEIPT_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call 
                'GetList(getRecNoAvalbleForDateQuery(VoucherDate.ToString(DBOperations1936.DB_DefaultDateFormat()), RequestingCentreID)).Rows[0][0].ToString().PadLeft(4, '0');
                ReceiptNo = ReceiptNo + "/" + ReceiptNoForDate.Rows(0)(0).ToString().PadLeft(4, "0")
            End If
            If (ReceiptNo.Length = 0) Then
                Throw New Exception(("ReceiptNo not generated for txn ID :" _
                                    + (Txn_REC_ID + (",yearId:" _
                                    + (Txn_Year_ID + (",vchdate:" _
                                    + (DonationVoucherDate.ToString("dd/mm/yyyy") + (",gencenid:" _
                                    + (GeneratingCentreID + (",reqCenID:" _
                                    + (RequestingCenterID + (",donType:" _
                                    + (DonationType.ToString + (",userID:" + UserID.ToString))))))))))))))
            End If


            Query = "update Donation_Status_Info Set DS_STATUS_MISC_ID =(SELECT REC_ID FROM Misc_info WHERE REC_STATUS IN(0,1,2) AND misc_name = 'receipt generated'), DS_STATUS_ON = getdate(),DS_STATUS_REMARKS = '', DS_STATUS_BY_ID = '" + cBase._open_User_ID.ToString() + "' where DS_TR_ID ='" + Txn_REC_ID + "' AND REC_STATUS IN (0,1,2) "
            _RealService.List(Tables.DONATION_STATUS_INFO, Query, Tables.DONATION_STATUS_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call
            '            Update(Query)
            Query = "SELECT REC_ID FROM Donation_Status_Info where DS_TR_ID ='" + Txn_REC_ID + "' AND REC_STATUS IN (0,1,2) "
            Dim doantion_status_ID As DataTable = _RealService.List(Tables.DONATION_STATUS_INFO, Query, Tables.DONATION_STATUS_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister))
            _RealService.MarkAsLocked(Tables.TRANSACTION_INFO, Txn_REC_ID, GetBaseParams(ClientScreen.Accounts_DonationRegister))
            '            FreezeNonMasters(DBOperations1936.StandardTables.DONATION.ToString, Txn_REC_ID, UserID.ToString)
            _RealService.MarkAsLocked(Tables.DONATION_STATUS_INFO, doantion_status_ID.Rows(0)(0).ToString(), GetBaseParams(ClientScreen.Accounts_DonationRegister))
            '            FreezeNonMasters(Donation_Status_Info.ToString, GetList(Query).Rows(0)(0).ToString, UserID.ToString)

            '        (Update / Insert) DonationAddressBook Entry 
            '
            Query = "SELECT C_CEN_ID ,C_NAME,C_PAN_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_PASSPORT_NO,C_R_DISTRICT_ID,C_R_STATE_ID,C_R_COUNTRY_ID, C_R_PINCODE FROM ADDRESS_BOOK WHERE REC_STATUS IN(0,1,2) AND REC_ID = '" + AB_Rec_Id + "'"
            Dim AddressDetails As DataRow = _RealService.List(Tables.ADDRESS_BOOK, Query, Tables.ADDRESS_BOOK.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)).Rows(0) ' GetList(Query).Rows(0)
            Query = " SELECT COUNT(*) FROM Donation_Receipt_address_book WHERE C_TR_ID = '" + Txn_REC_ID + "' AND REC_STATUS IN (0,1,2) "
            Dim GeneratedAddressCount As Int32 = Convert.ToInt32(_RealService.List(Tables.DONATION_RECEIPT_ADDRESS_BOOK, Query, Tables.DONATION_RECEIPT_ADDRESS_BOOK.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)).Rows(0)(0).ToString)
            If (GeneratedAddressCount > 1) Then
                _RealService.DeleteByCondition(Tables.DONATION_RECEIPT_ADDRESS_BOOK, "C_TR_ID= '" + Txn_REC_ID + "' AND REC_STATUS IN (0,1,2)", GetBaseParams(ClientScreen.Accounts_DonationRegister))
            End If

            'If (CurrStatus.ToLower().Equals("donation accepted") Or CurrStatus.ToLower().Equals("receipt request rejected")) Then

            Query = ""
                RECID = Guid.NewGuid().ToString()
                Query = "INSERT INTO Donation_Receipt_address_book (C_CEN_ID, C_COD_YEAR_ID,C_AB_ID,C_TR_ID,C_NAME,C_PAN_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_PASSPORT_NO,C_R_DISTRICT_ID,C_R_STATE_ID,C_R_COUNTRY_ID,C_R_PINCODE,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID) Values("
                Query = Query + cBase._open_Cen_ID.ToString()
                Query = Query + "," + cBase._open_Year_ID.ToString()
                Query = Query + ",'" + AB_Rec_Id + "'"
                Query = Query + ",'" + Txn_REC_ID + "'"
                Query = Query + ",'" + AddressDetails("C_NAME").ToString() + "'"
                Query = Query + ",'" + AddressDetails("C_PAN_NO").ToString() + "'"
                Query = Query + ",'" + AddressDetails("C_R_ADD1").ToString() + "'"
                Query = Query + ",'" + AddressDetails("C_R_ADD2").ToString() + "'"
                Query = Query + ",'" + AddressDetails("C_R_ADD3").ToString() + "'"
                Query = Query + ",'" + AddressDetails("C_R_ADD4").ToString() + "'"
                Query = Query + "," + IIf(AddressDetails("C_R_CITY_ID").ToString() = "", "NULL", "'" + AddressDetails("C_R_CITY_ID").ToString() + "'")
                Query = Query + ",'" + AddressDetails("C_PASSPORT_NO").ToString() + "'"
                Query = Query + "," + IIf(AddressDetails("C_R_DISTRICT_ID").ToString() = "", "NULL", "'" + AddressDetails("C_R_DISTRICT_ID").ToString() + "'")
                Query = Query + "," + IIf(AddressDetails("C_R_STATE_ID").ToString() = "", "NULL", "'" + AddressDetails("C_R_STATE_ID").ToString() + "'")
                Query = Query + ",'" + AddressDetails("C_R_COUNTRY_ID").ToString() + "'"
                Query = Query + ",'" + AddressDetails("C_R_PINCODE").ToString() + "'"
                Query = Query + ",getdate(),'" + UserID + "',getdate(),'" + UserID + "',2,getdate(),'" + UserID + "','" + RECID + "');"
                _RealService.List(Tables.DONATION_RECEIPT_ADDRESS_BOOK, Query, Tables.DONATION_RECEIPT_ADDRESS_BOOK.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call
            'ConnectOne.Base.GeneralDBInteractions.SaveData(Query, True);

            'End If

            '--Insert in Receipt Info--
            If (ReceiptNo.Length > 0) Then
                Query = "SELECT RI.DR_FORM_REC_DATE as FORMDATE,  CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END AS 'Status',TI.TR_AB_ID_1 AS AB_ID, RI.REC_ID AS ReceiptID, ins.ins_ao_apply AS IsAOApplicable, " &
                     "CAST(TI.TR_DATE AS DATE) AS 'DATE', TR_CEN_ID AS CENID,TR_COD_YEAR_ID AS YEARID , " &
                     "CI.CEN_NAME + '(' + CEN_UID + ')' AS CENTER , " &
                     "DRAB.c_name AS 'DONOR',  CASE WHEN RI.DR_NO IS NULL THEN 'Not Generated' WHEN LEN(RI.DR_NO) = 0 THEN 'Not Generated' ELSE RI.DR_NO END AS RECEIPT, " &
                     "CASE WHEN (DRAB.C_PASSPORT_NO) IS NULL THEN 'Not Specified' WHEN LEN(DRAB.C_PASSPORT_NO) = 0 THEN  'Not Specified' ELSE DRAB.C_PASSPORT_NO END AS 'PASSPORT No.', " &
                     "CASE WHEN tr_code = 5 THEN 'REGULAR' WHEN tr_code = 6 THEN 'FOREIGN' END  AS 'DONATION TYPE'," &
                     "CASE WHEN (TR_VNO) IS NULL THEN 'Not Specified' WHEN LEN(TR_VNO) = 0 THEN 'Not Specified' ELSE TR_VNO END AS VOUCHER, " &
                     "TR_MODE AS 'MODE', TR_AMOUNT AS AMOUNT, " &
                     "CASE WHEN LEN(COALESCE(DRAB.C_R_ADD1,'') + COALESCE(DRAB.C_R_ADD2,'') + COALESCE(DRAB.C_R_ADD3,'') + COALESCE(DRAB.C_R_ADD4,'')) = 0 THEN 'Not Specified' ELSE COALESCE(DRAB.C_R_ADD1,'')+' ' + COALESCE(DRAB.C_R_ADD2,'')+' ' + COALESCE(DRAB.C_R_ADD3,'')+' '+ COALESCE(DRAB.C_R_ADD4,'') END AS ADDRESS, " &
                     "CASE WHEN MDCI.CI_NAME IS NULL THEN 'Not Specified' ELSE MDCI.CI_NAME END AS CITY ,  " &
                     "CASE WHEN MDSI.ST_NAME IS NULL THEN 'Not Specified' ELSE MDSI.ST_NAME END AS STATE,  " &
                     "CASE WHEN MDDI.DI_NAME IS NULL THEN 'Not Specified' ELSE MDDI.DI_NAME END AS DISTRICT, " &
                     "CASE WHEN MDCOI.CO_NAME IS NULL THEN 'Not Specified' ELSE MDCOI.CO_NAME END AS COUNTRY,  " &
                     "CASE WHEN DRAB.C_R_PINCODE IS NULL THEN 'Not Specified' WHEN LEN(DRAB.C_R_PINCODE) = 0 THEN  'Not Specified' ELSE DRAB.C_R_PINCODE END AS PINCODE, " &
                     "CASE WHEN DRAB.C_PAN_NO IS NULL THEN 'Not Specified' WHEN LEN(DRAB.C_PAN_NO) = 0 THEN  'Not Specified' ELSE DRAB.C_PAN_NO END AS 'PAN NO',  " &
                     "CASE WHEN BI.BI_BANK_NAME IS NULL THEN 'Not Specified' ELSE BI.BI_BANK_NAME END AS 'REF BANK', " &
                     "CASE WHEN TI.TR_REF_BRANCH IS NULL THEN 'Not Specified' WHEN LEN(TI.TR_REF_BRANCH) = 0 THEN 'Not Specified' ELSE TI.TR_REF_BRANCH END AS 'REF BRANCH',  " &
                     "CASE WHEN TI.TR_REF_NO IS NULL THEN 'Not Specified' WHEN LEN(TI.TR_REF_NO) = 0 THEN 'Not Specified' ELSE TI.TR_REF_NO END AS 'REF NO',  " &
                     "CASE WHEN TI.TR_REF_CDATE IS NULL THEN 'Not Specified' ELSE CAST(TI.TR_REF_CDATE AS VARCHAR) END AS CLEARING, " &
                     "CASE WHEN TI.TR_NARRATION IS NULL THEN 'Not Specified' WHEN LEN(TI.TR_NARRATION) = 0 THEN 'Not Specified' ELSE TI.TR_NARRATION END AS NARRATION, " &
                     "CASE WHEN TI.TR_REMARKS IS NULL THEN 'Not Specified' WHEN LEN(TI.TR_REMARKS) = 0 THEN 'Not Specified' ELSE TI.TR_REMARKS END AS REMARKS, " &
                     "CASE WHEN TI.TR_REFERENCE IS NULL THEN 'Not Specified' WHEN LEN(TI.TR_REFERENCE) = 0 THEN 'Not Specified' ELSE TI.TR_REFERENCE END AS REFERENCE  " &
                     "FROM transaction_info AS TI INNER JOIN centre_info AS CI ON ti.tr_cen_id = ci.cen_id  " &
                     "INNER JOIN institute_info AS INS ON INS.INS_ID = CI.cen_ins_id " &
                     "LEFT OUTER JOIN donation_receipt_address_book AS DRAB ON (DRAB.C_AB_ID = TI.tr_ab_id_1 AND DRAB.C_TR_ID = TI.REC_ID AND DRAB.REC_STATUS != -1)" &
                     "LEFT OUTER JOIN donation_receipt_info AS RI ON (RI.DR_TR_ID = TI.Rec_id AND RI.dr_is_active = 1) " &
                     "LEFT OUTER JOIN map_city_info AS MDCI ON DRAB.C_R_CITY_ID = MDCI.REC_ID " &
                     "LEFT OUTER JOIN map_state_info AS MDSI ON DRAB.C_R_STATE_ID = MDSI.REC_ID " &
                     "LEFT OUTER JOIN map_country_info AS MDCOI ON DRAB.C_R_COUNTRY_ID = MDCOI.REC_ID " &
                     "LEFT OUTER JOIN map_district_info AS MDDI ON DRAB.C_R_DISTRICT_ID = MDDI.REC_ID " &
                     "LEFT OUTER JOIN  bank_info AS BI ON BI.REC_ID = TI.TR_REF_BANK_ID  " &
                     "LEFT OUTER JOIN Donation_Status_Info AS DS ON (TI.Rec_Id = DS.DS_TR_ID and DS.REC_STATUS != -1) " &
                     "LEFT OUTER JOIN misc_info AS TS ON ds.DS_STATUS_MISC_ID = TS.rec_id " &
                     "WHERE tr_code IN (5,6) and TI.REC_ID = '" + Txn_REC_ID + "'"
                Dim TxnDetails As DataRow = _RealService.List(Tables.DONATION_RECEIPT_ADDRESS_BOOK, Query, Tables.DONATION_RECEIPT_ADDRESS_BOOK.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)).Rows(0)
                RECID = Guid.NewGuid().ToString()
                Query = "INSERT INTO Donation_Receipt_Info (DR_CEN_ID,DR_COD_YEAR_ID,DR_TR_ID,DR_NO,DR_SR_NO,DR_FORM_REC_DATE,DR_TR_DATE,DR_Remarks,DR_IS_ACTIVE, REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID) Values("
                Query = Query + cBase._open_Cen_ID.ToString()
                Query = Query + "," + cBase._open_Year_ID.ToString()
                Query = Query + ",'" + Txn_REC_ID + "'"
                Query = Query + ",'" + ReceiptNo + "'"
                Query = Query + ",'" + ReceiptNo.Substring(23, 4) + "'"
                If (DonationFormReceiveDate.Equals(DateTime.MinValue)) Then
                    Query = Query + ",NULL"
                Else
                    Query = Query + ",'" + DonationFormReceiveDate.ToString("yyyy-MM-dd") + "'"
                End If
                Query = Query + ",'" + DonationVoucherDate.ToString("yyyy-MM-dd") + "'"
                Query = Query + ",''"
                Query = Query + ",1"
                Query = Query + ",getdate(),'" + UserID + "',getdate(),'" + UserID + "',2,getdate(),'" + UserID + "','" + RECID + "');"
                _RealService.List(Tables.DONATION_RECEIPT_INFO, Query, Tables.DONATION_RECEIPT_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call

            End If
            Return ReceiptNo
            '#
            '            Dim transScope.Complete As endregion
        End Function

        Public Function RejectReceipt(ByVal DonationID As String, ByVal RejectionRemarks As String, ByVal UserID As String, ByVal AddressRecID As String) As Boolean
            Try

                Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
                Dim Query As String = "update Donation_Status_Info Set DS_STATUS_MISC_ID =(SELECT REC_ID FROM MISC_INFO WHERE misc_name = 'receipt request rejected')"
                Query = Query + ", DS_STATUS_ON = getdate()"
                Query = Query + ", DS_STATUS_REMARKS = '" + RejectionRemarks + "'"
                Query = Query + ", DS_STATUS_BY_ID = '" + UserID.ToString() + "'"
                Query = Query + " where DS_TR_ID ='" + DonationID + "' AND REC_STATUS IN (0,1,2) "
                _RealService.List(Tables.DONATION_STATUS_INFO, Query, Tables.DONATION_STATUS_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call
                'Update(Query)
                _RealService.MarkAsComplete(Tables.TRANSACTION_INFO, DonationID, GetBaseParams(ClientScreen.Accounts_DonationRegister))
                'UnfreezeNonMasters(DBOperations1936.StandardTables.DONATION.ToString, REC_ID, UserID.ToString)

                Query = "update Donation_Receipt_Info Set DR_TR_ID = NULL , REC_EDIT_ON = GETDATE() "
                Query = Query + " where DR_TR_ID='" + DonationID + "' AND REC_STATUS IN (0,1,2) "
                _RealService.List(Tables.DONATION_RECEIPT_INFO, Query, Tables.DONATION_RECEIPT_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call
                'Update(Query)

                Query = "update Donation_Receipt_address_book Set C_TR_ID = NULL , REC_EDIT_ON = GETDATE() "
                Query = Query + " where C_TR_ID='" + DonationID + "' AND REC_STATUS IN (0,1,2) "
                _RealService.List(Tables.DONATION_RECEIPT_ADDRESS_BOOK, Query, Tables.DONATION_RECEIPT_ADDRESS_BOOK.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call
                'Update(Query)

                Query = "SELECT REC_ID FROM Donation_Status_Info where DS_TR_ID ='" + DonationID + "' AND REC_STATUS IN (0,1,2) "
                Dim StatusID As String = _RealService.List(Tables.DONATION_STATUS_INFO, Query, Tables.DONATION_STATUS_INFO.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)).Rows(0)(0).ToString()
                'Mark Prev Status as Deleted
                _RealService.Delete(Tables.DONATION_STATUS_INFO, StatusID, GetBaseParams(ClientScreen.Accounts_DonationRegister))
                'Delete(DBOperations1936.StandardTables.DONATION_STATUS.ToString, GetList(Query).Rows(0)(0).ToString, UserID.ToString)

                If (Not (AddressRecID) Is Nothing) Then
                    If (AddressRecID.Length > 0) Then
                        'checks if there is no other entry in Donation Address book with same C_AB_ID and different txn ID , to make sure same freezed address is not being shared with any other receipt
                        Dim strDel As String = "Update Donation_Receipt_address_book set REC_STATUS = -1, REC_STATUS_ON = getdate(), REC_STATUS_BY = '" + UserID + "', REC_EDIT_ON = getdate(),REC_EDIT_BY='" + UserID + "' where "
                        strDel = strDel + " C_AB_ID in ('" + AddressRecID + "')"
                        strDel = strDel + " AND C_TR_ID in ('" + DonationID + "')"
                        _RealService.List(Tables.DONATION_RECEIPT_ADDRESS_BOOK, strDel, Tables.DONATION_RECEIPT_ADDRESS_BOOK.ToString, GetBaseParams(ClientScreen.Accounts_DonationRegister)) 'this query is actually update call
                        'Delete(DBOperations1936.StandardTables.DONATION_ADDRESS_BOOK.ToString, DonationAddressBook.AddressBookRefID, AddressRecID, AddressRecID.GetType, DonationAddressBook.TransactionID, REC_ID, REC_ID.GetType, UserID.ToString)
                    End If
                End If
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function

    End Class
    <Serializable>
    Public Class Voucher_Donation
        Inherits SharedVariables
#Region "Return classes"
        <Serializable>
        Public Class Return_DonationVoucherItemList

            Public Property ITEM_ID As String
            Public Property ITEM_NAME As String
            Public Property LED_NAME As String
            Public Property ITEM_TRANS_TYPE As String
            Public Property ITEM_LED_ID As String
            Public Property ITEM_VOUCHER_TYPE As String

        End Class
        <Serializable>
        Public Class Return_DonationVoucherPartyList

            Public Property C_ID As String
            Public Property C_NAME As String
            Public Property C_PASSPORT_NO As String
            Public Property CI_NAME As String
            Public Property CO_NAME As String
            Public Property C_PAN_NO As String
            Public Property C_TAX_ID_NO As String
            Public Property C_UID_NO As String
            Public Property REC_EDIT_ON As Date?
            Public Property C_R_ADD1 As String
            Public Property C_R_ADD2 As String
            Public Property C_R_ADD3 As String
            Public Property C_R_ADD4 As String
            Public Property C_R_PINCODE As String
            Public Property ST_NAME As String
            Public Property DI_NAME As String
            Public Property C_OTHER_ID As String
            Public Property C_OTHER_ID_LABEL As String
            Public Property MOB As String
        End Class
        <Serializable>
        Public Class Return_ReferenceBankList
            Public Property BI_ID As String
            Public Property BI_BANK_NAME As String
            Public Property BI_SHORT_NAME As String

        End Class
        <Serializable>
        Public Class Return_DonationVocuherPurpose

            Public Property PUR_NAME As String
            Public Property PUR_ID As String

        End Class
        <Serializable>
        Public Class Return_GetBankAccounts
            Public BANK_NAME As String
            Public BI_SHORT_NAME As String
            Public BANK_BRANCH As String
            Public BANK_ACC_NO As String
            Public BA_ID As String
            Public REC_EDIT_ON As DateTime
            Public BA_BRANCH_ID As String
        End Class
        <Serializable>
        Public Class Return_DonationGetRecord
            Public TR_CEN_ID As Integer?
            Public TR_COD_YEAR_ID As Integer?
            Public TR_CODE As Integer?
            Public TR_DATE As Date?
            Public TR_VNO As String
            Public TR_ITEM_ID As String
            Public TR_CR_LED_ID As String
            Public TR_SUB_CR_LED_ID As String
            Public TR_DR_LED_ID As String
            Public TR_SUB_DR_LED_ID As String
            Public TR_MODE As String
            Public TR_REF_BANK_ID As String
            Public TR_REF_BRANCH As String
            Public TR_REF_NO As String
            Public TR_REF_DATE As Date?
            Public TR_REF_CDATE As Date?
            Public TR_REF_RNO As String
            Public TR_AMOUNT As Decimal?
            Public TR_NARRATION As String
            Public TR_REMARKS As String
            Public TR_REFERENCE As String
            Public TR_AB_ID_1 As String
            Public TR_AB_ID_2 As String
            Public REC_ADD_ON As Date?
            Public REC_ADD_BY As String
            Public REC_EDIT_ON As Date?
            Public REC_EDIT_BY As String
            Public REC_STATUS As Integer?
            Public REC_STATUS_ON As Date?
            Public REC_STATUS_BY As String
            Public REC_ID As String
            Public TR_TYPE As String
            Public TR_SR_NO As Integer?
            Public TR_M_ID As String
            Public TR_NOTEBOOK As String
            Public TR_DR_STATUS_MISC_ID As String
            Public TR_DR_STATUS_ON As Date?
            Public TR_DR_STATUS_BY_ID As Integer?
            Public TR_DR_STATUS_REMARKS As String
            Public TR_MT_BANK_ID As String
            Public TR_MT_ACC_NO As String
            Public TR_TRF_CROSS_REF_ID As String
            Public TR_QTY As Decimal?
            Public TR_REF_OTHERS As String
        End Class
        <Serializable>
        Public Class Return_DonationGetSlipRecord
            Public TR_CEN_ID As Integer?
            Public TR_COD_YEAR_ID As Integer?
            Public TR_M_ID As String
            Public TR_REC_ID As String
            Public TR_SR_NO As Integer?
            Public TR_SLIP_ID As String
            Public REC_ID As String
            Public REC_ADD_ON As Date?
            Public REC_ADD_BY As String
            Public REC_STATUS As Integer?
        End Class
#End Region
        Public Sub New(ByVal _cBase As Common)
                MyBase.New(_cBase)
            End Sub

            Public Function GetStatus(ByVal Rec_Id As String) As Object
                Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_Donation, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
            End Function

            ''' <summary>
            ''' GetOldStatusID, Shifted
            ''' </summary>
            ''' <param name="TxnID"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.VoucherDonation_GetOldStatusID</remarks>
            Public Function GetOldStatusID(ByVal TxnID As String) As Object
                Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.VoucherDonation_GetOldStatusID, ClientScreen.Accounts_Voucher_Donation, TxnID)
            End Function

        Public Function GetRecord(ByVal Rec_ID As String) As List(Of Return_DonationGetRecord)
            Dim _retTable As DataTable = GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_Donation, RealTimeService.Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)
            Dim data As List(Of Return_DonationGetRecord) = New List(Of Return_DonationGetRecord)()
            If (Not (_retTable) Is Nothing) Then
                For Each row As DataRow In _retTable.Rows
                    Dim newdata As Return_DonationGetRecord = New Return_DonationGetRecord()
                    newdata.TR_CEN_ID = row.Field(Of Integer?)("TR_CEN_ID")
                    newdata.TR_COD_YEAR_ID = row.Field(Of Integer?)("TR_COD_YEAR_ID")
                    newdata.TR_CODE = row.Field(Of Integer?)("TR_CODE")
                    newdata.TR_DATE = row.Field(Of Date?)("TR_DATE")
                    newdata.TR_VNO = row.Field(Of String)("TR_VNO")
                    newdata.TR_ITEM_ID = row.Field(Of String)("TR_ITEM_ID")
                    newdata.TR_CR_LED_ID = row.Field(Of String)("TR_CR_LED_ID")
                    newdata.TR_SUB_CR_LED_ID = row.Field(Of String)("TR_SUB_CR_LED_ID")
                    newdata.TR_DR_LED_ID = row.Field(Of String)("TR_DR_LED_ID")
                    newdata.TR_SUB_DR_LED_ID = row.Field(Of String)("TR_SUB_DR_LED_ID")
                    newdata.TR_MODE = row.Field(Of String)("TR_MODE")
                    newdata.TR_REF_BANK_ID = row.Field(Of String)("TR_REF_BANK_ID")
                    newdata.TR_REF_BRANCH = row.Field(Of String)("TR_REF_BRANCH")
                    newdata.TR_REF_NO = row.Field(Of String)("TR_REF_NO")
                    newdata.TR_REF_DATE = row.Field(Of Date?)("TR_REF_DATE")
                    newdata.TR_REF_CDATE = row.Field(Of Date?)("TR_REF_CDATE")
                    newdata.TR_REF_RNO = row.Field(Of String)("TR_REF_RNO")
                    newdata.TR_AMOUNT = row.Field(Of Decimal?)("TR_AMOUNT")
                    newdata.TR_NARRATION = row.Field(Of String)("TR_NARRATION")
                    newdata.TR_REMARKS = row.Field(Of String)("TR_REMARKS")
                    newdata.TR_REFERENCE = row.Field(Of String)("TR_REFERENCE")
                    newdata.TR_AB_ID_1 = row.Field(Of String)("TR_AB_ID_1")
                    newdata.TR_AB_ID_2 = row.Field(Of String)("TR_AB_ID_2")
                    newdata.REC_ADD_ON = row.Field(Of Date?)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_EDIT_ON = row.Field(Of Date?)("REC_EDIT_ON")
                    newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                    newdata.REC_STATUS = row.Field(Of Integer?)("REC_STATUS")
                    newdata.REC_STATUS_ON = row.Field(Of Date?)("REC_STATUS_ON")
                    newdata.REC_STATUS_BY = row.Field(Of String)("REC_STATUS_BY")
                    newdata.REC_ID = row.Field(Of String)("Rec_ID")
                    newdata.TR_TYPE = row.Field(Of String)("TR_TYPE")
                    newdata.TR_SR_NO = row.Field(Of Integer?)("TR_SR_NO")
                    newdata.TR_M_ID = row.Field(Of String)("TR_M_ID")
                    newdata.TR_NOTEBOOK = row.Field(Of String)("TR_NOTEBOOK")
                    newdata.TR_DR_STATUS_MISC_ID = row.Field(Of String)("TR_DR_STATUS_MISC_ID")
                    newdata.TR_DR_STATUS_ON = row.Field(Of Date?)("TR_DR_STATUS_ON")
                    newdata.TR_DR_STATUS_BY_ID = row.Field(Of Integer?)("TR_DR_STATUS_BY_ID")
                    newdata.TR_DR_STATUS_REMARKS = row.Field(Of String)("TR_DR_STATUS_REMARKS")
                    newdata.TR_MT_BANK_ID = row.Field(Of String)("TR_MT_BANK_ID")
                    newdata.TR_MT_ACC_NO = row.Field(Of String)("TR_MT_ACC_NO")
                    newdata.TR_TRF_CROSS_REF_ID = row.Field(Of String)("TR_TRF_CROSS_REF_ID")
                    newdata.TR_QTY = row.Field(Of Decimal?)("TR_QTY")
                    newdata.TR_REF_OTHERS = row.Field(Of String)("TR_REF_OTHERS")
                    data.Add(newdata)
                Next
                Return data
            Else
                Return Nothing
            End If
        End Function

        Public Function GetPurposeRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_Donation, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
        End Function

        Public Function GetForeignRecord(ByVal Rec_ID As String) As DataTable
                Return GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_Donation, RealTimeService.Tables.TRANSACTION_D_FOREIGN_INFO)
            End Function

            Public Function GetReceiptPrintRecord(ByVal Rec_ID As String) As DataTable
                Return GetRecordByColumn("DR_TR_ID", Rec_ID, ClientScreen.Accounts_Voucher_Donation, RealTimeService.Tables.DONATION_RECEIPT_INFO)
            End Function

            'Shifted
            Public Function GetItemList(Optional ByVal IsForeign As Boolean = False) As List(Of Return_DonationVoucherItemList)
                Dim _RetTable As DataTable
                If IsForeign Then
                    _RetTable = GetItems_Ledger(ClientScreen.Accounts_Voucher_Donation, "DONATION - FOREIGN")
                Else
                    _RetTable = GetItems_Ledger(ClientScreen.Accounts_Voucher_Donation, "DONATION")
                End If
                Dim _ItemList As List(Of Return_DonationVoucherItemList) = New List(Of Return_DonationVoucherItemList)
                If (Not (_RetTable) Is Nothing) Then
                    For Each row As DataRow In _RetTable.Rows
                        Dim newdata = New Return_DonationVoucherItemList
                        newdata.ITEM_ID = row.Field(Of String)("ITEM_ID")
                        newdata.ITEM_NAME = row.Field(Of String)("ITEM_NAME")
                        newdata.LED_NAME = row.Field(Of String)("LED_NAME")
                        newdata.ITEM_TRANS_TYPE = row.Field(Of String)("ITEM_TRANS_TYPE")
                        newdata.ITEM_LED_ID = row.Field(Of String)("ITEM_LED_ID")
                        newdata.ITEM_VOUCHER_TYPE = row.Field(Of String)("ITEM_VOUCHER_TYPE")
                        _ItemList.Add(newdata)
                    Next
                End If
                Return _ItemList
            End Function

            'Shifted
            Public Function GetPartyDetails(Optional ByVal IndianOnly As Boolean = True, Optional Party_RecID As String = Nothing) As List(Of Return_DonationVoucherPartyList)
                Dim _AB As Addresses = New Addresses(cBase)
                Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
                Param.IndianOnly = IndianOnly
                Param.Party_Rec_ID = Party_RecID
                Dim _RetTable As DataTable = _AB.GetList(ClientScreen.Accounts_Voucher_Donation, Param)
                Dim _PartyList As List(Of Return_DonationVoucherPartyList) = New List(Of Return_DonationVoucherPartyList)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_DonationVoucherPartyList
                    newdata.C_ID = row.Field(Of String)("C_ID")
                    newdata.C_NAME = row.Field(Of String)("C_NAME")
                    newdata.C_PASSPORT_NO = row.Field(Of String)("C_PASSPORT_NO")
                    newdata.C_PAN_NO = row.Field(Of String)("C_PAN_NO")
                    newdata.C_TAX_ID_NO = row.Field(Of String)("C_TAX_ID_NO")
                    newdata.C_UID_NO = row.Field(Of String)("C_UID_NO")
                    newdata.CI_NAME = row.Field(Of String)("CI_NAME")
                    newdata.CO_NAME = row.Field(Of String)("CO_NAME")
                    newdata.C_R_ADD1 = row.Field(Of String)("C_R_ADD1")
                    newdata.C_R_ADD2 = row.Field(Of String)("C_R_ADD2")
                    newdata.C_R_ADD3 = row.Field(Of String)("C_R_ADD3")
                    newdata.C_R_ADD4 = row.Field(Of String)("C_R_ADD4")
                    newdata.C_R_PINCODE = row.Field(Of String)("C_R_PINCODE")
                    newdata.ST_NAME = row.Field(Of String)("ST_NAME")
                    newdata.DI_NAME = row.Field(Of String)("DI_NAME")
                    newdata.REC_EDIT_ON = row.Field(Of Date?)("REC_EDIT_ON")
                    newdata.C_OTHER_ID = row.Field(Of String)("C_OTHER_ID")
                    newdata.C_OTHER_ID_LABEL = row.Field(Of String)("C_OTHER_ID_LABEL")
                    newdata.MOB = row.Field(Of String)("MOB")
                    _PartyList.Add(newdata)
                Next
                Return _PartyList
            Else
                Return Nothing
            End If
        End Function

            'Shifted
            Public Function GetCityList(ByVal CityIDs As String) As DataTable
                Return GetCitiesByID(ClientScreen.Accounts_Voucher_Donation, "CI_NAME", "REC_ID", CityIDs)
            End Function

            'Shifted
            Public Function GetStateList(ByVal CityIDs As String) As DataTable
                Return GetStatesByID(ClientScreen.Accounts_Voucher_Donation, "ST_NAME", "REC_ID", CityIDs)
            End Function

            'Shifted
            Public Function GetCountryList(ByVal COIDs As String) As DataTable
                Return GetCountriesByID(ClientScreen.Accounts_Voucher_Donation, "CO_NAME", "REC_ID", COIDs)
            End Function

            'Shifted
            Public Function GetDistrictList(ByVal CityIDs As String) As DataTable
                Return GetDistrictsByID(ClientScreen.Accounts_Voucher_Donation, "DI_NAME", "REC_ID", CityIDs)
            End Function

            ''' <summary>
            ''' Shifted
            ''' </summary>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetBankList() As List(Of Return_ReferenceBankList)
                Dim Query As String = "SELECT BI_BANK_NAME ,BI_SHORT_NAME,REC_ID as BI_ID " &
                              " From  BANK_INFO     " &
                              " Where   REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
                Dim _RetTable As DataTable = GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_Donation)
                Dim _BankList As List(Of Return_ReferenceBankList) = New List(Of Return_ReferenceBankList)
                If (Not (_RetTable) Is Nothing) Then
                    For Each row As DataRow In _RetTable.Rows
                        Dim newdata = New Return_ReferenceBankList
                        newdata.BI_ID = row.Field(Of String)("BI_ID")
                        newdata.BI_BANK_NAME = row.Field(Of String)("BI_BANK_NAME")
                        newdata.BI_SHORT_NAME = row.Field(Of String)("BI_SHORT_NAME")
                        _BankList.Add(newdata)
                    Next
                End If
                Return _BankList
            End Function

            'Shifted
            Public Function GetBankAccounts(Optional ByVal ForeignOnly As Boolean = False, Optional Bank_Account_Rec_ID As String = Nothing) As List(Of Return_GetBankAccounts)
                Dim _BA As BankAccounts = New BankAccounts(cBase)
                Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common()
                Param.ForeignOnly = ForeignOnly
                Param.Bank_Account_Rec_ID = Bank_Account_Rec_ID
                Dim _Ba_Table As DataTable = _BA.GetList(ClientScreen.Accounts_Voucher_Donation, Param)
                Dim _GetPOItemsOrdered_data As List(Of Return_GetBankAccounts) = New List(Of Return_GetBankAccounts)
            If (Not (_Ba_Table) Is Nothing) Then
                For Each row As DataRow In _Ba_Table.Rows
                    Dim newdata = New Return_GetBankAccounts
                    newdata.BANK_NAME = row.Field(Of String)("BANK_NAME")
                    newdata.BI_SHORT_NAME = row.Field(Of String)("BI_SHORT_NAME")
                    newdata.BANK_BRANCH = row.Field(Of String)("BANK_BRANCH")
                    newdata.BANK_ACC_NO = row.Field(Of String)("BANK_ACC_NO")
                    newdata.BA_ID = row.Field(Of String)("BA_ID")
                    newdata.BA_BRANCH_ID = row.Field(Of String)("BA_BRANCH_ID")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    _GetPOItemsOrdered_data.Add(newdata)
                Next
                Return _GetPOItemsOrdered_data
            Else
                Return Nothing
            End If
        End Function

            'Shifted
            Public Function GetPurposes() As List(Of Return_DonationVocuherPurpose)
                ' Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Donation, "PUR_NAME", "PUR_ID")
                Dim InParam As Param_GetMisc = New Param_GetMisc()
                '  InParam.MiscId = MiscId
                InParam.MiscNameColumnHead = "PUR_NAME"
                InParam.RecIDColumnHead = "PUR_ID"
                Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Data_GetPurpose, ClientScreen.Accounts_Voucher_Donation, InParam)
                Dim _Purposes As List(Of Return_DonationVocuherPurpose) = New List(Of Return_DonationVocuherPurpose)
                If (Not (_RetTable) Is Nothing) Then
                    For Each row As DataRow In _RetTable.Rows
                        Dim newdata = New Return_DonationVocuherPurpose
                        newdata.PUR_NAME = row.Field(Of String)("PUR_NAME")
                        newdata.PUR_ID = row.Field(Of String)("PUR_ID")
                        _Purposes.Add(newdata)
                    Next
                End If
                Return _Purposes
            End Function

            'Shifted
            Public Function GetCategories() As DataTable
                Return GetMisc("DONOR CATEGORY", ClientScreen.Accounts_Voucher_Donation, "CAT_NAME", "CAT_ID")
            End Function

            'Shifted
            Public Function GetCurrencies() As DataTable
                Return GetCurrencyList(ClientScreen.Accounts_Voucher_Donation)
            End Function

            ''' <summary>
            ''' Returns Transaction dates of Donation (Kind/Cash) Vouchers where Referred party has been used in past
            ''' </summary>
            ''' <param name="AbRecId"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetUsageAsPastDonor(ByVal AbRecId As String) As DataTable
                Return GetDataListOfRecords(RealServiceFunctions.Donation_CheckUsageAsPastDonor, ClientScreen.Facility_AddressBook, AbRecId)
            End Function

            ''' <summary>
            ''' Returns Transaction dates of Foreign Donation Vouchers where Referred party has been used in past
            ''' </summary>
            ''' <param name="AbRecId"></param>
            ''' <returns></returns>
            ''' <remarks></remarks>
            Public Function GetUsageAsPastForeignDonor(ByVal AbRecId As String) As DataTable
                Return GetDataListOfRecords(RealServiceFunctions.Donation_CheckUsageAsPastForeignDonor, ClientScreen.Facility_AddressBook, AbRecId)
            End Function

        Public Function GetSlipRecord(ByVal REC_ID As String) As List(Of Return_DonationGetSlipRecord)
            Dim _retTable As DataTable = GetRecordByColumn("TR_REC_ID", REC_ID, ClientScreen.Accounts_Voucher_Donation, RealTimeService.Tables.TRANSACTION_D_SLIP_INFO)
            Dim data As List(Of Return_DonationGetSlipRecord) = New List(Of Return_DonationGetSlipRecord)
            If (Not (_retTable) Is Nothing) Then
                For Each row As DataRow In _retTable.Rows
                    Dim newdata As Return_DonationGetSlipRecord = New Return_DonationGetSlipRecord()
                    newdata.TR_CEN_ID = row.Field(Of Integer?)("TR_CEN_ID")
                    newdata.TR_COD_YEAR_ID = row.Field(Of Integer?)("TR_COD_YEAR_ID")
                    newdata.TR_M_ID = row.Field(Of String)("TR_M_ID")
                    newdata.TR_REC_ID = row.Field(Of String)("TR_REC_ID")
                    newdata.TR_SR_NO = row.Field(Of Integer?)("TR_SR_NO")
                    newdata.TR_SLIP_ID = row.Field(Of String)("TR_SLIP_ID")
                    newdata.REC_ID = row.Field(Of String)("Rec_ID")
                    newdata.REC_ADD_ON = row.Field(Of Date?)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_STATUS = row.Field(Of Integer?)("REC_STATUS")
                    data.Add(newdata)
                Next
            End If
            Return data
        End Function

        'Public Function GetDonorsFor80GReceipts() As DataTable
        '    Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
        '    Dim paramters As String() = {}
        '    Dim values() As Object = {}
        '    Dim dbTypes() As System.Data.DbType = {}
        '    Dim lengths() As Integer = {}
        '    Return _RealService.ListFromSP(Tables.DONATION_RECEIPT_DISPATCH_INFO, "[sp_get_DonorsFor80GReceipts]", Tables.DONATION_RECEIPT_DISPATCH_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Option_UserPreferences))
        'End Function
        Public Function GetDonorsDonationDataFor80G(ByVal AB_ID As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@AB_ID"}
            Dim values() As Object = {AB_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String}
            Dim lengths() As Integer = {36}
            Return _RealService.ListFromSP(Tables.DONATION_RECEIPT_DISPATCH_INFO, "[sp_get_80GReceiptForDonor]", Tables.DONATION_RECEIPT_DISPATCH_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Option_UserPreferences))
        End Function
        Public Function Insert80GReceiptsDataSentByEmail(ByVal AB_ID As String, ByVal User_ID As String, ByVal Mode As String, ByVal Company As String, ByVal Other_Details As String, ByVal Location As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_insert_80GReceiptsDataSentByEmail"
            Dim params() As String = {"@AB_ID", "@USER_ID", "@MODE", "@COMPANY", "@OTHER_DETAILS", "@LOCATION"}
            Dim values() As Object = {AB_ID, User_ID, Mode, Company, Other_Details, Location}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {36, 255, 50, 255, 255, 255}
            'used public insert function as there are no transactional data involved 
            Return _RealService.InsertBySPPublic(Tables.DONATION_RECEIPT_DISPATCH_INFO, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_DonationRegister))
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_Voucher_Donation) As Boolean
                'InParam.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead
                Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherDonation_Insert, ClientScreen.Accounts_Voucher_Donation, InParam)
            End Function

            ''' <summary>
            ''' Insert Foreign Info, Shifted
            ''' </summary>
            ''' <param name="InFgnInfo"></param>
            ''' <returns></returns>
            ''' <remarks> RealServiceFunctions.VoucherDonation_InsertForeignInfo</remarks>
            Public Function InsertForeignInfo(ByVal InFgnInfo As Parameter_InsertForeignInfo_Voucher_Donation) As Boolean
                'InFgnInfo.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead
                Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherDonation_InsertForeignInfo, ClientScreen.Accounts_Voucher_Donation, InFgnInfo)
            End Function

            ''' <summary>
            ''' Insert Purpose, Shifted
            ''' </summary>
            ''' <param name="InPurpose"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.VoucherDonation_InsertPurpose</remarks>
            Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_Voucher_Donation) As Boolean
                'InPurpose.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead
                Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherDonation_InsertPurpose, ClientScreen.Accounts_Voucher_Donation, InPurpose)
            End Function

            ''' <summary>
            ''' Insert Donation Status, Shifted
            ''' </summary>
            ''' <param name="InDnStatus"></param>
            ''' <returns></returns>
            ''' <remarks>RealServiceFunctions.VoucherDonation_InsertDonationStatus</remarks>
            Public Function InsertDonationStatus(ByVal InDnStatus As Parameter_InsertDonStatus_Voucher_Donation) As Boolean
                'InDnStatus.openYearID = cBase._open_Year_ID ' Removed and used from basicParam instead
                Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherDonation_InsertDonationStatus, ClientScreen.Accounts_Voucher_Donation, InDnStatus)
            End Function

        ''' <summary>
        ''' Updates Info, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Voucher_Donation) As Boolean
            Dim _Return As Integer = UpdateRecord(RealTimeService.RealServiceFunctions.VoucherDonation_Update, ClientScreen.Accounts_Voucher_Donation, UpParam)
            If _Return Then cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Donation)
            Return _Return
        End Function

        ''' <summary>
        ''' Update Purpose, Shifted
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_UpdatePurpose</remarks>
        Public Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_Voucher_Donation) As Boolean
                Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherDonation_UpdatePurpose, ClientScreen.Accounts_Voucher_Donation, UpPurpose)
            End Function

        ''' <summary>
        ''' Update ForeignInfo, Shifted
        ''' </summary>
        ''' <param name="UpFgnInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_UpdateForeignInfo</remarks>
        Public Function UpdateForeignInfo(ByVal UpFgnInfo As Parameter_UpdateForeignInfo_Voucher_Donation) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpFgnInfo.TxnID, ClientScreen.Accounts_Voucher_Donation)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherDonation_UpdateForeignInfo, ClientScreen.Accounts_Voucher_Donation, UpFgnInfo)
        End Function

        ''' <summary>
        ''' Update Status, Shifted
        ''' </summary>
        ''' <param name="UpStatus"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_UpdateStatus</remarks>
        Public Function UpdateStatus(ByVal UpStatus As Parameter_UpdateStatus_Voucher_Donation) As Boolean
                Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherDonation_UpdateStatus, ClientScreen.Accounts_Voucher_Donation, UpStatus)
            End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Donation)
            Return DeleteRecord(Rec_Id, Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_Donation)
        End Function

        Public Overloads Function DeletePurpose(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Donation)
            Return DeleteByCondition("TR_REC_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_Donation)
        End Function

        Public Overloads Function DeleteStatus(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Donation)
            Return DeleteByCondition("DS_TR_ID = '" & Rec_Id & "'", Tables.DONATION_STATUS_INFO, ClientScreen.Accounts_Voucher_Donation)
        End Function

        Public Overloads Function DeleteForeignInfo(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(Rec_Id, ClientScreen.Accounts_Voucher_Donation)
            Return DeleteByCondition("TR_REC_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_FOREIGN_INFO, ClientScreen.Accounts_Voucher_Donation)
        End Function

        Public Function InsertDonation_Txn(Inparam As Param_Txn_Insert_VoucherDonation) As Boolean
                Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherDonation_InsertDonation_Txn, ClientScreen.Accounts_Voucher_Donation, Inparam)
            End Function

        Public Function UpdateDonation_Txn(UpParam As Param_Txn_Update_VoucherDonation) As Boolean
            Dim _Return As Boolean = ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherDonation_UpdateDonation_Txn, ClientScreen.Accounts_Voucher_Donation, UpParam)
            If _Return Then cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.param_Update.RecID, ClientScreen.Accounts_Voucher_Donation)
            Return _Return
        End Function

        Public Function DeleteDonation_Txn(DelParam As Param_Txn_Delete_VoucherDonation) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.RecID_Delete, ClientScreen.Accounts_Voucher_Donation)

            'FCRA Related code or Special Voucher Reference related code
            DeleteByCondition("COALESCE(TR_M_ID,TXN_REC_ID) ='" + DelParam.RecID_Delete + "'", Tables.TRANSACTION_D_REFERENCE_INFO, ClientScreen.Accounts_Voucher_CashBank)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherDonation_DeleteDonation_Txn, ClientScreen.Accounts_Voucher_Donation, DelParam)
        End Function

    End Class
#End Region
    End Class
