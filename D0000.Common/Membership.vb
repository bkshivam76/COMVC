'SQL, Shifted
Imports Common_Lib.RealTimeService

Partial Public Class DbOperations
    <Serializable>
    Public Class Parameter_InsertBalances_VoucherMembership
        Public REC_ID As String
        Public Sr_No As Integer
        Public Subs_ID As String
        Public Item_ID As String
        Public Entry_Date As String
        Public Period_From As String
        Public Period_To As String
        Public Amount As Double
        Public Status_Action As String
        Public TxnID As String
    End Class
    <Serializable>
    Public Class Parameter_InsertMembership_VoucherMembership
        Public AB_ID As String
        Public SUBS_ID As String
        Public Wing_Id As String
        Public StartDate As String
        Public Mem_Old_No As String
        Public Mem_No As String
        Public OtherDetails As String
        Public Status_Action As String
        Public Rec_ID As String
        Public TxnID As String
    End Class
    <Serializable>
    Public Class Parameter_InsertMembership_VoucherMembershipRenewal
        Public AB_ID As String
        Public SUBS_ID As String
        Public Wing_Id As String
        Public StartDate As String
        Public Mem_Old_No As String
        Public Mem_No As String
        Public OtherDetails As String
        Public Status_Action As String
        Public Rec_ID As String
        Public TxnID As String
    End Class
    <Serializable>
    Public Class Parameter_InsertBalances_VoucherMembershipRenewal
        Public REC_ID As String
        Public Sr_No As Integer
        Public Subs_ID As String
        Public Item_ID As String
        Public Entry_Date As String
        Public Period_From As String
        Public Period_To As String
        Public Amount As Double
        Public Status_Action As String
        Public TxnID As String
    End Class

#Region "--Profile--"
    <Serializable>
    Public Class Membership
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

#Region "Parameter Classes"
        <Serializable>
        Public Class Return_ExistingWingMembership
            Inherits CommonReturnFields
            ''' <summary>
            ''' Actual Field name is Member Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Member_Name As String
            ''' <summary>
            ''' Actual Field name is Membership No.
            ''' </summary>
            ''' <returns></returns>
            Public Property Membership_No As String
            Public Property Membership As String
            Public Property Wing As String
            ''' <summary>
            ''' Original Field name is Original Start Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Original_Start_Date As DateTime
            ''' <summary>
            ''' Original Field name is Start Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Start_Date As DateTime
            ''' <summary>
            ''' Original Field name is End Date
            ''' </summary>
            ''' <returns></returns>
            Public Property End_Date As DateTime?
            ''' <summary>
            ''' Actual Field name is Centre Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Centre_Name As String
            ''' <summary>
            ''' Actual Field name is Centre UID
            ''' </summary>
            ''' <returns></returns>
            Public Property Centre_UID As String
            Public Property Wing_ID As String
            Public Property Status As String
            ''' <summary>
            ''' Actual Field name is Discontinued From
            ''' </summary>
            ''' <returns></returns>
            Public Property Discontinued_From As DateTime?
            ''' <summary>
            ''' Actual Field name is Reason of Discontinued
            ''' </summary>
            ''' <returns></returns>
            Public Property Reason_of_Discontinued As String
            Public Property ADVANCE As Decimal?
            Public Property Arrear As Decimal?
            ''' <summary>
            ''' Actual Field name is Other Detail
            ''' </summary>
            ''' <returns></returns>
            Public Property Other_Detail As String
            ''' <summary>
            ''' actual field name is Member ID
            ''' </summary>
            ''' <returns></returns>
            Public Property Member_ID As String
            Public Property TR_ID As String
            ''' <summary>
            ''' Actual field name is Entry Type
            ''' </summary>
            ''' <returns></returns>
            Public Property Entry_Type As String
            Public Property YearID As Integer
            Public Property MemberID As String
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
            Public Property MembershipGridPk As String
        End Class
        <Serializable>
        Public Class Return_ExistingWingMembership_Balances
            ''' <summary>
            ''' Actual Field name is Entry Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Entry_Date As DateTime
            ''' <summary>
            ''' Actual Field name is Mem. No.
            ''' </summary>
            ''' <returns></returns>
            Public Property Mem_No As String
            Public Property Membership As String
            Public Property Description As String
            ''' <summary>
            ''' Actual Field name is Period From
            ''' </summary>
            ''' <returns></returns>
            Public Property Period_From As DateTime?
            ''' <summary>
            ''' Actual Field name is Period To
            ''' </summary>
            ''' <returns></returns>
            Public Property Period_To As DateTime?
            Public Property Amount As Decimal
            Public Property MS_ID As String
            Public Property pKey As String
        End Class
