Imports System.Data
Imports Real.ConnectOneWS

Namespace Real
    <Serializable>
    Public Class CoreFunctions
#Region "Parameter Classes"
        <Serializable>
        Public Class Param_GetBankBranchesForMultipleIDs
            Public _BranchID As String
            Public _Columns As String
        End Class
        <Serializable>
        Public Class Param_GetMisc
            Public MiscId As String
            Public MiscNameColumnHead As String
            Public RecIDColumnHead As String
            Public MISC_REMARK2 As String
        End Class
        <Serializable>
        Public Class Param_GetMiscDetails
            Public MiscIds As String
            Public MiscNameColumnHead As String
            Public RecIDColumnHead As String
        End Class
        <Serializable>
        Public Class Param_GetItems_ItemProfile
            Public item_profile As String
            Public IDColumnHeadName As String
            Public NameColumnHeadName As String
            Public currInsttID As String
        End Class
        <Serializable>
        Public Class Param_GetItemsAll
            Public IDColumnHeadName As String
            Public NameColumnHeadName As String
            Public currInsttID As String
        End Class
        <Serializable>
        Public Class Param_GetItemsByMultipleItemIDs
            Public iTEM_iDs As String
            Public currInsttID As String
        End Class
        <Serializable>
        Public Class Param_GetItemsByMultipleItemProfile
            Public item_profile As String
            Public item_profile2 As String
            Public IDColumnHeadName As String
            Public NameColumnHeadName As String
            Public currInsttID As String
        End Class
        <Serializable>
        Public Class Param_GetOpeningProfileItems
            Public item_profile As String
            Public IDColumnHeadName As String
            Public NameColumnHeadName As String
            Public currInsttID As String
        End Class
        <Serializable>
        Public Class Param_GetInstituteList
            Public NameColHead As String
            Public IDColHead As String
            Public SNameColHead As String
        End Class
        <Serializable>
        Public Class Param_GetInstituteListNameInShort
            Public NameColHeader As String
            Public IDColHEader As String
        End Class
        <Serializable>
        Public Class Param_GetCountriesList
            Public NameColumnHead As String
            Public CodeColumnHead As String
            Public IDColumnHead As String
        End Class
        <Serializable>
        Public Class Param_GetCountriesByID
            Public NameColumnHead As String
            Public IDColumnHead As String
            Public Country_IDs As String
        End Class
        <Serializable>
        Public Class Param_GetStatesList
            Public CountryCode As String
            Public NameColumnHead As String
            Public CodeColumnHead As String
            Public IDColumnHead As String
        End Class
        <Serializable>
        Public Class Param_GetStatesByID
            Public NameColumnHead As String
            Public IDColumnHead As String
            Public State_IDs As String
        End Class
        <Serializable>
        Public Class Param_GetDistrictsList
            Public CountryCode As String
            Public StateCode As Double
            Public NameColumnHead As String
            Public IDColumnHead As String
        End Class
        <Serializable>
        Public Class Param_GetDistrictsByID
            Public NameColumnHead As String
            Public IDColumnHead As String
            Public District_IDs As String
        End Class
        <Serializable>
        Public Class Param_GetCitiesListByCountryandstate
            Public CountryCode As String
            Public StateCode As Double
            Public NameColumnHead As String
            Public IDColumnHead As String
        End Class
        <Serializable>
        Public Class Param_GetCitiesListByCountry
            Public CountryCode As String
            Public NameColumnHead As String
            Public IDColumnHead As String
        End Class
        <Serializable>
        Public Class Param_GetCitiesByID
            Public NameColumnHead As String
            Public IDColumnHead As String
            Public City_IDs As String
        End Class
        <Serializable>
        Public Class Param_GetItemsLedgerCommon
            Public ItemApplicable As String
            Public ForeignDonationVoucher As String
            Public open_Ins_ID As String
            Public MembershipRenewalVoucher As String
            Public MembershipVoucher As String
            Public Type As String
            Public AllowForeignDonation As Boolean
            Public currInsttID As String
            Public LedgerID As String
        End Class
        <Serializable>
        Public Class Param_GetTDSRecords_Common
            Public PAN As String
            Public TDSCode As String
        End Class
        <Serializable>
        Public Class Param_GetMiscDetailsCommon
            Public MiscNameColumnHead As String
            Public Make As String
            Public Type As String
        End Class
        <Serializable>
        Public Class Param_GetItemsByQueryCommon
            Public ItemIDs As String
            Public Type As String
            Public Profile As String
            Public RecIdColumnHead As String
            Public NameColumnHead As String
            Public currInsttID As String
        End Class
        <Serializable>
        Public Class Param_GetCenterDetailsByQuery_Common
            Public BKPad As String
            Public SelectedCenID As String
        End Class
        <Serializable>
        Public Class Param_GetItems_Ledger
            Public Item_Profile As String
            Public currInsttID As String
        End Class
        <Serializable>
        Public Class Param_GetMisc_Common
            Public Category As String
            Public ReturnCategory As Boolean
        End Class

