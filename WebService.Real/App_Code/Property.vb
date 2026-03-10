Imports System.Data

Namespace Real
    'TEST
#Region "--Profile--"
    <Serializable>
    Public Class LandAndBuilding
#Region "Param Classes"
        <Serializable>
        Public Class Param_LandAndBuilding_GetList
            Public VoucherEntry As String
            Public ProfileEntry As String
        End Class
        <Serializable>
        Public Class Param_LandAndBuilding_GetListByCondition
            Public Cur_Ins_Short_Name As String
            Public OtherCondition As String
        End Class
        <Serializable>
        Public Class Param_LandAndBuilding_GetPropertyByName
            Public Name As String
            Public ID As String
            Public Prev_YearId As Integer
            Public Next_YearId As Integer
        End Class
        <Serializable>
        Public Class Parameter_Insert_LandAndBuilding
            Public ItemID As String
            'Public YearID As String
            Public PropertyType As String
            Public Category As String
            Public Use As String
            Public Name As String
            Public Address As String
            Public LB_Add1 As String
            Public LB_Add2 As String
            Public LB_Add3 As String
            Public LB_Add4 As String
            Public LB_CountryID As String
            Public LB_CityID As String
            Public LB_StateID As String
            Public LB_DisttID As String
            Public LB_PinCode As String
            Public Ownership As String
            Public Owner_Party_ID As String
            Public SurveyNo As String
            Public TotalArea As Double
            Public ConstructedArea As Double
            Public ConstructionYear As String
            Public RCCRoof As String
            Public DepositAmount As Double
            Public PaymentDate As String
            Public MonthlyRent As Double
            Public MonthlyOtherExpenses As Double
            Public PeriodFrom As String
            Public PeriodTo As String
            Public OtherDocs As String
            Public DocNames As String
            Public Value As Double
            Public OtherDetails As String
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertMasterIDAndSrNo_LandAndBuilding : Inherits Parameter_Insert_LandAndBuilding
            Public MasterID As String
            Public SrNo As Integer
            Public UpdLbID As String ' Contains old RecId of Property that is being re-posted(updated)
            Public InsertExtInfo() As LandAndBuilding.Parameter_InsertExtendedInfo_LandAndBuilding = Nothing
            Public InsertDocInfo() As LandAndBuilding.Parameter_InsertDocInfo_LandAndBuilding = Nothing
            Public param_InsertAssetLoc As AssetLocations.Param_AssetLoc_Insert = Nothing
        End Class
        <Serializable>
        Public Class Parameter_InsertExtendedInfo_LandAndBuilding
            Public LB_Rec_ID As String
            Public SrNo As String
            Public Inst_ID As String
            Public TotalArea As Double
            Public ConstructedArea As Double
            Public ConYear As String
            Public MOU_Date As String
            Public Value As Double
            Public OtherDetails As String
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_InsertDocInfo_LandAndBuilding
            Public LB_Rec_ID As String
            Public Doc_Misc_ID As String
            Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_Property_RentDetails
            Public DepositAmount As Double
            Public PaymentDate As String
            Public MonthlyRent As Double
            Public MonthlyOtherExpenses As Double
            Public PeriodFrom As String
            Public PeriodTo As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_Update_LandAndBuilding
            Public ItemID As String
            Public PropertyType As String
            Public Category As String
            Public Use As String
            Public Name As String
            Public Address As String
            Public LB_Add1 As String
            Public LB_Add2 As String
            Public LB_Add3 As String
            Public LB_Add4 As String
            Public LB_CityID As String
            Public LB_StateID As String
            Public LB_DisttID As String
            Public LB_PinCode As String
            Public Ownership As String
            Public Owner_Party_ID As String
            Public SurveyNo As String
            Public TotalArea As Double
            Public ConstructedArea As Double
            Public ConstructionYear As String
            Public RCCRoof As String
            Public DepositAmount As Double
            Public PaymentDate As String
            Public MonthlyRent As Double
            Public MonthlyOtherExpenses As Double
            Public PeriodFrom As String
            Public PeriodTo As String
            Public OtherDocs As String
            Public DocNames As String
            Public Value As Double
            Public OtherDetails As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_LandAndBuilding_GetListByQuery
            Public Type As String
            Public RecID As String
            Public MasterID As String
            Public ItemID As String
            Public LB_Rec_ID As String
            Public Cen_ID As Integer
        End Class
        <Serializable>
        Public Class Param_LandAndBuilding_GetListForExpenses
            Public MasterID As String
            Public LB_Rec_ID As String
            Public Next_Year_ID As Integer
            Public Prev_Year_ID As Integer
        End Class
        <Serializable>
        Public Class Param_LandAndBuilding_GetPropertyListingBySP
            Public Cen_ID As Integer
            'Public Instt_ID As String
            Public YearID As Integer
            Public LB_Rec_ID As String
            Public Prev_YearID As Integer
        End Class
        <Serializable>
        Public Class Param_LandAndBuilding_Get_Location_Property_ListingBySP
            Public CEN_BK_PAD_NO As String
            Public YearID As Integer
            Public Prev_YearId As Integer
            Public Next_YearID As Integer
            Public Asset_RecID As String
            Public TR_M_ID As String
        End Class
        <Serializable>
        Public Class Param_Txn_InsertProperty_LandAndBuilding
            Public param_InsertLandAndBuilding As Parameter_Insert_LandAndBuilding
            Public InExtendedInfo() As Parameter_InsertExtendedInfo_LandAndBuilding = Nothing
            Public InDocInfo() As Parameter_InsertDocInfo_LandAndBuilding = Nothing
            Public param_InsertAssetLoc As AssetLocations.Param_AssetLoc_Insert = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_UpdateProperty_LandAndBuilding
            Public param_UpdateLandAndBuilding As Parameter_Update_LandAndBuilding
            Public RecID_DeleteExtendedInfo As String = Nothing
            Public InExtendedInfo() As Parameter_InsertExtendedInfo_LandAndBuilding = Nothing
            Public RecID_DeleteDocumentInfo As String = Nothing
            Public InDocInfo() As Parameter_InsertDocInfo_LandAndBuilding = Nothing
            Public RecID_DeleteComplexBuilding As String = Nothing
            'Public param_UpdateAssetLoc As AssetLocations.Param_AssetLoc_Update = Nothing
        End Class
        <Serializable>
        Public Class Param_Txn_DeleteProperty_LandAndBuilding
            Public RecID_DeleteExtendedInfo As String = Nothing
            Public RecID_DeleteDocumentInfo As String = Nothing
            Public RecID_Delete As String = Nothing
            Public RecID_DeleteByLB As String = Nothing
            Public RecID_DeleteComplexBuilding As String = Nothing
            Public LBOrgRecID_DeletePropertyTypeChangeLog As String = Nothing
        End Class
        <Serializable>
        Public Class Param_Veh_GetTransactions
            Public Rec_IDs As String
            Public YearID As Integer
        End Class
        <Serializable>
        Public Class Param_Get_Property_Closing
            Public Asset_Profile As ConnectOneWS.AssetProfiles
            Public Year_Id As Integer
            Public Prev_YearId As Integer
            Public Asset_RecID As String
            Public TableName As ConnectOneWS.Tables
            Public CenId As Integer = Nothing
        End Class
        <Serializable>
        Public Class Param_GetMainCenters
            Public Prev_YearId As Integer
            Public Next_YearId As Integer
            Public Asset_RecID As String
            Public BKPADNo As String = ""
        End Class
        <Serializable>
        Public Class Parameter_Update_Insurance_Register
            Public REC_ID As String
            Public TYPE As String
            Public SPL_VALUE As Decimal
            Public REMARKS As String
            Public APPLICABLE As Int16
        End Class

