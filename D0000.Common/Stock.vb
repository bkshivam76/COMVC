Imports Common_Lib.RealTimeService

Partial Public Class DbOperations
    <Serializable>
    Public Class Return_GetDocumentsGridData
        Public Property Sr_No As Int64
        Public Property Document_Name As String
        Public Property Document_Type As String
        Public Property File_Name As String
        Public Property Remarks As String
        Public Property ID As String
        Public Property Added_On As DateTime
        Public Property Added_By As String
        Public Property Applicable_From As DateTime?
        Public Property Applicable_To As DateTime?
        Public Property File_Array As Byte()
        Public Property Document_Name_ID As String
    End Class
#Region "--Stock Screens--"
    <Serializable>
    Public Class StockProfile
        Inherits SharedVariables

#Region "Param Classes"
        ''' <summary>
        ''' Return class for GetProfiledata()
        ''' </summary>
        ''' 
        <Serializable>
        Public Class Return_GetProfiledata
            Public Store_Name As String
            Public Item_Name As String
            Public Serial_Lot_No As String
            Public Item_Type As String
            Public Item_Code As String
            Public Make As String
            Public Model As String
            Public Warranty As String
            Public Unit As String
            Public Opening_Qty As Decimal
            Public Opening_Value As Decimal?
            Public Date_of_Purchase As DateTime?
            Public Location As String
            Public Current_Qty As Decimal?
            Public Current_Value As Decimal?
            Public Remarks As String
            Public REC_ADD_ON As DateTime
            Public REC_ADD_BY As String
            Public REC_EDIT_ON As DateTime
            Public REC_EDIT_BY As String
            Public REC_STATUS As String
            Public REC_STATUS_ON As DateTime
            Public REC_STATUS_BY As String
            Public REC_ID As Integer
            Public Store_ID As Integer
            Public Stock_TR_ID As Integer?
            Public YEAR_ID As Integer
            Public Proj_ID As Integer?
            Public Proj_Name As String
        End Class
        <Serializable>
        Public Class Return_GetLocations
            Public Location_Name As String
            Public Loc_Id As String
            Public Matched_Type As String
            Public Matched_Name As String
            Public Matched_Instt As String
        End Class
        <Serializable>
        Public Class Return_GetUnits
            Public Unit As String
            Public Unit_ID As String
        End Class
        <Serializable>
        Public Class Return_GetStockItems
            Public Property Stock_Item_Name As String
            Public Property Item_Category As String
            Public Property Item_Type As String
            Public Property Item_Code As String
            Public Property Head As String
            Public Property Unit As String
            Public Property UnitID As String
            Public Property Item_ID As Integer
            ''' <summary>
            ''' this contains comma separated Store IDs mapped to the item. we need to check the current store ID against it, and hence show mapping check accordingly
            ''' </summary>
            Public Property StoreIDs_Mapped As String
        End Class
        <Serializable>
        Public Class Return_GetStockUsage
            Public Screen As String
            Public DateOfUsage As DateTime
            Public QtyUsed As Decimal
            Public Party_Dept_Involved As String
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Return_Get_Stocks_Listing
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Category As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Lot_Serial_No As String
            Public Property Store As String
            Public Property Location As String
            Public Property Org_Qty As Decimal
            Public Property Curr_Qty As Decimal
            Public Property Org_Cost As Decimal
            Public Property Curr_Cost As Decimal
            Public Property PurchaseDate As DateTime?
            Public Property ID As Int32
            Public Property ItemID As Int32
            Public Property Stock_Unit_ID As String
            Public Property Unit As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        ''' <summary>
        ''' GetProfiledata without param
        ''' </summary>
        ''' <returns></returns>
        Public Function GetProfiledata() As List(Of Return_GetProfiledata)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_GetProfiledata, ClientScreen.Profile_Stock)
            Dim _Stock_Profile_data As List(Of Return_GetProfiledata) = New List(Of Return_GetProfiledata)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetProfiledata
                    newdata.YEAR_ID = row.Field(Of Integer)("YEAR_ID")
                    newdata.Store_Name = row.Field(Of String)("Store Name")
                    newdata.Item_Name = row.Field(Of String)("Item Name")
                    newdata.Serial_Lot_No = row.Field(Of String)("Serial Lot No")
                    newdata.Item_Type = row.Field(Of String)("Item Type")
                    newdata.Item_Code = row.Field(Of String)("Item Code")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Warranty = row.Field(Of String)("Warranty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Opening_Qty = row.Field(Of Decimal)("Opening Qty")
                    newdata.Opening_Value = row.Field(Of Decimal?)("Opening Value")
                    newdata.Date_of_Purchase = row.Field(Of DateTime?)("Date of Purchase")
                    newdata.Location = row.Field(Of String)("Location")
                    newdata.Current_Qty = row.Field(Of Decimal?)("Curr Qty")
                    newdata.Current_Value = row.Field(Of Decimal?)("Curr Value")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                    newdata.REC_STATUS = row.Field(Of Integer)("REC_STATUS").ToString
                    newdata.REC_STATUS_ON = row.Field(Of DateTime)("REC_STATUS_ON")
                    newdata.REC_STATUS_BY = row.Field(Of String)("REC_STATUS_BY")
                    newdata.REC_ID = row.Field(Of Integer)("REC_ID")
                    newdata.Stock_TR_ID = row.Field(Of Integer?)("Stock_TR_ID")
                    newdata.Store_ID = row.Field(Of Integer)("Store_ID")
                    newdata.Proj_Name = row.Field(Of String)("Proj_Name")
                    newdata.Proj_ID = row.Field(Of Integer?)("Proj_ID")
                    _Stock_Profile_data.Add(newdata)
                Next
            End If
            Return _Stock_Profile_data
        End Function
        ''' <summary>
        ''' GetProfiledata with stockID
        ''' </summary>
        ''' <param name="StockID"></param>
        ''' <returns></returns>
        Public Function GetProfiledata(StockID As Int32) As List(Of Return_GetProfiledata)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_GetProfiledata, ClientScreen.Profile_Stock, StockID)
            Dim _Stock_Profile_data As List(Of Return_GetProfiledata) = New List(Of Return_GetProfiledata)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetProfiledata
                    newdata.Store_Name = row.Field(Of String)("Store Name")
                    newdata.Item_Name = row.Field(Of String)("Item Name")
                    newdata.Serial_Lot_No = row.Field(Of String)("Serial Lot No")
                    newdata.Item_Type = row.Field(Of String)("Item Type")
                    'newdata.Item_Code = row.Field(Of String)("Item Code")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Warranty = row.Field(Of String)("Warranty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Opening_Qty = row.Field(Of Decimal)("Opening Qty")
                    newdata.Opening_Value = row.Field(Of Decimal?)("Opening Value")
                    newdata.Date_of_Purchase = row.Field(Of DateTime?)("Date of Purchase")
                    newdata.Location = row.Field(Of String)("Location")
                    newdata.Current_Qty = row.Field(Of Decimal?)("Curr Qty")
                    newdata.Current_Value = row.Field(Of Decimal?)("Curr Value")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                    newdata.REC_STATUS = row.Field(Of Integer)("REC_STATUS").ToString
                    newdata.REC_STATUS_ON = row.Field(Of DateTime)("REC_STATUS_ON")
                    newdata.REC_STATUS_BY = row.Field(Of String)("REC_STATUS_BY")
                    newdata.REC_ID = row.Field(Of Integer)("REC_ID")
                    newdata.Store_ID = row.Field(Of Integer)("Store_ID")
                    newdata.Proj_Name = row.Field(Of String)("Proj_Name")
                    newdata.Proj_ID = row.Field(Of Integer?)("Proj_ID")
                    _Stock_Profile_data.Add(newdata)
                Next
            End If
            Return _Stock_Profile_data
        End Function
        Public Function Get_Stocks_Listing(screen As ClientScreen, inparam As RealTimeService.Param_Get_Stocks_Listing) As List(Of Return_Get_Stocks_Listing)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_Get_Stocks_Listing, screen, inparam)
            Dim _Stock_List_data As List(Of Return_Get_Stocks_Listing) = New List(Of Return_Get_Stocks_Listing)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_Get_Stocks_Listing
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Category = row.Field(Of String)("Item_Category")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Lot_Serial_No = row.Field(Of String)("Lot_Serial_No")
                    newdata.Store = row.Field(Of String)("Store")
                    newdata.Location = row.Field(Of String)("Location")
                    'newdata.Item_Code = row.Field(Of String)("Item Code")
                    newdata.Org_Qty = row.Field(Of Decimal)("Org_Qty")
                    newdata.Curr_Qty = row.Field(Of Decimal)("Curr_Qty")
                    newdata.Org_Cost = row.Field(Of Decimal)("Org_Cost")
                    newdata.Curr_Cost = row.Field(Of Decimal)("Curr_Cost")
                    newdata.PurchaseDate = row.Field(Of DateTime?)("PurchaseDate")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.ItemID = row.Field(Of Int32)("ItemID")
                    newdata.Stock_Unit_ID = row.Field(Of String)("UnitID")
                    newdata.Unit = row.Field(Of String)("Unit")
                    _Stock_List_data.Add(newdata)
                Next
            End If
            Return _Stock_List_data
        End Function
        Public Function GetUnits() As List(Of Return_GetUnits)
            Dim retTable As DataTable = GetMisc("UNITS", ClientScreen.Profile_Stock, "Unit", "Unit_ID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _Stock_Profile_GetUnits_data As List(Of Return_GetUnits) = New List(Of Return_GetUnits)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetUnits
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Unit_ID = row.Field(Of String)("Unit_ID")

                    _Stock_Profile_GetUnits_data.Add(newdata)
                Next
            End If
            Return _Stock_Profile_GetUnits_data
        End Function
        Public Function GetStockItems() As List(Of Return_GetStockItems)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_GetStockItems, ClientScreen.Profile_Stock)
            Dim _Stock_Profile_StockItems_data As List(Of Return_GetStockItems) = New List(Of Return_GetStockItems)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStockItems
                    newdata.Stock_Item_Name = row.Field(Of String)("Stock Item Name")
                    newdata.Item_Category = row.Field(Of String)("Item Category")
                    newdata.Item_Type = row.Field(Of String)("Item Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Item_Code = row.Field(Of String)("Item Code")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.Item_ID = row.Field(Of Integer)("ItemID")
                    _Stock_Profile_StockItems_data.Add(newdata)
                Next
            End If
            Return _Stock_Profile_StockItems_data
        End Function
        Public Function GetStockUsage(StockID As Int32, screen As ClientScreen) As List(Of Return_GetStockUsage)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_GetStockUsage, screen, StockID)
            Dim _Stock_Usage_data As List(Of Return_GetStockUsage) = New List(Of Return_GetStockUsage)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStockUsage
                    newdata.Screen = row.Field(Of String)("Screen")
                    newdata.DateOfUsage = row.Field(Of DateTime)("DateOfUsage")
                    newdata.QtyUsed = row.Field(Of Decimal)("QtyUsed")
                    newdata.Party_Dept_Involved = row.Field(Of String)("Party_Dept_Involved")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    _Stock_Usage_data.Add(newdata)
                Next
            End If
            Return _Stock_Usage_data
        End Function
        Public Function IsStockCarriedForward(RecID As Int32, recYearID As Int32) As Boolean
            'Return GetScalarBySP(RealServiceFunctions.StockProfile_IsStockCarriedForward, ClientScreen.Profile_Stock, RecID)
            Return IsRecordCarriedForward(RecID, recYearID, ClientScreen.Profile_Stock, Tables.STOCK_INFO)
        End Function
        ''' <summary>
        ''' GetRecord -Entry which has been updated or locked/unlocked in background, can not be locked 
        ''' </summary>
        ''' <param name="Rec_ID"></param>
        ''' <returns></returns>
        Public Function GetRecord(ByVal Rec_ID As Int32) As DataTable
            Return GetRecordByID(Rec_ID, ClientScreen.Profile_Stock, RealTimeService.Tables.STOCK_INFO, Common.ClientDBFolderCode.DATA)
        End Function
        Public Function AddStockAddition(ByVal InParam As Param_Add_Stock_Addition, Screen As ClientScreen) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.StockProfile_AddStockAddition, Screen, InParam)
        End Function
        Public Function AddStockProfile(ByVal InParam As Param_Add_StockProfile) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.StockProfile_AddStockProfile, ClientScreen.Profile_Stock, InParam)
        End Function
        Public Function UpdateStockProject(UpParam As Param_Update_StockProject) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockProfile_UpdateStockProject, ClientScreen.Profile_Stock, UpParam)
        End Function
        Public Function UpdateStockLocation(UpParam As Param_Update_StockLocation) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockProfile_UpdateStockLocation, ClientScreen.Profile_Stock, UpParam)
        End Function
        Public Function UpdateStockProfile(UpParam As Param_Update_StockProfile) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockProfile_UpdateStockProfile, ClientScreen.Profile_Stock, UpParam)
        End Function

        Public Function DeleteStockProfile(ByVal StockID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockProfile_DeleteStockProfile, ClientScreen.Profile_Stock, StockID)

        End Function
        Public Function GetLocations(Optional Store_Dept_ID As Int32 = 0, Optional PassCenID As Boolean = True) As List(Of Return_GetLocations)
            Dim retTable As DataTable = cBase._AssetLocDBOps.GetStockLocationList(ClientScreen.Profile_Stock, Nothing, Nothing, Store_Dept_ID, "", "", PassCenID)
            Dim _Locations_data As List(Of Return_GetLocations) = New List(Of Return_GetLocations)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetLocations
                    newdata.Location_Name = row.Field(Of String)("Location Name")
                    newdata.Loc_Id = row.Field(Of String)("AL_ID")
                    newdata.Matched_Type = row.Field(Of String)("Matched Type")
                    newdata.Matched_Name = row.Field(Of String)("Matched Name")
                    newdata.Matched_Instt = row.Field(Of String)("Matched Instt.")
                    _Locations_data.Add(newdata)
                Next
            End If
            Return _Locations_data
        End Function
        ''' <summary>
        ''' GetStockDuplication-Same lot no & make & model with same item name should not have been used elsewhere 
        ''' </summary>
        ''' <param name="YearId"></param>
        ''' <returns></returns>
        Public Function GetStockDuplication(StockItemID As String, make As String, model As String, serial_lot_no As String) As Boolean
            Dim upParam As New Param_Get_StockDuplication
            upParam.StockItemID = StockItemID
            upParam.Make = make
            upParam.Model = model
            upParam.Lot_Serial_No = serial_lot_no
            Return GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockProfile_GetStockDuplication, ClientScreen.Profile_Stock, upParam)
        End Function

        Public Overloads Function MarkAsLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsLocked(Rec_Id, Tables.STOCK_INFO, ClientScreen.Profile_Stock)
            Return Locked
        End Function

        Public Overloads Function MarkAsUnLocked(ByVal Rec_Id As String) As Boolean
            Dim Locked As Boolean = MyBase.MarkAsComplete(Rec_Id, Tables.STOCK_INFO, ClientScreen.Profile_Stock)
            Return Locked
        End Function
    End Class
    <Serializable>
    Public Class StockTransferOrders
        Inherits SharedVariables
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        '''GetTransferOrders- Stock Entry against which Asset Transfer / Transfer Order has been posted can not be updated
        ''' </summary>
        ''' <param name="StockID"></param>
        ''' <returns></returns>
        Public Function GetTransferOrders(StockID As Int32) As DataTable
            Return GetListOfRecordsBySP(RealServiceFunctions.StockTransferOrders_GetTransferOrders, ClientScreen.Profile_Stock, StockID)
        End Function
    End Class
    <Serializable>
    Public Class StockUserOrder
        Inherits SharedVariables