#End Region
        ''' <summary>
        ''' Returns HOEvents, called by Core_GetHOEvents(MessageName:="Get HO Events Function")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetHOEvents</remarks>
        Public Shared Function GetHOEvents(ByVal OnlyOpenEvents As Boolean, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT HE_EVENT_ID as ID ,HE_NAME as Name,HE_FROM , HE_TO , HE_LOCATION, HE_CONTACT_PERSON ,HE_CONTACT_NO,HE_CATEGORY FROM SO_HO_EVENT_INFO"
            If OnlyOpenEvents Then
                Query += " WHERE UPPER(HE_STATUS) = 'OPEN'  AND HE_COD_YEAR_ID = " & inBasicParam.openYearID.ToString
            Else
                Query += " WHERE HE_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ";"
            End If
            Return dbService.List(ConnectOneWS.Tables.SO_HO_EVENT_INFO, Query, ConnectOneWS.Tables.SO_HO_EVENT_INFO.ToString(), inBasicParam)
        End Function


        ''' <summary>
        ''' Returns TDSRecords, Queries are stored as per ClientScreen provided 
        ''' called by Core_GetTDSRecords(MessageName:="Get TDS Records Function")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetTDSRecords_Common</remarks>
        Public Shared Function GetTDSRecords_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal inParam As Object = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Dim parameter As Param_GetTDSRecords_Common = CType(inParam, Param_GetTDSRecords_Common)
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    If parameter.PAN.Length > 4 Then
                        Query = "SELECT TDS_RATE FROM tds_info WHERE TDS_PAN_CODE ='" & parameter.PAN.Substring(3, 1) & "' AND TDS_CODE='" & parameter.TDSCode & "'"
                    Else
                        Query = "SELECT TDS_RATE FROM tds_info WHERE TDS_PAN_CODE ='NONE' AND TDS_CODE='" & parameter.TDSCode & "'"
                    End If
                    'Each calling function Query shall be added here, calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.TDS_INFO, Query, ConnectOneWS.Tables.TDS_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetItemTDSCode(inBasicParam As ConnectOneWS.Basic_Param, ByVal itemRecID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT ITEM_TDS_CODE FROM ITEM_INFO WHERE REC_ID = '" & itemRecID & "'"
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, Query, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Bank details, called by Core_GetBankInfo(MessageName:="Get Bank Info Function")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetBankInfo</remarks>
        Public Shared Function GetBankInfo(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT BI_BANK_NAME,BI_BANK_PAN_NO,REC_ID  FROM BANK_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ORDER BY BI_BANK_NAME  "
            Return dbService.List(ConnectOneWS.Tables.BANK_INFO, Query, ConnectOneWS.Tables.BANK_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Bank details, Queries are stored as per ClientScreen provided
        ''' called by  Core_GetBankInfo_Common(MessageName:="Get Bank Info Common Function, Contains Queries for multiple functions")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetBankInfo_Common</remarks>
        Public Shared Function GetBankInfo_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal inParam As String = "") As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_CollectionBox, ConnectOneWS.ClientScreen.Accounts_Voucher_Donation
                    Query = "SELECT BI_BANK_NAME ,BI_SHORT_NAME,REC_ID as BI_ID From  BANK_INFO  Where   REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
                Case ConnectOneWS.ClientScreen.Accounts_DonationRegister, ConnectOneWS.ClientScreen.Report_Gift
                    Query = "SELECT BI_BANK_NAME From  BANK_INFO  Where   REC_ID = '" & inParam & "';" 'BankId
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Internal_Transfer
                    Query = " SELECT BI_BANK_NAME as TRF_BI_BANK_NAME ,BI_SHORT_NAME AS TRF_BI_SHORT_NAME,REC_ID as TRF_BI_ID  From   BANK_INFO  Where  REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Membership, ConnectOneWS.ClientScreen.Accounts_Voucher_Membership_Renewal,
                    ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt, ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = "SELECT BI_BANK_NAME ,BI_SHORT_NAME,REC_ID as BI_ID  From  BANK_INFO Where   REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
                Case ConnectOneWS.ClientScreen.Stock_Supplier_Master
                    Query = "SELECT BI_BANK_NAME Bank, BI_SHORT_NAME ShortName, REC_ID as ID From  BANK_INFO  Where  REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    If inParam.Length > 0 Then
                        If Not inParam.Trim().StartsWith("'") Then inParam = "'" & inParam & "'"
                        Query = "SELECT REC_ID,BI_BANK_NAME  From  BANK_INFO  Where  REC_STATUS IN (0,1,2) AND REC_ID IN (" & inParam & ")  ;" 'Bank_IDs
                    Else
                        Query = "SELECT BI_BANK_NAME ,BI_SHORT_NAME,REC_ID as BI_ID  From  BANK_INFO Where   REC_STATUS IN (0,1,2) ORDER BY BI_BANK_NAME ;"
                    End If
                    'Each calling function Query shall be added here , calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.BANK_INFO, Query, ConnectOneWS.Tables.BANK_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Bank and branch details for specified Multiple Comma Seperated Branch Rec IDS
        ''' called by Core_GetBankBranchesForMultipleIDs(MessageName:="Get Bank Branch for Multiple IDs Function")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetBankBranchesForMultipleIDs</remarks>
        Public Shared Function GetBankBranchesForMultipleIDs(ByVal Branch_IDs As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT B.BI_BANK_NAME AS Name,B.BI_BANK_PAN_NO,A.BB_BRANCH_NAME as Branch,A.BB_IFSC_CODE,A.BB_MICR_CODE, A.REC_ID AS BB_BRANCH_ID, A.BI_BANK_ID, B.BI_SHORT_NAME " &
                                     " From ( BANK_BRANCH_INFO A Inner join  BANK_INFO as B  on A.BI_BANK_ID  = B.REC_ID) " &
                                     " Where  A.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND A.REC_ID IN (" & Branch_IDs & ")  ;"
            Return dbService.List(ConnectOneWS.Tables.BANK_BRANCH_INFO, Query, ConnectOneWS.Tables.BANK_BRANCH_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Bank and branch specified Columns for specified Multiple Comma Seperated Branch Rec IDS
        ''' called by Core_GetBankBranchesForMultipleIDs(MessageName:="Get Bank Branch for Multiple IDs Function with custom column names")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Core_GetBankBranchesForMultipleIDsWithCustomColumnNames, Param_GetBankBranchesForMultipleIDs</remarks>
        Public Shared Function GetBankBranchesForMultipleIDs(ByVal Param As Param_GetBankBranchesForMultipleIDs, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT " & Param._Columns &
                                     " From ( BANK_BRANCH_INFO A Inner join  BANK_INFO as B  on A.BI_BANK_ID  = B.REC_ID) " &
                                     " Where  A.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND A.REC_ID IN (" & Param._BranchID & ")  ;"
            Return dbService.List(ConnectOneWS.Tables.BANK_BRANCH_INFO, Query, ConnectOneWS.Tables.BANK_BRANCH_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns branch details for specified Bank ID, called by Core_GetBankBranchesByBankID(MessageName:="Get Bank Branches by BankID Function")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Core_GetBankBranchesByBankID</remarks>
        Public Shared Function GetBankBranchesByBankID(ByVal BankIDs As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT BB_BRANCH_NAME ,BB_IFSC_CODE  ,BB_MICR_CODE  ,REC_ID AS BB_ID   FROM BANK_BRANCH_INFO " &
                                    "WHERE BI_BANK_ID ='" & BankIDs & "' AND REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ORDER BY BB_BRANCH_NAME "
            Return dbService.List(ConnectOneWS.Tables.BANK_BRANCH_INFO, Query, ConnectOneWS.Tables.BANK_BRANCH_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Bank and branch details for specified Branch Rec ID, called by Core_GetBankBranches(MessageName:="Get Bank Branches Function")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetBankBranches</remarks>
        Public Shared Function GetBankBranches(ByVal Branch_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT A.BI_BANK_ID,B.BI_BANK_PAN_NO,A.BB_IFSC_CODE,A.BB_MICR_CODE, A.REC_ID AS BB_BRANCH_ID " &
                                 " From ( BANK_BRANCH_INFO A Inner join  BANK_INFO as B  on A.BI_BANK_ID  = B.REC_ID) " &
                                 " Where  A.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND A.REC_ID = '" & Branch_ID & "'  ;"
            Return dbService.List(ConnectOneWS.Tables.BANK_BRANCH_INFO, Query, ConnectOneWS.Tables.BANK_BRANCH_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Misc Records by Misc ID, called by Core_GetMisc(MessageName:="Get Misc Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetMisc, Param_GetMisc</remarks>
        Public Shared Function GetMisc(ByVal Param As Param_GetMisc, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            If Not Param.MiscId.StartsWith("'") Then Param.MiscId = "'" & Param.MiscId
            If Not Param.MiscId.EndsWith("'") Then Param.MiscId = Param.MiscId & "'"
            If (Not Param.MISC_REMARK2.StartsWith("'")) And Param.MISC_REMARK2.Length > 0 Then Param.MISC_REMARK2 = "'" & Param.MISC_REMARK2
            If (Not Param.MISC_REMARK2.EndsWith("'")) And Param.MISC_REMARK2.Length > 0 Then Param.MISC_REMARK2 = Param.MISC_REMARK2 & "'"
            Dim Remark2Filter As String = ""
            If Param.MISC_REMARK2.Length > 0 Then
                Remark2Filter = " AND COALESCE(MISC_REMARK2,'') IN (" & Param.MISC_REMARK2 & ")"
            End If
            Dim ScreenFilter As String = ""
            If inBasicParam.screen = ClientScreen.Facility_ServiceReport Then
                ScreenFilter = " AND COALESCE(MISC_REMARK2,'') NOT IN ('Exclude From Service Report')" 'Exclude Azadi Ka Amrit Mahotsav And Occasions from project in godly service report
            End If
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR As String = " SELECT MISC_NAME as " & Param.MiscNameColumnHead & ",REC_ID as " & Param.RecIDColumnHead & " FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND MISC_ID IN (" & Param.MiscId & ") " & Remark2Filter & ScreenFilter & " order by misc_name "
            Return dbService.List(ConnectOneWS.Tables.MISC_INFO, SQL_STR, ConnectOneWS.Tables.MISC_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Misc Record Details by multiple Misc ID, called by Core_GetMiscDetails(MessageName:="Get Misc Details By multiple MiscIds Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetMiscDetails, Param_GetMiscDetails</remarks>
        Public Shared Function GetMiscDetails(ByVal Param As Param_GetMiscDetails, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR As String = " SELECT MISC_NAME as " & Param.MiscNameColumnHead & ",MISC_ID as MASTERID,MISC_SRNO AS Sr, REC_ID as " & Param.RecIDColumnHead & " FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND MISC_ID IN (" & Param.MiscIds & ") order by misc_name "
            Return dbService.List(ConnectOneWS.Tables.MISC_INFO, SQL_STR, ConnectOneWS.Tables.MISC_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Misc Record Details by multiple Misc ID, Queries are stored as per ClientScreen provided 
        ''' called by Core_GetMiscDetails(MessageName:="Get Bank Misc Record Details by multiple Misc ID Function, Contains Queries for multiple functions")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetMiscDetails_Common</remarks>
        Public Shared Function GetMiscDetails_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal inParam As Param_GetMiscDetailsCommon = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Options_DocLibrary
                    Query = "SELECT MISC_NAME AS Category,MISC_REMARK1 AS Title,MISC_REMARK2 as Filename,MISC_SRNO AS Sr,REC_ID AS ID FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('DOCUMENT LIBRARY')  ORDER BY CAST(MISC_SRNO AS DECIMAL)"
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    If inParam.Type = "1" Then 'MiscNameColumnHead
                        Query = " SELECT distinct MISC_NAME as " & inParam.MiscNameColumnHead & " FROM MISC_INFO WHERE  REC_STATUS IN (0,1,2) AND MISC_ID IN ('VEHICLE MAKE & MODEL') order by misc_name "
                    ElseIf inParam.Type = "2" Then
                        Query = " SELECT MISC_REMARK1 AS Name FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('VEHICLE MAKE & MODEL') AND MISC_NAME='" & inParam.Make & "' ORDER BY MISC_REMARK1 "
                    End If
                    'Each calling function Query shall be added here , calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.MISC_INFO, Query, ConnectOneWS.Tables.MISC_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Misc Records by custom Query,  Queries are stored as per ClientScreen provided 
        ''' called by Core_GetMisc(MessageName:="Get Misc Records by custom Query Function, Contains Queries for multiple functions")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetMisc_Common</remarks>
        Public Shared Function GetMisc_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal inParam As String = "") As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_DonationRegister, ConnectOneWS.ClientScreen.Report_Gift 'MiscID
                    Query = "SELECT MISC_NAME from MISC_INFO where REC_ID = '" & inParam & "' ;"
                Case ConnectOneWS.ClientScreen.Profile_Vehicles
                    If inParam.Length > 0 Then 'Make
                        Query = " SELECT MISC_REMARK1 AS Name FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('VEHICLE MAKE & MODEL') AND MISC_NAME='" & inParam & "' ORDER BY MISC_REMARK1 "
                    Else
                        Query = " SELECT DISTINCT MISC_NAME AS Name FROM MISC_INFO where  REC_STATUS IN (0,1,2) AND MISC_ID IN ('VEHICLE MAKE & MODEL') ORDER BY MISC_NAME "
                    End If
                Case ConnectOneWS.ClientScreen.Help_Attachments
                    If inParam.Length > 0 Then
                        If inParam.ToLower() = "category" Then ' Returns Categories of Documents 
                            Query = " SELECT DISTINCT MISC_REMARK1 as Category, MISC_REMARK1 as ID FROM MISC_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND MISC_ID IN ('Attachment Category') order by MISC_REMARK1"

                            'MsgBox("Query is :" & Query)

                        Else 'Returns Category specific Documents 
                            Query = " SELECT MISC_NAME as Name,MISC_REMARK1 as Category,MI.REC_ID as ID,PROP.AD_PROP_LABEL_MANDATORY ,PROP.AD_PROP_LABEL_FROM_DATE,PROP.AD_PROP_LABEL_TO_DATE,PROP.AD_PROP_LABEL_DESCRIPTION FROM MISC_INFO MI LEFT JOIN attachment_doc_properties AS PROP ON MI.REC_ID = PROP.AD_PROP_MISC_ID WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND MISC_ID IN ('Attachment Category') AND MISC_REMARK1 ='" & inParam & "' order by MISC_REMARK1,MISC_NAME "
                        End If
                    Else 'Returns all Documents 
                        Query = " SELECT MISC_NAME as Name,MISC_REMARK1 as Category,MI.REC_ID as ID,PROP.AD_PROP_LABEL_MANDATORY ,PROP.AD_PROP_LABEL_FROM_DATE,PROP.AD_PROP_LABEL_TO_DATE,PROP.AD_PROP_LABEL_DESCRIPTION  FROM MISC_INFO MI LEFT JOIN attachment_doc_properties AS PROP ON MI.REC_ID = PROP.AD_PROP_MISC_ID WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND MISC_ID IN ('Attachment Category') order by MISC_NAME "
                    End If
                    'Each calling function Query shall be added here , calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.MISC_INFO, Query, ConnectOneWS.Tables.MISC_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by Item Profile, called by Core_GetItems(MessageName:="Get Items By ItemProfile Function")
        ''' </summary>
        ''' <param name="item_profile"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItemsByItemProfile, Param_GetItems_ItemProfile</remarks>
        Public Shared Function GetItems(ByVal Param As Param_GetItems_ItemProfile, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR1 As String = "SELECT REC_ID AS " & Param.IDColumnHeadName & " ,ITEM_NAME as " & Param.NameColumnHeadName & " from ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & Param.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " where    UPPER(ITEM_PROFILE)='" & Param.item_profile & "' AND  ii.REC_STATUS IN (0,1,2)  ;"
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, SQL_STR1, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get All Items, called by Core_GetItems(MessageName:="Get All Items Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItemsAllItems, Param_GetItemsAll</remarks>
        Public Shared Function GetItems(ByVal Param As Param_GetItemsAll, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR1 As String = "SELECT REC_ID AS " & Param.IDColumnHeadName & " ,ITEM_NAME as " & Param.NameColumnHeadName & " from ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & Param.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " where ii.REC_STATUS IN (0,1,2) ;"
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, SQL_STR1, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by Multiple Item IDS, called by Core_GetItems(MessageName:="Get Items By multiple ItemIds Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItemsByMultipleItemIDs</remarks>
        Public Shared Function GetItems(ByVal param As Param_GetItemsByMultipleItemIDs, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR1 As String = "SELECT REC_ID AS ID ,ITEM_NAME from ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & param.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " where ii.REC_STATUS  IN (0,1,2) AND ii.REC_ID IN (" & param.iTEM_iDs & ");"
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, SQL_STR1, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by custom query, Queries are stored as per ClientScreen provided 
        ''' called by Core_GetItemsByQuery(MessageName:="Get Items by custom query Function, Contains Queries for multiple functions")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItemsByQuery_Common</remarks>
        Public Shared Function GetItemsByQuery_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal inParam As Param_GetItemsByQueryCommon = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Profile_OpeningBalances
                    Query = "SELECT I.REC_ID AS 'ID',I.ITEM_NAME AS 'Name',I.ITEM_LED_ID ,L.LED_NAME AS 'Head',L.LED_TYPE as 'Head Type' From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,(SELECT CEN_INS_ID FROM CENTRE_INFO WHERE CEN_ID = " & inBasicParam.openCenID.ToString & ")) ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_Acc_Type_ID,(SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID.ToString & " AND COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")) = (SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID.ToString & " AND COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") where  UPPER(I.ITEM_PROFILE) IN ('" & inParam.Profile & "')  AND  I.REC_STATUS IN (0,1,2)  ;"
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    If inParam.Type = "2" Then 'ItemID
                        Query = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME    from ITEM_INFO where  REC_ID  IN ('" & inParam.ItemIDs & "') AND  REC_STATUS IN (0,1,2)  ;"
                    ElseIf inParam.Type = "1" Then
                        Query = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS ITEM_ID   FROM ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  ii.REC_STATUS IN (0,1,2) AND ii.REC_ID='" & inParam.ItemIDs & "'  "
                    ElseIf inParam.Type = "3" Then
                        Query = "SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & "  where  UPPER(I.ITEM_PROFILE) IN ('" & inParam.Profile & "')  AND  I.REC_STATUS IN (0,1,2)  ;"
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    If inParam.Type = "1" Then
                        Query = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS ITEM_ID   FROM ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  ii.REC_STATUS IN (0,1,2) AND ii.REC_ID='" & inParam.ItemIDs & "'  "
                    ElseIf inParam.Type = "2" Then
                        Query = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME    from ITEM_INFO where  REC_ID  IN ('" & inParam.ItemIDs & "') AND  REC_STATUS IN (0,1,2)  ;"
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    If inParam.Type = "1" Then
                        Query = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS ITEM_ID   FROM ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  ii.REC_STATUS IN (0,1,2) AND ii.REC_ID='" & inParam.ItemIDs & "'  "
                    ElseIf inParam.Type = "2" Then
                        Query = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME,ITEM_OFFSET_REC_ID    from ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " where  ITEM_PROFILE='ADVANCES' AND  ii.REC_STATUS IN (0,1,2)  ;"
                    ElseIf inParam.Type = "3" Then
                        Query = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME,ITEM_OFFSET_REC_ID    from ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " where  ITEM_PROFILE='OTHER LIABILITIES' AND  ii.REC_STATUS IN (0,1,2)  ;"
                    End If
                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_Membership_Renewal
                    '    Query = " SELECT I.ITEM_NAME ,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_TRANS_STMT,I.ITEM_PARTY_REQ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " & _
                    '                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND L.REC_STATUS IN (0,1,2) AND I.REC_ID='" & inParam.ItemIDs & "'  "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Internal_Transfer
                    Query = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID  FROM ITEM_INFO WHERE  REC_STATUS IN (0,1,2) AND REC_ID='" & inParam.ItemIDs & "' AND UPPER(ITEM_TRANS_TYPE)='DEBIT' "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_FD
                    If inParam.Type = "1" Then
                        Query = "SELECT DISTINCT ITEM_NAME AS 'FD Activity', CASE WHEN ITEM_NAME = 'FD New' THEN  1 ELSE CASE WHEN ITEM_NAME = 'FD Renewed' THEN 2 ELSE  4 END END  AS Sr , REC_ID AS ITEMID FROM Item_Info as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE ITEM_VOUCHER_TYPE = 'fd' and ii.REC_ID NOT IN('65730a27-e365-4195-853e-2f59225fe8f4','1ed5cbe4-c8aa-4583-af44-eba3db08e117') UNION ALL SELECT DISTINCT 'FD Close', 3,'65730a27-e365-4195-853e-2f59225fe8f4' from ITEM_INFO ;"
                    ElseIf inParam.Type = "2" Then
                        Query = "SELECT ITEM_TRANS_TYPE, ITEM_LED_ID FROM ITEM_INFO WHERE REC_ID ='290063bc-a1a1-43af-bedb-f51b7a30c4f4';"
                    End If
                Case ConnectOneWS.ClientScreen.Report_Transaction_Statement
                    Query = " SELECT Rec_id AS II_REC_ID,item_name AS Particulars, ITEM_TRANS_TYPE AS Type, ITEM_TRANS_STMT AS Head  from item_info as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE ii.REC_STATUS IN (0,1,2)"
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = "SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " where  UPPER(I.ITEM_PROFILE) IN ('" & inParam.Profile & "')  AND  I.REC_STATUS IN (0,1,2)  ;"
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Membership
                    If inParam.Type = "1" Then
                        Query = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PROFILE, I.REC_ID AS ITEM_ID  " &
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('MEMBERSHIP') "
                    ElseIf inParam.Type = "2" Then
                        Query = " SELECT I.ITEM_NAME ,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_TRANS_STMT,I.ITEM_PARTY_REQ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN COD_INFO AS COD ON COD.CEN_ID = " & inBasicParam.openCenID.ToString & " AND COD.COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & inParam.currInsttID & "') ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(im.map_acc_type_id,COD.COD_CEN_ACC_TYPE_ID) = COD.COD_CEN_ACC_TYPE_ID WHERE  I.REC_STATUS IN (0,1,2) AND L.REC_STATUS IN (0,1,2) AND I.REC_ID='" & inParam.ItemIDs & "'  "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Membership_Renewal
                    If inParam.Type = "1" Then
                        Query = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PROFILE, I.REC_ID AS ITEM_ID  " &
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('MEMBERSHIP RENEWAL') "
                    ElseIf inParam.Type = "2" Then
                        Query = " SELECT I.ITEM_NAME ,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_TRANS_STMT,I.ITEM_PARTY_REQ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                  " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  I.REC_STATUS IN (0,1,2) AND L.REC_STATUS IN (0,1,2) AND I.REC_ID='" & inParam.ItemIDs & "'  "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    Query = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID  FROM ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  ii.REC_STATUS IN (0,1,2) AND ii.REC_ID='" & inParam.ItemIDs & "' AND UPPER(ITEM_TRANS_TYPE)='CREDIT' "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_FD
                    If inParam.Type = "1" Then
                        Query = "SELECT DISTINCT ITEM_NAME AS 'FD Activity', CASE WHEN ITEM_NAME = 'FD New' THEN  1 ELSE CASE WHEN ITEM_NAME = 'FD Renewed' THEN 2 ELSE  4 END END  AS Sr , REC_ID AS ITEMID FROM Item_Info as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE ITEM_VOUCHER_TYPE = 'fd' and ii.REC_ID NOT IN('65730a27-e365-4195-853e-2f59225fe8f4','1ed5cbe4-c8aa-4583-af44-eba3db08e117') UNION ALL SELECT DISTINCT 'FD Close', 3,'65730a27-e365-4195-853e-2f59225fe8f4' from ITEM_INFO ;"
                    ElseIf inParam.Type = "2" Then
                        Query = "SELECT ITEM_TRANS_TYPE, ITEM_LED_ID FROM ITEM_INFO WHERE REC_ID ='290063bc-a1a1-43af-bedb-f51b7a30c4f4';"
                    End If
                Case ConnectOneWS.ClientScreen.Profile_Assets
                    Query = "SELECT REC_ID AS " & inParam.RecIdColumnHead & " ,ITEM_NAME as " & inParam.NameColumnHead & ", ITEM_LED_ID from ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " where UPPER(ITEM_PROFILE)='OTHER ASSETS' AND  ii.REC_STATUS IN (0,1,2)  ;"
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    Query = "SELECT ITEM_VOUCHER_TYPE, ITEM_PROFILE FROM ITEM_INFO WHERE REC_ID ='" & inParam.ItemIDs & "'"
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    If inParam.Type = "2" Then 'ItemID
                        Query = "SELECT REC_ID AS ITEM_ID ,ITEM_NAME    from ITEM_INFO where  REC_ID  IN ('" & inParam.ItemIDs & "') AND  REC_STATUS IN (0,1,2)  ;"
                    ElseIf inParam.Type = "1" Then
                        Query = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ,ITEM_PROFILE,ITEM_CON_LED_ID,ITEM_CON_MIN_VALUE,ITEM_CON_MAX_VALUE,  REC_ID AS ITEM_ID   FROM ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  ii.REC_STATUS IN (0,1,2) AND ii.REC_ID='" & inParam.ItemIDs & "'  "
                    ElseIf inParam.Type = "3" Then
                        Query = "SELECT I.REC_ID AS ITEM_ID ,I.ITEM_NAME,I.ITEM_LED_ID,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE    From Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & "  where  UPPER(I.ITEM_PROFILE) IN ('" & inParam.Profile & "')  AND  I.REC_STATUS IN (0,1,2)  ;"
                    End If
                Case ConnectOneWS.ClientScreen.Report_Items_Documents
                    Query = "SELECT DISTINCT ITEM_NAME, REC_ID FROM ITEM_INFO II INNER JOIN transaction_doc_mapping AS MAP ON MAP.TR_DOC_ITEM_ID = II.REC_ID ORDER BY ITEM_NAME "
            End Select
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, Query, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by Item Profile, called by Core_GetItems(MessageName:="Get Items By Multiple ItemProfile Function")
        ''' </summary>
        ''' <param name="item_profile"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItemsByMultipleItemProfiles, Param_GetItemsByMultipleItemProfile</remarks>
        Public Shared Function GetItems(ByVal Param As Param_GetItemsByMultipleItemProfile, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR1 As String = "SELECT REC_ID AS " & Param.IDColumnHeadName & " ,ITEM_NAME as " & Param.NameColumnHeadName & " from ITEM_INFO as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & Param.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & "  where    UPPER(ITEM_PROFILE) IN ('" & Param.item_profile & "','" & Param.item_profile2 & "') AND  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ;"
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, SQL_STR1, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items, called by Core_GetItems(MessageName:="Get Items Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItems</remarks>
        Public Shared Function GetItems(inBasicParam As ConnectOneWS.Basic_Param, ByVal openInsID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR1 As String = "SELECT REC_ID AS ID ,ITEM_NAME ,ITEM_LED_ID,ITEM_TRANS_STMT  from Item_Info as ii INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & openInsID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ;"
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, SQL_STR1, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        '''  Gets items and ledger info by item profile filter, called by Core_GetItems_Ledger(MessageName:="Get Items & Ledgers By Item profile Function")
        ''' </summary>
        ''' <param name="ItemProfile"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItems_Ledger</remarks>
        Public Shared Function GetItems_Ledger(ByVal inParam As Param_GetItems_Ledger, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If Not inParam.Item_Profile.StartsWith("'") Then inParam.Item_Profile = "'" & inParam.Item_Profile
            If Not inParam.Item_Profile.EndsWith("'") Then inParam.Item_Profile = inParam.Item_Profile & "'"
            Dim SQL_STR1 As String = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                 " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & inParam.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & "  WHERE  I.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND UPPER(I.ITEM_VOUCHER_TYPE) IN (" & inParam.Item_Profile & ")"
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, SQL_STR1, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItems_Ledger_Common</remarks>
        Public Shared Function GetItems_Ledger_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal inParam As Object = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Dim parameter As Param_GetItemsLedgerCommon = CType(inParam, Param_GetItemsLedgerCommon)
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Gift
                    Query = " SELECT I.ITEM_NAME ,L.LED_NAME,L.LED_TYPE,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,CL.LED_TYPE AS 'CON_LED_TYPE', I.REC_ID AS ITEM_ID  " &
                            " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID LEFT OUTER JOIN Acc_Ledger_Info AS CL ON I.ITEM_CON_LED_ID = CL.LED_ID  INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "' AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")" &
                            " WHERE  I.REC_STATUS IN (0,1,2) AND ((UPPER(I.ITEM_PROFILE) IN ('GOLD','SILVER','OTHER ASSETS','LIVESTOCK','VEHICLES','LAND & BUILDING','WIP') AND UPPER(I.ITEM_PROFILE_OPENING)='YES') OR (UPPER(I.ITEM_VOUCHER_TYPE) = 'LAND & BUILDING' AND ITEM_LED_ID IN ('00045','00047'))  OR i.REC_ID in ('e3129da0-5a90-4bf3-8f46-8a28677909e6','8d0f8572-2f1c-4261-b165-c1288f14f128','e7e731e3-3d31-4240-a061-60600c6de8dc','affe9ba3-865f-4f7b-ac58-ea3d0636e87f')) AND UPPER(I.ITEM_TRANS_TYPE)='DEBIT' "
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    If parameter.Type.ToUpper() = "DUALQUERY" Then
                        If parameter.AllowForeignDonation Then
                            Query = "  SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                    "  FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('COLLECTION BOX','DONATION','DONATION - FOREIGN','DONATION - GIFT','CASH DEPOSITED','CASH WITHDRAWN','BANK TRANSFER','RECEIPTS','ADVANCES','PAYMENT','INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.','ASSET TRANSFER') AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") " &
                                    "     Union All " &
                                    "     SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                    "     FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "' AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT - INSTITUTE','RECEIPTS - INSTITUTE') AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ")  AND ITEM_INS_ID <> '" & parameter.open_Ins_ID & "' "
                        Else
                            Query = "  SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                    "  FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "' AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('COLLECTION BOX','DONATION'                     ,'DONATION - GIFT','CASH DEPOSITED','CASH WITHDRAWN','BANK TRANSFER','RECEIPTS','ADVANCES','PAYMENT','INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.','ASSET TRANSFER') AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") " &
                                    "     Union All " &
                                    "     SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                    "     FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "' AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT - INSTITUTE','RECEIPTS - INSTITUTE') AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ")  AND ITEM_INS_ID <> '" & parameter.open_Ins_ID & "' "
                        End If
                    Else
                        Query = "  SELECT DISTINCT I.ITEM_NAME ,L.LED_NAME,I.ITEM_PARTY_REQ,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                "  FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")" &
                                "  WHERE I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('COLLECTION BOX','DONATION','" & parameter.ForeignDonationVoucher & "','DONATION - GIFT','CASH DEPOSITED','CASH WITHDRAWN','BANK TRANSFER','RECEIPTS','ADVANCES','PAYMENT','INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.','ASSET TRANSFER','FD','LAND & BUILDING','LAND & BUILDING / Gift','SALE OF ASSET','" & parameter.MembershipVoucher & "','" & parameter.MembershipRenewalVoucher & "') " &
                                "    AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") " &
                                "  Union All     " &
                                "  SELECT DISTINCT I.ITEM_NAME ,L.LED_NAME,I.ITEM_PARTY_REQ,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                "  FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")" &
                                "  WHERE I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT - INSTITUTE','RECEIPTS - INSTITUTE') " &
                                "    AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ")  AND ITEM_INS_ID <> '" & parameter.open_Ins_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE,I.ITEM_LINK_REC_ID,I.ITEM_OFFSET_REC_ID,A.ITEM_NAME AS ITEM_OFFSET_NAME, I.REC_ID AS ITEM_ID  " &
                            " FROM (Item_Info AS I LEFT JOIN Item_Info AS A ON I.ITEM_OFFSET_REC_ID = A.REC_ID) INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('SALE OF ASSET') AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") "
                Case ConnectOneWS.ClientScreen.Report_Items
                    Query = " SELECT I.ITEM_NAME AS 'Item Name',I.ITEM_LED_ID  , L.LED_NAME AS Head,L.LED_TYPE ,I.ITEM_CON_LED_ID ,I.ITEM_CON_MIN_VALUE ,I.ITEM_CON_MAX_VALUE ,CL.LED_NAME AS CON_LED_NAME ,CL.LED_TYPE AS CON_LED_TYPE, I.ITEM_VOUCHER_TYPE as 'Voucher Type' ,I.ITEM_PROFILE as Profile,I.ITEM_TRANS_STMT AS 'Transaction Stmt',I.ITEM_CONST_STMT AS 'Construction Stmt',I.ITEM_PARTY_REQ as 'Party Required' ,I.ITEM_PETTY_CASH  AS 'Note Book Item', I.ITEM_TDS_CODE as 'TDS Section',I.ITEM_APPLICABLE AS Applicable, I.REC_ID AS ID  ," & Common.Rec_Detail("I") & " " &
                            " FROM  Item_Info AS I INNER JOIN Acc_Ledger_Info AS L  ON (I.ITEM_LED_ID = L.LED_ID)  INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & "  AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")  " &
                            "                      LEFT  JOIN Acc_Ledger_Info AS CL ON (I.ITEM_CON_LED_ID = CL.LED_ID) " &
                            " WHERE I.REC_STATUS IN (0,1,2) order by I.ITEM_NAME   " 'AND UPPER(I.ITEM_VOUCHER_TYPE) NOT IN ('NOT APPLICABLE')  
                Case ConnectOneWS.ClientScreen.Report_Collection_Box_Voucher
                    Query = "SELECT ITEM_NAME AS ITEM, LED_NAME AS HEAD FROM ITEM_INFO AS ii INNER JOIN ACC_LEDGER_INFO AS al ON ii.item_led_id =  al.LED_ID INNER JOIN Item_Mapping as im on ii.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")" &
                                "WHERE UPPER(ITEM_NAME) = UPPER('" & parameter.ItemApplicable & "')"  'Variable name 'ItemName' in local Function 
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Receipt
                    Query = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE,I.ITEM_LINK_REC_ID,I.ITEM_OFFSET_REC_ID,A.ITEM_NAME AS ITEM_OFFSET_NAME, I.REC_ID AS ITEM_ID  " &
                            " FROM (Item_Info AS I LEFT JOIN Item_Info AS A ON I.ITEM_OFFSET_REC_ID = A.REC_ID) INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('RECEIPTS') AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") " &
                            "  Union All " &
                            " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE,I.ITEM_LINK_REC_ID,I.ITEM_OFFSET_REC_ID,A.ITEM_NAME AS ITEM_OFFSET_NAME, I.REC_ID AS ITEM_ID  " &
                            " FROM (Item_Info AS I LEFT JOIN Item_Info AS A ON I.ITEM_OFFSET_REC_ID = A.REC_ID) INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('RECEIPTS - INSTITUTE')  AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ")  AND I.ITEM_INS_ID <> '" & parameter.open_Ins_ID & "' "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    Query = " SELECT I.ITEM_NAME ,L.LED_NAME,L.LED_TYPE,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,CL.LED_TYPE AS 'CON_LED_TYPE',I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE  " &
                            " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID LEFT OUTER JOIN Acc_Ledger_Info AS CL ON I.ITEM_CON_LED_ID = CL.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & "  AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")" &
                            " WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT','LAND & BUILDING','LAND & BUILDING / Gift')              AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") " &
                            " Union All " &
                            " SELECT I.ITEM_NAME ,L.LED_NAME,L.LED_TYPE,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,CL.LED_TYPE AS 'CON_LED_TYPE',I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE  " &
                            " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID LEFT OUTER JOIN Acc_Ledger_Info AS CL ON I.ITEM_CON_LED_ID = CL.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")" &
                            " WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('PAYMENT - INSTITUTE')  AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ")  AND ITEM_INS_ID <> '" & parameter.open_Ins_ID & "' "
                    'Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    'Query = " select DISTINCT * FROM  (SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE  " & _
                    '        " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_PROFILE) NOT IN ('MEMBERSHIP')  AND UPPER(I.ITEM_VOUCHER_TYPE) NOT IN ('BANK TRANSFER','COLLECTION BOX','FD','DONATION','DONATION - FOREIGN','DONATION - GIFT','SALE OF ASSET')  AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ")  AND ITEM_LED_ID NOT IN ('00079','00080')" & _
                    '        " Union All " & _
                    '        " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE  " & _
                    '        " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_PROFILE) NOT IN ('MEMBERSHIP')  AND UPPER(I.ITEM_VOUCHER_TYPE) NOT IN ('BANK TRANSFER','COLLECTION BOX','FD','DONATION','DONATION - FOREIGN','DONATION - GIFT','SALE OF ASSET')   AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ")  AND ITEM_LED_ID NOT IN ('00079','00080') AND ITEM_INS_ID <> '" & parameter.open_Ins_ID & "' )AS A order by ITEM_NAME"

                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    Query = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE, I.REC_ID AS ITEM_ID, 0 as TDS_RATE ,L.LED_TYPE,CL.LED_TYPE AS 'CON_LED_TYPE' " &
                            " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID  INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") LEFT OUTER JOIN Acc_Ledger_Info AS CL ON I.ITEM_CON_LED_ID = CL.LED_ID WHERE  I.REC_STATUS IN (0,1,2) AND ITEM_IS_JV_APPLICABLE = 1   AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ")  AND ITEM_INS_ID <> '" & parameter.open_Ins_ID & "' " &
                            " order by ITEM_NAME"

                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Internal_Transfer
                    Query = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE , I.REC_ID AS ITEM_ID  " &
                                    " FROM Item_Info AS I INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('INTERNAL TRANSFER','INTERNAL TRANSFER WITH H.Q.') AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") "

                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    Query = " SELECT I.ITEM_NAME ,L.LED_NAME,I.ITEM_TRANS_TYPE,I.ITEM_LED_ID, I.ITEM_VOUCHER_TYPE ,I.ITEM_PARTY_REQ ,I.ITEM_PROFILE,I.ITEM_CON_LED_ID,I.ITEM_CON_MIN_VALUE,I.ITEM_CON_MAX_VALUE,I.ITEM_TDS_CODE,I.ITEM_LINK_REC_ID,I.ITEM_OFFSET_REC_ID,A.ITEM_NAME AS ITEM_OFFSET_NAME, I.REC_ID AS ITEM_ID  " &
                                " FROM (Item_Info AS I LEFT JOIN Item_Info AS A ON I.ITEM_OFFSET_REC_ID = A.REC_ID) INNER JOIN Acc_Ledger_Info AS L ON I.ITEM_LED_ID = L.LED_ID INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and COALESCE(im.Map_Instt_ID,'" & parameter.currInsttID & "') ='" & parameter.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " AND COALESCE(IM.Map_Acc_Type_ID, ( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ")) =( SELECT COD_CEN_ACC_TYPE_ID FROM COD_INFO WHERE CEN_ID = " & inBasicParam.openCenID & " AND COD_YEAR_ID = " & inBasicParam.openYearID & ") WHERE  I.REC_STATUS IN (0,1,2) AND UPPER(I.ITEM_VOUCHER_TYPE) IN ('ASSET TRANSFER') AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") "
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_WIP_Finalization
                    Query = " SELECT i.rec_id    AS Txn_Cr_ItemId, i.item_name AS 'Txn_Cr_ItemName', acc.led_id as WIP_LED_ID, acc.led_name as 'WIP LEDGER' " &
               "FROM  acc_ledger_info AS acc  LEFT OUTER JOIN item_info AS i ON i.item_led_id = acc.led_id WHERE  acc.LED_SG_ID  = '00050' AND i.item_name LIKE '%(WIP) Finalization%' AND UPPER(I.ITEM_APPLICABLE) IN(" & parameter.ItemApplicable & ") "
                Case ConnectOneWS.ClientScreen.Account_CashbookAuditor
                    Query = " SELECT i.rec_id  AS ItemID, i.item_name AS ItemName, acc.led_id as LED_ID, acc.led_name as 'LEDGER' FROM  acc_ledger_info AS acc  LEFT OUTER JOIN item_info AS i ON i.item_led_id = acc.led_id WHERE  ITEM_LED_ID  = '" + parameter.LedgerID + "' OR LEN('" + parameter.LedgerID + "') = 0 ORDER BY i.item_name "

                    'Each calling function Query shall be added here , calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, Query, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Ledgers, called by Core_GetLedgersList(MessageName:="Get Ledgers Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetLedgersList</remarks>
        Public Shared Function GetLedgersList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR1 As String = "SELECT LED_ID,LED_NAME From Acc_Ledger_Info where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ORDER BY LED_NAME ;"
            Return dbService.List(ConnectOneWS.Tables.ACC_LEDGER_INFO, SQL_STR1, ConnectOneWS.Tables.ACC_LEDGER_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items' Details, called by Core_GetItemDetails(MessageName:="Get Item Details Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetItemDetails</remarks>
        Public Shared Function GetItemDetails(inBasicParam As ConnectOneWS.Basic_Param, ByVal currInsttID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR1 As String = " SELECT ITEM_NAME ,ITEM_TRANS_STMT,ITEM_TRANS_TYPE,ITEM_LED_ID, ITEM_VOUCHER_TYPE , ITEM_PARTY_REQ ,ITEM_PROFILE,  REC_ID AS ITEM_ID   FROM ITEM_INFO as I  INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & " WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND UPPER(ITEM_VOUCHER_TYPE) IN ('PAYMENT') AND UPPER(ITEM_PETTY_CASH)='YES' "
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, SQL_STR1, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Items by Item Profile with Opening Profile = Yes, called by Core_GetOpeningProfileItems(MessageName:="Get Items by Item Profile with Opening Profile = Yes Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetOpeningProfileItems, Param_GetOpeningProfileItems</remarks>
        Public Shared Function GetOpeningProfileItems(ByVal Param As Param_GetOpeningProfileItems, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SQL_STR1 As String = "SELECT REC_ID AS " & Param.IDColumnHeadName & " ,ITEM_NAME as " & Param.NameColumnHeadName & " from ITEM_INFO as I  INNER JOIN Item_Mapping as im on I.REC_ID = im.Map_Item_Rec_ID and im.Map_Instt_ID ='" & Param.currInsttID & "'  AND COALESCE(Map_Cen_ID," & inBasicParam.openCenID.ToString & ") = " & inBasicParam.openCenID.ToString & " AND COALESCE(Map_fn_year_from," & inBasicParam.openYearID.ToString & ") <= " & inBasicParam.openYearID.ToString & " AND COALESCE(Map_fn_year_to," & inBasicParam.openYearID.ToString & ") >= " & inBasicParam.openYearID.ToString & "  where    UPPER(ITEM_PROFILE)='" & Param.item_profile & "' AND I.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "   AND ITEM_PROFILE_OPENING='YES'   order by item_name ;"
            Return dbService.List(ConnectOneWS.Tables.ITEM_INFO, SQL_STR1, ConnectOneWS.Tables.ITEM_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Center Task Info for Active Center 
        ''' called by  Core_GetCenterTaskInfo(MessageName:="Get Center Task Info for Active Centre Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCenterTaskInfo</remarks>
        Public Shared Function GetCenterTaskInfo(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT TASK_NAME,PERMISSION FROM CENTRE_TASK_INFO  Where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID=" & inBasicParam.openCenID.ToString & "  AND TASK_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "; "
            Return dbService.List(ConnectOneWS.Tables.CENTRE_TASK_INFO, Query, ConnectOneWS.Tables.CENTRE_TASK_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get WINGS Center Task Info for Active Center 
        ''' called by  Core_GetWingsCenterTaskInfo(MessageName:="Get Center Wings Task Info for Active Centre Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetWingsCenterTaskInfo</remarks>
        Public Shared Function GetCenterWingsTaskInfo(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT TASK_NAME,PERMISSION,TASK_REF_ID FROM CENTRE_TASK_INFO  Where  REC_STATUS  IN (" & Common_Lib.Common.Record_Status._Incomplete & "," & Common_Lib.Common.Record_Status._Completed & "," & Common_Lib.Common.Record_Status._Locked & ") AND CEN_ID=" & inBasicParam.openCenID.ToString & "  AND TASK_TYPE='WING' AND TASK_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "; "
            Return dbService.List(ConnectOneWS.Tables.CENTRE_TASK_INFO, Query, ConnectOneWS.Tables.CENTRE_TASK_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Currency Name by ID, called by Core_GetCurrencyName(MessageName:="Get Currency Name by CurrID Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCurrencyName</remarks>
        Public Shared Function GetCurrencyName(ByVal CurrID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CUR_NAME FROM Currency_Info WHERE REC_ID ='" & CurrID & "' ;"
            Return dbService.List(ConnectOneWS.Tables.CURRENCY_INFO, Query, ConnectOneWS.Tables.CURRENCY_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Currency List, called by Core_GetCurrencyList(MessageName:="Get Currency List Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCurrencyList</remarks>
        Public Shared Function GetCurrencyList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT CUR_NAME,CUR_CODE,CUR_SYMBOL,REC_ID AS CUR_ID FROM Currency_Info where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " ORDER BY CUR_NAME "
            Return dbService.List(ConnectOneWS.Tables.CURRENCY_INFO, Query, ConnectOneWS.Tables.CURRENCY_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get HQ Centers, called by Core_GetHQCentersForCurrInstt(MessageName:="Get HQ Centers by InsID Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetHQCentersForCurrInstt</remarks>
        Public Shared Function GetHQCentersForCurrInstt(ByVal openInsID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT  DISTINCT T.CEN_ID AS HQ_CEN_ID  FROM Centre_Task_Info AS T INNER JOIN CENTRE_INFO AS C ON T.CEN_ID = C.CEN_ID WHERE T.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND C.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND  T.TASK_NAME='H.Q. CENTRE' AND C.CEN_INS_ID='" & openInsID & "'"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_TASK_INFO, Query, ConnectOneWS.Tables.CENTRE_TASK_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get CenterDetails for current Cen ID, called by Core_GetCenterDetailsForCenID(MessageName:="Get CenterDetails for current Cen ID Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCenterDetailsForCenID</remarks>
        Public Shared Function GetCenterDetailsForCenID(ByVal openCenIDMain As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim padService As PADService.Service1 = New PADService.Service1
            If Not ConfigurationManager.AppSettings("PADService") Is Nothing Then
                padService.Url = ConfigurationManager.AppSettings("PADService").ToString
            End If

            Dim Query As String = "SELECT [CEN_ID],[CEN_INS_ID],[CEN_MAIN], [CEN_UID],[CEN_BK_PAD_NO], [CEN_PAD_NO],[CEN_REG_NO], [CEN_STATUS],[CEN_NAME], [CEN_B_NAME],[CEN_ADD1], [CEN_ADD2],[CEN_ADD3],[CEN_ADD4], [CEN_CITY_ID],[CEN_DISTRICT_ID], [CEN_STATE_ID],COALESCE(CI_NAME, CEN_CITY) CEN_CITY, [CEN_DISTRICT],[CEN_STATE], [CEN_COUNTRY],[CEN_PINCODE], [CEN_TEL_NO_1],[CEN_TEL_NO_2], [CEN_MOB_NO_1],[CEN_MOB_NO_2], [CEN_FAX_NO_1],[CEN_FAX_NO_2], [CEN_EMAIL_ID_1],[CEN_EMAIL_ID_2], [CEN_WEBSITE_URL],[CEN_ZONE_ID], [CEN_ZONE_SUB_ID],[CEN_CC_ID], [CEN_INCHARGE],[CEN_IN_PAD_NO], [CEN_DOS],[CEN_CON_SCANCODE], CEN.[REC_ADD_ON],CEN.[REC_ADD_BY], CEN.[REC_EDIT_ON],CEN.[REC_EDIT_BY], CEN.[REC_STATUS],CEN.[REC_STATUS_ON], CEN.[REC_STATUS_BY],CEN.[REC_ID], [CEN_COUNTRY_ID],[CEN_CANCELLATION_DATE] from CENTRE_INFO as cEN  LEFT OUTER JOIN map_city_info AS CI ON CEN_CITY_ID = CI.REC_ID where  CEN.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID = " & openCenIDMain.ToString & ""
            Dim RetTable As DataTable = dbService.List(ConnectOneWS.Tables.CENTRE_INFO, Query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
            'Dim arr As Byte() = "sdqjsdnjnfmnqwdfemnklemoqkewenjenrfjwenrfjwnerfjwnrfjwnrfjn"

            If inBasicParam.screen = ConnectOneWS.ClientScreen.Profile_Core Then
                Dim arr(99999) As Byte
                Try
                    Dim _img_Incharge As Object = padService.GetInchargeImage(RetTable.Rows(0)("CEN_BK_PAD_NO"))
                    RetTable.Columns.Add("CEN_INCHARGE_IMAGE", arr.GetType)
                    If Not IsDBNull(_img_Incharge) Then RetTable.Rows(0)("CEN_INCHARGE_IMAGE") = _img_Incharge
                Catch ex As Exception
                    Dim ExMsg As String = ex.Message
                End Try

                Try
                    Dim _Center_Image As Object = padService.GetCentreImage(RetTable.Rows(0)("CEN_BK_PAD_NO"))
                    RetTable.Columns.Add("CENTRE_IMAGE", arr.GetType)
                    If Not IsDBNull(_Center_Image) Then RetTable.Rows(0)("CENTRE_IMAGE") = _Center_Image
                Catch ex As Exception
                    Dim ExMsg As String = ex.Message
                End Try
            End If

            Return RetTable
        End Function

        ''' <summary>
        ''' Common Function to Get CenterDetails for current Cen ID, designed for letter PAD
        ''' called by Core_GetCenterDetailsForLetterPAD(MessageName:="Get CenterDetails for current Cen ID, designed for letter PAD Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCenterDetailsForLetterPAD</remarks>
        Public Shared Function GetCenterDetailsForLetterPAD(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT CEN_NAME," &
                                    "CASE WHEN CEN_ADD1 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(CEN_ADD1)))>0 THEN LTRIM(RTRIM(CEN_ADD1)) + ' ' ELSE '' END END +" &
                                    "CASE WHEN CEN_ADD2 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(CEN_ADD2)))>0 THEN LTRIM(RTRIM(CEN_ADD2)) + ' ' ELSE '' END END +" &
                                    "CASE WHEN  CEN_ADD3 IS NULL THEN '' ELSE CASE WHEN  LEN(LTRIM(RTRIM(CEN_ADD3)))>0 THEN LTRIM(RTRIM(CEN_ADD3)) + ' ' ELSE '' END END +" &
                                    "CASE WHEN CEN_ADD4 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(CEN_ADD4)))>0 THEN LTRIM(RTRIM(CEN_ADD4)) + ' ' ELSE '' END END+" &
                                    "CASE WHEN CEN_CITY IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(CEN_CITY)))>0 THEN LTRIM(RTRIM(CEN_CITY)) + ' - ' ELSE '' END END +" &
                                    "CASE WHEN CEN_PINCODE IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(CEN_PINCODE)))>0 THEN LTRIM(RTRIM(CEN_PINCODE)) + ', ' ELSE '' END END +" &
                                    "CASE WHEN CEN_DISTRICT IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(CEN_DISTRICT)))>0 THEN LTRIM(RTRIM(CEN_DISTRICT)) + ', ' ELSE''END END +" &
                                    "CASE WHEN CEN_STATE IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(CEN_STATE)))>0 THEN LTRIM(RTRIM(CEN_STATE)) + ' ' ELSE ''END END  +" &
                                    "CASE WHEN CEN_COUNTRY IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(CEN_COUNTRY)))>0 THEN LTRIM(RTRIM(CEN_COUNTRY)) + ' ' ELSE '' END END  As CEN_ADDRESS," &
                                    "CASE WHEN CEN_TEL_NO_1 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(CEN_TEL_NO_1)))>0 THEN 'Tel.No.: ' + LTRIM(RTRIM(CEN_TEL_NO_1)) + ' ' ELSE '' END END +" &
                                    "CASE WHEN CEN_TEL_NO_2 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(CEN_TEL_NO_2)))>0 THEN ', ' + LTRIM(RTRIM(CEN_TEL_NO_2))  ELSE '' END END  As CEN_TEL_NO," &
                                    "CASE WHEN CEN_MOB_NO_1 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(CEN_MOB_NO_1)))>0 THEN 'Mob.No.: ' + LTRIM(RTRIM(CEN_MOB_NO_1)) + ' ' ELSE ''END END +" &
                                    "CASE WHEN CEN_MOB_NO_2 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(CEN_MOB_NO_2)))>0 THEN ', ' + LTRIM(RTRIM(CEN_MOB_NO_2))  ELSE ''END END  As CEN_MOB_NO, " &
                                    "CASE WHEN CEN_FAX_NO_1 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(CEN_FAX_NO_1)))>0 THEN 'Fax No.: ' + LTRIM(RTRIM(CEN_FAX_NO_1)) + ' ' ELSE '' END  END +" &
                                    "CASE WHEN CEN_FAX_NO_2 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(CEN_FAX_NO_2)))>0 THEN ', ' + LTRIM(RTRIM(CEN_FAX_NO_2))  ELSE '' END END As CEN_FAX_NO," &
                                    "CASE WHEN CEN_EMAIL_ID_1 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(CEN_EMAIL_ID_1)))>0 THEN 'Email: ' + LTRIM(RTRIM(CEN_EMAIL_ID_1)) + ' ' ELSE '' END END+" &
                                    "CASE WHEN CEN_EMAIL_ID_2 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(CEN_EMAIL_ID_2)))>0 THEN ', ' + LTRIM(RTRIM(CEN_EMAIL_ID_2)) ELSE '' END END  As CEN_EMAIL " &
                                    "FROM CENTRE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID = " & inBasicParam.openCenID.ToString & ""
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get CenterDetails for current Cen ID, in Short
        ''' called by Core_GetCenterAddress(MessageName:="Get CenterDetails in short for current Cen ID")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCenterAddress</remarks>
        Public Shared Function GetMainCenterAddress(inBasicParam As ConnectOneWS.Basic_Param, MainCenId As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT COALESCE(CEN_B_NAME,'') AS CEN_B_NAME, " &
                                      "        COALESCE(CEN_ADD1,'') AS CEN_ADD1, " &
                                      "        COALESCE(CEN_ADD2,'') AS CEN_ADD2, " &
                                      "        COALESCE(CEN_ADD3,'') AS CEN_ADD3, " &
                                      "        COALESCE(CEN_ADD4,'') AS CEN_ADD4, " &
                                      "        COALESCE(CEN_CITY_ID,'') AS CEN_CITY_ID, " &
                                      "        COALESCE(CEN_STATE_ID,'') AS CEN_STATE_ID, " &
                                      "        COALESCE(CEN_DISTRICT_ID,'') AS CEN_DISTRICT_ID, " &
                                      "        COALESCE(CEN_COUNTRY_ID,'') AS CEN_COUNTRY_ID,  " &
                                      "        COALESCE(CEN_PINCODE,'') AS CEN_PINCODE, " &
                                      "        COALESCE(CITY.CI_NAME,'') AS City, COALESCE(dist.DI_NAME,'') as Dist, COALESCE(ST.ST_NAME,'') as State " &
                                      " FROM CENTRE_INFO ci LEFT OUTER JOIN map_city_info AS CITY ON CEN_CITY_ID = CITY.REC_ID LEFT OUTER JOIN map_district_info AS DIST ON CEN_DISTRICT_ID = DIST.REC_ID LEFT OUTER JOIN map_state_info AS ST ON CEN_STATE_ID = ST.REC_ID WHERE  ci.REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID= " & MainCenId.ToString & ""
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, OnlineQuery, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Original Password, called by Core_GetOrgPasswordForCenID(MessageName:="Get Original Password Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetOrgPasswordForCenID</remarks>
        Public Shared Function GetOrgPasswordForCenID(ByVal CenID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT CEN_CON_SCANCODE  " &
                                  " FROM CENTRE_INFO WHERE   REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_ID=" & CenID.ToString & ""
            Return dbService.GetScalar(ConnectOneWS.Tables.CENTRE_INFO, Query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to CenIDs for Center BK PAD No, called by Core_GetCenIDForBKPad(MessageName:="Get CenIDs for BK PAD No Function") 
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCenIDForBKPad</remarks>
        Public Shared Function GetCenIDForBKPad(ByVal CenBKPadNo As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " select CEN_ID from CENTRE_INFO WHERE REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_BK_PAD_NO = '" & CenBKPadNo & "';"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, Query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get CenterDetails for current Cen ID, Queries are stored as per ClientScreen provided 
        ''' called by Core_GetCenterDetailsByQuery(MessageName:="Get CenterDetails for current Cen ID Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCenterDetailsByQuery_Common</remarks>
        Public Shared Function GetCenterDetailsByQuery_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal inParam As Object = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Vouchers
                    Query = " SELECT M.CEN_NAME ,C.CEN_BK_PAD_NO,C.REC_ID FROM CENTRE_INFO AS C INNER JOIN CENTRE_INFO AS M ON C.CEN_BK_PAD_NO = M.CEN_BK_PAD_NO " &
                                     " Where   C.REC_STATUS IN (0,1,2) AND M.REC_STATUS IN (0,1,2) AND  M.CEN_MAIN= 1 AND  C.REC_ID IN (" & CType(inParam, String) & ")" 'CEN_REC_IDs
                Case ConnectOneWS.ClientScreen.Profile_LandAndBuilding
                    Query = " SELECT COALESCE(CEN_B_NAME,'') AS CEN_B_NAME, " &
                                    "        COALESCE(CEN_ADD1,'') AS CEN_ADD1, " &
                                    "        COALESCE(CEN_ADD2,'') AS CEN_ADD2, " &
                                    "        COALESCE(CEN_ADD3,'') AS CEN_ADD3, " &
                                    "        COALESCE(CEN_ADD4,'') AS CEN_ADD4, " &
                                    "        COALESCE(CEN_CITY_ID,'') AS CEN_CITY_ID, " &
                                    "        COALESCE(CEN_STATE_ID,'') AS CEN_STATE_ID, " &
                                    "        COALESCE(CEN_DISTRICT_ID,'') AS CEN_DISTRICT_ID, " &
                                    "        COALESCE(CEN_COUNTRY,'') AS CEN_COUNTRY,  " &
                                     "        COALESCE(CEN_PINCODE,'') AS CEN_PINCODE " &
                                    " FROM CENTRE_INFO WHERE  REC_STATUS IN (0,1,2) AND CEN_ID= " & CType(inParam, String) & "" 'cBase._open_Cen_ID_Main
                Case ConnectOneWS.ClientScreen.Accounts_DonationRegister
                    Query = "SELECT MAIN.CEN_NAME,CI.CEN_INS_ID ,CI.CEN_UID,CI.CEN_PAD_NO FROM centre_info as CI INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO =MAIN.CEN_PAD_NO AND MAIN.CEN_MAIN = 1 WHERE CI.CEN_ID =" & CType(inParam, String) & " ;"
                Case ConnectOneWS.ClientScreen.Report_Potrait, ConnectOneWS.ClientScreen.Report_Landscape, ConnectOneWS.ClientScreen.Report_Collection_Box_Voucher
                    Query = " SELECT CI.CEN_NAME ,CI.CEN_BK_PAD_NO , CI.CEN_UID , COALESCE(CITY.CI_NAME, MAIN.CEN_CITY) CEN_CITY, MAIN.CEN_INCHARGE , MAIN.CEN_ZONE_ID , CI.CEN_ID , CI.REC_ID, MAIN.CEN_TEL_NO_1, MAIN.CEN_MOB_NO_1 FROM CENTRE_INFO  AS CI INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO = MAIN.CEN_PAD_NO AND MAIN.CEN_MAIN = 1 LEFT OUTER JOIN map_city_info AS CITY ON MAIN.CEN_CITY_ID = CITY.REC_ID Where CI.CEN_ID = " + CType(inParam, String) + "" 'Cen_id
                Case ConnectOneWS.ClientScreen.Report_Gift
                    Query = " SELECT CITY.CI_NAME CEN_CITY FROM CENTRE_INFO CI INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO = MAIN.CEN_BK_PAD_NO AND MAIN.CEN_MAIN =1 INNER JOIN map_city_info AS CITY ON MAIN.CEN_CITY_ID = CITY.REC_ID Where CI.CEN_ID = " + CType(inParam, String) + "" 'Cen_id
                Case ConnectOneWS.ClientScreen.Options_ResetPassword
                    Dim Parameter As Param_GetCenterDetailsByQuery_Common = CType(inParam, Param_GetCenterDetailsByQuery_Common)
                    Query = " SELECT I.INS_NAME AS 'Institution Name',C.CEN_UID AS 'UID',C.CEN_ID AS 'ID',CEN_BK_PAD_NO AS 'BK PAD', " &
                                " (SELECT CEN_NAME FROM CENTRE_INFO WHERE CEN_BK_PAD_NO=C.CEN_BK_PAD_NO AND CEN_MAIN=1) as 'Centre Name'," &
                                " (SELECT CEN_ID   FROM CENTRE_INFO WHERE CEN_BK_PAD_NO=C.CEN_BK_PAD_NO AND CEN_MAIN=1) as 'Cen_ID_Main' " &
                                " FROM Centre_Info AS C INNER JOIN Institute_Info AS I ON C.CEN_INS_ID = I.INS_ID " &
                                " WHERE C.REC_STATUS IN (0,1,2) " &
                                " AND C.CEN_BK_PAD_NO = '" & Parameter.BKPad & "' " &
                                " AND C.CEN_ID IN (" & Parameter.SelectedCenID & ") " &
                                " ORDER BY C.CEN_BK_PAD_NO, C.cen_ins_id, C.CEN_UID"
                    'Each calling function Query shall be added here , calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, Query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Center List by BK PAD No, called by Core_GetCentersByBKPAD(MessageName:="Get Center List by BK PAD No Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCentersByBKPAD</remarks>
        Public Shared Function GetCentersByBKPAD(ByVal openPADNoMain As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select CEN_INS_ID ,CEN_UID  , CEN_NAME , CEN_INCHARGE  FROM CENTRE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CEN_BK_PAD_NO ='" & openPADNoMain & "'"
            Return dbService.List(ConnectOneWS.Tables.CENTRE_INFO, Query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Center ID by Cen RecID, called by Core_GetCenterIDByCenRecID(MessageName:="Get Center ID by Centre RecID Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCenterIDByCenRecID</remarks>
        Public Shared Function GetCenterIDByCenRecID(ByVal Cen_Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select CEN_ID FROM CENTRE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID ='" & Cen_Rec_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.CENTRE_INFO, Query, ConnectOneWS.Tables.CENTRE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Institutes' Name and ID, called by Core_GetInstituteList(MessageName:="Get Institutes' List Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetInstituteList</remarks>
        Public Shared Function GetInstituteList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select INS_NAME , INS_ID, INS_SHORT  FROM INSTITUTE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Return dbService.List(ConnectOneWS.Tables.INSTITUTE_INFO, Query, ConnectOneWS.Tables.INSTITUTE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Institutes' Name and ID, called by Core_GetInstituteList(MessageName:="Get Institutes' Name,short and ID with custom column head names Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetInstituteListNameAndID, Param_GetInstituteList</remarks>
        Public Shared Function GetInstituteList(ByVal Param As Param_GetInstituteList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "select INS_NAME as '" & Param.NameColHead & "', INS_ID as '" & Param.IDColHead & "', INS_SHORT as '" & Param.SNameColHead & "' FROM INSTITUTE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Return dbService.List(ConnectOneWS.Tables.INSTITUTE_INFO, OnlineQuery, ConnectOneWS.Tables.INSTITUTE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Institutes' Name and ID, called by Core_GetInstituteList(MessageName:="Get Institutes' Name in short and ID with custom column head names Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetInstituteListNameInShort, Param_GetInstituteListNameInShort</remarks>
        Public Shared Function GetInstituteList(ByVal Param As Param_GetInstituteListNameInShort, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select INS_SHORT AS " & Param.NameColHeader & " ,INS_ID  AS " & Param.IDColHEader & "  FROM INSTITUTE_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Return dbService.List(ConnectOneWS.Tables.INSTITUTE_INFO, Query, ConnectOneWS.Tables.INSTITUTE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Institutes' Datails, specifically formatted, for letter head
        ''' called by Core_GetInstituteDetails(MessageName:="Get Institutes' Datails, specifically formatted for letter head")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetInstituteDetails</remarks>
        Public Shared Function GetInstituteDetails(ByVal InsID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT I.INS_NAME, I.INS_HEADER1, I.INS_HEADER2, I.INS_HEADER3, I.INS_HEADER4, " &
                                    "'Head Office: ' + CASE WHEN I.INS_HO_ADD1 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_ADD1)))>0 THEN LTRIM(RTRIM(I.INS_HO_ADD1)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_HO_ADD2 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_ADD2)))>0 THEN LTRIM(RTRIM(I.INS_HO_ADD2)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_HO_ADD3 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_ADD3)))>0 THEN LTRIM(RTRIM(I.INS_HO_ADD3)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_HO_ADD4 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_ADD4)))>0 THEN LTRIM(RTRIM(I.INS_HO_ADD4)) + ' ' ELSE '' END END+" &
                                    "', ' + A.MAP_CITY + ' - ' + A.MAP_PINCODE + ', ' + A.MAP_DISTRICT  + ', ' + A.MAP_STATE  + ', ' + A.MAP_COUNTRY +" &
                                    "' ' + CASE WHEN I.INS_HO_TEL_NO_1 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_TEL_NO_1)))>0 THEN 'Tel.No.: ' + LTRIM(RTRIM(I.INS_HO_TEL_NO_1)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_HO_TEL_NO_2 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_TEL_NO_2)))>0 THEN LTRIM(RTRIM(I.INS_HO_TEL_NO_2)) + ' ' ELSE '' END END+ " &
                                    "' ' + CASE WHEN I.INS_HO_FAX_NO_1 IS NULL  THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_FAX_NO_1)))>0 THEN 'Fax No.: ' + LTRIM(RTRIM(I.INS_HO_Fax_NO_1)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_HO_FAX_NO_2 IS NULL THEN  '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_FAX_NO_2)))>0 THEN LTRIM(RTRIM(I.INS_HO_FAX_NO_2)) + ' ' ELSE '' END END+ " &
                                    "' ' + CASE WHEN I.INS_HO_EMAIL_ID_1 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_EMAIL_ID_1)))>0 THEN 'Email: ' + LTRIM(RTRIM(I.INS_HO_EMAIL_ID_1)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_HO_EMAIL_ID_2 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_HO_EMAIL_ID_2)))>0 THEN LTRIM(RTRIM(I.INS_HO_EMAIL_ID_2)) + ' ' ELSE '' END END " &
                                    " AS 'INS_HO_ADDRESS', " &
                                    "'Administrative Office: ' + CASE WHEN I.INS_AO_ADD1 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_ADD1)))>0 THEN LTRIM(RTRIM(I.INS_AO_ADD1)) + ' ' ELSE ''END END+  " &
                                    "CASE WHEN I.INS_AO_ADD2 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_ADD2)))>0 THEN  LTRIM(RTRIM(I.INS_AO_ADD2)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_AO_ADD3 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_ADD3)))>0 THEN LTRIM(RTRIM(I.INS_AO_ADD3)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_AO_ADD4 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_ADD4)))>0 THEN LTRIM(RTRIM(I.INS_AO_ADD4)) + ' ' ELSE '' END END+ " &
                                    "', ' + B.MAP_CITY + ' - ' + B.MAP_PINCODE + ', ' + B.MAP_DISTRICT + ', ' + B.MAP_STATE + ', ' +  B.MAP_COUNTRY+ " &
                                    "' ' + CASE WHEN I.INS_AO_TEL_NO_1 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_TEL_NO_1)))>0 THEN 'Tel.No.: ' + LTRIM(RTRIM(I.INS_AO_TEL_NO_1)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_AO_TEL_NO_2 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_TEL_NO_2)))>0 THEN LTRIM(RTRIM(I.INS_AO_TEL_NO_2)) + ' ' ELSE '' END END+ " &
                                    "' ' + CASE WHEN I.INS_AO_FAX_NO_1 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_FAX_NO_1)))>0 THEN 'Fax No.: ' + LTRIM(RTRIM(I.INS_AO_Fax_NO_1)) + ' ' ELSE '' END END+ " &
                                    "CASE WHEN I.INS_AO_FAX_NO_2 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_FAX_NO_2)))>0 THEN LTRIM(RTRIM(I.INS_AO_FAX_NO_2)) + ' ' ELSE '' END END+ " &
                                    "' ' + CASE WHEN I.INS_AO_EMAIL_ID_1 IS NULL THEN '' ELSE  CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_EMAIL_ID_1)))>0 THEN 'Email: ' + LTRIM(RTRIM(I.INS_AO_EMAIL_ID_1)) + ' ' ELSE '' END END+ " &
                                    " CASE WHEN I.INS_AO_EMAIL_ID_2 IS NULL THEN '' ELSE CASE WHEN LEN(LTRIM(RTRIM(I.INS_AO_EMAIL_ID_2)))>0 THEN LTRIM(RTRIM(I.INS_AO_EMAIL_ID_2)) + ' ' ELSE ''END END " &
                                    " AS 'INS_AO_ADDRESS' " &
                                    " FROM  Institute_Info AS I LEFT JOIN City_Info AS A ON I.INS_HO_CITY_ID = A.REC_ID" &
                                    " LEFT JOIN City_Info AS B ON I.INS_AO_CITY_ID = B.REC_ID" &
                                    " WHERE I.INS_ID = '" & InsID & "'"
            Return dbService.List(ConnectOneWS.Tables.INSTITUTE_INFO, OnlineQuery, ConnectOneWS.Tables.INSTITUTE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Wings' Name and ID, called by Core_GetWingsList(MessageName:="Get Wings' Name and ID Function")
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetWingsList</remarks>
        Public Shared Function GetWingsList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select WING_NAME as Name,WING_ID AS ID,REC_ID FROM WINGS_INFO WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & "  ORDER BY WING_NAME "
            Return dbService.List(ConnectOneWS.Tables.WINGS_INFO, Query, ConnectOneWS.Tables.WINGS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Countries, called by Core_GetCountriesList(MessageName:="Get Countries with custom column head names Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCountriesList, Param_GetCountriesList</remarks>
        Public Shared Function GetCountriesList(ByVal Param As Param_GetCountriesList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CO_NAME AS " & Param.NameColumnHead & ", CO_CODE AS " & Param.CodeColumnHead & " ,REC_ID as " & Param.IDColumnHead & "  FROM Map_Country_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Return dbService.List(ConnectOneWS.Tables.MAP_COUNTRY_INFO, Query, ConnectOneWS.Tables.MAP_COUNTRY_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Countries By ID, called by Core_GetCountriesByID(MessageName:="Get Countries By ID with custom column head names Function ")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCountriesByID, Param_GetCountriesByID</remarks>
        Public Shared Function GetCountriesByID(ByVal Param As Param_GetCountriesByID, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CO_NAME AS " & Param.NameColumnHead & " ,REC_ID as " & Param.IDColumnHead & "  FROM Map_Country_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & Param.Country_IDs & ") ;"
            Return dbService.List(ConnectOneWS.Tables.MAP_COUNTRY_INFO, Query, ConnectOneWS.Tables.MAP_COUNTRY_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get States, called by Core_GetStatesList(MessageName:="Get States by Country Code with custom column head names Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetStatesList, Param_GetStatesList</remarks>
        Public Shared Function GetStatesList(ByVal Param As Param_GetStatesList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT ST_NAME AS " & Param.NameColumnHead & "  , ST_CODE AS " & Param.CodeColumnHead & " ,REC_ID as " & Param.IDColumnHead & " FROM Map_State_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CO_CODE='" & Param.CountryCode & "' ORDER BY ST_NAME "
            Return dbService.List(ConnectOneWS.Tables.MAP_STATE_INFO, Query, ConnectOneWS.Tables.MAP_STATE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get States by ID, called by Core_GetStatesByID(MessageName:="Get States by ID with custom column head names Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetStatesByID, Param_GetStatesByID</remarks>
        Public Shared Function GetStatesByID(ByVal Param As Param_GetStatesByID, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT ST_NAME AS " & Param.NameColumnHead & " ,REC_ID as " & Param.IDColumnHead & " FROM Map_State_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & Param.State_IDs & ") ;"
            Return dbService.List(ConnectOneWS.Tables.MAP_STATE_INFO, Query, ConnectOneWS.Tables.MAP_STATE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get DISTRICTS, called by Core_GetDistrictsList(MessageName:="Get DISTRICTS By State and Country codes with custom column heads Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetDistrictsList, Param_GetDistrictsList</remarks>
        Public Shared Function GetDistrictsList(ByVal Param As Param_GetDistrictsList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT DI_NAME  AS " & Param.NameColumnHead & " , REC_ID as " & Param.IDColumnHead & "  FROM Map_District_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CO_CODE='" & Param.CountryCode & "' AND ST_CODE= " & Param.StateCode
            Return dbService.List(ConnectOneWS.Tables.MAP_DISTRICT_INFO, Query, ConnectOneWS.Tables.MAP_DISTRICT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get DISTRICTS ByIDs, called by Core_GetDistrictsByID(MessageName:="Get DISTRICTS By IDs with custom column head names Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetDistrictsByID, Param_GetDistrictsByID</remarks>
        Public Shared Function GetDistrictsByID(ByVal Param As Param_GetDistrictsByID, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT DI_NAME  AS " & Param.NameColumnHead & " , REC_ID as " & Param.IDColumnHead & "  FROM Map_District_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & Param.District_IDs & ") ;"
            Return dbService.List(ConnectOneWS.Tables.MAP_DISTRICT_INFO, Query, ConnectOneWS.Tables.MAP_DISTRICT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Cities by Country and State, called by Core_GetCitiesList(MessageName:="Get Cities By Country And State with custom column head names Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCitiesList, Param_GetCitiesListByCountryandstate</remarks>
        Public Shared Function GetCitiesList(ByVal Param As Param_GetCitiesListByCountryandstate, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT CI_NAME AS " & Param.NameColumnHead & "  , REC_ID as " & Param.IDColumnHead & "  FROM Map_City_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CO_CODE='" & Param.CountryCode & "' AND ST_CODE= " & Param.StateCode
            Return dbService.List(ConnectOneWS.Tables.MAP_CITY_INFO, Query, ConnectOneWS.Tables.MAP_CITY_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Cities by Country, called by Core_GetCitiesList(MessageName:="Get Cities By Country with custom column head names Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCitiesListByCountry, Param_GetCitiesListByCountry</remarks>
        Public Shared Function GetCitiesList(ByVal Param As Param_GetCitiesListByCountry, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT CI_NAME AS " & Param.NameColumnHead & "  , REC_ID as " & Param.IDColumnHead & ", CO_CODE AS R_CI_CODE  FROM Map_City_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND CO_CODE='" & Param.CountryCode & "' "
            Return dbService.List(ConnectOneWS.Tables.MAP_CITY_INFO, Query, ConnectOneWS.Tables.MAP_CITY_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function to Get Cities from given IDs, called by Core_GetCitiesByID(MessageName:="Get Cities From Given IDs with custom column head names Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCitiesByID, Param_GetCitiesByID</remarks>
        Public Shared Function GetCitiesByID(ByVal Param As Param_GetCitiesByID, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT CI_NAME AS " & Param.NameColumnHead & "  , REC_ID as " & Param.IDColumnHead & "  FROM Map_City_Info WHERE  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND REC_ID IN (" & Param.City_IDs & ") ;"
            Return dbService.List(ConnectOneWS.Tables.MAP_CITY_INFO, Query, ConnectOneWS.Tables.MAP_CITY_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns City Name and RecID, called by Core_GetCities(MessageName:="Get City Name and RecID Function")
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Core_GetCities</remarks>
        Public Shared Function GetCities(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT CI_NAME, REC_ID FROM MAP_CITY_INFO"
            Return dbService.List(ConnectOneWS.Tables.MAP_CITY_INFO, Query, ConnectOneWS.Tables.MAP_CITY_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetLedgersDetails(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT LED_ID AS ID ,LED_NAME as Name,LED_TYPE AS Type from acc_ledger_info where REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND LED_ID <> '00000' ;"
            If inBasicParam.screen = ConnectOneWS.ClientScreen.Report_LedgerReport Then
                Query = "SELECT LED_ID AS ID ,LED_NAME as Name,LED_TYPE AS Type, '' AS Sub_Led_ID from acc_ledger_info where REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted & " AND LED_ID NOT IN ('00000','00079') " &
                        " UNION ALL SELECT '00079;'+BA.REC_ID, 'Bank-' + BI_SHORT_NAME + '-' + BA_ACCOUNT_NO, 'ASSET', BA.REC_ID from bank_account_info BA INNER JOIN bank_branch_info AS BR ON BA.BA_BRANCH_ID = BR.REC_ID INNER JOIN bank_info BI ON BR.BI_BANK_ID = BI.REC_ID WHERE BA_CEN_ID =" + inBasicParam.openCenID.ToString + " AND BA_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( BA_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR BA_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = BA_CEN_ID)) AND (BA_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = BA_CEN_ID) OR BA_COD_YEAR_ID= " & inBasicParam.openYearID.ToString & ")"
            End If
            Return dbService.List(ConnectOneWS.Tables.ACC_LEDGER_INFO, Query, ConnectOneWS.Tables.ACC_LEDGER_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetItemDocuments(ItemID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT ITEM_NAME, MISC_NAME AS DOC, TR_DOC_SCREEN AS ATTACH_SCREEN, II.REC_ID as ID FROM item_info II " &
                                    " INNER JOIN transaction_doc_mapping As MAP On MAP.TR_DOC_ITEM_ID = II.REC_ID " &
                                    " INNER Join misc_info AS DOC ON MAP.TR_DOC_MISC_ID = DOC.REC_ID" &
                                    " WHERE II.rec_ID ='" & ItemID & "' AND TR_DOC_TR_CODE IS NOT NULL AND COALESCE(tr_year_from_id," & inBasicParam.openYearID.ToString() & ") <= " & inBasicParam.openYearID.ToString() & " AND COALESCE(tr_year_to_id," & inBasicParam.openYearID.ToString() & ") >= " & inBasicParam.openYearID.ToString() & "" &
                                    " ORDER BY ATTACH_SCREEN, TR_DOC_TR_CODE "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING, Query, ConnectOneWS.Tables.TRANSACTION_DOC_MAPPING.ToString(), inBasicParam)
        End Function
    End Class 'AND TR_DOC_TR_CODE IS NOT NULL 
End Namespace