Imports Common_Lib.RealTimeService
Partial Public Class DbOperations

#Region "--Profile--"
    <Serializable>
    Public Class Magazine
        Inherits SharedVariables

#Region "Return Parameters"
        <Serializable>
        Public Class Return_MagazineDispatchType
            Public Property Name As String
            Public Property DispatchType_ID As String
            Public Property DispatchType_Default As Boolean
            Public Property AddBy As String
            Public Property AddDate As DateTime
            Public Property EditBy As String
            Public Property EditDate As DateTime
            Public Property ActionStatus As String
            Public Property ActionBy As String
            Public Property ActionDate As DateTime
        End Class
        <Serializable>
        Public Class Return_MagazineDispatchType_Charges
            Public Property EffectiveDate As DateTime
            Public Property Charges As Decimal
            Public Property DispatchTypeCharges_ID As String
            Public Property AddBy As String
            Public Property AddDate As DateTime
            Public Property EditBy As String
            Public Property EditDate As DateTime
            Public Property DispatchType_ID As String
            Public Property ActionBy As String
            Public Property ActionStatus As String
            Public Property ActionDate As DateTime
        End Class
        <Serializable>
        Public Class Return_Existing_Mag_Membership_List
            Public Property MMB_ID As String
            ''' <summary>
            ''' Actual Field name is Membership ID
            ''' </summary>
            ''' <returns></returns>
            Public Property Membership_ID As String
            ''' <summary>
            ''' Actual Field name is Start Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Start_Date As DateTime
            Public Property MEM_NAME As String
            Public Property MEM_ID As String
            Public Property COPIES As Integer
            ''' <summary>
            ''' Actual Field name is Curr. Due
            ''' </summary>
            ''' <returns></returns>
            Public Property Curr_Due As Decimal
            ''' <summary>
            ''' Actual Field name is Curr. Advance
            ''' </summary>
            ''' <returns></returns>
            Public Property Curr_Advance As Decimal
            Public Property Period As String
            Public Property Magazine As String
            ''' <summary>
            ''' Actual Field name is Membership Old ID
            ''' </summary>
            ''' <returns></returns>
            Public Property Membership_Old_ID As String
            Public Property MEM_ADDRESS As String
            Public Property MagID As String
            Public Property MS_ID As String
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
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String, Optional screen As ClientScreen = Nothing) As DataTable
            Dim Param As Param_GetList_Magazine = New Param_GetList_Magazine
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetList, IIf(screen = Nothing, ClientScreen.Profile_Magazine, screen), Param)
        End Function

        Public Function GetList_SubscriptionTypeList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String) As DataTable
            Dim Param As Param_GetList_Magazine = New Param_GetList_Magazine
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetList_SubcriptionType, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetList_SubscriptionTypeFeeList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String) As DataTable
            Dim Param As Param_GetList_Magazine = New Param_GetList_Magazine
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetList_SubcriptionTypeFee, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetSubscriptionTypeFee(ByVal SubsID As String, ByVal StartDate As Date) As DataTable
            Dim Param As Param_GetFee_Magazine = New Param_GetFee_Magazine
            Param.ID = SubsID
            Param.StartDate = StartDate
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetSubcriptionTypeFee, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetList_DispatchTypeList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String, Optional screen As ClientScreen = Nothing) As DataTable
            Dim Param As Param_GetList_Magazine = New Param_GetList_Magazine
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetList_DispatchType, IIf(screen = Nothing, ClientScreen.Profile_Magazine, screen), Param)

        End Function
        Public Function GetList_Magazine_DispatchTypeList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String, Optional screen As ClientScreen = Nothing) As List(Of Return_MagazineDispatchType)
            Dim Param As Param_GetList_Magazine = New Param_GetList_Magazine
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetList_DispatchType, IIf(screen = Nothing, ClientScreen.Profile_Magazine, screen), Param)
            Dim MagazineDispatchTypeList As List(Of Return_MagazineDispatchType) = New List(Of Return_MagazineDispatchType)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim magazinedispatchtypeinfo = New Return_MagazineDispatchType
                    magazinedispatchtypeinfo.Name = row.Field(Of String)("Name")
                    magazinedispatchtypeinfo.DispatchType_ID = row.Field(Of String)("ID")
                    magazinedispatchtypeinfo.DispatchType_Default = row.Field(Of Boolean)("Default")
                    magazinedispatchtypeinfo.AddBy = row.Field(Of String)("Add BY")
                    magazinedispatchtypeinfo.AddDate = row.Field(Of DateTime)("Add Date")
                    magazinedispatchtypeinfo.EditBy = row.Field(Of String)("Edit BY")
                    magazinedispatchtypeinfo.EditDate = row.Field(Of DateTime)("Edit Date")
                    magazinedispatchtypeinfo.ActionStatus = row.Field(Of String)("Action Status")
                    magazinedispatchtypeinfo.ActionBy = row.Field(Of String)("Action By")
                    magazinedispatchtypeinfo.ActionDate = row.Field(Of DateTime)("Action Date")
                    MagazineDispatchTypeList.Add(magazinedispatchtypeinfo)
                Next
            End If
            Return MagazineDispatchTypeList
        End Function

        Public Function GetList_DispatchTypeChargesList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String) As List(Of Return_MagazineDispatchType_Charges)
            Dim Param As Param_GetList_Magazine = New Param_GetList_Magazine
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetList_DispatchTypeCharges, ClientScreen.Profile_Magazine, Param)
            Dim MagazineDispatchTypeChargesList As List(Of Return_MagazineDispatchType_Charges) = New List(Of Return_MagazineDispatchType_Charges)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim magazinedispatchtypeinfo = New Return_MagazineDispatchType_Charges
                    magazinedispatchtypeinfo.EffectiveDate = row.Field(Of DateTime)("Effective Date")
                    magazinedispatchtypeinfo.DispatchTypeCharges_ID = row.Field(Of String)("ID")
                    magazinedispatchtypeinfo.Charges = row.Field(Of Decimal)("Charges")
                    magazinedispatchtypeinfo.AddBy = row.Field(Of String)("Add BY")
                    magazinedispatchtypeinfo.AddDate = row.Field(Of DateTime)("Add Date")
                    magazinedispatchtypeinfo.EditBy = row.Field(Of String)("Edit BY")
                    magazinedispatchtypeinfo.EditDate = row.Field(Of DateTime)("Edit Date")
                    magazinedispatchtypeinfo.DispatchType_ID = row.Field(Of String)("MDTC_MDT_ID")
                    magazinedispatchtypeinfo.ActionBy = row.Field(Of String)("Action By")
                    magazinedispatchtypeinfo.ActionDate = row.Field(Of DateTime)("Action Date")
                    magazinedispatchtypeinfo.ActionStatus = row.Field(Of String)("Action Status")

                    MagazineDispatchTypeChargesList.Add(magazinedispatchtypeinfo)
                Next
            End If
            Return MagazineDispatchTypeChargesList
        End Function

        Public Function GetDispatchTypeCharges(ByVal DT_ID As String, ByVal StartDate As Date) As DataTable
            Dim Param As Param_GetFee_Magazine = New Param_GetFee_Magazine
            Param.ID = DT_ID
            Param.StartDate = StartDate
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetDispatchTypeCharges, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetList_GeetaPathshala(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String) As DataTable
            Dim Param As Param_GetList_Magazine = New Param_GetList_Magazine
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetList_GeetaPathshala, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetList_Centres(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String) As DataTable
            Dim Param As Param_GetList_Magazine = New Param_GetList_Magazine
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetList_Centres, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetList_Membership(Optional ByVal Member_Name As String = "", Optional ByVal Membership_ID As String = "", Optional ByVal Membership_Old_ID As String = "", Optional ByVal Member_Type As String = "") As DataTable
            Dim Param As Param_Get_MagazineMembershipList = New Param_Get_MagazineMembershipList
            Param.Member_Name = Member_Name
            Param.Membership_ID = Membership_ID
            Param.Membership_Old_ID = Membership_Old_ID
            Param.MemberType = Member_Type
            Param.PrevYearID = cBase._prev_Unaudited_YearID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_Membership, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetList_ConnectedMembership(Optional ByVal Member_Type As String = "") As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_ConnectedMembership, ClientScreen.Profile_Magazine, Member_Type)
        End Function

        Public Function GetList_RelatedMembership(Optional ByVal Member_Name As String = "", Optional ByVal Membership_ID As String = "", Optional ByVal Membership_Old_ID As String = "", Optional ByVal Member_Type As String = "", Optional ByVal RelatedMemberRecID As String = "") As DataSet
            Dim Param As Param_Get_MagazineMembershipList = New Param_Get_MagazineMembershipList
            Param.Member_Name = Member_Name
            Param.Membership_ID = Membership_ID
            Param.Membership_Old_ID = Membership_Old_ID
            Param.MemberType = Member_Type
            Param.RelatedMemberRecID = RelatedMemberRecID
            Param.PrevYearID = cBase._prev_Unaudited_YearID
            Return GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_RelatedMembership, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetList_Dispatches(IssueId As String) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_Dispatches, ClientScreen.Profile_Magazine, IssueId)
        End Function

        Public Function Get_Dispatch_Details(IssueId As String, Membersip_Rec_ID As String) As DataTable
            Dim param As Parameter_Get_Dispatch_Details = New Parameter_Get_Dispatch_Details
            param.Issue_ID = IssueId
            param.Membership_Rec_ID = Membersip_Rec_ID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_Get_Dispatch_Details, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetList_MagazineDispatchRegister(Optional Mem_ID As String = Nothing, Optional Mem_Old_ID As String = Nothing, Optional Issue_Date As String = Nothing, Optional Magazine As String = Nothing, Optional DispMemberID As String = Nothing, Optional CutOffTime As DateTime = Nothing) As DataSet
            Dim param As New Parameter_GetList_MagazineDispatchRegister
            param.Issue_Date = Issue_Date
            param.Magazine = Magazine
            param.Membership_ID = Mem_ID
            param.Membership_Old_ID = Mem_Old_ID
            param.Disp_Membership_ID = DispMemberID
            param.Prev_Year_Id = cBase._prev_Unaudited_YearID
            If Not CutOffTime = Nothing Then param.CutOffTime = CutOffTime
            Return GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_MagazineDispatchRegister, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetList_Issues() As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_Issues, ClientScreen.Profile_Magazine, "")
        End Function

        Public Function GetCount_Issues(FromDate As DateTime, ToDate As DateTime, MagID As String, Optional GetCurrYearIssueCountOnly As Boolean = False) As Int32
            Dim inParam As Param_GetIssueCount = New Param_GetIssueCount
            inParam.FromDate = FromDate
            inParam.ToDate = ToDate
            inParam.MagID = MagID
            inParam.GetCurrYearIssuesOnly = GetCurrYearIssueCountOnly
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetIssueCount, ClientScreen.Magazine_Receipt_Register, inParam)
        End Function

        Public Function GetList_Sub_Cities(Rec_id As String, Optional CityID As String = "") As DataTable
            Dim inparam As Param_GetList_SubCities = New Param_GetList_SubCities
            inparam.Rec_ID = Rec_id
            inparam.City_ID = CityID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_SubCities, ClientScreen.Profile_Magazine, inparam)
        End Function

        Public Function GetMapping_SubCities(Search As String, Optional StateID As String = Nothing) As DataTable
            Dim param As Param_GetMapping_SubCities = New Param_GetMapping_SubCities
            param.searchString = Search
            param.stateID = StateID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetMapping_SubCities, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetNewMembershipNo(ByVal Mag_ID As String) As Object
            Dim get_no As Object = 0 : get_no = GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetNewMembershipNo, ClientScreen.Profile_Magazine, Mag_ID)
            Return get_no
        End Function

        Public Function GetList_ReceiptRegister(ByVal FROM_DATE As DateTime, ByVal TO_DATE As DateTime, ByVal YR_START_DATE As DateTime, Optional TR_M_ID As String = Nothing) As DataSet
            Dim Param As Param_get_MagazineMembershipRegister = New Param_get_MagazineMembershipRegister
            Param.FROM_DATE = FROM_DATE
            Param.TO_DATE = TO_DATE
            Param.YR_START_DATE = YR_START_DATE
            Param.Tr_m_Id = TR_M_ID
            Param.Prev_Year_Id = cBase._prev_Unaudited_YearID
            Return GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_ReceiptRegister, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetMembers(ByVal SearchStr As String, ByVal MemberType As Integer, Optional ByVal Use_Rec_ID As Boolean = False, Optional ByVal Member_Rec_Id As String = Nothing) As DataTable
            Dim Param As Param_GetMembers_Magazine = New Param_GetMembers_Magazine()
            Param.SearchStr = SearchStr
            Param.MemberType = MemberType
            Param.Use_Rec_ID = Use_Rec_ID
            Param.Member_Rec_Id = Member_Rec_Id
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetMembers, ClientScreen.Profile_Magazine, Param)
        End Function

        Public Function GetExistingMembers(SearchString As String) As List(Of Return_Existing_Mag_Membership_List)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetExistingMembers, ClientScreen.Facility_Magazine_Request, SearchString)
            Dim _ExistingMembers As List(Of Return_Existing_Mag_Membership_List) = New List(Of Return_Existing_Mag_Membership_List)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New Return_Existing_Mag_Membership_List
                    newdata.MMB_ID = row.Field(Of String)("MMB_ID")
                    newdata.Membership_ID = row.Field(Of String)("Membership ID")
                    newdata.Start_Date = row.Field(Of DateTime)("Start Date")
                    newdata.MEM_NAME = row.Field(Of String)("MEM_NAME")
                    newdata.MEM_ID = row.Field(Of String)("MEM_ID")
                    newdata.COPIES = row.Field(Of Integer)("COPIES")
                    newdata.Curr_Due = row.Field(Of Decimal)("Curr. Due")
                    newdata.Curr_Advance = row.Field(Of Decimal)("Curr. Advance")
                    newdata.Period = row.Field(Of String)("Period")
                    newdata.Magazine = row.Field(Of String)("Magazine")
                    newdata.Membership_Old_ID = row.Field(Of String)("Membership Old ID")
                    newdata.MEM_ADDRESS = row.Field(Of String)("MEM_ADDRESS")
                    newdata.MagID = row.Field(Of String)("MagID")
                    newdata.MS_ID = row.Field(Of String)("MS_ID")
                    newdata.iREQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    newdata.iCOMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    newdata.iRESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    newdata.iREJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
                    newdata.iOTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    newdata.iALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")
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
                    _ExistingMembers.Add(newdata)
                Next
            End If
            Return _ExistingMembers
        End Function

        Public Function GetPublishOnList(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
            Return GetMisc("PUBLISH ON", ClientScreen.Profile_Magazine, NameColumnHead, RecIdColumnHead)
        End Function

        Public Function GetLanguagesList(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
            Return GetMisc("LANGUAGES", ClientScreen.Profile_Magazine, NameColumnHead, RecIdColumnHead)
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_INFO, "MI_CEN_ID")
        End Function

        Public Function GetStatus_Membership(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_MEMBERSHIP_INFO, "MM_CEN_ID")
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_INFO, Common.ClientDBFolderCode.CORE)
        End Function

        Public Function GetRecord_Subs_Type(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_SUBS_TYPE, Common.ClientDBFolderCode.CORE)
        End Function

        Public Function GetRecord_Subs_Type_Fee(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_SUBS_TYPE_FEE, Common.ClientDBFolderCode.CORE)
        End Function

        Public Function GetRecord_Dispatch_Type(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_DISPATCH_TYPE, Common.ClientDBFolderCode.CORE)
        End Function

        Public Function GetRecord_Dispatch_Type_Charges(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES, Common.ClientDBFolderCode.CORE)
        End Function

        Public Function GetRecord_GeetaPathshala(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_GP_INFO, Common.ClientDBFolderCode.CORE)
        End Function

        Public Function GetRecord_Membership(ByVal Rec_ID As String) As DataTable
            '  Return GetRecordByID(Rec_ID, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_MEMBERSHIP_INFO, Common.ClientDBFolderCode.CORE)
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetRecord_MembershipProfile, ClientScreen.Profile_Magazine, Rec_ID)
        End Function

        Public Function GetMagazineCountByName(Name As String, Optional RecID As String = "") As Object
            Dim param As param_GetMagazineCountByName = New param_GetMagazineCountByName
            param.Name = Name
            param.REC_ID = RecID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetMagazineCountByName, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetMagazineCountByShortName(ShortName As String, Optional RecID As String = "") As Object
            Dim param As param_GetMagazineCountByShortName = New param_GetMagazineCountByShortName
            param.ShortName = ShortName
            param.REC_ID = RecID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetMagazineCountByShortName, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetMagazineSubsTypeCountByName(Name As String, MagazineID As String, Optional RecID As String = "") As Object
            Dim param As param_GetMagazineSubTypeCountByName = New param_GetMagazineSubTypeCountByName
            param.Name = Name
            param.REC_ID = RecID
            param.Magazine_ID = MagazineID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetMagazineSubsTypeCountByName, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetMagazineSubsTypeCountByShortName(shortName As String, MagazineID As String, Optional RecID As String = "") As Object
            Dim param As param_GetMagazineSubTypeCountByShortName = New param_GetMagazineSubTypeCountByShortName
            param.ShortName = shortName
            param.REC_ID = RecID
            param.Magazine_ID = MagazineID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetMagazineSubsTypeCountByShortName, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetMagazineSubsFeeCountByEffDate(EffectiveDate As String, SubsType_ID As String, Optional RecID As String = "") As Object
            Dim param As param_GetMagazineSubFeeCountByEffDate = New param_GetMagazineSubFeeCountByEffDate
            param.EffDate = EffectiveDate
            param.REC_ID = RecID
            param.SubsType_ID = SubsType_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetMagazineSubsFeeCountByEffDate, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetMagazineDispatchCountByName(Name As String, Optional RecID As String = "") As Object
            Dim param As param_GetMagazineDispatchCountByName = New param_GetMagazineDispatchCountByName
            param.Name = Name
            param.REC_ID = RecID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetMagazineDispatchCountByName, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetMagazineDispatchFeeCountByEffDate(EffectiveDate As String, DispatchType_ID As String, Optional RecID As String = "") As Object
            Dim param As param_GetMagazineDispatchFeeCountByEffDate = New param_GetMagazineDispatchFeeCountByEffDate
            param.EffDate = EffectiveDate
            param.REC_ID = RecID
            param.DispatchType_ID = DispatchType_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetMagazineDispatchFeeCountByEffDate, ClientScreen.Profile_Magazine, param)
        End Function

        Public Function GetMembershipCountByMemberID(MemberID As String, Magazine_ID As String, Optional RecID As String = "") As Object
            Dim param As param_GetMembershipCountByMemberID = New param_GetMembershipCountByMemberID
            param.MemberID = MemberID
            param.REC_ID = RecID
            param.Magazine_ID = Magazine_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetMembershipCountByMemberID, ClientScreen.Profile_Magazine, param)
        End Function

        ''' <summary>
        ''' Gets Last Entry Date, Shifted
        ''' </summary>
        ''' <param name="Mem_Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetLastEntryDate</remarks>
        Public Function GetLastEntryDate(ByVal Mem_Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetLastEntryDate, ClientScreen.Profile_Magazine, Mem_Rec_Id)
        End Function

        ''' <summary>
        ''' Gets Address for RecID
        ''' </summary>
        ''' <param name="Mem_Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetLastEntryDate</remarks>
        Public Function GetMagazineAddress(ByVal Rec_Id As String) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_Address_Magazine, ClientScreen.Profile_Magazine, Rec_Id)
        End Function

        Public Function GetList_Dues(ByVal PrevYearID As Integer) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetDues, ClientScreen.Profile_Magazine, PrevYearID)
        End Function

        Public Function GetMagazineRestrictionDate(userID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Magazine_GetRestriction_Date, ClientScreen.Profile_Magazine, userID)
        End Function

        Public Function GetMagazinesIssues(MagID As String) As Object
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Magazine_GetMagazinesIssues, ClientScreen.Magazine_Receipt_Register, MagID)
        End Function

        Public Function Insert(ByVal InParam As Parameter_Insert_Magazine) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_Subs_Type(ByVal InParam As Parameter_Insert_Magazine_Subs_Type) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Subs_Type, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_Subs_Type_Fee(ByVal InParam As Parameter_Insert_Magazine_Subs_Type_Fee) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Subs_Type_Fee, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_Dispatch_Type(ByVal InParam As Parameter_Insert_Magazine_Dispatch_Type) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Dispatch_Type, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_Dispatch_Type_Charges(ByVal InParam As Parameter_Insert_Magazine_Dispatch_Type_Charges) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Dispatch_Type_Charges, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_Membership(ByVal InParam As Parameter_Insert_Magazine_Membership_Profile) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Membership, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_SubCity(ByVal InParam As Parameter_Insert_Magazine_Subcity) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_SubCity, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_Dispatch(ByVal InParam As Parameter_Insert_Magazine_Dispatch) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Dispatch, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_Dispatch_Bundles(ByVal InParam As Parameter_Insert_Magazine_Dispatch_Bundles) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Dispatch_bundles, ClientScreen.Magazine_Receipt_Register, InParam)
        End Function

        Public Function Insert_Magazine_Dispatch_New_Voucher(ByVal InParam As Parameter_Insert_dispatch_New_Voucher) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Dispatch_New_Voucher, ClientScreen.Magazine_Receipt_Register, InParam)
        End Function

        Public Function Insert_Magazine_Issue(ByVal InParam As Param_Insert_Magazine_Issue) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Magazine_Issue, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Insert_Magazine_Similar_Issue(ByVal InParam As Param_Insert_Magazine_Similar_Issues) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Magazine_Similar_Issues, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Update(ByVal UpParam As Parameter_Update_Magazine) As Boolean

            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Subs_Type(ByVal UpParam As Parameter_Update_Magazine_Subs_Type) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Subs_Type, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Subs_Type_Fee(ByVal UpParam As Parameter_Update_Magazine_Subs_Type_Fee) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Subs_Type_Fee, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Dispatch_Type(ByVal UpParam As Parameter_Update_Magazine_Dispatch_Type) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Dispatch_Type, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Dispatch_Type_Charges(ByVal UpParam As Parameter_Update_Magazine_Dispatch_Type_Charges) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Dispatch_Type_Charges, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Magazine_Membership(ByVal UpParam As Parameter_Update_Magazine_Membership_Profile) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Magazine_Receipt_Register)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Membership, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Magazine_Membership_Identity(ByVal UpParam As Parameter_Update_Magazine_Membership_Identity) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Membership_Identity, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Magazine_SubCity(ByVal UpParam As Parameter_Update_Magazine_Subcity) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_SubCity, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Magazine_Dispatch(ByVal UpParam As Parameter_Update_Magazine_Dispatch) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Dispatch, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Function Update_Magazine_Issues(ByVal UpParam As Param_Update_Magazine_Issue) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Magazine_Issue, ClientScreen.Profile_Magazine, UpParam)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Return DeleteRecord(Rec_Id, Tables.MAGAZINE_INFO, ClientScreen.Profile_Magazine)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Magazine_Delete_Magazine, ClientScreen.Profile_Magazine, Rec_Id)
        End Function

        Public Overloads Function Delete_Subs_Type(ByVal Rec_Id As String) As Boolean
            'Return DeleteRecord(Rec_Id, Tables.MAGAZINE_SUBS_TYPE, ClientScreen.Profile_Magazine)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Magazine_Delete_Subscription_Type, ClientScreen.Profile_Magazine, Rec_Id)
        End Function

        Public Overloads Function Delete_Subs_Type_Fee(ByVal Rec_Id As String) As Boolean
            'Return DeleteRecord(Rec_Id, Tables.MAGAZINE_SUBS_TYPE_FEE, ClientScreen.Profile_Magazine)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Magazine_Delete_Subscription_Type_Fee, ClientScreen.Profile_Magazine, Rec_Id)
        End Function

        Public Overloads Function Delete_Dispatch_Type(ByVal Rec_Id As String) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Magazine_Delete_Dispatch_Type, ClientScreen.Profile_Magazine, Rec_Id)
            'Return DeleteRecord(Rec_Id, Tables.MAGAZINE_DISPATCH_TYPE, ClientScreen.Profile_Magazine)
        End Function

        Public Overloads Function Delete_Dispatch_Type_Charges(ByVal Rec_Id As String) As Boolean
            'Return DeleteRecord(Rec_Id, Tables.MAGAZINE_DISPATCH_TYPE_CHARGES, ClientScreen.Profile_Magazine)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Magazine_Delete_Dispatch_Type_Charges, ClientScreen.Profile_Magazine, Rec_Id)
        End Function

        Public Overloads Function Delete_Membership(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Magazine_Receipt_Register)
            'Return DeleteRecord(Rec_Id, Tables.MAGAZINE_MEMBERSHIP_INFO, ClientScreen.Profile_Magazine)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Magazine_Delete_Magazine_Membership_Profile, ClientScreen.Profile_Magazine, Rec_Id)
        End Function

        Public Overloads Function Delete_Magazine_SubCity(ByVal Rec_Id As String) As Boolean
            'Return DeleteRecord(Rec_Id, Tables.MAGAZINE_MEMBERSHIP_INFO, ClientScreen.Profile_Magazine)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Delete_SubCity, ClientScreen.Profile_Magazine, Rec_Id)
        End Function

        Public Overloads Function Delete_Magazine_dispatch(ByVal Rec_Id As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Delete_Dispatch, ClientScreen.Profile_Magazine, Rec_Id)
        End Function

        Public Overloads Function Delete_Magazine_Issues(ByVal Rec_Id As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Delete_Issues, ClientScreen.Profile_Magazine, Rec_Id)
        End Function


        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Cls"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Close</remarks>
        Public Function Close(ByVal Cls As Parameter_Close_Membership) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Membership_Close, ClientScreen.Profile_Membership, Cls)
        End Function

        ''' <summary>
        ''' Reopens membership, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Reopen</remarks>
        Public Function Reopen(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Membership_Reopen, ClientScreen.Profile_Membership, Rec_ID)
        End Function

        Public Function ConsiderForAutoRenewal(ByVal Consider As Boolean, ByVal Rec_ID As String) As Boolean
            Dim param As New Parameter_AutoRenewal_Membership
            param.ConsiderForAutoRenewal = Consider
            param.Rec_ID = Rec_ID
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Consider_ForAutoRenewal, ClientScreen.Magazine_Receipt_Register, param)
        End Function

        Public Function Set_Default_Magazine_Subscription(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Subscription_Set_Default, ClientScreen.Profile_Magazine, Rec_ID)
        End Function

        Public Function Remove_Default_Magazine_Subscription(MagID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Subscription_Remove_Default, ClientScreen.Profile_Magazine, MagID)
        End Function

        Public Function Set_Default_Magazine_Dispatch(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Dispatch_Set_Default, ClientScreen.Profile_Magazine, Rec_ID)
        End Function

        Public Function Remove_Default_Magazine_Dispatch() As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Dispatch_Remove_Default, ClientScreen.Profile_Magazine, Nothing)
        End Function

        Public Function Set_Default_Magazine_Issue(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Issue_Set_Default, ClientScreen.Profile_Magazine, Rec_ID)
        End Function

        Public Function Remove_Default_Magazine_Issue() As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Issue_Remove_Default, ClientScreen.Profile_Magazine, Nothing)
        End Function

        Public Function Add_Magazine_Restriction(ByVal RestrictedTill As DateTime, ByVal Userid As String) As Boolean
            Dim InParam As Parameter_Insert_Magazine_Client_Restriction = New Parameter_Insert_Magazine_Client_Restriction
            InParam.RestrictedTill = RestrictedTill
            InParam.UserID = Userid
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Add_Client_Restrictions, ClientScreen.Magazine_Receipt_Register, InParam)
        End Function

        Public Function Settle_Magazine_Ledgers() As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Magazine_Settle_Magazine_Ledgers, ClientScreen.Profile_Magazine, Nothing)
        End Function
    End Class