#Region "Return Classes"
        <Serializable>
        Public Class Return_GetRegister
            Public main_Register As List(Of Return_GetRegister_MainGrid)
            Public nested_Register As List(Of Return_GetRegister_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetRegister_MainGrid
            Public Property UO_number As String
            Public Property UO_Date As DateTime
            Public Property Initiation_Mode As String
            Public Property Delivery_Status As String
            Public Property Receipt_Status As String
            Public Property Project As String
            Public Property Job As String
            Public Property Complex As String
            Public Property Requestor_Name As String
            Public Property Requestee_Store As String
            Public Property Approval_Required As String
            Public Property UO_Status As String
            Public Property CurrUserRole As String
            Public Property Req_MainDeptID As Int32?
            Public Property Req_SubDeptID As Int32?
            Public Property StoreID As Int32
            Public Property JobID As Int32?
            Public Property Add_by As String
            Public Property Add_Date As DateTime
            Public Property Edit_By As String
            Public Property Edit_Date As DateTime
            Public Property ID As Integer
            Public Property UORR_Mapped As Int32?
            Public Property ProjID As Int32?
        End Class
        <Serializable>
        Public Class Return_GetRegister_NestedGrid
            Public Property Item_Name As String
            Public Property Make As String
            Public Property Model As String
            Public Property Serial_No_Lot_No As String
            Public Property Unit As String
            Public Property Requested_Qty As Decimal
            Public Property Approved_Qty As Decimal?
            Public Property Delivered_Qty As Decimal?
            Public Property Returned_Qty As Decimal?
            Public Property Penalty_Charged As Decimal
            Public Property Shipping_Location As String
            Public Property Add_by As String
            Public Property Add_Date As DateTime
            Public Property Edit_By As String
            Public Property Edit_Date As DateTime
            Public Property ID As Integer
            Public Property UOID As Integer
            Public Property SubItemID As Integer


        End Class
        <Serializable>
        Public Class Return_Get_RR_Details_ForUOmapping
            Public Property RequisitionDate As DateTime
            Public Property ReqType As String
            Public Property Supplier_Dept As String
            Public Property Purchasedby As String
            Public Property RequisitionAmount As Decimal?
            Public Property Requestor As String
            Public Property Job As String
            Public Property Project As String
            Public Property ItemsRequested As String
            Public Property RR_ID As Integer
            Public Property RR_Status As String
            Public Property UOIDs_Mapped As Boolean

        End Class
        <Serializable>
        Public Class Return_Get_UO_Item_Details_ForNewRR
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Requested_Qty As Decimal
            Public Property Approved_Qty As Decimal?
            Public Property Delivered_Qty As Decimal?
            Public Property Returned_Qty As Decimal?
            Public Property Supplier As String
            Public Property Unit As String
            Public Property Destination_Location As String
            Public Property RRI_Priority As String
            Public Property Rate As Decimal?
            Public Property Rate_after_Discount As Decimal?
            Public Property Tax As Decimal?
            Public Property TotalTaxPercent As Decimal?
            Public Property Discount As Decimal?
            Public Property Amount As Decimal?
            Public Property Required_Delivery_Date As DateTime
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property Unit_ID As String
            Public Property SubItemID As Int32
            Public Property LocationID As String
            Public Property SupplierID As Int32?
            Public Property UO_ID As Int32?
            Public Property UO_Item_ID As Int32?
            Public Property UO_Dept_ID As Int32?
            Public Property UO_Sub_Dept_ID As Int32?
            Public Property Remarks As String

        End Class
        <Serializable>
        Public Class Return_GetUO_Items
            Public Property Sr As Int64
            Public Property SubItemID As Int32
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Requested_Qty As Decimal
            Public Property Approved_Qty As Decimal?
            Public Property Pending_Qty As Decimal?
            Public Property Already_Delivered_Qty As Decimal?
            Public Property Unit As String
            Public Property UOI_Priority As String
            Public Property Requested_Delivery_Date As DateTime
            Public Property Scheduled_Delivery_Date As DateTime?
            Public Property Partial_Delivery_Allowed As Boolean?
            Public Property Delivery_Location As String
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property Mapped_RRID As Int32?
            Public Property Mapped_RRNo As String
            Public Property UnitID As String
            Public Property Delivery_Location_ID As String
        End Class
        'Public Class Return_GetUODocuments
        '    Inherits Jobs.Return_GetJobDocuments

        'End Class
        <Serializable>
        Public Class Return_GetUORemarks
            Inherits Jobs.Return_GetJobRemarks
        End Class
        <Serializable>
        Public Class Return_GetUOGoodsReceived
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Received_Qty As Decimal
            Public Property Unit As String
            Public Property Lot_No As String
            Public Property Received_Date As DateTime
            Public Property Received_Mode As String
            Public Property Carrier As String
            Public Property Received_Location As String
            Public Property Remarks As String
            Public Property UDS_ID As Int32
            Public Property StockID As Int32
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property DeliveryLocationId As String
            Public Property DeliveryEntryID As Int32?
            Public Property ReturnedEntryID As Int32?
            Public Property ReceivedByID As Int32?
            Public Property Bill_No As String
            Public Property Challan_No As String
            Public Property Original_Stock_Location_ID As String
            Public Property PendingToReturn As Decimal?
            Public Property UOI_ID As Int32

        End Class
        <Serializable>
        Public Class Return_GetUOGoodsReceivedAllPending
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Received_Qty As Decimal
            Public Property Unit As String
            Public Property Lot_No As String
            Public Property Received_Date As DateTime
            Public Property Received_Mode As String
            Public Property Carrier As String
            Public Property Received_Location As String
            Public Property Remarks As String
            Public Property UDS_ID As Int32
            Public Property StockID As Int32
            Public Property ID As Int32?
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property DeliveryLocationId As String
            Public Property DeliveryEntryID As Int32?
            Public Property ReturnedEntryID As Int32?
            Public Property ReceivedByID As Int32?
            Public Property Bill_No As String
            Public Property Challan_No As String
            Public Property Original_Stock_Location_ID As String
            Public Property PendingToReturn As Decimal?
            Public Property UOI_ID As Int32

        End Class
        <Serializable>
        Public Class Return_GetUOGoodsReturned
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Returned_Qty As Decimal
            Public Property Unit As String
            Public Property Lot_No As String
            Public Property Return_Date As DateTime
            Public Property Return_Mode As String
            Public Property Carrier As String
            Public Property Received_Location As String
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property ReturnLocationId As String
            Public Property ItemReceivedID As Int32
            Public Property ReturnedByID As Int32?
            Public Property DeliveredRecID As Int32?
            Public Property ReturnedRecID As Int32?
        End Class

        <Serializable>
        Public Class Return_GetUOGoodsReturnedAllPending
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Returned_Qty As Decimal
            Public Property Unit As String
            Public Property Lot_No As String
            Public Property Return_Date As DateTime
            Public Property Return_Mode As String
            Public Property Carrier As String
            Public Property Received_Location As String
            Public Property Remarks As String
            Public Property ID As Int32?
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property ReturnLocationId As String
            Public Property ItemReceivedID As Int32
            Public Property ReturnedByID As Int32?
            Public Property DeliveredRecID As Int32?
            Public Property ReturnedRecID As Int32?
        End Class

        <Serializable>
        Public Class Return_GetUORetReceivableItems
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Returned_Qty As Decimal
            Public Property Unit As String
            Public Property Lot_No As String
            Public Property Return_Date As DateTime
            Public Property Return_Mode As String
            Public Property Carrier As String
            Public Property Return_Location As String
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property ReturnLocationId As String
            Public Property ItemReceivedID As Int32
            Public Property ReturnedByID As Int32?
            Public Property Del_ID As Int32?
            Public Property Ret_ID As Int32?
            Public Property PendingToReturn As Decimal?
            Public Property UOI_ID As Int32
            Public Property UDS_ID As Int32
            Public Property ItemEntrySource As String
        End Class
        <Serializable>
        Public Class Return_GetUOScrapCreated
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Qty As Decimal
            Public Property Unit As String
            Public Property Rate As Decimal
            Public Property Amount As Decimal
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ItemID As Int32
            Public Property UnitID As String
            Public Property LocationID As String
        End Class
        <Serializable>
        Public Class Return_GetUOGoodsDelivered
            Public main_Register As List(Of Return_GetUOGoodsDelivered_MainGrid)
            Public nested_Register As List(Of Return_GetUOGoodsDelivered_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetUOGoodsDelivered_MainGrid
            Public Property Sr As Int64
            Public Property Item_Name As String

            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String

            Public Property Delivered_Qty As Decimal

            Public Property Required_Delivery_Date As DateTime?
            Public Property Scheduled_Delivery_Date As DateTime?
            Public Property Delivery_Date As DateTime
            Public Property Delivery_Mode As String
            Public Property Carrier As String
            Public Property Delivery_Location As String
            Public Property Remarks As String
            Public Property PenaltyToBeCharged As Decimal?
            Public Property Driver As String
            Public Property Vehicle_No As String
            Public Property SubItem_ID As Int32
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property DeliveryLocationId As String
            Public Property ItemRequestedID As Int32
            Public Property DeliveredByID As Int32?
            Public Property DriverID As Int32?

            Public Property Receiver_Name As String
        End Class


        <Serializable>
        Public Class Return_GetUOGoodsDelivered_NestedGrid
            Public Property UD_ID As Int32
            Public Property ID As Int32
            Public Property Sr As Int64
            Public Property MainSr As Int64
            Public Property UDS_Qty As Decimal
            Public Property StockRecordID As Int32
            Public Property Make As String
            Public Property Model As String
            Public Property Lot_No As String
            Public Property Stk_Location As String
            Public Property SubItem_ID As Int32
            Public Property Unit As String

        End Class
        <Serializable>
        Public Class Return_GetUOGoodsDeliveredAllPending

            Public main_Register As List(Of Return_GetUOGoodsDeliveredAllPending_MainGrid)
            Public nested_Register As List(Of Return_GetUOGoodsDeliveredAllPending_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetUOGoodsDeliveredAllPending_MainGrid
            Public Property Sr As Int64
            Public Property Item_Name As String

            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String

            Public Property Delivered_Qty As Decimal

            Public Property Scheduled_Delivery_Date As DateTime?
            Public Property Delivery_Date As DateTime
            Public Property Delivery_Mode As String
            Public Property Carrier As String
            Public Property Delivery_Location As String
            Public Property Remarks As String
            Public Property PenaltyToBeCharged As Decimal?
            Public Property Driver As String
            Public Property Vehicle_No As String
            Public Property SubItem_ID As Int32
            Public Property ID As Int32?
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property DeliveryLocationId As String
            Public Property ItemRequestedID As Int32
            Public Property DeliveredByID As Int32?
            Public Property DriverID As Int32?

            Public Property Receiver_Name As String
        End Class


        <Serializable>
        Public Class Return_GetUOGoodsDeliveredAllPending_NestedGrid
            Public Property UD_ID As Int32?
            Public Property ID As Int32?
            Public Property Sr As Int64
            Public Property MainSr As Int64
            Public Property UDS_Qty As Decimal
            Public Property StockRecordID As Int32
            Public Property Make As String
            Public Property Model As String
            Public Property Lot_No As String
            Public Property SubItem_ID As Int32
            Public Property Unit As String

        End Class

        <Serializable>
        Public Class Return_GetUOGoodsDeliveredSelected

            Public main_Register As List(Of Return_GetUOGoodsDeliveredSelected_MainGrid)
            Public nested_Register As List(Of Return_GetUOGoodsDeliveredSelected_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetUOGoodsDeliveredSelected_MainGrid
            Public Property Sr As Int64
            Public Property Item_Name As String

            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String

            Public Property Delivered_Qty As Decimal

            Public Property Scheduled_Delivery_Date As DateTime?
            Public Property Delivery_Date As DateTime
            Public Property Delivery_Mode As String
            Public Property Carrier As String
            Public Property Delivery_Location As String
            Public Property Remarks As String
            Public Property PenaltyToBeCharged As Decimal?
            Public Property Driver As String
            Public Property Vehicle_No As String
            Public Property SubItem_ID As Int32
            Public Property ID As Int32?
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property DeliveryLocationId As String
            Public Property ItemRequestedID As Int32
            Public Property DeliveredByID As Int32?
            Public Property DriverID As Int32?

            Public Property Receiver_Name As String
        End Class


        <Serializable>
        Public Class Return_GetUOGoodsDeliveredSelected_NestedGrid
            Public Property UD_ID As Int32?
            Public Property ID As Int32?
            Public Property Sr As Int64
            Public Property MainSr As Int64
            Public Property UDS_Qty As Decimal
            Public Property StockRecordID As Int32
            Public Property Make As String
            Public Property Model As String
            Public Property Lot_No As String
            Public Property SubItem_ID As Int32
            Public Property Unit As String

        End Class
        <Serializable>
        Public Class Return_GetUODeliveredItems
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Delivered_Qty As Decimal

            Public Property Scheduled_Delivery_Date As DateTime?
            Public Property Delivery_Date As DateTime
            Public Property Delivery_Mode As String
            Public Property Carrier As String
            Public Property Delivery_Location As String
            Public Property Remarks As String
            Public Property PenaltyToBeCharged As Decimal?
            Public Property Driver As String
            Public Property Vehicle_No As String
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property DeliveryLocationId As String
            Public Property ItemRequestedID As Int32
            Public Property DeliveredByID As Int32?
            Public Property DriverID As Int32?
            Public Property Receiver_Name As String
            Public Property Lot_No As String
            Public Property Unit_ID As String
            Public Property Unit As String
            Public Property Unreceived_Delivered_Qty As Decimal?
            Public Property Del_ID As Int32?
            Public Property Ret_ID As Int32?
            Public Property UDS_ID As Int32
            Public Property ItemEntrySource As String




        End Class
        <Serializable>
        Public Class Return_GetUOGoodsReturnReceived
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Make As String
            Public Property Model As String
            Public Property Received_Qty As Decimal
            Public Property Unit As String
            Public Property Lot_No As String
            Public Property Received_Date As DateTime
            Public Property Received_Mode As String
            Public Property Carrier As String
            Public Property Received_Location As String
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ModeID As String
            Public Property RecdLocationId As String
            Public Property DeliveryEntryID As Int32?
            Public Property ReturnEntryID As Int32?
            Public Property RecdByID As Int32?
            Public Property StockID As Int32
            Public Property UDS_ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetDeliveryModes
            Public Property Mode As String
            Public Property ModeID As String
        End Class
        <Serializable>
        Public Class Return_GetPersonnels
            Public Property Name As String
            Public Property Type As String
            Public Property Main_Dept As String
            Public Property Sub_Dept As String
            Public Property ID As Integer
            Public Property Mobile_No As String
            Public Property Skill_Type As String
            Public Property Designation As String
        End Class
        <Serializable>
        Public Class Return_GetUOGoodsReceivable
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Receivable_Qty As Decimal
            Public Property Unit As String
            Public Property Lot_No As String
            'Public Property Scheduled_Delivery_Date As DateTime?
            Public Property Delivery_Return_Date As DateTime
            'Public Property Delivery_Mode As String
            Public Property Carrier As String
            'Public Property Delivery_Location As String
            'Public Property Remarks As String
            'Public Property PenaltyToBeCharged As Decimal?
            'Public Property Driver As String
            ' Public Property Vehicle_No As String
            Public Property DeliveredRecID As Int32?
            Public Property ReturnedRecID As Int32?
            'Public Property Added_On As DateTime
            'Public Property Added_By As String
            Public Property ModeID As String
            Public Property DeliveryReturnLocationId As String
            ' Public Property Unreceived_Delivered_Qty As Decimal
            'Public Property ItemRequestedID As Int32
            ' Public Property DeliveredByID As Int32?
            ' Public Property DriverID As Int32?
            ' Public Property StockRecordID As Int32
            'Public Property Receiver_Name As String
        End Class
        <Serializable>
        Public Class Return_GetUODetails
            Public Property UO_number As String
            Public Property InitiationMode As String
            Public Property UO_Date As DateTime
            Public Property JobID As Int32
            Public Property StoreID As Int32
            Public Property RequestorID As Int32
            Public Property UO_Status As String
            Public Property DeliveryStatus As String
            Public Property Req_MainDeptID As Int32?
            Public Property Req_SubDeptID As Int32?
            Public Property ReceivalStatus As String
            Public Property ID As Int32
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property EditedOn As DateTime
            Public Property EditedBy As String
            Public Property ProjID As Int32?
            Public Property UORR_Mapped As Int32?
        End Class
        <Serializable>
        Public Class Return_Get_UO_Goods_Delivery_Stocks
            Public Property UD_UO_ID As Int32
            Public Property Sub_Item_ID As Int32
            Public Property Del_qty As Decimal
            Public Property Avlb_Qty As Decimal
            Public Property Stock_Lot_Serial_no As String
            Public Property Make As String
            Public Property Model As String
            Public Property Stk_Location As String
            Public Property StockUnit As String
            Public Property PurchaseDate As DateTime
            Public Property ID As Int32
            Public Property UD_ID As Int32?
            Public Property UDS_ID As Int32?

        End Class

        <Serializable>
        Public Class Return_Get_CenterDetails_StockAvailability
            Public Property CenterName As String
            Public Property Center_UID As String
            Public Property CENID As Int32

        End Class
        <Serializable>
        Public Class Return_Get_Store_Locations_StockAvailability
            Public Property LocationName As String
            Public Property MatchedType As String
            Public Property MatchedName As String
            Public Property LocID As String

        End Class
        <Serializable>
        Public Class Return_Get_Stock_Availability
            Public Property Stock_Item_Name As String
            Public Property Item_Code As String
            Public Property Make As String
            Public Property Model As String
            Public Property CenterName As String
            Public Property StoreName As String
            Public Property Dept As String
            Public Property AvlbQty As Decimal?
            Public Property ItemID As Integer
            Public Property StoreID As Integer
            Public Property LocationName As String
            Public Property StockID As Integer

        End Class
        <Serializable>
        Public Class Return_GetStoreList_Stock_Availability
            Public Store_Name As String
            Public Dept_Name As String
            Public Sub_Dept_Name As String
            Public Dept_Incharge_Name As String
            Public Store_Incharge_Name As String
            Public Store_Incharge_ID As Integer 'Mantis bug 0000503 fixed

            Public StoreID As Integer
        End Class

#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        ''' <summary>
        '''GetDeliveries - Stock Entry against which User Order has been posted and delivered can not be updated or deleted
        ''' </summary>
        ''' <param name="StockID"></param>
        ''' <returns></returns>
        Public Function GetDeliveries(StockID As Int32) As DataTable
            Return GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetDeliveries, ClientScreen.Profile_Stock, StockID)
        End Function
        Public Function GetRegister(fromDate As DateTime, toDate As DateTime) As Return_GetRegister
            Dim InParam As New Param_GetUserOrderRegister
            InParam.From_Date = fromDate
            InParam.ToDate = toDate
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.UserOrder_GetRegister, ClientScreen.Stock_UO, InParam)

            Dim _main_data As New Return_GetRegister
            Dim _uo_main_data As List(Of Return_GetRegister_MainGrid) = New List(Of Return_GetRegister_MainGrid)
            Dim _uo_nested_data As List(Of Return_GetRegister_NestedGrid) = New List(Of Return_GetRegister_NestedGrid)
            _main_data.main_Register = _uo_main_data
            _main_data.nested_Register = _uo_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetRegister_MainGrid
                    newdata.UO_number = row.Field(Of String)("UO_number")
                    newdata.UO_Date = row.Field(Of DateTime)("UO_Date")
                    newdata.Initiation_Mode = row.Field(Of String)("Initiation_Mode")
                    newdata.Delivery_Status = row.Field(Of String)("Delivery_Status")
                    newdata.Receipt_Status = row.Field(Of String)("Receipt_Status")
                    newdata.Project = row.Field(Of String)("Project")
                    newdata.Job = row.Field(Of String)("Job")
                    newdata.Complex = row.Field(Of String)("Complex")
                    newdata.Requestor_Name = row.Field(Of String)("Requestor_Name")
                    newdata.Requestee_Store = row.Field(Of String)("Requestee_Store")
                    newdata.Approval_Required = row.Field(Of String)("Approval_Required")
                    newdata.UO_Status = row.Field(Of String)("UO_Status")

                    newdata.Add_Date = row.Field(Of DateTime)("Add_Date")
                    newdata.Add_by = row.Field(Of String)("Add_by")
                    newdata.Edit_Date = row.Field(Of DateTime)("Edit_Date")
                    newdata.Edit_By = row.Field(Of String)("Edit_By")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.CurrUserRole = row.Field(Of String)("CurrUserRole")
                    newdata.StoreID = row.Field(Of Int32)("StoreID")
                    newdata.JobID = row.Field(Of Int32?)("JobID")
                    newdata.Req_MainDeptID = row.Field(Of Int32?)("Req_MainDeptID")
                    newdata.Req_SubDeptID = row.Field(Of Int32?)("Req_SubDeptID")
                    newdata.UORR_Mapped = row.Field(Of Int32?)("UORR_Mapped")
                    newdata.ProjID = row.Field(Of Int32?)("ProjID")

                    _uo_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetRegister_NestedGrid
                    subdata.Item_Name = row.Field(Of String)("Item_Name")
                    subdata.Make = row.Field(Of String)("Make")
                    subdata.Model = row.Field(Of String)("Model")
                    subdata.Serial_No_Lot_No = row.Field(Of String)("Serial_No_Lot_No")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.Requested_Qty = row.Field(Of Decimal)("Requested_Qty")
                    subdata.Approved_Qty = row.Field(Of Decimal?)("Approved_Qty")
                    subdata.Delivered_Qty = row.Field(Of Decimal?)("Delivered_Qty")
                    subdata.Returned_Qty = row.Field(Of Decimal?)("Returned_Qty")
                    subdata.Penalty_Charged = row.Field(Of Decimal)("Penalty_Charged")
                    subdata.Shipping_Location = row.Field(Of String)("Shipping_Location")
                    subdata.ID = row.Field(Of Integer)("ID")
                    subdata.UOID = row.Field(Of Integer)("UOID")
                    subdata.Add_Date = row.Field(Of DateTime)("AddedOn")
                    subdata.Add_by = row.Field(Of String)("AddedBy")
                    subdata.Edit_Date = row.Field(Of DateTime)("EditOn")
                    subdata.Edit_By = row.Field(Of String)("EditBy")
                    subdata.SubItemID = row.Field(Of Integer)("SubItemID")
                    _uo_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function GetUO_Items(UOID As Int32, Optional NotFullDeliveredRecordsOnly As Boolean = False, Optional UD_ID As Int32 = Nothing) As List(Of Return_GetUO_Items)
            Dim inparam As New Param_GetUOItems()
            inparam.UOID = UOID
            inparam.NotDeliveredOnly = NotFullDeliveredRecordsOnly
            inparam.UD_ID = UD_ID
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOItems, ClientScreen.Stock_UO, inparam)
            Dim _uo_items As List(Of Return_GetUO_Items) = New List(Of Return_GetUO_Items)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUO_Items
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Requested_Qty = row.Field(Of Decimal)("Requested_Qty")
                    newdata.Approved_Qty = row.Field(Of Decimal?)("Approved_Qty")
                    newdata.Already_Delivered_Qty = row.Field(Of Decimal?)("Already_Delivered_Qty")
                    newdata.Pending_Qty = row.Field(Of Decimal?)("Pending_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.UOI_Priority = row.Field(Of String)("UOI_Priority")
                    newdata.Requested_Delivery_Date = row.Field(Of DateTime)("Requested_Delivery_Date")
                    newdata.Scheduled_Delivery_Date = row.Field(Of DateTime?)("Scheduled_Delivery_Date")
                    newdata.Partial_Delivery_Allowed = row.Field(Of Boolean?)("Partial_Delivery_Allowed")
                    newdata.Delivery_Location = row.Field(Of String)("Delivery_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.Delivery_Location_ID = row.Field(Of String)("Delivery_Location_ID")
                    newdata.SubItemID = row.Field(Of Int32)("SubItem_ID")
                    newdata.Mapped_RRID = row.Field(Of Int32?)("Mapped_RRID")
                    newdata.Mapped_RRNo = row.Field(Of String)("Mapped_RRNo")
                    _uo_items.Add(newdata)
                Next
            End If
            Return _uo_items
        End Function
        Public Function GetUODetails(UOID As Int32) As Return_GetUODetails

            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUODetails, ClientScreen.Stock_UO, UOID)
            Dim newdata = New Return_GetUODetails

            If (Not (retDataTable) Is Nothing) Then
                Dim row As DataRow = retDataTable.Rows(0)

                newdata.UO_number = row.Field(Of String)("UO_number")
                newdata.InitiationMode = row.Field(Of String)("InitiationMode")
                newdata.UO_Date = row.Field(Of DateTime)("UO_Date")
                newdata.JobID = row.Field(Of Int32)("JobID")
                newdata.StoreID = row.Field(Of Int32)("StoreID")
                newdata.RequestorID = row.Field(Of Int32)("RequestorID")
                newdata.UO_Status = row.Field(Of String)("UO_Status")
                newdata.DeliveryStatus = row.Field(Of String)("DeliveryStatus")
                newdata.Req_MainDeptID = row.Field(Of Int32?)("Req_MainDeptID")
                newdata.Req_SubDeptID = row.Field(Of Int32?)("Req_SubDeptID")
                newdata.ReceivalStatus = row.Field(Of String)("ReceivalStatus")
                newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                newdata.EditedBy = row.Field(Of String)("EditedBy")
                newdata.UORR_Mapped = row.Field(Of Int32?)("UORR_Mapped")
                newdata.ID = row.Field(Of Int32)("ID")
                newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                newdata.AddedBy = row.Field(Of String)("AddedBy")
                newdata.ProjID = row.Field(Of Int32?)("ProjID")

            End If
            Return newdata
        End Function

        Public Function GetUODocuments(UOID As Integer) As List(Of Return_GetDocumentsGridData)
            Dim _Docs_Param As New Common_Lib.RealTimeService.Param_GetStockDocuments
            _Docs_Param.RefID = UOID
            _Docs_Param.Screen_Type = "UO"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockDocuments, ClientScreen.Stock_Project, _Docs_Param)
            Dim _Docs_data As List(Of Return_GetDocumentsGridData) = New List(Of Return_GetDocumentsGridData)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDocumentsGridData
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Document_Name = row.Field(Of String)("DOC_NAME")
                    newdata.Document_Type = row.Field(Of String)("DOC_TYPE")
                    newdata.File_Name = row.Field(Of String)("File_Name")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Added_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Applicable_From = row.Field(Of DateTime?)("Applicable_From")
                    newdata.Applicable_To = row.Field(Of DateTime?)("Applicable_To")
                    newdata.Document_Name_ID = row.Field(Of String)("Document_Name_ID")
                    _Docs_data.Add(newdata)
                Next
            End If
            Return _Docs_data
        End Function
        Public Function GetUORemarks(UOID As Integer) As List(Of Return_GetUORemarks)
            Dim _Remarks_Param As New Common_Lib.RealTimeService.Param_GetStockRemarks
            _Remarks_Param.RefID = UOID
            _Remarks_Param.Screen_Type = "UO"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockRemarks, ClientScreen.Stock_Project, _Remarks_Param)
            Dim _Remarks_data As List(Of Return_GetUORemarks) = New List(Of Return_GetUORemarks)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetUORemarks
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.Remarks_By = row.Field(Of String)("REMARKS_BY")
                    newdata.Remarks_By_Designation = row.Field(Of String)("ADD_DESIGNATION")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    _Remarks_data.Add(newdata)
                Next
            End If
            Return _Remarks_data
        End Function

        Public Function GetAttachmentLinkScreen(UOID As Int32, AttachmentID As String) As String
            Dim inparam As New Parameter_GetAttachmentLinkCount()
            inparam.RefRecordID = UOID
            inparam.RefScreen = "UO"
            inparam.AttachmentID = AttachmentID
            Return cBase._Attachments_DBOps.GetAttachmentLinkScreen(inparam)
        End Function
        Public Function GetUOGoodsReturned(UOID As Int32) As List(Of Return_GetUOGoodsReturned)
            ' Dim inparam As New Param_GetUOGoodsReturned()
            'inparam.UOID = UOID
            'inparam.NotReceivedOnly = NotFullReceivedRecordsOnly
            'inparam.IncludeDelivery = IncludeDelivery
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsReturned, ClientScreen.Stock_UO, UOID)
            Dim _uo_items As List(Of Return_GetUOGoodsReturned) = New List(Of Return_GetUOGoodsReturned)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUOGoodsReturned
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Returned_Qty = row.Field(Of Decimal)("Returned_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Return_Date = row.Field(Of DateTime)("Return_Date")
                    newdata.Return_Mode = row.Field(Of String)("Return_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Received_Location = row.Field(Of String)("Return_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.ReturnLocationId = row.Field(Of String)("ReturnLocationId")
                    newdata.ItemReceivedID = row.Field(Of Int32)("ItemReceivedID")
                    newdata.ReturnedByID = row.Field(Of Int32?)("ReturnedByID")

                    _uo_items.Add(newdata)
                Next
            End If
            Return _uo_items
        End Function
        Public Function GetUORetReceivableItems(UOID As Int32, Optional RetRecddID As Int32 = Nothing) As List(Of Return_GetUORetReceivableItems)
            Dim inparam As New Param_GetUORetReceivableItems()
            inparam.UOID = UOID
            inparam.RetRecddID = RetRecddID
            'inparam.NotReceivedOnly = NotFullReceivedRecordsOnly
            'inparam.IncludeDelivery = IncludeDelivery
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUORetReceivableItems, ClientScreen.Stock_UO, inparam)
            Dim _uo_itemsrec As List(Of Return_GetUORetReceivableItems) = New List(Of Return_GetUORetReceivableItems)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUORetReceivableItems
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Returned_Qty = row.Field(Of Decimal)("Returned_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Return_Date = row.Field(Of DateTime)("Return_Date")
                    newdata.Return_Mode = row.Field(Of String)("Return_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Return_Location = row.Field(Of String)("Return_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.ReturnLocationId = row.Field(Of String)("ReturnLocationId")
                    newdata.ItemReceivedID = row.Field(Of Int32)("ItemReceivedID")
                    newdata.ReturnedByID = row.Field(Of Int32?)("ReturnedByID")
                    newdata.Del_ID = row.Field(Of Int32?)("Del_ID")
                    newdata.Ret_ID = row.Field(Of Int32?)("Ret_ID")
                    newdata.PendingToReturn = row.Field(Of Decimal?)("PendingToReturn")
                    newdata.UDS_ID = row.Field(Of Int32?)("UDS_ID")
                    newdata.UOI_ID = row.Field(Of Int32)("UOI_ID")
                    newdata.ItemEntrySource = row.Field(Of String)("EntrySource")
                    _uo_itemsrec.Add(newdata)
                Next
            End If
            Return _uo_itemsrec
        End Function
        Public Function GetUOGoodsReceived(UOID As Int32, Optional NotFullReturnedRecordsOnly As Boolean = False, Optional RetID As Int32 = Nothing) As List(Of Return_GetUOGoodsReceived)
            Dim inparam As New Param_GetUOGoodsReceived()
            inparam.UOID = UOID
            inparam.NotReturnedOnly = NotFullReturnedRecordsOnly
            inparam.RetID = RetID
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsReceived, ClientScreen.Stock_UO, inparam)
            Dim _uo_items As List(Of Return_GetUOGoodsReceived) = New List(Of Return_GetUOGoodsReceived)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUOGoodsReceived
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Received_Qty = row.Field(Of Decimal)("Received_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Received_Date = row.Field(Of DateTime)("Received_Date")
                    newdata.Received_Mode = row.Field(Of String)("Received_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Received_Location = row.Field(Of String)("Received_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.UDS_ID = row.Field(Of Int32)("UDS_ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.DeliveryLocationId = row.Field(Of String)("DeliveryLocationID")
                    newdata.Original_Stock_Location_ID = row.Field(Of String)("Original_Stock_Location_ID")
                    newdata.DeliveryEntryID = row.Field(Of Int32?)("DeliveryEntryID")
                    newdata.ReturnedEntryID = row.Field(Of Int32?)("ReturnedEntryID")
                    newdata.ReceivedByID = row.Field(Of Int32?)("ReceivedByID")
                    newdata.Bill_No = row.Field(Of String)("Bill_No")
                    newdata.Challan_No = row.Field(Of String)("Challan_No")
                    newdata.StockID = row.Field(Of Int32)("StockID")
                    newdata.PendingToReturn = row.Field(Of Decimal?)("PendingToReturn")
                    newdata.UOI_ID = row.Field(Of Int32)("UOI_ID")
                    _uo_items.Add(newdata)
                Next
            End If
            Return _uo_items
        End Function
        Public Function GetUOScrapCreated(UOID As Int32) As List(Of Return_GetUOScrapCreated)
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOScrapCreated, ClientScreen.Stock_UO, UOID)
            Dim _uo_items As List(Of Return_GetUOScrapCreated) = New List(Of Return_GetUOScrapCreated)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUOScrapCreated
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Qty = row.Field(Of Decimal)("Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Rate = row.Field(Of Decimal)("Rate")
                    newdata.Amount = row.Field(Of Decimal)("Amount")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ItemID = row.Field(Of Int32)("ItemID")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.LocationID = row.Field(Of String)("LocationID")
                    _uo_items.Add(newdata)
                Next
            End If
            Return _uo_items
        End Function
        Public Function GetUOGoodsDelivered(UOID As Int32) As Return_GetUOGoodsDelivered
            'Dim inparam As New Param_GetUOGoodsDelivered
            'inparam.UOID = UOID
            'inparam.NotReceivedOnly = NotFullReceivedRecordsOnly
            'inparam.IncludeReturned = IncludeReturned
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsDelivered, ClientScreen.Stock_UO, UOID)
            Dim _uo_items As List(Of Return_GetUOGoodsDelivered) = New List(Of Return_GetUOGoodsDelivered)()

            Dim _delivery_data As New Return_GetUOGoodsDelivered
            Dim _uo_maindelivery_data As List(Of Return_GetUOGoodsDelivered_MainGrid) = New List(Of Return_GetUOGoodsDelivered_MainGrid)
            Dim _uo_nesteddelivery_data As List(Of Return_GetUOGoodsDelivered_NestedGrid) = New List(Of Return_GetUOGoodsDelivered_NestedGrid)
            _delivery_data.main_Register = _uo_maindelivery_data
            _delivery_data.nested_Register = _uo_nesteddelivery_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetUOGoodsDelivered_MainGrid
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    'newdata.Make = row.Field(Of String)("Make")
                    'newdata.Model = row.Field(Of String)("Model")
                    newdata.Delivered_Qty = row.Field(Of Decimal)("Delivered_Qty")
                    ' newdata.Unit = row.Field(Of String)("Unit")
                    ''newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Delivery_Date = row.Field(Of DateTime)("Delivery_Date")

                    newdata.Required_Delivery_Date = row.Field(Of DateTime?)("Required_Delivery_Date")
                    newdata.Scheduled_Delivery_Date = row.Field(Of DateTime?)("Scheduled_Delivery_Date")
                    newdata.Delivery_Mode = row.Field(Of String)("Delivery_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Delivery_Location = row.Field(Of String)("Delivery_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.PenaltyToBeCharged = row.Field(Of Decimal?)("PenaltyToBeCharged")
                    newdata.Driver = row.Field(Of String)("Driver")
                    newdata.Vehicle_No = row.Field(Of String)("Vehicle_No")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.DeliveryLocationId = row.Field(Of String)("DeliveryLocationID")
                    newdata.DeliveredByID = row.Field(Of Int32?)("DeliveredByID")
                    newdata.ItemRequestedID = row.Field(Of Int32)("ItemRequestedID")
                    newdata.DriverID = row.Field(Of Int32?)("DriverID")
                    newdata.SubItem_ID = row.Field(Of Int32)("SubItem_ID")
                    newdata.Receiver_Name = row.Field(Of String)("Receiver_Name")
                    'newdata.Del_ID = row.Field(Of Int32?)("Del_ID")
                    'newdata.Ret_ID = row.Field(Of Int32?)("Ret_ID")
                    'newdata.Unreceived_Delivered_Qty = row.Field(Of Decimal?)("Unreceived_Delivered_Qty")

                    _uo_maindelivery_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetUOGoodsDelivered_NestedGrid
                    subdata.Sr = row.Field(Of Int64)("Sr")
                    subdata.UD_ID = row.Field(Of Int32)("UD_ID")
                    subdata.ID = row.Field(Of Int32)("ID")
                    'subdata.Item_Name = row.Field(Of String)("Item_Name")
                    'subdata.Item_Code = row.Field(Of String)("Item_Code")
                    'subdata.Item_Type = row.Field(Of String)("Item_Type")
                    subdata.UDS_Qty = row.Field(Of Decimal)("UDS_Qty")
                    subdata.SubItem_ID = row.Field(Of Int32)("SubItem_ID")
                    subdata.StockRecordID = row.Field(Of Int32)("StockID")
                    subdata.Make = row.Field(Of String)("Make")
                    subdata.Model = row.Field(Of String)("Model")
                    subdata.Lot_No = row.Field(Of String)("Lot_No")
                    subdata.Stk_Location = row.Field(Of String)("Stk_Location")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.MainSr = row.Field(Of Int64)("MainSr")
                    _uo_nesteddelivery_data.Add(subdata)
                Next
            End If
            Return _delivery_data
        End Function
        Public Function GetUODeliveredItems(UOID As Int32, Optional Recd_ID As Int32 = Nothing) As List(Of Return_GetUODeliveredItems)
            Dim inparam As New Param_GetUODeliveredItems()
            inparam.UOID = UOID
            inparam.Recd_ID = Recd_ID
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_Get_UODeliveredItems, ClientScreen.Stock_UO, inparam)
            Dim _uo_del_items As List(Of Return_GetUODeliveredItems) = New List(Of Return_GetUODeliveredItems)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUODeliveredItems
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Delivered_Qty = row.Field(Of Decimal)("Delivered_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Unit_ID = row.Field(Of String)("Unit_ID")
                    newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Delivery_Date = row.Field(Of DateTime)("Delivery_Date")
                    newdata.Scheduled_Delivery_Date = row.Field(Of DateTime?)("Scheduled_Delivery_Date")
                    newdata.Delivery_Mode = row.Field(Of String)("Delivery_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Delivery_Location = row.Field(Of String)("Delivery_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.PenaltyToBeCharged = row.Field(Of Decimal?)("PenaltyToBeCharged")
                    newdata.Driver = row.Field(Of String)("Driver")
                    newdata.Vehicle_No = row.Field(Of String)("Vehicle_No")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.DeliveryLocationId = row.Field(Of String)("DeliveryLocationID")
                    newdata.DeliveredByID = row.Field(Of Int32?)("DeliveredByID")
                    newdata.ItemRequestedID = row.Field(Of Int32)("ItemRequestedID")
                    newdata.DriverID = row.Field(Of Int32?)("DriverID")
                    newdata.UDS_ID = row.Field(Of Int32)("UDS_ID")
                    newdata.Receiver_Name = row.Field(Of String)("Receiver_Name")
                    newdata.Del_ID = row.Field(Of Int32?)("Del_ID")
                    newdata.Ret_ID = row.Field(Of Int32?)("Ret_ID")
                    newdata.Unreceived_Delivered_Qty = row.Field(Of Decimal?)("Unreceived_Delivered_Qty")
                    newdata.ItemEntrySource = row.Field(Of String)("EntrySource")
                    _uo_del_items.Add(newdata)
                Next
            End If
            Return _uo_del_items
        End Function

        Public Function GetUOGoodsReturnReceived(UOID As Int32) As List(Of Return_GetUOGoodsReturnReceived)
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsReturnReceived, ClientScreen.Stock_UO, UOID)
            Dim _uo_items As List(Of Return_GetUOGoodsReturnReceived) = New List(Of Return_GetUOGoodsReturnReceived)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUOGoodsReturnReceived
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    ' newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Received_Qty = row.Field(Of Decimal)("Received_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Received_Date = row.Field(Of DateTime)("Received_Date")
                    newdata.Received_Mode = row.Field(Of String)("Received_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Received_Location = row.Field(Of String)("Received_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.RecdLocationId = row.Field(Of String)("RecdLocationId")
                    newdata.DeliveryEntryID = row.Field(Of Int32?)("DeliveryEntryID")
                    newdata.ReturnEntryID = row.Field(Of Int32?)("ReturnEntryID")
                    newdata.RecdByID = row.Field(Of Int32?)("RecdByID")
                    newdata.StockID = row.Field(Of Int32)("StockID")
                    newdata.UDS_ID = row.Field(Of Int32)("UDS_ID")
                    _uo_items.Add(newdata)
                Next
            End If
            Return _uo_items
        End Function
        'Public Function GetUOGoodsReceivable(UOID As Int32) As List(Of Return_GetUOGoodsReceivable)
        '    'Dim inparam As New Param_GetUOGoodsDelivered()
        '    'inparam.UOID = UOID
        '    'inparam.NotReceivedOnly = True
        '    Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsDelivered, ClientScreen.Stock_UO, inparam)

        '    Dim inparam_Ret As New Param_GetUOGoodsReturned()
        '    inparam_Ret.UOID = UOID
        '    inparam_Ret.NotReceivedOnly = True
        '    Dim retDataTableReturned As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsReturned, ClientScreen.Stock_UO, inparam_Ret)

        '    Dim _uo_items As List(Of Return_GetUOGoodsReceivable) = New List(Of Return_GetUOGoodsReceivable)()

        '    If (Not (retDataTable) Is Nothing) Then
        '        For Each row As DataRow In retDataTable.Rows
        '            Dim newdata = New Return_GetUOGoodsReceivable
        '            newdata.Item_Name = row.Field(Of String)("Item_Name")
        '            newdata.Item_Code = row.Field(Of String)("Item_Code")
        '            newdata.Item_Type = row.Field(Of String)("Item_Type")
        '            newdata.Head = row.Field(Of String)("Head")
        '            newdata.Make = row.Field(Of String)("Make")
        '            newdata.Model = row.Field(Of String)("Model")
        '            newdata.Receivable_Qty = row.Field(Of Decimal)("Delivered_Qty")
        '            newdata.Unit = row.Field(Of String)("Unit")
        '            newdata.Delivery_Return_Date = row.Field(Of DateTime)("Delivery_Date")
        '            newdata.Carrier = row.Field(Of String)("Carrier")
        '            newdata.DeliveredRecID = row.Field(Of Int32?)("ID")
        '            newdata.ModeID = row.Field(Of String)("ModeID")
        '            newdata.Lot_No = row.Field(Of String)("Lot_No")
        '            newdata.DeliveryReturnLocationId = row.Field(Of String)("DeliveryLocationID")
        '            'newdata.StockRecordID = row.Field(Of Int32)("StockRecordID")
        '            'newdata.Unreceived_Delivered_Qty = row.Field(Of Decimal?)("Unreceived_Delivered_Qty")
        '            _uo_items.Add(newdata)
        '        Next
        '    End If

        '    If (Not (retDataTableReturned) Is Nothing) Then
        '        'Main grid
        '        For Each row As DataRow In retDataTableReturned.Rows
        '            Dim newdata = New Return_GetUOGoodsReceivable
        '            newdata.Item_Name = row.Field(Of String)("Item_Name")
        '            newdata.Item_Code = row.Field(Of String)("Item_Code")
        '            newdata.Item_Type = row.Field(Of String)("Item_Type")
        '            newdata.Head = row.Field(Of String)("Head")
        '            newdata.Make = row.Field(Of String)("Make")
        '            newdata.Model = row.Field(Of String)("Model")
        '            newdata.Receivable_Qty = row.Field(Of Decimal)("Returned_Qty")
        '            newdata.Unit = row.Field(Of String)("Unit")
        '            newdata.Delivery_Return_Date = row.Field(Of DateTime)("Return_Date")
        '            newdata.Carrier = row.Field(Of String)("Carrier")
        '            newdata.ReturnedRecID = row.Field(Of Int32?)("ID")
        '            newdata.ModeID = row.Field(Of String)("ModeID")
        '            newdata.Lot_No = row.Field(Of String)("Lot_No")
        '            newdata.DeliveryReturnLocationId = row.Field(Of String)("ReturnLocationId")
        '            _uo_items.Add(newdata)
        '        Next
        '    End If

        '    Return _uo_items
        'End Function
        Public Function GetDeliveryModes() As List(Of Return_GetDeliveryModes)
            Dim retTable As DataTable = GetMisc("Delivery Mode", ClientScreen.Stock_UO, "Mode", "ModeID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _GetDeliveryModes_data As List(Of Return_GetDeliveryModes) = New List(Of Return_GetDeliveryModes)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDeliveryModes
                    newdata.Mode = row.Field(Of String)("Mode")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    _GetDeliveryModes_data.Add(newdata)
                Next
            End If
            Return _GetDeliveryModes_data
        End Function
        Public Function GetPersonnels(Optional StoreID As Int32 = Nothing, Optional Skill_Type As String = Nothing) As List(Of Return_GetPersonnels)
            Dim _param As New Param_GetStockPersonnels()
            If StoreID > 0 Then _param.Store_Dept_ID = StoreID
            _param.Skill_Type = Skill_Type
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Project, _param)
            Dim _Estimator_data As List(Of Return_GetPersonnels) = New List(Of Return_GetPersonnels)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPersonnels
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Skill_Type = row.Field(Of String)("Skill_Type")
                    newdata.Designation = row.Field(Of String)("Designation")
                    _Estimator_data.Add(newdata)
                Next
            End If
            Return _Estimator_data
        End Function

        Public Function GetUOGoodsDeliverAllPending(inparam As Param_GetUOGoodsDeliverAllPending) As Return_GetUOGoodsDeliveredAllPending
            'Dim inparam As New Param_GetUOGoodsDeliverAllPending

            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsDeliverAllPending, ClientScreen.Stock_UO, inparam)
            Dim _uo_items As List(Of Return_GetUOGoodsDeliveredAllPending) = New List(Of Return_GetUOGoodsDeliveredAllPending)()

            Dim _delivery_data As New Return_GetUOGoodsDeliveredAllPending
            Dim _uo_maindelivery_data As List(Of Return_GetUOGoodsDeliveredAllPending_MainGrid) = New List(Of Return_GetUOGoodsDeliveredAllPending_MainGrid)
            Dim _uo_nesteddelivery_data As List(Of Return_GetUOGoodsDeliveredAllPending_NestedGrid) = New List(Of Return_GetUOGoodsDeliveredAllPending_NestedGrid)
            _delivery_data.main_Register = _uo_maindelivery_data
            _delivery_data.nested_Register = _uo_nesteddelivery_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetUOGoodsDeliveredAllPending_MainGrid
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    'newdata.Make = row.Field(Of String)("Make")
                    'newdata.Model = row.Field(Of String)("Model")
                    newdata.Delivered_Qty = row.Field(Of Decimal)("Delivered_Qty")
                    ' newdata.Unit = row.Field(Of String)("Unit")
                    ''newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Delivery_Date = row.Field(Of DateTime)("Delivery_Date")
                    newdata.Scheduled_Delivery_Date = row.Field(Of DateTime?)("Scheduled_Delivery_Date")
                    newdata.Delivery_Mode = row.Field(Of String)("Delivery_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Delivery_Location = row.Field(Of String)("Delivery_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.PenaltyToBeCharged = row.Field(Of Decimal?)("PenaltyToBeCharged")
                    newdata.Driver = row.Field(Of String)("Driver")
                    newdata.Vehicle_No = row.Field(Of String)("Vehicle_No")
                    newdata.ID = row.Field(Of Int32?)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.DeliveryLocationId = row.Field(Of String)("DeliveryLocationID")
                    newdata.DeliveredByID = row.Field(Of Int32?)("DeliveredByID")
                    newdata.ItemRequestedID = row.Field(Of Int32)("ItemRequestedID")
                    newdata.DriverID = row.Field(Of Int32?)("DriverID")
                    newdata.SubItem_ID = row.Field(Of Int32)("SubItem_ID")
                    newdata.Receiver_Name = row.Field(Of String)("Receiver_Name")
                    'newdata.Del_ID = row.Field(Of Int32?)("Del_ID")
                    'newdata.Ret_ID = row.Field(Of Int32?)("Ret_ID")
                    'newdata.Unreceived_Delivered_Qty = row.Field(Of Decimal?)("Unreceived_Delivered_Qty")

                    _uo_maindelivery_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetUOGoodsDeliveredAllPending_NestedGrid
                    subdata.Sr = row.Field(Of Int64)("Sr")
                    subdata.UD_ID = row.Field(Of Int32?)("UD_ID")
                    subdata.ID = row.Field(Of Int32?)("ID")
                    'subdata.Item_Name = row.Field(Of String)("Item_Name")
                    'subdata.Item_Code = row.Field(Of String)("Item_Code")
                    'subdata.Item_Type = row.Field(Of String)("Item_Type")
                    subdata.UDS_Qty = row.Field(Of Decimal)("UDS_Qty")
                    subdata.SubItem_ID = row.Field(Of Int32)("SubItem_ID")
                    subdata.StockRecordID = row.Field(Of Int32)("StockID")
                    subdata.Make = row.Field(Of String)("Make")
                    subdata.Model = row.Field(Of String)("Model")
                    subdata.Lot_No = row.Field(Of String)("Lot_No")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.MainSr = row.Field(Of Int64)("MainSr")

                    _uo_nesteddelivery_data.Add(subdata)
                Next
            End If
            Return _delivery_data
        End Function

        Public Function GetUOGoodsDeliverSelectedItems(inparam As Param_GetUOGoodsDeliverSelected) As Return_GetUOGoodsDeliveredSelected
            'Dim inparam As New Param_GetUOGoodsDeliverAllPending

            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsDeliverSelectedItems, ClientScreen.Stock_UO, inparam)
            Dim _uo_items As List(Of Return_GetUOGoodsDeliveredSelected) = New List(Of Return_GetUOGoodsDeliveredSelected)()

            Dim _delivery_data As New Return_GetUOGoodsDeliveredSelected
            Dim _uo_maindelivery_data As List(Of Return_GetUOGoodsDeliveredSelected_MainGrid) = New List(Of Return_GetUOGoodsDeliveredSelected_MainGrid)
            Dim _uo_nesteddelivery_data As List(Of Return_GetUOGoodsDeliveredSelected_NestedGrid) = New List(Of Return_GetUOGoodsDeliveredSelected_NestedGrid)
            _delivery_data.main_Register = _uo_maindelivery_data
            _delivery_data.nested_Register = _uo_nesteddelivery_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetUOGoodsDeliveredSelected_MainGrid
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    'newdata.Make = row.Field(Of String)("Make")
                    'newdata.Model = row.Field(Of String)("Model")
                    newdata.Delivered_Qty = row.Field(Of Decimal)("Delivered_Qty")
                    ' newdata.Unit = row.Field(Of String)("Unit")
                    ''newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Delivery_Date = row.Field(Of DateTime)("Delivery_Date")
                    newdata.Scheduled_Delivery_Date = row.Field(Of DateTime?)("Scheduled_Delivery_Date")
                    newdata.Delivery_Mode = row.Field(Of String)("Delivery_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Delivery_Location = row.Field(Of String)("Delivery_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.PenaltyToBeCharged = row.Field(Of Decimal?)("PenaltyToBeCharged")
                    newdata.Driver = row.Field(Of String)("Driver")
                    newdata.Vehicle_No = row.Field(Of String)("Vehicle_No")
                    newdata.ID = row.Field(Of Int32?)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.DeliveryLocationId = row.Field(Of String)("DeliveryLocationID")
                    newdata.DeliveredByID = row.Field(Of Int32?)("DeliveredByID")
                    newdata.ItemRequestedID = row.Field(Of Int32)("ItemRequestedID")
                    newdata.DriverID = row.Field(Of Int32?)("DriverID")
                    newdata.SubItem_ID = row.Field(Of Int32)("SubItem_ID")
                    newdata.Receiver_Name = row.Field(Of String)("Receiver_Name")
                    'newdata.Del_ID = row.Field(Of Int32?)("Del_ID")
                    'newdata.Ret_ID = row.Field(Of Int32?)("Ret_ID")
                    'newdata.Unreceived_Delivered_Qty = row.Field(Of Decimal?)("Unreceived_Delivered_Qty")

                    _uo_maindelivery_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetUOGoodsDeliveredSelected_NestedGrid
                    subdata.Sr = row.Field(Of Int64)("Sr")
                    subdata.UD_ID = row.Field(Of Int32?)("UD_ID")
                    subdata.ID = row.Field(Of Int32?)("ID")
                    'subdata.Item_Name = row.Field(Of String)("Item_Name")
                    'subdata.Item_Code = row.Field(Of String)("Item_Code")
                    'subdata.Item_Type = row.Field(Of String)("Item_Type")
                    subdata.UDS_Qty = row.Field(Of Decimal)("UDS_Qty")
                    subdata.SubItem_ID = row.Field(Of Int32)("SubItem_ID")
                    subdata.StockRecordID = row.Field(Of Int32)("StockID")
                    subdata.Make = row.Field(Of String)("Make")
                    subdata.Model = row.Field(Of String)("Model")
                    subdata.Lot_No = row.Field(Of String)("Lot_No")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.MainSr = row.Field(Of Int64)("MainSr")

                    _uo_nesteddelivery_data.Add(subdata)
                Next
            End If
            Return _delivery_data
        End Function
        Public Function GetUOGoodsReceiveAllPending(inparam As Param_GetUOGoodsReceiveAllPending) As List(Of Return_GetUOGoodsReceivedAllPending)
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsReceiveAllPending, ClientScreen.Stock_UO, inparam)
            Dim _uo_items As List(Of Return_GetUOGoodsReceivedAllPending) = New List(Of Return_GetUOGoodsReceivedAllPending)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUOGoodsReceivedAllPending
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Received_Qty = row.Field(Of Decimal)("Received_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Received_Date = row.Field(Of DateTime)("Received_Date")
                    newdata.Received_Mode = row.Field(Of String)("Received_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Received_Location = row.Field(Of String)("Received_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32?)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.DeliveryLocationId = row.Field(Of String)("DeliveryLocationID")
                    newdata.Original_Stock_Location_ID = row.Field(Of String)("Original_Stock_Location_ID")
                    newdata.DeliveryEntryID = row.Field(Of Int32?)("DeliveryEntryID")
                    newdata.ReturnedEntryID = row.Field(Of Int32?)("ReturnedEntryID")
                    newdata.ReceivedByID = row.Field(Of Int32?)("ReceivedByID")
                    newdata.Bill_No = row.Field(Of String)("Bill_No")
                    newdata.Challan_No = row.Field(Of String)("Challan_No")
                    newdata.UDS_ID = row.Field(Of Int32)("UDS_ID")
                    _uo_items.Add(newdata)
                Next
            End If
            Return _uo_items
        End Function

        Public Function GetUOGoodsReceiveSelectedItems(inparam As Param_GetUOGoodsReceiveSelected) As List(Of Return_GetUOGoodsReceivedAllPending)
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsReceiveSelectedItems, ClientScreen.Stock_UO, inparam)
            Dim _uo_items As List(Of Return_GetUOGoodsReceivedAllPending) = New List(Of Return_GetUOGoodsReceivedAllPending)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUOGoodsReceivedAllPending
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Received_Qty = row.Field(Of Decimal)("Received_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Received_Date = row.Field(Of DateTime)("Received_Date")
                    newdata.Received_Mode = row.Field(Of String)("Received_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Received_Location = row.Field(Of String)("Received_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32?)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.DeliveryLocationId = row.Field(Of String)("DeliveryLocationID")
                    newdata.Original_Stock_Location_ID = row.Field(Of String)("Original_Stock_Location_ID")
                    newdata.DeliveryEntryID = row.Field(Of Int32?)("DeliveryEntryID")
                    newdata.ReturnedEntryID = row.Field(Of Int32?)("ReturnedEntryID")
                    newdata.ReceivedByID = row.Field(Of Int32?)("ReceivedByID")
                    newdata.Bill_No = row.Field(Of String)("Bill_No")
                    newdata.Challan_No = row.Field(Of String)("Challan_No")
                    newdata.UDS_ID = row.Field(Of Int32)("UDS_ID")
                    _uo_items.Add(newdata)
                Next
            End If
            Return _uo_items
        End Function
        Public Function GetUOGoodsReturnAllPending(inparam As Param_GetUOGoodsReturnAllPending) As List(Of Return_GetUOGoodsReturnedAllPending)
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_GetUOGoodsReturnAllPending, ClientScreen.Stock_UO, inparam)
            Dim _uo_items As List(Of Return_GetUOGoodsReturnedAllPending) = New List(Of Return_GetUOGoodsReturnedAllPending)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_GetUOGoodsReturnedAllPending
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Returned_Qty = row.Field(Of Decimal?)("Returned_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Lot_No = row.Field(Of String)("Lot_No")
                    newdata.Return_Date = row.Field(Of DateTime)("Return_Date")
                    newdata.Return_Mode = row.Field(Of String)("Return_Mode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.Received_Location = row.Field(Of String)("Return_Location")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32?)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    newdata.ReturnLocationId = row.Field(Of String)("ReturnLocationId")
                    newdata.ItemReceivedID = row.Field(Of Int32)("ItemReceivedID")
                    newdata.ReturnedByID = row.Field(Of Int32?)("ReturnedByID")
                    _uo_items.Add(newdata)
                Next
            End If
            Return _uo_items
        End Function
        Public Function GetUO_RR_Count(UO_ID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.UserOrder_GetUO_RR_Count, ClientScreen.Stock_UO, UO_ID))
        End Function
        Public Function GetUO_DependentEntry_Count(UO_ID As Integer) As Integer
            Return CInt(GetScalarBySP(RealTimeService.RealServiceFunctions.UserOrder_GetUO_DependentEntry_Count, ClientScreen.Stock_UO, UO_ID))
        End Function
        Public Function GetRecord(ByVal UO_ID As Int32) As DataTable
            Return GetDataListOfRecords(RealServiceFunctions.UserOrder_GetRecord, ClientScreen.Stock_UO, UO_ID)
        End Function
        Public Function GetUO_Job_Status(UO_ID As Integer) As String
            Return CStr(GetSingleValue_Data(RealTimeService.RealServiceFunctions.UserOrder_GetUO_Job_Status, ClientScreen.Stock_UO, UO_ID))
        End Function
        Public Function GetUO_RR_Status(UO_ID As Integer) As String
            Return CStr(GetSingleValue_Data(RealTimeService.RealServiceFunctions.UserOrder_GetUO_RR_Status, ClientScreen.Stock_UO, UO_ID))
        End Function
        Public Function GetUO_Deliveries_notReturned_Count(UO_ID As Integer) As Integer
            Return CInt(GetScalarBySP(RealTimeService.RealServiceFunctions.UserOrder_GetUO_Deliveries_notReturned_Count, ClientScreen.Stock_UO, UO_ID))
        End Function
        Public Function GetUO_Goods_In_Transit_Count(UO_ID As Integer) As Integer
            Return CInt(GetScalarBySP(RealTimeService.RealServiceFunctions.UserOrder_GetUO_Goods_In_Transit, ClientScreen.Stock_UO, UO_ID))
        End Function
        Public Function Get_RR_Details_ForUOmapping(inparam As Param_Get_RR_Details_ForUOmapping) As List(Of Return_Get_RR_Details_ForUOmapping)
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_Get_RR_Details_ForUOmapping, ClientScreen.Profile_Stock, inparam)
            Dim _uo_rr_mapping As List(Of Return_Get_RR_Details_ForUOmapping) = New List(Of Return_Get_RR_Details_ForUOmapping)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_Get_RR_Details_ForUOmapping
                    newdata.RequisitionDate = row.Field(Of DateTime)("RequisitionDate")
                    newdata.ReqType = row.Field(Of String)("ReqType")
                    newdata.Supplier_Dept = row.Field(Of String)("Supplier_Dept")
                    newdata.Purchasedby = row.Field(Of String)("Purchasedby")
                    newdata.RequisitionAmount = row.Field(Of Decimal?)("RequisitionAmount")
                    newdata.Requestor = row.Field(Of String)("Requestor")
                    newdata.Job = row.Field(Of String)("Job")
                    newdata.Project = row.Field(Of String)("Project")
                    newdata.ItemsRequested = row.Field(Of String)("ItemsRequested")
                    newdata.RR_ID = row.Field(Of Int32)("RR_ID")
                    newdata.UOIDs_Mapped = row.Field(Of Boolean)("Mapped")
                    newdata.RR_Status = row.Field(Of String)("RR_Status")
                    _uo_rr_mapping.Add(newdata)
                Next
            End If
            Return _uo_rr_mapping
        End Function

        Public Function Get_UO_Items_Detail_For_RR(inparam As Param_Get_UOItem_ForRR) As List(Of Return_Get_UO_Item_Details_ForNewRR)
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_Get_UO_Items_Detail_For_RR, ClientScreen.Stock_UO, inparam)
            Dim _uo_rr_new As List(Of Return_Get_UO_Item_Details_ForNewRR) = New List(Of Return_Get_UO_Item_Details_ForNewRR)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_Get_UO_Item_Details_ForNewRR
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Requested_Qty = row.Field(Of Decimal)("Requested_Qty")
                    newdata.Approved_Qty = row.Field(Of Decimal?)("Approved_Qty")
                    newdata.Delivered_Qty = row.Field(Of Decimal?)("Delivered_Qty")
                    newdata.Returned_Qty = row.Field(Of Decimal?)("Returned_Qty")
                    newdata.Supplier = row.Field(Of String)("Supplier")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Destination_Location = row.Field(Of String)("Destination_Location")
                    newdata.RRI_Priority = row.Field(Of String)("RRI_Priority")
                    newdata.Rate = row.Field(Of Decimal?)("Rate")
                    newdata.Rate_after_Discount = row.Field(Of Decimal?)("Rate_after_Discount")
                    newdata.Tax = row.Field(Of Decimal?)("Tax")
                    newdata.TotalTaxPercent = row.Field(Of Decimal?)("TotalTaxPercent")
                    newdata.Discount = row.Field(Of Decimal?)("Discount")
                    newdata.Amount = row.Field(Of Decimal?)("Amount")
                    newdata.Required_Delivery_Date = row.Field(Of DateTime)("Required_Delivery_Date")
                    newdata.Unit_ID = row.Field(Of String)("Unit_ID")
                    newdata.SubItemID = row.Field(Of Int32)("SubItemID")
                    newdata.LocationID = row.Field(Of String)("LocationID")
                    newdata.SupplierID = row.Field(Of Int32?)("SupplierID")
                    newdata.UO_ID = row.Field(Of Int32?)("UO_ID")
                    newdata.UO_Item_ID = row.Field(Of Int32?)("UO_Item_ID")
                    newdata.UO_Dept_ID = row.Field(Of Int32?)("UO_Dept_ID")
                    newdata.UO_Sub_Dept_ID = row.Field(Of Int32?)("UO_Sub_Dept_ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _uo_rr_new.Add(newdata)
                Next
            End If
            Return _uo_rr_new
        End Function
        Public Function Get_UO_Goods_Delivery_Stocks(inparam As Param_Get_UO_Goods_Delivery_Stocks) As List(Of Return_Get_UO_Goods_Delivery_Stocks)
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_Get_UO_Goods_Delivery_Stocks, ClientScreen.Stock_UO, inparam)
            Dim _uo_ As List(Of Return_Get_UO_Goods_Delivery_Stocks) = New List(Of Return_Get_UO_Goods_Delivery_Stocks)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_Get_UO_Goods_Delivery_Stocks
                    newdata.UD_UO_ID = row.Field(Of Int32)("UD_UO_ID")
                    newdata.Sub_Item_ID = row.Field(Of Int32)("Sub_Item_ID")
                    newdata.Del_qty = row.Field(Of Decimal)("Del_qty")
                    newdata.Avlb_Qty = row.Field(Of Decimal)("Avlb_Qty")
                    newdata.Stock_Lot_Serial_no = row.Field(Of String)("Stock_Lot_Serial_no")
                    newdata.StockUnit = row.Field(Of String)("StockUnit")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Stk_Location = row.Field(Of String)("Stk_Location")
                    newdata.PurchaseDate = row.Field(Of DateTime)("PurchaseDate")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.UD_ID = row.Field(Of Int32?)("UD_ID")
                    newdata.UDS_ID = row.Field(Of Int32?)("UDS_ID")
                    _uo_.Add(newdata)
                Next
            End If
            Return _uo_
        End Function

        Public Function Get_CenterDetails_StockAvailability() As List(Of Return_Get_CenterDetails_StockAvailability)
            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.UserOrder_Get_CenterDetails_StockAvailability, ClientScreen.Stock_UO)
            Dim _CenterDetails_data As List(Of Return_Get_CenterDetails_StockAvailability) = New List(Of Return_Get_CenterDetails_StockAvailability)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_Get_CenterDetails_StockAvailability
                    newdata.CENID = row.Field(Of Int32)("CENID")
                    newdata.CenterName = row.Field(Of String)("CenterName")
                    newdata.Center_UID = row.Field(Of String)("Center_UID")
                    _CenterDetails_data.Add(newdata)
                Next
            End If
            Return _CenterDetails_data
        End Function

        Public Function Get_Store_Locations_StockAvailability(inparam As Param_GetStoreLocations_StockAvailability) As List(Of Return_Get_Store_Locations_StockAvailability)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_Get_Store_Locations_StockAvailability, ClientScreen.Stock_UO, inparam)
            Dim _Locations_data As List(Of Return_Get_Store_Locations_StockAvailability) = New List(Of Return_Get_Store_Locations_StockAvailability)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_Get_Store_Locations_StockAvailability
                    newdata.LocationName = row.Field(Of String)("LocationName")
                    newdata.MatchedName = row.Field(Of String)("MatchedName")
                    newdata.MatchedType = row.Field(Of String)("MatchedType")
                    newdata.LocID = row.Field(Of String)("LocID")


                    _Locations_data.Add(newdata)
                Next
            End If
            Return _Locations_data
        End Function

        Public Function Get_Stock_Availability(inparam As Param_Get_Stock_Availability) As List(Of Return_Get_Stock_Availability)

            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.UserOrder_Get_Stock_Availability, ClientScreen.Stock_UO, inparam)
            Dim _User_Order_StockItems_data As List(Of Return_Get_Stock_Availability) = New List(Of Return_Get_Stock_Availability)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows

                    Dim newdata = New Return_Get_Stock_Availability
                    newdata.Stock_Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.CenterName = row.Field(Of String)("CentreName")
                    newdata.StoreName = row.Field(Of String)("StoreName")
                    newdata.Dept = row.Field(Of String)("Dept")
                    newdata.AvlbQty = row.Field(Of Decimal?)("AvlbQty")
                    newdata.ItemID = row.Field(Of Integer)("ItemID")
                    newdata.StoreID = row.Field(Of Integer)("StoreID")
                    newdata.StockID = row.Field(Of Integer)("StockID")
                    newdata.LocationName = row.Field(Of String)("LocationName")
                    _User_Order_StockItems_data.Add(newdata)

                Next
            End If
            Return _User_Order_StockItems_data
        End Function
        Public Function GetStoreList_StockAvailability(inparam As Param_GetStoreList_StockAvailability) As List(Of Return_GetStoreList_Stock_Availability)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockDeptStores_GetStoreList_SA, ClientScreen.Stock_UO, inparam)
            Dim _StockDeptStores_data As List(Of Return_GetStoreList_Stock_Availability) = New List(Of Return_GetStoreList_Stock_Availability)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreList_Stock_Availability
                    newdata.Store_Name = row.Field(Of String)("Store Name")
                    newdata.Dept_Name = row.Field(Of String)("Dept Name")
                    newdata.Sub_Dept_Name = row.Field(Of String)("Sub Dept Name")
                    newdata.Dept_Incharge_Name = row.Field(Of String)("Dept Incharge Name")
                    newdata.Store_Incharge_Name = row.Field(Of String)("Store Incharge Name")
                    newdata.StoreID = row.Field(Of Integer)("StoreID")
                    newdata.Store_Incharge_ID = row.Field(Of Integer)("StoreIncID")


                    _StockDeptStores_data.Add(newdata)
                Next
            End If
            Return _StockDeptStores_data
        End Function
        Public Function GetUOReqItem_Delivered_EntryCount(UO_ReqItemEntry_ID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.UserOrder_GetUOReqItem_Delivered_EntryCount, ClientScreen.Stock_UO, UO_ReqItemEntry_ID))
        End Function
        Public Function GetUODelivery_Received_EntryCount(UO_Delivery_ID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.UserOrder_GetUODelivery_Received_EntryCount, ClientScreen.Stock_UO, UO_Delivery_ID))
        End Function
        Public Function GetUOReceipt_Return_EntryCount(UO_Receipt_ID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.UserOrder_GetUOReceipt_Return_EntryCount, ClientScreen.Stock_UO, UO_Receipt_ID))
        End Function
        Public Function GetUOReturn_Received_EntryCount(UO_Return_ID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.UserOrder_GetUOReturn_Received_EntryCount, ClientScreen.Stock_UO, UO_Return_ID))
        End Function
        Public Function GetUO_Scrap_Creation_Count(UO_ID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.UserOrder_GetUO_Scrap_Creation_Count, ClientScreen.Stock_UO, UO_ID))
        End Function
        Public Function InsertUO(InParam As Param_Insert_UO_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.UserOrder_Insert_UO, ClientScreen.Stock_UO, InParam)
        End Function
        Public Function Insert_UO_Item(Inparam As Param_Insert_UO_Item) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.UserOrder_Insert_UO_Item, ClientScreen.Stock_UO, Inparam)
        End Function
        Public Function Insert_UO_Item_Delivered(Inparam As Param_Insert_UO_Item_Delivered) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.UserOrder_Insert_UO_Item_Delivered, ClientScreen.Stock_UO, Inparam)
        End Function
        'Public Function Insert_UO_Item_Delivered_Stocks(Inparam As Param_Insert_UO_Item_Delivered_Stocks) As Boolean
        '    Return InsertRecord(RealTimeService.RealServiceFunctions.UserOrder_Insert_UO_Item_Delivered_Stocks, ClientScreen.Stock_UO, Inparam)
        'End Function
        Public Function Insert_UO_Item_Received(Inparam As Param_Insert_UO_Item_Received) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.UserOrder_Insert_UO_Item_Received, ClientScreen.Stock_UO, Inparam)
        End Function
        Public Function Insert_UO_Item_Returned(Inparam As Param_Insert_UO_Item_Returned) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.UserOrder_Insert_UO_Item_Returned, ClientScreen.Stock_UO, Inparam)
        End Function
        Public Function Insert_UO_Item_Return_Received(Inparam As Param_Insert_UO_Item_Return_Received) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.UserOrder_Insert_UO_Item_Return_Received, ClientScreen.Stock_UO, Inparam)
        End Function
        Public Function InsertUOGoodsDeliverAllPending(inparam As Param_GetUOGoodsDeliverAllPending) As Boolean
            Return InsertRecord(RealServiceFunctions.UserOrder_InsertUOGoodsDeliverAllPending, ClientScreen.Stock_UO, inparam)
        End Function
        Public Function InsertUOGoodsReceiveAllPending(inparam As Param_GetUOGoodsReceiveAllPending) As Boolean
            Return InsertRecord(RealServiceFunctions.UserOrder_InsertUOGoodsReceiveAllPending, ClientScreen.Stock_UO, inparam)
        End Function
        Public Function InsertUOGoodsReturnAllPending(inparam As Param_GetUOGoodsReturnAllPending) As Boolean
            Return InsertRecord(RealServiceFunctions.UserOrder_InsertUOGoodsReturnAllPending, ClientScreen.Stock_UO, inparam)
        End Function
        Public Function InsertUORemarks(InParam As Param_InsertUORemarks) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.UserOrder_InsertUORemarks, ClientScreen.Stock_Project, InParam)
        End Function
        Public Function InsertUORRMapping(InParam As Param_UO_Insert_UO_RR_Mapping) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.UserOrder_InsertUORRMapping, ClientScreen.Stock_UO, InParam)
        End Function
        Public Function UpdateUO(InParam As Param_Update_UO_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.UserOrder_Update_UO, ClientScreen.Stock_UO, InParam)
        End Function
        Public Function UpdateUOStatus(UpParam As Param_Update_UO_Status) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.UserOrder_UpdateUOStatus, ClientScreen.Stock_UO, UpParam)
        End Function
        Public Function Update_Scheduled_Delivery(UpParam As Param_Update_UO_Scheduled_Delivery) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.UserOrder_Update_Scheduled_Delivery, ClientScreen.Stock_UO, UpParam)
        End Function
        Public Function DeleteUO(UO_ID As Int32, Optional Whether_To_Delete_Attachments As Boolean = True) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.UserOrder_Delete_UO, ClientScreen.Stock_UO, UO_ID)
        End Function
        Public Function UORRUnmapping(DelParam As Param_UO_RR_Unmapping) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.UserOrder_UORRUnmapping, ClientScreen.Stock_UO, DelParam)

        End Function
    End Class
    <Serializable>
    Public Class StockProduction
        Inherits SharedVariables

