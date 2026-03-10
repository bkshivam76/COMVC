Imports System.Data
'Imports System.Transactions
Imports System
Imports Real.Vouchers

Namespace Real
#Region "Accounts"
    <Serializable>
    Public Class DonationRegister
#Region "Param Classes"
        <Serializable>
        Public Class Param_DonationRegister_GetList
            Public FromDate As DateTime
            Public ToDate As DateTime
            Public ID As String = Nothing
        End Class
        <Serializable>
        Public Class Param_DonationRegister_GetAddressDetail_Form
            Public IsDonationOpen As Boolean
            Public Tr_ID As String
            Public ABID As String
        End Class
        <Serializable>
        Public Class Param_DonationRegister_InsertReceiptRequest
            Public TransactionID As String
            Public openYearID As Integer
        End Class
        <Serializable>
        Public Class Parameter_InsertDonationAddress_DonationRegister
            Public TransactionID As String
            Public AB_ID As String
            Public Name As String
            Public PAN As String
            Public PassportNo As String
            Public Add1 As String
            Public Add2 As String
            Public Add3 As String
            Public Add4 As String
            Public CityID As String
            Public DistrictID As String
            Public StateID As String
            Public CountryID As String
            Public PinCode As String
            Public openYearID As Integer
        End Class
        <Serializable>
        Public Class Parameter_Request_Receipt
            Public param_InsertReceiptRequest As Param_DonationRegister_InsertReceiptRequest = Nothing
            Public TxnID_UpdateReceiptRequest As String = Nothing
            Public TxnID_DeleteAddressBook As String = Nothing
            Public InAddress() As Parameter_InsertDonationAddress_DonationRegister
        End Class
