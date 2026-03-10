'SQL, Shifted
Imports System.Globalization
Imports Common_Lib.RealTimeService
'Imports System.Data;
Partial Public Class DbOperations
#Region "--Profile--"
    <Serializable>
    Public Class Report
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetCenterDetails() As DataTable
            Return GetCenterDetailsForCenID(ClientScreen.Profile_Report)
        End Function

        'Shifted
        Public Function GetInstitutes() As DataTable
            Return GetInstituteList(ClientScreen.Profile_Report, "C_Name", "ID")
        End Function

        'Shifted
        Public Function GetResponsiblePerson() As DataTable
            Dim _core As Core = New Core(cBase)
            Return _core.GetCenSupportInfo(ClientScreen.Profile_Report)
        End Function

        'Shifted
        Public Function GetCashOpeningBalance() As DataTable
            Dim LocalQuery As String = " SELECT '" & Format(DateAdd(DateInterval.Day, -1, cBase._open_Year_Sdt), cBase._Date_Format_Current) & "' as Date,OP_AMOUNT FROM Opening_Balances_Info " &
                                 " Where   REC_STATUS IN (0,1,2) AND OP_CEN_ID=" & cBase._open_Cen_ID.ToString & "  AND  REC_ID LIKE '%CASH%' ; "
            Dim Param As Param_GetOpeningBalance_Common = New Param_GetOpeningBalance_Common()
            Param.Date_Format_Current = cBase._Date_Format_Current
            Param.open_Year_Sdt = cBase._open_Year_Sdt
            Return GetOpeningBalance(ClientScreen.Profile_Report, Param)
        End Function

        'Shifted
        Public Function GetBankAccountInfo() As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Return _BA.GetList(ClientScreen.Profile_Report)
        End Function

        'Shifted
        Public Function GetBranchDetails(ByVal Branch_IDs As String) As DataTable
            Return GetBankBranchesForMultipleIDs(Branch_IDs, ClientScreen.Profile_Report)
        End Function

        'Shifted
        Public Function GetBankOpeningBalance()
            Return GetOpeningBalance(ClientScreen.Profile_Report, "BA_ID")
        End Function

        'Shifted
        Public Function GetFDs(ByVal OtherCondition As String) As DataTable
            Dim _Fd As FD = New FD(cBase)
            Return _Fd.GetListByCondition(ClientScreen.Profile_Report, OtherCondition)
        End Function

        'Shifted
        Public Function GetLandAndBuildings(ByVal Ins_Name As String, ByVal OtherCondition As String) As DataTable
            Dim _LB As LandAndBuilding = New LandAndBuilding(cBase)
            Return _LB.GetListByCondition(ClientScreen.Profile_Report, Ins_Name, OtherCondition)
        End Function

        'Shifted
        Public Function GetPartyIDs() As DataTable
            Return GetPartyIDList(ClientScreen.Profile_Report)
        End Function

        'Shifted
        Public Function GetPartyNames(ByVal PartyIDs As String) As DataTable
            Dim _address As Addresses = New Addresses(cBase)
            Return _address.GetPartiesListByRecIDs(ClientScreen.Profile_Report, PartyIDs)
        End Function

        'Shifted
        Public Function GetInsuranceCompanies(ByVal RecIdColumnHead As String, ByVal NameColumnHead As String)
            Return GetMisc("INSURANCE", ClientScreen.Profile_LiveStock, NameColumnHead, RecIdColumnHead)
        End Function

        'Shifted
        Public Function GetUsedItemID() As DataTable
            Return GetUsedItemIDList(ClientScreen.Profile_Report)
        End Function

        'Shifted
        Public Function GetItemsByID(ByVal ItemIDs As String) As DataTable
            Return GetItems(ItemIDs, ClientScreen.Profile_Report)
        End Function

        'Shifted
        Public Function GetAssetLocations() As DataTable
            Dim _al As AssetLocations = New AssetLocations(cBase)
            Return _al.GetList(ClientScreen.Profile_Report, Nothing, Nothing)
        End Function

        'Shifted
        Public Function GetAssets(ByVal OtherCondition As String) As DataTable
            Dim _asset As Assets = New Assets(cBase)
            Return _asset.GetListByCondition(ClientScreen.Profile_Report, OtherCondition)
        End Function

        'Shifted
        Public Function GetVehicles(ByVal OtherCondition As String) As DataTable
            Dim _vehicle As Vehicles = New Vehicles(cBase)
            Return _vehicle.GetListByCondition(ClientScreen.Profile_Report, OtherCondition)
        End Function

        'Shifted
        Public Function GetGoldSilver(ByVal OtherCondition As String) As DataTable
            Dim _gs As GoldSilver = New GoldSilver(cBase)
            Return _gs.GetListByCondition(ClientScreen.Profile_Report, OtherCondition)
        End Function

        'Shifted
        Public Function GetAdvances(ByVal OtherCondition As String) As DataTable
            Dim _adv As Advances = New Advances(cBase)
            Return _adv.GetListByCondition(ClientScreen.Profile_Report, OtherCondition)
        End Function

        'Shifted
        Public Function GetDeposits(ByVal OtherCondition As String) As DataTable
            Dim _dep As Deposits = New Deposits(cBase)
            Return _dep.GetListByCondition(ClientScreen.Profile_Report, OtherCondition)
        End Function

        'Shifted
        Public Function GetLiabilities(ByVal OtherCondition As String) As DataTable
            Dim _liability As Liabilities = New Liabilities(cBase)
            Return _liability.GetListByCondition(ClientScreen.Profile_Report, OtherCondition)
        End Function

        'Shifted
        Public Function GetLiveStocks(ByVal OtherCondition As String) As DataTable
            Dim _livestock As LiveStock = New LiveStock(cBase)
            Return _livestock.GetListByCondition(ClientScreen.Profile_Report, OtherCondition)
        End Function

        'Shifted
        Public Function GetConsumableStock() As DataTable
            Dim _CS As ConsumableStock = New ConsumableStock(cBase)
            Return _CS.GetList(ClientScreen.Profile_Report)
        End Function

        'Shifted
        Public Function GetCS_GS_Items() As DataTable
            Return GetMisc("'GOLD / SILVER ITEMS','STOCK OF CONSUMABLES'", ClientScreen.Profile_Report, "MISC_NAME", "ID")
        End Function
    End Class
#End Region

#Region "Reports"
    <Serializable>
    Public Class Report_ItemList
        Inherits SharedVariables
        <Serializable>
        Public Class ItemInfo

            ''' <summary>
            ''' Original code field name is Item Name
            ''' </summary>
            Public Property ITEM_NAME As String
            Public Property ITEM_LED_ID As String
            Public Property Head As String
            Public Property LED_TYPE As String
            Public Property ITEM_CON_LED_ID As String
            Public Property ITEM_CON_MIN_VALUE As Int32
            Public Property ITEM_CON_MAX_VALUE As Int32
            Public Property CON_LED_NAME As String
            Public Property CON_LED_TYPE As String
            ''' <summary>
            ''' Original code field name is Voucher Type
            ''' </summary>
            Public Property Voucher_Type As String
            Public Property Profile As String
            ''' <summary>
            ''' Original code field name is Transaction Stmt
            ''' </summary>
            Public Property Transaction_Stmt As String
            ''' <summary>
            ''' Original Field Name : Construction Stmt
            ''' </summary>
            Public Property Construction_Stmt As String
            ''' <summary>
            ''' Original Field Name : Party Required
            ''' </summary>
            Public Property Party_Required As String
            ''' <summary>
            ''' Original Field Name : Note Book Item
            ''' </summary>
            Public Property Note_Book_Item As String
            ''' <summary>
            ''' Original Field Name : TDS Section
            ''' </summary>
            Public Property TDS_Section As String
            Public Property Applicable As String
            Public Property ID As String
            ''Common Columns 
            ''' <summary>
            ''' Original Column name : Add By
            ''' </summary>
            Public Property Add_By As String
            ''' <summary>
            ''' Original Column name : Add Date
            ''' </summary>
            Public Property Add_Date As String
            ''' <summary>
            ''' Original Column name : Edit By
            ''' </summary>
            Public Property Edit_By As String
            ''' <summary>
            ''' Original Column name : Edit Date
            ''' </summary>
            Public Property Edit_Date As String
            ''' <summary>
            ''' Original Column name : Action Status 
            ''' </summary>
            Public Property Action_Status As String
            ''' <summary>
            ''' Original Column name : Action By
            ''' </summary>
            Public Property Action_By As String
            ''' <summary>
            ''' Original Column name : Action Date
            ''' </summary>
            Public Property Action_Date As String
        End Class
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        'Shifted
        Public Function GetItemsList() As List(Of ItemInfo)
            Dim LocalQuery As String = " SELECT I.ITEM_NAME AS [Item Name],I.ITEM_LED_ID, L.LED_NAME AS [Head],L.LED_TYPE ,I.ITEM_CON_LED_ID ,I.ITEM_CON_MIN_VALUE ,I.ITEM_CON_MAX_VALUE ,CL.LED_NAME AS CON_LED_NAME ,CL.LED_TYPE AS CON_LED_TYPE ,  I.ITEM_VOUCHER_TYPE as [Voucher Type] ,I.ITEM_PROFILE as [Profile],I.ITEM_TRANS_STMT AS [Transaction Stmt],I.ITEM_CONST_STMT AS [Construction Stmt],I.ITEM_PARTY_REQ as [Party Required] ,I.ITEM_PETTY_CASH  AS [Note Book Item], I.ITEM_TDS_CODE as [TDS Section],I.ITEM_APPLICABLE AS [Applicable], I.REC_ID AS [ID]  ," & cBase.Rec_Detail("I", Common.DbConnectionMode.Local) & " " &
                                       " FROM (Item_Info AS I INNER JOIN Acc_Ledger_Info AS L  ON I.ITEM_LED_ID = L.LED_ID ) " &
                                       "                      LEFT  JOIN Acc_Ledger_Info AS CL ON I.ITEM_CON_LED_ID = CL.LED_ID " &
                                       " WHERE  I.REC_STATUS IN (0,1,2) order by I.ITEM_NAME   " ' AND UCASE(I.ITEM_VOUCHER_TYPE) NOT IN ('NOT APPLICABLE') '
            Dim param As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon
            Dim rTable As DataTable = GetItems_Ledger(ClientScreen.Report_Items, LocalQuery, param) 'No parameter needed in this case 

            Dim _ItemList As List(Of ItemInfo) = New List(Of ItemInfo)
            If (Not (rTable) Is Nothing) Then
                For Each row As DataRow In rTable.Rows
                    Dim newdata = New ItemInfo
                    newdata.ITEM_NAME = row.Field(Of String)("Item Name")
                    newdata.ITEM_LED_ID = row.Field(Of String)("ITEM_LED_ID")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.LED_TYPE = row.Field(Of String)("LED_TYPE")
                    newdata.ITEM_CON_LED_ID = row.Field(Of String)("ITEM_CON_LED_ID")
                    newdata.ITEM_CON_MIN_VALUE = row.Field(Of Integer)("ITEM_CON_MIN_VALUE")
                    newdata.ITEM_CON_MAX_VALUE = row.Field(Of Integer)("ITEM_CON_MAX_VALUE")
                    newdata.CON_LED_NAME = row.Field(Of String)("CON_LED_NAME")
                    newdata.CON_LED_TYPE = row.Field(Of String)("CON_LED_TYPE")
                    newdata.Voucher_Type = row.Field(Of String)("Voucher Type")
                    newdata.Profile = row.Field(Of String)("Profile")
                    newdata.Transaction_Stmt = row.Field(Of String)("Transaction Stmt")
                    newdata.Construction_Stmt = row.Field(Of String)("Construction Stmt")
                    newdata.Party_Required = row.Field(Of String)("Party Required")
                    newdata.Note_Book_Item = row.Field(Of String)("Note Book Item")
                    newdata.TDS_Section = row.Field(Of String)("TDS Section")
                    newdata.Applicable = row.Field(Of String)("Applicable")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Add_By = row.Field(Of String)("Add By")
                    newdata.Add_Date = row.Field(Of DateTime?)("Add Date").ToString
                    newdata.Edit_By = row.Field(Of String)("Edit By")
                    newdata.Edit_Date = row.Field(Of DateTime?)("Edit Date").ToString
                    newdata.Action_Status = row.Field(Of String)("Action Status")
                    newdata.Action_By = row.Field(Of String)("Action By")
                    newdata.Action_Date = row.Field(Of DateTime?)("Action Date").ToString
                    newdata.ID = row.Field(Of String)("ID")
                    _ItemList.Add(newdata)
                Next
            End If

            Return _ItemList
        End Function
    End Class
    <Serializable>
    Public Class Report_Ledgers
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