#Region "Return Classes"
        <Serializable>
        Public Class Return_GetRegister
            Public main_Register As List(Of Return_GetRegister_MainGrid)
            Public nested_Register As List(Of Return_GetRegister_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetRegister_MainGrid
            Public Property Prod_Date As DateTime
            Public Property Proj_Name As String
            Public Property Produced_By As String
            Public Property Complex As String
            Public Property Prod_FromDate As DateTime?
            Public Property Prod_ToDate As DateTime?
            Public Property Place_Of_Prod As String
            Public Property LotNo As String
            Public Property TotalCost As Decimal?
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedBy As String
            Public Property EditedOn As DateTime
            Public Property ID As Int32
            Public Property CurrUserRole As String
        End Class
        <Serializable>
        Public Class Return_GetRegister_NestedGrid
            Public Property Item_Produced As String
            Public Property Head As String
            Public Property Unit As String
            Public Property Remarks As String
            Public Property Produced_Qty As Decimal
            Public Property Accepted_Qty As Decimal?
            Public Property Rejected_Qty As Decimal?
            'Public Property Serial_Lot_no As String
            Public Property Cost_Price As Decimal
            Public Property Market_Price As Decimal?
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedBy As String
            Public Property EditedOn As DateTime
            Public Property Prod_ID As Int32
            Public Property ID As Int32
            Public Property Perc_TotalValue As Decimal
            Public Property Item_Value As Decimal
        End Class
        <Serializable>
        Public Class Return_GetProdItemsConsumed
            Public Property Item_Name As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Consumed_Qty As Decimal
            Public Property Unit As String
            Public Property Amount As Decimal
            Public Property Remarks As String
            Public Property consumptionID As Int32
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property StockID As Int32
            Public Property Sr As Int64
        End Class
        <Serializable>
        Public Class Return_GetProdExpensesIncurred
            Public Property Sr As Int64
            Public Property ExpDate As DateTime
            Public Property ItemName As String
            Public Property Head As String
            Public Property Party As String
            Public Property Amount As Decimal?
            Public Property TDS As Decimal?
            Public Property Narration As String
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property ID As Int32
            Public Property Exp_Tr_ID As String
            Public Property Exp_Tr_Sr_No As Int32?
        End Class
        <Serializable>
        Public Class Return_GetProdManpowerUsage
            Public Property PersonName As String
            Public Property PersonType As String
            Public Property W_PeriodFrom As DateTime
            Public Property W_PeriodTo As DateTime
            Public Property Units As String
            Public Property Units_Worked As Decimal
            Public Property RatePerUnit As Decimal
            Public Property TotalCost As Decimal
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property ManpowerID As Int32
            Public Property ManpowerChargesID As Int32
            Public Property Sr As Int64
        End Class
        <Serializable>
        Public Class Return_GetProdMachineUsage
            Public Property Machine_Name As String
            Public Property Machine_No As String
            Public Property Mch_Count As Int32
            Public Property Usage_in_Hrs As Decimal
            Public Property Remarks As String
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_ID As Int32
            Public Property Machine_ID As Int32
            Public Property Sr As Int64
        End Class
        <Serializable>
        Public Class Return_GetProdItemProduced
            Public Property ItemName As String
            Public Property Head As String
            Public Property Qty_Produced As Decimal
            Public Property Qty_Accepted As Decimal?
            Public Property Qty_Rejected As Decimal?
            Public Property TotalValue As Decimal
            Public Property MarketRate As Decimal?
            Public Property MarketPrice As Decimal?
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property StockID As Int32
            Public Property ProducedItemID As Int32
            Public Property Sr As Int64
            Public Property TotalValue_Perc As Decimal
            Public Property UnitID As String
        End Class
        <Serializable>
        Public Class Return_GetProdScrapProduced
            Public Property Item_Name As String
            Public Property Qty As Decimal
            Public Property Unit As String
            Public Property Rate As Decimal
            Public Property Amount As Decimal
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property ItemID As Int32
            Public Property Sr As Int64
            Public Property CreatedStockID As Int32
            Public Property UnitID As String
        End Class
        <Serializable>
        Public Class Return_GetProdRemarks
            Inherits Jobs.Return_GetJobRemarks
        End Class
        'Public Class Return_GetProdDocuments
        '    Inherits Jobs.Return_GetJobDocuments
        'End Class
        <Serializable>
        Public Class Return_Get_Prod_Expenses_For_Mapping
            Inherits Jobs.Return_Get_Job_Expenses_For_Mapping
        End Class
        <Serializable>
        Public Class Return_GetPersonnels
            Inherits Projects.Return_GetProjectEnginners
        End Class
        <Serializable>
        Public Class Return_GetProdDetails
            Public Property Prod_No As String
            Public Property Prod_Date As DateTime
            Public Property Prod_FromDate As DateTime?
            Public Property Prod_ToDate As DateTime?
            Public Property LocationID As String
            Public Property Location As String
            Public Property Lot_No As String
            Public Property ProjID As Int32?
            Public Property Proj_Name As String
            Public Property Sanction_No As String
            Public Property ProdDoneBy As String
            Public Property Prod_Store As String
            Public Property Prod_Store_ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetStockItems
            Inherits SubItems.Return_GetStockItems
        End Class
#End Region
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' GetProductions-Stock Entry against which Production Entry has been posted can not be updated or deleted
        ''' </summary>
        ''' <param name="StockID"></param>
        ''' <returns></returns>
        Public Function GetProductions(StockID As Int32) As DataTable
            Return GetListOfRecordsBySP(RealServiceFunctions.StockProduction_GetProductions, ClientScreen.Profile_Stock, StockID)
        End Function
        Public Function GetRegister(fromDate As DateTime, toDate As DateTime) As Return_GetRegister
            Dim InParam As New Param_GetProductionRegister
            InParam.From_Date = fromDate
            InParam.ToDate = toDate
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockProduction_GetRegister, ClientScreen.Stock_Production, InParam)

            Dim _main_data As New Return_GetRegister
            Dim _po_main_data As List(Of Return_GetRegister_MainGrid) = New List(Of Return_GetRegister_MainGrid)
            Dim _po_nested_data As List(Of Return_GetRegister_NestedGrid) = New List(Of Return_GetRegister_NestedGrid)
            _main_data.main_Register = _po_main_data
            _main_data.nested_Register = _po_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetRegister_MainGrid
                    newdata.Prod_Date = row.Field(Of DateTime)("Prod_Date")
                    newdata.Proj_Name = row.Field(Of String)("Proj_Name")
                    newdata.Produced_By = row.Field(Of String)("Produced_By")
                    newdata.Complex = row.Field(Of String)("Complex")
                    newdata.Place_Of_Prod = row.Field(Of String)("Place_Of_Prod")
                    newdata.Prod_FromDate = row.Field(Of DateTime?)("Prod_FromDate")
                    newdata.Prod_ToDate = row.Field(Of DateTime?)("Prod_ToDate")
                    newdata.LotNo = row.Field(Of String)("LotNo")
                    newdata.TotalCost = row.Field(Of Decimal?)("TotalCost")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.CurrUserRole = row.Field(Of String)("CurrUserRole")

                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    _po_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetRegister_NestedGrid
                    subdata.Item_Produced = row.Field(Of String)("Item_Produced")
                    subdata.Prod_ID = row.Field(Of Int32)("Prod_ID")
                    subdata.Head = row.Field(Of String)("Head")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.Remarks = row.Field(Of String)("Remarks")
                    subdata.Produced_Qty = row.Field(Of Decimal)("Produced_Qty")
                    subdata.Accepted_Qty = row.Field(Of Decimal?)("Accepted_Qty")
                    subdata.Rejected_Qty = row.Field(Of Decimal?)("Rejected_Qty")
                    'subdata.Serial_Lot_no = row.Field(Of String)("Serial_Lot_no")
                    subdata.Cost_Price = row.Field(Of Decimal)("Cost_Price")
                    subdata.Market_Price = row.Field(Of Decimal?)("Market_Price")
                    subdata.ID = row.Field(Of Int32)("ID")
                    subdata.AddedBy = row.Field(Of String)("AddedBy")
                    subdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    subdata.EditedBy = row.Field(Of String)("EditedBy")
                    subdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    subdata.Perc_TotalValue = row.Field(Of Decimal)("Perc_TotalValue")
                    subdata.Item_Value = row.Field(Of Decimal)("Item_Value")
                    _po_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function GetUsedLeftManpowerCount(Production_ID As Integer) As Int32
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockProduction_GetUsedLeftManpowerCount, ClientScreen.Stock_Production, Production_ID))
        End Function
        Public Function GetProdItemsConsumed(ProductionID As Int32) As List(Of Return_GetProdItemsConsumed)
            Dim ReTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProduction_GetProdItemsConsumed, ClientScreen.Stock_Production, ProductionID)
            Dim _prod_data As List(Of Return_GetProdItemsConsumed) = New List(Of Return_GetProdItemsConsumed)
            For Each row As DataRow In ReTable.Rows
                Dim newdata = New Return_GetProdItemsConsumed
                newdata.Item_Name = row.Field(Of String)("Item_Name")
                newdata.Head = row.Field(Of String)("Head")
                newdata.Make = row.Field(Of String)("Make")
                newdata.Model = row.Field(Of String)("Model")
                newdata.Consumed_Qty = row.Field(Of Decimal)("Consumed_Qty")
                newdata.Unit = row.Field(Of String)("Unit")
                newdata.Amount = row.Field(Of Decimal)("Amount")
                newdata.Remarks = row.Field(Of String)("Remarks")
                newdata.consumptionID = row.Field(Of Int32)("consumptionID")
                newdata.AddedBy = row.Field(Of String)("AddedBy")
                newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                newdata.StockID = row.Field(Of Int32)("StockID")
                newdata.Sr = row.Field(Of Int64)("Sr")
                _prod_data.Add(newdata)
            Next
            Return _prod_data
        End Function
        Public Function GetProdExpensesIncurred(ProductionID As Int32) As List(Of Return_GetProdExpensesIncurred)
            Dim ReTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProduction_GetProdExpensesIncurred, ClientScreen.Stock_Production, ProductionID)
            Dim _prod_data As List(Of Return_GetProdExpensesIncurred) = New List(Of Return_GetProdExpensesIncurred)
            For Each row As DataRow In ReTable.Rows
                Dim newdata = New Return_GetProdExpensesIncurred
                newdata.ExpDate = row.Field(Of DateTime)("Date")
                newdata.Head = row.Field(Of String)("Head")
                newdata.ItemName = row.Field(Of String)("ItemName")
                newdata.Party = row.Field(Of String)("Party")
                newdata.Amount = row.Field(Of Decimal?)("Amount")
                newdata.TDS = row.Field(Of Decimal?)("TDS")
                newdata.Narration = row.Field(Of String)("Narration")
                newdata.AddedBy = row.Field(Of String)("AddedBy")
                newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                newdata.ID = row.Field(Of Int32)("ID")
                newdata.Sr = row.Field(Of Int64)("Sr")
                newdata.Exp_Tr_ID = row.Field(Of String)("Exp_Tr_ID")
                newdata.Exp_Tr_Sr_No = row.Field(Of Int32?)("Exp_Tr_Sr_No")
                _prod_data.Add(newdata)
            Next
            Return _prod_data
        End Function
        Public Function GetProdManpowerUsage(ProductionID As Int32) As List(Of Return_GetProdManpowerUsage)
            Dim ReTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProduction_GetProdManpowerUsage, ClientScreen.Stock_Production, ProductionID)
            Dim _prod_data As List(Of Return_GetProdManpowerUsage) = New List(Of Return_GetProdManpowerUsage)
            For Each row As DataRow In ReTable.Rows
                Dim newdata = New Return_GetProdManpowerUsage
                newdata.PersonName = row.Field(Of String)("PersonName")
                newdata.PersonType = row.Field(Of String)("PersonType")
                newdata.W_PeriodFrom = row.Field(Of DateTime)("W_PeriodFrom")
                newdata.W_PeriodTo = row.Field(Of DateTime)("W_PeriodTo")
                newdata.Units = row.Field(Of String)("Units")
                newdata.Units_Worked = row.Field(Of Decimal)("Units_Worked")
                newdata.RatePerUnit = row.Field(Of Decimal)("RatePerUnit")
                newdata.TotalCost = row.Field(Of Decimal)("TotalCost")
                newdata.Remarks = row.Field(Of String)("Remarks")
                newdata.AddedBy = row.Field(Of String)("AddedBy")
                newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                newdata.ID = row.Field(Of Int32)("ID")
                newdata.ManpowerID = row.Field(Of Int32)("ManpowerID")
                newdata.ManpowerChargesID = row.Field(Of Int32)("PC_ID")
                newdata.Sr = row.Field(Of Int64)("Sr")
                _prod_data.Add(newdata)
            Next
            Return _prod_data
        End Function
        Public Function GetProdMachineUsage(ProductionID As Int32) As List(Of Return_GetProdMachineUsage)
            Dim ReTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProduction_GetProdMachineUsage, ClientScreen.Stock_Production, ProductionID)
            Dim _prod_data As List(Of Return_GetProdMachineUsage) = New List(Of Return_GetProdMachineUsage)
            For Each row As DataRow In ReTable.Rows
                Dim newdata = New Return_GetProdMachineUsage
                newdata.Machine_Name = row.Field(Of String)("Machine_Name")
                newdata.Machine_No = row.Field(Of String)("Machine_No")
                newdata.Mch_Count = row.Field(Of Int32)("Mch_Count")
                newdata.Usage_in_Hrs = row.Field(Of Decimal)("Usage_in_Hrs")
                newdata.Remarks = row.Field(Of String)("Remarks")
                newdata.REC_ADD_BY = row.Field(Of String)("AddedBy")
                newdata.REC_ADD_ON = row.Field(Of DateTime)("AddedOn")
                newdata.REC_ID = row.Field(Of Int32)("ID")
                newdata.Machine_ID = row.Field(Of Int32)("Machine_ID")
                newdata.Sr = row.Field(Of Int64)("Sr")
                _prod_data.Add(newdata)
            Next
            Return _prod_data
        End Function
        Public Function GetProdItemProduced(ProductionID As Int32) As List(Of Return_GetProdItemProduced)
            Dim ReTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProduction_GetProdItemProduced, ClientScreen.Stock_Production, ProductionID)
            Dim _prod_data As List(Of Return_GetProdItemProduced) = New List(Of Return_GetProdItemProduced)
            For Each row As DataRow In ReTable.Rows
                Dim newdata = New Return_GetProdItemProduced
                newdata.ItemName = row.Field(Of String)("ItemName")
                newdata.Head = row.Field(Of String)("Head")
                newdata.Qty_Produced = row.Field(Of Decimal)("Qty_Produced")
                newdata.Qty_Accepted = row.Field(Of Decimal?)("Qty_Accepted")
                newdata.Qty_Rejected = row.Field(Of Decimal?)("Qty_Rejected")
                newdata.TotalValue = row.Field(Of Decimal)("TotalValue")
                newdata.MarketRate = row.Field(Of Decimal?)("MarketRate")
                newdata.MarketPrice = row.Field(Of Decimal?)("MarketPrice")
                newdata.Remarks = row.Field(Of String)("Remarks")
                newdata.AddedBy = row.Field(Of String)("AddedBy")
                newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                newdata.ID = row.Field(Of Int32)("ID")
                newdata.StockID = row.Field(Of Int32)("StockID")
                newdata.ProducedItemID = row.Field(Of Int32)("ProducedItemID")
                newdata.Sr = row.Field(Of Int64)("Sr")
                newdata.TotalValue_Perc = row.Field(Of Decimal)("TotalValue_Perc")
                newdata.UnitID = row.Field(Of String)("UnitID")
                _prod_data.Add(newdata)
            Next
            Return _prod_data
        End Function
        Public Function GetProdScrapProduced(ProductionID As Int32) As List(Of Return_GetProdScrapProduced)
            Dim ReTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProduction_GetProdScrapProduced, ClientScreen.Stock_Production, ProductionID)
            Dim _prod_data As List(Of Return_GetProdScrapProduced) = New List(Of Return_GetProdScrapProduced)
            For Each row As DataRow In ReTable.Rows
                Dim newdata = New Return_GetProdScrapProduced
                newdata.Item_Name = row.Field(Of String)("Item_Name")
                newdata.Qty = row.Field(Of Decimal)("Qty")
                newdata.Unit = row.Field(Of String)("Unit")
                newdata.Rate = row.Field(Of Decimal)("Rate")
                newdata.Amount = row.Field(Of Decimal)("Amount")
                newdata.Remarks = row.Field(Of String)("Remarks")
                newdata.Added_By = row.Field(Of String)("Added_By")
                newdata.Added_On = row.Field(Of DateTime)("Added_On")
                newdata.ID = row.Field(Of Int32)("ID")
                newdata.ItemID = row.Field(Of Int32)("ItemID")
                newdata.Sr = row.Field(Of Int64)("Sr")
                newdata.CreatedStockID = row.Field(Of Int32)("CreatedStockID")
                newdata.UnitID = row.Field(Of String)("UnitID")
                _prod_data.Add(newdata)
            Next
            Return _prod_data
        End Function
        Public Function GetProdRemarks(ProductionID As Integer) As List(Of Return_GetProdRemarks)
            Dim _Remarks_Param As New Common_Lib.RealTimeService.Param_GetStockRemarks
            _Remarks_Param.RefID = ProductionID
            _Remarks_Param.Screen_Type = "Production"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockRemarks, ClientScreen.Stock_Production, _Remarks_Param)
            Dim _Remarks_data As List(Of Return_GetProdRemarks) = New List(Of Return_GetProdRemarks)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetProdRemarks
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.Remarks_By = row.Field(Of String)("REMARKS_BY")
                    newdata.Remarks_By_Designation = row.Field(Of String)("ADD_DESIGNATION")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    _Remarks_data.Add(newdata)
                Next
            End If
            Return _Remarks_data
        End Function
        Public Function GetProdDocuments(UOID As Integer) As List(Of Return_GetDocumentsGridData)
            Dim _Docs_Param As New Common_Lib.RealTimeService.Param_GetStockDocuments
            _Docs_Param.RefID = UOID
            _Docs_Param.Screen_Type = "Production"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockDocuments, ClientScreen.Stock_Production, _Docs_Param)
            Dim _Docs_data As List(Of Return_GetDocumentsGridData) = New List(Of Return_GetDocumentsGridData)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDocumentsGridData
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Document_Name = row.Field(Of String)("DOC_NAME")
                    newdata.Document_Type = row.Field(Of String)("DOC_TYPE")
                    newdata.File_Name = row.Field(Of String)("File_Name")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Added_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Applicable_From = row.Field(Of DateTime?)("Applicable_From")
                    newdata.Applicable_To = row.Field(Of DateTime?)("Applicable_To")
                    newdata.Document_Name_ID = row.Field(Of String)("Document_Name_ID")
                    _Docs_data.Add(newdata)
                Next
            End If
            Return _Docs_data
        End Function
        Public Function GetConsumableStock(StoreID As Int32, Optional projectID As Int32 = Nothing) As List(Of DbOperations.StockProfile.Return_Get_Stocks_Listing)
            Dim _Param As New Common_Lib.RealTimeService.Param_Get_Stocks_Listing
            If projectID > 0 Then _Param.ProjID = projectID 'mantis bug 954
            _Param.CurrStockOnly = True
            _Param.StoreID = StoreID
            _Param.Stock_Consumtion_Type = "Consumable" 'mantis bug 954


            Dim _Profile As New StockProfile(cBase)
            Return _Profile.Get_Stocks_Listing(Common_Lib.RealTimeService.ClientScreen.Stock_Production, _Param)
        End Function
        Public Function Get_Prod_Expenses_For_Mapping(ProductionID As Integer) As List(Of Return_Get_Prod_Expenses_For_Mapping)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProduction_Get_Prod_Expenses_For_Mapping, ClientScreen.Stock_Production, ProductionID)
            Dim _Exp_data As List(Of Return_Get_Prod_Expenses_For_Mapping) = New List(Of Return_Get_Prod_Expenses_For_Mapping)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_Get_Prod_Expenses_For_Mapping
                    newdata._Date = row.Field(Of DateTime)("Date")
                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Party = row.Field(Of String)("Party")
                    newdata.Amount = row.Field(Of Decimal)("Amount")
                    newdata.Txn_ID = row.Field(Of String)("Txn_ID")
                    newdata.Txn_Sr_No = row.Field(Of Integer?)("Txn_Sr_No")
                    newdata.Mapped_On = row.Field(Of DateTime?)("Mapped_On")
                    newdata.Mapped_By = row.Field(Of String)("Mapped_By")
                    newdata.ID = row.Field(Of Int32?)("ID")
                    newdata.Sr = row.Field(Of Int64)("Sr")

                    _Exp_data.Add(newdata)
                Next
            End If
            Return _Exp_data
        End Function
        Public Function GetPersonnels(StoreID As Int32) As List(Of Return_GetPersonnels)
            Dim _param As New Param_GetStockPersonnels()
            _param.Store_Dept_ID = StoreID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Production, _param)
            Dim _Estimator_data As List(Of Return_GetPersonnels) = New List(Of Return_GetPersonnels)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPersonnels
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Joining_Date = row.Field(Of DateTime?)("Joining_Date") 'Mantis bug 0001045 resolved
                    newdata.Leaving_Date = row.Field(Of DateTime?)("Leaving_Date") 'Mantis bug 0001045 resolved
                    _Estimator_data.Add(newdata)
                Next
            End If
            Return _Estimator_data
        End Function
        Public Function GetCurrMachines(StoreID As Int32) As List(Of DbOperations.StockProfile.Return_Get_Stocks_Listing)
            Dim _Param As New Common_Lib.RealTimeService.Param_Get_Stocks_Listing
            _Param.StockType = "Machines"
            _Param.CurrStockOnly = True
            _Param.StoreID = StoreID
            Dim _Profile As New StockProfile(cBase)
            Return _Profile.Get_Stocks_Listing(Common_Lib.RealTimeService.ClientScreen.Stock_Production, _Param)
        End Function
        Public Function GetProdDetails(ProductionID As Integer) As Return_GetProdDetails
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProduction_GetProdDetails, ClientScreen.Stock_Production, ProductionID)
            Dim newdata = New Return_GetProdDetails
            If (Not (retTable) Is Nothing) Then
                Dim row As DataRow = retTable.Rows(0)

                newdata.Prod_Date = row.Field(Of DateTime)("Prod_Date")
                newdata.Prod_No = row.Field(Of String)("Prod_No")
                newdata.Prod_FromDate = row.Field(Of DateTime?)("Prod_FromDate")
                newdata.Prod_ToDate = row.Field(Of DateTime?)("Prod_ToDate")
                newdata.LocationID = row.Field(Of String)("LocationID")
                newdata.Location = row.Field(Of String)("Location")
                newdata.Lot_No = row.Field(Of String)("Lot_No")
                newdata.ProjID = row.Field(Of Int32?)("ProjID")
                newdata.Proj_Name = row.Field(Of String)("Proj_Name")
                newdata.Sanction_No = row.Field(Of String)("Sanction_No")
                newdata.ProdDoneBy = row.Field(Of String)("ProdDoneBy")
                newdata.Prod_Store = row.Field(Of String)("Prod_Store")
                newdata.Prod_Store_ID = row.Field(Of Int32)("Prod_Store_ID")
            End If
            Return newdata
        End Function
        ''' <summary>
        ''' Shows non-scrap items
        ''' </summary>
        ''' <param name="StoreID"></param>
        ''' <returns></returns>
        Public Function GetStockItems(StoreID As Int32) As List(Of Return_GetStockItems)
            Dim inparam As New Param_GetStockItems
            inparam.StoreID = StoreID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_GetStockItems, ClientScreen.Profile_Stock, inparam)
            Dim _Stock_Profile_StockItems_data As List(Of Return_GetStockItems) = New List(Of Return_GetStockItems)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    If row.Field(Of String)("Item Category").ToLower() <> "scrap" Then
                        Dim newdata = New Return_GetStockItems
                        newdata.Stock_Item_Name = row.Field(Of String)("Stock Item Name")
                        newdata.Item_Category = row.Field(Of String)("Item Category")
                        newdata.Item_Type = row.Field(Of String)("Item Type")
                        newdata.Item_Code = row.Field(Of String)("Item Code")
                        newdata.Unit = row.Field(Of String)("Unit")
                        newdata.UnitID = row.Field(Of String)("UnitID")
                        newdata.Item_ID = row.Field(Of Integer)("ItemID")
                        'newdata.StoreIDs_Mapped = row.Field(Of Boolean)("StoreID Mapped")
                        _Stock_Profile_StockItems_data.Add(newdata)
                    End If
                Next
            End If
            Return _Stock_Profile_StockItems_data
        End Function
        Public Function GetScrapItems(StoreID As Int32) As List(Of Return_GetStockItems)
            Dim inparam As New Param_GetStockItems
            inparam.StoreID = StoreID
            inparam.Main_Category = "Scrap"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_GetStockItems, ClientScreen.Profile_Stock, inparam)
            Dim _Stock_Profile_StockItems_data As List(Of Return_GetStockItems) = New List(Of Return_GetStockItems)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStockItems
                    newdata.Stock_Item_Name = row.Field(Of String)("Stock Item Name")
                    newdata.Item_Category = row.Field(Of String)("Item Category")
                    newdata.Item_Type = row.Field(Of String)("Item Type")
                    newdata.Item_Code = row.Field(Of String)("Item Code")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.Item_ID = row.Field(Of Integer)("ItemID")
                    ' newdata.StoreIDs_Mapped = row.Field(Of Boolean)("StoreID Mapped")
                    _Stock_Profile_StockItems_data.Add(newdata)
                Next
            End If
            Return _Stock_Profile_StockItems_data
        End Function
        Public Function GetAttachmentLinkScreen(ProdID As Int32, AttachmentID As String) As String
            Dim inparam As New Parameter_GetAttachmentLinkCount()
            inparam.RefRecordID = ProdID
            inparam.RefScreen = "Production"
            inparam.AttachmentID = AttachmentID
            Return cBase._Attachments_DBOps.GetAttachmentLinkScreen(inparam)
        End Function
        Public Function InsertProduction_Txn(InParam As Param_Insert_Production_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockProduction_InsertProduction_Txn, ClientScreen.Stock_Production, InParam)
        End Function
        Public Function UpdateProduction_Txn(InParam As Param_Update_Production_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockProduction_UpdateProduction_Txn, ClientScreen.Stock_Production, InParam)
        End Function
        Public Function DeleteProduction_Txn(ProductionID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockProduction_DeleteProduction_Txn, ClientScreen.Stock_Production, ProductionID)
        End Function
    End Class
    <Serializable>
    Public Class SubItems
        Inherits SharedVariables

