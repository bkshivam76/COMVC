Imports System.Data

Namespace Real
#Region "--Profile--"
    <Serializable>
    Public Class Magazine
        Public Shared _Server_Date_Format_Short As String = "yyyy-MM-dd"
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetList_Magazine
            Public VoucherEntry As String
            Public ProfileEntry As String
            Public OtherCondition As String

        End Class
        <Serializable>
        Public Class Param_GetFee_Magazine
            Public ID As String
            Public StartDate As Date
        End Class
        <Serializable>
        Public Class Param_GetMembers_Magazine
            Public MemberType As Integer
            Public SearchCondition As String
            Public SearchStr As String
            Public Use_Rec_ID As Boolean
            Public Member_Rec_Id As String

        End Class
        <Serializable>
        Public Class Param_Get_MagazineMembershipList
            Public Membership_ID As String = ""
            Public Membership_Old_ID As String = ""
            Public Member_Name As String = ""
            Public MemberType As String = ""
            Public RelatedMemberRecID As String = ""
            Public PrevYearID As Integer = Nothing
        End Class
        <Serializable>
        Public Class param_GetMagazineCountByName
            Public Name As String = ""
            Public REC_ID As String = ""
        End Class
        <Serializable>
        Public Class param_GetMagazineCountByShortName
            Public ShortName As String = ""
            Public REC_ID As String = ""
        End Class
        <Serializable>
        Public Class param_GetMagazineSubTypeCountByName
            Public Name As String = ""
            Public REC_ID As String = ""
            Public Magazine_ID As String = ""
        End Class
        <Serializable>
        Public Class param_GetMagazineSubTypeCountByShortName
            Public ShortName As String = ""
            Public REC_ID As String = ""
            Public Magazine_ID As String = ""
        End Class
        <Serializable>
        Public Class param_GetMagazineSubFeeCountByEffDate
            Public EffDate As String = ""
            Public REC_ID As String = ""
            Public SubsType_ID As String = ""
        End Class
        <Serializable>
        Public Class param_GetMagazineDispatchCountByName
            Public Name As String = ""
            Public REC_ID As String = ""
        End Class
        <Serializable>
        Public Class param_GetMagazineDispatchFeeCountByEffDate
            Public EffDate As String = ""
            Public REC_ID As String = ""
            Public DispatchType_ID As String = ""
        End Class
        <Serializable>
        Public Class param_GetMembershipCountByMemberID
            Public MemberID As String = ""
            Public Magazine_ID As String = ""
            Public REC_ID As String = ""
        End Class
        <Serializable>
        Public Class Param_GetMapping_SubCities
            Public searchString As String = ""
            Public stateID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Membership_Profile
            Public Member_Type As String
            Public MS_Start_Date As String
            ' Public Mem_No As Integer
            Public Mem_ID As String
            Public Mem_Old_ID As String
            Public Member_Address_GP_ID As String
            Public CC_Applicable As String
            Public CC_MS_ID As String = ""
            Public CC_Sponsored As Boolean
            Public CC_Dispatch As String
            Public Mag_ID As String
            Public MST_ID As String
            Public Period_Fr As String
            Public Period_To As String
            Public Category As String
            Public Copies As Integer
            Public FreeCopies As Integer
            Public Subs_Amt As Double
            Public MDT_ID As String
            Public Dispatch_Amt As Double
            Public Total_Bal_Amt As Double
            Public OtherDetails As String
            Public Next_Year_ID As Integer
            'Public Status_Action As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine
            Public Name As String
            Public ShortName As String
            Public Language As String
            Public Magazine_Regd_No As String
            Public Postal_Regd_No As String
            Public PublishOn As String
            Public FS_Applicable As String
            Public MS_Start_No As Integer
            Public Status_Action As String
            Public Year_Sdt As Date
            Public Copies_Per_Year As Integer
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine
            Public Name As String
            Public ShortName As String
            Public Language As String
            Public Magazine_Regd_No As String
            Public Postal_Regd_No As String
            Public PublishOn As String
            Public FS_Applicable As String
            Public MS_Start_No As Integer
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Subs_Type
            Public Name As String
            Public ShortName As String
            Public StartMonth As Integer
            Public MinMonth As Integer
            Public PeriodWise As String
            Public FixedPeriod As String
            Public Mag_ID As String
            Public Status_Action As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Subcity
            Public COUNTRY_REC_ID As String
            Public STATE_REC_ID As String
            Public CITY_REC_ID As String
            Public DISTRICT_REC_ID As String
            Public SUB_CITY As String
            Public PINCODE As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Subcity
            Public COUNTRY_REC_ID As String
            Public STATE_REC_ID As String
            Public CITY_REC_ID As String
            Public DISTRICT_REC_ID As String
            Public SUB_CITY As String
            Public PINCODE As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Subs_Type
            Public Name As String
            Public ShortName As String
            Public StartMonth As Integer
            Public MinMonth As Integer
            Public PeriodWise As String
            Public FixedPeriod As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Subs_Type_Fee
            Public Eff_Date As String
            Public Indian_Fee As Double
            Public Foreign_Fee As Double
            Public ST_ID As String
            Public Status_Action As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Subs_Type_Fee
            Public Eff_Date As String
            Public Indian_Fee As Double
            Public Foreign_Fee As Double
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Dispatch_Type
            Public Name As String
            Public Status_Action As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Dispatch_Type
            Public Name As String
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Dispatch_Type_Charges
            Public Eff_Date As String
            Public Charges As Double
            Public DT_ID As String
            Public Status_Action As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Dispatch_Type_Charges
            Public Eff_Date As String
            Public Charges As Double
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Membership_Profile
            Public Member_Type As String
            Public MS_Start_Date As String
            Public MS_No As Integer
            Public Mem_ID As String
            Public Mem_Old_ID As String
            Public Member_Address_GP_ID As String
            Public CC_Applicable As String
            Public CC_MS_ID As String = ""
            Public CC_Sponsored As Boolean
            Public CC_Dispatch As String
            Public Mag_ID As String
            Public MST_ID As String
            Public Period_Fr As String
            Public Period_To As String
            Public Category As String
            Public Copies As Integer
            Public FreeCopies As Integer
            Public Subs_Amt As Double
            Public MDT_ID As String
            Public Dispatch_Amt As Double
            Public Total_Amt As Double
            Public OtherDetails As String
            Public Rec_ID As String
            Public MMB_ID As String
            Public Next_Year_ID As Int32
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Membership_Identity
            Public Member_Type As String
            Public Mem_Old_ID As String
            Public Member_ID As String
            Public MagID As String
            Public CC_sponsored As Boolean
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_get_MagazineMembershipRegister
            Public YR_START_DATE As DateTime
            Public FROM_DATE As DateTime
            Public TO_DATE As DateTime
            Public Tr_m_Id As String
            Public Prev_Year_Id As Integer
        End Class
        <Serializable>
        Public Class Param_GetList_SubCities
            Public Rec_ID As String = ""
            Public City_ID As String = ""
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Dispatch
            Public Dispatch_ID As String = ""
            Public Issue_ID As String = ""
            Public Membership_ID As String = ""
            Public DispatchDate As String
            Public Status As String = ""
            Public Tr_ID As String = ""
            Public Copies As Int32 = 0
            Public Remarks As String = ""
            Public RPC_ID As String = ""
            Public PKT_NO As Int32 = 0
        End Class
        <Serializable>
        Public Class Parameter_Insert_dispatch_New_Voucher
            Public FromDate As DateTime
            Public ToDate As DateTime
            Public MagID As String
            Public MembershipID As String
            Public subDate As DateTime
            Public subsCopies As Int32
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Dispatch_Bundles
            Public Issue_ID As String = ""
            Public Membership_ID As String = ""
            Public Copies As Int32 = 0
            Public Remarks As String = ""
            Public Bundle_No As Int32 = 0
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Dispatch
            Inherits Parameter_Insert_Magazine_Dispatch
        End Class
        <Serializable>
        Public Class Parameter_Get_Dispatch_Details
            Public Membership_Rec_ID As String = ""
            Public Issue_ID As String = ""
        End Class
        <Serializable>
        Public Class Parameter_GetList_MagazineDispatchRegister
            Public Disp_Membership_ID As String
            Public Membership_ID As String
            Public Membership_Old_ID As String
            Public Issue_Date As String
            Public Magazine As String
            Public Prev_Year_Id As Int32
            Public CutOffTime As DateTime = Nothing
        End Class
        <Serializable>
        Public Class Param_GetIssueCount
            Public FromDate As DateTime
            Public ToDate As DateTime
            Public MagID As String
            Public GetCurrYearIssuesOnly As Boolean = False
        End Class
        <Serializable>
        Public Class Parameter_Insert_Magazine_Client_Restriction
            Public UserID As String
            Public RestrictedTill As DateTime
        End Class
        <Serializable>
        Public Class Parameter_AutoRenewal_Membership
            Public ConsiderForAutoRenewal As Boolean
            Public Rec_ID As String
        End Class
        <Serializable>
        Public Class Param_Insert_Magazine_Issue
            Public MAG_ID As String
            Public ISSUE_DATE As DateTime
            Public ISSUE_PART_NO As Integer
            Public ISSUE_VOL_NO As Integer
            Public ISSUE_NO As Integer
            Public BUNDLE_REG_SIZE As Integer
            Public BUNDLE_REG_SIZE_FGN As Integer
            Public PER_COPY_WEIGHT As Decimal
            Public BUNDLE_MAX_WEIGHT As Decimal
            Public BUNDLE_MAX_WEIGHT_FGN As Decimal
            Public RPC_SEED As Integer
            Public REG_EXP_IND As Decimal
            Public REG_EXP_FGN As Decimal
        End Class
        <Serializable>
        Public Class Param_Insert_Magazine_Similar_Issues
            Public ISSUE_ID As String
            Public YrCount As Integer
        End Class
        <Serializable>
        Public Class Param_Update_Magazine_Issue
            Public ISSUE_ID As String
            Public ISSUE_DATE As DateTime
            Public ISSUE_PART_NO As Integer
            Public ISSUE_VOL_NO As Integer
            Public ISSUE_NO As Integer
            Public BUNDLE_REG_SIZE As Integer
            Public BUNDLE_REG_SIZE_FGN As Integer
            Public PER_COPY_WEIGHT As Decimal
            Public BUNDLE_MAX_WEIGHT As Decimal
            Public BUNDLE_MAX_WEIGHT_FGN As Decimal
            Public RPC_SEED As Integer
            Public REG_EXP_IND As Decimal
            Public REG_EXP_FGN As Decimal
        End Class