#Region "Report Classes"
        <Serializable>
        Public Class MagSubDispatchReport
            Public Property Grid1 As List(Of MagSubDispatchReport_Grid1)
            Public Property Grid2 As List(Of MagSubDispatchReport_Grid2)
        End Class
        <Serializable>
        Public Class MagSubDispatchReport_Grid2
            Public Property Issue_Date As DateTime
            Public Property Paid_Subscription As Int32
            Public Property Free_Subscription As Int32
            Public Property Total_Subscription As Int32
            Public Property Total_Dispatch As Int32
            Public Property Temp_ID As String
        End Class
        <Serializable>
        Public Class MagSubDispatchReport_Grid1
            Public Property Magazine_Name As String
            Public Property Issue_Date As DateTime
            Public Property Member_ID As String
            Public Property Member_Name As String
            Public Property Paid_Subscription As Int32
            Public Property Free_Subscription As Int32
            Public Property Total_Subscription As Int32
            Public Property Total_Dispatch As Int32
            Public Property Member_Type As String
            Public Property Status As String
            Public Property Category As String
            Public Property Temp_ID As String
        End Class
        <Serializable>
        Public Class PartyBalancesReport
            Public Property PARTY As String
            Public Property PAN As String
            Public Property CITY As String
            Public Property Opening_Balance As String
            Public Property Opening_Value As Decimal
            ''' <summary>
            ''' Original Column name is DebitTxns
            ''' </summary>
            ''' <returns></returns>
            Public Property Debit_Txns As Decimal
            ''' <summary>
            ''' Original Column name is CreditTxns
            ''' </summary>
            ''' <returns></returns>
            Public Property Credit_Txns As Decimal
            Public Property Closing_Balance As String
            Public Property PARTYID As String
            Public Property Closing_Value As Decimal
        End Class
        <Serializable>
        Public Class PartyLedgerReport
            Public Property Voucher As String
            ''' <summary>
            ''' Original Column name is Date
            ''' </summary>
            ''' <returns></returns>
            Public Property _Date As String
            Public Property Particulars As String
            ''' <summary>
            ''' Original Column name is Item Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Item_Name As String
            Public Property Debit As Decimal?
            Public Property Credit As Decimal?
            Public Property REC_ID As String
            Public Property TR_M_ID As String
            Public Property iTR_TEMP_ID As String
            Public Property NARRATION As String
            Public Property Balance As String
            Public Property iREQ_ATTACH_COUNT As Int32?
            Public Property iCOMPLETE_ATTACH_COUNT As Int32?
            Public Property iRESPONDED_COUNT As Int32?
            Public Property iREJECTED_COUNT As Int32?
            Public Property iOTHER_ATTACH_CNT As Int32?
            Public Property iALL_ATTACH_CNT As Int32?
            Public Property iIcon As String
            Public Property Screen As String
            Public Property InvNo As String
            Public Property InvDt As Date?
            Public Property Payment_RefNo As String
            Public Property Num_Balance As Decimal
        End Class
        <Serializable>
        Public Class PartyList
            Public Property ID As String
            Public Property Name As String
            Public Property PAN As String
            Public Property State As String
            Public Property City As String
            Public Property GST As String
            Public Property Mobile As String
            Public Property Email As String
            Public Property REC_EDIT_ON As DateTime
        End Class
        <Serializable>
        Public Class LedgerDetailReport
            Public Property Voucher As String
            ''' <summary>
            ''' Original Column name is Date
            ''' </summary>
            ''' <returns></returns>
            Public Property _Date As DateTime
            Public Property Particulars As String
            ''' <summary>
            ''' Original Column name is Item Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Item_Name As String
            Public Property Party As String
            Public Property Debit As Decimal?
            Public Property Credit As Decimal?
            Public Property TR_NARRATION As String
            Public Property REC_ID As String
            Public Property TR_M_ID As String
            Public Property TR_REF_NO As String
            Public Property TR_REF_DATE As DateTime?
            Public Property iTR_TEMP_ID As String
            Public Property Balance As String
            Public Property iREQ_ATTACH_COUNT As Int32?
            Public Property iCOMPLETE_ATTACH_COUNT As Int32?
            Public Property iRESPONDED_COUNT As Int32?
            Public Property iREJECTED_COUNT As Int32?
            Public Property iOTHER_ATTACH_CNT As Int32?
            Public Property iALL_ATTACH_CNT As Int32?
            Public Property iIcon As String
        End Class
        <Serializable>
        Public Class LedgerList
            Public Property ID As String
            Public Property Name As String
            Public Property Type As String
            Public Property Sub_Led_ID As String
        End Class
        <Serializable>
        Public Class BankAccountReport
            Public Property Center As String
            Public Property UID As String
            Public Property Inst As String
            Public Property Add1 As String
            Public Property Add2 As String
            Public Property Add3 As String
            Public Property Add4 As String
            Public Property City As String
            Public Property State As String
            ''' <summary>
            ''' Actual Column Name is Connected Center
            ''' </summary>
            ''' <returns></returns>
            Public Property Connected_Center As String
            ''' <summary>
            ''' Actual Column name is CC UID
            ''' </summary>
            ''' <returns></returns>
            Public Property CC_UID As String
            Public Property Bank As String
            Public Property Branch As String
            Public Property IFSC As String
            ''' <summary>
            ''' Actual Column name is Acc No.
            ''' </summary>
            ''' <returns></returns>
            Public Property Acc_No As String
            Public Property Sign1 As String
            Public Property Sign2 As String
            Public Property Zone As String
            Public Property Status As String
            Public Property CIF As String
            ''' <summary>
            ''' Actual Field Name is Opening Date
            ''' </summary>
            ''' <returns></returns>
            Public Property Opening_Date As DateTime?
        End Class