#Region "Param Classes"
        ''' <summary>
        ''' Return class for GetList()
        ''' </summary>
        <Serializable>
        Public Class Return_GetList
            Public Property Item_Name As String
            Public Property Store_Applicable As String
            Public Property Consumption_Type As String
            Public Property Accounting_Item As String
            Public Property Main_Category As String
            Public Property Sub_Category As String
            Public Property Item_Code As String
            Public Property Primary_Unit As String
            Public Property Item_Properties As String
            Public Property Conversion_Units As String
            Public Property Closed_On As DateTime?
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_EDIT_ON As DateTime
            Public Property REC_EDIT_BY As String
            Public Property REC_ID As Integer
        End Class
        <Serializable>
        Public Class Return_GetRecord
            Public Property Item_Name As String
            Public Property Consumption_Type As String
            Public Property Accounting_Item As String
            Public Property Main_Category As String
            Public Property Sub_Category As String
            Public Property Item_Code As String
            Public Property Primary_Unit As String
            Public Property Mapped_Store As String
            Public Property image As Byte()
            Public Property Remarks As String
            Public Property REC_ADD_ON As DateTime
            Public Property REC_ADD_BY As String
            Public Property REC_EDIT_ON As DateTime
            Public Property REC_EDIT_BY As String
            Public Property REC_ID As Integer
            Public Property MappedStores As Int32()

        End Class
        <Serializable>
        Public Class Return_GetStoreItems
            Public Property Name As String
            Public Property Head As String
            Public Property Item_Type As String
            Public Property Item_Code As String
            Public Property Unit As String
            Public Property ItemID As Int32
            Public Property UnitID As String
        End Class
        <Serializable>
        Public Class Return_GetUsageList
            Public Property Used_In As String
            Public Property Store As String
            Public Property Dept As String
            Public Property Item As String
        End Class
        <Serializable>
        Public Class Return_GetMainCategoriesMaster
            Public Property Main_Category As String
        End Class
        <Serializable>
        Public Class Return_GetSubCategoriesMaster
            Public Property Sub_Category As String
        End Class
        <Serializable>
        Public Class Return_GetItemPropertiesMaster
            Public Property _Property As String
        End Class
        <Serializable>
        Public Class Return_GetPropertiesList_SubItem
            Public Property SrNo As Int64
            Public Property Property_Name As String
            Public Property Property_Value As String
            Public Property Remarks As String

        End Class
        <Serializable>
        Public Class Return_GetUnitConversionList_SubItem
            Public Property SrNo As Int32
            Public Property Converted_Unit As String
            Public Property Rate_Of_Conversion As Decimal
            Public Property Effective_Date As DateTime
            Public Property Converted_UnitID As String

        End Class
        <Serializable>
        Public Class Return_GetStockItems
            Public Property Stock_Item_Name As String
            Public Property Item_Category As String
            Public Property Item_Type As String
            Public Property Item_Code As String
            Public Property Unit As String
            Public Property UnitID As String
            Public Property Item_ID As Integer
            ''' <summary>
            ''' this contains comma separated Store IDs mapped to the item. we need to check the current store ID against it, and hence show mapping check accordingly
            ''' </summary>
            'Public Property StoreIDs_Mapped As Boolean
        End Class
        <Serializable>
        Public Class Return_GetFilteredStockItems
            Public Property Stock_Item_Name As String
            Public Property Item_Category As String
            Public Property Item_Type As String
            Public Property Item_Code As String
            Public Property Unit As String
            Public Property UnitID As String
            Public Property Item_ID As Integer
            ''' <summary>
            ''' this contains comma separated Store IDs mapped to the item. we need to check the current store ID against it, and hence show mapping check accordingly
            ''' </summary>
            Public Property StoreIDs_Mapped As Boolean
        End Class
#End Region
        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        ''' <summary>
        ''' Gets List of Sub-Items as allowed for logged in User 
        ''' </summary>
        ''' <returns></returns>
        Public Function GetList(StoreID As Int32?) As List(Of Return_GetList)

            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.SubItem_GetList, ClientScreen.Stock_Sub_Item, StoreID)
            Dim _Sub_Item_data As List(Of Return_GetList) = New List(Of Return_GetList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetList
                    newdata.Item_Name = row.Field(Of String)("Item Name")
                    newdata.Store_Applicable = row.Field(Of String)("Store Applicable")
                    newdata.Consumption_Type = row.Field(Of String)("Consumption Type")
                    newdata.Accounting_Item = row.Field(Of String)("Accounting Item")
                    newdata.Main_Category = row.Field(Of String)("Main Category")
                    newdata.Sub_Category = row.Field(Of String)("Sub Category")
                    newdata.Item_Code = row.Field(Of String)("Item Code")
                    newdata.Primary_Unit = row.Field(Of String)("Primary Unit")
                    newdata.Item_Properties = row.Field(Of String)("Item Properties")
                    newdata.Conversion_Units = row.Field(Of String)("Conversion Units")
                    newdata.Closed_On = row.Field(Of DateTime?)("Closed on")
                    newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                    newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                    newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                    newdata.REC_ID = row.Field(Of Integer)("REC_ID")
                    _Sub_Item_data.Add(newdata)
                Next
            End If
            Return _Sub_Item_data
        End Function
        Public Function GetStoreItems(inparam As Param_GetStoreItems, screen As ClientScreen) As List(Of Return_GetStoreItems)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.SubItem_GetStoreItems, screen, inparam)
            Dim _Sub_Item_data As List(Of Return_GetStoreItems) = New List(Of Return_GetStoreItems)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStoreItems
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.ItemID = row.Field(Of Int32)("ItemID")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    _Sub_Item_data.Add(newdata)
                Next
            End If
            Return _Sub_Item_data
        End Function
        Public Function GetUsageList(subItemID As Int32, screenName As ClientScreen, Optional StoreID As Int32 = Nothing) As List(Of Return_GetUsageList)
            Dim retTable As DataTable
            Dim Param As New Param_SubItem_GetUsageList()
            Param.StoreID = StoreID
            Param.SubItemID = subItemID
            retTable = GetListOfRecordsBySP(RealServiceFunctions.SubItem_GetUsageList, screenName, Param)
            Dim _Item_Usage_data As List(Of Return_GetUsageList) = New List(Of Return_GetUsageList)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetUsageList
                    newdata.Used_In = row.Field(Of String)("Used In")
                    newdata.Store = row.Field(Of String)("Store")
                    newdata.Dept = row.Field(Of String)("Dept")
                    newdata.Item = row.Field(Of String)("Item")
                    _Item_Usage_data.Add(newdata)
                Next
            End If
            Return _Item_Usage_data
        End Function
        Public Function GetMainCategoriesMaster() As List(Of Return_GetMainCategoriesMaster)
            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.SubItem_GetMainCategoriesMaster, ClientScreen.Stock_Sub_Item)
            Dim _Main_Categories_data As List(Of Return_GetMainCategoriesMaster) = New List(Of Return_GetMainCategoriesMaster)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetMainCategoriesMaster
                    newdata.Main_Category = row.Field(Of String)("Main Category")
                    _Main_Categories_data.Add(newdata)
                Next
            End If
            Return _Main_Categories_data
        End Function
        Public Function GetSubCategoriesMaster(MainCategory As String) As List(Of Return_GetSubCategoriesMaster)
            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.SubItem_GetSubCategoriesMaster, ClientScreen.Stock_Sub_Item, MainCategory)
            Dim _Sub_Categories_data As List(Of Return_GetSubCategoriesMaster) = New List(Of Return_GetSubCategoriesMaster)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetSubCategoriesMaster
                    newdata.Sub_Category = row.Field(Of String)("Sub Category")
                    _Sub_Categories_data.Add(newdata)
                Next
            End If
            Return _Sub_Categories_data
        End Function
        Public Function GetItemPropertiesMaster() As List(Of Return_GetItemPropertiesMaster)
            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.SubItem_GetItemPropertiesMaster, ClientScreen.Stock_Sub_Item)
            Dim _ItemProperties_data As List(Of Return_GetItemPropertiesMaster) = New List(Of Return_GetItemPropertiesMaster)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetItemPropertiesMaster
                    newdata._Property = row.Field(Of String)("Property")
                    _ItemProperties_data.Add(newdata)
                Next
            End If
            Return _ItemProperties_data
        End Function
        Public Function GetPropertiesList_SubItem(SubItemID As Int32) As List(Of Return_GetPropertiesList_SubItem)
            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.SubItem_GetPropertiesList_SubItem, ClientScreen.Stock_Sub_Item, SubItemID)
            Dim _ItemProperties_data As List(Of Return_GetPropertiesList_SubItem) = New List(Of Return_GetPropertiesList_SubItem)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPropertiesList_SubItem
                    newdata.SrNo = row.Field(Of Int64)("SrNo")
                    newdata.Property_Name = row.Field(Of String)("Property Name")
                    newdata.Property_Value = row.Field(Of String)("Property Value")
                    newdata.Remarks = row.Field(Of String)("Remarks")

                    _ItemProperties_data.Add(newdata)
                Next
            End If
            Return _ItemProperties_data
        End Function
        Public Function GetUnitConversionList_SubItem(SubItemID As Int32) As List(Of Return_GetUnitConversionList_SubItem)
            Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.SubItem_GetUnitConversionList_SubItem, ClientScreen.Stock_Sub_Item, SubItemID)
            Dim _ItemUnits_data As List(Of Return_GetUnitConversionList_SubItem) = New List(Of Return_GetUnitConversionList_SubItem)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetUnitConversionList_SubItem
                    newdata.SrNo = row.Field(Of Int64)("SrNo")
                    newdata.Converted_Unit = row.Field(Of String)("Converted Unit")
                    newdata.Rate_Of_Conversion = row.Field(Of Decimal)("Rate Of Conversion")
                    newdata.Effective_Date = row.Field(Of DateTime)("Effective Date")
                    newdata.Converted_UnitID = row.Field(Of String)("UnitID")
                    _ItemUnits_data.Add(newdata)
                Next
            End If
            Return _ItemUnits_data
        End Function
        Public Function GetRecord(ByVal Rec_ID As Int32) As Return_GetRecord
            Dim retDS As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.SubItem_GetRecord, ClientScreen.Stock_Sub_Item, Rec_ID)
            Dim retDataTable As DataTable = retDS.Tables(0)
            Dim newdata As Return_GetRecord = New Return_GetRecord()
            If (Not (retDataTable) Is Nothing) Then
                Dim row As DataRow = retDataTable.Rows(0)
                newdata.Item_Name = row.Field(Of String)("Sub_Item_Name")
                'newdata.Mapped_Store = row.Field(Of String)("Mapped Store")
                newdata.Consumption_Type = row.Field(Of String)("Sub_Item_Consumption_Type")
                newdata.Accounting_Item = row.Field(Of String)("Accounting_Item")
                newdata.Main_Category = row.Field(Of String)("Sub_Item_Main_Category")
                newdata.Sub_Category = row.Field(Of String)("Sub_Item_Sub_Category")
                newdata.Item_Code = row.Field(Of String)("Sub_Item_Code")
                newdata.Primary_Unit = row.Field(Of String)("Sub_Item_Unit_Id")
                newdata.image = row.Field(Of Byte())("Sub_Item_Image")
                newdata.Remarks = row.Field(Of String)("Sub_Item_Remarks")
                newdata.REC_ADD_ON = row.Field(Of DateTime)("REC_ADD_ON")
                newdata.REC_EDIT_ON = row.Field(Of DateTime)("REC_EDIT_ON")
                newdata.REC_ADD_BY = row.Field(Of String)("REC_ADD_BY")
                newdata.REC_EDIT_BY = row.Field(Of String)("REC_EDIT_BY")
                newdata.REC_ID = row.Field(Of Integer)("REC_ID")
                Dim MappedStoreData As DataTable = retDS.Tables(1).Copy
                If (Not (MappedStoreData) Is Nothing) Then
                    ReDim newdata.MappedStores(MappedStoreData.Rows.Count)
                    Dim i As Int32
                    If MappedStoreData.Rows.Count > 0 Then
                        For i = 0 To MappedStoreData.Rows.Count - 1
                            newdata.MappedStores(i) = MappedStoreData.Rows(i)(0)
                        Next
                    End If
                End If
            End If
            Return newdata



        End Function
        Public Function GetStockItems(inparam As Param_GetStockItems) As List(Of Return_GetStockItems)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_GetStockItems, ClientScreen.Profile_Stock, inparam)
            Dim _Stock_Profile_StockItems_data As List(Of Return_GetStockItems) = New List(Of Return_GetStockItems)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetStockItems
                    newdata.Stock_Item_Name = row.Field(Of String)("Stock Item Name")
                    newdata.Item_Category = row.Field(Of String)("Item Category")
                    newdata.Item_Type = row.Field(Of String)("Item Type")
                    newdata.Item_Code = row.Field(Of String)("Item Code")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.Item_ID = row.Field(Of Integer)("ItemID")
                    'newdata.StoreIDs_Mapped = row.Field(Of Boolean)("StoreID Mapped")
                    _Stock_Profile_StockItems_data.Add(newdata)
                Next
            End If
            Return _Stock_Profile_StockItems_data
        End Function

        Public Function GetFilteredStockItems(inparam As Param_GetFilteredStockItems) As List(Of Return_GetFilteredStockItems)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockProfile_GetFilteredStockItems, ClientScreen.Profile_Stock, inparam)
            Dim _Stock_Profile_FilteredStockItems_data As List(Of Return_GetFilteredStockItems) = New List(Of Return_GetFilteredStockItems)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetFilteredStockItems
                    newdata.Stock_Item_Name = row.Field(Of String)("Stock Item Name")
                    newdata.Item_Category = row.Field(Of String)("Item Category")
                    newdata.Item_Type = row.Field(Of String)("Item Type")
                    newdata.Item_Code = row.Field(Of String)("Item Code")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.Item_ID = row.Field(Of Integer)("ItemID")
                    newdata.StoreIDs_Mapped = row.Field(Of Boolean)("StoreID Mapped")
                    _Stock_Profile_FilteredStockItems_data.Add(newdata)
                Next
            End If
            Return _Stock_Profile_FilteredStockItems_data
        End Function
        Public Function InsertSubItem(ByVal InParam As Param_SubItem_Insert_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.SubItem_InsertSubItem_Txn, ClientScreen.Stock_Sub_Item, InParam)
        End Function

        Public Function InsertItemProperties(Inparam As Param_SubItem_Insert_Item_Properties) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.SubItem_InsertItemProperties, ClientScreen.Stock_Sub_Item, Inparam)
        End Function
        Public Function InsertItemUnitconversion(Inparam As Param_SubItem_Insert_Unit_Conversion) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.SubItem_InsertItemUnitconversion, ClientScreen.Stock_Sub_Item, Inparam)
        End Function
        ''' <summary>
        ''' Returns Booleans and fills Sub Item Usage Text MEssage in ByRef Param if the Item being used is being unmapped from a store where it has already been used 
        ''' </summary>
        ''' <param name="UpdParam"></param>
        ''' <returns></returns>
        Public Function UpdateSubItem(ByVal UpdParam As Param_SubItem_Update_Txn) As Boolean
            'Add check here to find if any such store is unmapped where sub item has been used 
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.SubItem_UpdateSubItem_Txn, ClientScreen.Stock_Sub_Item, UpdParam)
        End Function
        Public Function ReOpenSubItem(ByVal subItemID As Int32, ByRef UsedStoreUnmapped As String) As Boolean
            Return UpdateRecord(RealServiceFunctions.SubItem_Reopen, ClientScreen.Stock_Sub_Item, subItemID)
        End Function
        Public Function CloseSubItem(ByVal subItemID As Int32, CloseDate As DateTime, CloseRemarks As String, ByRef UsedStoreUnmapped As String) As Boolean
            Dim InPAram As New Param_CloseSubItem
            InPAram.CloseDate = CloseDate
            InPAram.CloseRemarks = CloseRemarks
            InPAram.Sub_Item_ID = subItemID
            Return UpdateRecord(RealServiceFunctions.SubItem_Close, ClientScreen.Stock_Sub_Item, InPAram)
        End Function
        ''' <summary>
        ''' Updates mappings of given store to Items
        ''' </summary>
        ''' <param name="Store_ID">mention Selected Store's ID</param>
        ''' <param name="mapped_items_id">Contains a comma separated list of Sub Item IDs which are marked as mapped in gridview (either already or by user)</param>
        ''' <param name="unmapped_items_id">Contains a comma separated list of Sub Item IDs which are marked as unmapped in gridview (either already or by user)</param>
        ''' <returns></returns>
        Public Function UpdateSubItem_Store_Mapping(ByVal Store_ID As Int32, mapped_items_id As String, unmapped_items_id As String) As Boolean
            Dim InPAram As New Param_SubItem_Update_Store_Mapping
            InPAram.Store_ID = Store_ID
            InPAram.mapped_id = mapped_items_id
            InPAram.unmapped_id = unmapped_items_id
            Return UpdateRecord(RealServiceFunctions.SubItem_UpdateSubItem_Store_Mapping, ClientScreen.Stock_Sub_Item, InPAram)
        End Function
        ''' <summary>
        ''' Returns Booleans and fills Sub Item Usage Text MEssage in ByRef Param if the Item is being used . In this case item is not deleted 
        ''' </summary>
        ''' <param name="SubItemID"></param>
        ''' <returns></returns>
        Public Function DeleteSubItem(ByVal SubItemID As Int32) As Boolean

            Return ExecuteGroup(RealTimeService.RealServiceFunctions.SubItem_DeleteSubItem_Txn, ClientScreen.Stock_Sub_Item, SubItemID)
        End Function
    End Class
    <Serializable>
    Public Class StockRequisitionRequest
        Inherits SharedVariables