#End Region
        ''' <summary>
        ''' Get Addresses
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetAddresses</remarks>
        Public Shared Function GetAddresses(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT C_NAME,C_PAN_NO,C_PASSPORT_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,'' AS C_R_CITY_NAME,C_R_STATE_ID,'' AS C_R_STATE_NAME, C_R_DISTRICT_ID,'' AS C_R_DISTRICT_NAME,C_R_COUNTRY_ID,'' AS C_R_COUNTRY_NAME,C_R_PINCODE,  REC_ID AS C_ID FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & "  "
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Address Detail
        ''' </summary>
        ''' <param name="ABID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.Donation_GetAddressDetail</remarks>
        Public Shared Function GetAddressDetail(ByVal ABID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT C_NAME,C_PAN_NO,C_PASSPORT_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_R_STATE_ID,C_R_DISTRICT_ID,C_R_COUNTRY_ID,C_R_PINCODE, REC_ID FROM ADDRESS_BOOK WHERE  REC_STATUS IN (0,1,2) AND C_CEN_ID  = " & inBasicParam.openCenID.ToString & " AND REC_ID='" & ABID & "'"
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Office AddressDetail
        ''' </summary>
        ''' <param name="ABID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetOfficeAddressDetail</remarks>
        Public Shared Function GetOfficeAddressDetail(ByVal ABID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT C_O_ADD1,C_O_ADD2,C_O_ADD3,C_O_ADD4,C_O_CITY_ID,C_O_STATE_ID,C_O_COUNTRY_ID,C_O_DISTRICT_ID FROM ADDRESS_BOOK WHERE REC_ID ='" & ABID & "' ;"
            Return dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Foreign Donation Detail
        ''' </summary>
        ''' <param name="TxnID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetForeignDonationDetail</remarks>
        Public Shared Function GetForeignDonationDetail(ByVal TxnID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_CUR_ID,TR_FOREIGN_AMT,TR_INR_AMT FROM TRANSACTION_D_FOREIGN_INFO where TR_REC_ID ='" & TxnID & "' ;"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_FOREIGN_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_FOREIGN_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' GetAddressDetail_Form
        ''' </summary>
        ''' <param name="Param"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetAddressDetail_Form</remarks>
        Public Shared Function GetAddressDetail_Form(ByVal Param As Param_DonationRegister_GetAddressDetail_Form, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = ""
            Dim dTable As DataTable = Nothing
            If Param.IsDonationOpen Then
                Query = "SELECT C_NAME,C_PAN_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_R_STATE_ID," &
                    "C_R_COUNTRY_ID,C_R_DISTRICT_ID,C_O_ADD1,C_O_ADD2,C_O_ADD3,C_O_ADD4,C_O_CITY_ID,C_O_STATE_ID, " &
                    "C_O_COUNTRY_ID,C_O_DISTRICT_ID,C_R_PINCODE,coalesce(NULLIF(C_MOB_NO_1,''),C_MOB_NO_2) as MobileNo, " &
                    "coalesce(NULLIF(C_EMAIL_ID_1,''),C_EMAIL_ID_2) as EmailID" &
                    " FROM ADDRESS_BOOK WHERE REC_ID ='" & Param.ABID & "' ;"
                dTable = dbService.List(ConnectOneWS.Tables.ADDRESS_BOOK, Query, ConnectOneWS.Tables.ADDRESS_BOOK.ToString(), inBasicParam)
            Else
                Query = "SELECT DR_AB.C_NAME,DR_AB.C_PAN_NO,DR_AB.C_R_ADD1,DR_AB.C_R_ADD2,DR_AB.C_R_ADD3," &
                        "DR_AB.C_R_ADD4,DR_AB.C_R_CITY_ID,DR_AB.C_R_COUNTRY_ID, DR_AB.C_R_STATE_ID," &
                        "DR_AB.C_R_DISTRICT_ID,DR_AB.C_AB_ID,DR_AB.C_R_PINCODE, " &
                        "coalesce(NULLIF(AB.C_MOB_NO_1,''), AB.C_MOB_NO_2) as MobileNo, " &
                        "coalesce(NULLIF(AB.C_EMAIL_ID_1,''), AB.C_EMAIL_ID_2) as EmailID " &
                        " FROM DONATION_RECEIPT_ADDRESS_BOOK DR_AB " &
                        " INNER join ADDRESS_BOOK AB on AB.REC_ID = DR_AB.C_AB_ID" &
                        " WHERE C_TR_ID ='" & Param.Tr_ID & "';"

                dTable = dbService.List(ConnectOneWS.Tables.DONATION_RECEIPT_ADDRESS_BOOK, Query, ConnectOneWS.Tables.DONATION_RECEIPT_ADDRESS_BOOK.ToString(), inBasicParam)
            End If
            Return dTable
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
        ''' <remarks>RealServiceFunctions.Donation_GetList</remarks>
        Public Shared Function GetList(InParam As Param_DonationRegister_GetList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Donation_Register"
            Dim params() As String = {"@CENID", "@YEARID", "@UserID", "@FROM_DATE", "@TO_DATE", "@RecID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, InParam.FromDate, InParam.ToDate, InParam.ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255, 20, 20, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        ''' <summary>
        '''  Get Record Detail
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetRecDetail</remarks>
        Public Shared Function GetRecDetail(ByVal RecID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_CODE,TR_REF_BANK_ID,TR_AB_ID_1,CASE WHEN DSI.DS_STATUS_MISC_ID IS NULL THEN '3a99fadc-b336-480d-8116-fbd144bd7671' ELSE DSI.DS_STATUS_MISC_ID END AS DS_STATUS_MISC_ID,TR_CEN_ID,TR_COD_YEAR_ID ,TR_DATE,TR_AMOUNT,TR_MODE,TR_REF_NO,TR_REF_BRANCH,TI.REC_EDIT_ON, ITEM_NAME FROM TRANSACTION_INFO AS TI " &
                              "LEFT OUTER JOIN DONATION_STATUS_INFO AS DSI ON DSI.DS_TR_ID = TI.REC_ID AND DSI.REC_STATUS IN (0,1,2) " &
                              " LEFT JOIN item_info AS II ON TR_ITEM_ID = II.REC_ID " &
                              " WHERE TI.REC_STATUS IN (0,1,2) AND TI.REC_ID ='" & RecID & "' ;"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Donation Status
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetDonationStatus</remarks>
        Public Shared Function GetDonationStatus(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " Select DS_STATUS_REMARKS , DS_STATUS_MISC_ID,DS_TR_ID   From Donation_Status_Info  Where  REC_STATUS IN (0,1,2) AND DS_CEN_ID=" & inBasicParam.openCenID.ToString & "  "
            Return dbService.List(ConnectOneWS.Tables.DONATION_STATUS_INFO, Query, ConnectOneWS.Tables.DONATION_STATUS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Donation Purposes
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetDonationPurposes</remarks>
        Public Shared Function GetDonationPurposes(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select P.TR_REC_ID,P.TR_PURPOSE_MISC_ID,'' AS P_NAME" &
                              " FROM ( Transaction_D_Purpose_Info P Inner join  Transaction_Info as T  on P.TR_REC_ID  = T.REC_ID) " &
                              " Where   P.REC_STATUS IN (0,1,2) AND P.TR_CEN_ID=" & inBasicParam.openCenID.ToString & "  AND T.TR_CODE IN (5,6) ORDER BY P.TR_REC_ID,P.TR_PURPOSE_MISC_ID "
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, Query, ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Donation Prints
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetDonationPrints</remarks>
        Public Shared Function GetDonationPrints(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select P.PR_TYPE AS 'Print',P.PR_DATE as 'Print Date',P.PR_LOCATION AS Location,  R.DR_TR_ID  " &
                              " From (Transaction_Info AS T INNER JOIN Donation_Receipt_Info AS R ON T.REC_ID = R.DR_TR_ID) INNER JOIN Donation_Receipt_Print_Info AS P ON R.REC_ID = P.PR_DR_ID " &
                              " Where   T.REC_STATUS IN (0,1,2) AND T.TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND T.TR_CODE IN (5,6) AND T.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & "   "
            Return dbService.List(ConnectOneWS.Tables.DONATION_RECEIPT_PRINT_INFO, Query, ConnectOneWS.Tables.DONATION_RECEIPT_PRINT_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Donation Rejections
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetDonationRejections</remarks>
        Public Shared Function GetDonationRejections(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "SELECT DS_STATUS_REMARKS AS 'Reason Of Rejection', DS_STATUS_ON AS 'Rejected On', DS_TR_ID  FROM donation_status_info " &
                                    "WHERE DS_STATUS_MISC_ID ='3a99fadc-b336-480d-8116-fbd144bd7671' AND DS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND DS_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & " "
            Return dbService.List(ConnectOneWS.Tables.DONATION_STATUS_INFO, OnlineQuery, ConnectOneWS.Tables.DONATION_STATUS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Get Donation Dispatches, Shifted
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_GetDonationDispatches</remarks>
        Public Shared Function GetDonationDispatches(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select D.DD_MODE AS 'Mode',D.DD_DATE AS 'Date',D.DD_COMPANY AS Reference,D.DD_REF_NO AS 'Reference No',D.DD_OTHER_DETAIL as Remarks,CASE WHEN COALESCE(D.DD_REF_NO,'') LIKE '80G|%' THEN '<a href=""'+DD_LOCATION+'"" target=""_blank"">Open 80G Receipt (10BE)</a>  |  <a href=""'+DD_LOCATION+'"" download>Download 80G Receipt (10BE)</a>' ELSE D.DD_LOCATION END AS Location, COALESCE(R.DR_TR_ID , D.DD_TR_ID) AS DR_TR_ID  " &
                              " From (Transaction_Info AS T LEFT JOIN Donation_Receipt_Info AS R ON T.REC_ID = R.DR_TR_ID) LEFT JOIN Donation_Receipt_Dispatch_Info AS D ON R.REC_ID = D.DD_DR_ID OR T.REC_ID = D.DD_TR_ID " &
                              " Where   D.REC_STATUS IN (0,1,2) AND T.TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND T.TR_COD_YEAR_ID=" & inBasicParam.openYearID.ToString & "    "
            Return dbService.List(ConnectOneWS.Tables.DONATION_RECEIPT_DISPATCH_INFO, Query, ConnectOneWS.Tables.DONATION_RECEIPT_DISPATCH_INFO.ToString(), inBasicParam)
        End Function



        ''' <summary>
        ''' Insert Receipt Request
        ''' </summary>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_InsertReceiptRequest</remarks>
        Private Shared Function InsertReceiptRequest(ByVal Param As Param_DonationRegister_InsertReceiptRequest, inBasicParam As ConnectOneWS.Basic_Param, AddTime As DateTime) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            Dim OnlineQuery As String = "INSERT INTO Donation_Status_Info(DS_CEN_ID,DS_COD_YEAR_ID,DS_TR_ID,DS_STATUS_MISC_ID," &
                                    "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                    ") VALUES(" &
                                    "" & inBasicParam.openCenID.ToString & "," &
                                    "" & Param.openYearID.ToString & "," &
                                    "'" & Param.TransactionID & "'," &
                                    "'" & "25d5fee0-c284-4ead-81d1-5e103700cdea" & "', " &
                                    "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.DONATION_STATUS_INFO, OnlineQuery, inBasicParam, RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update Receipt Request
        ''' </summary>
        ''' <param name="TransID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_UpdateReceiptRequest</remarks>
        Private Shared Function UpdateReceiptRequest(ByVal TransID As String, inBasicParam As ConnectOneWS.Basic_Param, EditTime As DateTime) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Donation_Status_Info SET " &
                                   " DS_STATUS_MISC_ID  = '25d5fee0-c284-4ead-81d1-5e103700cdea'," &
                                   " REC_EDIT_ON     ='" & Common.DateTimePlaceHolder & "'," &
                                   " REC_EDIT_BY     ='" & inBasicParam.openUserID & "-WA" & "'  " &
                                   " WHERE DS_TR_ID    ='" & TransID & "'"
            dbService.Update(ConnectOneWS.Tables.DONATION_STATUS_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Donation Address
        ''' </summary>
        ''' <param name="InDnAdd"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.Donation_InsertDonationAddress</remarks>
        Private Shared Function InsertDonationAddress(ByVal InDnAdd As Parameter_InsertDonationAddress_DonationRegister, inBasicParam As ConnectOneWS.Basic_Param, InsertTime As DateTime) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim RecID As String = Guid.NewGuid.ToString
            If InDnAdd.CityID.Trim.Length = 0 Then InDnAdd.CityID = "NULL" Else InDnAdd.CityID = "'" & InDnAdd.CityID & "'"
            If InDnAdd.DistrictID.Trim.Length = 0 Then InDnAdd.DistrictID = "NULL" Else InDnAdd.DistrictID = "'" & InDnAdd.DistrictID & "'"
            If InDnAdd.StateID.Trim.Length = 0 Then InDnAdd.StateID = "NULL" Else InDnAdd.StateID = "'" & InDnAdd.StateID & "'"
            If InDnAdd.CountryID.Trim.Length = 0 Then InDnAdd.CountryID = "NULL" Else InDnAdd.CountryID = "'" & InDnAdd.CountryID & "'"
            Dim OnlineQuery As String = "INSERT INTO Donation_Receipt_Address_Book(C_CEN_ID,C_COD_YEAR_ID,C_TR_ID,C_AB_ID,C_NAME,C_PAN_NO,C_PASSPORT_NO,C_R_ADD1,C_R_ADD2,C_R_ADD3,C_R_ADD4,C_R_CITY_ID,C_R_DISTRICT_ID,C_R_STATE_ID,C_R_COUNTRY_ID,C_R_PINCODE," &
                                   "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                   ") VALUES(" &
                                   "" & inBasicParam.openCenID.ToString & "," &
                                   "" & InDnAdd.openYearID.ToString & "," &
                                   " '" & InDnAdd.TransactionID & "'," &
                                   "'" & InDnAdd.AB_ID & "', " &
                                   "'" & InDnAdd.Name & "', " &
                                   "'" & InDnAdd.PAN & "', " &
                                   "'" & InDnAdd.PassportNo & "', " &
                                   "'" & InDnAdd.Add1 & "', " &
                                   "'" & InDnAdd.Add2 & "', " &
                                   "'" & InDnAdd.Add3 & "', " &
                                   "'" & InDnAdd.Add4 & "', " &
                                   " " & InDnAdd.CityID & " , " &
                                   " " & InDnAdd.DistrictID & " , " &
                                   " " & InDnAdd.StateID & " , " &
                                   " " & InDnAdd.CountryID & " , " &
                                   "'" & InDnAdd.PinCode & "', " &
                                    "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & Common_Lib.Common.Record_Status._Completed & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & RecID & "'" & ")"
            dbService.Insert(ConnectOneWS.Tables.DONATION_RECEIPT_ADDRESS_BOOK, OnlineQuery, inBasicParam, RecID, Nothing, InsertTime)
            Return True
        End Function

        'Consolidated function for Reuesting a Receipt with Txn Scope implemented 
        Public Shared Function RequestReceipt(inParam As Parameter_Request_Receipt, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            '  Using txn
            'Using Common.GetConnectionScope()
            If Not inParam.param_InsertReceiptRequest Is Nothing Then
                If Not InsertReceiptRequest(inParam.param_InsertReceiptRequest, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.TxnID_UpdateReceiptRequest Is Nothing Then
                If Not UpdateReceiptRequest(inParam.TxnID_UpdateReceiptRequest, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inParam.TxnID_DeleteAddressBook Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.DONATION_RECEIPT_ADDRESS_BOOK, inParam.TxnID_UpdateReceiptRequest, inBasicParam)
            End If
            For Each cPAram As Parameter_InsertDonationAddress_DonationRegister In inParam.InAddress
                If Not cPAram Is Nothing Then InsertDonationAddress(cPAram, inBasicParam, RequestTime)
            Next
            ' End Using
            ' commit here
            '  txn.Complete()
            ' End Using
            Return True
        End Function

        Public Shared Function GetReceiptDetails(ByVal Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim Query As String = "SELECT  RI.REC_ID AS ReceiptID, tfi.TR_FOREIGN_AMT AS ForeignAmount, tfi.TR_CUR_RATE AS ConversionRate, " &
            "cri.CUR_CODE ,INS_PAN_NO AS InsPAN, REPLACE(REPLACE(INS_DONATION_FOR,'Donation',ITEM_NAME),'Contribution',ITEM_NAME) AS DONATIONFOR, " &
            "CASE WHEN (LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )='donation accepted' or LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'receipt request rejected') THEN  RTRIM(LTRIM(CASE WHEN LEN(COALESCE(DCI.CI_NAME,'')) < 1 THEN '' ELSE COALESCE(DCI.CI_NAME,'')  END + CASE WHEN LEN(COALESCE(DSI.ST_NAME,'')) < 1 THEN '' ELSE ','+DSI.ST_NAME  END    + COALESCE(DCOI.CO_NAME,''))) ELSE RTRIM(LTRIM(CASE WHEN LEN(COALESCE(DRCI.CI_NAME,'')) < 1 THEN '' ELSE COALESCE(DRCI.CI_NAME,'')  END + CASE WHEN LEN(COALESCE(DRSI.ST_NAME,'')) < 1 THEN '' ELSE ','+DRSI.ST_NAME  END + ',' + COALESCE(DRCOI.CO_NAME,'')  )) END AS DONORAREA," &
            "TR_REF_NO AS INSTRUMENT_INFO,TR_REF_DATE AS INSTRUMENT_DATE, ins_ExemptionFooter AS EXMPFOOTER,ins_ReceiptSignAuthorityText AS 'AUTHORITY', " &
            "CI.CEN_NAME + ' (' + CI.CEN_UID + ')' AS CENTER , " &
            "COALESCE(RI.DR_NO,'') AS RECEIPTNO,TI.TR_DATE AS VDATE, " &
            "CASE WHEN (LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'donation accepted' or LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'receipt request rejected') THEN COALESCE(AB.C_PAN_NO,'') ELSE COALESCE(DRAB.C_PAN_NO,'') END AS PANNO," &
            "TR_AMOUNT AS AMOUNT, " &
            "CASE WHEN (LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'donation accepted' or LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'donation accepted') THEN COALESCE(AB.c_name,'') ELSE COALESCE(DRAB.c_name,'') END AS DONOR," &
            "CASE WHEN (LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'donation accepted' or LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'donation accepted') THEN RTRIM(LTRIM(COALESCE(AB.C_R_ADD1,'')+ ' '+COALESCE(AB.C_R_ADD2,'')+ ' '+ COALESCE(AB.C_R_ADD3,'')+ ' '+ COALESCE(AB.C_R_ADD4,''))) ELSE RTRIM(LTRIM(COALESCE(DRAB.C_R_ADD1,'')+ ' '+COALESCE(DRAB.C_R_ADD2,'')+ ' '+ COALESCE(DRAB.C_R_ADD3,'')+ ' '+ COALESCE(DRAB.C_R_ADD4,''))) END AS Address," &
            "CASE WHEN (LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'donation accepted' or LOWER(CASE WHEN TS.Misc_name IS NULL THEN 'Receipt Request Rejected' ELSE TS.Misc_name END )= 'donation accepted') THEN COALESCE(AB.C_R_PINCODE,'') ELSE COALESCE(DRAB.C_R_PINCODE,'') END AS PIN," &
            "COALESCE(AB.C_MOB_NO_1,'') AS MOB," &
            "COALESCE(AB.C_EMAIL_ID_1,'') AS EMAIL," &
            "TR_MODE AS MODE, TR_REF_NO AS RefNo,INS_NAME AS institute,INS_ID AS InsID, INS_HEADER1 AS Header1, INS_HEADER2 AS Header2, INS_HEADER3 AS Header3, INS_HEADER4 AS Header4, " &
            "INS_DONATION_INFO AS DonationInfo, " &
            "INS_HO_ADD1 AS HOAdd1, INS_HO_ADD2 AS HOAdd2, INS_HO_ADD3 AS HOAdd3, " &
            "INS_HO_ADD4 AS HOAdd4, HOCI.CI_NAME AS HOCity, HODI.DI_NAME AS HODist, HOSI.ST_NAME AS HOState, HOCOI.CO_NAME AS HOCountry, INS_HO_PINCODE AS HOPin, " &
            "INS_HO_TEL_NO_1 AS HOTel1, INS_HO_TEL_NO_2 AS HOTel2, INS_HO_MOB_NO_1 AS HOMob1, INS_HO_MOB_NO_2 AS HOMob2,  INS_HO_FAX_NO_1 AS HOFax1, " &
            "INS_HO_FAX_NO_2 AS HOFax2, INS_HO_EMAIL_ID_1 AS HOEmail1, INS_HO_EMAIL_ID_2 AS HOEmail2, " &
            "Txn_foreign.TR_CUR_RATE AS Rate,Txn_foreign.TR_FOREIGN_AMT as 'Foreign Amt', 'as '+ item_name as ReceiptItem, COALESCE(CI.CEN_MOB_NO_1, MAIN.CEN_MOB_NO_1) AS CenTel1, COALESCE(CI.CEN_EMAIL_ID_1, MAIN.CEN_EMAIL_ID_1) AS CenEmail1 " &
            "FROM transaction_info AS TI  INNER JOIN centre_info AS CI  ON TI.TR_CEN_ID = CI.CEN_ID  " &
            "INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO = MAIN.CEN_BK_PAD_NO AND MAIN.CEN_MAIN =1 " &
            "LEFT OUTER JOIN transaction_d_foreign_info as Txn_foreign ON Txn_foreign.TR_REC_ID = TI.REC_ID " &
            "INNER JOIN item_info AS Item ON TI.TR_ITEM_ID = Item.Rec_id  " &
            "INNER JOIN institute_info AS II ON CI.CEN_INS_ID = II.INS_ID  " &
            "LEFT OUTER JOIN donation_receipt_info AS RI ON (RI.DR_TR_ID = TI.Rec_id AND RI.DR_IS_Active = 1) " &
            "LEFT OUTER JOIN map_city_info AS HOCI ON II.INS_HO_CITY_ID = HOCI.REC_ID  " &
            "LEFT OUTER JOIN map_district_info AS HODI ON II.INS_HO_DISTRICT_ID = HODI.REC_ID  " &
            "LEFT OUTER JOIN map_state_info AS HOSI ON II.INS_HO_STATE_ID = HOSI.REC_ID  " &
            "LEFT OUTER JOIN map_country_info AS HOCOI ON II.INS_HO_COUNTRY_ID = HOCOI.REC_ID  " &
            "LEFT OUTER JOIN address_book AS AB ON AB.rec_id = TI.tr_ab_id_1  " &
            "LEFT OUTER JOIN donation_receipt_address_book AS DRAB ON DRAB.C_AB_ID = TI.tr_ab_id_1 AND DRAB.C_TR_ID = TI.REC_ID  AND DRAB.REC_STATUS IN (0,1,2) " &
            "LEFT OUTER JOIN map_city_info AS DCI ON AB.C_R_CITY_ID = DCI.REC_ID  " &
            "LEFT OUTER JOIN map_state_info AS DSI ON AB.C_R_STATE_ID = DSI.REC_ID " &
            "LEFT OUTER JOIN map_country_info AS DCOI ON AB.C_R_COUNTRY_ID = DCOI.REC_ID " &
            "LEFT OUTER JOIN map_city_info AS DRCI ON DRAB.C_R_CITY_ID = DRCI.REC_ID  " &
            "LEFT OUTER JOIN map_state_info AS DRSI ON DRAB.C_R_STATE_ID = DRSI.REC_ID " &
            "LEFT OUTER JOIN map_country_info AS DRCOI ON DRAB.C_R_COUNTRY_ID = DRCOI.REC_ID " &
            "LEFT OUTER JOIN Transaction_D_Foreign_Info AS TFI ON TFI.TR_REC_ID = ti.REC_ID AND ti.TR_CODE=6 " &
            "LEFT OUTER JOIN currency_info AS CRI ON cri.REC_ID = tfi.TR_CUR_ID " &
            "LEFT OUTER JOIN Donation_Status_Info AS DS ON (TI.Rec_Id = DS.DS_TR_ID and DS.REC_STATUS IN (0,1,2)) " &
            "LEFT OUTER JOIN misc_info AS TS ON ds.DS_STATUS_MISC_ID = TS.rec_id " &
            "Where COALESCE(TI.REC_STATUS,0) IN(0,1,2) AND COALESCE(CI.REC_STATUS,0) IN(0,1,2) AND COALESCE(Txn_foreign.REC_STATUS,0) IN(0,1,2) " &
            "AND COALESCE(II.REC_STATUS,0) IN(0,1,2) AND COALESCE(RI.REC_STATUS,0) IN(0,1,2) " &
            "AND COALESCE(HOCI.REC_STATUS,0) IN(0,1,2) AND COALESCE(HODI.REC_STATUS,0) IN(0,1,2) AND COALESCE(HOSI.REC_STATUS,0) IN(0,1,2) AND COALESCE(HOCOI.REC_STATUS,0) IN(0,1,2)  " &
            "AND COALESCE(AB.REC_STATUS,0) IN(0,1,2) AND COALESCE(DRAB.REC_STATUS,0) IN(0,1,2) " &
            "AND COALESCE(DCI.REC_STATUS,0) IN(0,1,2) AND COALESCE(DSI.REC_STATUS,0) IN(0,1,2) AND COALESCE(DCOI.REC_STATUS,0) IN(0,1,2) " &
            "AND COALESCE(DRCI.REC_STATUS,0) IN(0,1,2) AND COALESCE(DRSI.REC_STATUS,0) IN(0,1,2) AND COALESCE(DRCOI.REC_STATUS,0) IN(0,1,2) " &
            "AND COALESCE(TFI.REC_STATUS,0) IN(0,1,2) AND COALESCE(CRI.REC_STATUS,0) IN(0,1,2) AND COALESCE(DS.REC_STATUS,0) IN(0,1,2) " &
            "AND COALESCE(TS.REC_STATUS,0) IN(0,1,2)   AND TI.Rec_Id = '" & Rec_ID & "'"
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Return dbService.List(ConnectOneWS.Tables.DONATION_STATUS_INFO, Query, ConnectOneWS.Tables.DONATION_STATUS_INFO.ToString(), inBasicParam)
        End Function

    End Class

    <Serializable>
    Public Class Voucher_Donation
#Region "Param Classes"
        <Serializable>
        Public Class Parameter_Insert_Voucher_Donation
            Public TransCode As Integer
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Sub_Dr_Led_ID As String
            Public Mode As String
            Public RefBankID As String
            Public RefBranch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_ChequeDate As String
            Public Amount As Decimal
            Public DonorID As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String  ' Removed and used from basicParam instead
            'Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Parameter_InsertForeignInfo_Voucher_Donation
            Public TxnID As String
            Public CoBank As String
            Public CoBranch As String
            Public CurrID As String
            Public CurrRate As Decimal
            Public ForeignAmount As Decimal
            Public INR As Decimal
            Public Bankcharges As Decimal
            Public NettAmt As Decimal
            Public CatID As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead
        End Class
        <Serializable>
        Public Class Parameter_InsertPurpose_Voucher_Donation
            Public TxnID As String
            Public PurposeID As String
            Public Amount As Decimal
            Public Status_Action As String
            Public RecID As String
            ' Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_InsertDonStatus_Voucher_Donation
            Public TxnID As String
            Public StatusID As String
            Public Status_Action As String
            Public RecID As String
            'Public openYearID As String ' Removed and used from basicParam instead 
        End Class
        <Serializable>
        Public Class Parameter_Update_Voucher_Donation
            Public VNo As String
            Public TDate As String
            Public ItemID As String
            Public Type As String
            Public Cr_Led_ID As String
            Public Dr_Led_ID As String
            Public Sub_Cr_Led_ID As String
            Public Sub_Dr_Led_ID As String
            Public Mode As String
            Public RefBankID As String
            Public RefBranch As String
            Public Ref_No As String
            Public Ref_Date As String
            Public Ref_ChequeDate As String
            Public Amount As Decimal
            Public DonorID As String
            Public Narration As String
            Public Remarks As String
            Public Reference As String
            'Public Status_Action As String
            Public RecID As String
            'Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Parameter_UpdatePurpose_Voucher_Donation
            Public PurposeID As String
            Public Amount As Decimal
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Parameter_UpdateForeignInfo_Voucher_Donation
            Public TxnID As String
            Public CoBank As String
            Public CoBranch As String
            Public CurrID As String
            Public CurrRate As Decimal
            Public ForeignAmount As Decimal
            Public INR As Decimal
            Public Bankcharges As Decimal
            Public NettAmt As Decimal
            Public CatID As String
            'Public Status_Action As String
        End Class
        <Serializable>
        Public Class Parameter_UpdateStatus_Voucher_Donation
            Public StatusID As String
            'Public Status_Action As String
            Public RecID As String
        End Class
        <Serializable>
        Public Class Param_Txn_Insert_VoucherDonation
            Public param_Insert As Parameter_Insert_Voucher_Donation
            Public param_InsertPurpose As Parameter_InsertPurpose_Voucher_Donation = Nothing
            Public param_InsertDonStatus As Parameter_InsertDonStatus_Voucher_Donation = Nothing
            Public param_InsertFgnInfo As Parameter_InsertForeignInfo_Voucher_Donation = Nothing
            Public param_InsertSlip As Parameter_InsertSlip_VoucherDonation = Nothing
            Public InsertSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Update_VoucherDonation
            Public ID_DeleteSlip As String = Nothing
            Public param_Update As Parameter_Update_Voucher_Donation
            Public param_UpdatePurpose As Parameter_UpdatePurpose_Voucher_Donation = Nothing
            Public param_InsertDonSattus As Parameter_InsertDonStatus_Voucher_Donation = Nothing
            Public param_UpdateStatus As Parameter_UpdateStatus_Voucher_Donation = Nothing
            Public param_UpdateFgnInfo As Parameter_UpdateForeignInfo_Voucher_Donation
            Public param_InsertSlip As Parameter_InsertSlip_VoucherDonation = Nothing
            Public UpdateSplVchrRefs() As Parameter_InsertSplVchrRef_Vouchers
        End Class
        <Serializable>
        Public Class Param_Txn_Delete_VoucherDonation
            Public RecID_DeletePurpose As String = Nothing
            Public RecID_DeleteStatus As String = Nothing
            Public RecID_Delete As String = Nothing
            Public RecID_DeleteFgnInfo As String = Nothing
            Public ID_DeleteSlip As String = Nothing
        End Class
        <Serializable>
        Public Class Parameter_InsertSlip_VoucherDonation
            Public TxnID As String
            Public SlipNo As Integer
            Public RecID As String
            Public ID_DeleteSlip As String = Nothing
            Public Dep_BA_ID As String
        End Class
#End Region

        ''' <summary>
        ''' GetOldStatusID
        ''' </summary>
        ''' <param name="TxnID"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_GetOldStatusID</remarks>
        Public Shared Function GetOldStatusID(ByVal TxnID As String, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT REC_ID FROM Donation_Status_Info  WHERE REC_STATUS IN (0,1,2) AND DS_CEN_ID=" & inBasicParam.openCenID.ToString & " AND DS_TR_ID  = '" & TxnID & "' AND REC_STATUS IN (0,1,2)" 'Saurabh : to ensure that rejection status entry is not updated 
            Return dbService.GetScalar(ConnectOneWS.Tables.DONATION_STATUS_INFO, Query, ConnectOneWS.Tables.DONATION_STATUS_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Transaction dates of Donation (Kind/Cash) Vouchers where Referred party has been used in past 
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CheckUsageAsPastDonor(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_DATE FROM TRANSACTION_INFO TI INNER JOIN ITEM_INFO AS II ON TI.TR_ITEM_ID = II.REC_ID WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND (TR_CODE in (5,7) OR (TR_CODE = 14 AND ITEM_VOUCHER_TYPE ='Donation - Gift')) AND (TR_AB_ID_1 = '" & Rec_ID & "' OR TR_AB_ID_2 = '" & Rec_ID & "')"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Returns Transaction dates of Donation (Kind/Cash) Vouchers where Referred party has been used in past 
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function CheckUsageAsPastForeignDonor(Rec_ID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT TR_DATE FROM TRANSACTION_INFO TI INNER JOIN ITEM_INFO AS II ON TI.TR_ITEM_ID = II.REC_ID WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID=" & inBasicParam.openCenID.ToString & " AND (TR_CODE in (6)) AND (TR_AB_ID_1 = '" & Rec_ID & "' OR TR_AB_ID_2 = '" & Rec_ID & "')"
            Return dbService.List(ConnectOneWS.Tables.TRANSACTION_INFO, Query, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), inBasicParam)
        End Function

        ''' <summary>
        ''' Insert
        ''' </summary>
        ''' <param name="InParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_Insert</remarks>
        Public Shared Function Insert(ByVal InParam As Parameter_Insert_Voucher_Donation, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InParam.Sub_Cr_Led_ID.Trim.Length = 0 Then InParam.Sub_Cr_Led_ID = "NULL" Else InParam.Sub_Cr_Led_ID = "'" & InParam.Sub_Cr_Led_ID & "'"
            If InParam.Sub_Dr_Led_ID.Trim.Length = 0 Then InParam.Sub_Dr_Led_ID = "NULL" Else InParam.Sub_Dr_Led_ID = "'" & InParam.Sub_Dr_Led_ID & "'"
            If InParam.RefBankID.Trim.Length = 0 Then InParam.RefBankID = "NULL" Else InParam.RefBankID = "'" & InParam.RefBankID & "'"
            Dim OnlineQuery As String = "INSERT INTO TRANSACTION_INFO(TR_CEN_ID,TR_COD_YEAR_ID,TR_CODE,TR_VNO,TR_DATE,TR_ITEM_ID,TR_TYPE,TR_CR_LED_ID,TR_DR_LED_ID,TR_SUB_CR_LED_ID,TR_SUB_DR_LED_ID,TR_MODE,TR_REF_BANK_ID,TR_REF_BRANCH,TR_REF_NO,TR_REF_DATE,TR_REF_CDATE,TR_AMOUNT,TR_AB_ID_1,TR_NARRATION,TR_REMARKS,TR_REFERENCE," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  " " & InParam.TransCode & "," &
                                                      "'" & InParam.VNo & "', " &
                                                      "" & If(IsDate(InParam.TDate), "'" & Convert.ToDateTime(InParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                      "'" & InParam.ItemID & "', " &
                                                      "'" & InParam.Type & "', " &
                                                      "'" & InParam.Cr_Led_ID & "', " &
                                                      "'" & InParam.Dr_Led_ID & "', " &
                                                      "" & InParam.Sub_Cr_Led_ID & ", " &
                                                      "" & InParam.Sub_Dr_Led_ID & ", " &
                                                      "'" & InParam.Mode & "', " &
                                                      " " & InParam.RefBankID & " , " &
                                                      "'" & InParam.RefBranch & "', " &
                                                      "'" & InParam.Ref_No & "', " &
                                                      " " & If(IsDate(InParam.Ref_Date), "'" & Convert.ToDateTime(InParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                      " " & If(IsDate(InParam.Ref_ChequeDate), "'" & Convert.ToDateTime(InParam.Ref_ChequeDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                      " " & InParam.Amount & ", " &
                                                      "'" & InParam.DonorID & "', " &
                                                      "'" & InParam.Narration & "', " &
                                                      "'" & InParam.Remarks & "', " &
                                                      "'" & InParam.Reference & "', " &
                                            "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InParam.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InParam.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, InParam.RecID, InParam.TDate, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Foreign Info
        ''' </summary>
        ''' <param name="InFgnInfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks> RealServiceFunctions.VoucherDonation_InsertForeignInfo</remarks>
        Public Shared Function InsertForeignInfo(ByVal InFgnInfo As Parameter_InsertForeignInfo_Voucher_Donation, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If InFgnInfo.CurrID.Trim.Length = 0 Then InFgnInfo.CurrID = "NULL" Else InFgnInfo.CurrID = "'" & InFgnInfo.CurrID & "'"
            If InFgnInfo.CatID.Trim.Length = 0 Then InFgnInfo.CatID = "NULL" Else InFgnInfo.CatID = "'" & InFgnInfo.CatID & "'"
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Foreign_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_CO_BANK,TR_CO_BRANCH,TR_CUR_ID,TR_CUR_RATE,TR_FOREIGN_AMT,TR_INR_AMT,TR_BANK_CHARGES,TR_NET_AMT,TR_D_CAT_MISC_ID," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InFgnInfo.TxnID & "'," &
                                                  "'" & InFgnInfo.CoBank & "', " &
                                                  "'" & InFgnInfo.CoBranch & "', " &
                                                  " " & InFgnInfo.CurrID & " , " &
                                                  " " & InFgnInfo.CurrRate & ", " &
                                                  " " & InFgnInfo.ForeignAmount & ", " &
                                                  " " & InFgnInfo.INR & ", " &
                                                  " " & InFgnInfo.Bankcharges & ", " &
                                                  " " & InFgnInfo.NettAmt & ", " &
                                                  " " & InFgnInfo.CatID & " , " &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InFgnInfo.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InFgnInfo.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_FOREIGN_INFO, OnlineQuery, inBasicParam, InFgnInfo.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Purpose
        ''' </summary>
        ''' <param name="InPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_InsertPurpose</remarks>
        Public Shared Function InsertPurpose(ByVal InPurpose As Parameter_InsertPurpose_Voucher_Donation, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO Transaction_D_Purpose_Info(TR_CEN_ID,TR_COD_YEAR_ID,TR_REC_ID,TR_PURPOSE_MISC_ID,TR_AMOUNT," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InPurpose.TxnID & "'," &
                                                  "'" & InPurpose.PurposeID & "', " &
                                                  " " & InPurpose.Amount & ", " &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InPurpose.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InPurpose.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, InPurpose.RecID, Nothing, AddTime)
            Return True
        End Function

        Public Shared Function InsertSlip(ByVal InSlip As Parameter_InsertSlip_VoucherDonation, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing, Optional Param As ConnectOneWS.PrevRec_ParamsForReInsertion = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim Query As String = "SELECT REC_ID FROM SLIP_INFO WHERE SL_CEN_ID = " & inBasicParam.openCenID.ToString & " AND SL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND SL_NO = " & InSlip.SlipNo & " AND SL_BA_REC_ID = '" & InSlip.Dep_BA_ID & "'"
            Dim Slip_ID As Object = dbService.GetScalar(ConnectOneWS.Tables.SLIP_INFO, Query, "SLIP_INFO", inBasicParam)
            If Slip_ID Is Nothing Then
                Dim rec_ID As String = Guid.NewGuid.ToString
                Query = "INSERT INTO [dbo].[slip_info] ([SL_CEN_ID],[SL_COD_YEAR_ID],[SL_PRINT_DATE],[SL_NO],[REC_ID],[SL_BA_REC_ID],[REC_ADD_ON],[REC_ADD_BY],[REC_STATUS]" &
                                                 ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  " NULL," &
                                                  " " & InSlip.SlipNo & "," &
                                                  "'" & rec_ID & "'," &
                                                  " '" & InSlip.Dep_BA_ID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',1 " & ")"
                dbService.Insert(ConnectOneWS.Tables.SLIP_INFO, Query, inBasicParam, Nothing)

                Query = "SELECT REC_ID FROM SLIP_INFO WHERE SL_CEN_ID = " & inBasicParam.openCenID.ToString & " AND SL_COD_YEAR_ID = " & inBasicParam.openYearID.ToString & " AND SL_NO = " & InSlip.SlipNo & " AND SL_BA_REC_ID = '" & InSlip.Dep_BA_ID & "'"
                Slip_ID = dbService.GetScalar(ConnectOneWS.Tables.SLIP_INFO, Query, "SLIP_INFO", inBasicParam)
            End If

            Dim OnlineQuery As String = "INSERT INTO [dbo].[TRANSACTION_D_SLIP_INFO]([TR_CEN_ID],[TR_COD_YEAR_ID],[TR_REC_ID],[TR_SR_NO],[TR_SLIP_ID],[REC_ID],[REC_ADD_ON],[REC_ADD_BY],[REC_STATUS]" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InSlip.TxnID & "',0," &
                                                  "'" & Slip_ID & "', " &
                                                  " '" & InSlip.RecID & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "',1 " & ")"

            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_SLIP_INFO, OnlineQuery, inBasicParam, InSlip.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Insert Donation Status
        ''' </summary>
        ''' <param name="InDnStatus"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_InsertDonationStatus</remarks>
        Public Shared Function InsertDonationStatus(ByVal InDnStatus As Parameter_InsertDonStatus_Voucher_Donation, inBasicParam As ConnectOneWS.Basic_Param, Optional AddTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = "INSERT INTO Donation_Status_Info(DS_CEN_ID,DS_COD_YEAR_ID,DS_TR_ID,DS_STATUS_MISC_ID," &
                                                  "REC_ADD_ON,REC_ADD_BY,REC_EDIT_ON,REC_EDIT_BY,REC_STATUS,REC_STATUS_ON,REC_STATUS_BY,REC_ID" &
                                                  ") VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  "'" & InDnStatus.TxnID & "'," &
                                                  "'" & InDnStatus.StatusID & "', " &
                                        "'" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "-WA" & "', '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', " & InDnStatus.Status_Action & ", '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "', '" & InDnStatus.RecID & "'" & ")"

            dbService.Insert(ConnectOneWS.Tables.DONATION_STATUS_INFO, OnlineQuery, inBasicParam, InDnStatus.RecID, Nothing, AddTime)
            Return True
        End Function

        ''' <summary>
        ''' Update
        ''' </summary>
        ''' <param name="UpParam"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_Update</remarks>
        Public Shared Function Update(ByVal UpParam As Parameter_Update_Voucher_Donation, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpParam.Sub_Cr_Led_ID.Trim.Length = 0 Then UpParam.Sub_Cr_Led_ID = "NULL" Else UpParam.Sub_Cr_Led_ID = "'" & UpParam.Sub_Cr_Led_ID & "'"
            If UpParam.Sub_Dr_Led_ID.Trim.Length = 0 Then UpParam.Sub_Dr_Led_ID = "NULL" Else UpParam.Sub_Dr_Led_ID = "'" & UpParam.Sub_Dr_Led_ID & "'"
            If UpParam.RefBankID.Trim.Length = 0 Then UpParam.RefBankID = "NULL" Else UpParam.RefBankID = "'" & UpParam.RefBankID & "'"
            Dim OnlineQuery As String = " UPDATE TRANSACTION_INFO SET " &
                                            " TR_VNO         ='" & UpParam.VNo & "', " &
                                                " TR_DATE        =" & If(IsDate(UpParam.TDate), "'" & Convert.ToDateTime(UpParam.TDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                " TR_ITEM_ID     ='" & UpParam.ItemID & "', " &
                                                " TR_TYPE        ='" & UpParam.Type & "', " &
                                                " TR_CR_LED_ID   ='" & UpParam.Cr_Led_ID & "', " &
                                                " TR_DR_LED_ID   ='" & UpParam.Dr_Led_ID & "', " &
                                                " TR_SUB_CR_LED_ID  =" & UpParam.Sub_Cr_Led_ID & ", " &
                                                " TR_SUB_DR_LED_ID  =" & UpParam.Sub_Dr_Led_ID & ", " &
                                                " TR_MODE        ='" & UpParam.Mode & "', " &
                                                " TR_REF_BANK_ID = " & UpParam.RefBankID & " , " &
                                                " TR_REF_BRANCH  ='" & UpParam.RefBranch & "', " &
                                                " TR_REF_NO      ='" & UpParam.Ref_No & "', " &
                                                " TR_REF_DATE    = " & If(IsDate(UpParam.Ref_Date), "'" & Convert.ToDateTime(UpParam.Ref_Date).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                " TR_REF_CDATE   = " & If(IsDate(UpParam.Ref_ChequeDate), "'" & Convert.ToDateTime(UpParam.Ref_ChequeDate).ToString(Common.Server_Date_Format_Long) & "'", " NULL ") & ", " &
                                                " TR_AMOUNT      = " & UpParam.Amount & ", " &
                                                " TR_AB_ID_1     ='" & UpParam.DonorID & "', " &
                                                " TR_NARRATION   ='" & UpParam.Narration & "', " &
                                                " TR_REMARKS     ='" & UpParam.Remarks & "', " &
                                                " TR_REFERENCE   ='" & UpParam.Reference & "', " &
                                                "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                                "REC_EDIT_BY       ='" & inBasicParam.openUserID & "-WA" & "'  " &
                                                "  WHERE REC_ID    ='" & UpParam.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_INFO, OnlineQuery, inBasicParam, EditTime, UpParam.TDate)
            Return True
        End Function

        ''' <summary>
        ''' Update Purpose
        ''' </summary>
        ''' <param name="UpPurpose"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_UpdatePurpose</remarks>
        Public Shared Function UpdatePurpose(ByVal UpPurpose As Parameter_UpdatePurpose_Voucher_Donation, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Transaction_D_Purpose_Info SET " &
                                         " TR_PURPOSE_MISC_ID    ='" & UpPurpose.PurposeID & "', " &
                                         " TR_AMOUNT             =" & UpPurpose.Amount & ", " &
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "-WA" & "' " &
                                            "  WHERE TR_REC_ID    ='" & UpPurpose.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        ''' <summary>
        ''' Update ForeignInfo
        ''' </summary>
        ''' <param name="UpFgnInfo"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_UpdateForeignInfo</remarks>
        Public Shared Function UpdateForeignInfo(ByVal UpFgnInfo As Parameter_UpdateForeignInfo_Voucher_Donation, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            If UpFgnInfo.CurrID.Trim.Length = 0 Then UpFgnInfo.CurrID = "NULL" Else UpFgnInfo.CurrID = "'" & UpFgnInfo.CurrID & "'"
            If UpFgnInfo.CatID.Trim.Length = 0 Then UpFgnInfo.CatID = "NULL" Else UpFgnInfo.CatID = "'" & UpFgnInfo.CatID & "'"
            Dim OnlineQuery As String = " UPDATE Transaction_D_Foreign_Info SET " &
                                         " TR_CUR_ID        = " & UpFgnInfo.CurrID & " , " &
                                         " TR_CUR_RATE      = " & UpFgnInfo.CurrRate & ", " &
                                         " TR_CO_BANK       ='" & UpFgnInfo.CoBank & "', " &
                                         " TR_CO_BRANCH     ='" & UpFgnInfo.CoBranch & "', " &
                                         " TR_FOREIGN_AMT   = " & UpFgnInfo.ForeignAmount & ", " &
                                         " TR_INR_AMT       = " & UpFgnInfo.INR & ", " &
                                         " TR_BANK_CHARGES  = " & UpFgnInfo.Bankcharges & ", " &
                                         " TR_NET_AMT       = " & UpFgnInfo.NettAmt & ", " &
                                         " TR_D_CAT_MISC_ID = " & UpFgnInfo.CatID & " , " &
                                        " REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                        " REC_EDIT_BY       ='" & inBasicParam.openUserID & "-WA" & "' " &
                                        " WHERE TR_REC_ID    ='" & UpFgnInfo.TxnID & "'"

            dbService.Update(ConnectOneWS.Tables.TRANSACTION_D_FOREIGN_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        ''' <summary>
        ''' Update Status
        ''' </summary>
        ''' <param name="UpStatus"></param>
        ''' <param name="Screen"></param>
        ''' <param name="openUserID"></param>
        ''' <param name="openCenID"></param>
        ''' <param name="PCID"></param>
        ''' <param name="version"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.VoucherDonation_UpdateStatus</remarks>
        Public Shared Function UpdateStatus(ByVal UpStatus As Parameter_UpdateStatus_Voucher_Donation, inBasicParam As ConnectOneWS.Basic_Param, Optional EditTime As DateTime = Nothing) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim OnlineQuery As String = " UPDATE Donation_Status_Info SET " &
                                        " DS_STATUS_MISC_ID    ='" & UpStatus.StatusID & "', " &
                                            "REC_EDIT_ON       ='" & Common.DateTimePlaceHolder & "'," &
                                            "REC_EDIT_BY       ='" & inBasicParam.openUserID & "-WA" & "' " &
                                            "  WHERE REC_ID    ='" & UpStatus.RecID & "'"

            dbService.Update(ConnectOneWS.Tables.DONATION_STATUS_INFO, OnlineQuery, inBasicParam, EditTime)
            Return True
        End Function

        Public Shared Function InsertDonation_Txn(inparam As Param_Txn_Insert_VoucherDonation, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now

            'Check Special Partial Restriction
            Dim Res As Boolean

            If Not inparam.param_Insert Is Nothing Then
                If Not inparam.param_Insert.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inparam.param_Insert.TDate), inparam.param_Insert.Dr_Led_ID)
                If Not inparam.param_Insert.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inparam.param_Insert.TDate), inparam.param_Insert.Cr_Led_ID)
                If Not inparam.param_Insert.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inparam.param_Insert.TDate), inparam.param_Insert.Sub_Cr_Led_ID)
                If Not inparam.param_Insert.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inparam.param_Insert.TDate), inparam.param_Insert.Sub_Dr_Led_ID)
                If Not inparam.param_Insert.DonorID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(inparam.param_Insert.TDate), inparam.param_Insert.DonorID)
            End If


            ' Using txn
            'Using Common.GetConnectionScope()
            If Not inparam.param_Insert Is Nothing Then
                If Not Insert(inparam.param_Insert, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inparam.param_InsertPurpose Is Nothing Then
                If Not InsertPurpose(inparam.param_InsertPurpose, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inparam.param_InsertFgnInfo Is Nothing Then
                If Not InsertForeignInfo(inparam.param_InsertFgnInfo, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inparam.param_InsertDonStatus Is Nothing Then
                If Not InsertDonationStatus(inparam.param_InsertDonStatus, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not inparam.param_InsertSlip Is Nothing Then
                If Not InsertSlip(inparam.param_InsertSlip, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            ' txn.Complete()
            'End Using

            'Special Voucher References (FCRA)
            InsertSpecialVoucherReference(inparam.InsertSplVchrRefs, Nothing, inparam.param_Insert.RecID, Nothing, inBasicParam)
            'If inparam.InsertSplVchrRefs IsNot Nothing Then
            '    If inparam.InsertSplVchrRefs.Count > 0 Then
            '        For Each SVRParam As Parameter_InsertSplVchrRef_Vouchers In inparam.InsertSplVchrRefs
            '            If Not SVRParam Is Nothing Then InsertReference(SVRParam, inparam.param_InsertPurpose.TxnID, inparam.param_InsertPurpose.Amount, inBasicParam)
            '        Next
            '    End If
            'End If
            Return True
        End Function
        Public Shared Function InsertReference(Param As Parameter_InsertSplVchrRef_Vouchers, ByVal TxnID As String, Amount As Decimal, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Str As String = " "
            Str = " '" & Common.DateTimePlaceHolder & "', '" & inBasicParam.openUserID & "'"
            Dim ID As String = Guid.NewGuid.ToString()
            Dim OnlineQuery As String = "INSERT INTO transaction_d_reference_info(TR_CEN_ID, TR_COD_YEAR_ID, TR_M_ID, TXN_REC_ID, TR_SR_NO, " &
                                                  "TR_VOUCHER_REF, TR_AMOUNT, REC_ID, REC_ADD_ON, REC_ADD_BY)" &
                                                  " VALUES(" &
                                                  "" & inBasicParam.openCenID.ToString & "," &
                                                  "" & inBasicParam.openYearID.ToString & "," &
                                                  " NULL, " &
                                                  "'" & TxnID & "'," &
                                                  " NULL, " &
                                                  "'" & Param.Task_Name & "', " &
                                                  " " & Amount.ToString() & ", '" & ID & "', " & Str & ")"
            'MsgBox(OnlineQuery)
            'Console.WriteLine(OnlineQuery)
            dbService.Insert(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, OnlineQuery, inBasicParam, ID, )
            Return True
        End Function
        Public Shared Function UpdateDonation_Txn(upParam As Param_Txn_Update_VoucherDonation, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Check Special Partial Restriction
            Dim Res As Boolean
            If Not upParam.param_Update Is Nothing Then
                If Not upParam.param_Update.Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Update.TDate), upParam.param_Update.Dr_Led_ID)
                If Not upParam.param_Update.Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Update.TDate), upParam.param_Update.Cr_Led_ID)
                If Not upParam.param_Update.Sub_Cr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Update.TDate), upParam.param_Update.Sub_Cr_Led_ID)
                If Not upParam.param_Update.Sub_Dr_Led_ID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Update.TDate), upParam.param_Update.Sub_Dr_Led_ID)
                If Not upParam.param_Update.DonorID Is Nothing Then Res = dbService.IsPeriodPartiallyRestricted(inBasicParam.openCenID, Convert.ToDateTime(upParam.param_Update.TDate), upParam.param_Update.DonorID)
            End If

            '  Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope()
            If Not upParam.ID_DeleteSlip Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_SLIP_INFO, "TR_REC_ID = '" & upParam.ID_DeleteSlip & "'", inBasicParam)
            End If
            If Not upParam.param_Update Is Nothing Then
                If Not Update(upParam.param_Update, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdatePurpose Is Nothing Then
                If Not UpdatePurpose(upParam.param_UpdatePurpose, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdateFgnInfo Is Nothing Then
                If Not UpdateForeignInfo(upParam.param_UpdateFgnInfo, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertDonSattus Is Nothing Then
                If Not InsertDonationStatus(upParam.param_InsertDonSattus, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_UpdateStatus Is Nothing Then
                If Not UpdateStatus(upParam.param_UpdateStatus, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            If Not upParam.param_InsertSlip Is Nothing Then
                If Not InsertSlip(upParam.param_InsertSlip, inBasicParam, RequestTime) Then Throw New Exception(Common_Lib.Messages.SomeError)
            End If
            ' End Using
            ' txn.Complete()
            ' End Using

            'Special Voucher References (FCRA)
            dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_REFERENCE_INFO, "TXN_REC_ID = '" & upParam.param_UpdatePurpose.RecID & "'", inBasicParam)
            InsertSpecialVoucherReference(upParam.UpdateSplVchrRefs, Nothing, upParam.param_Update.RecID, Nothing, inBasicParam)
            'If upParam.UpdateSplVchrRefs IsNot Nothing Then
            '    If upParam.UpdateSplVchrRefs.Length > 0 Then
            '        For Each Svr_Param As Parameter_InsertSplVchrRef_Vouchers In upParam.UpdateSplVchrRefs
            '            If Not Svr_Param Is Nothing Then
            '                InsertReference(Svr_Param, upParam.param_UpdatePurpose.RecID, upParam.param_UpdatePurpose.Amount, inBasicParam)
            '            End If
            '        Next
            '    End If
            'End If

            Return True
        End Function

        Public Shared Function DeleteDonation_Txn(delParam As Param_Txn_Delete_VoucherDonation, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            ' Dim txn As System.Transactions.TransactionScope = Common.GetTransactionScope
            ' Dim RequestTime As DateTime = DateTime.Now
            ' Using txn
            'Using Common.GetConnectionScope() 
            'Txn_Master is not referred in Doantion transaction , thats why a roud fix needed to be made in IsPeriodRestricted() check in checkAuthorization, and Txn_Purpose has been included in the check for a while 
            'Once Master is included in Donation references, this Round Fix has to be removed
            If Not delParam.RecID_DeletePurpose Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_PURPOSE_INFO, "TR_REC_ID = '" & delParam.RecID_DeletePurpose & "'", inBasicParam)
            End If
            If Not delParam.RecID_DeleteFgnInfo Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_FOREIGN_INFO, "TR_REC_ID = '" & delParam.RecID_DeleteFgnInfo & "'", inBasicParam)
            End If
            If Not delParam.RecID_DeleteStatus Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.DONATION_STATUS_INFO, "DS_TR_ID = '" & delParam.RecID_DeleteStatus & "'", inBasicParam)
                'Delete Receipt 
                dbService.DeleteByCondition(ConnectOneWS.Tables.DONATION_RECEIPT_INFO, "DR_TR_ID = '" & delParam.RecID_DeleteStatus & "'", inBasicParam)
                'Donation Address Book 
                dbService.DeleteByCondition(ConnectOneWS.Tables.DONATION_RECEIPT_ADDRESS_BOOK, "C_TR_ID = '" & delParam.RecID_DeleteStatus & "'", inBasicParam)
            End If
            If Not delParam.ID_DeleteSlip Is Nothing Then
                dbService.DeleteByCondition(ConnectOneWS.Tables.TRANSACTION_D_SLIP_INFO, "TR_REC_ID = '" & delParam.ID_DeleteSlip & "'", inBasicParam)
            End If
            If Not delParam.RecID_Delete Is Nothing Then
                dbService.Delete(ConnectOneWS.Tables.TRANSACTION_INFO, delParam.RecID_Delete, inBasicParam)
            End If
            ' End Using
            '   txn.Complete()
            '  End Using
            Return True
        End Function

    End Class
#End Region
End Namespace