#End Region

        ''' <summary>
        ''' Get Wings, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetWings</remarks>
        Public Function GetWings() As DataTable
            Dim Query As String = "select WING_NAME,WING_SHORT_MS,REC_ID AS WING_REC_ID FROM WINGS_INFO WHERE  REC_STATUS IN (0,1,2)  ORDER BY WING_NAME "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Membership_GetWings, Query, ClientScreen.Profile_Membership, Tables.WINGS_INFO)
        End Function

        ''' <summary>
        ''' Get Subscription List, Shifted
        ''' </summary>
        ''' <param name="InsID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetSubscriptionList</remarks>
        Public Function GetSubscriptionList(ByVal InsID As String) As DataTable
            Dim Query As String = "select SI_NAME,SI_CATEGORY,SI_START_MONTH,SI_TOTAL_MONTH,REC_ID AS SI_REC_ID FROM SUBSCRIPTION_INFO WHERE  REC_STATUS IN (0,1,2) AND INS_ID = '" & InsID & "'  ORDER BY SI_SRNO "
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Membership_GetSubscriptionList, Query, ClientScreen.Profile_Membership, Tables.SUBSCRIPTION_INFO, InsID)
        End Function

        ''' <summary>
        ''' Get Subscription Fee, Shifted
        ''' </summary>
        ''' <param name="SubsID"></param>
        ''' <param name="StartDate"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetSubscriptionFee</remarks>
        Public Function GetSubscriptionFee(ByVal SubsID As String, ByVal StartDate As Date, Optional UseSubID As Boolean = True, Optional Subscription As String = "") As DataTable
            Dim localQuery As String = " select TOP 1 SI_REC_ID,SF_ENT_FEE,SF_SUBS_FEE,SF_RENEW_FEE,SF_EFF_DATE,REC_ID AS SF_REC_ID FROM SUBSCRIPTION_FEE_INFO WHERE  REC_STATUS IN (0,1,2) AND SI_REC_ID = '" & SubsID & "' AND SF_EFF_DATE  <= #" & Format(StartDate, cBase._Date_Format_Short) & "#  ORDER BY SF_EFF_DATE DESC "
            Dim Param As Param_GetSubscriptionFee_Membership = New Param_GetSubscriptionFee_Membership()
            Param.StartDate = StartDate
            Param.SubsID = SubsID
            Param.UseSubID = UseSubID
            Param.Subscription = Subscription
            Param.InsID = cBase._open_Ins_ID
            Return GetCoreListOfRecords(RealTimeService.RealServiceFunctions.Membership_GetSubscriptionFee, localQuery, ClientScreen.Profile_Membership, Tables.SUBSCRIPTION_FEE_INFO, Param)
        End Function

        ''' <summary>
        ''' Get Duplicate Count, Shifted
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="AB_ID"></param>
        ''' <param name="WING_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetDuplicateCount</remarks>
        Public Function GetDuplicateCount(ByVal Rec_Id As String, ByVal AB_ID As String, ByVal WING_ID As String) As Object
            Dim Param As Param_GetDuplicateCount_Membership = New Param_GetDuplicateCount_Membership()
            Param.Rec_Id = Rec_Id
            Param.AB_ID = AB_ID
            Param.WING_ID = WING_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetDuplicateCount, ClientScreen.Profile_Membership, Param)
        End Function

        ''' <summary>
        ''' Get Count For Continue, Shifted
        ''' </summary>
        ''' <param name="AB_ID"></param>
        ''' <param name="WING_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetCountForContinue</remarks>
        Public Function GetCountForContinue(ByVal AB_ID As String, ByVal WING_ID As String) As Object
            Dim Param As Param_GetCountForContinue_Membership = New Param_GetCountForContinue_Membership()
            Param.AB_ID = AB_ID
            Param.WING_ID = WING_ID
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetCountForContinue, ClientScreen.Profile_Membership, Param)
        End Function

        ''' <summary>
        ''' Get Duplicate OldNo Count, Shifted
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <param name="OLD_NO"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetDuplicateOldNoCount</remarks>
        Public Function GetDuplicateOldNoCount(ByVal Rec_Id As String, ByVal OLD_NO As String) As Object
            Dim Param As Param_GetDuplicateOldNoCount_Membership = New Param_GetDuplicateOldNoCount_Membership()
            Param.OLD_NO = OLD_NO
            Param.Rec_Id = Rec_Id
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetDuplicateOldNoCount, ClientScreen.Profile_Membership, Param)
        End Function

        ''' <summary>
        ''' Get Membership No, Shifted
        ''' </summary>
        ''' <param name="M_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetMembershipNo</remarks>
        Public Function GetMembershipNo(ByVal M_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetMembershipNo, ClientScreen.Profile_Membership, M_Id)
        End Function

        ''' <summary>
        ''' Get New Membership No, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetNewMembershipNo</remarks>
        Public Function GetNewMembershipNo() As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetNewMembershipNo, ClientScreen.Profile_Membership)
        End Function
        ''' <summary>
        ''' Get Last Period, Shifted
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetLastPeriod</remarks>
        Public Function GetLastPeriod(ByVal Rec_Id As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Membership_GetLastPeriod, ClientScreen.Profile_Membership, Rec_Id)
        End Function

        ''' <summary>
        ''' Gets Last Entry Date, Shifted
        ''' </summary>
        ''' <param name="Mem_Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetLastEntryDate</remarks>
        Public Function GetLastEntryDate(ByVal Mem_Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetLastEntryDate, ClientScreen.Profile_Membership, Mem_Rec_Id)
        End Function

        ''' <summary>
        ''' Get Last Transaction Date, Shifted
        ''' </summary>
        ''' <param name="Mem_Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetLastTransactionDate</remarks>
        Public Function GetLastTransactionDate(ByVal Mem_Rec_Id As String) As Object
            Dim onlineQuery As String = "SELECT MAX(TR_DATE) AS LAST_TRANS_DT FROM TRANSACTION_D_MASTER_INFO WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TR_CEN_ID='" & cBase._open_Cen_ID & "' AND TR_REF_ID = '" & Mem_Rec_Id & "' AND TR_CODE IN (12,13)"
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetLastTransactionDate, ClientScreen.Profile_Membership, Mem_Rec_Id)
        End Function

        ''' <summary>
        ''' Get Master Transaction List, Shifted
        ''' </summary>
        ''' <param name="ByMasterID"></param>
        ''' <param name="ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetMasterTransactionList</remarks>
        Public Function GetMasterTransactionList(ByVal ByMasterID As Boolean, ByVal ID As String) As DataTable
            Dim Param As Param_GetMasterTransactionList_Membership = New Param_GetMasterTransactionList_Membership()
            Param.ByMasterID = ByMasterID
            Param.ID = ID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Membership_GetMasterTransactionList, ClientScreen.Profile_Membership, Param)
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
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetDiscontinued, ClientScreen.Profile_Membership, Param)
        End Function

        'Shifted
        Public Function GetPartyDetails(ByVal SearchStr As String, Optional ByVal Use_Rec_ID As Boolean = False, Optional ByVal Member_Rec_Id As String = Nothing) As DataTable
            Dim SearchCondition As String = ""

            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
            Param.SearchCondition = SearchCondition
            Param.SearchStr = SearchStr
            Param.Use_Rec_ID = Use_Rec_ID
            Param.Member_Rec_Id = Member_Rec_Id
            Dim _AB As Addresses = New Addresses(cBase)
            Return _AB.GetList(ClientScreen.Profile_Membership, Param)
        End Function

        'Shifted
        Public Function GetAddressEditTime(ByVal Rec_ID As String, Optional Screen As ClientScreen = Nothing) As Object
            Dim _AB As Addresses = New Addresses(cBase)
            If Screen = Nothing Then Screen = ClientScreen.Profile_Membership
            Return _AB.GetLastEditTime(Rec_ID, Screen)
        End Function


        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <param name="VoucherEntry"></param>
        ''' <param name="ProfileEntry"></param>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetList</remarks>
        Public Function GetList(ByVal VoucherEntry As String, ByVal ProfileEntry As String, ByVal OtherCondition As String) As List(Of Return_ExistingWingMembership)
            'Dim onlineQuery As String = " SELECT AB.C_NAME AS 'Member Name',MS.MS_NO as 'Membership No.',SI.SI_NAME AS 'Membership',MS.MS_START_DATE as 'Start Date',CASE WHEN COALESCE(AB.C_CEN_CATEGORY,0) = 0 THEN C.CEN_NAME ELSE O.CEN_NAME END AS 'Centre Name', CASE WHEN COALESCE(AB.C_CEN_CATEGORY,0) = 0 THEN C.CEN_UID ELSE O.CEN_UID END AS 'Centre UID' ,MS.MS_WING_ID as 'Wing ID',WS.WING_NAME AS 'Wing',CASE WHEN MS.MS_CLOSE_DATE IS NULL THEN 'Continue' ELSE 'Discontinued' END AS Status,MS.MS_CLOSE_DATE as 'Discontinued From',MS.MS_CLOSE_REMARKS AS 'Reason of Discontinued',MS.MS_OTHER_DETAIL as 'Other Detail',MS.MS_OLD_NO AS 'Old Membership No.',MS.MS_AB_ID AS 'Member ID',COALESCE(MS.MS_TR_ID,'') AS TR_ID,MS.REC_ID AS ID ,CASE WHEN MS.MS_TR_ID IS NULL THEN '" & ProfileEntry & "' ELSE  '" & VoucherEntry & "' END as 'Entry Type' ," & cBase.Rec_Detail("MS", Common.DbConnectionMode.Online) & "" & _
            '                            " FROM       MEMBERSHIP_INFO      AS MS " & _
            '                            " INNER JOIN ADDRESS_BOOK         AS AB ON (MS.MS_AB_ID       = AB.C_ORG_REC_ID AND C_COD_YEAR_ID =" & cBase._open_Year_ID.ToString & " AND AB.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                            " INNER JOIN WINGS_INFO           AS WS ON (MS.MS_WING_ID     = WS.REC_ID AND WS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                            " INNER JOIN SUBSCRIPTION_INFO    AS SI ON (MS.MS_SI_ID       = SI.REC_ID AND SI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) " & _
            '                            " LEFT JOIN CENTRE_INFO           AS C  ON (AB.C_CLASS_CEN_ID = C.CEN_ID  AND AB.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
            '                            " LEFT JOIN OVERSEAS_CENTRE_INFO  AS O  ON (AB.C_CLASS_CEN_ID = O.CEN_ID  AND AB.C_CEN_CATEGORY = 1 AND O.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
            '                            " Where MS.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MS.MS_CEN_ID=" & cBase._open_Cen_ID.ToString & " " & OtherCondition & " ORDER BY MS.MS_NO "

            ''                           " LEFT JOIN MEMBERSHIP_ENDDATE    AS ME ON (MS.REC_ID         = ME.MS_REC_ID ) " & _

            Dim Param As Param_GetList_Membership = New Param_GetList_Membership()
            Param.VoucherEntry = VoucherEntry
            Param.ProfileEntry = ProfileEntry
            Param.OtherCondition = OtherCondition
            Dim ReturnTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Membership_GetList, ClientScreen.Profile_Membership, Param)
            Dim _members As List(Of Return_ExistingWingMembership) = New List(Of Return_ExistingWingMembership)
            If (Not (ReturnTable) Is Nothing) Then
                For Each row As DataRow In ReturnTable.Rows
                    Dim newdata = New Return_ExistingWingMembership
                    newdata.Member_Name = row.Field(Of String)("Member Name")
                    newdata.Membership_No = row.Field(Of String)("Membership No.")
                    newdata.Membership = row.Field(Of String)("Membership")
                    newdata.Wing = row.Field(Of String)("Wing")
                    newdata.Original_Start_Date = row.Field(Of DateTime)("Original Start Date")
                    newdata.Start_Date = row.Field(Of DateTime)("Start Date")
                    newdata.End_Date = row.Field(Of DateTime?)("End Date")
                    newdata.Centre_Name = row.Field(Of String)("Centre Name")
                    newdata.Centre_UID = row.Field(Of String)("Centre UID")
                    newdata.Wing_ID = row.Field(Of String)("Wing ID")
                    newdata.Status = row.Field(Of String)("Status")
                    newdata.Discontinued_From = row.Field(Of DateTime?)("Discontinued From")
                    newdata.Reason_of_Discontinued = row.Field(Of String)("Reason of Discontinued")
                    newdata.ADVANCE = row.Field(Of Decimal?)("ADVANCE")
                    newdata.Arrear = row.Field(Of Decimal?)("Arrear")
                    newdata.Other_Detail = row.Field(Of String)("Other Detail")
                    newdata.Member_ID = row.Field(Of String)("Member ID")
                    newdata.TR_ID = row.Field(Of String)("TR_ID")
                    newdata.Entry_Type = row.Field(Of String)("Entry Type")
                    newdata.YearID = row.Field(Of Integer)("YearID")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.MemberID = row.Field(Of String)("ID")
                    newdata.Add_Date = row.Field(Of DateTime)("Add Date")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit Date")
                    newdata.Action_Date = row.Field(Of DateTime)("Action Date")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Status = row.Field(Of String)("Action Status")
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
                    newdata.MembershipGridPk = row.Field(Of String)("ID") & row.Field(Of String)("Wing ID")
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

                    _members.Add(newdata)
                Next
            End If
            Return _members
        End Function

        ''' <summary>
        ''' Get Balances List, Shifted
        ''' </summary>
        ''' <param name="OtherCondition"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetBalancesList</remarks>
        Public Function GetBalancesList(ByVal OtherCondition As String) As List(Of Return_ExistingWingMembership_Balances)
            Dim param As Param_GetBalancesList = New Param_GetBalancesList
            param.OtherCondition = OtherCondition
            param.YearEndDate = cBase._open_Year_Edt
            Dim ReturnTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Membership_GetBalancesList, ClientScreen.Profile_Membership, param)
            Dim _members_bal As List(Of Return_ExistingWingMembership_Balances) = New List(Of Return_ExistingWingMembership_Balances)
            If (Not (ReturnTable) Is Nothing) Then
                For Each row As DataRow In ReturnTable.Rows
                    Dim newdata = New Return_ExistingWingMembership_Balances
                    newdata.Entry_Date = row.Field(Of DateTime)("Entry Date")
                    newdata.Mem_No = row.Field(Of String)("Mem. No.")
                    newdata.Membership = row.Field(Of String)("Membership")
                    newdata.Description = row.Field(Of String)("Description")
                    newdata.Period_From = row.Field(Of DateTime?)("Period From")
                    newdata.Period_To = row.Field(Of DateTime?)("Period To")
                    newdata.Amount = row.Field(Of Decimal)("Amount")
                    newdata.MS_ID = row.Field(Of String)("ID")
                    newdata.pKey = Guid.NewGuid().ToString()
                    _members_bal.Add(newdata)
                Next
            End If
            Return _members_bal
        End Function

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Profile_Membership, RealTimeService.Tables.MEMBERSHIP_INFO, "MS_CEN_ID")
        End Function

        Public Function GetSubscriptionList_Master(ByVal UpParam As String) As DataTable
            Return GetListOfRecordsBySP(RealServiceFunctions.Membership_GetSubscritonList_Master, ClientScreen.Profile_Membership, UpParam, False)
        End Function

        Public Function GetSubscritonFeeList_Master(ByVal UpParam As String) As DataTable
            Return GetListOfRecordsBySP(RealServiceFunctions.Memebership_GetSubscritonFeeList_Master, ClientScreen.Profile_Membership, UpParam, False)
        End Function


        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Membership, RealTimeService.Tables.MEMBERSHIP_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Get Membership no if current address is used in membership 
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetUsageAsPastMember(ByVal Org_Rec_Id As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Membership_CheckUsageAsPastMember, ClientScreen.Profile_Membership, Org_Rec_Id)
        End Function


        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Insert</remarks> 
        Public Function Insert(ByVal InParam As Parameter_Insert_Membership) As Boolean
            'InParam.openYearID = cBase._open_Year_ID   ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Membership_Insert, InParam.Screen, InParam)
        End Function
        Public Function InsertSubscriptionList(ByVal InParam As Param_Inset_SubcriptionList) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Membership_InsertSubscriptionList, ClientScreen.Profile_Membership, InParam)
        End Function

        Public Function InsertSubscriptionFee(ByVal InParam As Param_Insert_SubscriptionFee) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.Membership_InsertSubscriptionFee, ClientScreen.Profile_Membership, InParam)
        End Function

        ''' <summary>
        ''' Insert Balances, Shifted
        ''' </summary>
        ''' <param name="InBal"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_InsertBalances</remarks>
        Public Function InsertBalances(ByVal InBal As Parameter_InsertBalances_Membership) As Boolean
            'InBal.openYearID = cBase._open_Year_ID   ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.Membership_InsertBalances, InBal.Screen, InBal)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_Membership) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.Rec_ID, ClientScreen.Profile_Membership)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Membership_Update, ClientScreen.Profile_Membership, UpParam)
        End Function

        ''' <summary>
        ''' Updates Subscription Type Master, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_UpdateSubscriptionList</remarks>
        Public Function UpdateSubscriptionList(ByVal UpParam As Param_Update_SubscirptionList) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Membership_UpdateSubscriptionList, ClientScreen.Profile_Membership, UpParam)
        End Function

        Public Function UpdateSubscriptionFee(ByVal UpParam As Param_Update_SubscriptionFee) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Membership_UpdateSubscriptionFee, ClientScreen.Profile_Membership, UpParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Cls"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Close</remarks>
        Public Function Close(ByVal Cls As Parameter_Close_Membership) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Membership_Close, ClientScreen.Profile_Membership, Cls)
        End Function

        ''' <summary>
        ''' Reopens membership, Shifted
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Reopen</remarks>
        Public Function Reopen(ByVal Rec_ID As String) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.Membership_Reopen, ClientScreen.Profile_Membership, Rec_ID)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Membership)

            Return DeleteRecord(Rec_Id, Tables.MEMBERSHIP_INFO, ClientScreen.Profile_Membership)
        End Function

        Public Overloads Function DeleteBalances(ByVal Rec_Id As String) As Boolean
            Return DeleteByCondition("MS_REC_ID    ='" & Rec_Id & "'", Tables.MEMBERSHIP_BALANCES_INFO, ClientScreen.Profile_Membership)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String, ByVal Screen As ClientScreen) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & Rec_Id & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Membership)

            Return DeleteByCondition("MS_TR_ID    ='" & Rec_Id & "'", Tables.MEMBERSHIP_INFO, Screen)
        End Function

        Public Overloads Function DeleteSubscriptionList(ByVal Param As String) As Boolean
            Return Delete_SO_Table_Record(RealServiceFunctions.Membership_DeleteSubscriptionList, ClientScreen.Profile_Membership, Param)
        End Function

        Public Overloads Function DeleteSubscriptionFee(ByVal Param As Param_Delet_SubscriptionFee) As Boolean
            'Return DeleteRecord(Rec_Id, Tables.SUBSCRIPTION_INFO, ClientScreen.Profile_Membership)
            Return Delete_SO_Table_Record(RealServiceFunctions.Membership_DeleteSubscriptionFee, ClientScreen.Profile_Membership, Param)
        End Function

        Public Overloads Function MarkAsComplete(ByVal Rec_Id As String) As Boolean
            Return MyBase.MarkAsComplete(Rec_Id, Tables.MEMBERSHIP_INFO, ClientScreen.Profile_Membership)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.ASSET_INFO, ClientScreen.Profile_Membership)
            Return Locked
        End Function

        Public Function InsertMembership_Txn(ByVal InParam As Param_Txn_Insert_Membership) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Membership_InsertMembership_Txn, ClientScreen.Profile_Membership, InParam)
        End Function

        Public Function UpdateMembership_Txn(UpParam As Param_Txn_Update_Membership) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.param_UpdateMembership.Rec_ID, ClientScreen.Profile_Membership)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Membership_UpdateMembership_Txn, ClientScreen.Profile_Membership, UpParam)
        End Function

        Public Function DeleteMembership_Txn(DelParam As Param_Txn_Delete_Membership) As Boolean
            'Remove all Vouching posted against entry 
            DeleteByCondition("VA_ENTRY_ID = '" & DelParam.RecID_Delete & "' ", Tables.VOUCHING_AUDIT, ClientScreen.Profile_Membership)

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.Membership_DeleteMembership_Txn, ClientScreen.Profile_Membership, DelParam)
        End Function

        Public Function UpdateWingMembershipConfirmation(ByVal ResponseID As String, ByVal Approver As String, ByVal Action As String) As Boolean
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "sp_update_WingMembership_ByEmail"
            Dim params() As String = {"@ResponseID", "@Approver", "@Action"}
            Dim values() As Object = {ResponseID, Approver, Action}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {36, 255, 20}
            'used public update function as there are no transactional data involved 
            Return _RealService.UpdateBySPPublic(Tables.SERVICE_CHART_RESPONSES, SPName, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Profile_Membership))

        End Function

        Public Function GetInsttShortcodeForMembership()
            Select Case Val(cBase._open_Ins_ID)
                Case 1
                    Return "PBK"
                Case 2
                    Return "WRT"
                Case 3
                    Return "RRF"
                Case 4
                    Return "BKE"
                Case 5
                    Return "TCT"
                Case 6
                    Return "SSF"
                Case 7
                    Return "MKS"
                Case 8
                    Return "RMC"
            End Select
            Return ""
        End Function
        Public Function GetMemberEmails(WingShort As String, Optional StartChar As String = "1", Optional EndChar As String = "z") As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim DataBase_xls As DataTable = _RealService.List(Tables.MEMBERSHIP_INFO, "SELECT ab_curr.C_EMAIL_ID_1 FROM membership_info MS INNER JOIN address_book AS AB ON MS.MS_AB_ID = AB.REC_ID INNER JOIN address_book AS AB_CURR ON AB.C_ORG_REC_ID  = AB_CURR.C_ORG_REC_ID AND AB_CURR.C_COD_YEAR_ID=" + cBase._open_Year_ID.ToString() + " WHERE MS_WING_ID = (SELECT REC_ID FROM wings_info WHERE WING_SHORT_MS='" + WingShort + "') And LEN(COALESCE(ab_curr.C_EMAIL_ID_1,''))> 0 and  left(ab_curr.C_EMAIL_ID_1,1) >= '" + StartChar + "'  and  left(ab_curr.C_EMAIL_ID_1,1) <= '" + EndChar + "' ", Tables.MEMBERSHIP_INFO.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))

            Return DataBase_xls
        End Function
    End Class
#End Region

#Region "--Accounts--"
    <Serializable>
    Public Class Membership_Receipt_Register
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        '''  Get Max Transaction Date, Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_GetMaxTransactionDate</remarks>
        Public Function GetMaxTransactionDate() As Object
            Dim Query As String = "SELECT MAX(TR_DATE) AS MAXDATE FROM Transaction_Info WHERE  REC_STATUS IN (0,1,2)  AND TR_CEN_ID=" & cBase._open_Cen_ID.ToString & " AND TR_CODE IN (12,13) "
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.MembershipReceiptRegister_GetMaxTransactionDate, ClientScreen.Accounts_Notebook)
        End Function
        ''' <summary>
        ''' Get List, Shifted
        ''' </summary>
        ''' <param name="FromDate"></param>
        ''' <param name="ToDate"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_GetList</remarks>
        Public Function GetList(ByVal FromDate As Date, ByVal ToDate As Date) As DataTable
            Dim Param As Param_GetList_Membership_Receipt_Register = New Param_GetList_Membership_Receipt_Register()
            Param.FromDate = FromDate
            Param.openYearSdt = cBase._open_Year_Sdt
            Param.ToDate = ToDate
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.MembershipReceiptRegister_GetList, ClientScreen.Membership_Receipt_Register, Param)
        End Function

        ''' <summary>
        ''' Get Receipt Count, Shifted
        ''' </summary>
        ''' <param name="M_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_GetReceiptCount</remarks>
        Public Function GetReceiptCount(ByVal M_ID As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.MembershipReceiptRegister_GetReceiptCount, ClientScreen.Membership_Receipt_Register, M_ID)
        End Function

        ''' <summary>
        ''' Insert Receipt, Shifted
        ''' </summary>
        ''' <param name="M_ID"></param>
        ''' <param name="VDate"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_InsertReceipt</remarks>
        Public Function InsertReceipt(ByVal M_ID As String, ByVal VDate As String) As Boolean
            Dim Param As Param_InsertReceipt_Membership_Receipt_Register = New Param_InsertReceipt_Membership_Receipt_Register()
            Param.M_ID = M_ID
            Param.VDate = VDate
            Param.openYearID = cBase._open_Year_ID
            Param.openYearSdt = cBase._open_Year_Sdt
            Return InsertRecord(RealTimeService.RealServiceFunctions.MembershipReceiptRegister_InsertReceipt, ClientScreen.Membership_Receipt_Register, Param)
        End Function

        ''' <summary>
        ''' Delete Receipt, Shifted
        ''' </summary>
        ''' <param name="Reason"></param>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_DeleteReceipt</remarks>
        Public Overloads Function DeleteReceipt(ByVal Reason As String, ByVal Rec_Id As String) As Boolean
            Dim Param As Param_DeleteReceipt_Membership_Receipt_Register = New Param_DeleteReceipt_Membership_Receipt_Register()
            Param.Reason = Reason
            Param.Rec_Id = Rec_Id
            Return UpdateRecord(RealTimeService.RealServiceFunctions.MembershipReceiptRegister_DeleteReceipt, ClientScreen.Profile_Membership, Param)

            '            Return DeleteByCondition("MR_TR_M_ID    ='" & Rec_Id & "'", Tables.MEMBERSHIP_RECEIPT_INFO, ClientScreen.Membership_Receipt_Register, cBase._data_ConStr_Data)
        End Function

        Public Overloads Function DeleteReceiptRef(ByVal Rec_Id As String) As Boolean
            Dim Param As Param_DeleteReceipt_Membership_Receipt_Register = New Param_DeleteReceipt_Membership_Receipt_Register()
            Param.Rec_Id = Rec_Id
            Return UpdateRecord(RealTimeService.RealServiceFunctions.MembershipReceiptRegister_DeleteReceiptRef, ClientScreen.Profile_Membership, Param)
        End Function
    End Class
    <Serializable>
    Public Class Voucher_Membership
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_Membership, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        ''' <summary>
        ''' Get MasterID, Shifted
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_GetMasterID</remarks>
        Public Function GetMasterID(ByVal Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.VoucherMembership_GetMasterID, ClientScreen.Accounts_Voucher_Membership, Rec_Id)
        End Function

        Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_Membership, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_M_ID", Rec_ID, "TR_SR_NO", "1", ClientScreen.Accounts_Voucher_Membership, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        Public Function GetPurposeRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_Membership, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO)
        End Function

        ''' <summary>
        ''' Get Txn Bank Payment Detail, Shifted
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_GetTxnBankPaymentDetail</remarks>
        Public Function GetTxnBankPaymentDetail(ByVal MasterID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.VoucherMembership_GetTxnBankPaymentDetail, ClientScreen.Accounts_Voucher_Payment, MasterID)
        End Function

        Public Function GetMembershipRecord(ByVal Rec_ID As String) As DataTable
            ' Return GetRecordByColumn("MS_TR_ID", Rec_ID, ClientScreen.Accounts_Voucher_Membership, RealTimeService.Tables.MEMBERSHIP_INFO)
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim _Table As DataTable = _RealService.List(Tables.DataBase_xls, "SELECT [MS_CEN_ID],[MS_COD_YEAR_ID],AB.C_ORG_REC_ID [MS_AB_ID],[MS_SI_ID],[MS_START_DATE],[MS_CLOSE_DATE],[MS_CLOSE_REMARKS],[MS_WING_ID],[MS_NO],[MS_OLD_NO],[MS_OTHER_DETAIL],ms.[REC_ADD_ON],ms.[REC_ADD_BY],ms.[REC_EDIT_ON],ms.[REC_EDIT_BY],ms.[REC_STATUS],ms.[REC_STATUS_ON],ms.[REC_STATUS_BY],ms.[REC_ID],[MS_TR_ID] FROM [dbo].[membership_info] MS INNER JOIN address_book AS AB ON MS.MS_AB_ID = AB.REC_ID   WHERE MS_TR_ID = '" + Rec_ID + "'", Tables.DataBase_xls.ToString(), GetBaseParams(ClientScreen.Account_CashbookAuditor))
            Return _Table
        End Function



        ''' <summary>
        ''' Get Last Period, Shifted
        ''' </summary>
        ''' <param name="M_Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_GetLastPeriod</remarks>
        Public Function GetLastPeriod(ByVal M_Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.VoucherMembership_GetLastPeriod, ClientScreen.Accounts_Voucher_Membership, M_Rec_Id)
        End Function

        'Shifted
        Public Function GetNewMembershipNo() As Object
            Dim _MemShip As Membership = New Membership(cBase)
            Return _MemShip.GetNewMembershipNo
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemList() As DataTable
            Dim LocalQuery As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PROFILE, I.REC_ID AS ITEM_ID  " & _
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) And UCASE(I.ITEM_VOUCHER_TYPE) In ('MEMBERSHIP') "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "1"
            Return GetItemsByQuery_Common(LocalQuery, ClientScreen.Accounts_Voucher_Membership, inParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Item_Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemsByID(ByVal Item_Rec_Id As String) As DataTable
            Dim Query As String = " SELECT I.ITEM_NAME ,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_TRANS_STMT,I.ITEM_PARTY_REQ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " & _
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND L.REC_STATUS IN (0,1,2) AND I.REC_ID='" & Item_Rec_Id & "'  "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.ItemIDs = Item_Rec_Id
            inParam.Type = "2"
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_Membership, inParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBankList() As DataTable
            Dim Query As String = "SELECT BI_BANK_NAME ,BI_SHORT_NAME,REC_ID as BI_ID " & _
                              " From  BANK_INFO     " & _
                              " Where   REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
            Return GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_Membership)
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional ByVal ForeignOnly As Boolean = False) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common()
            Param.ForeignOnly = ForeignOnly
            Return _BA.GetList(ClientScreen.Accounts_Voucher_Membership, Param)
        End Function

        'Shifted
        Public Function GetPurposes() As DataTable
            Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Membership, "PUR_NAME", "PUR_ID")
        End Function

        'Shifted
        Public Function GetBranchDetails(ByVal Branch_IDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Accounts_Voucher_Membership, "  B.BI_BANK_NAME AS Name,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as Branch, A.REC_ID AS BB_BRANCH_ID ")
        End Function

        Public Function GetCountForWing2WingConversion(Param As Param_GetCountForWing2WingConversion) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetCountForWing2WingConversion, ClientScreen.Accounts_Voucher_Membership, Param)
        End Function

        Public Function GetDiscontinued_Date(ByVal Param As Param_GetCountForContinue_Membership) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.Membership_GetDiscontinued_Date, ClientScreen.Accounts_Voucher_Membership, Param)
        End Function

        ''' <summary>
        ''' Insert Master Info, Shifted
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertMasterInfo</remarks>
        Public Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMasterInfo_VoucherMembership) As Boolean
            'InMInfo.openYearID = cBase._open_Year_ID   ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembership_InsertMasterInfo, ClientScreen.Accounts_Voucher_Membership, InMInfo)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherMembership) As Boolean
            'InParam.openYearID = cBase._open_Year_ID   ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembership_Insert, ClientScreen.Accounts_Voucher_Membership, InParam)
        End Function

        ''' <summary>
        ''' Insert Item, Shifted
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertItem</remarks>
        Public Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherMembership) As Boolean
            'InItem.openYearID = cBase._open_Year_ID   ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembership_InsertItem, ClientScreen.Accounts_Voucher_Membership, InItem)
        End Function

        ''' <summary>
        ''' Insert Payment, Shifted
        ''' </summary>
        ''' <param name="InPmt"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertPayment</remarks>
        Public Function InsertPayment(ByVal InPmt As Parameter_InsertPayment_VoucherMembership) As Boolean
            'InPmt.openYearID = cBase._open_Year_ID   ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembership_InsertPayment, ClientScreen.Accounts_Voucher_Membership, InPmt)
        End Function

        ''' <summary>
        ''' Insert Purpose, Shifted
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_InsertPurpose</remarks>
        Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherMembership) As Boolean
            'InPurpose.openYearID = cBase._open_Year_ID   ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembership_InsertPurpose, ClientScreen.Accounts_Voucher_Membership, InPurpose)
        End Function

        'No Need To shift
        Public Function InsertMembership(ByVal InMem As Parameter_InsertMembership_VoucherMembership) As Boolean
            Dim _MemShip As Membership = New Membership(cBase)
            Dim inParam As Parameter_Insert_Membership = New Parameter_Insert_Membership()
            inParam.AB_ID = InMem.AB_ID
            inParam.SUBS_ID = InMem.SUBS_ID
            inParam.Wing_Id = InMem.Wing_Id
            inParam.StartDate = InMem.StartDate
            inParam.Mem_Old_No = InMem.Mem_Old_No
            inParam.Mem_No = InMem.Mem_No
            inParam.OtherDetails = InMem.OtherDetails
            inParam.Status_Action = InMem.Status_Action
            inParam.Rec_ID = InMem.Rec_ID
            inParam.Txn_ID = InMem.TxnID
            inParam.Screen = ClientScreen.Accounts_Voucher_Membership
            Return _MemShip.Insert(inParam)
        End Function

        'No Need To shift
        Public Function InsertBalances(ByVal InBal As Parameter_InsertBalances_VoucherMembership) As Boolean
            Dim _MemShip As Membership = New Membership(cBase)
            Dim inParam As Parameter_InsertBalances_Membership = New Parameter_InsertBalances_Membership()
            inParam.REC_ID = InBal.REC_ID
            inParam.Sr_No = InBal.Sr_No
            inParam.SUBS_ID = InBal.Subs_ID
            inParam.Item_ID = InBal.Item_ID
            inParam.Entry_Date = InBal.Entry_Date
            inParam.Period_From = InBal.Period_From
            inParam.Period_To = InBal.Period_To
            inParam.Amount = InBal.Amount
            inParam.Status_Action = InBal.Status_Action
            inParam.Txn_ID = InBal.TxnID
            inParam.Screen = ClientScreen.Accounts_Voucher_Membership
            Return _MemShip.InsertBalances(inParam)
            'Dim Param As Common_Lib.RealTimeService.Param_Txn_Insert_Membership = New Common_Lib.RealTimeService.Param_Txn_Insert_Membership
            'Param.param_InsertBalances = inParam
            'Return _MemShip.InsertMembership_Txn(Param)
        End Function

        ''' <summary>
        ''' Update Master, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_UpdateMaster</remarks>
        Public Function UpdateMaster(ByVal UpParam As Parameter_UpdateMaster_VoucherMembership) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Membership)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherMembership_UpdateMaster, ClientScreen.Accounts_Voucher_Payment, UpParam)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="Updt"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_Update</remarks>
        Public Function Update(ByVal Updt As Parameter_Update_VoucherMembership) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(Updt.RecID, ClientScreen.Accounts_Voucher_Membership)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherMembership_Update, ClientScreen.Accounts_Voucher_Membership, Updt)
        End Function

        ''' <summary>
        ''' Updates Purpose, Shifted
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembership_UpdatePurpose</remarks>
        Public Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_VoucherMembership) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpPurpose.RecID, ClientScreen.Accounts_Voucher_Membership)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherMembership_UpdatePurpose, ClientScreen.Accounts_Voucher_Membership, UpPurpose)
        End Function

        Public Overloads Function DeleteMaster(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteRecord(Master_Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_Membership)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_Membership)
        End Function

        Public Overloads Function DeleteItems(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_ITEM_INFO, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        Public Overloads Function DeletePurpose(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("TR_REC_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_Membership)
        End Function

        Public Overloads Function DeletePayment(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("TR_M_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_D_PAYMENT_INFO, ClientScreen.Accounts_Voucher_Membership)
        End Function

        Public Overloads Function DeleteMembership(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("MS_TR_ID    ='" & Rec_Id & "'", Tables.MEMBERSHIP_INFO, ClientScreen.Accounts_Voucher_Membership)
        End Function

        Public Overloads Function DeleteBalances(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("MB_TR_ID    ='" & Rec_Id & "'", Tables.MEMBERSHIP_BALANCES_INFO, ClientScreen.Accounts_Voucher_Membership)
        End Function

        Public Function InsertMembershipVoucher_Txn(InParam As Param_Txn_Insert_VoucherMembership) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembership_InsertMembershipVoucher_Txn, ClientScreen.Accounts_Voucher_Membership, InParam)
        End Function

        Public Function UpdateMembershipVoucher_Txn(UpParam As Param_Txn_Update_VoucherMembership) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.MID_Delete, ClientScreen.Accounts_Voucher_Membership)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembership_UpdateMembershipVoucher_Txn, ClientScreen.Accounts_Voucher_Membership, UpParam)
        End Function

        Public Function DeleteMembershipVoucher_Txn(DelParam As Param_Txn_Delete_VoucherMembership) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.MID_Delete, ClientScreen.Accounts_Voucher_Membership)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembership_DeleteMembershipVoucher_Txn, ClientScreen.Accounts_Voucher_Membership, DelParam)
        End Function

    End Class
    <Serializable>
    Public Class Voucher_Membership_Renewal
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetStatus(ByVal Rec_Id As String) As Object
            Return GetRecordStatus(Rec_Id, ClientScreen.Accounts_Voucher_Membership_Renewal, RealTimeService.Tables.TRANSACTION_INFO, "TR_CEN_ID")
        End Function

        ''' <summary>
        ''' Gets MasterID, Shifted
        ''' </summary>
        ''' <param name="Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetMasterID</remarks>
        Public Function GetMasterID(ByVal Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_GetMasterID, ClientScreen.Accounts_Voucher_Membership_Renewal, Rec_Id)
        End Function

        Public Function GetMasterRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Accounts_Voucher_Membership_Renewal, RealTimeService.Tables.TRANSACTION_D_MASTER_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        Public Function GetRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_M_ID", Rec_ID, "TR_SR_NO", "1", ClientScreen.Accounts_Voucher_Membership_Renewal, RealTimeService.Tables.TRANSACTION_INFO)
        End Function

        Public Function GetPurposeRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("TR_REC_ID", Rec_ID, ClientScreen.Accounts_Voucher_Membership_Renewal, RealTimeService.Tables.TRANSACTION_D_PURPOSE_INFO, Common.ClientDBFolderCode.DATA)
        End Function

        ''' <summary>
        ''' Get Txn Bank Payment Detail, Shifted
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetTxnBankPaymentDetail</remarks>
        Public Function GetTxnBankPaymentDetail(ByVal MasterID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_GetTxnBankPaymentDetail, ClientScreen.Accounts_Voucher_Payment, MasterID)
        End Function

        ''' <summary>
        ''' Get Party Details, Shifted
        ''' </summary>
        ''' <param name="OtherCondition"></param>
        ''' <param name="SearchStr"></param>
        ''' <param name="Use_Rec_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetPartyDetails</remarks>
        Public Function GetPartyDetails(ByVal OtherCondition As String, ByVal SearchStr As String, ByVal Membership_Rec_ID As String, Optional ByVal Use_Rec_ID As Boolean = False) As DataTable
            Dim Param As Param_GetPartyDetails_VoucherMembershipRenewal = New Param_GetPartyDetails_VoucherMembershipRenewal()
            Param.OtherCondition = OtherCondition
            Param.SearchStr = SearchStr
            Param.Use_Rec_ID = Use_Rec_ID
            Param.Membership_REC_ID = Membership_Rec_ID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_GetPartyDetails, ClientScreen.Accounts_Voucher_Membership_Renewal, Param)
        End Function

        Public Function GetMembershipRecord(ByVal Rec_ID As String) As DataTable
            Return GetRecordByColumn("MS_TR_ID", Rec_ID, ClientScreen.Accounts_Voucher_Membership_Renewal, RealTimeService.Tables.MEMBERSHIP_INFO)
        End Function

        ''' <summary>
        ''' Get Last Period, Shifted
        ''' </summary>
        ''' <param name="M_Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_GetLastPeriod</remarks>
        Public Function GetLastPeriod(ByVal M_Rec_Id As String) As Object
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_GetLastPeriod, ClientScreen.Accounts_Voucher_Membership_Renewal, M_Rec_Id)
        End Function

        'Shifted
        Public Function GetNewMembershipNo() As Object
            Dim _MemShip As Membership = New Membership(cBase)
            Return _MemShip.GetNewMembershipNo
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemList() As DataTable
            Dim LocalQuery As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PROFILE, I.REC_ID AS ITEM_ID  " & _
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('MEMBERSHIP RENEWAL') "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.Type = "1"
            Return GetItemsByQuery_Common(LocalQuery, ClientScreen.Accounts_Voucher_Membership_Renewal, inParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <param name="Item_Rec_Id"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemsByID(ByVal Item_Rec_Id As String) As DataTable
            Dim Query As String = " SELECT I.ITEM_NAME ,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_TRANS_STMT,I.ITEM_PARTY_REQ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " & _
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND L.REC_STATUS IN (0,1,2) AND I.REC_ID='" & Item_Rec_Id & "'  "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            inParam.ItemIDs = Item_Rec_Id
            inParam.Type = "2"
            Return GetItemsByQuery_Common(Query, ClientScreen.Accounts_Voucher_Membership_Renewal, inParam)
        End Function

        ''' <summary>
        ''' Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBankList() As DataTable
            Dim Query As String = "SELECT BI_BANK_NAME ,BI_SHORT_NAME,REC_ID as BI_ID " & _
                              " From  BANK_INFO     " & _
                              " Where   REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
            Return GetBankInfo(Query, Query, ClientScreen.Accounts_Voucher_Membership_Renewal)
        End Function

        'Shifted
        Public Function GetBankAccounts(Optional ByVal ForeignOnly As Boolean = False) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Dim Param As Param_Bank_GetList_Common = New Param_Bank_GetList_Common()
            Param.ForeignOnly = ForeignOnly
            Return _BA.GetList(ClientScreen.Accounts_Voucher_Membership_Renewal, Param)
        End Function

        'Shifted
        Public Function GetPurposes() As DataTable
            Return GetMisc("GODLY SERVICE PROJECTS", ClientScreen.Accounts_Voucher_Membership_Renewal, "PUR_NAME", "PUR_ID")
        End Function

        'Shifted
        Public Function GetBranchDetails(ByVal Branch_IDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Accounts_Voucher_Membership_Renewal, "  B.BI_BANK_NAME AS Name,B.BI_SHORT_NAME,A.BB_BRANCH_NAME as Branch, A.REC_ID AS BB_BRANCH_ID ")
        End Function

        ''' <summary>
        '''  Insert Master Info, Shifted
        ''' </summary>
        ''' <param name="InMinfo"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_InsertMasterInfo</remarks>
        Public Function InsertMasterInfo(ByVal InMinfo As Parameter_InsertMasterInfo_VoucherMembershipRenewal) As Boolean
            'InMinfo.openYearID = cBase._open_Year_ID   ' Removed and used from basicParam instead 
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_InsertMasterInfo, ClientScreen.Accounts_Voucher_Membership_Renewal, InMinfo)
        End Function

        ''' <summary>
        ''' Insert, Shifted
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_Insert</remarks>
        Public Function Insert(ByVal InParam As Parameter_Insert_VoucherMembershipRenewal) As Boolean
            'InParam.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam ins
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_Insert, ClientScreen.Accounts_Voucher_Membership_Renewal, InParam)
        End Function

        ''' <summary>
        ''' Insert Item, Shifted
        ''' </summary>
        ''' <param name="InItem"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_InsertItem</remarks>
        Public Function InsertItem(ByVal InItem As Parameter_InsertItem_VoucherMembershipRenewal) As Boolean
            'InItem.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam ins
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_InsertItem, ClientScreen.Accounts_Voucher_Membership_Renewal, InItem)
        End Function

        ''' <summary>
        ''' Insert Payment, Shifted
        ''' </summary>
        ''' <param name="InPayment"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_InsertPayment</remarks>
        Public Function InsertPayment(ByVal InPayment As Parameter_InsertPayment_VoucherMembershipRenewal) As Boolean
            'InPayment.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam ins
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_InsertPayment, ClientScreen.Accounts_Voucher_Membership_Renewal, InPayment)
        End Function

        ''' <summary>
        ''' Insert Purpose, Shifted
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_InsertPurpose</remarks>
        Public Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherMembershipRenewal) As Boolean
            'InPurpose.openYearID = cBase._open_Year_ID  ' Removed and used from basicParam ins
            Return InsertRecord(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_InsertPurpose, ClientScreen.Accounts_Voucher_Membership_Renewal, InPurpose)
        End Function

        'No need to shift
        Public Function InsertMembership(ByVal InMem As Parameter_InsertMembership_VoucherMembershipRenewal) As Boolean
            Dim _MemShip As Membership = New Membership(cBase)
            Dim inParam As Parameter_Insert_Membership = New Parameter_Insert_Membership()
            inParam.AB_ID = InMem.AB_ID
            inParam.SUBS_ID = InMem.SUBS_ID
            inParam.Wing_Id = InMem.Wing_Id
            inParam.StartDate = InMem.StartDate
            inParam.Mem_Old_No = InMem.Mem_Old_No
            inParam.Mem_No = InMem.Mem_No
            inParam.OtherDetails = InMem.OtherDetails
            inParam.Status_Action = InMem.Status_Action
            inParam.Rec_ID = InMem.Rec_ID
            inParam.Txn_ID = InMem.TxnID
            inParam.Screen = ClientScreen.Accounts_Voucher_Membership_Renewal
            Return (_MemShip.Insert(inParam))
            'Dim Param As Common_Lib.RealTimeService.Param_Txn_Insert_Membership = New Common_Lib.RealTimeService.Param_Txn_Insert_Membership
            'Param.param_InsertMembership = inParam
            'Return _MemShip.InsertMembership_Txn(Param)
        End Function

        'No need to Shift
        Public Function InsertBalances(ByVal InBal As Parameter_InsertBalances_VoucherMembershipRenewal) As Boolean
            Dim _MemShip As Membership = New Membership(cBase)
            Dim inParam As Parameter_InsertBalances_Membership = New Parameter_InsertBalances_Membership()
            inParam.REC_ID = InBal.REC_ID
            inParam.Sr_No = InBal.Sr_No
            inParam.SUBS_ID = InBal.Subs_ID
            inParam.Item_ID = InBal.Item_ID
            inParam.Entry_Date = InBal.Entry_Date
            inParam.Period_From = InBal.Period_From
            inParam.Period_To = InBal.Period_To
            inParam.Amount = InBal.Amount
            inParam.Status_Action = InBal.Status_Action
            inParam.Txn_ID = InBal.TxnID
            inParam.Screen = ClientScreen.Accounts_Voucher_Membership_Renewal
            Return _MemShip.InsertBalances(inParam)
            'Dim Param As Common_Lib.RealTimeService.Param_Txn_Insert_Membership = New Common_Lib.RealTimeService.Param_Txn_Insert_Membership
            'Param.param_InsertBalances = inParam
            'Return _MemShip.InsertMembership_Txn(Param)
        End Function

        ''' <summary>
        ''' Update Master, Shifted
        ''' </summary>
        ''' <param name="UpMaster"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_UpdateMaster</remarks>
        Public Function UpdateMaster(ByVal UpMaster As Parameter_UpdateMaster_VoucherMembershipRenewal) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpMaster.RecID, ClientScreen.Accounts_Voucher_Membership)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_UpdateMaster, ClientScreen.Accounts_Voucher_Payment, UpMaster)
        End Function

        ''' <summary>
        ''' Update, Shifted
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_Update</remarks>
        Public Function Update(ByVal UpParam As Parameter_Update_VoucherMembershipRenewal) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.RecID, ClientScreen.Accounts_Voucher_Membership)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_Update, ClientScreen.Accounts_Voucher_Membership_Renewal, UpParam)
        End Function

        ''' <summary>
        ''' Update Purpose, Shifted
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherMembershipRenewal_UpdatePurpose</remarks>
        Public Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_VoucherMembershipRenewal) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpPurpose.RecID, ClientScreen.Accounts_Voucher_Membership)
            Return UpdateRecord(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_UpdatePurpose, ClientScreen.Accounts_Voucher_Membership_Renewal, UpPurpose)
        End Function

        Public Overloads Function DeleteMaster(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteRecord(Master_Rec_Id, Tables.TRANSACTION_D_MASTER_INFO, ClientScreen.Accounts_Voucher_Membership_Renewal)
        End Function

        Public Overloads Function Delete(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_INFO, ClientScreen.Accounts_Voucher_Membership_Renewal)
        End Function

        Public Overloads Function DeleteItems(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("TR_M_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_ITEM_INFO, ClientScreen.Accounts_Voucher_SaleOfAsset)
        End Function

        Public Overloads Function DeletePurpose(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("TR_REC_ID = '" & Rec_Id & "'", Tables.TRANSACTION_D_PURPOSE_INFO, ClientScreen.Accounts_Voucher_Membership_Renewal)
        End Function

        Public Overloads Function DeletePayment(ByVal Master_Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Master_Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("TR_M_ID = '" & Master_Rec_Id & "'", Tables.TRANSACTION_D_PAYMENT_INFO, ClientScreen.Accounts_Voucher_Membership_Renewal)
        End Function

        Public Overloads Function DeleteMembership(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("MS_TR_ID    ='" & Rec_Id & "'", Tables.MEMBERSHIP_INFO, ClientScreen.Accounts_Voucher_Membership_Renewal)
        End Function
        Public Overloads Function DeleteBalances(ByVal Rec_Id As String) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(Rec_Id, ClientScreen.Accounts_Voucher_Membership)
            Return DeleteByCondition("MB_TR_ID    ='" & Rec_Id & "'", Tables.MEMBERSHIP_BALANCES_INFO, ClientScreen.Accounts_Voucher_Membership_Renewal)
        End Function

        Public Function InsertMemRenewalVoucher_Txn(InParam As Param_Txn_Insert_VoucherMembershipRenewal) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_InsertMemRenewalVoucher_Txn, ClientScreen.Accounts_Voucher_Membership, InParam)
        End Function

        Public Function UpdateMemRenewalVoucher_Txn(UpParam As Param_Txn_Update_VoucherMembershipRenewal) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.MID_Delete, ClientScreen.Accounts_Voucher_Membership)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_UpdateMemRenewalVoucher_Txn, ClientScreen.Accounts_Voucher_Membership, UpParam)
        End Function

        Public Function DeleteMemRenewalVoucher_Txn(DelParam As Param_Txn_Delete_VoucherMembershipRenewal) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.MID_Delete, ClientScreen.Accounts_Voucher_Membership)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembershipRenewal_DeleteMemRenewalVoucher_Txn, ClientScreen.Accounts_Voucher_Membership, DelParam)
        End Function
    End Class
    <Serializable>
    Public Class Voucher_Membership_Conversion
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetPartyDetails(ByVal OtherCondition As String, ByVal SearchStr As String, ByVal Membership_Rec_ID As String, ConvertedMemberID As String, Optional ByVal Use_Rec_ID As Boolean = False) As DataTable
            Dim Param As Param_GetPartyDetails_VoucherMembershipConversion = New Param_GetPartyDetails_VoucherMembershipConversion()
            Param.OtherCondition = OtherCondition
            Param.SearchStr = SearchStr
            Param.Use_Rec_ID = Use_Rec_ID
            Param.Membership_REC_ID = Membership_Rec_ID
            Param.Converted_Member_ID = ConvertedMemberID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.VoucherMembershipConversion_GetPartyDetails, ClientScreen.Accounts_Voucher_Membership_Conversion, Param)
        End Function

        Public Function GetItemList() As DataTable
            Dim LocalQuery As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PROFILE, I.REC_ID AS ITEM_ID  " & _
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UCASE(I.ITEM_VOUCHER_TYPE) IN ('MEMBERSHIP') "
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            ' inParam.Type = "3"
            Return GetItemsByQuery_Common(LocalQuery, ClientScreen.Accounts_Voucher_Membership_Conversion, inParam)
        End Function

        Public Function GetAdjustmentPaymentRecord(ByVal TR_M_ID As String) As DataTable
            Return GetRecordByColumn("TR_M_ID", TR_M_ID, "TR_PAY_TYPE", "ADJUSTMENT", ClientScreen.Accounts_Voucher_Membership_Conversion, RealTimeService.Tables.TRANSACTION_D_PAYMENT_INFO)
        End Function

        Public Function InsertMemConversionVoucher_Txn(InParam As Param_Txn_Insert_VoucherMembershipConversion) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembershipConversion_InsertMembershipVoucherConversion_Txn, ClientScreen.Accounts_Voucher_Membership_Conversion, InParam)
        End Function

        Public Function UpdateMemConversionVoucher_Txn(UpParam As Param_Txn_Update_VoucherMembershipConversion) As Boolean
            cBase._Audit_DBOps.UnVouchEntryByReference(UpParam.MID_Delete, ClientScreen.Accounts_Voucher_Membership)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembershipConversion_UpdateMembershipVoucherConversion_Txn, ClientScreen.Accounts_Voucher_Membership_Conversion, UpParam)
        End Function

        Public Function DeleteMemConversionVoucher_Txn(DelParam As Param_Txn_Delete_VoucherMembershipConversion) As Boolean
            cBase._Audit_DBOps.DeleteAllVouchingAgainstReference(DelParam.MID_Delete, ClientScreen.Accounts_Voucher_Membership)
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.VoucherMembershipConversion_DeleteMembershipVoucherConversion_Txn, ClientScreen.Accounts_Voucher_Membership_Conversion, DelParam)
        End Function
    End Class

#End Region
End Class