#Region "Return Classes"
        <Serializable>
        Public Class Return_GetRegister
            Public main_Register As List(Of Return_GetRegister_MainGrid)
            Public nested_Register As List(Of Return_GetRegister_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetRegister_MainGrid
            Public Property Requisition_ID As String
            Public Property Requisition_Raised As DateTime
            Public Property Requisition_Status As String
            Public Property Requisition_Type As String
            Public Property Tot_Req_Amount As Decimal?
            Public Property Purchased_by As String
            Public Property Req_Dept As String
            Public Property Remarks As String
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property Edited_On As DateTime
            Public Property Edited_By As String
            Public Property ID As Int32
            Public Property Latest_UOID As Int32?
            Public Property CurrUserRole As String
            Public Property Requestor As String
        End Class
        <Serializable>
        Public Class Return_GetRegister_NestedGrid
            Public Property Item_Name As String
            Public Property Destination_Location As String
            Public Property Requested_Quantity As Decimal
            Public Property Approved_Quantity As Decimal?
            Public Property Unit As String
            Public Property Rate As Decimal?
            Public Property Discount_Promised As Decimal?
            Public Property Amount As Decimal?
            Public Property Supplier_Dept As String
            Public Property RR_Priority As String
            Public Property Req_Delivery_Date As DateTime?
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property Edited_On As DateTime
            Public Property Edited_By As String
            Public Property ID As Int32
            Public Property RR_ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetPurchasedBy
            Public Property Name As String
            Public Property Type As String
            Public Property Main_Dept As String
            Public Property Sub_Dept As String
            Public Property ID As Integer
            Public Property Mobile_No As String
        End Class
        <Serializable>
        Public Class Return_Get_RR_Detail
            Public Property ID_No As String
            Public Property Status As String
            Public Property RR_Date As DateTime
            Public Property Project_ID As Int32?
            Public Property Job_ID As Int32?
            Public Property Dept_Store_ID As Int32
            Public Property Requestor_ID As Int32
            Public Property RR_Type As String
            Public Property Trf_From_Dept_ID As Int32?
            Public Property Purchased_by_ID As Int32?
            Public Property Special_Discount As Decimal?
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property Edited_On As DateTime
            Public Property Edited_By As String
        End Class

        <Serializable>
        Public Class Return_Get_RR_Tax_Detail
            Public Property Sr As Int64
            Public Property TaxTypeID As String
            Public Property TaxType As String
            Public Property TaxPercent As Decimal
            Public Property Tax_Amount As Decimal?
            Public Property Remarks As String
            Public Property ID As Int32
        End Class
        <Serializable>
        Public Class Return_Get_RR_Items
            Public Main_grid_Data As List(Of Return_Get_RR_Items_MainGrid)
            Public Nested_grid_Data As List(Of Return_Get_RR_Items_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_Get_RR_Items_MainGrid
            Public Property Sr As Int64
            Public Property Item_Name As String
            Public Property Item_Code As String
            Public Property Item_Type As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Requested_Qty As Decimal
            Public Property Approved_Qty As Decimal?
            Public Property Delivered_Qty As Decimal?
            Public Property Returned_Qty As Decimal?
            Public Property Supplier As String
            Public Property Unit As String
            Public Property Destination_Location As String
            Public Property RRI_Priority As String
            Public Property Rate As Decimal?
            Public Property Rate_after_Discount As Decimal?
            Public Property Tax As Decimal?
            Public Property TotalTaxPercent As Decimal?
            Public Property Discount As Decimal?
            Public Property Amount As Decimal?
            Public Property Required_Delivery_Date As DateTime
            Public Property ID As Int32
            Public Property Added_On As DateTime
            Public Property Added_By As String
            Public Property Unit_ID As String
            Public Property SubItemID As Int32
            Public Property LocationID As String
            Public Property SupplierID As Int32?
            Public Property UO_ID As Int32?
            Public Property UO_Item_ID As Int32?
            Public Property UO_Dept_ID As Int32?
            Public Property UO_Sub_Dept_ID As Int32?
            Public Property Remarks As String
        End Class
        <Serializable>
        Public Class Return_Get_RR_Items_NestedGrid
            Public Property Sr As Int64
            Public Property MainSr As Int64
            Public Property Tax_Type As String
            Public Property TaxPercent As Decimal
            Public Property TaxRemarks As String
            Public Property RRI_ID As Int32
            Public Property ID As Int32
            Public Property Tax_TypeID As String
            Public Property Tax_Amount As Decimal?
        End Class
        <Serializable>
        Public Class Return_GetRequisitionRemarks
            Inherits Jobs.Return_GetJobRemarks
        End Class
        'Public Class Return_GetRequisitionDocuments
        '    Inherits Jobs.Return_GetJobDocuments
        'End Class
        <Serializable>
        Public Class Return_Get_RR_Usage_Count
            Public UO_Count As Int32
            Public PO_Count As Int32
            Public TO_Count As Int32
        End Class
        <Serializable>
        Public Class Return_Get_RR_Linked_UO
            Public Property UO_number As String
            Public Property UO_Date As DateTime
            Public Property Requestor As String
            Public Property Job As String
            Public Property Requestee_Store As String
            Public Property Requestor_Dept As String
            Public Property Requestor_Sub_Dept As String
            Public Property Amount As Decimal?
            Public Property UO_Status As String
            Public Property ID As Int32
            Public Property Sr As Int32
        End Class

        <Serializable>
        Public Class Return_GetTaxType
            Public Property Tax_Type As String
            Public Property Tax_ID As String
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub


        Public Function GetRegister(fromDate As DateTime, toDate As DateTime) As Return_GetRegister
            Dim InParam As New Param_GetRequisitionRequestRegister
            InParam.From_Date = fromDate
            InParam.ToDate = toDate
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockRequisitionRequest_GetRegister, ClientScreen.Stock_RR, InParam)

            Dim _main_data As New Return_GetRegister
            Dim _RR_main_data As List(Of Return_GetRegister_MainGrid) = New List(Of Return_GetRegister_MainGrid)
            Dim _RR_nested_data As List(Of Return_GetRegister_NestedGrid) = New List(Of Return_GetRegister_NestedGrid)
            _main_data.main_Register = _RR_main_data
            _main_data.nested_Register = _RR_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetRegister_MainGrid
                    newdata.Requisition_ID = row.Field(Of String)("Requisition_ID")
                    newdata.Requisition_Raised = row.Field(Of DateTime)("Requisition_Raised")
                    newdata.Requisition_Status = row.Field(Of String)("Requisition_Status")
                    newdata.Requisition_Type = row.Field(Of String)("Requisition_Type")
                    newdata.Tot_Req_Amount = row.Field(Of Decimal?)("Tot_Req_Amount")
                    newdata.Purchased_by = row.Field(Of String)("Purchased_by")
                    newdata.Req_Dept = row.Field(Of String)("Req_Dept")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.Requestor = row.Field(Of String)("Requestor")

                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.Edited_On = row.Field(Of DateTime)("Edited_On")
                    newdata.Edited_By = row.Field(Of String)("Edited_By")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.CurrUserRole = row.Field(Of String)("CurrUserRole")
                    newdata.Latest_UOID = row.Field(Of Int32?)("Latest_UOID")

                    _RR_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetRegister_NestedGrid
                    subdata.Item_Name = row.Field(Of String)("Item_Name")
                    subdata.Destination_Location = row.Field(Of String)("Destination_Location")
                    subdata.Requested_Quantity = row.Field(Of Decimal)("Requested_Quantity")
                    subdata.Approved_Quantity = row.Field(Of Decimal?)("Approved_Quantity")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.Rate = row.Field(Of Decimal?)("Rate")
                    subdata.Discount_Promised = row.Field(Of Decimal?)("Discount_Promised")
                    subdata.Amount = row.Field(Of Decimal?)("Amount")
                    subdata.Supplier_Dept = row.Field(Of String)("Supplier_Dept")
                    subdata.RR_Priority = row.Field(Of String)("RR_Priority")
                    subdata.Req_Delivery_Date = row.Field(Of DateTime)("Req_Delivery_Date")
                    subdata.Added_On = row.Field(Of DateTime)("Added_On")
                    subdata.Added_By = row.Field(Of String)("Added_By")
                    subdata.Edited_On = row.Field(Of DateTime)("Edited_On")
                    subdata.Edited_By = row.Field(Of String)("Edited_By")
                    subdata.ID = row.Field(Of Int32)("ID")
                    subdata.RR_ID = row.Field(Of Int32)("RR_ID")
                    _RR_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function
        ''' <summary>
        ''' Contains Purchasers + Personnels of Requesting Store/Dept
        ''' </summary>
        ''' <returns></returns>
        Public Function GetPurchasedBy(ReqStoreDept As Int32) As List(Of Return_GetPurchasedBy)
            Dim _param As New Param_GetStockPersonnels()
            _param.Skill_Type = "Purchase Officer"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_RR, _param)
            Dim _Purchaser_data As List(Of Return_GetPurchasedBy) = New List(Of Return_GetPurchasedBy)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPurchasedBy
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _Purchaser_data.Add(newdata)
                Next
            End If

            _param = New Param_GetStockPersonnels()
            _param.Store_Dept_ID = ReqStoreDept
            retTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_RR, _param)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    If (_Purchaser_data.Find(Function(p) p.ID = row.Field(Of Integer)("ID")) Is Nothing) Then
                        Dim newdata = New Return_GetPurchasedBy
                        newdata.Name = row.Field(Of String)("Name")
                        newdata.Type = row.Field(Of String)("Type")
                        newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                        newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                        newdata.ID = row.Field(Of Integer)("ID")
                        _Purchaser_data.Add(newdata)
                    End If
                Next
            End If
            Return _Purchaser_data
        End Function
        Public Function Get_RR_Detail(RR_ID As Int32) As Return_Get_RR_Detail
            Dim reTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockRequisitionRequest_Get_RR_Detail, ClientScreen.Stock_RR, RR_ID)

            Dim newdata As Return_Get_RR_Detail = New Return_Get_RR_Detail()

            If (Not (reTable) Is Nothing) Then
                'Main grid
                Dim row As DataRow = reTable.Rows(0)
                newdata.ID_No = row.Field(Of String)("RR_ID_No")
                newdata.Status = row.Field(Of String)("RR_Status")
                newdata.RR_Date = row.Field(Of DateTime)("RR_Date")
                newdata.Project_ID = row.Field(Of Int32?)("RR_Project_ID")
                newdata.Job_ID = row.Field(Of Int32?)("RR_Job_ID")
                newdata.Dept_Store_ID = row.Field(Of Int32)("RR_Dept_Store_ID")
                newdata.Requestor_ID = row.Field(Of Int32)("RR_Requestor_ID")
                newdata.RR_Type = row.Field(Of String)("RR_Type")
                newdata.Trf_From_Dept_ID = row.Field(Of Int32?)("RR_Trf_From_Dept_ID")
                newdata.Purchased_by_ID = row.Field(Of Int32?)("RR_Purchased_by_ID")
                newdata.Special_Discount = row.Field(Of Decimal?)("Special_Discount")
                newdata.Added_On = row.Field(Of DateTime)("Added_On")
                newdata.Added_By = row.Field(Of String)("Added_By")
                newdata.Edited_On = row.Field(Of DateTime)("Edited_On")
                newdata.Edited_By = row.Field(Of String)("Edited_By")
            End If
            Return newdata
        End Function
        Public Function Get_RR_Items(inparam As Param_Get_RR_Items) As Return_Get_RR_Items
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockRequisitionRequest_Get_RR_Items, ClientScreen.Stock_RR, inparam)

            Dim _main_data As New Return_Get_RR_Items
            Dim _RR_main_data As List(Of Return_Get_RR_Items_MainGrid) = New List(Of Return_Get_RR_Items_MainGrid)
            Dim _RR_nested_data As List(Of Return_Get_RR_Items_NestedGrid) = New List(Of Return_Get_RR_Items_NestedGrid)
            _main_data.Main_grid_Data = _RR_main_data
            _main_data.Nested_grid_Data = _RR_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid data
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_Get_RR_Items_MainGrid
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Item_Name = row.Field(Of String)("Item_Name")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.Item_Type = row.Field(Of String)("Item_Type")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.Requested_Qty = row.Field(Of Decimal)("Requested_Qty")
                    newdata.Approved_Qty = row.Field(Of Decimal?)("Approved_Qty")
                    newdata.Delivered_Qty = row.Field(Of Decimal?)("Delivered_Qty")
                    newdata.Returned_Qty = row.Field(Of Decimal?)("Returned_Qty")
                    newdata.Supplier = row.Field(Of String)("Supplier")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.Destination_Location = row.Field(Of String)("Destination_Location")
                    newdata.RRI_Priority = row.Field(Of String)("RRI_Priority")
                    newdata.Rate = row.Field(Of Decimal?)("Rate")
                    newdata.Rate_after_Discount = row.Field(Of Decimal?)("Rate_after_Discount")
                    newdata.Tax = row.Field(Of Decimal?)("Tax")
                    newdata.TotalTaxPercent = row.Field(Of Decimal?)("TotalTaxPercent")
                    newdata.Discount = row.Field(Of Decimal?)("Discount")
                    newdata.Amount = row.Field(Of Decimal?)("Amount")
                    newdata.Required_Delivery_Date = row.Field(Of DateTime)("Required_Delivery_Date")
                    newdata.Unit_ID = row.Field(Of String)("Unit_ID")
                    newdata.SubItemID = row.Field(Of Int32)("SubItemID")
                    newdata.LocationID = row.Field(Of String)("LocationID")
                    newdata.SupplierID = row.Field(Of Int32?)("SupplierID")
                    newdata.UO_ID = row.Field(Of Int32?)("UO_ID")
                    newdata.UO_Item_ID = row.Field(Of Int32?)("UO_Item_ID")
                    newdata.UO_Dept_ID = row.Field(Of Int32?)("UO_Dept_ID")
                    newdata.UO_Sub_Dept_ID = row.Field(Of Int32?)("UO_Sub_Dept_ID")
                    newdata.Added_On = row.Field(Of DateTime)("Added_On")
                    newdata.Added_By = row.Field(Of String)("Added_By")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _RR_main_data.Add(newdata)
                Next
                'Nested Grid data
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_Get_RR_Items_NestedGrid
                    subdata.Sr = row.Field(Of Int64)("Sr")
                    subdata.MainSr = row.Field(Of Int64)("MainSr")
                    subdata.Tax_Type = row.Field(Of String)("Tax_Type")
                    subdata.TaxPercent = row.Field(Of Decimal)("TaxPercent")
                    subdata.TaxRemarks = row.Field(Of String)("TaxRemarks")
                    subdata.Tax_TypeID = row.Field(Of String)("Tax_TypeID")
                    subdata.ID = row.Field(Of Int32)("ID")
                    subdata.RRI_ID = row.Field(Of Int32)("RRI_ID")
                    subdata.Tax_Amount = row.Field(Of Decimal?)("Tax_Amount")
                    _RR_nested_data.Add(subdata)
                Next
            End If
            Return _main_data

        End Function
        Public Function Get_RR_Linked_UO(RR_ID As Int32) As List(Of Return_Get_RR_Linked_UO)
            Dim retDatatable As DataTable = GetDataListOfRecords(RealServiceFunctions.StockRequisitionRequest_Get_RR_Linked_UO, ClientScreen.Stock_RR, RR_ID)
            Dim _main_data As New List(Of Return_Get_RR_Linked_UO)

            If (Not (retDatatable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDatatable.Rows
                    Dim newdata = New Return_Get_RR_Linked_UO
                    newdata.UO_number = row.Field(Of String)("UO_number")
                    newdata.UO_Date = row.Field(Of DateTime)("UO_Date")
                    newdata.Requestor = row.Field(Of String)("Requestor")
                    newdata.Job = row.Field(Of String)("Job")
                    newdata.Requestee_Store = row.Field(Of String)("Requestee_Store")
                    newdata.Requestor_Dept = row.Field(Of String)("Requestor_Dept")
                    newdata.Requestor_Sub_Dept = row.Field(Of String)("Requestor_Sub_Dept")
                    newdata.Amount = row.Field(Of Decimal?)("Amount")
                    newdata.UO_Status = row.Field(Of String)("UO_Status")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _main_data.Add(newdata)
                Next
            End If
            Return _main_data

        End Function
        Public Function Get_RR_Locations(StoreID As Int32, Optional UO_Dpt_ID As Int32 = 0, Optional UO_Sub_Dpt_ID As Int32 = 0) As List(Of StockDeptStores.Return_GetLocations)
            'Dim Locations As List(Of StockDeptStores.Return_GetLocations) = cBase._StockDeptStores_dbops.GetMappedLocations(StoreID)
            'If UO_Dpt_ID > 0 Then
            '    Locations.AddRange(cBase._StockDeptStores_dbops.GetMappedLocations(UO_Dpt_ID))
            'End If
            'If UO_Sub_Dpt_ID > 0 Then
            '    Locations.AddRange(cBase._StockDeptStores_dbops.GetMappedLocations(UO_Sub_Dpt_ID))
            'End If
            'Return Locations

            Dim Locations As List(Of StockDeptStores.Return_GetLocations) = cBase._StockDeptStores_dbops.GetMappedLocations(StoreID)
            If UO_Dpt_ID > 0 Then
                Dim Locations_maindept As List(Of StockDeptStores.Return_GetLocations) = cBase._StockDeptStores_dbops.GetMappedLocations(UO_Dpt_ID)
                For Each _loc As StockDeptStores.Return_GetLocations In Locations_maindept

                    If (Locations.Find(Function(p) p.Loc_Id = _loc.Loc_Id) Is Nothing) Then
                        Dim newdata = New StockDeptStores.Return_GetLocations
                        newdata.Location_Name = _loc.Location_Name
                        newdata.Loc_Id = _loc.Loc_Id
                        newdata.Matched_Instt = _loc.Matched_Instt
                        newdata.Matched_Name = _loc.Matched_Name
                        newdata.Matched_Type = _loc.Matched_Type
                        Locations.Add(newdata)
                    End If
                Next



            End If

            If UO_Sub_Dpt_ID > 0 Then
                Dim Locations_subdept As List(Of StockDeptStores.Return_GetLocations) = cBase._StockDeptStores_dbops.GetMappedLocations(UO_Sub_Dpt_ID)
                For Each _loc As StockDeptStores.Return_GetLocations In Locations_subdept

                    If (Locations.Find(Function(p) p.Loc_Id = _loc.Loc_Id) Is Nothing) Then
                        Dim newdata = New StockDeptStores.Return_GetLocations
                        newdata.Location_Name = _loc.Location_Name
                        newdata.Loc_Id = _loc.Loc_Id
                        newdata.Matched_Instt = _loc.Matched_Instt
                        newdata.Matched_Name = _loc.Matched_Name
                        newdata.Matched_Type = _loc.Matched_Type
                        Locations.Add(newdata)
                    End If
                Next



            End If
            Return Locations
        End Function
        Public Function GetRequisitionRemarks(RR_ID As Integer) As List(Of Return_GetRequisitionRemarks)
            Dim _Remarks_Param As New Common_Lib.RealTimeService.Param_GetStockRemarks
            _Remarks_Param.RefID = RR_ID
            _Remarks_Param.Screen_Type = "Requisition"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockRemarks, ClientScreen.Stock_RR, _Remarks_Param)
            Dim _Remarks_data As List(Of Return_GetRequisitionRemarks) = New List(Of Return_GetRequisitionRemarks)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetRequisitionRemarks
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.Remarks_By = row.Field(Of String)("REMARKS_BY")
                    newdata.Remarks_By_Designation = row.Field(Of String)("ADD_DESIGNATION")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    _Remarks_data.Add(newdata)
                Next
            End If
            Return _Remarks_data
        End Function
        Public Function GetRequisitionDocuments(RR_ID As Integer) As List(Of Return_GetDocumentsGridData)
            Dim _Docs_Param As New Common_Lib.RealTimeService.Param_GetStockDocuments
            _Docs_Param.RefID = RR_ID
            _Docs_Param.Screen_Type = "Requisition"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockDocuments, ClientScreen.Stock_RR, _Docs_Param)
            Dim _Docs_data As List(Of Return_GetDocumentsGridData) = New List(Of Return_GetDocumentsGridData)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDocumentsGridData
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Document_Name = row.Field(Of String)("DOC_NAME")
                    newdata.Document_Type = row.Field(Of String)("DOC_TYPE")
                    newdata.File_Name = row.Field(Of String)("File_Name")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Added_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Applicable_From = row.Field(Of DateTime?)("Applicable_From")
                    newdata.Applicable_To = row.Field(Of DateTime?)("Applicable_To")
                    newdata.Document_Name_ID = row.Field(Of String)("Document_Name_ID")
                    _Docs_data.Add(newdata)
                Next
            End If
            Return _Docs_data
        End Function
        Public Function Get_RR_Usage_Count(RR_ID As Integer) As Return_Get_RR_Usage_Count
            Dim newdata = New Return_Get_RR_Usage_Count
            Dim retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.StockRequisitionRequest_Get_RR_Usage_Count, ClientScreen.Stock_RR, RR_ID)
            If (Not (retTable) Is Nothing) Then
                newdata.PO_Count = retTable.Rows(0).Field(Of Int32)("PO_Count")
                newdata.TO_Count = retTable.Rows(0).Field(Of Int32)("TO_Count")
                newdata.UO_Count = retTable.Rows(0).Field(Of Int32)("UO_Count")
            End If
            Return newdata
        End Function



        Public Function Get_PO_TO_Incomplete_Count(RR_ID As Integer) As Int32
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockRequisitionRequest_Get_PO_Incomplete_Count, ClientScreen.Stock_RR, RR_ID))

        End Function

        Public Function GetAttachmentLinkScreen(RRID As Int32, AttachmentID As String) As String
            Dim inparam As New Parameter_GetAttachmentLinkCount()
            inparam.RefRecordID = RRID
            inparam.RefScreen = "Requisition"
            inparam.AttachmentID = AttachmentID
            Return cBase._Attachments_DBOps.GetAttachmentLinkScreen(inparam)
        End Function



        Public Function GetTaxType() As List(Of Return_GetTaxType)
            Dim retTable As DataTable = GetMisc("Tax Type", ClientScreen.Stock_RR, "Tax_Type", "REC_ID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _RR_GetTaxType_data As List(Of Return_GetTaxType) = New List(Of Return_GetTaxType)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetTaxType
                    newdata.Tax_Type = row.Field(Of String)("Tax_Type")
                    newdata.Tax_ID = row.Field(Of String)("REC_ID")

                    _RR_GetTaxType_data.Add(newdata)
                Next
            End If
            Return _RR_GetTaxType_data
        End Function

        Public Function Get_RR_Tax_Detail(RRID As Int32, RRItemID As Int32, SubItemID As Int32) As List(Of Return_Get_RR_Tax_Detail)
            Dim inparam As New Param_Get_RR_Tax_Detail()
            inparam.RR_ID = RRID
            inparam.RRItemID = RRItemID
            inparam.SubItemID = SubItemID
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockRequisitionRequest_Get_RR_Tax_Detail, ClientScreen.Stock_RR, inparam)
            Dim _rr_tax_items As List(Of Return_Get_RR_Tax_Detail) = New List(Of Return_Get_RR_Tax_Detail)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_Get_RR_Tax_Detail
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.TaxTypeID = row.Field(Of String)("TaxTypeID")
                    newdata.TaxType = row.Field(Of String)("TaxType")
                    newdata.TaxPercent = row.Field(Of Decimal)("TaxPercent")
                    newdata.Tax_Amount = row.Field(Of Decimal?)("Tax_Amount")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")

                    _rr_tax_items.Add(newdata)
                Next
            End If
            Return _rr_tax_items
        End Function
        Public Function InsertRequisitionRequest_Txn(InParam As Param_Insert_RequisitionRequest_Txn) As Int32
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockRequisitionRequest_Insert_RR, ClientScreen.Stock_RR, InParam)
        End Function
        Public Function UpdateRequisitionRequest_Txn(InParam As Param_Update_RequisitionRequest_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockRequisitionRequest_Update_RR, ClientScreen.Stock_RR, InParam)
        End Function
        Public Function UpdateRRStatus(UpParam As Param_Update_RR_Status) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockRequisitionRequest_Update_RR_Status, ClientScreen.Stock_RR, UpParam)
        End Function
        Public Function DeleteRequisitionRequest_Txn(RR_ID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockRequisitionRequest_Delete_RR, ClientScreen.Stock_RR, RR_ID)
        End Function
    End Class
    <Serializable>
    Public Class StockPurchaseOrder
        Inherits SharedVariables