#End Region

        Public Shared Function GetList(ByVal Param As Param_GetList_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT MI.MI_NAME AS 'Name',MI.MI_SHORT_NAME as  'Short Name',MI.MI_LANGUAGE as 'Language',MI.MI_PUBLISH_ON as 'Publish On',MI.MI_MAG_REGD_NO as 'Magazine Regd. No.',MI.MI_POSTAL_REGD_NO as 'Postal Regd. No.',MI.MI_MS_START_NO AS 'Membership Start No.',MI.MI_FS_APPLICABLE as 'Foreign Subscriptions', MI.REC_ID AS ID , " & Common.Rec_Detail("MI") & "" & _
                                        " FROM       MAGAZINE_INFO      AS MI " & _
                                        " Where MI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") "
            If inBasicParam.screen <> ConnectOneWS.ClientScreen.Facility_Magazine_Request Then
                onlineQuery += " AND MI.MI_CEN_ID=" & inBasicParam.openCenID.ToString & ""
            End If
            onlineQuery += Param.OtherCondition & " ORDER BY MI.MI_NAME "
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_INFO, onlineQuery, ConnectOneWS.Tables.MAGAZINE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_SubscriptionType(ByVal Param As Param_GetList_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT ST.MST_SR_NO as 'Sr.',ST.MST_TYPE as 'Type',ST.MST_SHORT_NAME as 'Short Name',case  isnull(ST.MST_START_MONTH,0) when 1 then 'Jan' when 2 then 'Feb' when 3 then 'Mar' when 4 then 'Apr' when 5 then 'May' when 6 then 'Jun' when 7 then 'Jul' when 8 then 'Aug' when 9 then 'Sep' when 10 then 'Oct' when 11 then 'Nov' when 12 then 'Dec' else '' end as 'Start Month',ST.MST_START_MONTH as 'St_Month',ST.MST_MIN_MONTHS as 'Min.Months',ST.MST_FIXED_PERIOD as 'Fixed Period',ST.MST_FEE_PERIOD_WISE as 'Period wise Fee Calculation',ST.MST_MI_ID,ST.REC_ID AS ID,(SELECT MII_ISSUE_DATE FROM magazine_issue_info WHERE MII_DEFAULT = 1 AND MII_CEN_ID = MST_CEN_ID ) as 'Default_From_Date',  ST.MST_DEFAULT AS 'Default' , " & Common.Rec_Detail("ST") & "" & _
                                        " FROM       MAGAZINE_SUBS_TYPE      AS ST " & _
                                        " Where ST.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") " & Param.OtherCondition & " ORDER BY ST.MST_SR_NO "
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, onlineQuery, ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_SubscriptionTypeFee(ByVal Param As Param_GetList_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT  CONVERT(varchar ,STF.MSTF_EFF_DATE,106)  AS 'Effective Date',STF.MSTF_INDIAN_FEE as 'Indian Fee',STF.MSTF_FOREIGN_FEE as 'Foreign Fee', STF.MSTF_MST_ID, STF.REC_ID AS ID , " & Common.Rec_Detail("STF") & "" & _
                                        " FROM       MAGAZINE_SUBS_TYPE_FEE      AS STF " & _
                                        " Where STF.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND STF.MSTF_CEN_ID=" & inBasicParam.openCenID.ToString & " " & Param.OtherCondition & " ORDER BY STF.REC_ID,STF.MSTF_EFF_DATE DESC "
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE, onlineQuery, ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE.ToString(), inBasicParam)
        End Function

        Public Shared Function GetSubscriptionTypeFee(ByVal Param As Param_GetFee_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "select TOP 1 MSTF_MST_ID,MSTF_INDIAN_FEE,MSTF_FOREIGN_FEE,MSTF_EFF_DATE,REC_ID FROM MAGAZINE_SUBS_TYPE_FEE WHERE  REC_STATUS IN (0,1,2) AND MSTF_MST_ID = '" & Param.ID & "' AND CAST(MSTF_EFF_DATE AS DATE) <='" & Format(Param.StartDate, Common.Server_Date_Format_Short) & "'  ORDER BY MSTF_EFF_DATE DESC "
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE, onlineQuery, ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_DispatchType(ByVal Param As Param_GetList_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT DT.MDT_NAME as 'Name',DT.REC_ID AS ID ,DT.MDT_DEFAULT AS 'Default', " & Common.Rec_Detail("DT") & "" & _
                                        " FROM       MAGAZINE_DISPATCH_TYPE      AS DT " & _
                                        " Where DT.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") "
            If inBasicParam.screen <> ConnectOneWS.ClientScreen.Facility_Magazine_Request Then
                onlineQuery += " AND DT.MDT_CEN_ID=" & inBasicParam.openCenID.ToString & ""
            End If
            onlineQuery += Param.OtherCondition & " ORDER BY DT.MDT_NAME "
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, onlineQuery, ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_DispatchTypeCharges(ByVal Param As Param_GetList_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT CONVERT(varchar ,DTC.MDTC_EFF_DATE,106)  AS 'Effective Date',DTC.MDTC_CHARGES AS 'Charges',DTC.MDTC_MDT_ID,DTC.REC_ID AS ID , " & Common.Rec_Detail("DTC") & "" &
                                        " FROM       MAGAZINE_DISPATCH_TYPE_CHARGES      AS DTC " &
                                        " Where DTC.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND DTC.MDTC_CEN_ID=" & inBasicParam.openCenID.ToString & " " & Param.OtherCondition & " ORDER BY DTC.MDTC_EFF_DATE DESC "
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES, onlineQuery, ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES.ToString(), inBasicParam)
        End Function

        Public Shared Function GetDispatchTypeCharges(ByVal Param As Param_GetFee_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "select TOP 1 MDTC_MDT_ID,MDTC_CHARGES,MDTC_EFF_DATE,REC_ID FROM MAGAZINE_DISPATCH_TYPE_CHARGES WHERE  REC_STATUS IN (0,1,2) AND MDTC_MDT_ID = '" & Param.ID & "' AND CAST(MDTC_EFF_DATE AS DATE) <='" & Format(Param.StartDate, Common.Server_Date_Format_Short) & "'  ORDER BY MDTC_EFF_DATE DESC "
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES, onlineQuery, ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_GeetaPathshala(ByVal Param As Param_GetList_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT GP.GP_NAME as 'Name',C.CEN_NAME AS 'Connecting Centre Name',GP.GP_ADD1 AS 'Address Line.1',GP.GP_ADD2 AS 'Address Line.2',GP.GP_ADD3 AS 'Address Line.3',GP.GP_ADD4 AS 'Address Line.4',CT.CI_NAME as 'City', ST.ST_NAME as 'State', DI.DI_NAME as 'District', CO.CO_NAME as 'Country',GP.GP_PINCODE AS 'Pincode'," & _
                                        " GP.GP_TEL_NO_1  + ' ' + GP.GP_TEL_NO_2 as 'Tel.No(s)',GP.GP_FAX_NO_1  + ' ' + GP.GP_FAX_NO_2 as 'Fax.No(s)',GP.GP_MOB_NO_1  + ' ' + GP.GP_MOB_NO_2 as 'Mobile No(s)',GP.GP_EMAIL_ID_1 as 'Email ID', GP.REC_ID AS ID ," & Common.Rec_Detail("GP") & "" & _
                                        " FROM      MAGAZINE_GP_INFO      AS GP " & _
                                        " LEFT JOIN MAP_CITY_INFO         AS CT     ON (GP.GP_CITY_ID      = CT.REC_ID AND CT.REC_STATUS IN (0,1,2) ) " & _
                                        " LEFT JOIN MAP_STATE_INFO        AS ST     ON (GP.GP_STATE_ID     = ST.REC_ID AND ST.REC_STATUS IN (0,1,2) ) " & _
                                        " LEFT JOIN MAP_DISTRICT_INFO     AS DI     ON (GP.GP_DISTRICT_ID  = DI.REC_ID AND DI.REC_STATUS IN (0,1,2) ) " & _
                                        " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (GP.GP_COUNTRY_ID   = CO.REC_ID AND CO.REC_STATUS IN (0,1,2) ) " & _
                                        " LEFT JOIN CENTRE_INFO           AS C      ON (GP.GP_CC_CEN_ID    = C.CEN_ID  AND C.REC_STATUS  IN (0,1,2) )" & _
                                        " Where  GP.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND GP.GP_CEN_ID=" & inBasicParam.openCenID.ToString & " " & Param.OtherCondition & " ORDER BY GP.GP_NAME "
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_GP_INFO, onlineQuery, ConnectOneWS.Tables.MAGAZINE_GP_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetList_Centres(ByVal Param As Param_GetList_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT CI.CEN_NAME as CC_NAME, 'Centre/Sub Centre' as 'CC_Type', CT.CI_NAME AS CC_CITY, ST.ST_NAME AS CC_STATE, DI.DI_NAME AS CC_DISTRICT, CO.CO_NAME AS CC_COUNTRY, CI.CEN_PINCODE AS CC_PINCODE,CI.CEN_ID AS CC_CEN_ID, CI.CEN_UID AS CC_CEN_UID , " & _
                                       " NULL AS GP_REC_ID, CI.REC_ID AS CC_ID, CI.REC_ID AS VALUE  " & _
                                       " FROM      CENTRE_INFO           AS CI " & _
                                       " LEFT JOIN MAP_CITY_INFO         AS CT     ON (CI.CEN_CITY_ID      = CT.REC_ID AND CT.REC_STATUS IN (0,1,2) ) " & _
                                       " LEFT JOIN MAP_STATE_INFO        AS ST     ON (CI.CEN_STATE_ID     = ST.REC_ID AND ST.REC_STATUS IN (0,1,2) ) " & _
                                       " LEFT JOIN MAP_DISTRICT_INFO     AS DI     ON (CI.CEN_DISTRICT_ID  = DI.REC_ID AND DI.REC_STATUS IN (0,1,2) ) " & _
                                       " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (CI.CEN_COUNTRY_ID   = CO.REC_ID AND CO.REC_STATUS IN (0,1,2) ) " & _
                                       " WHERE CI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                       " AND CI.CEN_INS_ID='00001' " & _
                                       " union all " & _
                                       " SELECT (S.SP_SERVICE_PLACE_NAME + ' ( ' + C.CEN_NAME  + ' ) ') as CC_NAME  , 'Pathshala' as 'CC_Type', CT.CI_NAME AS CC_CITY, ST.ST_NAME AS CC_STATE, DI.DI_NAME AS CC_DISTRICT, CO.CO_NAME AS CC_COUNTRY, A.C_R_PINCODE AS CC_PINCODE, C.CEN_ID AS CC_CEN_ID, C.CEN_UID AS CC_CEN_UID ,  " & _
                                       " S.REC_ID AS GP_REC_ID, C.REC_ID AS CC_ID, S.REC_ID AS VALUE  " & _
                                       " FROM       SERVICE_PLACE_INFO    AS S " & _
                                       " INNER JOIN ADDRESS_BOOK          AS A    ON (A.REC_ID = S.SP_PLACEAT_AB_ID   AND  A.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                       " LEFT JOIN  MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID         AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                       " LEFT JOIN  MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID         AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                       " LEFT JOIN  MAP_DISTRICT_INFO     AS DI   ON (A.C_R_DISTRICT_ID = DI.REC_ID         AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                       " LEFT JOIN  MAP_COUNTRY_INFO      AS CO   ON (A.C_R_COUNTRY_ID  = CO.REC_ID         AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                                       " LEFT JOIN  CENTRE_INFO           AS C    ON (S.SP_CEN_ID    = C.CEN_ID  AND C.REC_STATUS  IN (0,1,2) )" & _
                                       " WHERE S.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                                       "   AND UPPER(S.SP_SERVICE_PLACE_TYPE)  ='PATHSHALA'  AND A.C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & _
                                       " ORDER BY CC_Type ,CI.CEN_NAME "
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, onlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMembers(ByVal Param As Param_GetMembers_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""

            If Param.MemberType = 0 Then  '0=Centre/Sub-Centre
                If Param.Use_Rec_ID = True Then
                    Param.SearchCondition = " ( CI.REC_ID LIKE '%" & Param.SearchStr & "%' AND CI.CEN_INS_ID='00001' )"
                Else
                    Param.SearchCondition = " ( CI.CEN_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR (CASE WHEN CI.CEN_NAME IS NULL THEN '' ELSE CI.CEN_NAME + ' ' END  + CASE WHEN CI.CEN_ADD1 IS NULL THEN '' ELSE CI.CEN_ADD1 + ' ' END + CASE WHEN CI.CEN_ADD2 IS NULL THEN '' ELSE CI.CEN_ADD2 + ' ' END + CASE WHEN CI.CEN_ADD3 IS NULL THEN '' ELSE CI.CEN_ADD3  + ' ' END + CASE WHEN CI.CEN_ADD4 IS NULL THEN '' ELSE CI.CEN_ADD4 + ' '+ COALESCE(CT.CI_NAME,'')+ '-'+COALESCE(CI.CEN_PINCODE,'') END)  LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR CI.CEN_UID LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR CT.CI_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                            "  )  AND CI.CEN_INS_ID='00001' "
                End If

                Query = " SELECT CI.CEN_NAME as MEM_NAME, CT.CI_NAME AS MEM_CITY, ST.ST_NAME AS MEM_STATE, DI.DI_NAME AS MEM_DISTRICT, CO.CO_NAME AS MEM_COUNTRY, CI.CEN_PINCODE AS MEM_PINCODE,CASE WHEN CI.CEN_NAME IS NULL THEN '' ELSE CI.CEN_NAME + ' ' END  + CASE WHEN CI.CEN_ADD1 IS NULL THEN '' ELSE CI.CEN_ADD1 + ' ' END + CASE WHEN CI.CEN_ADD2 IS NULL THEN '' ELSE CI.CEN_ADD2 + ' ' END + CASE WHEN CI.CEN_ADD3 IS NULL THEN '' ELSE CI.CEN_ADD3  + ' ' END + CASE WHEN CI.CEN_ADD4 IS NULL THEN ' ,' ELSE CI.CEN_ADD4 + ' ,' + COALESCE(CT.CI_NAME,'')+ '-'+COALESCE(CI.CEN_PINCODE,'') END AS MEM_ADDRESS, CI.CEN_ID AS MEM_CEN_ID, CI.CEN_UID AS MEM_CEN_UID, '' AS MEM_CEN_NAME,'' as Magazine, '' as MagID, '' as MS_ID, " & _
                        " CI.REC_ID AS MEM_ID  " & _
                        " FROM      CENTRE_INFO      AS CI " & _
                        " LEFT JOIN MAP_CITY_INFO         AS CT     ON (CI.CEN_CITY_ID      = CT.REC_ID AND CT.REC_STATUS IN (0,1,2) ) " & _
                        " LEFT JOIN MAP_STATE_INFO        AS ST     ON (CI.CEN_STATE_ID     = ST.REC_ID AND ST.REC_STATUS IN (0,1,2) ) " & _
                        " LEFT JOIN MAP_DISTRICT_INFO     AS DI     ON (CI.CEN_DISTRICT_ID  = DI.REC_ID AND DI.REC_STATUS IN (0,1,2) ) " & _
                        " LEFT JOIN MAP_COUNTRY_INFO      AS CO 	ON (CI.CEN_COUNTRY_ID   = CO.REC_ID AND CO.REC_STATUS IN (0,1,2) ) " & _
                        " WHERE CI.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                        "   AND " & Param.SearchCondition & " " & _
                        " ORDER BY CI.CEN_NAME "

                Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, Query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
            End If

            If Param.MemberType = 1 Then  '1=GeetaPathshala
                If Param.Use_Rec_ID = True Then
                    Param.SearchCondition = " ( S.REC_ID LIKE '%" & Param.SearchStr & "%'  )"
                Else
                    Param.SearchCondition = " ( S.SP_SERVICE_PLACE_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR (CASE WHEN A.C_NAME IS NULL THEN '' ELSE A.C_NAME + ' ' END  + CASE WHEN A.C_R_ADD1 IS NULL THEN '' ELSE A.C_R_ADD1 + ' ' END + CASE WHEN A.C_R_ADD2 IS NULL THEN '' ELSE A.C_R_ADD2 + ' ' END + CASE WHEN A.C_R_ADD3 IS NULL THEN '' ELSE A.C_R_ADD3  + ' ' END + CASE WHEN A.C_R_ADD4 IS NULL THEN '' ELSE A.C_R_ADD4 + ' '+ ' ,' + COALESCE(CT.CI_NAME,'')+ '-' + COALESCE(A.C_R_PINCODE,'') END)  LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR C.CEN_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR C.CEN_UID LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR CT.CI_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                            "  ) AND UPPER(S.SP_SERVICE_PLACE_TYPE)  ='PATHSHALA'  AND A.C_COD_YEAR_ID = '" & inBasicParam.openYearID & "' "
                End If

                Query = " SELECT S.SP_SERVICE_PLACE_NAME as MEM_NAME, C.CEN_NAME AS MEM_CEN_NAME,CASE WHEN A.C_NAME IS NULL THEN '' ELSE A.C_NAME + ' ' END  + CASE WHEN A.C_R_ADD1 IS NULL THEN '' ELSE A.C_R_ADD1 + ' ' END + CASE WHEN A.C_R_ADD2 IS NULL THEN '' ELSE A.C_R_ADD2 + ' ' END + CASE WHEN A.C_R_ADD3 IS NULL THEN '' ELSE A.C_R_ADD3  + ' ' END + CASE WHEN A.C_R_ADD4 IS NULL THEN '' ELSE A.C_R_ADD4 + ' ,' + COALESCE(CT.CI_NAME,'')+ '-' + COALESCE(A.C_R_PINCODE,'') END AS 'MEM_ADDRESS',S.SP_CEN_ID AS MEM_CEN_ID,C.REC_ID AS MEM_CEN_REC_ID, C.CEN_UID AS MEM_CEN_UID, C.CEN_INCHARGE AS 'MEM_CEN_INCHARGE',CT.CI_NAME AS MEM_CITY, ST.ST_NAME AS MEM_STATE, DI.DI_NAME AS MEM_DISTRICT, CO.CO_NAME AS MEM_COUNTRY, A.C_R_PINCODE AS MEM_PINCODE,'' as Magazine, '' as MagID, '' as MS_ID, " & _
                        " S.REC_ID AS MEM_ID  " & _
                        " FROM       SERVICE_PLACE_INFO    AS S " & _
                        " INNER JOIN ADDRESS_BOOK          AS A    ON (A.REC_ID = S.SP_PLACEAT_AB_ID   AND  A.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN  MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID         AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN  MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID         AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN  MAP_DISTRICT_INFO     AS DI   ON (A.C_R_DISTRICT_ID = DI.REC_ID         AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN  MAP_COUNTRY_INFO      AS CO   ON (A.C_R_COUNTRY_ID  = CO.REC_ID         AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN  CENTRE_INFO           AS C    ON (S.SP_CEN_ID    = C.CEN_ID  AND C.REC_STATUS  IN (0,1,2) )" & _
                        " WHERE S.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                        "   AND " & Param.SearchCondition & " " & _
                        " ORDER BY S.SP_SERVICE_PLACE_NAME "

                Return dbService.List(ConnectOneWS.Tables.SERVICE_PLACE_INFO, Query, ConnectOneWS.Tables.SERVICE_PLACE_INFO.ToString(), inBasicParam)
            End If


            If Param.MemberType = 2 Then  '2=Individual
                If Param.Use_Rec_ID = True Then
                    Param.SearchCondition = " ( A.C_ORG_REC_ID LIKE '%" & Param.SearchStr & "%' AND A.C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")"
                Else
                    Param.SearchCondition = " ( A.C_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR (CASE WHEN A.C_NAME IS NULL THEN '' ELSE A.C_NAME + ' ' END  + CASE WHEN A.C_R_ADD1 IS NULL THEN '' ELSE A.C_R_ADD1 + ' ' END + CASE WHEN A.C_R_ADD2 IS NULL THEN '' ELSE A.C_R_ADD2 + ' ' END + CASE WHEN A.C_R_ADD3 IS NULL THEN '' ELSE A.C_R_ADD3  + ' ' END + CASE WHEN A.C_R_ADD4 IS NULL THEN '' ELSE A.C_R_ADD4 + ' '+ COALESCE(CT.CI_NAME,'')+ '-' + COALESCE(A.C_R_PINCODE,'') END)  LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR C.CEN_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR C.CEN_UID LIKE '%" & Param.SearchStr & "%' " & _
                                            " OR CT.CI_NAME LIKE '%" & Param.SearchStr & "%' " & _
                                            "  ) AND A.C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " "
                End If

                Query = " SELECT A.C_NAME as MEM_NAME, CT.CI_NAME AS MEM_CITY, ST.ST_NAME AS MEM_STATE, DI.DI_NAME AS MEM_DISTRICT, CO.CO_NAME AS MEM_COUNTRY, A.C_R_PINCODE AS MEM_PINCODE,CASE WHEN A.C_NAME IS NULL THEN '' ELSE A.C_NAME + ' ' END  + CASE WHEN A.C_R_ADD1 IS NULL THEN '' ELSE A.C_R_ADD1 + ' ' END + CASE WHEN A.C_R_ADD2 IS NULL THEN '' ELSE A.C_R_ADD2 + ' ' END + CASE WHEN A.C_R_ADD3 IS NULL THEN '' ELSE A.C_R_ADD3  + ' ' END + CASE WHEN A.C_R_ADD4 IS NULL THEN ' ,' ELSE A.C_R_ADD4 + ' ,' + COALESCE(CT.CI_NAME,'')+ '-' + COALESCE(A.C_R_PINCODE,'') END AS 'MEM_ADDRESS',A.C_CLASS_CEN_ID AS MEM_CEN_ID, C.CEN_UID AS MEM_CEN_UID, C.CEN_NAME AS MEM_CEN_NAME, '' as Magazine, '' as MagID,'' as MS_ID, " & _
                        " A.C_ORG_REC_ID AS MEM_ID  " & _
                        " FROM ADDRESS_BOOK               AS A    " & _
                        " LEFT JOIN CENTRE_INFO           AS C    ON (C.CEN_ID          = A.C_CLASS_CEN_ID  AND A.C_CEN_CATEGORY = 0 AND C.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN MAP_CITY_INFO         AS CT   ON (A.C_R_CITY_ID     = CT.REC_ID         AND CT.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN MAP_STATE_INFO        AS ST   ON (A.C_R_STATE_ID    = ST.REC_ID         AND ST.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN MAP_DISTRICT_INFO     AS DI   ON (A.C_R_DISTRICT_ID = DI.REC_ID         AND DI.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " LEFT JOIN MAP_COUNTRY_INFO      AS CO   ON (A.C_R_COUNTRY_ID  = CO.REC_ID         AND CO.REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") )" & _
                        " WHERE A.REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")" & _
                        "   AND A.C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " " & _
                        "   AND " & Param.SearchCondition & " " & _
                        " ORDER BY A.C_NAME "

                Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
            End If
        End Function

        Public Shared Function GetExistingMembers(SearchString As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_ExistingMagazineMembersList"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "SEARCH", "@UserID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, SearchString, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 255, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_Membership(ByVal Param As Param_Get_MagazineMembershipList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_MagazineMembershipList"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "MS_ID", "MS_OLD_ID", "MEMBER_NAME", "MEMBER_TYPE", "PREV_YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Membership_ID, Param.Membership_Old_ID, Param.Member_Name, IIf(Param.MemberType = "", DBNull.Value, Param.MemberType), Param.PrevYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 50, 50, 255, 20, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_Magazine_Dues(ByVal PrevYearID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_MagazineDueList"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "PREV_YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, PrevYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_ConnectedMembership(mem_type As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_MagazineConnectedMembershipList"
            Dim params() As String = {"CEN_ID", "MEM_TYPE"}
            Dim values() As Object = {inBasicParam.openCenID, mem_type}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 30}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_RelatedMembership(ByVal Param As Param_Get_MagazineMembershipList, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_RelatedMagazineMembershipList"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "MS_ID", "MS_OLD_ID", "MEMBER_NAME", "MEMBER_TYPE", "RELATED_MS_REC_ID", "PREV_YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Membership_ID, Param.Membership_Old_ID, Param.Member_Name, IIf(Param.MemberType = "", DBNull.Value, Param.MemberType), IIf(Param.RelatedMemberRecID = "", DBNull.Value, Param.RelatedMemberRecID), IIf(Param.PrevYearID = Nothing, DBNull.Value, Param.PrevYearID)}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 50, 50, 255, 20, 36, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_Address_Magazine(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Magazine_Address"
            Dim params() As String = {"AB_ID"}
            Dim values() As Object = {Rec_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.ADDRESS_BOOK, SPName, ConnectOneWS.Tables.ADDRESS_BOOK.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_Issues(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Magazine_Issue_Info"
            Dim params() As String = {"CEN_ID", "YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetIssueCount(ByVal Param As Param_GetIssueCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim FutureIssues As Int16 = 0
            Dim Query As String = "SELECT COUNT(MII_ISSUE_DATE) FROM magazine_issue_info WHERE MII_MI_ID = '" & Param.MagID & "' AND MII_ISSUE_DATE BETWEEN '" & Param.ToDate.ToString(Common.Server_Date_Format_Long) & "' AND '" & Param.ToDate.AddDays(30).ToString(Common.Server_Date_Format_Long) & "'"
            FutureIssues = dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, Query, ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO.ToString(), inBasicParam)

            If FutureIssues = 0 Then
                Throw New Exception("|Sorry!! Issues not available in given period. Calculated Cost may not be Correct.|")
            End If

            Dim returnValue As Integer = 0
            If Param.GetCurrYearIssuesOnly Then
                Query = "SELECT MAX(MII_ISSUE_DATE) FROM MAGAZINE_ISSUE_INFO AS MII WHERE MII_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND MII_CEN_ID = " & inBasicParam.openCenID.ToString & ""
                Dim CURR_YR_MAX_ISS_DATE As DateTime = dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, Query, ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO.ToString(), inBasicParam)

                Query = "SELECT COUNT(MII_ISSUE_DATE) FROM magazine_issue_info WHERE MII_MI_ID = '" & Param.MagID & "' AND MII_ISSUE_DATE BETWEEN '" & Param.FromDate.ToString(Common.Server_Date_Format_Long) & "' AND '" & IIf(Param.ToDate > CURR_YR_MAX_ISS_DATE, CURR_YR_MAX_ISS_DATE.ToString(Common.Server_Date_Format_Long), Param.ToDate.ToString(Common.Server_Date_Format_Long)) & "'"
                returnValue = dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, Query, ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO.ToString(), inBasicParam)
            Else
                Query = "SELECT COUNT(MII_ISSUE_DATE) FROM magazine_issue_info WHERE MII_MI_ID = '" & Param.MagID & "' AND MII_ISSUE_DATE BETWEEN '" & Param.FromDate.ToString(Common.Server_Date_Format_Long) & "' AND '" & Param.ToDate.ToString(Common.Server_Date_Format_Long) & "'"
                returnValue = dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, Query, ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO.ToString(), inBasicParam)
            End If

            Return returnValue
        End Function

        Public Shared Function GetList_SubCities(param As Param_GetList_SubCities, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Subcity_Listing"
            Dim params() As String = {"REC_ID", "CITY_ID"}
            Dim values() As Object = {IIf(param.Rec_ID = "", Nothing, param.Rec_ID), IIf(param.City_ID = "", Nothing, param.City_ID)}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAP_SUB_CITY_INFO, SPName, ConnectOneWS.Tables.MAP_SUB_CITY_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetMapping_SubCities(param As Param_GetMapping_SubCities, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_City_Mapping"
            Dim params() As String = {"Search", "StateID"}
            Dim values() As Object = {param.searchString, param.stateID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {500, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAP_SUB_CITY_INFO, SPName, ConnectOneWS.Tables.MAP_SUB_CITY_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_Dispatches(ByVal Issue_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_rpt_Magazine_Dispatch_Info"
            Dim params() As String = {"MAG_ISSUE_ID"}
            Dim values() As Object = {Issue_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Get_Dispatch_Details(ByVal param As Parameter_Get_Dispatch_Details, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Magazine_Dispatch_Details"
            Dim params() As String = {"MII_ID", "MS_ID"}
            Dim values() As Object = {param.Issue_ID, param.Membership_Rec_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetList_MagazineDispatchRegister(param As Parameter_GetList_MagazineDispatchRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Magazine_Dispatch_Register"
            Dim params() As String = {"CENID", "YEARID", "MEM_ID", "MEM_OLD_ID", "MAG_ISSUE_DATE", "MAG_ID", "DISP_MEM_ID", "PREV_YEAR_ID", "CUT_OFF_TIME"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, param.Membership_ID, param.Membership_Old_ID, param.Issue_Date, param.Magazine, param.Disp_Membership_ID, param.Prev_Year_Id, IIf(param.CutOffTime = Nothing, Nothing, param.CutOffTime)}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.DateTime2}
            Dim lengths() As Integer = {5, 4, 50, 50, 50, 36, 50, 4, 50}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetRecord_MembershipProfile(ByVal MS_REC_id As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT MM.*,MM_CC.*,MMB_MST_ID,MMB_DATE,MMB_PERIOD_FR, MMB_PERIOD_TO,MMB_COPIES,MMB_FREE_COPIES,MMB_MDT_ID,MMB_AMOUNT,MMB_SUBS_AMT,MMB_DISPATCH_AMT,MMB_CC_DISPATCH, CC.MM_MEMBER_TYPE AS CC_MEMBER_TYPE FROM  MAGAZINE_MEMBERSHIP_INFO AS MM INNER JOIN MAGAZINE_MEMBERSHIP_BALANCES_INFO MMB ON MM.REC_ID = MMB.MMB_MS_ID AND MM. REC_ID = '" & MS_REC_id & "' INNER JOIN (SELECT TOP 1 REC_ID FROM magazine_membership_balances_info WHERE  MMB_MS_ID = '" & MS_REC_id & "' AND MMB_BAL_TYPE IN ('SUBS', 'OPENING') ORDER BY MMB_COD_YEAR_ID, REC_ADD_ON DESC) AS MAX_MMB ON MMB.REC_ID = MAX_MMB.REC_ID LEFT OUTER JOIN MAGAZINE_MEMBERSHIP_CC_INFO AS MM_CC ON MM.REC_INT_ID = MM_CC.MM_INT_ID AND MMB_COD_YEAR_ID = MM_CC.MM_COD_YEAR_ID LEFT OUTER JOIN magazine_membership_info AS CC ON MM_CC.MM_CC_MS_ID = CC.REC_ID"
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, Query, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetNewMembershipNo(ByVal Mag_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_NewMagazineMembershipNo"
            Dim params() As String = {"MAG_ID"}
            Dim values() As Object = {Mag_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString, params, values, dbTypes, lengths, inBasicParam).Rows(0)(0)
        End Function

        Public Shared Function GetList_ReceiptRegister(ByVal Param As Param_get_MagazineMembershipRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_MagazineMembershipRegister"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "YR_START_DATE", "FROM_DATE", "TO_DATE", "TR_M_ID", "PREV_YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.YR_START_DATE, Param.FROM_DATE, Param.TO_DATE, Param.Tr_m_Id, Param.Prev_Year_Id}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Date, Data.DbType.Date, Data.DbType.Date, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 7, 7, 7, 36, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetMagazineCountByName(ByVal param As param_GetMagazineCountByName, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM Magazine_Info  WHERE REC_STATUS IN (0,1,2) AND MI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(MI_NAME)  = '" & param.Name.ToUpper & "' AND REC_ID <>'" & param.REC_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_INFO, Query, ConnectOneWS.Tables.MAGAZINE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMagazineCountByShortName(ByVal param As param_GetMagazineCountByShortName, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM Magazine_Info  WHERE REC_STATUS IN (0,1,2) AND MI_CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(MI_SHORT_NAME)  = '" & param.ShortName.ToUpper & "' AND REC_ID <>'" & param.REC_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_INFO, Query, ConnectOneWS.Tables.MAGAZINE_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMagazineSubsTypeCountByName(ByVal param As param_GetMagazineSubTypeCountByName, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM magazine_subs_type  WHERE REC_STATUS IN (0,1,2) AND MST_CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(MST_TYPE)  = '" & param.Name.ToUpper & "' AND MST_MI_ID ='" & param.Magazine_ID & "' AND REC_ID <>'" & param.REC_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, Query, ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMagazineSubsTypeCountByShortName(ByVal param As param_GetMagazineSubTypeCountByShortName, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM magazine_subs_type  WHERE REC_STATUS IN (0,1,2) AND MST_CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(MST_SHORT_NAME)  = '" & param.ShortName.ToUpper & "' AND MST_MI_ID ='" & param.Magazine_ID & "' AND REC_ID <>'" & param.REC_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, Query, ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMagazineSubsFeeCountByEffDate(ByVal param As param_GetMagazineSubFeeCountByEffDate, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM magazine_subs_type_fee  WHERE REC_STATUS IN (0,1,2) AND MSTF_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MSTF_EFF_DATE  = '" & Convert.ToDateTime(param.EffDate).ToString(Common.Server_Date_Format_Short) & "' AND MSTF_MST_ID ='" & param.SubsType_ID & "' AND REC_ID <>'" & param.REC_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE, Query, ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMagazineDispatchCountByName(ByVal param As param_GetMagazineDispatchCountByName, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM magazine_dispatch_type  WHERE REC_STATUS IN (0,1,2) AND MDT_CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(MDT_NAME)  = '" & param.Name.ToUpper & "' AND REC_ID <>'" & param.REC_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, Query, ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMagazineDispatchFeeCountByEffDate(ByVal param As param_GetMagazineDispatchFeeCountByEffDate, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM magazine_dispatch_type_charges  WHERE REC_STATUS IN (0,1,2) AND MDTC_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MDTC_EFF_DATE  = '" & Convert.ToDateTime(param.EffDate).ToString(Common.Server_Date_Format_Short) & "' AND MDTC_MDT_ID ='" & param.DispatchType_ID & "' AND REC_ID <>'" & param.REC_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES, Query, ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMembershipCountByMemberID(ByVal param As param_GetMembershipCountByMemberID, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM magazine_membership_info  WHERE REC_STATUS IN (0,1,2) AND MM_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MM_MEMBER_ID  = '" & param.MemberID & "' AND MM_MI_ID  = '" & param.Magazine_ID & "' AND REC_ID <>'" & param.REC_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, Query, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Last Entry Date
        ''' </summary>
        ''' <param name="Mem_Rec_Id"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetLastEntryDate(ByVal Mem_Rec_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT MAX(MMB_DATE) AS LAST_EDT FROM MAGAZINE_MEMBERSHIP_BALANCES_INFO WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MMB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MMB_MS_ID = '" & Mem_Rec_Id & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, onlineQuery, ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMagazinesRestrictionDate(ByVal User_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT CR_TODATE FROM so_client_restrictions where CR_TYPE='MAG_DATE_BLOCKED' AND CR_USER_ID = '" & User_Id & "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.SO_CLIENT_RESTRICTIONS, onlineQuery, ConnectOneWS.Tables.SO_CLIENT_RESTRICTIONS.ToString(), inBasicParam)
        End Function

        Public Shared Function GetMagazinesIssues(ByVal Mag_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = "SELECT MII_ISSUE_DATE as Issue, REC_ID as ID FROM MAGAZINE_ISSUE_INFO WHERE MII_MI_ID ='" & Mag_ID & "' ORDER BY MII_ISSUE_DATE"
            Return dbService.List(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, onlineQuery, ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO.ToString(), inBasicParam)
        End Function

        '#11446 
        'SELECT COUNT(*) FROM MAGAZINE_INFO WHERE MI_NAME ='" & Param.Mag_Name & "' 
        'AND REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MI_CEN_ID='" & inBasicParam.openCenID & "' AND REC_ID NOT IN ('" & Param.Rec_Id & "') 

        'SELECT COUNT(*) FROM MAGAZINE_SUBS_INFO WHERE MST_TYPE ='" & Param.subs_type & "' 
        'AND REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MST_CEN_ID='" & inBasicParam.openCenID & "' AND REC_ID NOT IN ('" & Param.Rec_Id & "')   AND MST_MI_ID  = '" & Param.Mag_Id  & "'

        'SELECT COUNT(*) FROM MAGAZINE_DISPATCH_TYPE WHERE MDT_NAME ='" & Param.subs_type & "' 
        'AND REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MDT_CEN_ID='" & inBasicParam.openCenID & "' AND REC_ID NOT IN ('" & Param.Rec_Id & "') 

        'SELECT COUNT(*) FROM MAGAZINE_ISSUE_INFO WHERE MII_ISSUE_NO ='" & Param.ISSUE_NO & "' 
        'AND REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MII_CEN_ID='" & inBasicParam.openCenID & "' AND REC_ID NOT IN ('" & Param.Rec_Id & "') 
        'AND MII_MI_ID  = '" & Param.Mag_Id  & "'
        '
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO Magazine_info(MI_CEN_ID,MI_COD_YEAR_ID,MI_NAME,MI_SHORT_NAME,MI_LANGUAGE,MI_MAG_REGD_NO,MI_POSTAL_REGD_NO,MI_PUBLISH_ON,MI_FS_APPLICABLE,MI_MS_START_NO,MI_COPIES_PER_YEAR," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.Name & "', " & _
                                                  "'" & InParam.ShortName & "', " & _
                                                  "'" & InParam.Language & "', " & _
                                                  "'" & InParam.Magazine_Regd_No & "', " & _
                                                  "'" & InParam.Postal_Regd_No & "', " & _
                                                  "'" & InParam.PublishOn & "', " & _
                                                  "'" & InParam.FS_Applicable & "', " & _
                                                  " " & InParam.MS_Start_No & " , " & _
                                                  " " & InParam.Copies_Per_Year & " , " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.MAGAZINE_INFO, OnlineQuery, inBasicParam, RecID)
            Dim MI_RecID As String = RecID
            RecID = Guid.NewGuid.ToString
            OnlineQuery = "INSERT INTO Magazine_Subs_Type(MST_CEN_ID,MST_COD_YEAR_ID,MST_MI_ID,MST_SR_NO,MST_TYPE,MST_SHORT_NAME,MST_START_MONTH,MST_MIN_MONTHS,MST_FEE_PERIOD_WISE,MST_FIXED_PERIOD," & _
                                                 "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                 ") VALUES(" & _
                                                 "" & inBasicParam.openCenID.ToString & "," & _
                                                 "" & inBasicParam.openYearID.ToString & "," & _
                                                 "'" & MI_RecID & "', " & _
                                                 " 1, " & _
                                                 "'Single Copy', " & _
                                                 "'SC', " & _
                                                 " " & Month(InParam.Year_Sdt) & " , " & _
                                                 " 1 , " & _
                                                 "'NO', " & _
                                                 "'YES', " & _
                                                 "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, OnlineQuery, inBasicParam, RecID)

            Return True
        End Function

        Public Shared Function Insert_Magazine_Subs_Type(ByVal InParam As Parameter_Insert_Magazine_Subs_Type, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO MAGAZINE_SUBS_TYPE(MST_CEN_ID,MST_COD_YEAR_ID,MST_MI_ID,MST_SR_NO,MST_TYPE,MST_SHORT_NAME,MST_START_MONTH,MST_MIN_MONTHS,MST_FEE_PERIOD_WISE,MST_FIXED_PERIOD," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.Mag_ID & "', " & _
                                                  " ( select isnull(max(mst_sr_no),0) + 1 FROM  magazine_subs_type  where mst_mi_id = '" & InParam.Mag_ID & "' and MST_CEN_ID='" & inBasicParam.openCenID & "' and REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") ) , " & _
                                                  "'" & InParam.Name & "', " & _
                                                  "'" & InParam.ShortName & "', " & _
                                                  " " & InParam.StartMonth & " , " & _
                                                  " " & InParam.MinMonth & " , " & _
                                                  "'" & InParam.PeriodWise & "', " & _
                                                  "'" & InParam.FixedPeriod & "', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, OnlineQuery, inBasicParam, RecID)

            Return True
        End Function

        Public Shared Function Insert_Magazine_Subs_Type_Fee(ByVal InParam As Parameter_Insert_Magazine_Subs_Type_Fee, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            'Dim OnlineQuery As String = "INSERT INTO MAGAZINE_SUBS_TYPE_FEE(MSTF_CEN_ID,MSTF_COD_YEAR_ID,MSTF_MST_ID,MSTF_EFF_DATE,MSTF_INDIAN_FEE,MSTF_FOREIGN_FEE," & _
            '                                      "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
            '                                      ") VALUES(" & _
            '                                      "" & inBasicParam.openCenID.ToString & "," & _
            '                                      "" & inBasicParam.openYearID.ToString & "," & _
            '                                      "'" & InParam.ST_ID & "', " & _
            '                                      " " & If(IsDate(InParam.Eff_Date), "'" & Convert.ToDateTime(InParam.Eff_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
            '                                      " " & InParam.Indian_Fee & " , " & _
            '                                      " " & InParam.Foreign_Fee & " , " & _
            '                                      "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            'dbService.Insert(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE, OnlineQuery, inBasicParam, RecID)

            Dim SPName As String = "sp_Insert_MagazineSubscription_Type_Fee"
            Dim params() As String = {"@CEN_ID", "@YEAR_ID", "@ST_ID", "@Eff_Date", "@Indian_Fee", "@Foreign_Fee", "@USER_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, InParam.ST_ID, InParam.Eff_Date, InParam.Indian_Fee, InParam.Foreign_Fee, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Date, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 36, 12, 19, 19, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Insert_Magazine_Dispatch_Type(ByVal InParam As Parameter_Insert_Magazine_Dispatch_Type, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO Magazine_Dispatch_Type(MDT_CEN_ID,MDT_COD_YEAR_ID,MDT_NAME," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.Name & "', " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, OnlineQuery, inBasicParam, RecID)

            Return True
        End Function

        Public Shared Function Insert_Magazine_Dispatch_Type_Charges(ByVal InParam As Parameter_Insert_Magazine_Dispatch_Type_Charges, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO Magazine_Dispatch_Type_Charges(MDTC_CEN_ID,MDTC_COD_YEAR_ID,MDTC_MDT_ID,MDTC_EFF_DATE,MDTC_CHARGES," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.DT_ID & "', " & _
                                                  " " & If(IsDate(InParam.Eff_Date), "'" & Convert.ToDateTime(InParam.Eff_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                                  " " & InParam.Charges & " , " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES, OnlineQuery, inBasicParam, RecID)

            Return True
        End Function

        Public Shared Function Insert_Magazine_Membership(ByVal InParam As Parameter_Insert_Magazine_Membership_Profile, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            're-generate membership id.................................\
            'Dim MS_NO As Object = dbService.Wrap_GetSingleValue(ConnectOneWS.RealServiceFunctions.Magazine_GetNewMembershipNo, inBasicParam, InParam.Mag_ID)

            Dim SPName As String = "sp_Insert_MagazineMembership_Profile"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "MS_ID", "MEMBER_TYPE", "MEMBER_START_DATE", "MS_OLD_ID", "MEMBER_ID", "CC_APPLICABLE", "CC_MS_ID", "CC_SPONSORED", "CC_DISPATCH", "MAG_ID", "MAG_CATEGORY", "OTHER_DETAILS", "User_ID", "SUB_ID", "PERIOD_FROM_DATE", "PERIOD_TO_DATE", "MEM_COPIES", "MEM_DISPATCH_ID", "BAL_AMOUNT", "MMB_SUBS_AMT", "MMB_DISPATCH_AMT", "MEM_FREE_COPIES", "NEXT_YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, InParam.Mem_ID, InParam.Member_Type, InParam.MS_Start_Date, InParam.Mem_Old_ID, InParam.Member_Address_GP_ID, InParam.CC_Applicable, IIf(InParam.CC_MS_ID = "", DBNull.Value, InParam.CC_MS_ID), InParam.CC_Sponsored, InParam.CC_Dispatch, InParam.Mag_ID, InParam.Category, InParam.OtherDetails, inBasicParam.openUserID, InParam.MST_ID, InParam.Period_Fr, InParam.Period_To, InParam.Copies, InParam.MDT_ID, InParam.Total_Bal_Amt, InParam.Subs_Amt, InParam.Dispatch_Amt, InParam.FreeCopies, InParam.Next_Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.Date, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 50, 50, 15, 50, 36, 3, 36, 1, 3, 36, 10, 255, 255, 36, 15, 15, 0, 36, 19, 19, 19, 7, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Insert_Magazine_Issue(ByVal InParam As Param_Insert_Magazine_Issue, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_Magazine_Issue"
            Dim params() As String = {"@CEN_ID", "@YEAR_ID", "@MAG_ID", "@ISSUE_DATE", "@ISSUE_PART_NO", "@ISSUE_VOL_NO", "@ISSUE_NO", "@USER_ID",
                                      "@BUNDLE_REG_SIZE", "@BUNDLE_REG_SIZE_FGN", "@PER_COPY_WEIGHT", "@BUNDLE_MAX_WEIGHT", "@BUNDLE_MAX_WEIGHT_FGN", "@RPC_SEED", "@REG_EXP_IND", "@REG_EXP_FGN"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, InParam.MAG_ID, InParam.ISSUE_DATE, InParam.ISSUE_PART_NO, InParam.ISSUE_VOL_NO, InParam.ISSUE_NO, inBasicParam.openUserID,
                                      InParam.BUNDLE_REG_SIZE, InParam.BUNDLE_REG_SIZE_FGN, InParam.PER_COPY_WEIGHT, InParam.BUNDLE_MAX_WEIGHT, InParam.BUNDLE_MAX_WEIGHT_FGN, InParam.RPC_SEED, InParam.REG_EXP_IND, InParam.REG_EXP_FGN}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Date, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String,
                                                   Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal}
            Dim lengths() As Integer = {5, 4, 36, 12, 10, 10, 10, 255, 10, 10, 9, 9, 9, 10, 9, 9}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Insert_Magazine_Similar_Issues(ByVal InParam As Param_Insert_Magazine_Similar_Issues, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_SimilarMagazineIssues"
            Dim params() As String = {"@YEAR_ID", "@ISSUE_ID", "@YEARS", "@USER_ID"}
            Dim values() As Object = {inBasicParam.openYearID, InParam.ISSUE_ID, InParam.YrCount, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 4, 20}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Insert_Magazine_SubCity(ByVal InParam As Parameter_Insert_Magazine_Subcity, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_Subcity"
            Dim params() As String = {"COUNTRY_REC_ID", "STATE_REC_ID", "CITY_REC_ID", "DISTRICT_REC_ID", "SUB_CITY", "PINCODE", "USER_ID"}
            Dim values() As Object = {InParam.COUNTRY_REC_ID, InParam.STATE_REC_ID, InParam.CITY_REC_ID, InParam.DISTRICT_REC_ID, InParam.SUB_CITY, InParam.PINCODE, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {36, 36, 36, 36, 255, 20, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.MAP_SUB_CITY_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Insert_Magazine_Dispatch(ByVal InParam As Parameter_Insert_Magazine_Dispatch, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_Magazine_Dispatch"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "DISP_ID", "ISSUE_ID", "MS_ID", "DATE", "STATUS", "USER_ID", "TR_ID", "COPIES", "REMARKS", "RPC_ID", "PKT_NO"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, InParam.Dispatch_ID, InParam.Issue_ID, InParam.Membership_ID, InParam.DispatchDate, InParam.Status, inBasicParam.openUserID, InParam.Tr_ID, InParam.Copies, InParam.Remarks, InParam.RPC_ID, InParam.PKT_NO}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 36, 36, 36, 7, 20, 255, 36, 5, 8000, 100, 5}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Insert_Dispatches_for_new_Voucher(ByVal InParam As Parameter_Insert_dispatch_New_Voucher, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT REC_ID FROM magazine_issue_info  WHERE MII_MI_ID  = '" & InParam.MagID & "' AND MII_ISSUE_DATE BETWEEN '" & InParam.FromDate.ToString(Common.Server_Date_Format_Long) & "' and '" & InParam.ToDate.ToString(Common.Server_Date_Format_Long) & "'"
            Dim Issues As DataTable = dbService.List(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, Query, ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO.ToString(), inBasicParam)

            Query = "SELECT REC_ID FROM magazine_dispatch_type  WHERE MDT_NAME = 'by hand' AND MDT_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            Dim ByHandDispID As String = dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, Query, ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE.ToString(), inBasicParam)

            For Each cRow As DataRow In Issues.Rows
                Dim inDispatch As Parameter_Insert_Magazine_Dispatch = New Parameter_Insert_Magazine_Dispatch
                inDispatch.Issue_ID = cRow(0)
                inDispatch.Membership_ID = InParam.MembershipID
                inDispatch.PKT_NO = 0
                inDispatch.Remarks = ""
                inDispatch.RPC_ID = Nothing
                inDispatch.Status = "DELIVERED"
                inDispatch.Tr_ID = Guid.NewGuid.ToString
                inDispatch.Copies = InParam.subsCopies
                inDispatch.DispatchDate = InParam.subDate
                inDispatch.Dispatch_ID = ByHandDispID
                Insert_Magazine_Dispatch(inDispatch, inBasicParam)
            Next
            Return True
        End Function

        Public Shared Function Insert_Magazine_Dispatch_Bundles(ByVal InParam As Parameter_Insert_Magazine_Dispatch_Bundles, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_Magazine_Dispatch_Bundles"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "ISSUE_ID", "MS_ID", "COPIES", "REMARKS", "BUNDLE_NO"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, InParam.Issue_ID, InParam.Membership_ID, InParam.Copies, InParam.Remarks, InParam.Bundle_No}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 36, 36, 7, 255, 7}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_DISPATCH_BUNDLES, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Add_Magazine_Restrictions(ByVal InParam As Parameter_Insert_Magazine_Client_Restriction, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "CR_TYPE = 'MAG_DATE_BLOCKED' AND CR_USER_ID ='" & InParam.UserID & "'"
            dbService.DeleteByCondition(ConnectOneWS.Tables.SO_CLIENT_RESTRICTIONS, OnlineQuery, inBasicParam)

            OnlineQuery = "INSERT INTO [dbo].[so_client_restrictions] ([CR_CEN_ID],[CR_SCREEN],[CR_FROMDATE], [CR_TODATE],[CR_TYPE], [CR_EVENT_ID],[CR_USER_ID]" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "NULL," & _
                                                  "'2014-04-01', " & _
                                                  "'" & Convert.ToDateTime(InParam.RestrictedTill).ToString(Common.Server_Date_Format_Long) & "'," & _
                                                  "'MAG_DATE_BLOCKED', " & _
                                                  "NULL, " & _
                                                  "'" & InParam.UserID & "'" & _
                                                  ")"
            dbService.Insert(ConnectOneWS.Tables.SO_CLIENT_RESTRICTIONS, OnlineQuery, inBasicParam, RecID)

            Return True
        End Function

        Public Shared Function Update_Magazine_Dispatch(ByVal InParam As Parameter_Update_Magazine_Dispatch, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_Magazine_Dispatch"
            Dim params() As String = {"DISP_ID", "ISSUE_ID", "MS_ID", "DATE", "STATUS", "USER_ID", "TR_ID", "COPIES", "REMARKS"}
            Dim values() As Object = {InParam.Dispatch_ID, InParam.Issue_ID, InParam.Membership_ID, InParam.DispatchDate, InParam.Status, inBasicParam.openUserID, InParam.Tr_ID, InParam.Copies, InParam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {36, 36, 36, 7, 20, 255, 36, 10, 8000}
            dbService.UpdateBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_SubCity(ByVal UpParam As Parameter_Update_Magazine_Subcity, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Try
                Dim SPName As String = "sp_Update_Subcity"
                Dim params() As String = {"COUNTRY_REC_ID", "STATE_REC_ID", "CITY_REC_ID", "DISTRICT_REC_ID", "SUB_CITY", "PINCODE", "USER_ID", "REC_ID"}
                Dim values() As Object = {UpParam.COUNTRY_REC_ID, UpParam.STATE_REC_ID, UpParam.CITY_REC_ID, UpParam.DISTRICT_REC_ID, UpParam.SUB_CITY, UpParam.PINCODE, inBasicParam.openUserID, UpParam.Rec_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
                Dim lengths() As Integer = {36, 36, 36, 36, 255, 20, 255, 36}
                dbService.UpdateBySP(ConnectOneWS.Tables.MAP_SUB_CITY_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return True
        End Function

        Public Shared Function Update(ByVal UpParam As Parameter_Update_Magazine, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " UPDATE MAGAZINE_INFO SET " & _
                                         "MI_NAME           ='" & UpParam.Name & "', " & _
                                         "MI_SHORT_NAME     ='" & UpParam.ShortName & "', " & _
                                         "MI_LANGUAGE       = '" & UpParam.Language & "', " & _
                                         "MI_MAG_REGD_NO    = '" & UpParam.Magazine_Regd_No & "', " & _
                                         "MI_POSTAL_REGD_NO ='" & UpParam.Postal_Regd_No & "', " & _
                                         "MI_PUBLISH_ON     ='" & UpParam.PublishOn & "', " & _
                                         "MI_FS_APPLICABLE  ='" & UpParam.FS_Applicable & "', " & _
                                         "MI_MS_START_NO    = " & UpParam.MS_Start_No & ", " & _
                                         " " & _
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Subs_Type(ByVal UpParam As Parameter_Update_Magazine_Subs_Type, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " UPDATE MAGAZINE_SUBS_TYPE SET " & _
                                        " MST_TYPE              ='" & UpParam.Name & "', " & _
                                        " MST_SHORT_NAME        ='" & UpParam.ShortName & "', " & _
                                        " MST_START_MONTH       = " & UpParam.StartMonth & ", " & _
                                        " MST_MIN_MONTHS        = " & UpParam.MinMonth & ", " & _
                                        " MST_FEE_PERIOD_WISE   ='" & UpParam.PeriodWise & "', " & _
                                        " MST_FIXED_PERIOD      ='" & UpParam.FixedPeriod & "', " & _
                                        " " & _
                                        " REC_EDIT_ON           ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_EDIT_BY           ='" & inBasicParam.openUserID & "' " & _
                                        " WHERE REC_ID          ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Subs_Type_Fee(ByVal UpParam As Parameter_Update_Magazine_Subs_Type_Fee, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            'Dim OnlineQuery As String = " UPDATE MAGAZINE_SUBS_TYPE_FEE SET " & _
            '                            " MSTF_EFF_DATE     = " & If(IsDate(UpParam.Eff_Date), "'" & Convert.ToDateTime(UpParam.Eff_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
            '                            " MSTF_INDIAN_FEE   ='" & UpParam.Indian_Fee & "', " & _
            '                            " MSTF_FOREIGN_FEE  = '" & UpParam.Foreign_Fee & "', " & _
            '                            " " & _
            '                            " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
            '                            " REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
            '                            " WHERE REC_ID      ='" & UpParam.Rec_ID & "'"
            'dbService.Update(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE, OnlineQuery, inBasicParam)
            Try
                Dim SPName As String = "sp_Update_Magazine_Subscription_Type_Fee"
                Dim params() As String = {"REC_ID", "MSTF_EFF_DATE", "MSTF_INDIAN_FEE", "MSTF_FOREIGN_FEE", "USER_ID"}
                Dim values() As Object = {UpParam.Rec_ID, UpParam.Eff_Date, UpParam.Indian_Fee, UpParam.Foreign_Fee, inBasicParam.openUserID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.DateTime, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String}
                Dim lengths() As Integer = {36, 12, 19, 19, 255}
                dbService.UpdateBySP(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return True
        End Function

        Public Shared Function Update_Magazine_Dispatch_Type(ByVal UpParam As Parameter_Update_Magazine_Dispatch_Type, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " UPDATE Magazine_Dispatch_type SET " & _
                                         "MDT_NAME           ='" & UpParam.Name & "', " & _
                                         " " & _
                                         "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                         "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                         "  WHERE REC_ID    ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Dispatch_Type_Charges(ByVal UpParam As Parameter_Update_Magazine_Dispatch_Type_Charges, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " UPDATE MAGAZINE_DISPATCH_TYPE_CHARGES SET " & _
                                        " MDTC_EFF_DATE     = " & If(IsDate(UpParam.Eff_Date), "'" & Convert.ToDateTime(UpParam.Eff_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        " MDTC_CHARGES   ='" & UpParam.Charges & "', " & _
                                        " " & _
                                        " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                        " WHERE REC_ID      ='" & UpParam.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Membership(ByVal UpParam As Parameter_Update_Magazine_Membership_Profile, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            If UpParam.Next_Year_ID = 0 Then UpParam.Next_Year_ID = Nothing
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_MagazineMembership_Profile"
            Dim params() As String = {"MMB_REC_ID", "MS_REC_ID", "YEAR_ID", "MS_NO", "MS_ID", "MEMBER_TYPE", "MEMBER_START_DATE", "MS_OLD_ID", "MEMBER_ID", "CC_APPLICABLE", "CC_MS_ID", "CC_SPONSORED", "CC_DISPATCH", "MAG_ID", "MAG_CATEGORY", "OTHER_DETAILS", "User_ID", "SUB_ID", "PERIOD_FROM_DATE", "PERIOD_TO_DATE", "MEM_COPIES", "MEM_DISPATCH_ID", "BAL_AMOUNT", "MMB_SUBS_AMT", "MMB_DISPATCH_AMT", "MEM_FREE_COPIES", "NEXT_YEAR_ID"}
            Dim values() As Object = {UpParam.MMB_ID, UpParam.Rec_ID, inBasicParam.openYearID, UpParam.MS_No, UpParam.Mem_ID, UpParam.Member_Type, UpParam.MS_Start_Date, UpParam.Mem_Old_ID, UpParam.Member_Address_GP_ID, UpParam.CC_Applicable, IIf(UpParam.CC_MS_ID = "", DBNull.Value, UpParam.CC_MS_ID), UpParam.CC_Sponsored, UpParam.CC_Dispatch, UpParam.Mag_ID, UpParam.Category, UpParam.OtherDetails, inBasicParam.openUserID, UpParam.MST_ID, UpParam.Period_Fr, UpParam.Period_To, UpParam.Copies, UpParam.MDT_ID, UpParam.Total_Amt, UpParam.Subs_Amt, UpParam.Dispatch_Amt, UpParam.FreeCopies, UpParam.Next_Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.Date, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {36, 36, 4, 0, 50, 50, 15, 50, 36, 3, 36, 1, 3, 36, 10, 255, 255, 36, 15, 15, 0, 36, 19, 19, 19, 7, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Membership_Identity(ByVal UpParam As Parameter_Update_Magazine_Membership_Identity, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_MagazineMembership_Identity"
            Dim params() As String = {"MS_REC_ID", "MEMBER_TYPE", "MS_OLD_ID", "MEMBER_ID", "User_ID", "MAG_ID", "CC_SPONSORED"}
            Dim values() As Object = {UpParam.Rec_ID, UpParam.Member_Type, UpParam.Mem_Old_ID, UpParam.Member_ID, inBasicParam.openUserID, UpParam.MagID, UpParam.CC_sponsored}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Boolean}
            Dim lengths() As Integer = {36, 50, 50, 36, 255, 36, 1}
            dbService.UpdateBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Issue(ByVal InParam As Param_Update_Magazine_Issue, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_Magazine_Issue"
            Dim params() As String = {"@ISSUE_DATE", "@ISSUE_PART_NO", "@ISSUE_VOL_NO", "@ISSUE_NO", "@USER_ID",
                                      "@BUNDLE_REG_SIZE", "@BUNDLE_REG_SIZE_FGN", "@PER_COPY_WEIGHT", "@BUNDLE_MAX_WEIGHT", "@BUNDLE_MAX_WEIGHT_FGN", "@RPC_SEED", "@REG_EXP_IND", "@REG_EXP_FGN", "ISSUE_ID"}
            Dim values() As Object = {InParam.ISSUE_DATE, InParam.ISSUE_PART_NO, InParam.ISSUE_VOL_NO, InParam.ISSUE_NO, inBasicParam.openUserID,
                                      InParam.BUNDLE_REG_SIZE, InParam.BUNDLE_REG_SIZE_FGN, InParam.PER_COPY_WEIGHT, InParam.BUNDLE_MAX_WEIGHT, InParam.BUNDLE_MAX_WEIGHT_FGN, InParam.RPC_SEED, InParam.REG_EXP_IND, InParam.REG_EXP_FGN, InParam.ISSUE_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Date, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String,
                                                   Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {12, 10, 10, 10, 255, 10, 10, 9, 9, 9, 10, 9, 9, 36}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Delete_Magazine_Dispatch_Type(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param)
            Try
                Dim dbService As ConnectOneWS = New ConnectOneWS()
                Dim SPName As String = "sp_Delete_MagazineDispatchType"
                Dim params() As String = {"REC_ID"}
                Dim values() As Object = {Rec_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
                Dim lengths() As Integer = {36}
                dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function Delete_Magazine_Dispatch_Type_Charges(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param)
            Try
                Dim dbService As ConnectOneWS = New ConnectOneWS()
                Dim SPName As String = "sp_Delete_MagazineDispatchTypeCharges"
                Dim params() As String = {"REC_ID"}
                Dim values() As Object = {Rec_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
                Dim lengths() As Integer = {36}
                dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE_CHARGES, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function Delete_Magazine(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param)
            Try
                Dim dbService As ConnectOneWS = New ConnectOneWS()
                Dim SPName As String = "sp_Delete_Magazine"
                Dim params() As String = {"REC_ID"}
                Dim values() As Object = {Rec_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
                Dim lengths() As Integer = {36}
                dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function Delete_Magazine_Subscription_Type(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param)
            Try
                Dim dbService As ConnectOneWS = New ConnectOneWS()
                Dim SPName As String = "sp_Delete_Magazine_Subscription_Type"
                Dim params() As String = {"REC_ID"}
                Dim values() As Object = {Rec_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
                Dim lengths() As Integer = {36}
                dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function Delete_Magazine_Subscription_Type_Fee(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param)
            Try
                Dim dbService As ConnectOneWS = New ConnectOneWS()
                Dim SPName As String = "sp_Delete_Magazine_Subscription_Type_Fee"
                Dim params() As String = {"REC_ID"}
                Dim values() As Object = {Rec_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
                Dim lengths() As Integer = {36}
                dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE_FEE, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function Delete_Magazine_Membership_Profile(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param)
            Try
                Dim dbService As ConnectOneWS = New ConnectOneWS()
                Dim SPName As String = "sp_Delete_Magazine_Membership_Profile"
                Dim params() As String = {"REC_ID"}
                Dim values() As Object = {Rec_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
                Dim lengths() As Integer = {36}
                dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function Delete_Magazine_SubCity(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param)
            Try
                Dim dbService As ConnectOneWS = New ConnectOneWS()
                Dim SPName As String = "sp_Delete_Subcity"
                Dim params() As String = {"REC_ID"}
                Dim values() As Object = {Rec_ID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
                Dim lengths() As Integer = {36}
                dbService.DeleteFromSP(ConnectOneWS.Tables.MAP_SUB_CITY_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function

        Public Shared Function Delete_Magazine_Dispatch(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Delete_Magazine_Dispatch"
            Dim params() As String = {"REC_ID"}
            Dim values() As Object = {Rec_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Delete_Magazine_Issues(ByVal Issue_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_delete_Magazine_Issue_Info"
            Dim params() As String = {"ISSUE_ID"}
            Dim values() As Object = {Issue_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        ''' <summary>
        ''' Close
        ''' </summary>
        ''' <param name="Cls"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Close</remarks>
        Public Shared Function Close(ByVal Cls As Membership.Parameter_Close_Membership, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Magazine_Membership_info SET " & _
                                        "MM_CLOSE_DATE     = " & If(IsDate(Cls.CloseDate), "'" & Convert.ToDateTime(Cls.CloseDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " & _
                                        "MM_CLOSE_REMARKS  ='" & Cls.Reason & "', " & _
                                        " " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                        "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                        "  WHERE REC_ID    ='" & Cls.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        ''' <summary>
        ''' Reopens membership
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_Reopen</remarks>
        Public Shared Function Reopen(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Magazine_Membership_info SET " & _
                                        "MM_CLOSE_DATE     = NULL, " & _
                                        "MM_CLOSE_REMARKS  = NULL, " & _
                                        " " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                        "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                        "  WHERE REC_ID    ='" & Rec_ID & "'"

            dbService.Update(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Set_Default_Magazine_Subscription(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " UPDATE MAGAZINE_SUBS_TYPE SET MST_DEFAULT = 0 WHERE MST_MI_ID = (SELECT MST_MI_ID FROM MAGAZINE_SUBS_TYPE WHERE REC_ID    ='" & Rec_ID & "' ) "
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, OnlineQuery, inBasicParam)

            OnlineQuery = " UPDATE MAGAZINE_SUBS_TYPE SET " & _
                            "MST_DEFAULT     = 1, " & _
                            " " & _
                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                            "  WHERE REC_ID    ='" & Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Remove_Default_Magazine_Subscription(MagID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE MAGAZINE_SUBS_TYPE SET MST_DEFAULT = 0  WHERE MST_MI_ID = '" & MagID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Set_Default_Magazine_Dispatch(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " UPDATE MAGAZINE_DISPATCH_TYPE SET MDT_DEFAULT = 0  WHERE MDT_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, OnlineQuery, inBasicParam)

            OnlineQuery = " UPDATE MAGAZINE_DISPATCH_TYPE SET " & _
                        "MDT_DEFAULT     = 1, " & _
                        " " & _
                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                        "  WHERE REC_ID    ='" & Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Remove_Default_Magazine_Dispatch(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE MAGAZINE_DISPATCH_TYPE SET MDT_DEFAULT = 0 WHERE MDT_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_DISPATCH_TYPE, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Set_Default_Magazine_Issue(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = " UPDATE MAGAZINE_ISSUE_INFO SET MII_DEFAULT = 0 WHERE MII_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, OnlineQuery, inBasicParam)

            OnlineQuery = " UPDATE MAGAZINE_ISSUE_INFO SET " & _
                        "MII_DEFAULT     = 1, " & _
                        " " & _
                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                        "  WHERE REC_ID    ='" & Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Remove_Default_Magazine_Issue(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE MAGAZINE_ISSUE_INFO SET MII_DEFAULT = 0  WHERE MII_CEN_ID = " & inBasicParam.openCenID.ToString & ""
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_ISSUE_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function ConsiderForAutoRenewal(ByVal Cls As Parameter_AutoRenewal_Membership, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Magazine_Membership_info SET " & _
                                        "MM_AUTO_RENEWAL  =" & IIf(Cls.ConsiderForAutoRenewal, 1, 0) & ", " & _
                                        " " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                        "REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                        "REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                        "  WHERE REC_ID    ='" & Cls.Rec_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function Settle_Magazine_Ledgers(inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[sp_Txn_Magazine_Ledger_Final_Settlement]"
            Dim params() As String = {"@CENID", "@YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            dbService.DeleteFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

    End Class

#End Region

#Region "--Register--"
    <Serializable>
    Public Class Voucher_Magazine_Register
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_InsertMaster_VoucherMagazineMembership
            Public TxnCode As Integer
            Public VNo As String
            Public TDate As String
            Public PartyID As String
            Public Ref_ID As String
            Public SubTotal As Double
            Public Cash As Double
            Public Bank As Double
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Insert_VoucherMagazineMembership
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public SUB_Cr_Led_ID As String
            Public SUB_Dr_Led_ID As String
            Public Amount As Double
            Public Mode As String
            Public Ref_BANK_ID As String
            Public Ref_Branch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_CDate As String
            Public Party1 As String
            Public Narration As String
            Public Reference As String
            Public MasterTxnID As String
            Public SrNo As String
            Public Status_Action As String
            Public RecID As String
            Public CrossRefId As String = ""
            Public MSRecId As String = ""
            Public Next_Year_ID As String
            'Public openYearID As String   ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_GetVoucherDetails_OnMemberSelection
            Public MS_RecId As String
            Public Txn_M_Id As String
            Public Mag_ID As String
            Public Subs_Start_Date As Date
            Public Category As String
            Public Prev_Year_ID As Integer
        End Class
        <Serializable>
        Public Class Parameter_InsertBalances_VoucherMagazineMembership
            Public CEN_ID As Integer
            Public YEAR_ID As Integer
            'Public MS_NO As Integer = 0
            Public MS_ID As String = ""
            Public MEM_TYPE As String
            Public MEM_START_DATE As Date
            Public MS_OLD_ID As String
            Public MEMBER_ID As String
            Public CC_APPLICABLE As String
            Public CC_MS_ID As String = ""
            Public CC_SPONSORED As Boolean
            Public CC_DISPATCH As String
            Public MAG_ID As String
            Public MAG_CATEGORY As String
            Public OTHER_DETAILS As String
            Public User_ID As String
            Public SUB_ID As String
            Public FROM_DATE As Date = DateTime.MinValue
            Public TO_DATE As Date = DateTime.MinValue
            Public MEM_COPIES As Integer = 0
            Public MEM_FREE As Integer = 0
            Public MEM_DISPATCH_ID As String
            Public BAL_AMOUNT As Double = 0
            Public MMB_SUBS_AMT As Double = 0
            Public MMB_DISPATCH_AMT As Double = 0
            Public MMB_BALANCE_TYPE As String = ""
            'Public Rec_Id As String
            'Public Status_Action As String
            Public Tr_Id As String
            Public Tr_Sr_No As Int32
            Public MS_REC_ID As String
            Public MS_NO As Int64 = 0
            Public NEXT_YEAR_ID As Int32
        End Class
        <Serializable>
        Public Class Parameter_InsertPayment_VoucherMagazineMembership
            Public TxnMID As String
            Public Type As String
            Public Pmt_Date As String
            Public SrNo As String
            Public Mode As String
            Public RefID As String
            Public RefBranch As String
            Public RefNo As String
            Public RefDate As String
            Public ClearingDate As String
            Public RefAmount As Double
            Public Dep_BA_ID As String
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_GetMembers_VoucherMagazine
            Public Cen_Rec_Id As String '--REC_ID OF SEARCHED CENTER
            Public SP_Rec_Id As String
            Public AB_Org_RecId As String   '--ORGIGINAL RECID OF SEARCHED ADDRESS
            Public SearchString As String
            Public SearchType As String
            Public SearchCondition As String
            Public Mem_Type As String
            Public Use_Rec_ID As String
            Public Magazine_ID As String
            Public Prev_Year_ID As Integer
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_VoucherMagazineMembership
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Double
            Public SrNo As Integer
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherMagazineMembership
            'Public Param_InsertMaster As Parameter_InsertMaster_VoucherMagazineMembership
            Public Param_InsertMaster() As Parameter_InsertMaster_VoucherMagazineMembership
            Public Param_Insert_Payment_Txn() As Parameter_Insert_VoucherMagazineMembership
            Public Param_Insert_Subs_Txn() As Parameter_Insert_VoucherMagazineMembership
            'Public Param_InsertMembership As Parameter_InsertBalances_VoucherMagazineMembership
            Public Param_InsertSubsBalances() As Parameter_InsertBalances_VoucherMagazineMembership
            Public Param_InsertPmtBalances() As Parameter_InsertBalances_VoucherMagazineMembership
            Public Param_InsertPurpose_subs() As Parameter_InsertPurpose_VoucherMagazineMembership
            Public Param_InsertPurpose_Pmt() As Parameter_InsertPurpose_VoucherMagazineMembership
            Public Param_InsertPayment() As Parameter_InsertPayment_VoucherMagazineMembership
            Public Param_Update_Membership As Param_Update_Magazine_Membership
            Public Param_DeletedVouchers As String()
            Public Param_Update_Memebership_Balances As Param_Update_Magazine_Membership_Balances
            Public Param_Insert_dispatch_New_Voucher As Magazine.Parameter_Insert_dispatch_New_Voucher
        End Class
        <Serializable>
        Public Class Param_InsertReceipt_Magazine_Receipt_Register
            Public M_ID As String
            Public VDate As String
            Public MDate As String
            Public openYearSdt As Date
            Public openUserID As String
            Public MEMBER_ID As String
            Public MEMBER_TYPE As String
        End Class
        <Serializable>
        Public Class Param_DeleteReceipt_Magazine_Receipt_Register
            Public Reason As String
            Public Rec_Id As String
        End Class
        <Serializable>
        Public Class Param_Update_Magazine_Membership
            Public MM_MS_OLD_ID As String = ""
            Public MM_CC_APPLICABLE As String = ""
            Public MM_OTHER_DETAIL As String = ""
            Public MM_CC_MS_ID As String = ""
            Public MS_REC_ID As String = ""
            Public MM_CC_SPONSORED As Boolean
            Public PartyID As String
        End Class
        <Serializable>
        Public Class Param_Update_Magazine_Membership_Balances
            Public MMB_FROM_DATE As DateTime = Nothing
            Public MMB_TO_DATE As DateTime = Nothing
            Public MMB_AMT As Decimal
            Public MMB_COPIES As Integer
            Public MMB_FREE_COPIES As Integer
            Public MMB_CC_DISPATCH As String = ""
            Public MMB_MDT_ID As String = ""
            Public MMB_MST_ID As String = ""
            Public REC_ID As String = ""
        End Class
        <Serializable>
        Public Class Param_GetPayeeLedger
            Public MS_ID As String
            Public Prev_year_ID As Integer
        End Class
        <Serializable>
        Public Class Param_GetMagazineAccLedger
            Public Payee_AB_ID As String
            Public Prev_year_ID As Integer
            Public LedgerID As String = ""
        End Class
        <Serializable>
        Public Class Param_Update_Magazine_Disp_CC
            Public DispOnCC As Boolean
            Public Membership_Balances_ID As String
        End Class
#End Region

        Public Shared Function GetMembers(Param As Parameter_GetMembers_VoucherMagazine, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If Param.Mem_Type.ToUpper = "CENTRE / SUB-CENTRE" Then  '0=Centre/Sub-Centre
                If Param.Use_Rec_ID = True Then
                    Param.Cen_Rec_Id = Param.SearchString
                End If
            End If
            If Param.Mem_Type.ToUpper = "GEETA PATHASHALA" Then  '1 =Geeta Pathshala
                If Param.Use_Rec_ID = True Then
                    Param.SP_Rec_Id = Param.SearchString
                End If
            End If
            If Param.Mem_Type.ToUpper = "INDIVIDUAL" Then  '2=Individual
                If Param.Use_Rec_ID = True Then
                    Param.AB_Org_RecId = Param.SearchString
                End If
            End If
            Dim SPName As String = "sp_get_SearchMagazineMember"
            Dim params() As String = {"CEN_ID", "CEN_REC_ID", "SP_REC_ID", "AB_ORG_REC_ID", "YEAR_ID", "PREV_YEAR_ID", "SEARCH_STRING", "MEMBER_TYPE", "MAG_ID", "SEARCH_TYPE"}
            Dim values() As Object = {inBasicParam.openCenID, Param.Cen_Rec_Id, Param.SP_Rec_Id, Param.AB_Org_RecId, inBasicParam.openYearID, Param.Prev_Year_ID, Param.SearchString, IIf(Param.Mem_Type.Length > 0, Param.Mem_Type, Nothing), Param.Magazine_ID, Param.SearchType}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 36, 36, 36, 4, 4, 255, 30, 36, 30}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetPayeeLedger(inParam As Param_GetPayeeLedger, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_MagazinePayeeLedger"
            Dim params() As String = {"MS_ID", "YEAR_ID", "PREV_YEAR_ID"}
            Dim values() As Object = {inParam.MS_ID, inBasicParam.openYearID, inParam.Prev_year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {36, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetMagazineAccLedger(inParam As Param_GetMagazineAccLedger, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Get_Magazine_Acc_Ledger"
            Dim params() As String = {"@CENID", "@YEARID", "@PREV_YEARID", "@MEMBER_ID", "@LED_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inParam.Prev_year_ID, inParam.Payee_AB_ID, inParam.LedgerID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 36, 5}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetVoucherDetails_OnMemberSelection(Param As Parameter_GetVoucherDetails_OnMemberSelection, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_MagazineVoucher_Details"
            Dim params() As String = {"CENID", "YEARID", "MS_RECID", "TR_M_ID", "MAG_ID", "SUBS_START_DATE", "CATEGORY", "PREVYEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.MS_RecId, Param.Txn_M_Id, Param.Mag_ID, Param.Subs_Start_Date, Param.Category, Param.Prev_Year_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4, 36, 36, 36, 8, 10, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Get Discontinued
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Membership_GetDiscontinued</remarks>
        Public Shared Function GetDiscontinued(ByVal Param As Membership.Param_GetDiscontinued_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = ""
            If Param.ByMasterID Then
                Dim Query As String = " SELECT DISTINCT TR_REF_ID FROM TRANSACTION_D_MASTER_INFO WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_ID ='" & Param.Id & "' "
                onlineQuery = " SELECT CASE WHEN MM_CLOSE_DATE IS NULL THEN 'Continue' ELSE 'Discontinued' END FROM MAGAZINE_MEMBERSHIP_INFO WHERE  REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MM_CEN_ID=" & inBasicParam.openCenID.ToString & " " & _
                              " AND REC_ID IN ('" & dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam) & "') "
            Else
                onlineQuery = " SELECT CASE WHEN MM_CLOSE_DATE IS NULL THEN 'Continue' ELSE 'Discontinued' END FROM MAGAZINE_MEMBERSHIP_INFO WHERE  REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND MM_CEN_ID=" & inBasicParam.openCenID.ToString & " AND REC_ID = '" & Param.Id & "' "
            End If
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, onlineQuery, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Receipt Count
        ''' </summary>
        ''' <param name="M_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_GetReceiptCount</remarks>
        Public Shared Function GetReceiptCount(ByVal M_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT COUNT(*) FROM MAGAZINE_RECEIPT_INFO  WHERE REC_STATUS IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ")  AND MR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND MR_TR_M_ID ='" & M_ID & "'  AND MR_CANCEL IS NULL "
            Return dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_RECEIPT_INFO, OnlineQuery, ConnectOneWS.Tables.MAGAZINE_RECEIPT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Cancelled Receipts
        ''' </summary>
        ''' <param name="MS_ID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetCancelledReceipts(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_CancelledReceiptRegister"
            Dim params() As String = {"CEN_ID", "YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="InMInfo"></param>
        ''' <param name="inBasicParam"></param>
        ''' <param name="AddTime"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Shared Function InsertMasterInfo(ByVal InMInfo As Parameter_InsertMaster_VoucherMagazineMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional SubsType As String = Nothing, Optional UpdateTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = ""
            Dim MembershipType As String = ""
            If Not SubsType Is Nothing Then
                OnlineQuery = " SELECT MST_MIN_MONTHS FROM MAGAZINE_SUBS_TYPE WHERE REC_ID = '" & SubsType & "' "
                MembershipType = dbService.GetScalar(ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE, OnlineQuery, ConnectOneWS.Tables.MAGAZINE_SUBS_TYPE.ToString(), inBasicParam)
            End If

            If AddTime = Nothing Then AddTime = DateTime.Now
            If UpdateTime = Nothing Then UpdateTime = DateTime.Now

            Dim Adv_Amt As Decimal = 0 ' aDVANCE BEING POSTED IN CASE OF LIFE SUBSCRIPTION TRANSACTION 
            If MembershipType = "0" Then Adv_Amt = InMInfo.SubTotal
            OnlineQuery = "INSERT INTO TRANSACTION_D_MASTER_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_AB_ID_1,TR_REF_ID,TR_SUB_AMT,TR_CASH_AMT,TR_BANK_AMT,TR_ADVANCE_AMT,TR_LB_AMT,TR_CREDIT_AMT,TR_TDS_AMT," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InMInfo.TxnCode & "'," &
                                                  "'" & InMInfo.VNo & "', " &
                                                  " " & If(IsDate(InMInfo.TDate), "'" & Convert.ToDateTime(InMInfo.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                  "'" & InMInfo.PartyID & "', " &
                                                  "'" & InMInfo.Ref_ID & "', " &
                                                  " " & InMInfo.SubTotal & "," &
                                                  " " & InMInfo.Cash & "," &
                                                  " " & InMInfo.Bank & "," &
                                                  " " & Adv_Amt & "," &
                                                  " " & 0 & "," &
                                                  " " & 0 & "," &
                                                  " " & 0 & "," &
                                        "'" & IIf(AddTime = Nothing, Common.DateTimePlaceHolder, AddTime.ToString(Common.DateFormatLong)) & "', '" & inBasicParam.openUserID & "', '" & IIf(UpdateTime = Nothing, Common.DateTimePlaceHolder, UpdateTime.ToString(Common.DateFormatLong)) & "', '" & inBasicParam.openUserID & "', " & InMInfo.Status_Action & ", '" & IIf(AddTime = Nothing, Common.DateTimePlaceHolder, AddTime.ToString(Common.DateFormatLong)) & "', '" & inBasicParam.openUserID & "', '" & InMInfo.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, InMInfo.RecID, InMInfo.TDate, AddTime)
            Return True
        End Function

        Private Shared Function Insert(ByVal InParam As Parameter_Insert_VoucherMagazineMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing, Optional UpdateTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            ''''
            Dim SPName As String = "sp_Insert_MagazineTxn_Profile"

            If AddTime = Nothing Then AddTime = DateTime.Now
            If UpdateTime = Nothing Then UpdateTime = DateTime.Now

            ' If Param.MEM_TYPE = "" Then Param.MEM_TYPE = Nothing ' Nullable Parameters , passed NULL if Empty
            If Not InParam.Cr_Led_ID Is Nothing Then If InParam.Cr_Led_ID.Length = 0 Then InParam.Cr_Led_ID = Nothing
            If Not InParam.Dr_Led_ID Is Nothing Then If InParam.Dr_Led_ID.Length = 0 Then InParam.Dr_Led_ID = Nothing
            If Not InParam.SUB_Cr_Led_ID Is Nothing Then If InParam.SUB_Cr_Led_ID.Length = 0 Then InParam.SUB_Cr_Led_ID = Nothing
            If Not InParam.SUB_Dr_Led_ID Is Nothing Then If InParam.SUB_Dr_Led_ID.Length = 0 Then InParam.SUB_Dr_Led_ID = Nothing
            If Not InParam.Ref_BANK_ID Is Nothing Then If InParam.Ref_BANK_ID.Length = 0 Then InParam.Ref_BANK_ID = Nothing
            If Not InParam.Ref_Branch Is Nothing Then If InParam.Ref_Branch.Length = 0 Then InParam.Ref_Branch = Nothing
            If Not InParam.Ref_No Is Nothing Then If InParam.Ref_No.Length = 0 Then InParam.Ref_No = Nothing
            If Not InParam.MasterTxnID Is Nothing Then If InParam.MasterTxnID.Length = 0 Then InParam.MasterTxnID = Nothing
            If Not InParam.Mode Is Nothing Then If InParam.Mode.Length = 0 Then InParam.Mode = Nothing
            If Not InParam.Party1 Is Nothing Then If InParam.Party1.Length = 0 Then InParam.Party1 = Nothing
            If Not InParam.SrNo Is Nothing Then If InParam.SrNo.Length = 0 Then InParam.SrNo = Nothing
            If Not InParam.CrossRefId Is Nothing Then If InParam.CrossRefId.Length = 0 Then InParam.CrossRefId = Nothing
            If Not IsDate(InParam.Ref_Date) Then InParam.Ref_Date = Nothing
            If Not IsDate(InParam.Ref_CDate) Then InParam.Ref_CDate = Nothing
            If Not IsDate(InParam.TDate) Or InParam.TDate = Date.MinValue Then InParam.TDate = Nothing
            If Not InParam.Ref_Date Is Nothing Then If InParam.Ref_Date = Date.MinValue Then InParam.Ref_Date = Nothing
            If Not InParam.Ref_CDate Is Nothing Then If InParam.Ref_CDate = Date.MinValue Then InParam.Ref_CDate = Nothing
            If Not InParam.Next_Year_ID = 0 Then InParam.Next_Year_ID = Nothing

            Dim params() As String = {"@CENID", "@YEARID", "@TransCode", "@VNo", "@TDate", "@ItemID", "@Type",
                                      "@Cr_Led_ID", "@Dr_Led_ID", "@SUB_Cr_Led_ID", "@SUB_Dr_Led_ID", "@Mode", "@Ref_BANK_ID",
                                      "@Ref_Branch", "@Ref_No", "@Ref_Date", "@Ref_CDate", "@Amount", "@PartyID", "@Narration",
                                      "@Reference", "@RECID", "@MS_RECID", "@openUserID", "@MasterTxnID", "@SrNo", "@CrossRefId", "@ADD_ON", "@EDIT_ON"}

            Dim values() As Object = {
                                        inBasicParam.openCenID, inBasicParam.openYearID, InParam.TransCode, InParam.VNo, InParam.TDate, InParam.ItemID, InParam.Type,
                                        InParam.Cr_Led_ID, InParam.Dr_Led_ID, InParam.SUB_Cr_Led_ID, InParam.SUB_Dr_Led_ID, InParam.Mode, InParam.Ref_BANK_ID,
                                        InParam.Ref_Branch, InParam.Ref_No, InParam.Ref_Date, InParam.Ref_CDate, InParam.Amount, InParam.Party1, InParam.Narration,
                                        InParam.Reference, InParam.RecID, InParam.MSRecId, inBasicParam.openUserID, InParam.MasterTxnID, InParam.SrNo, InParam.CrossRefId,
                                        AddTime, UpdateTime
                                     }
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.String,
                                                   Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                                                   Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.Date, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String,
                                                   Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime, Data.DbType.DateTime}
            Dim lengths() As Integer = {5, 4, 10, 255, 8, 36, 10,
                                        5, 5, 36, 36, 50, 36,
                                        255, 255, 8, 8, 19, 36, 2000,
                                        2000, 36, 36, 255, 36, 10, 36, 8, 8}
            dbService.InsertBySP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam, InParam.TDate)
            Return True
        End Function

        Private Shared Function InsertBalances(ByVal Param As Parameter_InsertBalances_VoucherMagazineMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param1 As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing, Optional UpdateTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_MagazineMembership_Voucher"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "MEMBER_TYPE", "MEMBER_START_DATE", "MS_OLD_ID", "MEMBER_ID", "CC_APPLICABLE", "CC_MS_ID", "CC_SPONSORED", "CC_DISPATCH", "MAG_ID", "MAG_CATEGORY", "OTHER_DETAILS", "User_ID", "SUB_ID", "PERIOD_FROM_DATE", "PERIOD_TO_DATE", "MEM_COPIES", "MEM_FREE", "MEM_DISPATCH_ID", "BAL_AMOUNT", "MMB_SUBS_AMT", "MMB_DISPATCH_AMT", "MMB_BALANCE_TYPE", "TR_M_ID", "TR_SR_NO", "MS_REC_ID", "MS_ID", "MS_NO", "NEXT_YEAR_ID", "@ADD_ON", "@EDIT_ON"}
            If Param.MEM_TYPE = "" Then Param.MEM_TYPE = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.MS_OLD_ID = "" Then Param.MS_OLD_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.MEMBER_ID = "" Then Param.MEMBER_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.CC_APPLICABLE = "" Then Param.CC_APPLICABLE = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.CC_MS_ID = "" Then Param.CC_MS_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.CC_GP_ID = "" Then Param.CC_GP_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.CC_DISPATCH = "" Then Param.CC_DISPATCH = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.MAG_ID = "" Then Param.MAG_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.SUB_ID = "" Then Param.SUB_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.MEM_START_DATE = DateTime.MinValue Then Param.MEM_START_DATE = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.FROM_DATE = DateTime.MinValue Then Param.FROM_DATE = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.TO_DATE = DateTime.MinValue Then Param.TO_DATE = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.MEM_DISPATCH_ID = "" Then Param.MEM_DISPATCH_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Tr_Id = "" Then Param.Tr_Id = Nothing
            If Param.MS_REC_ID = "" Then Param.MS_REC_ID = Nothing
            If Param.NEXT_YEAR_ID = 0 Then Param.NEXT_YEAR_ID = Nothing

            If AddTime = Nothing Then AddTime = DateTime.Now
            If UpdateTime = Nothing Then UpdateTime = DateTime.Now

            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.MEM_TYPE, IIf(Param.MEM_START_DATE = DateTime.MinValue, DBNull.Value, Param.MEM_START_DATE), Param.MS_OLD_ID, Param.MEMBER_ID,
                                     Param.CC_APPLICABLE, Param.CC_MS_ID, Param.CC_SPONSORED, Param.CC_DISPATCH, Param.MAG_ID, Param.MAG_CATEGORY,
                                      Param.OTHER_DETAILS, inBasicParam.openUserID, Param.SUB_ID, IIf(Param.FROM_DATE = DateTime.MinValue, DBNull.Value, Param.FROM_DATE), IIf(Param.TO_DATE = DateTime.MinValue, DBNull.Value, Param.TO_DATE), Param.MEM_COPIES, Param.MEM_FREE,
                                      Param.MEM_DISPATCH_ID, Param.BAL_AMOUNT, Param.MMB_SUBS_AMT, Param.MMB_DISPATCH_AMT, Param.MMB_BALANCE_TYPE, Param.Tr_Id, Param.Tr_Sr_No,
                                      Param.MS_REC_ID, Param.MS_ID, Param.MS_NO, Param.NEXT_YEAR_ID, AddTime, UpdateTime}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.String,
                                                   Data.DbType.String, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String,
                                                   Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.Date, Data.DbType.Int32, Data.DbType.Int32,
                                                   Data.DbType.String, Data.DbType.Double, Data.DbType.Double, Data.DbType.Double, Data.DbType.String, Data.DbType.String, Data.DbType.Int32,
                                                   Data.DbType.String, Data.DbType.String, Data.DbType.Int64, Data.DbType.Int32, Data.DbType.DateTime, Data.DbType.DateTime}
            Dim lengths() As Integer = {5, 4, 50, 7, 50, 36,
                                        3, 36, 1, 3, 36, 10,
                                        255, 255, 36, 7, 7, 8, 8,
                                        36, 19, 19, 19, 25, 36, 8,
                                        36, 50, 15, 4, 8, 8}
            dbService.InsertBySP(ConnectOneWS.Tables.MEMBERSHIP_BALANCES_INFO, SPName, params, values, dbTypes, lengths, inBasicParam, Param.MEM_START_DATE)
            Return True
        End Function

        Private Shared Function InsertPayment(ByVal InPmt As Parameter_InsertPayment_VoucherMagazineMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If InPmt.RefID.Length = 0 Then InPmt.RefID = "NULL" Else If Not InPmt.RefID.StartsWith("'") Then InPmt.RefID = "'" & InPmt.RefID & "'"
            If InPmt.Dep_BA_ID.Length = 0 Then InPmt.Dep_BA_ID = "NULL" Else If Not InPmt.Dep_BA_ID.StartsWith("'") Then InPmt.Dep_BA_ID = "'" & InPmt.Dep_BA_ID & "'"
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPmt.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_D_PAYMENT_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_M_ID,TR_PAY_TYPE,TR_SR_NO,TR_MODE,TR_REF_ID,TR_REF_BRANCH,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_REF_AMT,TR_DEP_BA_REC_ID," & _
                                        "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" & _
                                        ") VALUES(" & _
                                        "" & inBasicParam.openCenID.ToString & "," & _
                                        "" & inBasicParam.openYearID.ToString & "," & _
                                        "'" & InPmt.TxnMID & "', " & _
                                        "'" & InPmt.Type & "', " & _
                                        " " & InPmt.SrNo & " , " & _
                                        "'" & InPmt.Mode & "', " & _
                                        " " & InPmt.RefID & " , " & _
                                        "'" & InPmt.RefBranch & "', " & _
                                        "'" & InPmt.RefNo & "', " & _
                                        " " & If(InPmt.RefDate <> DateTime.MinValue, "'" & Convert.ToDateTime(InPmt.RefDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        " " & If(InPmt.ClearingDate <> DateTime.MinValue, "'" & Convert.ToDateTime(InPmt.ClearingDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                        " " & InPmt.RefAmount & ", " & _
                                        " " & InPmt.Dep_BA_ID & " , " & _
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPmt.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, inBasicParam, InPmt.RecID, InPmt.Pmt_Date, AddTime)
            Return True
        End Function

        Private Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_VoucherMagazineMembership, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'," & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT,TR_ITEM_SR_NO," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InPurpose.TxnID & "'," &
                                                  "'" & InPurpose.PurposeID & "', " &
                                                  " " & InPurpose.Amount & ", " &
                                                  " " & InPurpose.SrNo & ", " &
                                        "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function DeleteMagazineMembershipVoucher(Txn_M_Id As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Delete_Magazine_Membership_Voucher"
            Dim params() As String = {"TR_M_ID"}
            Dim values() As Object = {Txn_M_Id}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}

            Dim DateQuery As String = "SELECT TR_DATE FROM TRANSACTION_D_MASTER_INFO  WHERE REC_ID ='" & Txn_M_Id & "' "
            Dim TxnDate As Date = dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, DateQuery, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam)

            dbService.DeleteFromSP(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam, TxnDate, "REC_ID = '" & Txn_M_Id & "'")
            Return True
        End Function

        'Public Shared Function Insert_Magazine_Membership(ByVal InParam As Parameter_Insert_MagazineVoucher_Membership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()

        '    're-generate membership id.................................\
        '    Dim MS_NO As Object = dbService.Wrap_GetSingleValue(ConnectOneWS.RealServiceFunctions.Magazine_GetNewMembershipNo, inBasicParam, InParam.Mag_ID)

        '    Dim SPName As String = "sp_Insert_MagazineMembership_Profile"
        '    Dim params() As String = {"CEN_ID", "YEAR_ID", "MS_NO", "MS_ID", "MEMBER_TYPE", "MEMBER_START_DATE", "MS_OLD_ID", "MEMBER_ID", "CC_APPLICABLE", "CC_CEN_ID", "CC_GP_ID", "CC_DISPATCH", "MAG_ID", "MAG_CATEGORY", "OTHER_DETAILS", "User_ID", "SUB_ID", "PERIOD_FROM_DATE", "PERIOD_TO_DATE", "MEM_COPIES", "MEM_DISPATCH_ID", "BAL_AMOUNT", "MMB_SUBS_AMT", "MMB_DISPATCH_AMT"}
        '    Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, MS_NO, InParam.Mem_ID, InParam.Member_Type, InParam.MS_Start_Date, InParam.Mem_Old_ID, InParam.Member_Address_GP_ID, InParam.CC_Applicable, IIf(InParam.CC_Cen_ID = "", DBNull.Value, InParam.CC_Cen_ID), IIf(InParam.CC_GP_ID = "", DBNull.Value, InParam.CC_GP_ID), InParam.CC_Dispatch, InParam.Mag_ID, InParam.Category, InParam.OtherDetails, inBasicParam.openUserID, InParam.MST_ID, InParam.Period_Fr, InParam.Period_To, InParam.Copies, InParam.MDT_ID, InParam.Total_Bal_Amt, InParam.Subs_Amt, InParam.Dispatch_Amt}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.Date, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal}
        '    Dim lengths() As Integer = {5, 4, 0, 50, 50, 15, 50, 36, 3, 5, 36, 3, 36, 10, 255, 255, 36, 15, 15, 0, 36, 19, 19, 19}
        '    dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        '    Return True
        'End Function

        Public Shared Function Update_Magazine_Disp_CC(inParam As Param_Update_Magazine_Disp_CC, inBasicParam As ConnectOneWS.Basic_Param)
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "UPDATE magazine_membership_balances_info " & _
                                      "SET MMB_CC_DISPATCH = '" & IIf(inParam.DispOnCC, "YES", "NO") & "'" & _
                                      " WHERE [REC_ID] = '" & inParam.Membership_Balances_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_BALANCES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Membership(inParam As Param_Update_Magazine_Membership, inBasicParam As ConnectOneWS.Basic_Param)
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If Len(inParam.MM_CC_MS_ID) = 0 Then
                inParam.MM_CC_MS_ID = "NULL"
            Else
                inParam.MM_CC_MS_ID = "'" & inParam.MM_CC_MS_ID & "'"
            End If
            Dim OnlineQuery As String = "UPDATE magazine_membership_info " & _
                                        "SET MM_MS_OLD_ID = '" & inParam.MM_MS_OLD_ID & "'" & _
                                        ",[MM_OTHER_DETAIL] = '" & inParam.MM_OTHER_DETAIL & "'" & _
                                        ",[REC_EDIT_ON] = '" & Common.DateTimePlaceHolder & "'" & _
                                        ",[REC_EDIT_BY] = '" & inBasicParam.openUserID & "'" & _
                                        " WHERE [REC_ID] = '" & inParam.MS_REC_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, OnlineQuery, inBasicParam)

            OnlineQuery = " SELECT MM_CC_APPLICABLE,MM_CC_MS_ID,MM_CC_SPONSORED FROM MAGAZINE_MEMBERSHIP_CC_INFO WHERE [MM_ID] = '" & inParam.MS_REC_ID & "' AND MM_COD_YEAR_ID = " & inBasicParam.openYearID
            Dim ExistingRecord As DataTable = dbService.List(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_CC_INFO, OnlineQuery, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_CC_INFO.ToString(), inBasicParam)

            If ExistingRecord.Rows.Count > 0 Then
                Dim old_MM_CC_APPLICABLE As Object = ExistingRecord.Rows(0)("MM_CC_APPLICABLE")
                Dim old_MM_CC_MS_ID As Object = IIf(IsDBNull(ExistingRecord.Rows(0)("MM_CC_MS_ID")), "NULL", ExistingRecord.Rows(0)("MM_CC_MS_ID"))
                Dim old_MM_CC_SPONSORED As Object = ExistingRecord.Rows(0)("MM_CC_SPONSORED")
                If old_MM_CC_APPLICABLE <> inParam.MM_CC_APPLICABLE Or old_MM_CC_MS_ID <> If(inParam.MM_CC_MS_ID, "") Or old_MM_CC_SPONSORED <> inParam.MM_CC_SPONSORED Then
                    OnlineQuery = "UPDATE MAGAZINE_MEMBERSHIP_CC_INFO " & _
                                        "SET MM_CC_APPLICABLE = '" & inParam.MM_CC_APPLICABLE & "'" & _
                                        ",[MM_CC_MS_ID] = " & inParam.MM_CC_MS_ID & "" & _
                                        ",[MM_CC_SPONSORED] = " & IIf(inParam.MM_CC_SPONSORED, 1, 0) & "" & _
                                        ",MM_CC_MS_INT_ID = (SELECT REC_INT_ID FROM MAGAZINE_MEMBERSHIP_INFO WHERE REC_ID =" & inParam.MM_CC_MS_ID & " )" & _
                                        " WHERE [MM_ID] = '" & inParam.MS_REC_ID & "' AND MM_COD_YEAR_ID >= " & inBasicParam.openYearID
                    dbService.Update(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_CC_INFO, OnlineQuery, inBasicParam)

                    OnlineQuery = "UPDATE TRANSACTION_D_MASTER_INFO " & _
                                                "SET TR_AB_ID_1 = '" & inParam.PartyID & "'" & _
                                                " WHERE TR_REF_ID = '" & inParam.MS_REC_ID & "' AND TR_COD_YEAR_ID >= " & inBasicParam.openYearID
                    dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, OnlineQuery, inBasicParam, DateTime.Now)

                    OnlineQuery = "UPDATE TRANSACTION_INFO " & _
                                               "SET TR_AB_ID_1 = '" & inParam.PartyID & "'" & _
                                               " WHERE TR_M_ID IN (SELECT REC_ID FROM TRANSACTION_D_MASTER_INFO WHERE TR_REF_ID = '" & inParam.MS_REC_ID & "' AND TR_COD_YEAR_ID >= " & inBasicParam.openYearID & ")"
                    dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, DateTime.Now, DateTime.MinValue, Nothing, SqlDbType.BigInt, Nothing, " TR_M_ID IN (SELECT REC_ID FROM TRANSACTION_D_MASTER_INFO WHERE TR_REF_ID = '" & inParam.MS_REC_ID & "' AND TR_COD_YEAR_ID >= " & inBasicParam.openYearID & ") ")
                End If
            End If
            Return True
        End Function

        Public Shared Function Update_Magazine_Membership_Balances(inParam As Param_Update_Magazine_Membership_Balances, inBasicParam As ConnectOneWS.Basic_Param)
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim OnlineQuery As String = "UPDATE [magazine_membership_balances_info] " &
                                        " SET [MMB_PERIOD_FR] = '" & Convert.ToDateTime(inParam.MMB_FROM_DATE).ToString(Common.Server_Date_Format_Long) & "'" &
                                        "   ,[MMB_PERIOD_TO] = " & " " & If(inParam.MMB_TO_DATE <> DateTime.MinValue, "'" & Convert.ToDateTime(inParam.MMB_TO_DATE).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " " &
                                        "   ,[MMB_COPIES] = " & inParam.MMB_COPIES & "" &
                                        "   ,[REC_EDIT_ON] = '" & Common.DateTimePlaceHolder & "'" &
                                        "   ,[REC_EDIT_BY] = '" & inBasicParam.openUserID & "'" &
                                        "   ,[MMB_CC_DISPATCH] = '" & inParam.MMB_CC_DISPATCH & "'" &
                                        "   ,[MMB_FREE_COPIES] = " & inParam.MMB_FREE_COPIES & "" &
                                        "   ,[MMB_MDT_ID] = '" & inParam.MMB_MDT_ID & "'" &
                                        "   ,[MMB_MST_ID] = '" & inParam.MMB_MST_ID & "'" &
                                    " WHERE [REC_ID] = '" & inParam.REC_ID & "'"
            '"   ,[MMB_AMOUNT] = " & inParam.MMB_AMT & "" & _
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_BALANCES_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Insert_VoucherMagazine_Txn(inParam As Param_Txn_Insert_VoucherMagazineMembership, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim AddOn_DeletedVouchers As New Dictionary(Of String, DateTime)
            Dim AddTime As DateTime

            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.Param_DeletedVouchers Is Nothing Then
                For Each Param As String In inParam.Param_DeletedVouchers
                    If Not Param Is Nothing Then
                        If Not AddOn_DeletedVouchers.ContainsKey(Param) Then AddOn_DeletedVouchers.Add(Param, GetAddDateforTxnId(Param, inBasicParam))
                        DeleteMagazineMembershipVoucher(Param, inBasicParam)
                    End If
                Next
            End If

            If Not inParam.Param_Update_Memebership_Balances Is Nothing Then
                Update_Magazine_Membership_Balances(inParam.Param_Update_Memebership_Balances, inBasicParam)
            End If

            Dim SubsType As String = Nothing
            If Not inParam.Param_InsertSubsBalances Is Nothing Then
                For Each Param As Parameter_InsertBalances_VoucherMagazineMembership In inParam.Param_InsertSubsBalances
                    If Not Param Is Nothing Then SubsType = Param.SUB_ID
                Next
            End If

            If Not inParam.Param_InsertMaster Is Nothing Then
                For Each param As Parameter_InsertMaster_VoucherMagazineMembership In inParam.Param_InsertMaster
                    If Not param Is Nothing Then
                        AddTime = RequestTime
                        If AddOn_DeletedVouchers.ContainsKey(param.RecID) Then AddTime = AddOn_DeletedVouchers(param.RecID)
                        If Not InsertMasterInfo(param, inBasicParam, AddTime, SubsType) Then Throw New Exception(Common_Lib.Messages.SomeError)
                    End If
                Next
            End If

            If Not inParam.Param_Insert_Payment_Txn Is Nothing Then
                For Each Param As Parameter_Insert_VoucherMagazineMembership In inParam.Param_Insert_Payment_Txn
                    If Not Param Is Nothing Then
                        AddTime = RequestTime
                        If AddOn_DeletedVouchers.ContainsKey(Param.MasterTxnID) Then AddTime = AddOn_DeletedVouchers(Param.MasterTxnID)
                        Insert(Param, inBasicParam, AddTime)
                    End If
                Next
            End If

            If Not inParam.Param_Insert_Subs_Txn Is Nothing Then
                For Each Param As Parameter_Insert_VoucherMagazineMembership In inParam.Param_Insert_Subs_Txn
                    If Not Param Is Nothing Then
                        AddTime = RequestTime
                        If AddOn_DeletedVouchers.ContainsKey(Param.MasterTxnID) Then AddTime = AddOn_DeletedVouchers(Param.MasterTxnID)
                        Insert(Param, inBasicParam, AddTime)
                    End If
                Next
            End If

            If Not inParam.Param_InsertSubsBalances Is Nothing Then
                For Each Param As Parameter_InsertBalances_VoucherMagazineMembership In inParam.Param_InsertSubsBalances
                    If Not Param Is Nothing Then
                        AddTime = RequestTime
                        If AddOn_DeletedVouchers.ContainsKey(Param.Tr_Id) Then AddTime = AddOn_DeletedVouchers(Param.Tr_Id)
                        InsertBalances(Param, inBasicParam, AddTime) ' Posts Magazine Membership and Balances Entries. If next year is there then posts income there too
                    End If
                Next
            End If

            If Not inParam.Param_InsertPmtBalances Is Nothing Then
                For Each Param As Parameter_InsertBalances_VoucherMagazineMembership In inParam.Param_InsertPmtBalances
                    If Not Param Is Nothing Then
                        AddTime = RequestTime
                        If AddOn_DeletedVouchers.ContainsKey(Param.Tr_Id) Then AddTime = AddOn_DeletedVouchers(Param.Tr_Id)
                        InsertBalances(Param, inBasicParam, AddTime)
                    End If
                Next
            End If

            If Not inParam.Param_InsertPurpose_subs Is Nothing Then
                For Each Param As Parameter_InsertPurpose_VoucherMagazineMembership In inParam.Param_InsertPurpose_subs
                    If Not Param Is Nothing Then
                        AddTime = RequestTime
                        If AddOn_DeletedVouchers.ContainsKey(Param.TxnID) Then AddTime = AddOn_DeletedVouchers(Param.TxnID)
                        InsertPurpose(Param, inBasicParam, AddTime)
                    End If
                Next
            End If

            If Not inParam.Param_InsertPurpose_Pmt Is Nothing Then
                For Each Param As Parameter_InsertPurpose_VoucherMagazineMembership In inParam.Param_InsertPurpose_Pmt
                    If Not Param Is Nothing Then
                        AddTime = RequestTime
                        If AddOn_DeletedVouchers.ContainsKey(Param.TxnID) Then AddTime = AddOn_DeletedVouchers(Param.TxnID)
                        InsertPurpose(Param, inBasicParam, AddTime)
                    End If
                Next
            End If

            If Not inParam.Param_InsertPayment Is Nothing Then
                For Each Param As Parameter_InsertPayment_VoucherMagazineMembership In inParam.Param_InsertPayment
                    If Not Param Is Nothing Then
                        AddTime = RequestTime
                        If AddOn_DeletedVouchers.ContainsKey(Param.TxnMID) Then AddTime = AddOn_DeletedVouchers(Param.TxnMID)
                        InsertPayment(Param, inBasicParam, AddTime)
                    End If
                Next
            End If

            If Not inParam.Param_Update_Membership Is Nothing Then
                Update_Magazine_Membership(inParam.Param_Update_Membership, inBasicParam)
            End If

            If Not inParam.Param_Insert_dispatch_New_Voucher Is Nothing Then
                Magazine.Insert_Dispatches_for_new_Voucher(inParam.Param_Insert_dispatch_New_Voucher, inBasicParam)
            End If

            ' End Using
            'Commit here 
            ' txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function GetAddDateforTxnId(Tr_m_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DateTime
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = ""
            onlineQuery = " SELECT REC_ADD_ON FROM TRANSACTION_D_MASTER_INFO WHERE REC_ID IN ('" & Tr_m_ID & "') "
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO, onlineQuery, ConnectOneWS.Tables.TRANSACTION_D_MASTER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert Receipt
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_InsertReceipt</remarks>
        Public Shared Function InsertReceipt(ByVal Param As Param_InsertReceipt_Magazine_Receipt_Register, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_MagazineReceipt"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "TR_M_ID", "M_DATE", "USER_ID", "YR_ST_DATE", "V_DATE", "MEMBER_ID", "MEMBER_TYPE"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.M_ID, Param.MDate, Param.openUserID, Param.openYearSdt, Param.VDate, Param.MEMBER_ID, Param.MEMBER_TYPE}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Date, Data.DbType.String, Data.DbType.Date, Data.DbType.Date, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 36, 8, 255, 8, 8, 36, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_RECEIPT_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            Return True
        End Function

        ''' <summary>
        ''' Delete Receipt
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.MembershipReceiptRegister_DeleteReceipt</remarks>
        Public Overloads Shared Function DeleteReceipt(ByVal Param As Param_DeleteReceipt_Magazine_Receipt_Register, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE MAGAZINE_RECEIPT_INFO SET " & _
                                        " MR_CANCEL         = 'YES'," & _
                                        " MR_CANCEL_REMARKS  ='" & Param.Reason & "', " & _
                                        " " & _
                                        " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_EDIT_BY       ='" & inBasicParam.openUserID & "', " & _
                                        " REC_STATUS        =" & Common_Lib.Common.Record_Status._Completed & "," & _
                                        " REC_STATUS_ON     ='" & Common.DateTimePlaceHolder & "'," & _
                                        " REC_STATUS_BY     ='" & inBasicParam.openUserID & "'  " & _
                                        " WHERE REC_ID    ='" & Param.Rec_Id & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_RECEIPT_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function
    End Class
#End Region

#Region "--Register--"
    <Serializable>
    Public Class Magazine_Request_Register
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_Magazine_Membership_Request
            Public Member_Address_ID As String
            Public Mag_ID As String
            Public MST_ID As String
            Public Period_Fr As String
            Public Period_To As String
            Public Copies As Integer
            Public Subs_Amt As Double
            Public MDT_ID As String
            Public Total_Bal_Amt As Double
            Public OtherDetails As String
            Public NewRec As Boolean
            Public Rec_ID As String
            Public MM_ID As String = ""
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Membership_Request
            Public Member_Address_ID As String
            Public Mag_ID As String
            Public MST_ID As String
            Public Period_Fr As String
            Public Period_To As String
            Public Copies As Integer
            Public Subs_Amt As Double
            Public MDT_ID As String
            Public Total_Bal_Amt As Double
            Public OtherDetails As String
            Public Rec_ID As String
            Public NewRec As Boolean
            Public MM_ID As String = ""
        End Class
        <Serializable>
        Public Class Parameter_Update_Magazine_Request_status
            Public MMR_REC_ID As String = ""
            Public MMR_TR_ID As String = ""
        End Class

#End Region

        Public Shared Function GetList_Requests(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_MagazineMembershipRequestList"
            Dim params() As String = {"CEN_ID", "YEAR_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {5, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_REQUEST_INFO, SPName, ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_REQUEST_INFO.ToString, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Insert_Magazine_Membership_Request(ByVal InParam As Parameter_Insert_Magazine_Membership_Request, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Insert_MagazineMembership_Request"
            Dim params() As String = {"CEN_ID", "YEAR_ID", "MEMBER_ID", "MAG_ID", "OTHER_DETAILS", "User_ID", "SUB_ID", "PERIOD_FROM_DATE", "PERIOD_TO_DATE", "MEM_COPIES", "MEM_DISPATCH_ID", "BAL_AMOUNT", "MMB_SUBS_AMT", "NEW_REC", "REC_ID", "MM_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, InParam.Member_Address_ID, InParam.Mag_ID, InParam.OtherDetails, inBasicParam.openUserID, InParam.MST_ID, InParam.Period_Fr, InParam.Period_To, InParam.Copies, InParam.MDT_ID, InParam.Total_Bal_Amt, InParam.Subs_Amt, InParam.NewRec, InParam.Rec_ID, InParam.MM_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.Date, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 36, 36, 255, 255, 36, 19, 19, 1, 36, 19, 19, 1, 36, 36}
            dbService.InsertBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Membership_Request(ByVal UpParam As Parameter_Update_Magazine_Membership_Request, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_MagazineMembership_Request"
            Dim params() As String = {"REQ_REC_ID", "CEN_ID", "YEAR_ID", "MEMBER_ID", "MAG_ID", "OTHER_DETAILS", "User_ID", "SUB_ID", "PERIOD_FROM_DATE", "PERIOD_TO_DATE", "MEM_COPIES", "MEM_DISPATCH_ID", "BAL_AMOUNT", "MMB_SUBS_AMT", "NEW_REC", "MM_ID"}
            Dim values() As Object = {UpParam.Rec_ID, inBasicParam.openCenID, inBasicParam.openYearID, UpParam.Member_Address_ID, UpParam.Mag_ID, UpParam.OtherDetails, inBasicParam.openUserID, UpParam.MST_ID, UpParam.Period_Fr, UpParam.Period_To, UpParam.Copies, UpParam.MDT_ID, UpParam.Total_Bal_Amt, UpParam.Subs_Amt, UpParam.NewRec, UpParam.MM_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Date, Data.DbType.Date, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Boolean, Data.DbType.String}
            Dim lengths() As Integer = {36, 5, 4, 36, 36, 255, 255, 36, 19, 19, 1, 36, 19, 19, 1, 36}
            dbService.UpdateBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function Update_Magazine_Request_status(inParam As Parameter_Update_Magazine_Request_status, inBasicParam As ConnectOneWS.Basic_Param)
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "UPDATE MAGAZINE_MEMBERSHIP_REQUEST_INFO " & _
                                        "SET MMR_TR_ID = '" & inParam.MMR_TR_ID & "'" & _
                                        " WHERE [REC_ID] = '" & inParam.MMR_REC_ID & "'"
            dbService.Update(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_REQUEST_INFO, OnlineQuery, inBasicParam)
            Return True
        End Function

        Public Shared Function Delete_Magazine_Membership_Request(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Delete_Magazine_Membership_Request"
            Dim params() As String = {"REC_ID"}
            Dim values() As Object = {RecID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {36}
            dbService.UpdateBySP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
    End Class

    Public Class Magazine_Reports

        Public Shared Function GetIncome_Breakup(ReportType As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[sp_Get_Magazine_Income_Bkp]"
            Dim params() As String = {"CENID", "YEARID", "TYPE"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, ReportType}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 15}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetParty_Balances(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[sp_Get_Magazine_Party_Balances]"
            Dim params() As String = {"CENID", "YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetOpeningAccountingBalances_Bkp(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[sp_Get_Magazine_Opening_Acc_Balances_Bkp]"
            Dim params() As String = {"CENID", "YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetMagazine_Receivables_Ledger(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[sp_Get_Magazine_Receivables_Ledger]"
            Dim params() As String = {"CENID", "YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetMagazine_Advances_Ledger(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[sp_Get_Magazine_advances_Ledger]"
            Dim params() As String = {"CENID", "YEARID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.MAGAZINE_MEMBERSHIP_INFO, SPName, "MAGAZINE_MEMBERSHIP_INFO", params, values, dbTypes, lengths, inBasicParam)
        End Function

    End Class
#End Region

End Namespace