#End Region
#Region "--Voucher--"
    <Serializable>
    Public Class Voucher_Magazine
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetMembers(Param As Parameter_GetMembers_VoucherMagazine) As DataTable
            Return GetListOfRecordsBySP(RealServiceFunctions.Voucher_Magazine_Register_GetMembers, ClientScreen.Magazine_Receipt_Register, Param)
        End Function

        Public Function GetMagazineAccLedger(Optional Payee_AB_ID As String = Nothing, Optional LedgerID As String = Nothing) As DataTable
            Dim inparam As New Param_GetMagazineAccLedger
            inparam.Prev_year_ID = cBase._prev_Unaudited_YearID
            inparam.Payee_AB_ID = Payee_AB_ID
            inparam.LedgerID = LedgerID
            Return GetListOfRecordsBySP(RealServiceFunctions.Magazine_GetMagazineAccLedger, ClientScreen.Magazine_Receipt_Register, inparam)
        End Function

        Public Function GetPayeeLedger(MS_ID As String) As DataTable
            Dim inparam As New Param_GetPayeeLedger
            inparam.MS_ID = MS_ID
            inparam.Prev_year_ID = cBase._prev_Unaudited_YearID
            Return GetListOfRecordsBySP(RealServiceFunctions.Magazine_GetPayeeLedger, ClientScreen.Magazine_Receipt_Register, inparam)
        End Function

        Public Function GetVoucherDetails_OnMemberSelection(Param As Parameter_GetVoucherDetails_OnMemberSelection) As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Voucher_Magazine_Register_GetVoucherDetailsOnMemberSelection, ClientScreen.Magazine_Receipt_Register, Param)
        End Function

        Public Function GetReceiptCount(ByVal M_ID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Voucher_Magazine_Register_GetReceiptCount, ClientScreen.Magazine_Receipt_Register, M_ID)
        End Function

        Public Function GetCancelledReceipts() As DataTable
            Return GetListOfRecordsBySP(RealServiceFunctions.Magazine_GetCancelledReceipts, ClientScreen.Magazine_Receipt_Register, Nothing)
        End Function

        ''' <summary>
        ''' Get Discontinued, Shifted
        ''' </summary>
        ''' <param name="ByMasterID"></param>
        ''' <param name="Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetDiscontinued</remarks>
        Public Function GetDiscontinued(ByVal ByMasterID As Boolean, ByVal Id As String) As Object
            Dim Param As Param_GetDiscontinued_Membership = New Param_GetDiscontinued_Membership()
            Param.ByMasterID = ByMasterID
            Param.Id = Id
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Voucher_Magazine_Register_GetDiscontinued, ClientScreen.Magazine_Receipt_Register, Param)
        End Function

        Public Function InsertMagazineMembership_Txn(Param As Param_Txn_Insert_VoucherMagazineMembership) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(Param.Param_Update_Memebership_Balances.REC_ID, ClientScreen.Magazine_Receipt_Register)
            Return ExecuteGroup(RealServiceFunctions.Voucher_Magazine_Register_InsertMagazine_Txn, ClientScreen.Magazine_Receipt_Register, Param)
        End Function

        Public Function InsertMagazineMembership_Receipt(Tr_M_ID As String, MemDate As DateTime, VoucherDate As DateTime, MEMBER_ID As String, MEMBER_TYPE As String) As Boolean
            Dim paramRec As Param_InsertReceipt_Magazine_Receipt_Register = New Param_InsertReceipt_Magazine_Receipt_Register
            paramRec.M_ID = Tr_M_ID
            paramRec.MDate = Convert.ToDateTime(MemDate).ToString(cBase._Server_Date_Format_Short)
            paramRec.VDate = Convert.ToDateTime(VoucherDate).ToString(cBase._Server_Date_Format_Short)
            paramRec.openUserID = cBase._open_User_ID
            paramRec.openYearSdt = cBase._open_Year_Sdt
            paramRec.MEMBER_ID = MEMBER_ID
            paramRec.MEMBER_TYPE = MEMBER_TYPE
            Return InsertRecord(RealServiceFunctions.Voucher_Magazine_Register_InsertReceipt, ClientScreen.Magazine_Receipt_Register, paramRec)
        End Function

        Public Function DeleteMagazineMembershipVoucher(MId As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(MId, ClientScreen.Magazine_Receipt_Register)
            Return DeleteRecord(MId, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Magazine_Receipt_Register)
        End Function

        ''' <summary>
        ''' Delete Receipt, Shifted
        ''' </summary>
        ''' <param name="Reason"></param>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_DeleteReceipt</remarks>
        Public Overloads Function DeleteReceipt(ByVal Reason As String, ByVal Rec_Id As String) As Boolean
            Dim Param As Param_DeleteReceipt_Magazine_Receipt_Register = New Param_DeleteReceipt_Magazine_Receipt_Register()
            Param.Reason = Reason
            Param.Rec_Id = Rec_Id
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Voucher_Magazine_Register_DeleteReceipt, ClientScreen.Magazine_Receipt_Register, Param)
        End Function

        Public Function Delete_All_Magazine_dispatches(ByVal MM_MS_ID As String) As Boolean
            Return DeleteByCondition(" MDI_MS_ID  IN (SELECT REC_ID FROM MAGAZINE_MEMBERSHIP_INFO WHERE MM_MS_ID ='" & MM_MS_ID & "') ", Tables.MAGAZINE_DISPATCH_INFO, ClientScreen.Magazine_Receipt_Register)
        End Function

        Public Function Delete_All_Issue_dispatches(ByVal Issue_Date As DateTime, MagazineID As String) As Boolean
            DeleteByCondition(" MDI_STATUS ='DELIVERED' AND MDI_MDT_ID NOT IN (SELECT REC_ID FROM magazine_dispatch_type WHERE MDT_NAME ='By Hand') AND MDI_MII_ID  IN (SELECT REC_ID FROM MAGAZINE_ISSUE_INFO WHERE MII_ISSUE_DATE ='" & Issue_Date.ToString(cBase._Server_Date_Format_Long) & "' AND MII_MI_ID = '" & MagazineID & "') ", Tables.MAGAZINE_DISPATCH_INFO, ClientScreen.Magazine_Receipt_Register)
            DeleteByCondition(" MDB_MII_ID  IN (SELECT REC_ID FROM MAGAZINE_ISSUE_INFO WHERE MII_ISSUE_DATE ='" & Issue_Date.ToString(cBase._Server_Date_Format_Long) & "' AND MII_MI_ID = '" & MagazineID & "') ", Tables.MAGAZINE_DISPATCH_BUNDLES, ClientScreen.Magazine_Receipt_Register)
            Return True
        End Function

        Public Function Update_Magazine_Disp_CC(Mmb_Rec_ID As String, dispOnCC As Boolean) As Boolean
            Dim Param As New Param_Update_Magazine_Disp_CC
            Param.DispOnCC = dispOnCC
            Param.Membership_Balances_ID = Mmb_Rec_ID
            Return UpdateRecord(RealServiceFunctions.Voucher_Magazine_Update_Magazine_Disp_CC, ClientScreen.Magazine_Receipt_Register, Param)
        End Function
    End Class
#End Region
    <Serializable>
    Public Class Magazine_Membership_Requests
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetList_Requests() As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetList_MembershipRequests, ClientScreen.Profile_Magazine, Nothing)
        End Function

        Public Function Insert_Magazine_Membership_Request(ByVal InParam As Parameter_Insert_Magazine_Membership_Request) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Magazine_Insert_Membership_Request, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Update_Magazine_Membership_Request(ByVal InParam As Parameter_Update_Magazine_Membership_Request) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Membership_Request, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function Update_Magazine_Request_Status(ByVal InParam As Parameter_Update_Magazine_Request_status) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Magazine_Update_Request_Status, ClientScreen.Profile_Magazine, InParam)
        End Function

        Public Function GetRecord_Request(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Magazine, RealTimeService.Tables.MAGAZINE_MEMBERSHIP_REQUEST_INFO, Common.ClientDBFolderCode.SYS)
        End Function

        Public Function DeleteMagazineRequest(RecID As String) As Boolean
            Return ExecuteGroup(RealServiceFunctions.Magazine_Delete_Membership_Request, ClientScreen.Profile_Magazine, RecID)
        End Function
    End Class
    <Serializable>
    Public Class Magazine_Reports
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetIncome_Breakup(ReportType As String) As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetIncome_Breakup, ClientScreen.Profile_Magazine, ReportType)
        End Function

        Public Function GetParty_Balances() As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetParty_Balances, ClientScreen.Profile_Magazine, Nothing)
        End Function

        Public Function GetOpeningAccountingBalances_Bkp() As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_GetOpeningAccountingBalances_Bkp, ClientScreen.Profile_Magazine, Nothing)
        End Function

        Public Function GetMagazine_Receivables_Ledger() As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_Receivables_Ledger, ClientScreen.Profile_Magazine, Nothing)
        End Function

        Public Function GetMagazine_Advances_Ledger() As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Magazine_Advances_Ledger, ClientScreen.Profile_Magazine, Nothing)
        End Function
    End Class
End Class