#Region "Return Classes"
        <Serializable>
        Public Class Return_GetRegister
            Public main_Register As List(Of Return_GetRegister_MainGrid)
            Public nested_Register As List(Of Return_GetRegister_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetRegister_MainGrid
            Public Property PO_No As String
            Public Property PO_Date As DateTime
            Public Property RequisitionID As String
            Public Property PO_Status As String
            Public Property DeliveryStatus As String
            Public Property PaymentStatus As String
            Public Property Req_Dest_Location As String
            'Public Req_Raised_Location As String
            Public Property TotalAmount As Decimal?
            Public Property Supplier As String
            Public Property Project As String
            Public Property Job As String
            Public Property Requestor_Dept As String
            Public Property Requestor As String
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property EditedOn As DateTime
            Public Property EditedBy As String
            Public Property ID As Int32
            Public Property SupplierID As Int32?
            Public Property CurrUserRole As String
        End Class
        <Serializable>
        Public Class Return_GetRegister_NestedGrid
            Public Property PO_ID As Int32
            Public Property ItemName As String
            Public Property Ordered_Qty As Decimal
            Public Property Received_Qty As Decimal?
            Public Property Unit As String
            Public Property Rate As Decimal?
            Public Property Amount As Decimal?

            Public Property Item_Del_Status As String
            Public Property Req_Del_Date As DateTime?
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property EditedOn As DateTime
            Public Property EditedBy As String
            Public Property ID As Int32

            Public Property SubitemID As Int32
        End Class
        <Serializable>
        Public Class Return_GetPOLinkedUserOrders
            Public main_Register As List(Of Return_GetPOLinkedUserOrders_MainGrid)
            Public nested_Register As List(Of Return_GetPOLinkedUserOrders_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetPOLinkedUserOrders_MainGrid
            Public Property Sr As Int64
            Public Property UO_number As String
            Public Property UO_Date As DateTime
            Public Property Requestor As String
            Public Property Req_Dept As String
            Public Property RR As String
            Public Property Project As String
            Public Property Job As String
            Public Property TotalAmount As Decimal?
            Public Property UO_Status As String
            Public Property ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetPOLinkedUserOrders_NestedGrid
            Public Property Sr As Int64
            Public Property ItemName As String
            Public Property ItemCode As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Requested_Qty As Decimal
            Public Property Unit As String
            Public Property Dest_Location As String
            Public Property UOID As Int32
            Public Property ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetPOLinkedRequisitions
            Public main_Register As List(Of Return_GetPOLinkedRequisitions_MainGrid)
            Public nested_Register As List(Of Return_GetPOLinkedRequisitions_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetDeliveryModes
            Inherits StockUserOrder.Return_GetDeliveryModes
        End Class
        <Serializable>
        Public Class Return_GetPOLinkedRequisitions_MainGrid
            Public Property Sr As Int64
            Public Property RR_ID As String
            Public Property RR_Date As DateTime
            Public Property Requestor As String
            Public Property Req_Dept As String
            Public Property Req_Sub_Dept As String
            Public Property Project As String
            Public Property Sanction_No As String
            Public Property Complex As String
            Public Property Job As String
            Public Property Job_No As Int32?
            Public Property Job_Type As String
            Public Property RR_Amt As Decimal?
            Public Property RR_Status As String
            Public Property ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetPOLinkedRequisitions_NestedGrid
            Public Property Sr As Int64
            Public Property ItemName As String
            Public Property ItemCode As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property RequestedQty As Decimal
            Public Property Unit As String
            Public Property DestLocation As String
            Public Property Rate As Decimal?
            Public Property Taxes As Decimal?
            Public Property Discount As Decimal?
            Public Property Amount As Decimal?
            Public Property RRI_Priority As String
            Public Property Req_Del_Date As DateTime
            Public Property ID As Int32
            Public Property RRID As Int32
        End Class
        <Serializable>
        Public Class Return_Get_PO_Tax_Detail
            Public Property Sr As Int64
            Public Property TaxTypeID As String
            Public Property TaxType As String
            Public Property TaxPercent As Decimal
            Public Property Tax_Amount As Decimal?
            Public Property Remarks As String
            Public Property ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetPOItemsOrdered
            Public Main_grid_Data As List(Of Return_Get_PO_Items_MainGrid)
            Public Nested_grid_Data As List(Of Return_Get_PO_Items_NestedGrid)
        End Class
        <Serializable>
        Public Class Return_Get_PO_Items_MainGrid
            Public Property Sr As Int32
            Public Property GroupSr As Int32
            Public Property RR As String
            Public Property ItemName As String
            Public Property ItemCode As String
            Public Property ItemType As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property Requested_Qty As Decimal
            Public Property OrderedQty As Decimal
            Public Property Unit As String
            Public Property DestLocation As String
            Public Property POI_Priority As String
            Public Property Rate As Decimal?
            Public Property Rate_after_Discount As Decimal?
            Public Property Amount As Decimal?
            Public Property Taxes As Decimal?
            Public Property Discount As Decimal?
            Public Property Reqd_Del_Date As DateTime? '0000109 bug Fixed
            Public Property ID As Int32
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property ItemID As Int32
            Public Property LocationID As String
            Public Property UnitID As String
            Public Property Remarks As String
            Public Property PostedBy As String
            Public Property PendingQty As Decimal
            Public Property RequestID As Int32?
            Public Property RR_Item_Sr_No As Int32?
            Public Property AddUpdateReason As String
            Public Property EditedOn As DateTime
            Public Property EditedBy As String
            Public Property Stock_Project_ID As Int32?
        End Class
        <Serializable>
        Public Class Return_Get_PO_Items_NestedGrid
            Public Property Sr As Int64
            Public Property MainSr As Int64
            Public Property Tax_Type As String
            Public Property TaxPercent As Decimal
            Public Property TaxRemarks As String
            Public Property PO_Item_ID As Int32
            Public Property ID As Int32
            Public Property Tax_TypeID As String
            Public Property Tax_Amount As Decimal?
        End Class
        <Serializable>
        Public Class Return_GetTaxType
            Public Property Tax_Type As String
            Public Property Tax_ID As String
        End Class
        <Serializable>
        Public Class Return_GetPOPayments
            Public Property Sr As Int64
            Public Property Mode As String
            Public Property Payment_Amt As Decimal?
            Public Property Payment_Date As DateTime?
            Public Property Deposited_BankBranch As String
            Public Property Deposited_AcctNo As String
            Public Property Ref_No As String
            Public Property Ref_Date As DateTime?
            Public Property ClearingDate As DateTime?
            Public Property Payment_Branch As String
            Public Property Payment_Bank As String
            Public Property Payment_AcctNo As String
            Public Property ID As String
            Public Property AddedOn As DateTime?
            Public Property AddedBy As String
            Public Property TxnMID As String
            Public Property Txn_Sr_No As Int32
        End Class
        <Serializable>
        Public Class Return_GetPOGoodsReceived
            Public Property Sr As Int64
            Public Property ItemName As String
            Public Property ItemCode As String
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property ReceivedQty As Decimal
            Public Property Unit As String
            Public Property ReceivedDate As DateTime
            Public Property LotNo As String
            Public Property BillNo As String
            Public Property ChallanNo As String
            Public Property ShipmentMode As String
            Public Property Carrier As String
            Public Property ReceivedBy As String
            Public Property Remarks As String
            Public Property FOB As Boolean?
            Public Property DelLocation As String
            Public Property ID As Int32
            Public Property CreatedStockID As Int32
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property EditedOn As DateTime
            Public Property EditedBy As String
            Public Property ItemID As Int32
            Public Property ReqDelDate As DateTime?
            Public Property ShipmentModeID As String
            Public Property LocationID As String
            Public Property ReceivedByID As Int32?
            Public Property POI_Priority As String
            Public Property PO_Item_Rec_ID As Int32
            Public Property Stock_Store_Dept_ID As Int32
            Public Property Stock_Proj_ID As Int32?
            Public Property Stock_Value As Decimal
            Public Property Warranty As String
            Public Property UnitID As String
            Public Property PendingToReturn As Decimal?
        End Class
        <Serializable>
        Public Class Return_GetPOGoodsReturned
            Public Property Sr As Int64
            Public Property ItemName As String
            Public Property ItemCode As String
            Public Property ItemID As Int32
            Public Property Head As String
            Public Property Make As String
            Public Property Model As String
            Public Property ReturnedQty As Decimal
            Public Property Unit As String
            Public Property ReturnedDate As DateTime
            Public Property LotNo As String
            Public Property BillNo As String
            Public Property ChallanNo As String
            Public Property ShipmentMode As String
            Public Property Carrier As String
            Public Property ReturnedBy As String
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property ReturnedStockID As Int32
            Public Property AddedOn As DateTime
            Public Property AddedBy As String
            Public Property EditedOn As DateTime
            Public Property EditedBy As String
            Public Property RecdEntryID As Int32
            Public Property ShipmentModeID As String
            Public Property ReturnedByID As Int32?
        End Class
        <Serializable>
        Public Class Return_Get_PO_Detail
            Public Property PO_Number As String
            Public Property PO_Status As String
            Public Property PO_Date As DateTime
            Public Property SupplierID As Int32?
            Public Property Supplier As String
            Public Property PurchasedBy As String
            Public Property DeliveryStatus As String
            Public Property TotalAmount As Decimal
            Public Property PaidAmount As Decimal
            Public Property PendingAmount As Decimal
            Public Property Special_Discount As Decimal?
        End Class
        <Serializable>
        Public Class Return_GetPORemarks
            Inherits Jobs.Return_GetJobRemarks
        End Class
        <Serializable>
        Public Class Return_GetPOPaymentsForMapping
            Public Property Mode As String
            Public Property Payment_Amt As Decimal?
            Public Property Payment_Date As DateTime?
            Public Property Deposited_BankBranch As String
            Public Property Deposited_AcctNo As String
            Public Property Ref_No As String
            Public Property Ref_Date As DateTime?
            Public Property ClearingDate As DateTime?
            Public Property Payment_Branch As String
            Public Property Payment_Bank As String
            Public Property Payment_AcctNo As String
            Public Property ID As String
            Public Property Txn_ID As String
            Public Property Txn_Sr_No As Int32
            Public Property AddedOn As DateTime?
            Public Property AddedBy As String
        End Class
        <Serializable>
        Public Class Return_Get_PO_Item_Received_By
            Public Name As String
            Public Type As String
            Public Main_Dept As String
            Public Sub_Dept As String
            Public ID As Integer
            Public Mobile_No As String
            Public Skill_Type As String
            Public Contractor As String
        End Class
        <Serializable>
        Public Class Return_Get_PO_Item_Dest_Locations
            Inherits StockProfile.Return_GetLocations
            Public Store_Dept_ID As Int32?
        End Class
        <Serializable>
        Public Class Return_Param_Get_PriceHistory
            Public Property ItemName As String
            Public Property ItemCode As String
            Public Property Unit As String
            Public Property VendorName As String
            Public Property Supplier_Address As String
            Public Property MobileNo As String
            Public Property Purchase_Qty As Decimal?
            Public Property Purchase_Date As DateTime
            Public Property Rate_Applied As Decimal?
            Public Property Tax As Decimal?
            Public Property Discount As Decimal?
            Public Property SubItemID As Integer
            Public Property SupplierID As Integer?
            Public Property POID As Integer
            Public Property Make As String
            Public Property Model As String
        End Class

        <Serializable>
        Public Class Return_Param_GetAllSuppliers
            Public Property SuppName As String
            Public Property CompanyCode As String
            Public Property ContactNo As String
            Public Property ID As Int32

        End Class


        <Serializable>
        Public Class Return_GetItemsPriceHistory
            Public Property ItemName As String
            Public Property ItemType As String
            Public Property Item_Code As String
            Public Property ID As Int32
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub

        Public Function GetRegister(fromDate As DateTime, toDate As DateTime) As Return_GetRegister
            Dim InParam As New Param_GetPurcahseOrderRegister
            InParam.From_Date = fromDate
            InParam.ToDate = toDate
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetRegister, ClientScreen.Stock_PO, InParam)

            Dim _main_data As New Return_GetRegister
            Dim _po_main_data As List(Of Return_GetRegister_MainGrid) = New List(Of Return_GetRegister_MainGrid)
            Dim _po_nested_data As List(Of Return_GetRegister_NestedGrid) = New List(Of Return_GetRegister_NestedGrid)
            _main_data.main_Register = _po_main_data
            _main_data.nested_Register = _po_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetRegister_MainGrid
                    newdata.PO_No = row.Field(Of String)("PO_No")
                    newdata.PO_Date = row.Field(Of DateTime)("PO_Date")
                    newdata.RequisitionID = row.Field(Of String)("RequisitionID")
                    newdata.PO_Status = row.Field(Of String)("PO_Status")
                    newdata.DeliveryStatus = row.Field(Of String)("DeliveryStatus")
                    newdata.PaymentStatus = row.Field(Of String)("PaymentStatus")
                    newdata.Req_Dest_Location = row.Field(Of String)("Req_Dest_Location")
                    'newdata.Req_Raised_Location = row.Field(Of String)("Req_Raised_Location")
                    newdata.TotalAmount = row.Field(Of Decimal?)("TotalAmount")
                    newdata.Supplier = row.Field(Of String)("Supplier")
                    newdata.Project = row.Field(Of String)("Project")
                    newdata.Job = row.Field(Of String)("Job")
                    newdata.Requestor_Dept = row.Field(Of String)("Requestor_Dept")
                    newdata.Requestor = row.Field(Of String)("Requestor")

                    newdata.SupplierID = row.Field(Of Integer?)("SupplierID")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.CurrUserRole = row.Field(Of String)("CurrUserRole")
                    _po_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetRegister_NestedGrid
                    subdata.PO_ID = row.Field(Of Int32)("PO_ID")
                    subdata.ItemName = row.Field(Of String)("ItemName")
                    subdata.Ordered_Qty = row.Field(Of Decimal)("Ordered_Qty")
                    subdata.Received_Qty = row.Field(Of Decimal?)("Received_Qty")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.Rate = row.Field(Of Decimal?)("Rate")
                    subdata.Amount = row.Field(Of Decimal?)("Amount")
                    subdata.Item_Del_Status = row.Field(Of String)("Item_Del_Status")
                    subdata.Req_Del_Date = row.Field(Of DateTime?)("Req_Del_Date")
                    subdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    subdata.AddedBy = row.Field(Of String)("AddedBy")
                    subdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    subdata.EditedBy = row.Field(Of String)("EditedBy")
                    subdata.SubitemID = row.Field(Of Int32)("Sub_Item_ID")
                    subdata.ID = row.Field(Of Integer)("ID")
                    _po_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function Get_PO_Detail(PO_ID As Int32) As Return_Get_PO_Detail
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_Get_PO_Detail, ClientScreen.Stock_PO, PO_ID)
            Dim newdata = New Return_Get_PO_Detail
            If (Not (retTable) Is Nothing) Then
                Dim row As DataRow = retTable.Rows(0)
                newdata.PO_Number = row.Field(Of String)("PO_Number")
                newdata.PO_Status = row.Field(Of String)("PO_Status")
                newdata.PO_Date = row.Field(Of DateTime)("PO_Date")
                newdata.SupplierID = row.Field(Of Int32?)("SupplierID")
                newdata.Supplier = row.Field(Of String)("Supplier")
                newdata.PurchasedBy = row.Field(Of String)("PurchasedBy")
                newdata.DeliveryStatus = row.Field(Of String)("DeliveryStatus")
                newdata.TotalAmount = row.Field(Of Decimal)("TotalAmount")
                newdata.PaidAmount = row.Field(Of Decimal)("PaidAmount")
                newdata.PendingAmount = row.Field(Of Decimal)("PendingAmount")
                newdata.Special_Discount = row.Field(Of Decimal?)("Special_Discount")

            End If
            Return newdata
        End Function
        Public Function Get_PO_Item_Dest_Locations(PO_ID As Int32) As List(Of Return_Get_PO_Item_Dest_Locations)
            'Fetches all Store/Dept ID for linked UO/RR
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_Get_Dept_for_POItem_DestLoc, ClientScreen.Stock_PO, PO_ID)

            Dim _Return_Locations_data As List(Of Return_Get_PO_Item_Dest_Locations) = New List(Of Return_Get_PO_Item_Dest_Locations)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim Store_Dept_ID As Int32 = CInt(row(0).ToString())
                    Dim _get_Locations_data As List(Of StockProfile.Return_GetLocations) = cBase._Stock_Profile_DBOps.GetLocations(Store_Dept_ID)
                    For Each _loc As StockProfile.Return_GetLocations In _get_Locations_data

                        'If (_Return_Locations_data.Find(Function(p) p.Loc_Id = _loc.Loc_Id 
                        If (_Return_Locations_data.Find(Function(p) p.Loc_Id = _loc.Loc_Id) Is Nothing) Then
                            Dim newdata = New Return_Get_PO_Item_Dest_Locations
                            newdata.Location_Name = _loc.Location_Name
                            newdata.Loc_Id = _loc.Loc_Id
                            newdata.Matched_Instt = _loc.Matched_Instt
                            newdata.Matched_Name = _loc.Matched_Name
                            newdata.Matched_Type = _loc.Matched_Type
                            newdata.Store_Dept_ID = Store_Dept_ID
                            _Return_Locations_data.Add(newdata)
                        End If
                    Next

                Next
            End If
            Return _Return_Locations_data
        End Function
        Public Function Get_PO_Item_Received_By(PO_ID As Int32) As List(Of Return_Get_PO_Item_Received_By)
            'Fetches all Store/Dept ID for linked UO/RR
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_Get_Dept_for_POItem_DestLoc, ClientScreen.Stock_PO, PO_ID)

            Dim _Received_By_data As List(Of Return_Get_PO_Item_Received_By) = New List(Of Return_Get_PO_Item_Received_By)

            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim _param As New Param_GetStockPersonnels()
                    _param.Store_Dept_ID = CInt(row(0).ToString())
                    Dim _persTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Project, _param)
                    'Dim _Estimator_data As List(Of Return_Get_PO_Item_Received_By) = New List(Of Return_Get_PO_Item_Received_By)

                    If (Not (_persTable) Is Nothing) Then
                        For Each _crow As DataRow In _persTable.Rows
                            If (_Received_By_data.Find(Function(p) p.ID = _crow.Field(Of Integer)("ID")) Is Nothing) Then
                                Dim newdata = New Return_Get_PO_Item_Received_By
                                newdata.Name = _crow.Field(Of String)("Name")
                                newdata.Type = _crow.Field(Of String)("Type")
                                newdata.Skill_Type = _crow.Field(Of String)("Skill_Type")
                                newdata.Contractor = _crow.Field(Of String)("Contractor")
                                newdata.Main_Dept = _crow.Field(Of String)("MAIN_DEPT")
                                newdata.Sub_Dept = _crow.Field(Of String)("SUB_DEPT")
                                newdata.ID = _crow.Field(Of Integer)("ID")
                                _Received_By_data.Add(newdata)
                            End If
                        Next
                    End If

                    ' Dim _get_Received_By_data As List(Of StockProduction.Return_GetPersonnels) = cBase._Stock_Production_DBOps.GetPersonnels(CInt(retTable.Rows(0)(0).ToString()))
                    ' _Received_By_data.AddRange(_get_Received_By_data)
                Next
            End If

            Return _Received_By_data
        End Function
        Public Function GetAttachmentLinkScreen(POID As Int32, AttachmentID As String) As String
            Dim inparam As New Parameter_GetAttachmentLinkCount()
            inparam.RefRecordID = POID
            inparam.RefScreen = "PO"
            inparam.AttachmentID = AttachmentID
            Return cBase._Attachments_DBOps.GetAttachmentLinkScreen(inparam)
        End Function
        Public Function GetPORemarks(PO_ID As Integer) As List(Of Return_GetPORemarks)
            Dim _Remarks_Param As New Common_Lib.RealTimeService.Param_GetStockRemarks
            _Remarks_Param.RefID = PO_ID
            _Remarks_Param.Screen_Type = "PO"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockRemarks, ClientScreen.Stock_PO, _Remarks_Param)
            Dim _Remarks_data As List(Of Return_GetPORemarks) = New List(Of Return_GetPORemarks)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPORemarks
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.Remarks_By = row.Field(Of String)("REMARKS_BY")
                    newdata.Remarks_By_Designation = row.Field(Of String)("ADD_DESIGNATION")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    _Remarks_data.Add(newdata)
                Next
            End If
            Return _Remarks_data
        End Function
        Public Function GetPODocuments(PO_ID As Integer) As List(Of Return_GetDocumentsGridData)
            Dim _Docs_Param As New Common_Lib.RealTimeService.Param_GetStockDocuments
            _Docs_Param.RefID = PO_ID
            _Docs_Param.Screen_Type = "PO"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockDocuments, ClientScreen.Stock_PO, _Docs_Param)
            Dim _Docs_data As List(Of Return_GetDocumentsGridData) = New List(Of Return_GetDocumentsGridData)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDocumentsGridData
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Document_Name = row.Field(Of String)("DOC_NAME")
                    newdata.Document_Type = row.Field(Of String)("DOC_TYPE")
                    newdata.File_Name = row.Field(Of String)("File_Name")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    newdata.Added_By = row.Field(Of String)("REC_ADD_BY")
                    newdata.Applicable_From = row.Field(Of DateTime?)("Applicable_From")
                    newdata.Applicable_To = row.Field(Of DateTime?)("Applicable_To")
                    newdata.Document_Name_ID = row.Field(Of String)("Document_Name_ID")
                    _Docs_data.Add(newdata)
                Next
            End If
            Return _Docs_data
        End Function
        Public Function GetPOLinkedUserOrders(POID As Int32) As Return_GetPOLinkedUserOrders
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetPOLinkedUserOrders, ClientScreen.Stock_PO, POID)

            Dim _main_data As New Return_GetPOLinkedUserOrders
            Dim _po_main_data As List(Of Return_GetPOLinkedUserOrders_MainGrid) = New List(Of Return_GetPOLinkedUserOrders_MainGrid)
            Dim _po_nested_data As List(Of Return_GetPOLinkedUserOrders_NestedGrid) = New List(Of Return_GetPOLinkedUserOrders_NestedGrid)
            _main_data.main_Register = _po_main_data
            _main_data.nested_Register = _po_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetPOLinkedUserOrders_MainGrid
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.UO_number = row.Field(Of String)("UO_number")
                    newdata.UO_Date = row.Field(Of DateTime)("UO_Date")
                    newdata.Requestor = row.Field(Of String)("Requestor")
                    newdata.RR = row.Field(Of String)("RR")
                    newdata.Project = row.Field(Of String)("Project")
                    newdata.Job = row.Field(Of String)("Job")
                    newdata.TotalAmount = row.Field(Of Decimal?)("TotalAmount")
                    newdata.UO_Status = row.Field(Of String)("UO_Status")
                    newdata.Req_Dept = row.Field(Of String)("Req_Dept")
                    newdata.ID = row.Field(Of Int32)("ID")
                    _po_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetPOLinkedUserOrders_NestedGrid
                    subdata.Sr = row.Field(Of Int64)("Sr")
                    subdata.ItemName = row.Field(Of String)("ItemName")
                    subdata.ItemCode = row.Field(Of String)("ItemCode")
                    subdata.Head = row.Field(Of String)("Head")
                    subdata.Make = row.Field(Of String)("Make")
                    subdata.Model = row.Field(Of String)("Model")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.Requested_Qty = row.Field(Of Decimal)("Requested_Qty")
                    subdata.Dest_Location = row.Field(Of String)("Dest_Location")
                    subdata.UOID = row.Field(Of Int32)("UOID")
                    subdata.ID = row.Field(Of Int32)("ID")
                    _po_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function GetPOLinkedRequisitions(POID As Int32) As Return_GetPOLinkedRequisitions
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetPOLinkedRequisitions, ClientScreen.Stock_PO, POID)

            Dim _main_data As New Return_GetPOLinkedRequisitions
            Dim _po_main_data As List(Of Return_GetPOLinkedRequisitions_MainGrid) = New List(Of Return_GetPOLinkedRequisitions_MainGrid)
            Dim _po_nested_data As List(Of Return_GetPOLinkedRequisitions_NestedGrid) = New List(Of Return_GetPOLinkedRequisitions_NestedGrid)
            _main_data.main_Register = _po_main_data
            _main_data.nested_Register = _po_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetPOLinkedRequisitions_MainGrid
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.RR_ID = row.Field(Of String)("RR_ID")
                    newdata.RR_Date = row.Field(Of DateTime)("RR_Date")
                    newdata.Requestor = row.Field(Of String)("Requestor")
                    newdata.Req_Dept = row.Field(Of String)("Req_Dept")
                    newdata.Req_Sub_Dept = row.Field(Of String)("Req_Sub_Dept")
                    newdata.Project = row.Field(Of String)("Project")
                    newdata.Sanction_No = row.Field(Of String)("Sanction_No")
                    newdata.Complex = row.Field(Of String)("Complex")
                    newdata.Job = row.Field(Of String)("Job")
                    newdata.Job_No = row.Field(Of Int32?)("Job_No")
                    newdata.Job_Type = row.Field(Of String)("Job_Type")
                    newdata.RR_Amt = row.Field(Of Decimal?)("RR_Amt")
                    newdata.RR_Status = row.Field(Of String)("RR_Status")
                    newdata.ID = row.Field(Of Int32)("ID")
                    _po_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetPOLinkedRequisitions_NestedGrid
                    subdata.Sr = row.Field(Of Int64)("Sr")
                    subdata.ItemName = row.Field(Of String)("ItemName")
                    subdata.ItemCode = row.Field(Of String)("ItemCode")
                    subdata.Head = row.Field(Of String)("Head")
                    subdata.Make = row.Field(Of String)("Make")
                    subdata.Model = row.Field(Of String)("Model")
                    subdata.Unit = row.Field(Of String)("Unit")
                    subdata.RequestedQty = row.Field(Of Decimal)("RequestedQty")
                    subdata.DestLocation = row.Field(Of String)("DestLocation")
                    subdata.Rate = row.Field(Of Decimal?)("Rate")
                    subdata.Taxes = row.Field(Of Decimal?)("Taxes")
                    subdata.Discount = row.Field(Of Decimal?)("Discount")
                    subdata.Amount = row.Field(Of Decimal?)("Amount")
                    subdata.RRI_Priority = row.Field(Of String)("RRI_Priority")
                    subdata.Req_Del_Date = row.Field(Of DateTime)("Req_Del_Date")
                    subdata.ID = row.Field(Of Int32)("ID")
                    subdata.RRID = row.Field(Of Int32)("RRID")
                    _po_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function GetPOItemsOrdered(PO_ID As Int32, Optional ItemRequested_not_FullyReceived_Only As Boolean? = False, Optional Recd_ID As Int32 = Nothing) As Return_GetPOItemsOrdered
            Dim inparam As New Param_GetPOItemsOrdered()
            inparam.PO_ID = PO_ID
            inparam.Recd_ID = Recd_ID
            inparam.ItemRequested_not_FullyReceived_Only = ItemRequested_not_FullyReceived_Only
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetPOItemsOrdered, ClientScreen.Stock_PO, inparam)

            Dim _main_data As New Return_GetPOItemsOrdered


            Dim _PO_main_data As List(Of Return_Get_PO_Items_MainGrid) = New List(Of Return_Get_PO_Items_MainGrid)
            Dim _PO_nested_data As List(Of Return_Get_PO_Items_NestedGrid) = New List(Of Return_Get_PO_Items_NestedGrid)
            _main_data.Main_grid_Data = _PO_main_data
            _main_data.Nested_grid_Data = _PO_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid data
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_Get_PO_Items_MainGrid
                    newdata.Sr = row.Field(Of Int32)("Sr")
                    newdata.GroupSr = row.Field(Of Int32)("GroupSr")
                    newdata.RR = row.Field(Of String)("RR")
                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.ItemCode = row.Field(Of String)("ItemCode")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.OrderedQty = row.Field(Of Decimal)("OrderedQty")
                    newdata.Requested_Qty = row.Field(Of Decimal)("Requested_Qty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.DestLocation = row.Field(Of String)("DestLocation")
                    newdata.POI_Priority = row.Field(Of String)("POI_Priority")
                    newdata.Amount = row.Field(Of Decimal?)("Amount")
                    newdata.Rate = row.Field(Of Decimal?)("Rate")
                    newdata.Rate_after_Discount = row.Field(Of Decimal?)("Rate_after_Discount")
                    newdata.Taxes = row.Field(Of Decimal?)("Taxes")
                    newdata.Discount = row.Field(Of Decimal?)("Discount")
                    newdata.Reqd_Del_Date = row.Field(Of DateTime?)("Reqd_Del_Date")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    newdata.ItemID = row.Field(Of Int32)("ItemID")
                    newdata.LocationID = row.Field(Of String)("LocationID")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.PostedBy = row.Field(Of String)("PostedBy")
                    newdata.PendingQty = row.Field(Of Decimal)("PendingQty")
                    newdata.RequestID = row.Field(Of Int32?)("RequestID")
                    newdata.RR_Item_Sr_No = row.Field(Of Int32?)("RR_Item_Sr_No")
                    newdata.ItemType = row.Field(Of String)("ItemType")
                    newdata.AddUpdateReason = row.Field(Of String)("AddUpdateReason")
                    newdata.Stock_Project_ID = row.Field(Of Int32?)("Proj_ID")
                    _PO_main_data.Add(newdata)
                Next
                'Nested Grid data
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_Get_PO_Items_NestedGrid
                    subdata.Sr = row.Field(Of Int64)("Sr")
                    subdata.MainSr = row.Field(Of Int64)("MainSr")
                    subdata.Tax_Type = row.Field(Of String)("Tax_Type")
                    subdata.TaxPercent = row.Field(Of Decimal)("TaxPercent")
                    subdata.TaxRemarks = row.Field(Of String)("TaxRemarks")
                    subdata.Tax_TypeID = row.Field(Of String)("Tax_TypeID")
                    subdata.ID = row.Field(Of Int32)("ID")
                    subdata.PO_Item_ID = row.Field(Of Int32)("POI_ID")
                    subdata.Tax_Amount = row.Field(Of Decimal?)("Tax_Amount")
                    _PO_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function GetPOItemsOrdered_NotFullyReceived(PO_ID As Int32, Optional Recd_ID As Int32 = Nothing) As Return_GetPOItemsOrdered
            Return GetPOItemsOrdered(PO_ID, True, Recd_ID)
        End Function
        Public Function GetPOPayments(PO_ID As Int32) As List(Of Return_GetPOPayments)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetPOPayments, ClientScreen.Stock_PO, PO_ID)
            Dim _GetPOItemsOrdered_data As List(Of Return_GetPOPayments) = New List(Of Return_GetPOPayments)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPOPayments
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.Mode = row.Field(Of String)("Mode")
                    newdata.Payment_Amt = row.Field(Of Decimal?)("Payment_Amt")
                    newdata.Payment_Date = row.Field(Of DateTime?)("Payment_Date")
                    newdata.Deposited_BankBranch = row.Field(Of String)("Deposited_BankBranch")
                    newdata.Deposited_AcctNo = row.Field(Of String)("Deposited_AcctNo")
                    newdata.Ref_No = row.Field(Of String)("Ref_No")
                    newdata.Ref_Date = row.Field(Of DateTime?)("Ref_Date")
                    newdata.ClearingDate = row.Field(Of DateTime?)("ClearingDate")
                    newdata.Payment_Branch = row.Field(Of String)("Payment_Branch")
                    newdata.Payment_Bank = row.Field(Of String)("Payment_Bank")
                    newdata.Payment_AcctNo = row.Field(Of String)("Payment_AcctNo")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.AddedOn = row.Field(Of DateTime?)("AddedOn")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.TxnMID = row.Field(Of String)("TxnMID")
                    newdata.Txn_Sr_No = row.Field(Of Int32)("Txn_Sr_No")
                    _GetPOItemsOrdered_data.Add(newdata)
                Next
            End If
            Return _GetPOItemsOrdered_data
        End Function
        Public Function GetPOPaymentsForMapping(SupplierID As Int32) As List(Of Return_GetPOPaymentsForMapping)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetPOPaymentsForMapping, ClientScreen.Stock_PO, SupplierID)
            Dim _GetPOItemsOrdered_data As List(Of Return_GetPOPaymentsForMapping) = New List(Of Return_GetPOPaymentsForMapping)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPOPaymentsForMapping
                    newdata.Mode = row.Field(Of String)("Mode")
                    newdata.Payment_Amt = row.Field(Of Decimal?)("Payment_Amt")
                    newdata.Payment_Date = row.Field(Of DateTime?)("Payment_Date")
                    newdata.Deposited_BankBranch = row.Field(Of String)("Deposited_BankBranch")
                    newdata.Deposited_AcctNo = row.Field(Of String)("Deposited_AcctNo")
                    newdata.Ref_No = row.Field(Of String)("Ref_No")
                    newdata.Ref_Date = row.Field(Of DateTime?)("Ref_Date")
                    newdata.ClearingDate = row.Field(Of DateTime?)("ClearingDate")
                    newdata.Payment_Branch = row.Field(Of String)("Payment_Branch")
                    newdata.Payment_Bank = row.Field(Of String)("Payment_Bank")
                    newdata.Payment_AcctNo = row.Field(Of String)("Payment_AcctNo")
                    newdata.ID = row.Field(Of String)("ID")
                    newdata.Txn_ID = row.Field(Of String)("Txn_ID")
                    newdata.Txn_Sr_No = row.Field(Of Int32)("Txn_Sr_No")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    _GetPOItemsOrdered_data.Add(newdata)
                Next
            End If
            Return _GetPOItemsOrdered_data
        End Function
        Public Function GetPOGoodsReceived(PO_ID As Int32, Optional ItemReceived_not_FullyReturned_Only As Boolean = Nothing, Optional RetID As Int32 = Nothing) As List(Of Return_GetPOGoodsReceived)
            Dim inParam As New Param_GetPOGoodsReceived()
            inParam.PO_ID = PO_ID
            inParam.RetID = RetID
            inParam.ItemReceived_not_FullyReturned_Only = ItemReceived_not_FullyReturned_Only
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetPOGoodsReceived, ClientScreen.Stock_PO, inParam)
            Dim _GetPOItemsOrdered_data As List(Of Return_GetPOGoodsReceived) = New List(Of Return_GetPOGoodsReceived)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPOGoodsReceived
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.ItemCode = row.Field(Of String)("ItemCode")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.ReceivedDate = row.Field(Of DateTime)("ReceivedDate")
                    newdata.ReceivedQty = row.Field(Of Decimal)("ReceivedQty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.LotNo = row.Field(Of String)("LotNo")
                    newdata.BillNo = row.Field(Of String)("BillNo")
                    newdata.ChallanNo = row.Field(Of String)("ChallanNo")
                    newdata.ShipmentMode = row.Field(Of String)("ShipmentMode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.ReceivedBy = row.Field(Of String)("ReceivedBy")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.FOB = row.Field(Of Boolean?)("FOB")
                    newdata.DelLocation = row.Field(Of String)("DelLocation")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.CreatedStockID = row.Field(Of Int32)("CreatedStockID")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    newdata.ItemID = row.Field(Of Int32)("ItemID")
                    newdata.ReqDelDate = row.Field(Of DateTime?)("ReqDelDate")
                    newdata.ShipmentModeID = row.Field(Of String)("ShipmentModeID")
                    newdata.LocationID = row.Field(Of String)("LocationID")
                    newdata.ReceivedByID = row.Field(Of Int32?)("ReceivedByID")
                    newdata.POI_Priority = row.Field(Of String)("POI_Priority")
                    newdata.PO_Item_Rec_ID = row.Field(Of Int32)("POI_ID")
                    newdata.Stock_Store_Dept_ID = row.Field(Of Int32)("Store_Dept_ID")
                    newdata.Stock_Proj_ID = row.Field(Of Int32?)("Proj_ID")
                    newdata.Stock_Value = row.Field(Of Decimal)("Stock_Value")
                    newdata.Warranty = row.Field(Of String)("Warranty")
                    newdata.UnitID = row.Field(Of String)("UnitID")
                    newdata.PendingToReturn = row.Field(Of Decimal?)("PendingToReturn")
                    _GetPOItemsOrdered_data.Add(newdata)
                Next
            End If
            Return _GetPOItemsOrdered_data
        End Function
        Public Function GetPOGoodsReceived_not_FullyReturned_Only(PO_ID As Int32, Optional RetID As Int32 = Nothing) As List(Of Return_GetPOGoodsReceived)
            Return GetPOGoodsReceived(PO_ID, True, RetID)
        End Function
        Public Function GetPOGoodsReturned(PO_ID As Int32) As List(Of Return_GetPOGoodsReturned)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetPOGoodsReturned, ClientScreen.Stock_PO, PO_ID)
            Dim _GetPOItemsOrdered_data As List(Of Return_GetPOGoodsReturned) = New List(Of Return_GetPOGoodsReturned)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPOGoodsReturned
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.ItemCode = row.Field(Of String)("ItemCode")
                    newdata.ItemID = row.Field(Of Int32)("ItemID")
                    newdata.Head = row.Field(Of String)("Head")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    newdata.ReturnedQty = row.Field(Of Decimal)("ReturnedQty")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.ReturnedDate = row.Field(Of DateTime)("ReturnedDate")
                    newdata.LotNo = row.Field(Of String)("LotNo")
                    newdata.BillNo = row.Field(Of String)("BillNo")
                    newdata.ChallanNo = row.Field(Of String)("ChallanNo")
                    newdata.ShipmentMode = row.Field(Of String)("ShipmentMode")
                    newdata.Carrier = row.Field(Of String)("Carrier")
                    newdata.ReturnedBy = row.Field(Of String)("ReturnedBy")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")
                    newdata.ReturnedStockID = row.Field(Of Int32)("ReturnedStockID")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    newdata.RecdEntryID = row.Field(Of Int32)("RecdEntryID")
                    newdata.ShipmentModeID = row.Field(Of String)("ShipmentModeID")
                    newdata.ReturnedByID = row.Field(Of Int32?)("ReturnedByID")
                    _GetPOItemsOrdered_data.Add(newdata)
                Next
            End If
            Return _GetPOItemsOrdered_data
        End Function
        Public Function GetDeliveryModes() As List(Of Return_GetDeliveryModes)
            Dim retTable As DataTable = GetMisc("Delivery Mode", ClientScreen.Stock_UO, "Mode", "ModeID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _GetDeliveryModes_data As List(Of Return_GetDeliveryModes) = New List(Of Return_GetDeliveryModes)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetDeliveryModes
                    newdata.Mode = row.Field(Of String)("Mode")
                    newdata.ModeID = row.Field(Of String)("ModeID")
                    _GetDeliveryModes_data.Add(newdata)
                Next
            End If
            Return _GetDeliveryModes_data
        End Function
        Public Function Get_PO_Latest_RR_ID(PO_ID As Integer) As Int32?
            Return GetSingleValue_Data(RealServiceFunctions.StockPurchaseOrder_Get_PO_Latest_RR_ID, ClientScreen.Stock_PO, PO_ID)
        End Function
        Public Function Get_PO_Latest_UO_ID(PO_ID As Integer) As Int32?
            Dim UO_Return As Object 'Mantis bug 0000114 fixed
            UO_Return = GetSingleValue_Data(RealServiceFunctions.StockPurchaseOrder_Get_PO_Latest_UO_ID, ClientScreen.Stock_PO, PO_ID)
            If UO_Return Is System.DBNull.Value Then 'Mantis bug 0000114 fixed
                Return Nothing
            End If
            Return DirectCast(UO_Return, Int32?) 'Mantis bug 0000114 fixed
        End Function
        Public Function Get_PO_Job_Project_Completed(PO_ID As Integer) As Boolean
            Return GetSingleValue_Data(RealServiceFunctions.StockPurchaseOrder_Get_PO_Job_Project_Completed, ClientScreen.Stock_PO, PO_ID)
        End Function
        Public Function Get_PO_Non_Rate_Items(PO_ID As Integer) As Boolean
            Return GetSingleValue_Data(RealServiceFunctions.StockPurchaseOrder_Get_PO_Non_Rate_Items, ClientScreen.Stock_PO, PO_ID)
        End Function
        Public Function Get_PO_Pending_Due(PO_ID As Integer) As Decimal?
            Return GetSingleValue_Data(RealServiceFunctions.StockPurchaseOrder_Get_PO_Pending_Due, ClientScreen.Stock_PO, PO_ID)
        End Function

        Public Function Get_Stock_Current_Quantity_Count(Stock_ID As Integer) As Decimal?
            Return GetSingleValue_Data(RealServiceFunctions.StockPurchaseOrder_Get_Stock_Current_Quantity_Count, ClientScreen.Stock_PO, Stock_ID)
        End Function
        Public Function Get_PO_Related_ClosedDept_Count(PO_ID As Integer) As Int32?
            Return GetSingleValue_Data(RealServiceFunctions.StockPurchaseOrder_Get_PO_Related_ClosedDept_Count, ClientScreen.Stock_PO, PO_ID)
        End Function

        Public Function Get_PO_Duplicate_LotNo_Count(StockID As Int32?, LotNo As String, stockrefID As Int32?, subitemID As Int32?) As Int32?
            Dim upParam As New Param_Get_Lotno_Duplication
            upParam.StockID = StockID
            upParam.Lot_Serial_No = LotNo
            upParam.StockRefID = stockrefID
            upParam.SubItemID = subitemID
            Return GetSingleValue_Data(RealServiceFunctions.StockPurchaseOrder_Get_PO_Duplicate_LotNo_Count, ClientScreen.Stock_PO, upParam)
        End Function
        Public Function GetPOItem_Received_EntryCount(PO_Item_ID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockPurchaseOrder_GetPOItem_Received_EntryCount, ClientScreen.Stock_PO, PO_Item_ID))
        End Function
        Public Function GetPOReceipt_Return_EntryCount(PO_Receipt_ID As Integer) As Integer
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockPurchaseOrder_GetPOReceipt_Return_EntryCount, ClientScreen.Stock_PO, PO_Receipt_ID))
        End Function
        Public Function GetSupplier_PriceHistory() As List(Of Return_Param_GetAllSuppliers)
            Dim _retTable As DataTable = GetDataListOfRecords(RealServiceFunctions.StockPurchaseOrder_GetSupplier_PriceHistory, ClientScreen.Stock_Supplier_Master)

            Dim main_data As List(Of Return_Param_GetAllSuppliers) = New List(Of Return_Param_GetAllSuppliers)
            If (Not (_retTable) Is Nothing) Then
                For Each row As DataRow In _retTable.Rows
                    Dim newdata = New Return_Param_GetAllSuppliers
                    newdata.SuppName = row.Field(Of String)("SuppName")
                    newdata.CompanyCode = row.Field(Of String)("CompanyCode")
                    newdata.ContactNo = row.Field(Of String)("ContactNo")
                    newdata.ID = row.Field(Of Int32)("ID")
                    main_data.Add(newdata)
                Next
            End If
            Return main_data
        End Function

        Public Function Get_PriceHistory(inparam As Param_Get_PriceHistory) As List(Of Return_Param_Get_PriceHistory)

            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_Get_PriceHistory, ClientScreen.Stock_PO, inparam)
            Dim _PO_PriceHistory_data As List(Of Return_Param_Get_PriceHistory) = New List(Of Return_Param_Get_PriceHistory)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows

                    Dim newdata = New Return_Param_Get_PriceHistory
                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.ItemCode = row.Field(Of String)("ItemCode")
                    newdata.Unit = row.Field(Of String)("Unit")
                    newdata.VendorName = row.Field(Of String)("VendorName")
                    newdata.Supplier_Address = row.Field(Of String)("Supplier_Address")
                    newdata.MobileNo = row.Field(Of String)("MobileNo")
                    newdata.Purchase_Qty = row.Field(Of Decimal?)("Purchase_Qty")
                    newdata.Purchase_Date = row.Field(Of DateTime)("Purchase_Date")
                    newdata.Rate_Applied = row.Field(Of Decimal?)("Rate_Applied")
                    newdata.Tax = row.Field(Of Decimal?)("Tax")
                    newdata.Discount = row.Field(Of Decimal?)("Discount")
                    newdata.SubItemID = row.Field(Of Integer)("SubItemID")
                    newdata.SupplierID = row.Field(Of Integer?)("SupplierID")
                    newdata.POID = row.Field(Of Integer)("POID")
                    newdata.Make = row.Field(Of String)("Make")
                    newdata.Model = row.Field(Of String)("Model")
                    _PO_PriceHistory_data.Add(newdata)

                Next
            End If
            Return _PO_PriceHistory_data
        End Function
        Public Function GetItemsPriceHistory(screenName As ClientScreen, SupplierID As Int32?) As List(Of Return_GetItemsPriceHistory)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockPurchaseOrder_GetItemsPriceHistory, screenName, SupplierID)
            Dim _items_data As List(Of Return_GetItemsPriceHistory) = New List(Of Return_GetItemsPriceHistory)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows

                    Dim newdata = New Return_GetItemsPriceHistory

                    newdata.ItemName = row.Field(Of String)("ItemName")
                    newdata.ItemType = row.Field(Of String)("ItemType")
                    newdata.Item_Code = row.Field(Of String)("Item_Code")
                    newdata.ID = row.Field(Of Int32)("ID")
                    _items_data.Add(newdata)
                Next
            End If
            Return _items_data
        End Function

        Public Function GetTaxType() As List(Of Return_GetTaxType)
            Dim retTable As DataTable = GetMisc("Tax Type", ClientScreen.Stock_PO, "Tax_Type", "REC_ID")
            ' Dim retTable As DataTable = GetDataListOfRecords(RealTimeService.RealServiceFunctions.StockProfile_GetUnits, ClientScreen.Profile_Stock)
            Dim _PO_GetTaxType_data As List(Of Return_GetTaxType) = New List(Of Return_GetTaxType)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetTaxType
                    newdata.Tax_Type = row.Field(Of String)("Tax_Type")
                    newdata.Tax_ID = row.Field(Of String)("REC_ID")

                    _PO_GetTaxType_data.Add(newdata)
                Next
            End If
            Return _PO_GetTaxType_data
        End Function

        Public Function Get_PO_Tax_Detail(RRID As Int32, RRItemID As Int32, SubItemID As Int32) As List(Of Return_Get_PO_Tax_Detail)
            Dim inparam As New Param_Get_RR_Tax_Detail()
            inparam.RR_ID = RRID
            inparam.RRItemID = RRItemID
            inparam.SubItemID = SubItemID
            Dim retDataTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockRequisitionRequest_Get_RR_Tax_Detail, ClientScreen.Stock_RR, inparam)
            Dim _rr_tax_items As List(Of Return_Get_PO_Tax_Detail) = New List(Of Return_Get_PO_Tax_Detail)()

            If (Not (retDataTable) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataTable.Rows
                    Dim newdata = New Return_Get_PO_Tax_Detail
                    newdata.Sr = row.Field(Of Int64)("Sr")
                    newdata.TaxTypeID = row.Field(Of String)("TaxTypeID")
                    newdata.TaxType = row.Field(Of String)("TaxType")
                    newdata.TaxPercent = row.Field(Of Decimal)("TaxPercent")
                    newdata.Tax_Amount = row.Field(Of Decimal?)("Tax_Amount")
                    newdata.Remarks = row.Field(Of String)("Remarks")
                    newdata.ID = row.Field(Of Int32)("ID")

                    _rr_tax_items.Add(newdata)
                Next
            End If
            Return _rr_tax_items
        End Function
        Public Function InsertPurchaseOrderGoodsReceived(InParam As Param_InsertPurchaseOrderGoodsReceived) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.StockPurchaseOrder_InsertPurchaseOrderGoodsReceived, ClientScreen.Stock_PO, InParam)
        End Function
        Public Function InsertPurchaseOrderGoodsReturned(InParam As Param_InsertPurchaseOrderGoodsReturned) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.StockPurchaseOrder_InsertPurchaseOrderGoodsReturned, ClientScreen.Stock_PO, InParam)
        End Function
        Public Function InsertPurchaseOrderPayment(InParam As Param_InsertPurchaseOrderPayment) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.StockPurchaseOrder_InsertPurchaseOrderPayment, ClientScreen.Stock_PO, InParam)
        End Function
        Public Function UpdatePurchaseOrder_Txn(InParam As Param_Update_PurchaseOrder_Txn) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockPurchaseOrder_UpdatePurchaseOrder_Txn, ClientScreen.Stock_PO, InParam)
        End Function
        Public Function UpdatePurchaseOrderStatus(UpParam As Param_UpdatePurchaseOrderStatus) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockPurchaseOrder_UpdatePurchaseOrderStatus, ClientScreen.Stock_PO, UpParam)
        End Function
        Public Function DeletePurchaseOrder_Txn(PO_ID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockPurchaseOrder_DeletePurchaseOrder_Txn, ClientScreen.Stock_PO, PO_ID)
        End Function
    End Class
    <Serializable>
    Public Class StockMachineToolAllocation
        Inherits SharedVariables