#End Region

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetList</remarks>
        Public Shared Function GetList(ByVal Param As Param_LandAndBuilding_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim OnlineQuery As String = " SELECT  LB_PRO_CATEGORY AS Category,LB_PRO_TYPE AS Type,LB_PRO_NAME,LB_PRO_ADDRESS as 'Address',LB_PRO_USE as 'Use of Property',LB_OWNERSHIP as Ownership,LB_SURVEY_NO,LB_TOT_P_AREA,LB_CON_AREA,LB_CON_YEAR as 'Construction Year',LB_RCC_ROOF as 'RCC Roof',LB_VALUE AS 'Opening Value',LB_VALUE AS 'Curr Value', LB_DEPOSIT_AMT as 'Deposit Amount',LB_PAID_DATE as 'Paid Date',LB_MONTH_RENT as 'Monthly Rent',LB_MONTH_O_PAYMENTS as 'Other Monthly Payments',LB_PERIOD_FROM AS 'Period From',LB_PERIOD_TO AS 'Period To',LB_OTHER_DETAIL  as 'Other Detail' ,LB_COD_YEAR_ID AS YearID, 'Un-Sold' AS 'Sale Status','' as 'Sale Date',LB_ADDRESS1, LB_ADDRESS2,LB_ADDRESS3,LB_ADDRESS4,LB_COUNTRY_ID,LB_STATE_ID,LB_DISTRICT_ID,LB_CITY_ID,LB_PINCODE,REC_ID AS ID ,CASE WHEN LB_TR_ID IS NULL  OR LB_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & " THEN '' ELSE LB_TR_ID END  as TR_ID,CASE WHEN LB_TR_ID IS NULL OR LB_COD_YEAR_ID <> " & inBasicParam.openYearID.ToString & "  THEN '" & Param.ProfileEntry & "' ELSE '" & Param.VoucherEntry & "' END as 'Entry Type'," & Common.Remarks_Detail("LAND_BUILDING_INFO", DataFunctions.GetCurrentDateTime(inBasicParam), Common.Server_Date_Format_Short) & "," & Common.Rec_Detail("Land_Building_Info") & "" & _
            '                              " FROM Land_Building_Info " & _
            '                              " Where   REC_STATUS IN (0,1,2) AND LB_CEN_ID=" & inBasicParam.openCenID.ToString & "  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "); "
            'Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_INFO, OnlineQuery, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)

            Dim paramters As String() = {"@CEN_ID", "@YEAR_ID", "@ProfileEntry", "@VoucherEntry"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.ProfileEntry, Param.VoucherEntry}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.Int32, DbType.String, DbType.String}
            Dim lengths() As Integer = {4, 4, 100, 100}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, "[sp_get_LB_Profile]", ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)

        End Function

        ''' <summary>
        ''' Returns Property Profile Listing
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function Get_ProfileListing(ByVal Param As Assets.Param_GetProfileListing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Property_Profile"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "@UserID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 255}
            Return dbService.ListFromSP(Param.TableName, SPName, Param.TableName.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Get_Property_Closing(ByVal Param As Param_Get_Property_Closing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Get_Property_ClosingValues"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "ASSET_RECID"}
            'If Param.Year_Id = "" Then Param.Year_Id = inBasicParam.openYearID
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.CenId = "" Then Param.CenId = inBasicParam.openCenID
            Dim values() As Object = {Param.CenId, Param.Year_Id, Param.Prev_YearId, Param.Asset_RecID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 36}
            Return dbService.ListFromSP(Param.TableName, SPName, Param.TableName.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetAllPropertyList(ByVal BK_PAD_NO As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT I.INS_SHORT AS 'Institute',C.CEN_NAME AS 'Centre Name',C.CEN_UID AS 'UID',C.CEN_PAD_NO AS 'No.',L.LB_PRO_NAME as 'Property Name' , L.LB_PRO_CATEGORY as 'Category',L.LB_PRO_TYPE as 'Type',L.LB_PRO_USE as 'Use', L.REC_ID as 'LB_ID',L.REC_EDIT_ON " & _
                                        " FROM Land_Building_Info AS L  " & _
                                        " INNER JOIN Centre_info AS C ON (L.LB_CEN_ID=C.CEN_ID) " & _
                                        " INNER JOIN Institute_info AS I ON (C.CEN_INS_ID=I.INS_ID) " & _
                                        " LEFT OUTER JOIN Transaction_D_Payment_Info AS TP ON TP.REC_STATUS IN (0,1,2) AND TP.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID = L.REC_ID " & _
                                        " Where L.REC_STATUS IN (0,1,2) AND C.CEN_BK_PAD_NO='" & BK_PAD_NO & "' AND COALESCE(TP.TR_REF_AMT,-1) < 0 " & _
                                        " and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & "); " & _
                                        " "
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_INFO, onlineQuery, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets List For Expenses
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetListForExpenses</remarks>
        Public Shared Function GetListForExpenses(ByVal Param As Param_LandAndBuilding_GetListForExpenses, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "LB_RECID", "TR_M_ID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_Year_ID, Param.Next_Year_ID, Param.LB_Rec_ID, Param.MasterID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32, DbType.Int32, DbType.[String], DbType.[String]}
            Dim lengths As Integer() = {5, 4, 4, 4, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, "sp_get_Property_Ref_Listing", ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets List For Expenses
        ''' </summary>
        ''' <param name="MasterID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetListForExpenses</remarks>
        Public Shared Function GetListForMOU(ByVal NEXT_YEARID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim paramters As String() = {"CENID", "YEARID", "NEXT_YEARID"}
            Dim values As Object() = {inBasicParam.openCenID, inBasicParam.openYearID, NEXT_YEARID}
            Dim dbTypes As System.Data.DbType() = {DbType.Int32, DbType.Int32, DbType.Int32}
            Dim lengths As Integer() = {5, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, "sp_get_Property_MOU_Listing", ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), paramters, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Extension Details
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetExtensionDetails</remarks>
        Public Shared Function GetExtensionDetails(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT LB_SR_NO, LB_INS_ID, LB_TOT_P_AREA, LB_CON_AREA,LB_CON_YEAR, LB_MOU_DATE, LB_VALUE, LB_OTHER_DETAIL FROM LAND_BUILDING_EXTENDED_INFO where LB_REC_ID ='" & RecID & "'"
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        '''  Gets Building Documents Info
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetDocsDetails</remarks>
        Public Shared Function GetDocsDetails(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT LB_MISC_ID FROM LAND_BUILDING_DOCUMENTS_INFO where LB_REC_ID ='" & RecID & "'"
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get List By Condition
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetListByCondition</remarks>
        Public Shared Function GetListByCondition(ByVal Param As Param_LandAndBuilding_GetListByCondition, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "  SELECT  LB_PRO_CATEGORY  ,LB_PRO_TYPE  ,LB_PRO_NAME, LB_PRO_ADDRESS,LB_PRO_USE,CASE WHEN LB_OWNERSHIP='INSTITUTION' THEN '" & Param.Cur_Ins_Short_Name & "' ELSE LB_OWNERSHIP END  AS LB_OWNERSHIP ,LB_SURVEY_NO,LB_TOT_P_AREA,LB_CON_AREA,LB_CON_YEAR,LB_RCC_ROOF,LB_DEPOSIT_AMT,LB_PAID_DATE,LB_MONTH_RENT,LB_MONTH_O_PAYMENTS,LB_PERIOD_FROM,LB_PERIOD_TO,LB_OTHER_DETAIL ,LB_VALUE,LB_ADDRESS1, LB_ADDRESS2,LB_ADDRESS3,LB_ADDRESS4,LB_COUNTRY_ID,LB_STATE_ID,LB_DISTRICT_ID,LB_CITY_ID,LB_PINCODE,REC_ID AS ID  " & _
                                        " FROM Land_Building_Info " & _
                                        " Where   REC_STATUS IN (0,1,2) AND LB_CEN_ID=" & inBasicParam.openCenID.ToString & " " & Param.OtherCondition
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_INFO, OnlineQuery, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Common Function
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetListByQuery_Common</remarks>
        Public Shared Function GetListByQuery_Common(inBasicParam As ConnectOneWS.Basic_Param, Optional ByVal Param As Param_LandAndBuilding_GetListByQuery = Nothing) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Select Case inBasicParam.screen
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Payment
                    If Param.Type.ToUpper() = "1" Then
                        Query = "SELECT LB_TR_ID,LB_TR_ITEM_SRNO,LB_PRO_TYPE, LB_PRO_CATEGORY, LB_PRO_USE, LB_PRO_NAME, LB_PRO_ADDRESS, LB_OWNERSHIP, LB_OWNERSHIP_PARTY_ID, LB_SURVEY_NO, LB_CON_YEAR, LB_RCC_ROOF, LB_PAID_DATE, LB_PERIOD_FROM, LB_PERIOD_TO, LB_DOC_OTHERS, LB_DOC_NAME, LB_OTHER_DETAIL , LB_TOT_P_AREA, LB_CON_AREA, LB_DEPOSIT_AMT, LB_MONTH_RENT, LB_MONTH_O_PAYMENTS, REC_ID AS LB_REC_ID,LB_ADDRESS1, LB_ADDRESS2,LB_ADDRESS3,LB_ADDRESS4,LB_COUNTRY_ID,LB_STATE_ID,LB_DISTRICT_ID,LB_CITY_ID,LB_PINCODE,REC_ID FROM Land_Building_Info WHERE REC_STATUS IN (0,1,2) AND LB_TR_ID ='" & Param.RecID & "' ORDER BY LB_TR_ITEM_SRNO"
                    Else
                        Query = "SELECT LB_PRO_NAME FROM land_building_info where  REC_STATUS IN (0,1,2) AND LB_CEN_ID=" & inBasicParam.openCenID.ToString & "  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " )"
                        If Param.MasterID.Length > 0 Then Query += " AND COALESCE(LB_TR_ID,'') !='" & Param.MasterID & "'"
                    End If

                Case ConnectOneWS.ClientScreen.Accounts_Voucher_Journal
                    If Not Param.ItemID Is Nothing Then
                        Query = "SELECT LB_PRO_TYPE as 'Type' , LB_PRO_CATEGORY as Category, LB_PRO_USE AS 'Use', LB_PRO_NAME as Item, CASE WHEN LB_OWNERSHIP ='INSTITUTION' THEN 'INSTITUTION' ELSE AB.C_NAME END AS OWNER,  COALESCE(LB_VALUE,0) as 'Org Value',  COALESCE(LB_VALUE,0) as 'Curr Value' ,LB.REC_ID, LB.REC_EDIT_ON FROM land_building_info AS LB LEFT OUTER JOIN address_book AS AB  ON AB.REC_ID = LB.LB_OWNERSHIP_PARTY_ID AND C_CEN_ID =" & inBasicParam.openCenID.ToString & " WHERE LB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND LB_ITEM_ID = '" & Param.ItemID & "' and LB.REC_STATUS IN (0,1,2) AND UPPER(LB_PRO_CATEGORY) NOT IN ('RENTED','FREE USE', 'LEASED (SHORT TERM)','MORTGAGE (SHORT TERM)')  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " ) "
                    Else
                        Query = "SELECT LB_PRO_TYPE as 'Type' , LB_PRO_CATEGORY as Category, LB_PRO_USE AS 'Use', LB_PRO_NAME as Item, CASE WHEN LB_OWNERSHIP ='INSTITUTION' THEN 'INSTITUTION' ELSE AB.C_NAME END AS OWNER,  COALESCE(LB_VALUE,0) as 'Org Value',  COALESCE(LB_VALUE,0) as 'Curr Value' ,LB.REC_ID, LB.REC_EDIT_ON FROM land_building_info AS LB LEFT OUTER JOIN address_book AS AB  ON AB.REC_ID = LB.LB_OWNERSHIP_PARTY_ID AND C_CEN_ID =" & inBasicParam.openCenID.ToString & " WHERE LB_CEN_ID=" & inBasicParam.openCenID.ToString & " and LB.REC_STATUS IN (0,1,2) AND UPPER(LB_PRO_CATEGORY) NOT IN ('RENTED','FREE USE', 'LEASED (SHORT TERM)','MORTGAGE (SHORT TERM)')  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    End If
                    If Not Param.LB_Rec_ID Is Nothing Then
                        Query += " AND LB.REC_ID  = '" & Param.LB_Rec_ID & "'"
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_SaleOfAsset
                    Query = " SELECT  '' AS 'REF_ITEM',LB_ITEM_ID AS 'REF_ITEM_ID',1 AS 'REF_QTY',LB_PRO_NAME + ', ' +  LB_PRO_CATEGORY + ' (' + LB_PRO_USE + ')' as 'REF_DESC','' AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0.0 AS 'SALE_QTY',REC_EDIT_ON  FROM Land_Building_Info  Where   REC_STATUS IN (0,1,2) AND LB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND LB_PRO_CATEGORY IN ('PURCHASED','PURCHASED AND CONSTRUCTED','GIFTED','GIFTED AND CONSTRUCTED')  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
                    If Not Param.LB_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND REC_ID = '" & Param.LB_Rec_ID & "' "
                    End If
                Case ConnectOneWS.ClientScreen.Accounts_Voucher_AssetTransfer
                    Query = " SELECT  '' AS 'REF_ITEM',LB_ITEM_ID AS 'REF_ITEM_ID',1 AS 'REF_QTY',LB_PRO_NAME + ', ' +  LB_PRO_CATEGORY + ' (' + LB_PRO_USE + ')' as 'REF_DESC',LB_PRO_NAME AS 'REF_NAME', '' AS 'REF_MISC_ID' ,'' as 'REF_LED_ID','' as REF_TRANS_TYPE, REC_ID AS 'REF_ID',0.0 AS 'SALE_QTY',LB_OWNERSHIP AS 'REF_OWNERSHIP',(select	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = LB_OWNERSHIP_PARTY_ID) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  AS 'REF_OWNERSHIP_ID',LB_PRO_USE AS 'REF_USE',REC_EDIT_ON FROM Land_Building_Info  Where   REC_STATUS IN (0,1,2) AND LB_CEN_ID=" & Param.Cen_ID.ToString & " AND LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & "  and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") AND LB_PRO_CATEGORY IN ('PURCHASED','PURCHASED AND CONSTRUCTED','GIFTED','GIFTED AND CONSTRUCTED','LEASED (Long Term)','LEASED AND CONSTRUCTED (Long Term)')  "
                    If Not Param.LB_Rec_ID Is Nothing Then 'added for multiuser check
                        Query += " AND REC_ID = '" & Param.LB_Rec_ID & "' "
                    End If

                    'Each calling function Query shall be added here, calling function need not provide the query, it will be identified by ClientScreen
            End Select
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_PropertyListingBySP(ByVal Param As Param_LandAndBuilding_GetPropertyListingBySP, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Property_Listing"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "LB_REC_ID"}
            Dim values() As Object = {Param.Cen_ID, Param.YearID, Param.Prev_YearID, Param.LB_Rec_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, SPName, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Get_Location_Property_ListingBySP(Param As Param_LandAndBuilding_Get_Location_Property_ListingBySP, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Location_Property_Listing"
            Dim params() As String = {"CEN_BK_PADNO", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "TR_M_ID"}
            'If Param.Next_YearID = "" Then Param.Next_YearID = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.Asset_RecID = "" Then Param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.TR_M_ID = "" Then Param.TR_M_ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {Param.CEN_BK_PAD_NO, Param.YearID, Param.Prev_YearId, Param.Next_YearID, Param.Asset_RecID, Param.TR_M_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, SPName, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Property Record
        Public Shared Function GetRecord(inBasicParam As ConnectOneWS.Basic_Param, lb_RecID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim onlineQuery As String = " SELECT LB_CEN_ID,LB_PRO_TYPE,LB_PRO_CATEGORY,LB_PRO_USE,LB_PRO_NAME,LB_OWNERSHIP,(select	REC_ID FROM address_book WHERE C_ORG_REC_ID = (select C_ORG_REC_ID from address_book where REC_ID = LB_OWNERSHIP_PARTY_ID) AND C_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") LB_OWNERSHIP_PARTY_ID" &
                                          ",LB_SURVEY_NO,LB_TOT_P_AREA,LB_CON_AREA,LB_CON_YEAR,LB_RCC_ROOF,LB_DEPOSIT_AMT,LB_PAID_DATE,LB_MONTH_RENT,LB_MONTH_O_PAYMENTS,LB_PERIOD_FROM" &
                                          ",LB_PERIOD_TO,LB_VALUE,LB_DOC_OTHERS,LB_DOC_NAME,LB_OTHER_DETAIL,REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON" &
                                          ",REC_STATUS_BY,REC_ID, LB_PRO_ADDRESS,LB_TR_ID,LB_TR_ITEM_SRNO,LB_ITEM_ID,LB_COD_YEAR_ID,LB_INSURANCE_APPLICABLE,LB_REMARKS_FOR_INSURANCE,LB_SPECIAL_VALUE,LB_INSURANCE_SRNO" &
                                          ",LB_ADDRESS1, LB_ADDRESS2,LB_ADDRESS3,LB_ADDRESS4,LB_COUNTRY_ID,LB_STATE_ID,LB_DISTRICT_ID,LB_CITY_ID,LB_PINCODE,LB_ORG_REC_ID" &
                                          " FROM land_building_info" &
                                        " Where REC_ID = '" & lb_RecID & "' "
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_INFO, onlineQuery, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Transaction Details
        ''' </summary>
        ''' <param name="Rec_IDs"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetTransactions</remarks>
        Public Shared Function GetTransactions(ByVal inParam As Param_Veh_GetTransactions, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " SELECT TP.TR_REF_ID, SUM(TM.TR_SALE_QTY) AS 'Sale Quantity',MAX(TM.TR_SALE_DATE) AS 'Sale Date'  FROM Transaction_D_Master_Info AS TM INNER JOIN Transaction_D_Payment_Info AS TP ON TM.REC_ID = TP.TR_M_ID WHERE  TM.REC_STATUS IN (0,1,2) AND TP.REC_STATUS IN (0,1,2) AND TM.TR_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND TP.TR_PAY_TYPE IN ('SALE') AND TP.TR_REF_ID IN (" & inParam.Rec_IDs & ") AND TM.TR_CODE=11  and TM.TR_COD_YEAR_ID =" & inParam.YearID.ToString & "  GROUP BY TP.TR_REF_ID "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO, OnlineQuery, ConnectOneWS.Tables.TRANSACTION_D_PAYMENT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Center Count By Name
        ''' </summary>
        ''' <param name="Name"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetCenterCountByName</remarks>
        Public Shared Function GetCenterCountByName(ByVal Name As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM Land_Building_Info  WHERE REC_STATUS IN (0,1,2) AND LB_CEN_ID=" & inBasicParam.openCenID.ToString & " AND UPPER(LB_PRO_NAME)  = '" & Name.ToUpper & "'  and LB_COD_YEAR_ID <=" & inBasicParam.openYearID.ToString & " and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            Return dbService.GetScalar(ConnectOneWS.Tables.LAND_BUILDING_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Center Count By Name And Id
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetCenterCountByNameAndId, uses Param_LandAndBuilding_GetCenterCountByName</remarks>
        Public Shared Function GetPropertyByName(ByVal Param As Param_LandAndBuilding_GetPropertyByName, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Property_ExistingName"
            Dim params() As String = {"CENID", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID", "PROP_NAME"}
            'If Param.Prev_YearId = "" Then Param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            'If Param.Next_YearId = "" Then Param.Next_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If Param.ID = "" Then Param.ID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Param.Prev_YearId, Param.Next_YearId, Param.ID, Param.Name}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, SPName, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        ''' Gets Main Centres Count
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetMainCenterCount</remarks>
        Public Shared Function GetMainCenters(param As Param_GetMainCenters, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Property_MainCenter"
            Dim params() As String = {"BK_PAD_NO", "YEARID", "PREV_YEARID", "NEXT_YEARID", "ASSET_RECID"}
            'If param.Prev_YearId = "" Then param.Prev_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            'If param.Next_YearId = "" Then param.Next_YearId = Nothing ' Nullable Parameters , passed NULL if Empty
            If param.Asset_RecID = "" Then param.Asset_RecID = Nothing ' Nullable Parameters , passed NULL if Empty
            Dim values() As Object = {param.BKPADNo, inBasicParam.openYearID, param.Prev_YearId, param.Next_YearId, param.Asset_RecID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {5, 4, 4, 4, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.LAND_BUILDING_INFO, SPName, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ' ''' <summary>
        ' ''' Gets Main Centres Count By RecID
        ' ''' </summary>
        ' ''' <param name="RecID"></param>
        ' ''' <param name="Screen"></param>
        ' ''' <param name="openUserID"></param>
        ' ''' <param name="openCenID"></param>
        ' ''' <param name="PCID"></param>
        ' ''' <param name="version"></param>
        ' ''' <returns></returns>
        ' ''' <remarks>RealServiceFunctions.Property_GetMainCenterCountByRecID</remarks>
        'Public Shared Function GetMainCenterCount(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim OnlineQuery As String = "SELECT COUNT(REC_ID) FROM Land_Building_Info  WHERE REC_STATUS IN (0,1,2) AND LB_CEN_ID='" & inBasicParam.openCenID & "' AND UPPER(LB_PRO_USE)  = 'MAIN CENTRE' AND REC_ID <> '" & RecID & "'  and LB_COD_YEAR_ID <='" & inBasicParam.openYearID & "' and ( LB_COD_YEAR_ID ='" & inBasicParam.openYearID & "' OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID)) AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = '" & inBasicParam.openYearID & "') "
        '    Return dbService.GetScalar(ConnectOneWS.Tables.LAND_BUILDING_INFO, OnlineQuery, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        'End Function

        ''' <summary>
        ''' Gets Transaction Count
        ''' </summary>
        ''' <param name="LB_ID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetTransactionCount</remarks>
        Public Shared Function GetTransactionCount(ByVal LB_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT Count(REC_ID) FROM TRANSACTION_INFO WHERE REC_STATUS IN (0,1,2) AND TR_TRF_CROSS_REF_ID ='" & LB_ID & "'"
            Return dbService.GetScalar(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Gets IDs By TransactionID
        ''' </summary>
        ''' <param name="TxnID"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_GetIDsBytxnID</remarks>
        Public Shared Function GetIDsBytxnID(ByVal TxnID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT REC_ID FROM Land_Building_Info WHERE LB_TR_ID ='" & TxnID & "' and  REC_STATUS IN (0,1,2)"
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function GetPendingTfs_LocNames(inBasicParam As ConnectOneWS.Basic_Param, ByVal Cen_RecID As String) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "select LB_PRO_NAME from land_building_info where rec_id in (select distinct TR_REF_OTHERS from transaction_info where TR_CODE = 15 and TR_AB_ID_1 ='" & Cen_RecID & "' and TR_TRF_CROSS_REF_ID is NULL)"
            Return dbService.List(ConnectOneWS.Tables.LAND_BUILDING_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_LandAndBuilding, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Owner_Party_ID Is Nothing Then InParam.Owner_Party_ID = " NULL "
            Dim OnlineQuery As String = "INSERT INTO Land_Building_Info(LB_CEN_ID,LB_COD_YEAR_ID,LB_ITEM_ID,LB_PRO_TYPE,LB_PRO_CATEGORY,LB_PRO_USE,LB_PRO_NAME,LB_PRO_ADDRESS,LB_OWNERSHIP,LB_OWNERSHIP_PARTY_ID,LB_SURVEY_NO,LB_TOT_P_AREA,LB_CON_AREA,LB_CON_YEAR,LB_RCC_ROOF," & _
                                                  "LB_DEPOSIT_AMT,LB_PAID_DATE,LB_MONTH_RENT,LB_MONTH_O_PAYMENTS,LB_PERIOD_FROM,LB_PERIOD_TO,LB_DOC_OTHERS,LB_DOC_NAME,LB_VALUE,LB_OTHER_DETAIL,LB_ADDRESS1,LB_ADDRESS2,LB_ADDRESS3,LB_ADDRESS4,LB_COUNTRY_ID,LB_STATE_ID,LB_DISTRICT_ID,LB_CITY_ID,LB_PINCODE," & _
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID,LB_ORG_REC_ID" & _
                                                  ") VALUES(" & _
                                                  "" & inBasicParam.openCenID.ToString & "," & _
                                                  "" & inBasicParam.openYearID.ToString & "," & _
                                                  "'" & InParam.ItemID & "', " & _
                                                  "'" & InParam.PropertyType & "', " & _
                                                  "'" & InParam.Category & "', " & _
                                                  "'" & InParam.Use & "', " & _
                                                  "'" & InParam.Name & "', " & _
                                                  "'" & InParam.Address & "', " & _
                                                  "'" & InParam.Ownership & "', " & _
                                                  " " & InParam.Owner_Party_ID & " , " & _
                                                  "'" & InParam.SurveyNo & "', " & _
                                                  " " & InParam.TotalArea & ", " & _
                                                  " " & InParam.ConstructedArea & ", " & _
                                                  " '" & InParam.ConstructionYear & "', " & _
                                                  " '" & InParam.RCCRoof & "' , " & _
                                                  " " & InParam.DepositAmount & ", " & _
                                                  " " & If(IsDate(InParam.PaymentDate), "'" & Convert.ToDateTime(InParam.PaymentDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                  " " & InParam.MonthlyRent & ", " & _
                                                  " " & InParam.MonthlyOtherExpenses & ", " & _
                                                  " " & If(IsDate(InParam.PeriodFrom), "'" & Convert.ToDateTime(InParam.PeriodFrom).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                  " " & If(IsDate(InParam.PeriodTo), "'" & Convert.ToDateTime(InParam.PeriodTo).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                                  " '" & InParam.OtherDocs & "' , " & _
                                                  " '" & InParam.DocNames & "', " & _
                                                  " " & InParam.Value & ",'" & InParam.OtherDetails & "', " & _
                                                  " '" & InParam.LB_Add1 & "' , " & _
                                                  " '" & InParam.LB_Add2 & "' , " & _
                                                  " '" & InParam.LB_Add3 & "' , " & _
                                                  " '" & InParam.LB_Add4 & "' , " & _
                                                  " " & IIf(InParam.LB_CountryID Is Nothing, "NULL", "'" & InParam.LB_CountryID & "'") & ", " & _
                                                  " " & IIf(InParam.LB_StateID Is Nothing, "NULL", "'" & InParam.LB_StateID & "'") & ", " & _
                                                  " " & IIf(InParam.LB_DisttID Is Nothing, "NULL", "'" & InParam.LB_DisttID & "'") & ", " & _
                                                  " " & IIf(InParam.LB_CityID Is Nothing, "NULL", "'" & InParam.LB_CityID & "'") & ", " & _
                                                  " '" & InParam.LB_PinCode & "',  " & _
                                                  "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "', '" & InParam.RecID & "'  " & ")"
            dbService.Insert(ConnectOneWS.Tables.LAND_BUILDING_INFO, OnlineQuery, inBasicParam, InParam.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert
        ''' Parameter 'AddTime' is added for txn scope
        ''' </summary>
        ''' <param name="InParam1"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_InsertMasterIdSrNo</remarks>
        Public Shared Function Insert(ByVal InParam1 As Parameter_InsertMasterIDAndSrNo_LandAndBuilding, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam1.Owner_Party_ID Is Nothing Then InParam1.Owner_Party_ID = " NULL "
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InParam1.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Land_Building_Info(LB_CEN_ID,LB_COD_YEAR_ID, LB_ITEM_ID,LB_PRO_TYPE,LB_PRO_CATEGORY,LB_PRO_USE,LB_PRO_NAME,LB_PRO_ADDRESS,LB_OWNERSHIP,LB_OWNERSHIP_PARTY_ID,LB_SURVEY_NO,LB_TOT_P_AREA,LB_CON_AREA,LB_CON_YEAR,LB_RCC_ROOF," &
                                                  "LB_DEPOSIT_AMT,LB_PAID_DATE,LB_MONTH_RENT,LB_MONTH_O_PAYMENTS,LB_PERIOD_FROM,LB_PERIOD_TO,LB_DOC_OTHERS,LB_DOC_NAME,LB_VALUE,LB_OTHER_DETAIL,LB_TR_ID,LB_TR_ITEM_SRNO,LB_ADDRESS1,LB_ADDRESS2,LB_ADDRESS3,LB_ADDRESS4,LB_COUNTRY_ID,LB_STATE_ID,LB_DISTRICT_ID,LB_CITY_ID,LB_PINCODE," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID,LB_ORG_REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InParam1.ItemID & "', " &
                                                  "'" & InParam1.PropertyType & "', " &
                                                  "'" & InParam1.Category & "', " &
                                                  "'" & InParam1.Use & "', " &
                                                  "'" & InParam1.Name & "', " &
                                                  "'" & InParam1.Address & "', " &
                                                  "'" & InParam1.Ownership & "', " &
                                                  " " & InParam1.Owner_Party_ID & " , " &
                                                  "'" & InParam1.SurveyNo & "', " &
                                                  " " & InParam1.TotalArea & ", " &
                                                  " " & InParam1.ConstructedArea & ", " &
                                                  " '" & InParam1.ConstructionYear & "', " &
                                                  " '" & InParam1.RCCRoof & "' , " &
                                                  " " & InParam1.DepositAmount & ", " &
                                                  " " & If(IsDate(InParam1.PaymentDate), "'" & Convert.ToDateTime(InParam1.PaymentDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                  " " & InParam1.MonthlyRent & ", " &
                                                  " " & InParam1.MonthlyOtherExpenses & ", " &
                                                  " " & If(IsDate(InParam1.PeriodFrom), "'" & Convert.ToDateTime(InParam1.PeriodFrom).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                  " " & If(IsDate(InParam1.PeriodTo), "'" & Convert.ToDateTime(InParam1.PeriodTo).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                                  " '" & InParam1.OtherDocs & "' , " &
                                                  " '" & InParam1.DocNames & "', " &
                                                  " " & InParam1.Value & ",'" & InParam1.OtherDetails & "', " &
                                                  "'" & InParam1.MasterID & "', " &
                                                  " " & InParam1.SrNo & " , " &
                                                   " '" & InParam1.LB_Add1 & "' , " &
                                                  " '" & InParam1.LB_Add2 & "' , " &
                                                  " '" & InParam1.LB_Add3 & "' , " &
                                                  " '" & InParam1.LB_Add4 & "' , " &
                                                  " " & IIf(InParam1.LB_CountryID Is Nothing, "NULL", "'" & InParam1.LB_CountryID & "'") & ", " &
                                                  " " & IIf(InParam1.LB_StateID Is Nothing, "NULL", "'" & InParam1.LB_StateID & "'") & ", " &
                                                  " " & IIf(InParam1.LB_DisttID Is Nothing, "NULL", "'" & InParam1.LB_DisttID & "'") & ", " &
                                                  " " & IIf(InParam1.LB_CityID Is Nothing, "NULL", "'" & InParam1.LB_CityID & "'") & ", " &
                                                  " '" & InParam1.LB_PinCode & "',  " &
                                                  "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InParam1.RecID & "', '" & InParam1.RecID & "' " & ")"

            dbService.Insert(ConnectOneWS.Tables.LAND_BUILDING_INFO, OnlineQuery, inBasicParam, InParam1.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Inserts Building Extended Info
        ''' </summary>
        ''' <param name="InEInfo"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_InsertExtendedInfo</remarks>
        Public Shared Function InsertExtendedInfo(ByVal InEInfo As Parameter_InsertExtendedInfo_LandAndBuilding, inBasicParam As ConnectOneWS.Basic_Param, Optional InsertTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InEInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Land_Building_Extended_Info (LB_CEN_ID,LB_REC_ID,LB_SR_NO,LB_INS_ID,LB_TOT_P_AREA,LB_CON_AREA,LB_CON_YEAR,LB_MOU_DATE,LB_VALUE,LB_OTHER_DETAIL," &
                                    "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                    ") VALUES(" &
                                    "" & inBasicParam.openCenID.ToString & "," &
                                    "'" & InEInfo.LB_Rec_ID & "'," &
                                    " " & InEInfo.SrNo & "," &
                                    "'" & InEInfo.Inst_ID & "'," &
                                    " " & InEInfo.TotalArea & ", " &
                                    " " & InEInfo.ConstructedArea & ", " &
                                    "'" & InEInfo.ConYear & "', " &
                                    " " & If(IsDate(InEInfo.MOU_Date), "'" & Convert.ToDateTime(InEInfo.MOU_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                    " " & InEInfo.Value & ", " &
                                    "'" & InEInfo.OtherDetails & "', " &
                                    "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InEInfo.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, OnlineQuery, inBasicParam, InEInfo.RecID, Nothing, InsertTime)
            Return True
        End Function

        ''' <summary>
        ''' Inserts Documents Info
        ''' </summary>
        ''' <param name="InDocInfo"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_InsertDocumentsInfo</remarks>
        Public Shared Function InsertDocumentsInfo(ByVal InDocInfo As Parameter_InsertDocInfo_LandAndBuilding, inBasicParam As ConnectOneWS.Basic_Param, Optional InsertTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            If Param Is Nothing Then
                Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "'," & InDocInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',"
            Else
                Str = " '" & Param.LastAddOn.ToString(Common.DateFormatLong) & "' , '" & Param.LastAddBy & "' , " & Param.LastStatus & ", '" & Param.LastStatusOn.ToString(Common.DateFormatLong) & "', '" & Param.LastStatusBy & "-WA" & "' , "
            End If
            Dim OnlineQuery As String = "INSERT INTO Land_Building_Documents_Info (LB_CEN_ID,LB_REC_ID,LB_MISC_ID," &
                                "REC_ADD_ON,REC_ADD_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_EDIT_ON,REC_EDIT_BY,REC_ID" &
                                ") VALUES(" &
                                "" & inBasicParam.openCenID.ToString & "," &
                                "'" & InDocInfo.LB_Rec_ID & "'," &
                                "'" & InDocInfo.Doc_Misc_ID & "'," &
                                    "" & Str & " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & InDocInfo.RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, OnlineQuery, inBasicParam, InDocInfo.RecID, Nothing, InsertTime)
            Return True
        End Function

        ''' <summary>
        ''' Updates Land And Building Info
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_LandAndBuilding, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.Owner_Party_ID Is Nothing Then UpParam.Owner_Party_ID = " NULL "
            Dim OnlineQuery As String = "  UPDATE Land_Building_Info SET " & _
                                         "LB_ITEM_ID           ='" & UpParam.ItemID & "', " & _
                                         "LB_PRO_TYPE           ='" & UpParam.PropertyType & "', " & _
                                         "LB_PRO_CATEGORY       ='" & UpParam.Category & "', " & _
                                         "LB_PRO_USE            ='" & UpParam.Use & "', " & _
                                         "LB_PRO_NAME           ='" & UpParam.Name & "', " & _
                                         "LB_PRO_ADDRESS        ='" & UpParam.Address & "', " & _
                                         "LB_OWNERSHIP          ='" & UpParam.Ownership & "', " & _
                                         "LB_OWNERSHIP_PARTY_ID = " & UpParam.Owner_Party_ID & " , " & _
                                         "LB_SURVEY_NO          ='" & UpParam.SurveyNo & "', " & _
                                         "LB_TOT_P_AREA         = " & UpParam.TotalArea & ", " & _
                                         "LB_CON_AREA           = " & UpParam.ConstructedArea & ", " & _
                                         "LB_CON_YEAR           = '" & UpParam.ConstructionYear & "', " & _
                                         "LB_RCC_ROOF           = '" & UpParam.RCCRoof & "', " & _
                                         "LB_DEPOSIT_AMT        = " & UpParam.DepositAmount & ", " & _
                                         "LB_PAID_DATE          = " & If(IsDate(UpParam.PaymentDate), "'" & Convert.ToDateTime(UpParam.PaymentDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                         "LB_MONTH_RENT         = " & UpParam.MonthlyRent & ", " & _
                                         "LB_MONTH_O_PAYMENTS   = " & UpParam.MonthlyOtherExpenses & ", " & _
                                         "LB_PERIOD_FROM        = " & If(IsDate(UpParam.PeriodFrom), "'" & Convert.ToDateTime(UpParam.PeriodFrom).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                         "LB_PERIOD_TO          = " & If(IsDate(UpParam.PeriodTo), "'" & Convert.ToDateTime(UpParam.PeriodTo).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " & _
                                         "LB_DOC_OTHERS         = '" & UpParam.OtherDocs & "', " & _
                                         "LB_DOC_NAME           = '" & UpParam.DocNames & "', " & _
                                         "LB_OTHER_DETAIL       = '" & UpParam.OtherDetails & "', " & _
                                         "LB_VALUE              =  " & UpParam.Value & ", " & _
                                        "LB_ADDRESS1       = '" & UpParam.LB_Add1 & "', " & _
                                        "LB_ADDRESS2       = '" & UpParam.LB_Add2 & "', " & _
                                        "LB_ADDRESS3       = '" & UpParam.LB_Add3 & "', " & _
                                        "LB_ADDRESS4       = '" & UpParam.LB_Add4 & "', " & _
                                        "LB_STATE_ID       = '" & UpParam.LB_StateID & "', " & _
                                        "LB_DISTRICT_ID       = '" & UpParam.LB_DisttID & "', " & _
                                        "LB_CITY_ID       = '" & UpParam.LB_CityID & "', " & _
                                        "LB_PINCODE       = '" & UpParam.LB_PinCode & "', " & _
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," & _
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " & _
                                        "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.LAND_BUILDING_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function
        Public Shared Function UpdateProperty_RentDetails(ByVal UpParam As Parameter_Update_Property_RentDetails, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "  UPDATE Land_Building_Info SET " &
                                         "LB_DEPOSIT_AMT        = " & UpParam.DepositAmount & ", " &
                                         "LB_PAID_DATE          = " & If(IsDate(UpParam.PaymentDate), "'" & Convert.ToDateTime(UpParam.PaymentDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                         "LB_MONTH_RENT         = " & UpParam.MonthlyRent & ", " &
                                         "LB_MONTH_O_PAYMENTS   = " & UpParam.MonthlyOtherExpenses & ", " &
                                         "LB_PERIOD_FROM        = " & If(IsDate(UpParam.PeriodFrom), "'" & Convert.ToDateTime(UpParam.PeriodFrom).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                         "LB_PERIOD_TO          = " & If(IsDate(UpParam.PeriodTo), "'" & Convert.ToDateTime(UpParam.PeriodTo).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & " , " &
                                        "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        "REC_EDIT_BY       ='" & inBasicParam.openUserID & "' " &
                                        "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.LAND_BUILDING_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function
        Public Shared Function InsertProperty_Txn(inParam As Param_Txn_InsertProperty_LandAndBuilding, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertLandAndBuilding Is Nothing Then
                If Not Insert(inParam.param_InsertLandAndBuilding, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            For Each Param As Parameter_InsertExtendedInfo_LandAndBuilding In inParam.InExtendedInfo
                If Not Param Is Nothing Then InsertExtendedInfo(Param, inBasicParam, RequestTime)
            Next
            For Each Param As Parameter_InsertDocInfo_LandAndBuilding In inParam.InDocInfo
                If Not Param Is Nothing Then InsertDocumentsInfo(Param, inBasicParam, RequestTime)
            Next
            If Not inParam.param_InsertAssetLoc Is Nothing Then
                If Not AssetLocations.Insert_AllSisterUIDs(inParam.param_InsertAssetLoc, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            'Commit here 
            ' txn.Complete()
            '  End Using
            Return True
        End Function

        Public Shared Function UpdateProperty_Txn(UpParam As Param_Txn_UpdateProperty_LandAndBuilding, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            Dim d1 As DataTable = DataFunctions.GetCommonParamsById(ConnectOneWS.Tables.LAND_BUILDING_INFO, UpParam.param_UpdateLandAndBuilding.RecID, inBasicParam)
            Dim CommonParam As ConnectOneWS.PrevRec_ParamsForReInsertion = New ConnectOneWS.PrevRec_ParamsForReInsertion
            CommonParam.LastAddOn = d1.Rows(0)("REC_ADD_ON")
            CommonParam.LastAddBy = d1.Rows(0)("REC_ADD_BY")
            CommonParam.LastStatus = d1.Rows(0)("REC_STATUS")
            CommonParam.LastStatusBy = d1.Rows(0)("REC_STATUS_BY")
            CommonParam.LastStatusOn = d1.Rows(0)("REC_STATUS_ON")
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not UpParam.param_UpdateLandAndBuilding Is Nothing Then
                If Not Update(UpParam.param_UpdateLandAndBuilding, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            'If Not UpParam.RecID_DeleteComplexBuilding Is Nothing Then'Test Result #23673
            '    dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, " CB_LB_ID   ='" & UpParam.RecID_DeleteComplexBuilding & "'", inBasicParam)
            'End If
            If Not UpParam.RecID_DeleteExtendedInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, " LB_REC_ID    ='" & UpParam.RecID_DeleteExtendedInfo & "'", inBasicParam)
            End If
            For Each Param As Parameter_InsertExtendedInfo_LandAndBuilding In UpParam.InExtendedInfo
                If Not Param Is Nothing Then InsertExtendedInfo(Param, inBasicParam, RequestTime, CommonParam)
            Next
            If Not UpParam.RecID_DeleteDocumentInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, " LB_REC_ID    ='" & UpParam.RecID_DeleteDocumentInfo & "'", inBasicParam)
            End If
            For Each Param As Parameter_InsertDocInfo_LandAndBuilding In UpParam.InDocInfo
                If Not Param Is Nothing Then InsertDocumentsInfo(Param, inBasicParam, RequestTime, CommonParam)
            Next
            'If Not UpParam.param_UpdateAssetLoc Is Nothing Then
            '    If Not AssetLocations.UpdateByReference(UpParam.param_UpdateAssetLoc, inBasicParam) Then Throw New Exception(Common_Lib.Messages.SomeError)
            'End If
            ' End Using
            'Commit here 
            '  txn.Complete()
            '   End Using
            Return True
        End Function

        Public Shared Function DeleteProperty_Txn(DelParam As Param_Txn_DeleteProperty_LandAndBuilding, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not DelParam.RecID_DeleteComplexBuilding Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.COMPLEX_BUILDING_INFO, " CB_LB_ID   ='" & DelParam.RecID_DeleteComplexBuilding & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteExtendedInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_EXTENDED_INFO, " LB_REC_ID    ='" & DelParam.RecID_DeleteExtendedInfo & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteDocumentInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.LAND_BUILDING_DOCUMENTS_INFO, " LB_REC_ID    ='" & DelParam.RecID_DeleteDocumentInfo & "'", inBasicParam)
            End If
            If Not DelParam.RecID_DeleteByLB Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.ASSET_LOCATION_INFO, " LB_REC_ID = '" & DelParam.RecID_DeleteByLB & "' ", inBasicParam)
            End If
            If Not DelParam.LBOrgRecID_DeletePropertyTypeChangeLog Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.PROPERTY_TYPE_CHANGE_LOG, " LB_ORG_REC_ID = '" & DelParam.LBOrgRecID_DeletePropertyTypeChangeLog & "' ", inBasicParam)
            End If
            If Not DelParam.RecID_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.LAND_BUILDING_INFO, DelParam.RecID_Delete, inBasicParam)
            End If

            '  End Using
            'Commit here 
            ' txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function Update_Insurance_Register(ByVal UpParam As Parameter_Update_Insurance_Register, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Try
                Dim SPName As String = "sp_Update_Insurance_Register"
                Dim params() As String = {"REC_ID", "TYPE", "CEN_ID", "YEAR_ID", "SPL_VALUE", "REMARKS", "APPLICABLE", "USER_ID"}
                Dim values() As Object = {UpParam.REC_ID, UpParam.TYPE, inBasicParam.openCenID, inBasicParam.openYearID, UpParam.SPL_VALUE, UpParam.REMARKS, UpParam.APPLICABLE, inBasicParam.openUserID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
                Dim lengths() As Integer = {36, 10, 5, 4, 19, 8000, 1, 255}
                dbService.UpdateBySP(ConnectOneWS.Tables.LAND_BUILDING_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
            Return True
        End Function
    End Class
#End Region

#Region "--Accounts--"
    <Serializable>
    Public Class Voucher_Property
#Region "Param Classes"
        <Serializable>
        Public Class Param_Voucher_Property_CheckDuplicateMainCenter
            Public tag As Integer
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Voucher_Property_CheckDuplicatePropertyName
            Public tag As Integer
            Public RecID As String
            Public PropertyName As String
            Public CenID As Integer
        End Class
#End Region

        ''' <summary>
        ''' Check Duplicate Property Names
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Property_CheckDuplicatePropertyName</remarks>
        Public Shared Function CheckDuplicatePropertyName(ByVal Param As Param_Voucher_Property_CheckDuplicatePropertyName, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Dim CentreID As Integer
            If Param.CenID = Nothing Then CentreID = inBasicParam.openCenID
            If Val(Param.tag) = 1 Then Query = "SELECT COUNT(REC_ID) FROM Land_Building_Info  WHERE REC_STATUS IN (0,1,2) AND LB_CEN_ID=" & CentreID & " AND UPPER(LB_PRO_NAME)  = '" & Param.PropertyName.ToUpper & "' and ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID))  AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ")  "
            If Val(Param.tag) = 2 Then Query = "SELECT COUNT(REC_ID) FROM Land_Building_Info  WHERE REC_STATUS IN (0,1,2) AND LB_CEN_ID=" & CentreID & " AND UPPER(LB_PRO_NAME)  = '" & Param.PropertyName.ToUpper & "' AND ( LB_COD_YEAR_ID =" & inBasicParam.openYearID.ToString & " OR LB_COD_YEAR_ID IN (SELECT COD_YEAR_ID FROM COD_INFO WHERE COD_AUDITED = 0  AND CEN_ID = LB_CEN_ID)) AND REC_ID <> '" & Param.RecID & "' AND (LB_COD_YEAR_ID NOT IN (SELECT YEAR_ID FROM SO_TRIAL_BALANCE WHERE CEN_ID = LB_CEN_ID) OR LB_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & ") "
            Return dbService.GetScalar(ConnectOneWS.Tables.LAND_BUILDING_INFO, Query, ConnectOneWS.Tables.LAND_BUILDING_INFO.ToString(), inBasicParam)
        End Function
    End Class
#End Region

End Namespace