#End Region
        Public Function GetMagSubDispatchReport(ByVal inParam As Param_MagSubDispatchReport) As MagSubDispatchReport
            Dim _RetTable As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.Reports_GetMagSubDispatchReport, ClientScreen.Report_MagSubDispatch, inParam)
            Dim data As MagSubDispatchReport = New MagSubDispatchReport
            Dim Grid1 As List(Of MagSubDispatchReport_Grid1) = New List(Of MagSubDispatchReport_Grid1)
            Dim Grid2 As List(Of MagSubDispatchReport_Grid2) = New List(Of MagSubDispatchReport_Grid2)
            If (Not (_RetTable.Tables(0)) Is Nothing) Then
                For Each row As DataRow In _RetTable.Tables(0).Rows
                    Dim newdata = New MagSubDispatchReport_Grid1
                    newdata.Magazine_Name = row.Field(Of String)("Magazine Name")
                    newdata.Issue_Date = row.Field(Of DateTime)("Issue Date")
                    newdata.Member_ID = row.Field(Of String)("Member ID")
                    newdata.Member_Name = row.Field(Of String)("Member Name")
                    newdata.Paid_Subscription = row.Field(Of Int32)("Paid Subscription")
                    newdata.Free_Subscription = row.Field(Of Int32)("Free Subscription")
                    newdata.Total_Subscription = row.Field(Of Int32)("Total Subscription")
                    newdata.Total_Dispatch = row.Field(Of Int32)("Total Despatch")
                    newdata.Member_Type = row.Field(Of String)("Member Type (Individual/Centre)")
                    newdata.Status = row.Field(Of String)("Status (continue/discontinue)")
                    newdata.Category = row.Field(Of String)("Category (Indian/Foreign)")
                    newdata.Temp_ID = row.Field(Of String)("Temp_ID")
                    Grid1.Add(newdata)
                Next
            End If
            If (Not (_RetTable.Tables(1)) Is Nothing) Then
                For Each row As DataRow In _RetTable.Tables(1).Rows
                    Dim newdata = New MagSubDispatchReport_Grid2
                    newdata.Issue_Date = row.Field(Of DateTime)("Issue Date")
                    newdata.Paid_Subscription = row.Field(Of Int32)("Paid Subscription")
                    newdata.Free_Subscription = row.Field(Of Int32)("Free Subscription")
                    newdata.Total_Subscription = row.Field(Of Int32)("Total Subscription")
                    newdata.Total_Dispatch = row.Field(Of Int32)("Total Despatch")
                    newdata.Temp_ID = row.Field(Of String)("Temp_ID")
                    Grid2.Add(newdata)
                Next
            End If
            data.Grid1 = Grid1
            data.Grid2 = Grid2
            Return data
        End Function
        Public Function GetPartyList(ByVal inParam As Param_GetPartyListing) As List(Of PartyBalancesReport)
            Dim _RetTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Reports_GetPartyListing, ClientScreen.Report_PartyListing, inParam)
            Dim _PartyBalances As List(Of PartyBalancesReport) = New List(Of PartyBalancesReport)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New PartyBalancesReport
                    newdata.PARTY = row.Field(Of String)("PARTY")
                    newdata.PAN = row.Field(Of String)("PAN")
                    newdata.CITY = row.Field(Of String)("CITY")
                    newdata.Opening_Balance = row.Field(Of String)("Opening_Balance")
                    newdata.Opening_Value = row.Field(Of Decimal)("Opening_Value")
                    newdata.Debit_Txns = row.Field(Of Decimal)("DebitTxns")
                    newdata.Credit_Txns = row.Field(Of Decimal)("CreditTxns")
                    newdata.Closing_Balance = row.Field(Of String)("Closing_Balance")
                    newdata.PARTYID = row.Field(Of String)("PARTYID")
                    newdata.Closing_Value = row.Field(Of Decimal)("Closing_Value")
                    _PartyBalances.Add(newdata)
                Next
            End If
            Return _PartyBalances
        End Function

        Public Function GetPartyReport(ByVal inParam As Param_GetPartyReport, Opening As Decimal, FrDate As DateTime) As List(Of PartyLedgerReport)
            Dim _Party_Table As DataTable = GetDataListOfRecords(RealServiceFunctions.Reports_GetPartyReport, ClientScreen.Report_PartyReport, inParam)
            _Party_Table.Columns.Add(New DataColumn("Balance", inParam.PartyId.GetType()))
            _Party_Table.Columns.Add(New DataColumn("Num_Balance", GetType(Decimal)))
            Dim Total As Double = Opening
            Dim TotCredit As Double = 0
            Dim TotDebit As Double = 0
            For Each cRow As DataRow In _Party_Table.Rows
                If Not IsDBNull(cRow("Debit")) Then
                    If cRow("Debit").ToString().Length > 0 Then Total = Math.Round(Total + Convert.ToDouble(cRow("Debit")), 2) : TotDebit += Convert.ToDouble(cRow("Debit"))
                End If
                If Not IsDBNull(cRow("Credit")) Then
                    If cRow("Credit").ToString().Length > 0 Then Total = Math.Round(Total - Convert.ToDouble(cRow("Credit")), 2) : TotCredit += Convert.ToDouble(cRow("Credit"))
                End If
                If Total > 0 Then
                    'cRow("Balance") = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:N2}{1}", Total, " Dr") 'Total.ToString() + " Dr"
                    cRow("Balance") = "Dr"
                ElseIf Total < 0 Then
                    'cRow("Balance") = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:N2}{1}", -1 * Total, " Cr") '(Total * -1).ToString() + " Cr"
                    cRow("Balance") = "Cr"
                Else
                    cRow("Balance") = "-"
                End If
                cRow("Num_Balance") = Total
            Next

            Dim OpeningRow As DataRow = _Party_Table.NewRow
            OpeningRow("Date") = FrDate.ToString("MMM-dd-yyyy")
            OpeningRow("Particulars") = "Opening Balance"
            OpeningRow("iTR_TEMP_ID") = "Opening_Balance"
            'If Opening > 0 Then OpeningRow("Balance") = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:N2}{1}", Opening, " Dr")
            'If Opening < 0 Then OpeningRow("Balance") = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:N2}{1}", -1 * Opening, " Cr") '(-1 * Opening).ToString + " Cr."
            'If Opening = 0 Then OpeningRow("Balance") = Opening.ToString

            If Opening > 0 Then OpeningRow("Balance") = "Dr"
            If Opening < 0 Then OpeningRow("Balance") = "Cr"
            'If Opening = 0 Then OpeningRow("Balance") = Opening.ToString

            OpeningRow("Num_Balance") = Opening
            _Party_Table.Rows.InsertAt(OpeningRow, 0)

            Dim _PartyLedger As List(Of PartyLedgerReport) = New List(Of PartyLedgerReport)
            If (Not (_Party_Table) Is Nothing) Then
                For Each row As DataRow In _Party_Table.Rows
                    Dim newdata = New PartyLedgerReport()
                    newdata.Voucher = row.Field(Of String)("Voucher")
                    newdata._Date = row.Field(Of String)("Date")
                    newdata.Particulars = row.Field(Of String)("Particulars")
                    newdata.Item_Name = row.Field(Of String)("Item Name")
                    If Not IsDBNull(row("Debit")) And String.IsNullOrWhiteSpace(row("Debit").ToString()) = False Then
                        newdata.Debit = Convert.ToDecimal(row("Debit"))
                    End If
                    If Not IsDBNull(row("Credit")) And String.IsNullOrWhiteSpace(row("Credit").ToString()) = False Then
                        newdata.Credit = Convert.ToDecimal(row("Credit"))
                    End If
                    newdata.REC_ID = row.Field(Of String)("REC_ID")
                    newdata.TR_M_ID = row.Field(Of String)("TR_M_ID")
                    newdata.iTR_TEMP_ID = row.Field(Of String)("iTR_TEMP_ID")
                    newdata.NARRATION = row.Field(Of String)("NARRATION")
                    newdata.Screen = row.Field(Of String)("SCREEN")
                    newdata.InvNo = row.Field(Of String)("InvNo")
                    newdata.InvDt = row.Field(Of Date?)("InvDt")
                    newdata.Payment_RefNo = row.Field(Of String)("Payment_Ref_No")
                    newdata.Balance = row.Field(Of String)("Balance")
                    newdata.Num_Balance = row.Field(Of Decimal)("Num_Balance")
                    If row.Table.Columns.Contains("REQ_ATTACH_COUNT") Then
                        newdata.iREQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    End If
                    If row.Table.Columns.Contains("COMPLETE_ATTACH_COUNT") Then
                        newdata.iCOMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    End If
                    If row.Table.Columns.Contains("RESPONDED_COUNT") Then
                        newdata.iRESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    End If
                    If row.Table.Columns.Contains("REJECTED_COUNT") Then
                        newdata.iREJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
                    End If
                    If row.Table.Columns.Contains("OTHER_ATTACH_CNT") Then
                        newdata.iOTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    End If
                    If row.Table.Columns.Contains("ALL_ATTACH_CNT") Then
                        newdata.iALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")
                    End If

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


                    _PartyLedger.Add(newdata)
                Next
            End If
            Return _PartyLedger
        End Function

        Public Function GetLedgerReport(ByVal inParam As Param_GelLedgerReport, Opening As Decimal, FrDate As DateTime) As List(Of LedgerDetailReport)
            Dim _Ledger_Table As DataTable = GetDataListOfRecords(RealServiceFunctions.Reports_GetLedgerReport, ClientScreen.Report_LedgerReport, inParam)
            _Ledger_Table.Columns.Add(New DataColumn("Balance", inParam.Led_ID.GetType()))
            Dim Total As Decimal = Opening
            Dim TotCredit As Decimal = 0
            Dim TotDebit As Decimal = 0
            For Each cRow As DataRow In _Ledger_Table.Rows
                If Not IsDBNull(cRow("Debit")) Then
                    If cRow("Debit").ToString().Length > 0 Then Total = Total + Convert.ToDecimal(cRow("Debit")) : TotDebit += Convert.ToDecimal(cRow("Debit"))
                End If
                If Not IsDBNull(cRow("Credit")) Then
                    If cRow("Credit").ToString().Length > 0 Then Total = Total - Convert.ToDecimal(cRow("Credit")) : TotCredit += Convert.ToDecimal(cRow("Credit"))
                End If
                If Total > 0 Then
                    cRow("Balance") = Total.ToString() + " Dr"
                ElseIf Total < 0 Then
                    cRow("Balance") = (Total * -1).ToString() + " Cr"
                End If
            Next

            Dim OpeningRow As DataRow = _Ledger_Table.NewRow
            OpeningRow("Date") = FrDate.ToString("MMM-dd-yyyy")
            OpeningRow("Particulars") = "Opening Balance"
            OpeningRow("iTR_TEMP_ID") = "Opening_Balance"
            If Opening > 0 Then OpeningRow("Balance") = Opening.ToString + " Dr."
            If Opening < 0 Then OpeningRow("Balance") = (-1 * Opening).ToString + " Cr."
            If Opening = 0 Then OpeningRow("Balance") = Opening.ToString
            _Ledger_Table.Rows.InsertAt(OpeningRow, 0)

            Dim _Ledger As List(Of LedgerDetailReport) = New List(Of LedgerDetailReport)
            If (Not (_Ledger_Table) Is Nothing) Then
                For Each row As DataRow In _Ledger_Table.Rows
                    Dim newdata = New LedgerDetailReport()
                    newdata.Voucher = row.Field(Of String)("Voucher")
                    newdata._Date = row.Field(Of DateTime)("Date")
                    newdata.Particulars = row.Field(Of String)("Particulars")
                    newdata.Item_Name = row.Field(Of String)("Item Name")
                    newdata.Debit = row.Field(Of Decimal?)("Debit")
                    newdata.Credit = row.Field(Of Decimal?)("Credit")
                    newdata.REC_ID = row.Field(Of String)("REC_ID")
                    newdata.TR_M_ID = row.Field(Of String)("TR_M_ID")
                    newdata.TR_NARRATION = row.Field(Of String)("TR_NARRATION")
                    newdata.Party = row.Field(Of String)("Party")
                    newdata.Balance = row.Field(Of String)("Balance")
                    newdata.iTR_TEMP_ID = row.Field(Of String)("iTR_TEMP_ID")
                    newdata.TR_REF_NO = row.Field(Of String)("TR_REF_NO")
                    newdata.TR_REF_DATE = row.Field(Of DateTime?)("TR_REF_DATE")
                    If row.Table.Columns.Contains("REQ_ATTACH_COUNT") Then
                        newdata.iREQ_ATTACH_COUNT = row.Field(Of Int32?)("REQ_ATTACH_COUNT")
                    End If
                    If row.Table.Columns.Contains("COMPLETE_ATTACH_COUNT") Then
                        newdata.iCOMPLETE_ATTACH_COUNT = row.Field(Of Int32?)("COMPLETE_ATTACH_COUNT")
                    End If
                    If row.Table.Columns.Contains("RESPONDED_COUNT") Then
                        newdata.iRESPONDED_COUNT = row.Field(Of Int32?)("RESPONDED_COUNT")
                    End If
                    If row.Table.Columns.Contains("REJECTED_COUNT") Then
                        newdata.iREJECTED_COUNT = row.Field(Of Int32?)("REJECTED_COUNT")
                    End If
                    If row.Table.Columns.Contains("OTHER_ATTACH_CNT") Then
                        newdata.iOTHER_ATTACH_CNT = row.Field(Of Int32?)("OTHER_ATTACH_CNT")
                    End If
                    If row.Table.Columns.Contains("ALL_ATTACH_CNT") Then
                        newdata.iALL_ATTACH_CNT = row.Field(Of Int32?)("ALL_ATTACH_CNT")
                    End If

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



                    _Ledger.Add(newdata)
                Next
            End If
            Return _Ledger
        End Function

        Public Function GetTransactionDetailsForLedger(ByVal txnMID As String) As DataTable
            Return GetDataListOfRecords(RealServiceFunctions.Reports_GetTransactionDetailsForLedger, ClientScreen.Report_LedgerReport, txnMID)
        End Function

        Public Function GetLedgerOpening(ByVal inParam As Param_GetLedgerOpeningBalance) As Object
            Return GetSingleValue_Data(RealServiceFunctions.Reports_GetLedgerOpeningBalance, ClientScreen.Report_LedgerReport, inParam)
        End Function

        Public Function GetVehicleData(ByVal inParam As Param_GetVehicleReport) As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Reports_GetVehicleReportData, ClientScreen.Report_Vehicles, inParam)
        End Function

        Public Function GetAssetData(ByVal inParam As Param_GetAssetReport) As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Reports_GetAssetReportData, ClientScreen.Report_Assets, inParam)
        End Function

        Public Function GetGSData(ByVal inParam As Param_GetGSReport) As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Reports_GetGSReportData, ClientScreen.Report_GS, inParam)
        End Function

        Public Function GetLBData(ByVal inParam As Param_GetLBReport) As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Reports_GetLBReportData, ClientScreen.Report_LB, inParam)
        End Function

        Public Function GetFDData(ByVal inParam As Param_GetFDReport) As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Reports_GetFDReportData, ClientScreen.Report_FDReport, inParam)
        End Function

        Public Function GetLedgerList() As List(Of LedgerList)
            Dim _Ledgers_Table As DataTable = GetLedgers(ClientScreen.Report_LedgerReport)
            Dim _LedgerList As List(Of LedgerList) = New List(Of LedgerList)
            If (Not (_Ledgers_Table) Is Nothing) Then
                For Each row As DataRow In _Ledgers_Table.Rows
                    Dim newdata = New LedgerList
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Sub_Led_ID = row.Field(Of String)("Sub_Led_ID")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Type = row.Field(Of String)("Type")
                    _LedgerList.Add(newdata)
                Next
            End If
            Return _LedgerList
        End Function

        Public Function GetBankAccountsList() As List(Of BankAccountReport)
            Dim BankTable As DataTable = GetDataListOfRecords(RealServiceFunctions.Reports_GetBankAccountsList, ClientScreen.Report_Bank_Account_List)
            Dim _BankList As List(Of BankAccountReport) = New List(Of BankAccountReport)
            If (Not (BankTable) Is Nothing) Then
                For Each row As DataRow In BankTable.Rows
                    Dim newdata = New BankAccountReport
                    newdata.Center = row.Field(Of String)("Center")
                    newdata.UID = row.Field(Of String)("UID")
                    newdata.Inst = row.Field(Of String)("Inst")
                    newdata.Add1 = row.Field(Of String)("Add1")
                    newdata.Add2 = row.Field(Of String)("Add2")
                    newdata.Add3 = row.Field(Of String)("Add3")
                    newdata.Add4 = row.Field(Of String)("Add4")
                    newdata.City = row.Field(Of String)("City")
                    newdata.State = row.Field(Of String)("State")
                    newdata.Connected_Center = row.Field(Of String)("Connected Center")
                    newdata.CC_UID = row.Field(Of String)("CC UID")
                    newdata.Bank = row.Field(Of String)("Bank")
                    newdata.Branch = row.Field(Of String)("Branch")
                    newdata.IFSC = row.Field(Of String)("IFSC")
                    newdata.Acc_No = row.Field(Of String)("Acc No.")
                    newdata.Sign1 = row.Field(Of String)("Sign1")
                    newdata.Sign2 = row.Field(Of String)("Sign2")
                    newdata.Zone = row.Field(Of String)("Zone")
                    newdata.Status = row.Field(Of String)("Status")
                    newdata.CIF = row.Field(Of String)("CIF")
                    newdata.Opening_Date = row.Field(Of DateTime?)("Opening Date")
                    _BankList.Add(newdata)
                Next
            End If
            Return _BankList
        End Function

        Public Function GetInsurancePropertyList(ByVal inParam As Param_GetInsurancePropertyList) As DataSet
            Return GetDatasetOfRecordsBySP(RealServiceFunctions.Reports_GetInsurancePropertyList, ClientScreen.Report_LB, inParam)
        End Function

        Public Function GetParties(Optional Party_Rec_ID As String = Nothing) As List(Of PartyList)
            Dim _RetTable As DataTable = cBase._Journal_voucher_DBOps.GetParties("Name", "ID", Party_Rec_ID)
            Dim _PartyList As List(Of PartyList) = New List(Of PartyList)
            If (Not (_RetTable) Is Nothing) Then
                For Each row As DataRow In _RetTable.Rows
                    Dim newdata = New PartyList
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.PAN = row.Field(Of String)("PAN")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime?)("REC_EDIT_ON")
                    newdata.GST = row.Field(Of String)("GST")
                    newdata.State = row.Field(Of String)("State")
                    newdata.City = row.Field(Of String)("City")
                    newdata.Mobile = row.Field(Of String)("Mobile")
                    newdata.Email = row.Field(Of String)("Email")
                    _PartyList.Add(newdata)
                Next
            End If
            Return _PartyList
        End Function
    End Class
    <Serializable>
    Public Class Reports_All
        Inherits SharedVariables