#Region "Return Classes"
        <Serializable>
        Public Class Return_GetRegister
            Public main_Register As List(Of Return_GetRegister_MainGrid)
            Public nested_Register As List(Of Return_GetRegister_NestedGrid)
            Public sub_nested_Register As List(Of Return_GetRegister_SubNestedGrid)
        End Class
        <Serializable>
        Public Class Return_GetRegister_MainGrid
            Public Property Issue_Date As DateTime
            Public Property Issued_To As String
            Public Property Gender As String
            Public Property Contractor As String
            Public Property Job As String
            Public Property Complex As String
            Public Property Allocation_Returned_Status As String
            Public Property Issued_By As String
            Public Property Allocation_Return_Remarks As String
            Public Property AddedBy As String
            Public Property AddedOn As DateTime
            Public Property EditedOn As DateTime
            Public Property EditedBy As String
            Public Property ID As Int32
            Public Property CurrUserRole As String
            Public Property Sr As Int32


        End Class
        <Serializable>
        Public Class Return_GetRegister_NestedGrid
            Public Property Mach_Tool_Name As String
            Public Property Mach_Tool_Code As String
            Public Property Qty_Allocated As Decimal
            Public Property Qty_Returned As Decimal
            Public Property ID As Int32
            Public Property MachineToolID As Int32
            Public Property IssueMainRecID As Int32
            Public Property IssueDate As DateTime
            Public Property IssuingStore As String
        End Class
        <Serializable>
        Public Class Return_GetRegister_SubNestedGrid
            Public Property Return_Qty As Decimal
            Public Property Return_Date As DateTime
            Public Property Penalty As Decimal
            Public Property Remarks As String
            Public Property ID As Int32
            Public Property IssueItemRecID As Int32
            Public Property MachineToolID As Int32
        End Class
        <Serializable>
        Public Class Return_GetMachToolIssueRemarks
            Inherits Jobs.Return_GetJobRemarks
        End Class
        <Serializable>
        Public Class Return_GetMachToolReturnRemarks
            Inherits Jobs.Return_GetJobRemarks
        End Class
        <Serializable>
        Public Class Return_GetPersonnels
            Inherits Projects.Return_GetProjectEnginners
        End Class
        <Serializable>
        Public Class Return_GetRecord
            Public Issue_Date As DateTime
            Public Issuing_Store_ID As Int32
            Public Issued_To_ID As Int32
            Public Issued_By_ID As Int32
            Public Job_ID As Int32?
            Public Usage_Site_ID As Int32?
            ' Public Remarks As String
            Public Add_On As DateTime
            Public Add_By As String
            Public Edit_On As DateTime
            Public Edit_By As String
            Public ID As Int32
            Public IssuedItemGridData As List(Of Return_GetRecord_IssuedItemGridData)
        End Class
        <Serializable>
        Public Class Return_GetReturnRecord
            Public Issue_ID As Int32
            Public Return_Date As Date
            Public Returned_Qty As Decimal
            Public Add_On As DateTime
            Public Add_By As String
            Public Edit_On As DateTime
            Public Edit_By As String
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Return_GetRecord_IssuedItemGridData
            Public Property Machine_Tool_Name As String
            Public Property ToolStockID As Int32
            Public Property Qty_Issued As Decimal
            Public Property ID As Int32
            Public Property Sr As Int32
        End Class
        <Serializable>
        Public Class Return_get_store_Pending_Returns
            Public Name As String
            Public Machine_Code As String
            Public Issue_Date As DateTime
            Public Issued_Qty As Decimal
            Public Issued_To As String
            Public Pending_Qty As Decimal
            Public Usage_Site As String
            Public Job As String
            Public IssueID As Int32
            Public IssueItemID As Int32
            Public MachineToolID As Int32
            Public Issuing_Store_ID As Int32
            Public Sr As Int32
        End Class