#Region "Report Classes"
        <Serializable>
        Public Class AssetLocationLogs
            ''' <summary>
            ''' Original code field name is Transfer On
            ''' </summary>
            Public Property Transfer_On As DateTime
            Public Property Make As String
            Public Property Model As String
            Public Property Item As String
            Public Property Qty As Decimal
            ''' <summary>
            ''' Original code field name is Transferred From
            ''' </summary>
            Public Property Transferred_From As String
            ''' <summary>
            ''' Original Column Name is Transferred To
            ''' </summary>
            ''' <returns></returns>
            Public Property Transferred_To As String
            Public Property Remarks As String
        End Class
        <Serializable>
        Public Class ConstructionReport
            Public Property MonthNo As Integer?
            Public Property Month As String
            Public Property Amt As Decimal?
            Public Property Items As String
            ''' <summary>
            ''' Original Field name is Property 
            ''' </summary>
            ''' <returns></returns>
            Public Property _Property As String
        End Class
        <Serializable>
        Public Class WIPReport
            Public Property MonthNo As Integer?
            Public Property Month As String
            Public Property Amt As Decimal?
            Public Property Items As String
            ''' <summary>
            ''' Original Field name is Property 
            ''' </summary>
            ''' <returns></returns>
            Public Property _Property As String
        End Class
        <Serializable>
        Public Class PurposeReport
            ''' <summary>
            ''' Actual Field Name is Date
            ''' </summary>
            ''' <returns></returns>
            Public Property TxnDate As String
            Public Property Item As String
            Public Property Ledger As String
            Public Property Purpose As String
            Public Property Amount As Decimal?
            Public Property Nature As String

        End Class
        <Serializable>
        Public Class AssetInsuranceBreakUpReport
            Public Property Complex As String
            Public Property Item As String
            ''' <summary>
            ''' Real Field name is Asset Insurance Value
            ''' </summary>
            ''' <returns></returns>
            Public Property Asset_Insurance_Value As Decimal?
            ''' <summary>
            ''' Real Field name is Location Name
            ''' </summary>
            ''' <returns></returns>
            Public Property Location_Name As String
            Public Property Name As String
            Public Property TYPE As String
            Public Property UID As String
        End Class
        <Serializable>
        Public Class BalanceSheetReport
            ''' <summary>
            ''' Original code field name is Liability Details
            ''' </summary>
            Public Property Liability_Details As String
            ''' <summary>
            ''' Original field name is liabTotal
            ''' </summary>
            ''' <returns></returns>
            Public Property liab_Total As Decimal?
            ''' <summary>
            ''' Original field name is Asset Details
            ''' </summary>
            ''' <returns></returns>
            Public Property Asset_Details As String
            ''' <summary>
            ''' Original field name is assetTotal
            ''' </summary>
            ''' <returns></returns>
            Public Property asset_Total As Decimal?
        End Class
        <Serializable>
        Public Class TrialBalanceReport
            ''' <summary>
            ''' Original code field name is Liability Details
            ''' </summary>
            Public Property LEDGER As String
            Public Property NATURE As String
            Public Property PRIMARY_TYPE As String
            Public Property SECONDARY_TYPE As String
            Public Property Opening_Dr As Decimal?
            Public Property Opening_Cr As Decimal?
            Public Property DR As Decimal?
            Public Property CR As Decimal?
            Public Property Closing_Dr As Decimal?
            Public Property Closing_Cr As Decimal?
            Public Property LED_ID As String
        End Class
        <Serializable>
        Public Class IncomeExpenditureReport
            ''' <summary>
            ''' Original code field name is Expense Particulars
            ''' </summary>
            Public Property Expense_Particulars As String
            Public Property ExpAmount As Decimal?
            ''' <summary>
            ''' Original code field name is Income Particulars
            ''' </summary>
            Public Property Income_Particulars As String
            Public Property IncAmount As Decimal?
        End Class
        <Serializable>
        Public Class UtilizationReport
            Public Property ITEM As String
            Public Property IAmount As Decimal?
            Public Property IGroupSum As String
            Public Property PITEM As String
            Public Property IPAmount As Decimal?
            Public Property IPGroupSum As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Private Function getTransactionTable(ByVal IsReceipt As Boolean, ByVal FrDate As DateTime, ByVal ToDate As DateTime) As DataTable
            Dim dt As DataTable = cBase._Reports_Common_DBOps.GetTSummaryList(Common_Lib.RealTimeService.ClientScreen.Report_Transaction_Summary, IsReceipt, FrDate, ToDate)
            If dt Is Nothing Then
                DevExpress.XtraEditors.XtraMessageBox.Show(Common_Lib.Messages.SomeError, "Error!!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Return Nothing
                Exit Function
            End If
            Dim CashBankTotalAmt As String = "0.00"
            If dt.Rows.Count > 0 Then
                CashBankTotalAmt = dt.Rows(0)("IGroupSum")
                'remove 1st row from the table-- this represents the total sum. This will be added as last row
                dt.Rows.RemoveAt(0)
                'make GroupSum display adjustment. 
                'Table obtained from the database will have GroupSum displayed corresponding to its groupname
                'Hence a small adjustment is made to show GroupSum at the end of subgroups
                Dim totalAmount As String = ""
                Dim tempAmount As String = ""
                For i = 0 To dt.Rows.Count - 1
                    If Not String.IsNullOrEmpty(dt.Rows(i).Item("IGroupSum")) Then
                        If Not String.IsNullOrEmpty(totalAmount) Then
                            tempAmount = totalAmount
                        End If
                        totalAmount = dt.Rows(i).Item("IGroupSum")
                        dt.Rows(i).Item("IGroupSum") = ""
                        ' dt.Rows(i).Item("ITEM") = ""
                    End If

                    If (String.IsNullOrEmpty(dt.Rows(i).Item("IAmount")) And i > 0) Then
                        dt.Rows(i - 1).Item("IGroupSum") = tempAmount
                    End If
                Next
                dt.Rows(dt.Rows.Count - 1).Item("IGroupSum") = totalAmount
                dt.AcceptChanges()

                Dim rows = dt.[Select]("ITEM = 'FD With Banks'")
                For Each row As DataRow In rows
                    row.Delete()
                Next
                dt.AcceptChanges()

                Dim nTable As DataTable = dt.Clone
                For Each dRow As DataRow In dt.Rows
                    If ((Not String.IsNullOrEmpty(dRow.Item("ITEM")))) Then
                        Dim newRow As DataRow = nTable.NewRow()
                        newRow.ItemArray = dRow.ItemArray
                        nTable.Rows.Add(newRow)
                    End If
                Next
                dt = nTable
            End If

            dt.AcceptChanges()
            Return dt
        End Function

        Public Function GetUtilizationReport(ByVal screen As ClientScreen, ByVal xFr_Date As Date, ByVal xTo_Date As Date) As DataSet 'List(Of UtilizationReport)

            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CEN_ID", "@INS_ID", "@YEAR_ID", "@YEAR_START_DATE", "@YEAR_END_DATE"}
            Dim values() As Object = {cBase._open_Cen_ID, cBase._open_Ins_ID, cBase._open_Year_ID, xFr_Date, xTo_Date}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.Int32, DbType.DateTime, DbType.DateTime}
            Dim lengths() As Integer = {4, 5, 4, 100, 100}
            Return _RealService.ListDatasetFromSP(Tables.SERVICE_CHART_INFO, "[sp_rpt_UTILIZATION_REPORT]", paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Facility_ChartInfo))


            'Dim receiptsDt As New DataTable
            'Dim paymentsDt As New DataTable

            'receiptsDt = getTransactionTable(True, cBase._open_Year_Sdt, cBase._open_Year_Edt) ' Gets datatable for receipts
            'paymentsDt = getTransactionTable(False, cBase._open_Year_Sdt, cBase._open_Year_Edt) ' Gets datatable for Payments

            'Dim Utilization_Table As New DataTable : Dim ROW As DataRow
            'With Utilization_Table
            '    .Columns.Add("ITEM", Type.GetType("System.String"))
            '    .Columns.Add("IAmount", Type.GetType("System.Decimal"))
            '    .Columns.Add("IGroupSum", Type.GetType("System.String"))
            '    .Columns.Add("PITEM", Type.GetType("System.String"))
            '    .Columns.Add("IPAmount", Type.GetType("System.Decimal"))
            '    .Columns.Add("IPGroupSum", Type.GetType("System.String"))
            'End With
            'Dim totalRows As Integer = IIf(receiptsDt.Rows.Count > paymentsDt.Rows.Count, receiptsDt.Rows.Count, paymentsDt.Rows.Count)
            'Dim testDouble As Double = 0
            'For i = 0 To totalRows - 1
            '    ROW = Utilization_Table.NewRow()
            '    If (receiptsDt.Rows.Count > i) Then
            '        ROW("ITEM") = receiptsDt.Rows(i)("ITEM")
            '        ROW("IAmount") = IIf(Double.TryParse(receiptsDt.Rows(i)("IAmount").ToString(), testDouble), testDouble, 0)
            '        ROW("IGroupSum") = IIf(Double.TryParse(receiptsDt.Rows(i)("IGroupSum").ToString(), testDouble), receiptsDt.Rows(i)("IGroupSum"), "")
            '    Else
            '        ROW("ITEM") = ""
            '        ROW("IAmount") = ""
            '        ROW("IGroupSum") = ""
            '    End If

            '    If (paymentsDt.Rows.Count > i) Then
            '        ROW("PITEM") = paymentsDt.Rows(i)("ITEM")
            '        ROW("IPAmount") = IIf(Double.TryParse(paymentsDt.Rows(i)("IAmount").ToString(), testDouble), testDouble, 0)
            '        ROW("IPGroupSum") = IIf(Double.TryParse(paymentsDt.Rows(i)("IGroupSum").ToString(), testDouble), paymentsDt.Rows(i)("IGroupSum"), "")
            '    Else
            '        ROW("PITEM") = ""
            '        ROW("IPAmount") = ""
            '        ROW("IPGroupSum") = ""
            '    End If
            '    If Not (IsDBNull(ROW("ITEM")) And IsDBNull(ROW("PITEM"))) Then Utilization_Table.Rows.Add(ROW)
            'Next
            'Dim _UtilizationReport As List(Of UtilizationReport) = New List(Of UtilizationReport)
            'If (Not (Utilization_Table) Is Nothing) Then
            '    For Each _row As DataRow In Utilization_Table.Rows
            '        Dim newdata = New UtilizationReport
            '        newdata.ITEM = _row.Field(Of String)("ITEM")
            '        newdata.IAmount = _row.Field(Of Decimal?)("IAmount")
            '        newdata.IGroupSum = _row.Field(Of String)("IGroupSum")
            '        newdata.PITEM = _row.Field(Of String)("PITEM")
            '        newdata.IPAmount = _row.Field(Of Decimal?)("IPAmount")
            '        newdata.IPGroupSum = _row.Field(Of String)("IPGroupSum")
            '        _UtilizationReport.Add(newdata)
            '    Next
            'End If
            'Return _UtilizationReport
        End Function

        'Shifted
        'Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box_Voucher
        'ClientScreen.Report_Landscape
        'ClientScreen.Report_Potrait

        Public Function GetCentreDetails(ByVal Cen_id As Integer, ByVal screen As ClientScreen) As DataTable
            Dim LocalQuery As String = " SELECT CI.CEN_NAME ,CI.CEN_BK_PAD_NO , CI.CEN_UID , COALESCE(CITY.CI_NAME, MAIN.CEN_CITY) CEN_CITY, MAIN.CEN_INCHARGE , MAIN.CEN_ZONE_ID , CI.CEN_ID , CI.REC_ID, MAIN.CEN_TEL_NO_1, MAIN.CEN_MOB_NO_1 FROM CENTRE_INFO  AS CI INNER JOIN CENTRE_INFO AS MAIN ON CI.CEN_BK_PAD_NO = MAIN.CEN_PAD_NO AND MAIN.CEN_MAIN = 1 LEFT OUTER JOIN map_city_info AS CITY ON MAIN.CEN_CITY_ID = CITY.REC_ID Where CI.CEN_ID =  " + Cen_id.ToString + ""
            Return GetCenterDetailsByQuery(LocalQuery, screen, Cen_id)
        End Function

        'Shifted
        'Common_Lib.RealTimeService.ClientScreen.Report_Gift
        Public Function GetCentreCity(ByVal Cen_id As Integer, ByVal screen As ClientScreen) As DataTable
            Dim Query As String
            Query = " SELECT CEN_CITY FROM CENTRE_INFO Where CEN_ID = " + Cen_id.ToString + ""
            Return GetCenterDetailsByQuery(Query, screen, Cen_id)
        End Function

        'Shifted
        Public Function GetCityList(ByVal CityIDs As String) As DataTable
            Return GetCitiesByID(ClientScreen.Report_Gift, "CI_NAME", "REC_ID", CityIDs)
        End Function

        'Shifted
        Public Function GetStateList(ByVal StateIDs As String) As DataTable
            Return GetStatesByID(ClientScreen.Report_Gift, "ST_NAME", "REC_ID", StateIDs)
        End Function

        'Shifted
        Public Function GetDistrictList(ByVal DistrictIDs As String) As DataTable
            Return GetDistrictsByID(ClientScreen.Report_Gift, "DI_NAME", "REC_ID", DistrictIDs)
        End Function

        'Shifted
        Public Function GetCountryList(ByVal CountryIDs As String) As DataTable
            Return GetCountriesByID(ClientScreen.Report_Gift, "CO_NAME", "REC_ID", CountryIDs)
        End Function

        ''' <summary>
        ''' shifted
        ''' </summary>
        ''' <param name="MiscID"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetMiscNameByID(ByVal MiscID As String) As Object
            Dim Query As String = "SELECT MISC_NAME from MISC_INFO where REC_ID = '" & MiscID & "' ;"
            Dim dTable As DataTable = GetMisc_Common(Query, Query, ClientScreen.Report_Gift, MiscID)
            If dTable Is Nothing Then Return Nothing
            If dTable.Rows.Count = 0 Then Return Nothing
            Return dTable.Rows(0)(0)
        End Function

        ''' <summary>
        ''' shifted
        ''' </summary>
        ''' <param name="BankId"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetBankNameByID(ByVal BankId As String) As Object
            Dim Query As String = "SELECT BI_BANK_NAME From  BANK_INFO     " &
                                " Where   REC_ID = '" & BankId & "';"
            Dim dTable As DataTable = GetBankInfo(Query, Query, ClientScreen.Report_Gift, BankId)
            If dTable Is Nothing Then Return Nothing
            If dTable.Rows.Count = 0 Then Return Nothing
            Return dTable.Rows(0)(0)
        End Function

        'Shifted
        Public Function GetItemNameByID(ByVal ItemId As String) As DataTable
            Dim dTable As DataTable = GetItems(ItemId, ClientScreen.Report_Gift)
            Return dTable
        End Function

        ''' <summary>
        '''  Get Donation Status ID, Shifted
        ''' </summary>
        ''' <param name="RecID"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetDonationStatusID</remarks>
        Public Function GetDonationStatusID(ByVal RecID As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetDonationStatusID, screen, RecID)
        End Function

        'ClientScreen.Report_Transaction_Statement
        'Shifted
        Public Function GetItemsList(ByVal screen As ClientScreen) As DataTable
            Dim Query As String
            Query = " SELECT Rec_id AS II_REC_ID,item_name AS Particulars, ITEM_TRANS_TYPE AS Type, ITEM_TRANS_STMT AS Head  from item_info WHERE REC_STATUS IN (0,1,2)"
            Dim rTable As DataTable = GetItemsByQuery_Common(Query, screen, Nothing)
            'retTable = rTable
            Return rTable
        End Function

        '.ClientScreen.Report_Construction_Statement
        'Shifted
        Public Function GetApplicableItemsList(ByVal screen As ClientScreen) As DataTable
            Dim Query As String = " SELECT ITEM_CONST_STMT AS item, Rec_ID from item_info where ITEM_CONST_STMT <>'Not Applicable' AND REC_STATUS IN (0,1,2)"
            Dim inParam As Param_GetItemsByQueryCommon = New Param_GetItemsByQueryCommon()
            Return GetItemsByQuery_Common(Query, screen, inParam)
        End Function

        ''' <summary>
        ''' Get Transaction List, Shifted
        ''' </summary>
        ''' <param name="Fr_Date"></param>
        ''' <param name="To_Date"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetTransactionList</remarks>
        Public Function GetTransactionList(ByVal Fr_Date As Date, ByVal To_Date As Date, ByVal screen As ClientScreen) As DataTable
            Dim Param As Param_ReportsAll_GetTransactionList = New Param_ReportsAll_GetTransactionList()
            Param.Fr_Date = Fr_Date
            Param.To_Date = To_Date
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetTransactionList, screen, Param)
        End Function

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="Fr_Date"></param>
        ''' <param name="To_Date"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetConstructionWIPExpensesList(ByVal Fr_Date As Date, ByVal To_Date As Date, ByVal screen As ClientScreen) As DataSet
            Dim Param As Param_ReportsAll_GetConstructionExpensesList = New Param_ReportsAll_GetConstructionExpensesList()
            Param.Fr_Date = Fr_Date
            Param.To_Date = To_Date
            Return GetDatasetOfRecordsBySP(RealTimeService.RealServiceFunctions.ReportsAll_GetConstructionWIPExpensesList, screen, Param)
        End Function

        ''' <summary>
        ''' Get Gift Transaction List, Shifted
        ''' </summary>
        ''' <param name="Fr_Date"></param>
        ''' <param name="To_Date"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetGiftTransactionList</remarks>
        Public Function GetGiftTransactionList(ByVal Fr_Date As Date, ByVal To_Date As Date, ByVal screen As ClientScreen) As DataTable
            Dim Param As Param_ReportsAll_GetGiftTransactionList = New Param_ReportsAll_GetGiftTransactionList()
            Param.Fr_Date = Fr_Date
            Param.To_Date = To_Date
            Param.openYearID = cBase._open_Year_ID
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetGiftTransactionList, screen, Param)
        End Function

        ''' <summary>
        ''' Get CollectionBox Transaction List, Shifted
        ''' </summary>
        ''' <param name="Fr_Date"></param>
        ''' <param name="To_Date"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetCollectionBoxTransactionList</remarks>
        Public Function GetCollectionBoxTransactionList(ByVal Fr_Date As Date, ByVal To_Date As Date, ByVal screen As ClientScreen) As DataTable
            Dim Param As Param_ReportsAll_GetCollectionBoxTransactionList = New Param_ReportsAll_GetCollectionBoxTransactionList()
            Param.Fr_Date = Fr_Date
            Param.To_Date = To_Date
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetCollectionBoxTransactionList, screen, Param)
        End Function

        ''' <summary>
        ''' Get CollectionBox Transaction List, Shifted
        ''' </summary>
        ''' <param name="RecId"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetCollectionBoxTransactionListWithRecID</remarks>
        Public Function GetCollectionBoxTransactionList(ByVal RecId As String, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetCollectionBoxTransactionListWithRecID, screen, RecId)
        End Function
        'Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box
        'Shifted
        Public Function GetAddressList(ByVal screen As ClientScreen) As DataTable
            Dim LocalQuery As String = "Select ab1.C_TITLE + ' ' + ab1.C_NAME AS name, REC_ID from ADDRESS_BOOK ab1 where  REC_STATUS <> " & Common_Lib.Common.Record_Status._Deleted
            Dim _ab As Addresses = New Addresses(cBase)
            'call to _ab.GetList twice in same class/screen code, identify with absence/presence of param AB_IDs
            Return _ab.GetList(screen)
        End Function

        'Common_Lib.RealTimeService.ClientScreen.Report_Collection_Box_Voucher
        'Shifted
        Public Function GetAddressList(ByVal screen As ClientScreen, ByVal AB_IDs As String) As DataTable
            Dim _ab As Addresses = New Addresses(cBase)
            'call to _ab.GetList twice in same class/screen code, identify with absence/presence of param AB_IDs
            Dim Param As Parameter_Addresses_GetList_Common = New Parameter_Addresses_GetList_Common()
            Param.AB_IDs = AB_IDs
            Return _ab.GetList(screen, Param)
        End Function

        ''' <summary>
        ''' Get Addresses, Shifted
        ''' </summary>
        ''' <param name="ABID"></param>
        ''' <param name="Locked"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetAddresses</remarks>
        Public Function GetAddresses(ByVal ABID As String, ByVal Locked As Boolean) As DataTable
            Dim Param As Param_ReportsAll_GetAddresses = New Param_ReportsAll_GetAddresses()
            Param.ABID = ABID
            Param.Locked = Locked
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetAddresses, ClientScreen.Report_Gift, Param)
        End Function

        ''' <summary>
        ''' GetTSummaryList, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="IsReceipt"></param>
        ''' <param name="FrDate"></param>
        ''' <param name="ToDate"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetTSummaryList</remarks>
        Public Function GetTSummaryList(ByVal screen As ClientScreen, ByVal IsReceipt As Boolean, ByVal FrDate As DateTime, ByVal ToDate As DateTime) As DataTable
            Dim Param As Param_ReportsAll_GetTSummaryList = New Param_ReportsAll_GetTSummaryList()
            Param.FrDate = FrDate
            Param.IsReceipt = IsReceipt
            Param.ToDate = ToDate
            Dim Dt As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetTSummaryList, screen, Param)
            Dt.TableName = IIf(IsReceipt, "RECEIPTS", "PAYMENTS") ' 
            Return Dt
        End Function

        'Public Function GetTSummaryList_SQL(ByVal screen As ClientScreen, ByVal IsReceipt As Boolean, ByVal FrDate As DateTime, ByVal ToDate As DateTime) As DataTable
        '    Dim PaymentQuery As String = "SELECT " & _
        '                                    "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.ItemFinal END AS ITEM, " & _
        '                                    "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.TYPES END AS IType," & _
        '                                    "CASE WHEN B.DontUse IS NULL THEN '' ELSE CAST(B.Amount AS VARCHAR) END AS IAmount," & _
        '                                    "CASE WHEN B.DontUse IS NULL THEN CAST(B.GroupSum AS VARCHAR) ELSE '' END AS IGroupSum , " & _
        '                                    "CASE WHEN B.DontUse IS NULL AND B.Groups IS NULL THEN -1 ELSE CCODE END AS CC " & _
        '                                    "FROM " & _
        '                                    "(SELECT DISTINCT  " & _
        '                                    "A.Groups,A.Item AS DontUse," & _
        '                                    "CASE WHEN A.ITEM IS NULL THEN A.Groups ELSE A.item END AS ItemFinal, " & _
        '                                    "MAX(A.TYPES) AS TYPES, MAX(A.AMOUNT) AS AMOUNT, " & _
        '                                    "SUM(A.Amount) AS GroupSum , MAX(CCODE) AS CCODE " & _
        '                                    "FROM " & _
        '                                    "(SELECT 0 AS SrNo, SG_NAME AS 'Groups'," & _
        '                                    "DR_AI.LED_NAME AS Item, " & _
        '                                    "'PAYMENTS' AS 'TYPES',  " & _
        '                                    "SUM(TR_AMOUNT) AS Amount ,  " & _
        '                                    "CASE WHEN max(dr_ai.LED_TYPE) = 'LIABILITY' THEN 3 WHEN max(dr_ai.LED_TYPE) = 'INCOME' THEN 2 WHEN max(dr_ai.LED_TYPE) = 'ASSET' THEN 1 WHEN max(dr_ai.LED_TYPE) = 'EXPENSE' THEN 0  END AS CCODE  " & _
        '                                    "FROM transaction_info AS ti  " & _
        '                                    "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
        '                                    "LEFT OUTER JOIN acc_ledger_info AS dr_ai ON ti.tr_dr_led_id = dr_ai.led_id  " & _
        '                                    "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = dr_ai.led_sg_id  " & _
        '                                    "WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID = '" & cBase._open_Cen_ID & "' AND TR_CODE NOT IN (5, 6, 1, 2) AND ITEM_TRANS_TYPE = 'DEBIT'  " & _
        '                                    "AND (CAST(TR_DATE AS DATE) BETWEEN '" & FrDate.ToString(cBase._Server_Date_Format_Short) & "' AND '" & ToDate.ToString(cBase._Server_Date_Format_Short) & "') " & _
        '                                    "GROUP BY LED_NAME, ITEM_TRANS_TYPE,SG_NAME  " & _
        '                                    "UNION ALL " & _
        '                                    "SELECT  " & _
        '                                    "0 AS SrNo,SG_NAME AS 'Groups',item_name+ ' BY '+ TR_MODE AS Item, " & _
        '                                    "'PAYMENTS' AS 'TYPE',SUM(TR_AMOUNT) AS Amount , " & _
        '                                    "CASE WHEN max(dr_ai.LED_TYPE) = 'LIABILITY' THEN 3 WHEN max(dr_ai.LED_TYPE) = 'INCOME' THEN 2 WHEN max(dr_ai.LED_TYPE) = 'ASSET' THEN 1 WHEN max(dr_ai.LED_TYPE) = 'EXPENSE' THEN 0  END AS CCODE  " & _
        '                                    "FROM transaction_info AS ti  " & _
        '                                    "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
        '                                    "LEFT OUTER JOIN acc_ledger_info AS dr_ai ON ti.tr_dr_led_id = dr_ai.led_id  " & _
        '                                    "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = dr_ai.led_sg_id  " & _
        '                                    "WHERE (ti.REC_STATUS IN (0,1,2)) AND TR_CEN_ID = '" & cBase._open_Cen_ID & "' AND TR_CODE IN (5, 6) AND ITEM_TRANS_TYPE = 'DEBIT'  " & _
        '                                    "AND (CAST(TR_DATE AS DATE) BETWEEN '" & FrDate.ToString(cBase._Server_Date_Format_Short) & "' AND '" & ToDate.ToString(cBase._Server_Date_Format_Short) & "') " & _
        '                                    "GROUP BY item_name,ITEM_TRANS_TYPE,SG_NAME,TR_MODE " & _
        '                                    ")AS A  " & _
        '                                    "GROUP BY A.Groups,A.ITEM WITH ROLLUP) AS B  " & _
        '                                    "ORDER BY cc, b.Groups,b.dontuse "
        '    Dim ReceiptQuery As String = "SELECT " & _
        '                                  "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.ItemFinal END AS ITEM, " & _
        '                                  "CASE WHEN B.DontUse IS NULL THEN '' ELSE B.TYPES END AS IType, " & _
        '                                  "CASE WHEN B.DontUse IS NULL THEN '' ELSE CAST(B.Amount AS VARCHAR) END AS IAmount, " & _
        '                                  "CASE WHEN B.DontUse IS NULL THEN CAST( B.GroupSum AS VARCHAR) ELSE '' END AS IGroupSum ,  " & _
        '                                  "CASE WHEN B.DontUse IS NULL AND B.GROUPS IS NULL THEN -1 ELSE CCODE END AS CC " & _
        '                                  "FROM " & _
        '                                  "(SELECT DISTINCT  " & _
        '                                  "A.GROUPS,A.Item AS DontUse,	 " & _
        '                                  "CASE WHEN A.ITEM IS NULL THEN A.GROUPS ELSE A.item END AS ItemFinal, " & _
        '                                  "MAX(A.TYPES) AS TYPES, MAX(A.AMOUNT) AS AMOUNT, " & _
        '                                  "SUM(A.Amount) AS GroupSum , MAX(CCODE) AS CCODE " & _
        '                                  "FROM " & _
        '                                  "(SELECT 0 AS SrNo,SG_NAME AS 'GROUPS', " & _
        '                                  "CR_AI.LED_NAME AS Item, " & _
        '                                  "'RECEIPTS' AS 'TYPES', SUM(TR_AMOUNT) AS Amount ,  " & _
        '                                  "CASE WHEN MAX(Cr_ai.LED_TYPE) = 'LIABILITY' THEN 2 WHEN MAX(Cr_ai.LED_TYPE) = 'INCOME' THEN 3 WHEN MAX(Cr_ai.LED_TYPE) = 'ASSET' THEN 0 WHEN MAX(Cr_ai.LED_TYPE) = 'EXPENSE' THEN 1  END AS CCODE  " & _
        '                                  "FROM transaction_info AS ti  " & _
        '                                  "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
        '                                  "INNER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
        '                                  "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
        '                                  "WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID = '" & cBase._open_Cen_ID & "' AND TR_CODE NOT IN (5, 6, 1, 2, 11) AND ITEM_TRANS_TYPE <> 'DEBIT'  " & _
        '                                  "AND (CAST(TR_DATE AS DATE) BETWEEN '" & FrDate.ToString(cBase._Server_Date_Format_Short) & "' AND '" & ToDate.ToString(cBase._Server_Date_Format_Short) & "') " & _
        '                                  "GROUP BY LED_NAME, ITEM_TRANS_TYPE,SG_NAME  " & _
        '                                  "UNION ALL " & _
        '                                  "SELECT  " & _
        '                                  "0 AS SrNo,SG_NAME AS 'GROUPS',item_name+ ' BY '+ TR_MODE AS Item, " & _
        '                                  "'RECEIPTS' AS 'TYPE',SUM(TR_AMOUNT) AS Amount , " & _
        '                                  "CASE WHEN MAX(Cr_ai.LED_TYPE) = 'LIABILITY' THEN 2 WHEN MAX(Cr_ai.LED_TYPE) = 'INCOME' THEN 3 WHEN MAX(Cr_ai.LED_TYPE) = 'ASSET' THEN 0 WHEN MAX(Cr_ai.LED_TYPE) = 'EXPENSE' THEN 1  END AS CCODE   " & _
        '                                  "FROM " & _
        '                                  "transaction_info AS ti  " & _
        '                                  "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
        '                                  "LEFT OUTER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
        '                                  "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
        '                                  "WHERE (ti.REC_STATUS IN (0,1,2)) AND TR_CEN_ID = '" & cBase._open_Cen_ID & "' AND TR_CODE IN (5, 6) AND ITEM_TRANS_TYPE <> 'DEBIT'  " & _
        '                                  "AND (CAST(TR_DATE AS DATE) BETWEEN '" & FrDate.ToString(cBase._Server_Date_Format_Short) & "' AND '" & ToDate.ToString(cBase._Server_Date_Format_Short) & "') " & _
        '                                  "GROUP BY item_name,ITEM_TRANS_TYPE,SG_NAME,TR_MODE " & _
        '                                  "UNION ALL  " & _
        '                                  "SELECT 0 AS SrNo,'Sale of Assets' AS 'Group', " & _
        '                                  "CR_AI.LED_NAME AS Item, " & _
        '                                  "'RECEIPTS' AS 'TYPE', SUM(TR_AMOUNT) AS Amount ,  " & _
        '                                  "CASE WHEN MAX(Cr_ai.LED_TYPE) = 'LIABILITY' THEN 2 WHEN MAX(Cr_ai.LED_TYPE) = 'INCOME' THEN 3 WHEN MAX(Cr_ai.LED_TYPE) = 'ASSET' THEN 0 WHEN MAX(Cr_ai.LED_TYPE) = 'EXPENSE' THEN 1  END AS CCODE  " & _
        '                                  "FROM transaction_info AS ti  " & _
        '                                  "INNER JOIN item_info AS ii ON ii.rec_id = ti.tr_item_id  " & _
        '                                  "INNER JOIN acc_ledger_info AS cr_ai ON ti.tr_cr_led_id = cr_ai.led_id  " & _
        '                                  "INNER JOIN acc_secondary_group_info AS sg ON sg.sg_id = cr_ai.led_sg_id  " & _
        '                                  "WHERE ti.REC_STATUS IN (0,1,2) AND TR_CEN_ID = '" & cBase._open_Cen_ID & "' AND TR_CODE IN (11) AND ITEM_TRANS_TYPE <> 'DEBIT' 	 " & _
        '                                  "AND (CAST(TR_DATE AS DATE) BETWEEN '" & FrDate.ToString(cBase._Server_Date_Format_Short) & "' AND '" & ToDate.ToString(cBase._Server_Date_Format_Short) & "') " & _
        '                                  "GROUP BY LED_NAME, ITEM_TRANS_TYPE,SG_NAME  " & _
        '                                  ")AS A  " & _
        '                                  "GROUP BY A.GROUPS,A.ITEM WITH ROLLUP) AS B  " & _
        '                                  "ORDER BY cc, b.GROUPS,b.dontuse "

        '    Dim Dt As DataTable = GetListOfRecords(IIf(IsReceipt, ReceiptQuery, PaymentQuery), "", screen, Tables.TRANSACTION_INFO, Common.ClientDBFolderCode.DATA)
        '    Dt.TableName = IIf(IsReceipt, "RECEIPTS", "PAYMENTS")
        '    Return Dt
        'End Function

        'ClientScreen.Report_Transaction_Summary
        'Shifted
        Public Function GetSavingBankAccounts(ByVal screen As ClientScreen) As DataTable
            Dim _BA As BankAccounts = New BankAccounts(cBase)
            Return _BA.GetList(screen)
        End Function

        'Shifted
        Public Function GetBranches(ByVal BranchIDs As String, ByVal screen As ClientScreen) As DataTable
            Return GetBankBranchesForMultipleIDs(BranchIDs, screen, " B.BI_SHORT_NAME, A.REC_ID AS BB_BRANCH_ID ")
        End Function

        'Shifted
        Public Function GetOpBalance(ByVal screen As ClientScreen) As DataTable
            Return GetOpeningBalance(screen, "BA_ID")
        End Function

        Public Function GetCashOpeningBalance(ByVal screen As ClientScreen) As DataTable
            Return GetCashOpeningBalanceAmount(screen)
        End Function

        ''' <summary>
        ''' Common Function to get Cash Cr , Dr Sum 
        ''' Shifted
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetCashTransSum(ByVal screen As ClientScreen) As DataTable
            Return GetCashTransSumAmount(screen)
        End Function

        ''' <summary>
        ''' Get Cash Bank Trans Sum, Shifted
        ''' </summary>
        ''' <param name="FromDate"></param>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetCashBankTransSum</remarks>
        Public Function GetCashBankTransSum(ByVal FromDate As Date, ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetCashBankTransSum, screen, FromDate)
        End Function

        ''' <summary>
        ''' Common Function to get Item Name and Head, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function GetItemsAndLedger(ByVal screen As ClientScreen, ByVal ItemName As String)
            Dim LocalQuery As String = "SELECT ITEM_NAME AS ITEM, LED_NAME AS HEAD FROM ITEM_INFO AS ii INNER JOIN ACC_LEDGER_INFO AS al ON ii.item_led_id =  al.LED_ID " &
                                    "WHERE UCASE(ITEM_NAME) = UCASE('" & ItemName & "')" 'parameter used is ItemApplicable 
            'ClientScreen.Report_Collection_Box_Voucher
            Dim OnlineParam As Param_GetItemsLedgerCommon = New Param_GetItemsLedgerCommon()
            OnlineParam.ItemApplicable = ItemName
            Return GetItems_Ledger(screen, LocalQuery, OnlineParam)
        End Function

        ''' <summary>
        ''' Get Membership Receipt, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="M_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetMembershipReceipt</remarks>
        Public Function GetMembershipReceipt(ByVal screen As ClientScreen, ByVal M_ID As String) As DataTable
            Dim Param As Param_ReportsAll_GetMembershipReceipt = New Param_ReportsAll_GetMembershipReceipt()
            Param.M_ID = M_ID
            Param.openYearSdt = cBase._open_Year_Sdt
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetMembershipReceipt, screen, Param)
        End Function

        ''' <summary>
        ''' Get Membership Subscription Fee, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="M_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetMembershipSubscriptionFee</remarks>
        Public Function GetMembershipSubscriptionFee(ByVal screen As ClientScreen, ByVal M_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetMembershipSubscriptionFee, screen, M_ID)
        End Function

        ''' <summary>
        ''' Get Membership Receipt Payment, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <param name="M_ID"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetMembershipReceiptPayment</remarks>
        Public Function GetMembershipReceiptPayment(ByVal screen As ClientScreen, ByVal M_ID As String) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetMembershipReceiptPayment, screen, M_ID)
        End Function

        ''' <summary>
        ''' Get Trial Balance, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetTrialBalance</remarks>
        Public Function GetTrialBalance(ByVal screen As ClientScreen, ByVal xFr_Date As Date, ByVal xTo_Date As Date) As DataTable
            Dim Param As Param_GetTrial_Balance = New Param_GetTrial_Balance()
            Param.openYearID = cBase._open_Year_ID
            Param.xFR_Date = xFr_Date
            Param.xTo_Date = xTo_Date
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetTrialBalance, screen, Param)
        End Function
        Public Function GetMembershipReceiptIntitute() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim SPName As String = "[sp_get_membership_receipt_institute_detail]"
            Dim params() As String = {"@CEN_ID"}
            Dim values() As Object = {cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {4}
            Return _RealService.ListFromSP(Tables.INSTITUTE_INFO, SPName, Tables.INSTITUTE_INFO.ToString, params, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_Voucher_Membership))
        End Function

        ''' <summary>
        ''' Get Trial Balance, Shifted
        ''' </summary>
        ''' <param name="screen"></param>
        ''' <returns></returns>
        ''' <remarks>RealServiceFunctions.ReportsAll_GetTrialBalance</remarks>
        Public Function GetReportTrialBalance(ByVal screen As ClientScreen, ByVal xFr_Date As Date, ByVal xTo_Date As Date, Optional Spl_Vouch_Ref As String = "ALL") As List(Of TrialBalanceReport)
            Dim Param As Param_ReportsAll_GetTrialBalReport = New Param_ReportsAll_GetTrialBalReport()
            Param.openYearSdt = cBase._open_Year_Sdt
            Param.FrDate = xFr_Date
            Param.ToDate = xTo_Date
            Param.InsttId = cBase._open_Ins_ID
            Param.ZoneId = cBase._open_Zone_ID
            Param.specialVoucherReference = Spl_Vouch_Ref
            Dim retTable As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.ReportsAll_GetTrialBalanceRaport, screen, Param)
            Dim _TB As List(Of TrialBalanceReport) = New List(Of TrialBalanceReport)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New TrialBalanceReport
                    newdata.LEDGER = row.Field(Of String)("LEDGER")
                    newdata.NATURE = row.Field(Of String)("NATURE")
                    newdata.PRIMARY_TYPE = row.Field(Of String)("PRIMARY_TYPE")
                    newdata.SECONDARY_TYPE = row.Field(Of String)("SECONDARY_TYPE")
                    newdata.Opening_Dr = row.Field(Of Decimal?)("Opening_Dr")
                    newdata.Opening_Cr = row.Field(Of Decimal?)("Opening_Cr")
                    newdata.DR = row.Field(Of Decimal?)("DR")
                    newdata.CR = row.Field(Of Decimal?)("CR")
                    newdata.Closing_Dr = row.Field(Of Decimal?)("Closing_Dr")
                    newdata.Closing_Cr = row.Field(Of Decimal?)("Closing_Cr")
                    newdata.LED_ID = row.Field(Of String)("LED_ID")
                    _TB.Add(newdata)
                Next
            End If
            Return _TB
        End Function
        ''' <summary>
        ''' Fetchs the list of all the SVR references created in HO Module in the current FY. The SVRs enabled for the UID will be filtered later in the Controller.
        ''' </summary>
        ''' <returns>
        ''' Data Table with Columns TASK_NAME, REC_ID, PERMISSION, CEN_ID.
        ''' </returns>
        ''' <remarks> This caller function of the service is used in the voucher screens also to fetch the list of the SVR to display as check box list.</remarks>
        Public Function GetSVRList(screenName As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Vouchers_GetSplVoucherRefsFromCenterTasks, screenName)
        End Function

        Public Function GetReportIncomeExpenditure(ByVal screen As ClientScreen, ByVal xFr_Date As Date, ByVal xTo_Date As Date) As List(Of IncomeExpenditureReport)
            Dim Param As Param_ReportsAll_GetTrialBalReport = New Param_ReportsAll_GetTrialBalReport()
            Param.openYearSdt = cBase._open_Year_Sdt
            Param.FrDate = xFr_Date
            Param.ToDate = xTo_Date
            Param.InsttId = cBase._open_Ins_ID
            Param.ZoneId = cBase._open_Zone_ID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.ReportsAll_GetIncomeExpenditureReport, screen, Param)
            Dim _IE As List(Of IncomeExpenditureReport) = New List(Of IncomeExpenditureReport)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New IncomeExpenditureReport
                    newdata.Expense_Particulars = row.Field(Of String)("Expense Particulars")
                    newdata.ExpAmount = row.Field(Of Decimal?)("ExpAmount")
                    newdata.Income_Particulars = row.Field(Of String)("Income Particulars")
                    newdata.IncAmount = row.Field(Of Decimal?)("IncAmount")
                    _IE.Add(newdata)
                Next
            End If
            Return _IE
        End Function

        Public Function GetReportBalanceSheet(ByVal screen As ClientScreen, ByVal xFr_Date As Date, ByVal xTo_Date As Date) As List(Of BalanceSheetReport)
            Dim Param As Param_ReportsAll_GetTrialBalReport = New Param_ReportsAll_GetTrialBalReport()
            Param.openYearSdt = cBase._open_Year_Sdt
            Param.FrDate = xFr_Date
            Param.ToDate = xTo_Date
            Param.InsttId = cBase._open_Ins_ID
            Param.ZoneId = cBase._open_Zone_ID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.ReportsAll_GetBalanceSheetReport, screen, Param)

            Dim _BS As List(Of BalanceSheetReport) = New List(Of BalanceSheetReport)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New BalanceSheetReport
                    newdata.Liability_Details = row.Field(Of String)("Liability Details")
                    newdata.liab_Total = row.Field(Of Decimal?)("liabTotal")
                    newdata.Asset_Details = row.Field(Of String)("Asset Details")
                    newdata.asset_Total = row.Field(Of Decimal?)("assetTotal")
                    _BS.Add(newdata)
                Next
            End If
            Return _BS
        End Function

        Public Function GetPurposeReport(ByVal screen As ClientScreen) As List(Of PurposeReport)
            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.Reports_Purpose, screen)
            Dim _PurposeData As List(Of PurposeReport) = New List(Of PurposeReport)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New PurposeReport
                    newdata.TxnDate = row.Field(Of String)("Date")
                    newdata.Item = row.Field(Of String)("Item")
                    newdata.Ledger = row.Field(Of String)("Ledger")
                    newdata.Purpose = row.Field(Of String)("Purpose")
                    newdata.Nature = row.Field(Of String)("Nature")
                    newdata.Amount = row.Field(Of Decimal?)("Amount")
                    _PurposeData.Add(newdata)
                Next
            End If
            Return _PurposeData
        End Function
        Public Function GetVoucherReferenceReport(ByVal screen As ClientScreen) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Reports_VoucherReference, screen)
            'Dim _PurposeData As List(Of PurposeReport) = New List(Of PurposeReport)
            'If (Not (retTable) Is Nothing) Then
            '    For Each row As DataRow In retTable.Rows
            '        Dim newdata = New PurposeReport
            '        newdata.TxnDate = row.Field(Of String)("Date")
            '        newdata.Item = row.Field(Of String)("Item")
            '        newdata.Ledger = row.Field(Of String)("Ledger")
            '        newdata.Purpose = row.Field(Of String)("Purpose")
            '        newdata.Nature = row.Field(Of String)("Nature")
            '        newdata.Amount = row.Field(Of Decimal?)("Amount")
            '        _PurposeData.Add(newdata)
            '    Next
            'End If
            'Return _PurposeData
        End Function

        Public Function GetTelephoneBillDetails(ByVal screen As ClientScreen, ByVal TP_ID As String, FrDate As DateTime, ToDate As DateTime) As DataTable
            Dim Param As Param_ReportsAll_GetTelephoneBill = New Param_ReportsAll_GetTelephoneBill
            Param.TP_ID = TP_ID
            Param.FromDate = FrDate
            Param.ToDate = ToDate
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.ReportsAll_GetTelephoneBillDetails, screen, Param)
        End Function

        Public Function GetInsurancePolicyDetails(YearID As Integer) As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Report_GetInsurancePolicyDetails, ClientScreen.Report_InsuranceLetter, YearID)
        End Function

        Public Function GetInsuranceLetterDetails(YearId As Integer) As DataTable
            Dim param As Param_GetInsuranceLetterData = New Param_GetInsuranceLetterData
            param.YearID = YearId
            param.BK_PAD_NO = cBase._open_PAD_No_Main
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Report_GetInsuranceLetterData, ClientScreen.Report_InsuranceLetter, param).Select("APPLICABLE = 'YES'").CopyToDataTable()
        End Function

        Public Function GetConsumableBreakUpReport() As DataTable
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Reports_GetConsumableBreakUpReport, ClientScreen.Report_ConsumableValue_Breakup, cBase._open_PAD_No_Main)
        End Function

        Public Function GetAssetInsuranceBreakUpReport() As List(Of AssetInsuranceBreakUpReport)
            Dim param As Param_AssetInsuranceBreakUpReport = New Param_AssetInsuranceBreakUpReport
            param.openBkPadNo = cBase._open_PAD_No_Main
            param.prev_year_id = cBase._prev_Unaudited_YearID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.Reports_GetAssetInsuranceBreakUpReport, ClientScreen.Report_AssetInsurance_Breakup, param)
            Dim _AssetData As List(Of AssetInsuranceBreakUpReport) = New List(Of AssetInsuranceBreakUpReport)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New AssetInsuranceBreakUpReport
                    newdata.Complex = row.Field(Of String)("Complex")
                    newdata.Item = row.Field(Of String)("Item")
                    newdata.Asset_Insurance_Value = row.Field(Of Decimal?)("Asset Insurance Value")
                    newdata.Location_Name = row.Field(Of String)("Location Name")
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.TYPE = row.Field(Of String)("TYPE")
                    newdata.UID = row.Field(Of String)("UID")
                    _AssetData.Add(newdata)
                Next
            End If
            Return _AssetData
        End Function

        Public Function GetInsuredYears() As DataTable
            Return GetDataListOfRecords(RealTimeService.RealServiceFunctions.Report_GetInsuredYears, ClientScreen.Report_InsuranceLetter)
        End Function

        Public Function ShowInsuranceLetter() As Boolean
            Return GetSingleValue_Data(RealServiceFunctions.ReportsAll_ShowInsuranceLetter, ClientScreen.Report_InsuranceLetter)
        End Function

        Public Function GetWIPReport() As List(Of WIPReport)
            Dim Param As Param_ReportsAll_GetConstructionExpensesList = New Param_ReportsAll_GetConstructionExpensesList()
            Param.Fr_Date = cBase._open_Year_Sdt
            Param.To_Date = cBase._open_Year_Edt
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Reports_GetWIPReport, ClientScreen.Report_WIPInfo, Param)
            Dim _WIPData As List(Of WIPReport) = New List(Of WIPReport)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New WIPReport
                    newdata.MonthNo = row.Field(Of Integer)("MonthNo")
                    newdata.Month = row.Field(Of String)("Month")
                    newdata.Amt = row.Field(Of Decimal?)("Amt")
                    newdata.Items = row.Field(Of String)("Items")
                    newdata._Property = row.Field(Of String)("Property")
                    _WIPData.Add(newdata)
                Next
            End If
            Return _WIPData
        End Function

        Public Function GetConstructionReport(ByVal Fr_Date As Date, ByVal To_Date As Date, ByVal screen As ClientScreen) As List(Of ConstructionReport)
            Dim Param As Param_ReportsAll_GetConstructionExpensesList = New Param_ReportsAll_GetConstructionExpensesList()
            Param.Fr_Date = Fr_Date
            Param.To_Date = To_Date
            Dim retTable As DataTable = GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.ReportsAll_GetConstructionList, ClientScreen.Report_Construction_List, Param)
            Dim _ConstData As List(Of ConstructionReport) = New List(Of ConstructionReport)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New ConstructionReport
                    newdata.MonthNo = row.Field(Of Integer)("MonthNo")
                    newdata.Month = row.Field(Of String)("Month")
                    newdata.Amt = row.Field(Of Decimal?)("Amt")
                    newdata.Items = row.Field(Of String)("Items")
                    newdata._Property = row.Field(Of String)("Property")
                    _ConstData.Add(newdata)
                Next
            End If
            Return _ConstData
        End Function

        Public Function GetAssetMovementLogs(ByVal Screen As Common_Lib.RealTimeService.ClientScreen, Optional Asset_Rec_ID As String = "") As List(Of AssetLocationLogs)
            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.AssetLocations_Get_Asset_Movement_Logs, Screen, Asset_Rec_ID)
            Dim _MovtLogs As List(Of AssetLocationLogs) = New List(Of AssetLocationLogs)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New AssetLocationLogs
                    newdata.Transfer_On = row.Field(Of DateTime)("Transfer On")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Item = row.Field(Of String)("Item")
                    newdata.Qty = row.Field(Of Decimal)("Qty")
                    newdata.Transferred_From = row.Field(Of String)("Transferred From")
                    newdata.Transferred_To = row.Field(Of String)("Transferred To")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    _MovtLogs.Add(newdata)
                Next
            End If
            Return _MovtLogs
        End Function

        Public Function GetMagazineReceipt(ByVal screen As ClientScreen, ByVal M_ID As String) As DataTable
            Dim param As New Param_GetMagazineReceipt()
            param.Prev_Year_ID = cBase._prev_Unaudited_YearID
            param.Tr_m_ID = M_ID
            Return GetListOfRecordsBySP(RealTimeService.RealServiceFunctions.ReportsAll_GetMagazineReceipt, screen, param)
        End Function

        Public Function UpdateClearingDate(inparam As Param_UpdateClearingDate) As Boolean
            Return UpdateRecord(RealServiceFunctions.Report_UpdateClearingDate, ClientScreen.Report_Daily_Balances, inparam)
        End Function

        Public Function GlobalAddressSearchList(SearchMode As String, SearchBy As String, SearchText As String) As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@Search_Type", "@Search_Field", "@Search_FieldValue", "@Login_BKPAD_NO", "@Year_ID"}
            Dim values() As Object = {SearchMode, SearchBy, SearchText, cBase._open_PAD_No_Main, cBase._open_Year_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.String, DbType.String, DbType.String, DbType.String, DbType.Int32}
            Dim lengths() As Integer = {225, 225, 225, 225, 8}
            Return _RealService.ListFromSP(Tables.SO_CENTER_AUDIT_STATS, "[sp_get_Address_Book_Search]", Tables.SO_CENTER_AUDIT_STATS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        Public Function UserRightsReportList() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@CENID"}
            Dim values() As Object = {cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32}
            Dim lengths() As Integer = {8}
            Return _RealService.ListFromSP(Tables.SO_CENTER_AUDIT_STATS, "[sp_rpt_User_Rights]", Tables.SO_CENTER_AUDIT_STATS.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Account_CashbookAuditor))
        End Function
        Public Function TDS_Applicability_Report() As DataTable
            Dim _RealService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@YEARID", "@INS_ID", "@CENID"}
            Dim values() As Object = {cBase._open_Year_ID, cBase._open_Ins_ID, cBase._open_Cen_ID}
            Dim dbTypes() As System.Data.DbType = {DbType.Int32, DbType.String, DbType.Int32}
            Dim lengths() As Integer = {8, 10, 8}
            Return _RealService.ListFromSP(Tables.TRANSACTION_D_TDS_INFO, "[rpt_TDS_Applicability]", Tables.TRANSACTION_D_TDS_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Report_TDS_Applicability))
        End Function
        Public Function MultiBankCashBookReport(ByVal Param As Param_Vouchers_GetListWithMultipleParams) As DataTable
            Dim dbService As RealTimeService.ConnectOneWS = NewRealService(cBase)
            Dim paramters As String() = {"@FROM_DATE", "@TO_DATE", "@CENID", "@YEARID", "@Advanced_Filter_Category", "@Advanced_Filter_Ref_ID", "@UserID", "@Show_Vouch_Status", "@Show_Attachment_Status", "@SHOW_BANK_COLUMNS"}
            Dim values() As Object = {Param.FromDate, Param.ToDate, cBase._open_Cen_ID, cBase._open_Year_ID, Param.Advanced_Filter_Category, Param.Advanced_Filter_Ref_ID, cBase._open_User_ID, cBase._prefer_show_vouching_indicator, cBase._prefer_show_attachment_indicator, False}
            Dim dbTypes() As System.Data.DbType = {DbType.DateTime, DbType.DateTime, DbType.Int32, DbType.Int32, DbType.String, DbType.String, DbType.String, DbType.Boolean, DbType.Boolean, DbType.Boolean}
            Dim lengths() As Integer = {20, 20, 4, 4, 8000, 36, 255, 1, 1, 1}
            Return dbService.ListFromSP(Tables.TRANSACTION_INFO, "get_Cashbook_ListingDynamic", Tables.TRANSACTION_INFO.ToString(), paramters, values, dbTypes, lengths, GetBaseParams(ClientScreen.Accounts_CashBook))
        End Function
    End Class

#End Region
End Class