#End Region

        Public Sub New(ByVal _cBase As Common)
            MyBase.New(_cBase)
        End Sub
        Public Function GetMachToolIssueRemarks(MachineToolIssueID As Integer) As List(Of Return_GetMachToolIssueRemarks)
            Dim _Remarks_Param As New Common_Lib.RealTimeService.Param_GetStockRemarks
            _Remarks_Param.RefID = MachineToolIssueID
            _Remarks_Param.Screen_Type = "MachineToolIssue"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockRemarks, ClientScreen.Stock_Project, _Remarks_Param)
            Dim _Remarks_data As List(Of Return_GetMachToolIssueRemarks) = New List(Of Return_GetMachToolIssueRemarks)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetMachToolIssueRemarks
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.Remarks_By = row.Field(Of String)("REMARKS_BY")
                    newdata.Remarks_By_Designation = row.Field(Of String)("ADD_DESIGNATION")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    _Remarks_data.Add(newdata)
                Next
            End If
            Return _Remarks_data
        End Function
        Public Function GetRegister(fromDate As DateTime, toDate As DateTime) As Return_GetRegister
            Dim InParam As New Param_GetMachineToolRegister
            InParam.From_Date = fromDate
            InParam.ToDate = toDate
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockMachineToolAllocation_GetRegister, ClientScreen.Stock_Machine_Tool_Issue, InParam)

            Dim _main_data As New Return_GetRegister
            Dim _mt_main_data As List(Of Return_GetRegister_MainGrid) = New List(Of Return_GetRegister_MainGrid)
            Dim _mt_nested_data As List(Of Return_GetRegister_NestedGrid) = New List(Of Return_GetRegister_NestedGrid)
            Dim _mt_sub_nested_data As List(Of Return_GetRegister_SubNestedGrid) = New List(Of Return_GetRegister_SubNestedGrid)
            _main_data.main_Register = _mt_main_data
            _main_data.nested_Register = _mt_nested_data
            _main_data.sub_nested_Register = _mt_sub_nested_data

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                For Each row As DataRow In retDataset.Tables(0).Rows
                    Dim newdata = New Return_GetRegister_MainGrid
                    newdata.Issue_Date = row.Field(Of DateTime)("Issue_Date")
                    newdata.Issued_To = row.Field(Of String)("Issued_To")
                    newdata.Gender = row.Field(Of String)("Gender")
                    newdata.Contractor = row.Field(Of String)("Contractor")
                    newdata.Job = row.Field(Of String)("Job")
                    newdata.Complex = row.Field(Of String)("Complex")
                    newdata.Allocation_Returned_Status = row.Field(Of String)("Allocation_Returned_Status")
                    newdata.Issued_By = row.Field(Of String)("Issued_By")
                    newdata.Allocation_Return_Remarks = row.Field(Of String)("Allocation_Return_Remarks")
                    newdata.AddedOn = row.Field(Of DateTime)("AddedOn")
                    newdata.AddedBy = row.Field(Of String)("AddedBy")
                    newdata.EditedOn = row.Field(Of DateTime)("EditedOn")
                    newdata.EditedBy = row.Field(Of String)("EditedBy")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.CurrUserRole = row.Field(Of String)("CurrUserRole")
                    _mt_main_data.Add(newdata)
                Next
                'Nested Grid 
                For Each row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetRegister_NestedGrid
                    subdata.Mach_Tool_Name = row.Field(Of String)("Mach_Tool_Name")
                    subdata.Mach_Tool_Code = row.Field(Of String)("Mach_Tool_Code")
                    subdata.Qty_Allocated = row.Field(Of Decimal)("Qty_Allocated")
                    subdata.Qty_Returned = row.Field(Of Decimal?)("Qty_Returned")
                    subdata.ID = row.Field(Of Int32)("ID")
                    subdata.MachineToolID = row.Field(Of Int32)("MachineToolID")
                    subdata.IssueMainRecID = row.Field(Of Int32)("IssueMainRecID")
                    subdata.IssueDate = row.Field(Of DateTime)("IssueDate")
                    subdata.IssuingStore = row.Field(Of String)("IssuingStore")
                    _mt_nested_data.Add(subdata)
                Next
                'Sub Nested Grid 
                For Each row As DataRow In retDataset.Tables(2).Rows
                    Dim subdata = New Return_GetRegister_SubNestedGrid
                    subdata.Return_Qty = row.Field(Of Decimal)("Return_Qty")
                    subdata.Return_Date = row.Field(Of DateTime)("Return_Date")
                    subdata.Penalty = row.Field(Of Decimal)("Penalty")
                    subdata.Remarks = row.Field(Of String)("Remarks")
                    subdata.ID = row.Field(Of Int32)("ID")
                    subdata.IssueItemRecID = row.Field(Of Int32)("IssueItemRecID")
                    subdata.MachineToolID = row.Field(Of Int32)("MachineToolID")
                    _mt_sub_nested_data.Add(subdata)
                Next
            End If
            Return _main_data
        End Function
        Public Function GetPersonnels(StoreID As Int32) As List(Of Return_GetPersonnels)
            Dim _param As New Param_GetStockPersonnels()
            _param.Store_Dept_ID = StoreID
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockPersonnel, ClientScreen.Stock_Machine_Tool_Issue, _param)
            Dim _Estimator_data As List(Of Return_GetPersonnels) = New List(Of Return_GetPersonnels)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetPersonnels
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Type = row.Field(Of String)("Type")
                    newdata.Main_Dept = row.Field(Of String)("MAIN_DEPT")
                    newdata.Sub_Dept = row.Field(Of String)("SUB_DEPT")
                    newdata.Skill_Type = row.Field(Of String)("Skill_Type")
                    newdata.ID = row.Field(Of Integer)("ID")
                    _Estimator_data.Add(newdata)
                Next
            End If
            Return _Estimator_data
        End Function
        Public Function Get_MachineToolStock(StoreID As Int32) As List(Of DbOperations.StockProfile.Return_Get_Stocks_Listing)
            Dim _Param As New Common_Lib.RealTimeService.Param_Get_Stocks_Listing
            _Param.Stock_Consumtion_Type = "Non-Consumable"
            _Param.StoreID = StoreID
            _Param.CurrStockOnly = True
            Dim _Profile As New StockProfile(cBase)
            Dim _RetMachineToolList As List(Of DbOperations.StockProfile.Return_Get_Stocks_Listing) = _Profile.Get_Stocks_Listing(Common_Lib.RealTimeService.ClientScreen.Stock_Machine_Tool_Issue, _Param)


            Return _RetMachineToolList
        End Function
        Public Function GetRecord(MachineToolIssueID As Int32) As Return_GetRecord
            Dim retDataset As DataSet = GetDatasetOfRecordsBySP(RealServiceFunctions.StockMachineToolAllocation_GetRecord, ClientScreen.Stock_Machine_Tool_Issue, MachineToolIssueID)

            Dim _main_data As New Return_GetRecord
            Dim _IssuedItemGridData As List(Of Return_GetRecord_IssuedItemGridData) = New List(Of Return_GetRecord_IssuedItemGridData)
            _main_data.IssuedItemGridData = _IssuedItemGridData

            If (Not (retDataset) Is Nothing) Then
                'Main grid
                Dim row As DataRow = retDataset.Tables(0).Rows(0)
                _main_data.Issue_Date = row.Field(Of DateTime)("STA_Date")
                _main_data.Issuing_Store_ID = row.Field(Of Int32)("STA_Issuing_Store_ID")
                _main_data.Issued_To_ID = row.Field(Of Int32)("STA_Issued_to_ID")
                _main_data.Issued_By_ID = row.Field(Of Int32)("STA_Issued_by_ID")
                _main_data.Job_ID = row.Field(Of Int32?)("STA_Job_ID")
                _main_data.Usage_Site_ID = row.Field(Of Int32?)("STA_Usage_Site_ID")
                ' _main_data.Remarks = row.Field(Of String)("STA_Remarks")
                _main_data.Add_On = row.Field(Of DateTime)("REC_ADD_ON")
                _main_data.Edit_On = row.Field(Of DateTime)("REC_EDIT_ON")
                _main_data.Add_By = row.Field(Of String)("REC_ADD_BY")
                _main_data.Edit_By = row.Field(Of String)("REC_EDIT_BY")
                _main_data.ID = row.Field(Of Int32)("REC_ID")

                'Issued Item Grid 
                For Each _row As DataRow In retDataset.Tables(1).Rows
                    Dim subdata = New Return_GetRecord_IssuedItemGridData
                    subdata.Machine_Tool_Name = _row.Field(Of String)("Sub_Item_Name")
                    subdata.ToolStockID = _row.Field(Of Int32)("STI_Stock_ID")
                    subdata.Qty_Issued = _row.Field(Of Decimal)("STI_Issued_Qty")
                    subdata.ID = _row.Field(Of Int32)("REC_ID")
                    _IssuedItemGridData.Add(subdata)
                Next

            End If
            Return _main_data
        End Function
        Public Function GetReturnRecord(MachineToolReturnID As Int32) As Return_GetReturnRecord
            Dim retTable As DataTable = GetRecordByID(MachineToolReturnID.ToString(), ClientScreen.Stock_Machine_Tool_Issue, Tables.STOCK_TOOLS_RETURN_INFO, Common.ClientDBFolderCode.DATA)
            Dim _main_data As New Return_GetReturnRecord
            If (Not (retTable) Is Nothing) Then
                'Main grid
                Dim row As DataRow = retTable.Rows(0)
                _main_data.Issue_ID = row.Field(Of Int32)("STR_Issue_Item_ID")
                _main_data.Return_Date = row.Field(Of DateTime)("STR_Return_Date")
                _main_data.Returned_Qty = row.Field(Of Decimal)("STR_Returned_Qty")
                _main_data.Add_On = row.Field(Of DateTime)("REC_ADD_ON")
                _main_data.Edit_On = row.Field(Of DateTime)("REC_EDIT_ON")
                _main_data.Add_By = row.Field(Of String)("REC_ADD_BY")
                _main_data.Edit_By = row.Field(Of String)("REC_EDIT_BY")
                _main_data.ID = row.Field(Of Int32)("REC_ID")
            End If
            Return _main_data
        End Function
        Public Function GetPendingReturns(StoreID As Int32) As List(Of Return_get_store_Pending_Returns)
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.StockMachineToolAllocation_GetPendingReturns, ClientScreen.Stock_Machine_Tool_Issue, StoreID)
            Dim _Sub_Item_data As List(Of Return_get_store_Pending_Returns) = New List(Of Return_get_store_Pending_Returns)
            If (Not (retTable) Is Nothing) Then
                Dim i = 1
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_get_store_Pending_Returns
                    newdata.Name = row.Field(Of String)("Name")
                    newdata.Machine_Code = row.Field(Of String)("Machine_Code")
                    newdata.Issue_Date = row.Field(Of DateTime)("Issue_Date")
                    newdata.Issued_Qty = row.Field(Of Decimal)("Issued_Qty")
                    newdata.Issued_To = row.Field(Of String)("Issued_To")
                    newdata.Pending_Qty = row.Field(Of Decimal)("Pending_Qty")
                    newdata.Usage_Site = row.Field(Of String)("Usage_Site")
                    newdata.Job = row.Field(Of String)("Job")
                    newdata.IssueID = row.Field(Of Int32)("IssueID")
                    newdata.IssueItemID = row.Field(Of Int32)("IssueItemID")
                    newdata.MachineToolID = row.Field(Of Int32)("MachineToolID")
                    newdata.Issuing_Store_ID = row.Field(Of Int32)("Issuing_Store_ID")
                    newdata.Sr = i
                    i = i + 1
                    _Sub_Item_data.Add(newdata)
                Next
            End If
            Return _Sub_Item_data
        End Function
        Public Function GetMachToolReturnRemarks(MachineToolReturnID As Integer) As List(Of Return_GetMachToolReturnRemarks)
            Dim _Remarks_Param As New Common_Lib.RealTimeService.Param_GetStockRemarks
            _Remarks_Param.RefID = MachineToolReturnID
            _Remarks_Param.Screen_Type = "MachineToolReturn"
            Dim retTable As DataTable = GetListOfRecordsBySP(RealServiceFunctions.Projects_GetStockRemarks, ClientScreen.Stock_Project, _Remarks_Param)
            Dim _Remarks_data As List(Of Return_GetMachToolReturnRemarks) = New List(Of Return_GetMachToolReturnRemarks)
            If (Not (retTable) Is Nothing) Then
                For Each row As DataRow In retTable.Rows
                    Dim newdata = New Return_GetMachToolReturnRemarks
                    newdata.Sr_No = row.Field(Of Int64)("SR_NO")
                    newdata.Remarks = row.Field(Of String)("REMARKS")
                    newdata.Remarks_By = row.Field(Of String)("REMARKS_BY")
                    newdata.Remarks_By_Designation = row.Field(Of String)("ADD_DESIGNATION")
                    newdata.ID = row.Field(Of Integer)("ID")
                    newdata.Added_On = row.Field(Of DateTime)("REC_ADD_ON")
                    _Remarks_data.Add(newdata)
                Next
            End If
            Return _Remarks_data
        End Function
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="IssueItemRecID">Contains IssueItem RecID for which return is posted, so that the same issue is excluded from resultset</param>
        ''' <param name="IssuedMachineToolID"> ID of Stock row for MAchine Tool Issued </param>
        ''' <returns></returns>
        Public Function GetMachineToolIssueCount(IssueItemRecID As Int32, IssuedMachineToolID As Int32) As Integer
            Dim inparam As New Param_GetMachineToolIssueCount()
            inparam.IssueItemRecID = IssueItemRecID
            inparam.IssuedMachineToolID = IssuedMachineToolID
            Return CInt(GetSingleValue_Data(RealTimeService.RealServiceFunctions.StockMachineToolAllocation_GetMachineToolIssueCount, ClientScreen.Stock_Machine_Tool_Issue, inparam))
        End Function
        Public Function InsertMachineToolIssue(Inparam As Param_Insert_MachineTool_Issue) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.StockMachineToolAllocation_InsertMachineToolIssue, ClientScreen.Stock_Machine_Tool_Issue, Inparam)
        End Function
        Public Function InsertMachineToolReturn(Inparam As Param_Insert_MachineTool_Return) As Boolean
            Return InsertRecord(RealTimeService.RealServiceFunctions.StockMachineToolAllocation_InsertMachineToolReturn, ClientScreen.Stock_Machine_Tool_Issue, Inparam)
        End Function
        Public Function UpdateMachineToolIssue(Inparam As Param_Update_MachineTool_Issue) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockMachineToolAllocation_UpdateMachineToolIssue, ClientScreen.Stock_Machine_Tool_Issue, Inparam)
        End Function
        Public Function UpdateMachineToolReturn(Inparam As Param_Update_MachineTool_Return) As Boolean
            Return UpdateRecord(RealTimeService.RealServiceFunctions.StockMachineToolAllocation_UpdateMachineToolReturn, ClientScreen.Stock_Machine_Tool_Issue, Inparam)
        End Function
        Public Function DeleteMachineToolIssue(MachineToolIssueID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockMachineToolAllocation_DeleteMachineToolIssue, ClientScreen.Stock_Machine_Tool_Issue, MachineToolIssueID)
        End Function
        Public Function DeleteMachineToolReturn(MachineToolReturnID As Int32) As Boolean
            Return ExecuteGroup(RealTimeService.RealServiceFunctions.StockMachineToolAllocation_DeleteMachineToolReturn, ClientScreen.Stock_Machine_Tool_Issue, MachineToolReturnID)
        End Function
    End Class

#End Region
End Class