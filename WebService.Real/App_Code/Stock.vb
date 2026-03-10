Imports System.Data
Imports Microsoft.VisualBasic
Namespace Real
    <Serializable>
    Public Class StockProfile
        <Serializable>
        Public Enum Stock_Addition_Source
            Asset_Transfer
            Payment_Voucher
            Production_Produced
            Production_Scrap
            Purchase_Order_Received
            Stock_Profile
            User_Order_Scrap_Screen
        End Enum
#Region "Param classes"
        <Serializable>
        Public Class Param_Update_StockProject
            Public StockID As Integer
            Public ProjectID As Integer
            'Public RequestorID As String
            Public TransferQty As Decimal
            Public TransferRemarks As String
        End Class
        <Serializable>
        Public Class Param_Update_StockLocation
            Public StockID As Integer
            Public LocationID As String
            Public UpdationRemarks As String
            Public RequestorID As Integer
            Public Location_Change_Date As DateTime
        End Class
        <Serializable>
        Public Class Param_Get_StockDuplication
            Public StockItemID As String
            Public Make As String
            Public Model As String
            Public Lot_Serial_No As String
        End Class
        <Serializable>
        Public Class Param_Add_StockProfile
            Public Store_Dept_ID As Int32
            Public item_id As Int32
            Public make As String
            Public model As String
            Public serial_no As String
            Public Quantity As Double
            Public Unit_Id As String
            Public Date_Of_Purchase As DateTime? = Nothing
            Public total_value As Double
            Public Location_Id As String
            Public Project_ID As Integer? = Nothing
            Public Warranty As String
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Add_Stock_Addition
            Inherits Param_Add_StockProfile
            Public Stock_TR_ID As String
            Public Stock_Tr_Sr_No As Int32?
            Public Stock_Ref_ID As Int32?
            Public Stock_Ref_Entry_Source As Stock_Addition_Source = Stock_Addition_Source.Stock_Profile
            Public Stock_Ref_Sub_ID As Int32?
        End Class
        <Serializable>
        Public Class Param_Update_StockProfile
            Public Store_Dept_ID As Int32
            Public sub_Item_ID As Int32
            Public make As String
            Public model As String
            Public serial_no As String
            Public Quantity As Double
            Public Unit_Id As String
            Public Date_Of_Purchase As DateTime? = Nothing
            Public total_value As Double
            Public Location_Id As String
            Public Project_ID As Integer? = Nothing
            Public Warranty As String
            Public Remarks As String
            Public StockID As String
            Public Rec_ID As Integer
            'Public Stock_TR_ID As String
            'Public Stock_TR_Sr_No As Integer
        End Class

        'Public Class Param_Update_Stock_Addition
        '    Inherits Param_Update_StockProfile
        '    Public Stock_TR_ID As String
        '    Public Stock_Tr_Sr_No As Int32?
        '    Public Stock_Ref_ID As Int32?
        '    Public Stock_Ref_Entry_Source As Stock_Addition_Source = Stock_Addition_Source.Stock_Profile
        '    Public Stock_Ref_Sub_ID As Int32?
        'End Class
        <Serializable>
        Public Class Param_Get_Stocks_Listing
            Public CurrStockOnly As Boolean = False
            Public StockType As String
            Public ItemID As Int32?
            Public StoreID As Int32?
            Public Stock_Consumtion_Type As String
            Public ProjID As Int32?
        End Class
#End Region
        Public Shared Function GetStockItems(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Stock_Items"
            Dim params() As String = {"user_id"}
            Dim values() As Object = {inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStockUsage(StockID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Stock_Usage"
            Dim params() As String = {"@StockID"}
            Dim values() As Object = {StockID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStockDuplication(inparam As Param_Get_StockDuplication, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COUNT(REC_ID) FROM STOCK_INFO WHERE Stock_sub_Item_ID = '" + inparam.StockItemID + "' AND Stock_Make = '" + inparam.Make + "' AND Stock_Model='" + inparam.Model + "' AND Stock_Lot_Serial_no = '" + inparam.Lot_Serial_No + "' "
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetProfiledata(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_stock_Profile_Register"
            Dim params() As String = {"Stock_CEN_ID", "Stock_YEAR_ID", "User_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetProfiledata(StockID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_stock_Profile_Register"
            Dim params() As String = {"Stock_CEN_ID", "Stock_YEAR_ID", "User_ID", "StockID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, StockID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_Stocks_Listing(inParam As Param_Get_Stocks_Listing, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Stocks_Listing"
            Dim params() As String = {"Stock_CEN_ID", "Stock_YEAR_ID", "User_ID", "StockType", "currStockOnly", "SubItemID", "StoreId", "Stock_Consumtion_Type", "ProjID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, inParam.StockType, inParam.CurrStockOnly, inParam.ItemID, inParam.StoreID, inParam.Stock_Consumtion_Type, inParam.ProjID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Boolean, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 255, 255, 2, 4, 4, 255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function UpdateStockProject(inparam As Param_Update_StockProject, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_Stock_Profile_Project"
            Dim params() As String = {"@Cen_ID", "@YearID", "@user_id", "@ProjectID", "@StockID", "@TransferQty", "@Remarks"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, inparam.ProjectID, inparam.StockID, inparam.TransferQty, inparam.TransferRemarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255, 4, 4, 19, 8000}
            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function DeleteStockProfile(StockID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Try
                Dim dbService As ConnectOneWS = New ConnectOneWS()
                Dim SPName As String = "sp_delete_Stock_profile"
                Dim params() As String = {"REC_ID"}
                Dim values() As Object = {StockID}
                Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
                Dim lengths() As Integer = {36}
                dbService.DeleteFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
                Return True
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function UpdateStockLocation(inparam As Param_Update_StockLocation, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_Stock_Profile_Location"
            Dim params() As String = {"@Cen_id", "@YearID", "@user_id", "@Stock_Location_ID", "@StockID", "@Remarks", "@RequestorID", "@Location_Change_Date"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inBasicParam.openUserID, inparam.LocationID, inparam.StockID, inparam.UpdationRemarks, inparam.RequestorID, inparam.Location_Change_Date}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.DateTime}
            Dim lengths() As Integer = {4, 4, 255, 36, 4, 255, 4, 12}
            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdateStockProfile(inparam As Param_Update_StockProfile, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "update_stock"
            Dim params() As String = {"@user_id", "@Stock_sub_Item_ID", "@Stock_Store_ID", "@Stock_Lot_Serial_no", "@Stock_Make", "@Stock_Model", "@Stock_Warranty", "@Stock_Unit_ID", "@Stock_Quantity", "@Stock_Date_Of_Purchase", "@Stock_Location_ID", "@Stock_Project_ID", "@Stock_Value", "@Stock_Remarks", "@REC_ID"}
            Dim values() As Object = {inBasicParam.openUserID, inparam.sub_Item_ID, inparam.Store_Dept_ID, inparam.serial_no, inparam.make, inparam.model, inparam.Warranty, inparam.Unit_Id, inparam.Quantity, inparam.Date_Of_Purchase, inparam.Location_Id, inparam.Project_ID, inparam.total_value, inparam.Remarks, inparam.Rec_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4, 50, 255, 255, 50, 36, 11, 7, 36, 4, 19, 8000, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        'Public Shared Function UpdateStockAddition(InParam As Param_Update_Stock_Addition, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim Entry_Source As String = Nothing
        '    Select Case InParam.Stock_Ref_Entry_Source

        '        Case Stock_Addition_Source.User_Order_Scrap_Screen
        '            Entry_Source = "UO - Scrap"

        '    End Select
        '    Dim SPName As String = "update_stock"
        '    Dim params() As String = {"@user_id", "@Stock_CEN_ID", "@Stock_YEAR_ID", "@Stock_sub_Item_ID", "@Stock_Store_ID", "@Stock_Lot_Serial_no", "@Stock_Make", "@Stock_Model", "@Stock_Warranty", "@Stock_Unit_ID", "@Stock_Quantity", "@Stock_Date_Of_Purchase", "@Stock_Location_ID", "@Stock_Project_ID", "@Stock_Value", "@Stock_Remarks", "@REC_ID", "@Stock_TR_ID", "@Stock_Tr_Sr_No", "@Stock_Ref_ID", "@Stock_Ref_Entry_Source", "@Stock_Ref_Sub_ID"}
        '    Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openCenID, inBasicParam.openYearID, InParam.sub_Item_ID, InParam.Store_Dept_ID, InParam.serial_no, InParam.make, InParam.model, InParam.Warranty, InParam.Unit_Id, InParam.Quantity, InParam.Date_Of_Purchase, InParam.Location_Id, InParam.Project_ID, InParam.total_value, InParam.Remarks, InParam.Rec_ID, InParam.Stock_TR_ID, InParam.Stock_Tr_Sr_No, InParam.Stock_Ref_ID, Entry_Source, InParam.Stock_Ref_Sub_ID}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
        '    Dim lengths() As Integer = {255, 4, 4, 4, 4, 50, 255, 255, 50, 36, 11, 7, 36, 4, 19, 8000, 4, 36, 4, 4, 50, 4}
        '    dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        'End Function
        Public Shared Function AddStockAddition(ByVal InParam As Param_Add_Stock_Addition, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Entry_Source As String = Nothing
            Select Case InParam.Stock_Ref_Entry_Source
                Case Stock_Addition_Source.Asset_Transfer
                    Entry_Source = "Asset Transfer"
                Case Stock_Addition_Source.Payment_Voucher
                    Entry_Source = "Payment Voucher"
                Case Stock_Addition_Source.Production_Produced
                    Entry_Source = "Production Produced"
                Case Stock_Addition_Source.Production_Scrap
                    Entry_Source = "Production - Scrap"
                Case Stock_Addition_Source.User_Order_Scrap_Screen
                    Entry_Source = "UO - Scrap"
                Case Stock_Addition_Source.Purchase_Order_Received
                    Entry_Source = "PO - Item Received"
            End Select
            Dim SPName As String = "Insert_Stock"
            Dim params() As String = {"@user_id", "@Stock_CEN_ID", "@Stock_YEAR_ID", "@Stock_sub_Item_ID", "@Stock_Store_ID", "@Stock_Lot_Serial_no", "@Stock_Make", "@Stock_Model", "@Stock_Warranty", "@Stock_Unit_ID", "@Stock_Quantity", "@Stock_Date_Of_Purchase", "@Stock_Location_ID", "@Stock_Project_ID", "@Stock_Value", "@Stock_Remarks", "@Stock_TR_ID", "@Stock_Tr_Sr_No", "@Stock_Ref_ID", "@Stock_Ref_Entry_Source", "@Stock_Ref_Sub_ID"}
            Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openCenID, inBasicParam.openYearID, InParam.item_id, InParam.Store_Dept_ID, InParam.serial_no, InParam.make, InParam.model, InParam.Warranty, InParam.Unit_Id, InParam.Quantity, InParam.Date_Of_Purchase, InParam.Location_Id, InParam.Project_ID, InParam.total_value, InParam.Remarks, InParam.Stock_TR_ID, InParam.Stock_Tr_Sr_No, InParam.Stock_Ref_ID, Entry_Source, InParam.Stock_Ref_Sub_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4, 4, 4, 50, 255, 255, 50, 36, 11, 7, 36, 4, 19, 8000, 36, 4, 4, 50, 4}
            Dim StockID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return StockID
        End Function
        Public Shared Function AddStockProfile(ByVal InParam As Param_Add_StockProfile, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Stock"
            Dim params() As String = {"@user_id", "@Stock_CEN_ID", "@Stock_YEAR_ID", "@Stock_sub_Item_ID", "@Stock_Store_ID", "@Stock_Lot_Serial_no", "@Stock_Make", "@Stock_Model", "@Stock_Warranty", "@Stock_Unit_ID", "@Stock_Quantity", "@Stock_Date_Of_Purchase", "@Stock_Location_ID", "@Stock_Project_ID", "@Stock_Value", "@Stock_Remarks"}
            Dim values() As Object = {inBasicParam.openUserID, inBasicParam.openCenID, inBasicParam.openYearID, InParam.item_id, InParam.Store_Dept_ID, InParam.serial_no, InParam.make, InParam.model, InParam.Warranty, InParam.Unit_Id, InParam.Quantity, InParam.Date_Of_Purchase, InParam.Location_Id, InParam.Project_ID, InParam.total_value, InParam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 4, 4, 4, 50, 255, 255, 50, 36, 11, 7, 36, 4, 19, 8000}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class StockTransferOrders
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="StockID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        Public Shared Function GetTransferOrders(StockID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_transfer_orders"
            Dim params() As String = {"user_id", "StockID", "CEN_ID"}
            Dim values() As Object = {inBasicParam.openUserID, StockID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_TRANSFER_ORDER, SPName, ConnectOneWS.Tables.STOCK_TRANSFER_ORDER.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

    End Class
    <Serializable>
    Public Class StockUserOrder
        <Serializable>
        Public Enum UO_Status
            _New
            Cancelled
            Rejected
            Changes_Recommended
            Requested
            Assigned_for_Estimation_Creation
            Submitted_for_Estimate_Approval
            Approved
            Assigned
            In_Progress
            Completed
        End Enum
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetUserOrderRegister
            Public From_Date As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_Update_UO_Status
            Public UpdatedStatus As UO_Status
            Public UOID As Int32
            Public Remarks As String

        End Class
        <Serializable>
        Public Class Param_Update_UO_Scheduled_Delivery
            Public Scheduled_Delivery_Date As DateTime
            Public UO_ID As Int32
        End Class
        <Serializable>
        Public Class Param_GetUOItems
            Public UOID As Int32
            Public NotDeliveredOnly As Boolean = False
            Public UD_ID As Int32? = Nothing
        End Class
        <Serializable>
        Public Class Param_GetUOGoodsReceived
            Public UOID As Int32
            Public NotReturnedOnly As Boolean = False
            Public RetID As Int32? = Nothing
        End Class
        <Serializable>
        Public Class Param_GetUODeliveredItems
            Public UOID As Int32
            Public Recd_ID As Int32? = Nothing
        End Class
        <Serializable>
        Public Class Param_GetUORetReceivableItems
            Public UOID As Int32
            Public RetRecddID As Int32? = Nothing
        End Class
        'Public Class Param_GetUOGoodsReturned
        '    Public UOID As Int32
        '    Public NotReceivedOnly As Boolean = False
        '    Public IncludeDelivery As Int32
        'End Class
        <Serializable>
        Public Class Param_UO_Insert_UO_RR_Mapping
            Public UO_ID As Int32
            Public RR_ID As Int32
            Public UO_Item_ID As Int32?
        End Class

        Public Class Param_Get_UOItem_ForRR
            Public UO_ID As Int32
            Public SubItemID As String

        End Class
        <Serializable>
        Public Class Param_Get_Stock_Availability
            Public ItemID As Int32
            Public ItemType As String
            Public Center As Int32?
            Public StoreDeptID As Int32?
            Public LocationID As String

        End Class
        <Serializable>
        Public Class Param_GetStoreList_StockAvailability
            Public DeptID As Int32?
            Public CEN_ID As Int32?
            Public GetAllStores As Boolean = False
        End Class
        <Serializable>
        Public Class Param_GetStoreLocations_StockAvailability
            Public StoreID As Int32?
            Public CEN_ID As Int32?
        End Class
        <Serializable>
        Public Class Param_Get_RR_Details_ForUOmapping
            Public UO_ID As Int32
            Public UO_Item_ID As String
        End Class
        <Serializable>
        Public Class Param_UO_RR_Unmapping
            Public UO_ID As Int32
            Public Mapped_RR_ID As Int32?
            Public UO_Item_ID As Int32?
        End Class
        <Serializable>
        Public Class Param_Insert_UO_Txn
            Inherits Param_Insert_UO
            Public InAttachment As Attachments.Parameter_Insert_Attachment()
            Public InUOItems As Param_Insert_UO_Item()
            Public InUODeliveries As Param_Insert_UO_Item_Delivered()
            Public InUOReceipts As Param_Insert_UO_Item_Received()
            Public InUOReturns As Param_Insert_UO_Item_Returned()
            Public InUORerturnReceipts As Param_Insert_UO_Item_Return_Received()
            Public InUOScrapCreated As StockProfile.Param_Add_Stock_Addition()
        End Class
        <Serializable>
        Public Class Param_Insert_UO
            Public UO_Initiated_Mode As String
            Public UO_Date As DateTime
            Public UO_Job_ID As Int32
            Public UO_store_ID As Int32
            Public UO_Requestor_ID As Int32
            Public UO_Requestor_Main_Dept_Id As Int32?
            Public UO_Requestor_Sub_Dept_Id As Int32?
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_UO_Item
            Public UO_ID As Int32
            Public Sub_Item_ID As Int32
            Public Make As String
            Public Model As String
            Public Unit_ID As String
            Public Requested_Qty As Decimal
            Public Required_Date As DateTime
            Public Scheduled_Delivery_Date As DateTime?
            Public Priority As String
            Public Part_Delivery_Allowed As Boolean?
            Public Remarks As String
            Public Delivery_Location_Id As String
            Public Approved_Qty As Decimal?
        End Class
        <Serializable>
        Public Class Param_Insert_UO_Item_Delivered
            Public UO_ID As Int32
            Public UO_Item_ID As Int32
            Public Delivered_Qty As Decimal
            Public Delivery_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Delivery_Location_ID As String
            Public Delivered_By_ID As Int32?
            Public Delivery_Driver_ID As Int32?
            Public Delivery_Receiver_Name As String
            Public Delivery_Remarks As String
            Public VehicleNo As String
            Public _Delivered_Stock As List(Of Param_Insert_UO_Item_Delivered_Stocks)
        End Class
        <Serializable>
        Public Class Param_Insert_UO_Item_Delivered_Stocks
            Public UO_ID As Int32
            Public UD_ID As Int32
            Public Stock_ID As Int32
            Public UDS_Qty As Decimal


        End Class
        <Serializable>
        Public Class Param_Insert_UO_Item_Received
            Public UO_ID As Int32
            Public UO_Delivered_ID As Int32?
            Public Recd_Qty As Decimal
            Public Recd_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Received_Location_ID As String
            Public Bill_No As String
            Public Challan_No As String
            Public Received_By_ID As Int32?
            Public Receiver_Remarks As String
            Public UO_Returned_ID As Int32?
            Public UDS_ID As Int32
        End Class
        <Serializable>
        Public Class Param_Insert_UO_Item_Returned
            Public UO_ID As Int32
            Public Recd_Item_ID As Int32
            Public Returned_Qty As Decimal
            Public Returned_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Returned_Location_ID As String
            Public Returned_By_ID As Int32?
            Public Returned_by_Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_UO_Item_Return_Received
            Public UO_ID As Int32
            Public UO_Returned_ID As Int32?
            Public Received_Qty As Decimal
            Public Received_Date As DateTime
            Public Received_Mode_ID As String
            Public Delivery_Carrier As String
            Public Ret_Rec_Location_ID As String
            Public Received_By_ID As Int32?
            Public Receiver_Remarks As String
            Public UO_Delivered_ID As Int32?
            Public URR_UDS_ID As Int32
        End Class
        <Serializable>
        Public Class Param_InsertUORemarks
            Public Remarks As String
            Public UO_ID As Int32
        End Class
        <Serializable>
        Public Class Param_GetUOGoodsDeliverAllPending
            Public UO_ID As Int32
            Public Delivery_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Delivery_Location_ID As String
            Public Delivered_By_ID As Int32?
            Public Delivery_Driver_ID As Int32?
            Public Delivery_Receiver_Name As String
            Public Delivery_Remarks As String
            Public VehicleNo As String
        End Class
        <Serializable>
        Public Class Param_GetUOGoodsDeliverSelected
            Public UO_ID As Int32
            Public UO_Item_ID As String
            Public Delivery_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Delivery_Location_ID As String
            Public Delivered_By_ID As Int32?
            Public Delivery_Driver_ID As Int32?
            Public Delivery_Receiver_Name As String
            Public Delivery_Remarks As String
            Public VehicleNo As String
        End Class
        <Serializable>
        Public Class Param_GetUOGoodsReceiveAllPending
            Public UO_ID As Int32
            'Public UO_Delivered_ID As Int32
            Public Recd_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Received_Location_ID As String
            Public Bill_No As String
            Public Challan_No As String
            Public Received_By_ID As Int32?
            Public Receiver_Remarks As String
        End Class
        <Serializable>
        Public Class Param_GetUOGoodsReceiveSelected
            Public UO_ID As Int32
            Public UO_Item_ID As String
            Public Recd_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Received_Location_ID As String
            Public Bill_No As String
            Public Challan_No As String
            Public Received_By_ID As Int32?
            Public Receiver_Remarks As String
        End Class
        <Serializable>
        Public Class Param_GetUOGoodsReturnAllPending
            Public UO_ID As Int32
            'Public Recd_Item_ID As Int32
            Public Returned_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Returned_Location_ID As String
            Public Returned_By_ID As Int32?
            Public Returned_by_Remarks As String
        End Class
        <Serializable>
        Public Class Param_Get_UO_Goods_Delivery_Stocks
            Public UO_ID As Int32
            Public Sub_Item_ID As Int32
            Public DeliveredRecID As Int32?
            Public Rec_ID As Int32


        End Class
        <Serializable>
        Public Class Param_Update_UO_Txn
            Inherits Param_Update_UO
            Public Remarks As String
            '==Additions
            ''' <summary>
            ''' Array of Attachments added during Update of UO
            ''' </summary>
            Public Added_Attachments As Attachments.Parameter_Insert_Attachment()
            ''' <summary>
            ''' Array of Items added during Update of UO
            ''' </summary>
            Public InUOItems As Param_Insert_UO_Item()
            ''' <summary>
            ''' Array of Goods Deliveries added during Update of UO
            ''' </summary>
            Public InUODeliveries As Param_Insert_UO_Item_Delivered()
            ''' <summary>
            ''' Array of Goods Receipts added during Update of UO
            ''' </summary>
            Public InUOReceipts As Param_Insert_UO_Item_Received()
            ''' <summary>
            ''' Array of Goods Returns added during Update of UO
            ''' </summary>
            Public InUOReturns As Param_Insert_UO_Item_Returned()
            ''' <summary>
            ''' Array of Goods Returns Received back added during Update of UO
            ''' </summary>
            Public InUORerturnReceipts As Param_Insert_UO_Item_Return_Received()
            ''' <summary>
            ''' Array of Scrap added during Update of UO
            ''' </summary>
            Public InUOScrapCreated As StockProfile.Param_Add_Stock_Addition()
            '==Updates
            ''' <summary>
            ''' Array of Documents updated during Update of UO
            ''' </summary>
            Public Updated_Attachments As Attachments.Parameter_Update_Attachment()
            ''' <summary>
            ''' Array of Items updated during Update of UO
            ''' </summary>
            Public UpdateUOItems As Param_Update_UO_Item()
            ''' <summary>
            ''' Array of Goods Deliveries Updated during Update of UO
            ''' </summary>
            Public UpdateUODeliveries As Param_Update_UO_Delivery()
            ''' <summary>
            ''' Array of Goods Receipts Updated during Update of UO
            ''' </summary>
            Public UpdateUOReceipts As Param_Update_UO_Received()
            ''' <summary>
            ''' Array of Goods Returns Updated during Update of UO
            ''' </summary>
            Public UpdateUOReturns As Param_Update_UO_Returned()
            ''' <summary>
            ''' Array of Goods Returns Received back Updated during Update of UO
            ''' </summary>
            Public UpdateUORerturnReceipts As Param_Update_UO_Return_Received()
            ''' <summary>
            ''' Array of Scrap Updated during Update of UO
            ''' </summary>
            Public UpdateUOScrapCreated As StockProfile.Param_Update_StockProfile()

            '==Deletions
            ''' <summary>
            ''' Array of Remarks Deleted during Update of UO
            ''' </summary>
            Public Deleted_UORemarks As Param_Deleted_UO_Remarks()
            ''' <summary>
            ''' Array of Documents Deleted during Update of UO
            ''' </summary>
            Public Deleted_UOAttachments As Param_Deleted_UO_Attachments()
            ''' <summary>
            ''' Array of Documents unlinked from Project during Update of UO
            ''' </summary>
            Public Unlinked_UOAttachments As Param_Unlinked_UO_Attachments()
            ''' <summary>
            ''' Array of Requested Item Deleted during Update of UO
            ''' </summary>
            Public Deleted_UO_Items As Param_Deleted_UO_Items()
            ''' <summary>
            ''' Array of Requested Item Deliveries Deleted during Update of UO
            ''' </summary>
            Public Deleted_UO_Items_Delivered As Param_Deleted_UO_Items_Delivered()
            ''' <summary>
            ''' Array of Item Receipts Deleted during Update of UO
            ''' </summary>
            Public Deleted_UO_Items_Receipts As Param_Deleted_UO_Items_Received()
            ''' <summary>
            ''' Array of Item Returns Deleted during Update of UO
            ''' </summary>
            Public Deleted_UO_Items_Returns As Param_Deleted_UO_Items_Returned()
            ''' <summary>
            ''' Array of Item Return Receipts Deleted during Update of UO
            ''' </summary>
            Public Deleted_UO_Items_Return_Receipts As Param_Deleted_UO_Items_ReturnReceived()
            ''' <summary>
            ''' Array of Scraps Deleted during Update of UO
            ''' </summary>
            Public Deleted_UO_Scrap As Param_Deleted_UO_Scrap()
        End Class
        <Serializable>
        Public Class Param_Update_UO
            Public UO_Initiated_Mode As String
            Public UO_Date As DateTime
            Public UO_Job_ID As Int32
            Public UO_store_ID As Int32
            Public UO_Requestor_ID As Int32
            Public UO_Requestor_Main_Dept_Id As Int32?
            Public UO_Requestor_Sub_Dept_Id As Int32?
            Public UOID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_UO_Item
            Public UO_ID As Int32
            Public Sub_Item_ID As Int32
            Public Make As String
            Public Model As String
            Public Unit_ID As String
            Public Requested_Qty As Decimal
            Public Required_Date As DateTime
            Public Scheduled_Delivery_Date As DateTime?
            Public Priority As String
            Public Part_Delivery_Allowed As Boolean?
            Public Remarks As String
            Public Delivery_Location_Id As String
            Public Approved_Qty As Decimal?
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_UO_Delivery
            Public UO_ID As Int32
            Public UO_Item_ID As Int32
            Public Delivered_Qty As Decimal
            Public Delivery_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Delivery_Location_ID As String
            Public Delivered_By_ID As Int32?
            Public Delivery_Driver_ID As Int32?
            Public Delivery_Receiver_Name As String
            Public Delivery_Remarks As String
            Public VehicleNo As String
            Public ID As Int32
            Public _Added_Delivered_Stock As List(Of Param_Insert_UO_Item_Delivered_Stocks)
            'Public _Updated_Delivered_Stock As List(Of Param_Update_UO_Delivery_Stocks)
            Public _Deleted_Delivered_Stock As List(Of Int32)
        End Class


        'Public Class Param_Update_UO_Delivery_Stocks
        '    Public UO_ID As Int32
        '    Public UD_ID As Int32
        '    Public Stock_ID As Int32
        '    Public UDS_Qty As Decimal
        '    Public UDS_ID As Int32
        'End Class
        <Serializable>
        Public Class Param_Update_UO_Received
            Public UO_ID As Int32
            Public UO_Delivered_ID As Int32?
            Public Recd_Qty As Decimal
            Public Recd_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Received_Location_ID As String
            Public Bill_No As String
            Public Challan_No As String
            Public Received_By_ID As Int32?
            Public Receiver_Remarks As String
            Public UO_Returned_ID As Int32?
            Public ID As Int32
            Public UDS_ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_UO_Returned
            Public UO_ID As Int32
            Public Recd_Item_ID As Int32
            Public Returned_Qty As Decimal
            Public Returned_Date As DateTime
            Public Delivery_Mode_ID As String
            Public Delivery_Carrier As String
            Public Returned_Location_ID As String
            Public Returned_By_ID As Int32?
            Public Returned_by_Remarks As String
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_UO_Return_Received
            Public UO_ID As Int32
            Public UO_Returned_ID As Int32?
            Public Received_Qty As Decimal
            Public Received_Date As DateTime
            Public Received_Mode_ID As String
            Public Delivery_Carrier As String
            Public Ret_Rec_Location_ID As String
            Public Received_By_ID As Int32?
            Public Receiver_Remarks As String
            Public UO_Delivered_ID As Int32?
            Public ID As Int32
            Public URR_UDS_ID As Int32
        End Class
        <Serializable>
        Public Class Param_Deleted_UO_Attachments
            Inherits Projects.PKs_String
        End Class
        <Serializable>
        Public Class Param_Unlinked_UO_Attachments
            Inherits Projects.PKs_String
        End Class
        <Serializable>
        Public Class Param_Deleted_UO_Items
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_UO_Items_Delivered
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_UO_Items_Received
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_UO_Items_Returned
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_UO_Items_ReturnReceived
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_UO_Scrap
            Inherits Projects.PKs_Int
        End Class
        <Serializable>
        Public Class Param_Deleted_UO_Remarks
            Inherits Projects.PKs_Int
        End Class
#End Region

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="StockID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        Public Shared Function GetDeliveries(StockID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_user_order_deliveries"
            Dim params() As String = {"user_id", "StockID", "CEN_ID"}
            Dim values() As Object = {inBasicParam.openUserID, StockID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, ConnectOneWS.Tables.USER_ORDER.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetRecord(UO_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT * from user_Order UO WHERE UO.REC_ID = " + UO_ID.ToString() + ""
            Return dbService.GetSingleRecord(ConnectOneWS.Tables.USER_ORDER, Query, ConnectOneWS.Tables.USER_ORDER.ToString(), inBasicParam)
        End Function
        Public Shared Function GetRegister(inParam As Param_GetUserOrderRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Register"
            Dim params() As String = {"FromDate", "ToDate", "Logged_User_ID", "CEN_ID"}
            Dim values() As Object = {inParam.From_Date, inParam.ToDate, inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 12, 255, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOItems(inparam As Param_GetUOItems, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Items"
            Dim params() As String = {"@UO_ID", "@Requested_NotDelivered_Only", "@UD_ID"}
            Dim values() As Object = {inparam.UOID, inparam.NotDeliveredOnly, inparam.UD_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 2, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUODetails(UOID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Details"
            Dim params() As String = {"@UO_ID"}
            Dim values() As Object = {UOID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, ConnectOneWS.Tables.USER_ORDER.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetUOGoodsReceived(inparam As Param_GetUOGoodsReceived, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Goods_Received"
            Dim params() As String = {"@UO_ID", "@ReceivedItem_Not_Returned_Only", "@RetID"}
            Dim values() As Object = {inparam.UOID, inparam.NotReturnedOnly, inparam.RetID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 2, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOGoodsReturned(UOID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Goods_Return"
            Dim params() As String = {"@UO_ID"}
            Dim values() As Object = {UOID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUORetReceivableItems(inparam As Param_GetUORetReceivableItems, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Ret_Receivable_Items"
            Dim params() As String = {"@UO_ID", "@RetRecddID"}
            Dim values() As Object = {inparam.UOID, inparam.RetRecddID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOScrapCreated(UOID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Scrap_Items"
            Dim params() As String = {"@Ref_ID", "@ScreenType"}
            Dim values() As Object = {UOID, "UO - Scrap"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 50}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOGoodsDelivered(UOID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Goods_Delivery"
            Dim params() As String = {"@UO_ID"}
            Dim values() As Object = {UOID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_UO_Delivered_Items(inparam As Param_GetUODeliveredItems, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Delivered_Items"
            Dim params() As String = {"@UO_ID", "@Recd_ID"}
            Dim values() As Object = {inparam.UOID, inparam.Recd_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOGoodsReturnReceived(UOID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Goods_ReturnReceived"
            Dim params() As String = {"@UO_ID"}
            Dim values() As Object = {UOID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURN_RECEIVED, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM_RETURN_RECEIVED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOGoodsDeliverAllPending(inparam As Param_GetUOGoodsDeliverAllPending, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Item_Deliver_All"
            Dim params() As String = {"@UO_ID", "@Delivery_Date", "@Delivery_Mode", "@Delivery_Carrier", "@Delivery_Location_ID", "@Delivered_By_ID", "@Delivery_Driver_ID", "@Delivery_Receiver_Name", "@Delivery_Remarks", "@User_ID", "@VehicleNo"}
            Dim values() As Object = {inparam.UO_ID, inparam.Delivery_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Delivery_Location_ID, inparam.Delivered_By_ID, inparam.Delivery_Driver_ID, inparam.Delivery_Receiver_Name, inparam.Delivery_Remarks, inBasicParam.openUserID, inparam.VehicleNo}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 14, 36, 100, 36, 4, 4, 100, 500, 255, 100}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetUOGoodsDeliverSelectedItems(inparam As Param_GetUOGoodsDeliverSelected, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Item_Deliver_Selected"
            Dim params() As String = {"@UO_ID", "@UOI_ID", "@Delivery_Date", "@Delivery_Mode", "@Delivery_Carrier", "@Delivery_Location_ID", "@Delivered_By_ID", "@Delivery_Driver_ID", "@Delivery_Receiver_Name", "@Delivery_Remarks", "@User_ID", "@VehicleNo"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Item_ID, inparam.Delivery_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Delivery_Location_ID, inparam.Delivered_By_ID, inparam.Delivery_Driver_ID, inparam.Delivery_Receiver_Name, inparam.Delivery_Remarks, inBasicParam.openUserID, inparam.VehicleNo}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, -1, 14, 36, 100, 36, 4, 4, 100, 500, 255, 100}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOGoodsReceiveAllPending(inparam As Param_GetUOGoodsReceiveAllPending, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Item_Receive_All"
            Dim params() As String = {"@UO_ID", "@Recd_Date", "@Delivery_Mode", "@Delivery_Carrier", "@Received_Location_ID", "@Bill_No", "@Challan_No", "@Received_By_ID", "@User_ID", "@Receiver_Remarks"}
            Dim values() As Object = {inparam.UO_ID, inparam.Recd_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Received_Location_ID, inparam.Bill_No, inparam.Challan_No, inparam.Received_By_ID, inBasicParam.openUserID, inparam.Receiver_Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 12, 36, 100, 36, 50, 50, 4, 255, 500}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetUOGoodsReceiveSelectedItems(inparam As Param_GetUOGoodsReceiveSelected, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Item_Receive_Selected"
            Dim params() As String = {"@UO_ID", "@UOI_ID", "@Recd_Date", "@Delivery_Mode", "@Delivery_Carrier", "@Received_Location_ID", "@Bill_No", "@Challan_No", "@Received_By_ID", "@User_ID", "@Receiver_Remarks"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Item_ID, inparam.Recd_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Received_Location_ID, inparam.Bill_No, inparam.Challan_No, inparam.Received_By_ID, inBasicParam.openUserID, inparam.Receiver_Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, -1, 12, 36, 100, 36, 50, 50, 4, 255, 500}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOGoodsReturnAllPending(inparam As Param_GetUOGoodsReturnAllPending, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Item_Return_All"
            Dim params() As String = {"@UO_ID", "@Returned_Date", "@Delivery_Mode", "@Delivery_Carrier", "@Returned_Location_ID", "@Returned_By_ID", "@Returned_by_Remarks", "@User_ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.Returned_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Returned_Location_ID, inparam.Returned_By_ID, inparam.Returned_by_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 12, 36, 100, 36, 4, 500, 255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, SPName, ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUO_RR_Count(UO_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COUNT(RRM_UO_ID) Cnt from Requisition_Request_Info rr INNER JOIN RR_UO_Mapping map on rr.REC_ID = map.RRM_RR_ID Where map.RRM_UO_ID = " + UO_ID.ToString() + " "
            Return dbService.GetScalar(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, Query, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUO_Scrap_Creation_Count(UO_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Count(Distinct T0.Stock_sub_Item_ID) Scrap_Created From Stock_info T0 Inner Join Sub_item_info T1 on T0.Stock_sub_Item_ID = T1.REC_ID and T1.Sub_Item_Main_Category = 'Scrap' Where T0.Stock_Ref_Entry_Source = 'UO - Scrap' AND T0.Stock_Ref_ID = " + UO_ID.ToString() + " "
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUO_DependentEntry_Count(UO_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Total_UO_depended"
            Dim params() As String = {"@UO_ID"}
            Dim values() As Object = {UO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUO_Job_Status(UO_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT Job_Status FROM Job_info WHERE REC_ID	IN (SELECT UO_Job_ID FROM User_Order WHERE REC_ID = " + UO_ID.ToString() + ")"
            Return dbService.GetScalar(ConnectOneWS.Tables.JOB_INFO, Query, ConnectOneWS.Tables.JOB_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUO_RR_Status(UO_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim Query As String = "SELECT RR_Status FROM Requisition_Request_Info WHERE RR_UO_ID = " + UO_ID.ToString() + " "
            Dim Query As String = "SELECT CASE When Count(DISTINCT A.RR_Status) = 0 Then 'New' Else 'NotNew' End RR_Status FROM Requisition_Request_Info A Inner Join RR_UO_Mapping A1 on A.REC_ID = A1.RRM_RR_ID WHERE A.RR_Status <> 'New' AND A1.RRM_UO_ID = " + UO_ID.ToString() + " "
            Return dbService.GetScalar(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, Query, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUO_Deliveries_notReturned_Count(UO_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Itm_Del_greater_Ret"
            Dim params() As String = {"@UO_ID"}
            Dim values() As Object = {UO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUOReqItem_Delivered_EntryCount(UO_ReqItemEntry_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE(Count(REC_ID),0) Del_ReqItem_Count From User_Order_Item_Delivered Where UD_UO_Item_ID = " + UO_ReqItemEntry_ID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, Query, ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUODelivery_Received_EntryCount(UO_Delivery_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT COALESCE(Del_From_Rec + Del_From_RetRec,0) Del_Rec_Count FROM (Select (Select COALESCE(Count(REC_ID),0) Del_Rec_Count from User_Order_Item_Received  Where UR_UO_Delivered_ID = " + UO_Delivery_ID.ToString() + ") Del_From_Rec , (Select COALESCE(COUNT(REC_ID),0) Del_Rec_Count From User_Order_Item_Return_Received Where URR_UO_Delivered_ID = " + UO_Delivery_ID.ToString() + ") Del_From_RetRec) A  "
            Return dbService.GetScalar(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, Query, ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUOReceipt_Return_EntryCount(UO_Received_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE(Count(REC_ID),0) Recd_Ret_Count From User_Order_Item_Returned Where UR_Recd_Item_ID = " + UO_Received_ID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, Query, ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUOReturn_Received_EntryCount(UO_Return_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = " SELECT COALESCE(Ret_From_Rec + Ret_From_RetRec,0) Ret_Rec_Count FROM (Select (Select COALESCE(Count(REC_ID),0) Ret_Rec_Count from User_Order_Item_Received Where UR_UO_Returned_ID =  " + UO_Return_ID.ToString() + ") Ret_From_Rec , (Select COALESCE(COUNT(REC_ID),0) Ret_Rec_Count From User_Order_Item_Return_Received Where URR_UO_Returned_ID = " + UO_Return_ID.ToString() + ") Ret_From_RetRec) A"
            Return dbService.GetScalar(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, Query, ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUO_Goods_In_Transit(UO_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Goods_In_Transit"
            Dim params() As String = {"@UO_ID"}
            Dim values() As Object = {UO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ScalarFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_UO_Goods_Delivery_Stocks(inparam As Param_Get_UO_Goods_Delivery_Stocks, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Goods_Delivery_Stocks"
            Dim params() As String = {"@UD_UO_ID", "@SubItemID", "@UO_DEL_ID", "@UOI_ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.Sub_Item_ID, inparam.DeliveredRecID, inparam.Rec_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, ConnectOneWS.Tables.USER_ORDER.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_RR_Details_ForUOmapping(inparam As Param_Get_RR_Details_ForUOmapping, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_RR_Details_UOmapping"
            Dim params() As String = {"@UO_ID", "@SubItemID"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Item_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, -1}
            Return dbService.ListFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Get_CenterDetails_StockAvailability(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select DISTINCT A1.CEN_NAME CenterName, A1.CEN_UID Center_UID, A1.CEN_ID CENID From Store_Dept_Info A Inner Join centre_info A1 on A.SD_Cen_ID = A1.CEN_ID Order by 2"
            Return dbService.List(ConnectOneWS.Tables.STORE_DEPT_INFO, Query, ConnectOneWS.Tables.USER_ORDER_CENTER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_Store_Locations_StockAvailability(inparam As Param_GetStoreLocations_StockAvailability, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Store_Locations"
            Dim params() As String = {"@user_id", "@CEN_ID", "@StoreID"}
            Dim values() As Object = {inBasicParam.openUserID, inparam.CEN_ID, inparam.StoreID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_STORE_LOCATION_INFO, SPName, ConnectOneWS.Tables.USER_ORDER_STORE_LOCATION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStoreList_StockAvailability(inparam As Param_GetStoreList_StockAvailability, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Store_List"
            Dim params() As String = {"@user_id", "@CEN_ID", "@DeptID", "@SHOW_ALL_STORES"}
            Dim values() As Object = {inBasicParam.openUserID, inparam.CEN_ID, inparam.DeptID, inparam.GetAllStores}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Boolean}
            Dim lengths() As Integer = {255, 4, 4, 2}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STORE_DEPT_INFO, SPName, ConnectOneWS.Tables.STORE_DEPT_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_Stock_Availability(inParam As Param_Get_Stock_Availability, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Stock_Availablity"
            Dim params() As String = {"@ItemID", "@ItemType", "@CENID", "@StoreDeptID", "@LocationID"}
            Dim values() As Object = {inParam.ItemID, inParam.ItemType, inParam.Center, inParam.StoreDeptID, inParam.LocationID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 4, 4, 36}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER_STOCK_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function Get_UO_Items_Detail_For_RR(inParam As Param_Get_UOItem_ForRR, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_UO_Items_ForNewRR"
            Dim params() As String = {"@UO_ID", "@UOI_SubItems"}
            Dim values() As Object = {inParam.UO_ID, inParam.SubItemID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, -1}
            Return dbService.ListFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function InsertUORRMapping(ByVal InParam As Param_UO_Insert_UO_RR_Mapping, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "UORR_Map"
            Dim params() As String = {"@UserID", "@UO_ID", "@RR_ID", "@SubItemID"}
            Dim values() As Object = {inBasicParam.openUserID, InParam.UO_ID, InParam.RR_ID, InParam.UO_Item_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4, 4}

            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_UO_RR_MAPPING, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function


        Public Shared Function InsertUOTxn(inparam_Txn As Param_Insert_UO_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim inparam As New Param_Insert_UO
            inparam.UO_Initiated_Mode = inparam_Txn.UO_Initiated_Mode
            inparam.UO_Date = inparam_Txn.UO_Date
            inparam.UO_Job_ID = inparam_Txn.UO_Job_ID
            inparam.UO_store_ID = inparam_Txn.UO_store_ID
            inparam.UO_Requestor_ID = inparam_Txn.UO_Requestor_ID
            'inparam.UO_Delivery_Location_ID = inparam_Txn.UO_Delivery_Location_ID
            inparam.UO_Requestor_Main_Dept_Id = inparam_Txn.UO_Requestor_Main_Dept_Id
            inparam.UO_Requestor_Sub_Dept_Id = inparam_Txn.UO_Requestor_Sub_Dept_Id

            Dim UOID As String = InsertUO(inparam, inBasicParam)
            If Not inparam_Txn.Remarks = Nothing Then
                Dim inRemarks As New Param_InsertUORemarks
                inRemarks.Remarks = inparam_Txn.Remarks
                inRemarks.UO_ID = UOID
                InsertUORemarks(inRemarks, inBasicParam)
            End If

            If Not inparam_Txn.InAttachment Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In inparam_Txn.InAttachment
                    InAttachment.Ref_Rec_ID = UOID
                    InAttachment.Ref_Screen = "UO"
                    Attachments.Insert(InAttachment, inBasicParam)
                Next
            End If
            If Not inparam_Txn.InUOItems Is Nothing Then
                For Each InItem As Param_Insert_UO_Item In inparam_Txn.InUOItems
                    InItem.UO_ID = UOID
                    Insert_UO_Item(InItem, inBasicParam)
                Next
            End If
            If Not inparam_Txn.InUODeliveries Is Nothing Then
                For Each InDelivery As Param_Insert_UO_Item_Delivered In inparam_Txn.InUODeliveries
                    InDelivery.UO_ID = UOID
                    Insert_UO_Item_Delivered(InDelivery, inBasicParam)
                Next
            End If
            If Not inparam_Txn.InUOReceipts Is Nothing Then
                For Each InReceived As Param_Insert_UO_Item_Received In inparam_Txn.InUOReceipts
                    InReceived.UO_ID = UOID
                    Insert_UO_Item_Received(InReceived, inBasicParam)
                Next
            End If
            If Not inparam_Txn.InUOReturns Is Nothing Then
                For Each InReturn As Param_Insert_UO_Item_Returned In inparam_Txn.InUOReturns
                    InReturn.UO_ID = UOID
                    Insert_UO_Item_Returned(InReturn, inBasicParam)
                Next
            End If
            If Not inparam_Txn.InUORerturnReceipts Is Nothing Then
                For Each InReturnRec As Param_Insert_UO_Item_Return_Received In inparam_Txn.InUORerturnReceipts
                    InReturnRec.UO_ID = UOID
                    Insert_UO_Item_Return_Received(InReturnRec, inBasicParam)
                Next
            End If
            If Not inparam_Txn.InUOScrapCreated Is Nothing Then
                For Each InScrapCreated As StockProfile.Param_Add_Stock_Addition In inparam_Txn.InUOScrapCreated
                    InScrapCreated.Stock_Ref_ID = UOID
                    InScrapCreated.Stock_Ref_Entry_Source = StockProfile.Stock_Addition_Source.User_Order_Scrap_Screen
                    StockProfile.AddStockAddition(InScrapCreated, inBasicParam)
                Next
            End If

            Return True
        End Function
        Public Shared Function UpdateUOTxn(inparam_Txn As Param_Update_UO_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim inparam As New Param_Update_UO
            inparam.UO_Initiated_Mode = inparam_Txn.UO_Initiated_Mode
            inparam.UO_Date = inparam_Txn.UO_Date
            inparam.UO_Job_ID = inparam_Txn.UO_Job_ID
            inparam.UO_store_ID = inparam_Txn.UO_store_ID
            inparam.UO_Requestor_ID = inparam_Txn.UO_Requestor_ID
            inparam.UO_Requestor_Main_Dept_Id = inparam_Txn.UO_Requestor_Main_Dept_Id
            inparam.UO_Requestor_Sub_Dept_Id = inparam_Txn.UO_Requestor_Sub_Dept_Id
            inparam.UOID = inparam_Txn.UOID
            UpdateUO(inparam, inBasicParam)

            '==Additions in Child Records
            'Add Remarks 
            If Not inparam_Txn.Remarks = Nothing Then
                Dim inRemarks As New Param_InsertUORemarks
                inRemarks.Remarks = inparam_Txn.Remarks
                inRemarks.UO_ID = inparam_Txn.UOID
                InsertUORemarks(inRemarks, inBasicParam)
            End If
            'Add Attachment
            If Not inparam_Txn.Added_Attachments Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In inparam_Txn.Added_Attachments
                    If Not InAttachment Is Nothing Then
                        InAttachment.Ref_Rec_ID = inparam_Txn.UOID
                        InAttachment.Ref_Screen = "UO"
                        Attachments.Insert(InAttachment, inBasicParam)
                    End If
                Next
            End If
            'Add Requested Item
            If Not inparam_Txn.InUOItems Is Nothing Then
                For Each InItem As Param_Insert_UO_Item In inparam_Txn.InUOItems
                    If Not InItem Is Nothing Then
                        InItem.UO_ID = inparam_Txn.UOID
                        Insert_UO_Item(InItem, inBasicParam)
                    End If
                Next
            End If
            'Add UO Delivery
            If Not inparam_Txn.InUODeliveries Is Nothing Then
                For Each InDelivery As Param_Insert_UO_Item_Delivered In inparam_Txn.InUODeliveries
                    If Not InDelivery Is Nothing Then
                        InDelivery.UO_ID = inparam_Txn.UOID
                        Insert_UO_Item_Delivered(InDelivery, inBasicParam)
                    End If
                Next
            End If
            'Add UO Receipt
            If Not inparam_Txn.InUOReceipts Is Nothing Then
                For Each InReceived As Param_Insert_UO_Item_Received In inparam_Txn.InUOReceipts
                    If Not InReceived Is Nothing Then
                        InReceived.UO_ID = inparam_Txn.UOID
                        Insert_UO_Item_Received(InReceived, inBasicParam)
                    End If
                Next
            End If
            'Add UO Return
            If Not inparam_Txn.InUOReturns Is Nothing Then

                For Each InReturn As Param_Insert_UO_Item_Returned In inparam_Txn.InUOReturns
                    If Not InReturn Is Nothing Then
                        InReturn.UO_ID = inparam_Txn.UOID
                        Insert_UO_Item_Returned(InReturn, inBasicParam)
                    End If
                Next
            End If
            'Add UO Return Receipt
            If Not inparam_Txn.InUORerturnReceipts Is Nothing Then
                For Each InReturnRec As Param_Insert_UO_Item_Return_Received In inparam_Txn.InUORerturnReceipts
                    If Not InReturnRec Is Nothing Then
                        InReturnRec.UO_ID = inparam_Txn.UOID
                        Insert_UO_Item_Return_Received(InReturnRec, inBasicParam)
                    End If
                Next
            End If
            'Add UO Scrap Created 
            If Not inparam_Txn.InUOScrapCreated Is Nothing Then
                For Each InScrapCreated As StockProfile.Param_Add_Stock_Addition In inparam_Txn.InUOScrapCreated
                    If Not InScrapCreated Is Nothing Then
                        InScrapCreated.Stock_Ref_ID = inparam_Txn.UOID
                        InScrapCreated.Stock_Ref_Entry_Source = StockProfile.Stock_Addition_Source.User_Order_Scrap_Screen
                        StockProfile.AddStockAddition(InScrapCreated, inBasicParam)
                    End If
                Next
            End If
            '==Deletions in Child Records
            'Delete Remarks
            If Not inparam_Txn.Deleted_UORemarks Is Nothing Then
                For Each delRemarks As Param_Deleted_UO_Remarks In inparam_Txn.Deleted_UORemarks
                    If Not delRemarks Is Nothing Then
                        Delete_UO_Remarks(delRemarks.Rec_ID, inBasicParam)
                    End If
                Next
            End If
            'Delete Attachment
            If Not inparam_Txn.Deleted_UOAttachments Is Nothing Then
                For Each delAttachment As Param_Deleted_UO_Attachments In inparam_Txn.Deleted_UOAttachments
                    '  If Not delAttachment Is Nothing Then
                    Attachments.Delete_Attachment_ByID(delAttachment.Rec_ID, inBasicParam)
                    ' End If
                Next
            End If
            'Unlink Attachment 
            If Not inparam_Txn.Unlinked_UOAttachments Is Nothing Then
                For Each unlinkAttachment As Param_Unlinked_UO_Attachments In inparam_Txn.Unlinked_UOAttachments
                    'If Not unlinkAttachment Is Nothing Then
                    Dim unlinkParam As New Attachments.Parameter_Attachment_Unlink
                    unlinkParam.AttachmentID = unlinkAttachment.Rec_ID
                    unlinkParam.Ref_Rec_ID = inparam.UOID
                    Attachments.Unlink_Attachment(unlinkParam, inBasicParam)
                    'End If
                Next
            End If
            'Delete Requested Item
            If Not inparam_Txn.Deleted_UO_Items Is Nothing Then
                For Each delItems As Param_Deleted_UO_Items In inparam_Txn.Deleted_UO_Items
                    If Not delItems Is Nothing Then
                        Delete_UO_Item(delItems.Rec_ID, inBasicParam)
                    End If
                Next
            End If
            'Delete Item Deliveries
            If Not inparam_Txn.Deleted_UO_Items_Delivered Is Nothing Then
                For Each delItemDelivery As Param_Deleted_UO_Items_Delivered In inparam_Txn.Deleted_UO_Items_Delivered
                    If Not delItemDelivery Is Nothing Then
                        Delete_UO_Item_Delivered(delItemDelivery.Rec_ID, inBasicParam)
                    End If
                Next
            End If
            'Delete Item Receipt
            If Not inparam_Txn.Deleted_UO_Items_Receipts Is Nothing Then
                For Each delItemReceipts As Param_Deleted_UO_Items_Received In inparam_Txn.Deleted_UO_Items_Receipts
                    If Not delItemReceipts Is Nothing Then
                        Delete_UO_Item_Received(delItemReceipts.Rec_ID, inBasicParam)
                    End If
                Next
            End If
            'Delete Item Return
            If Not inparam_Txn.Deleted_UO_Items_Returns Is Nothing Then
                For Each delItemReturn As Param_Deleted_UO_Items_Returned In inparam_Txn.Deleted_UO_Items_Returns
                    If Not delItemReturn Is Nothing Then
                        Delete_UO_Item_Returned(delItemReturn.Rec_ID, inBasicParam)
                    End If
                Next
            End If
            'Delete Item Return Recd
            If Not inparam_Txn.Deleted_UO_Items_Return_Receipts Is Nothing Then
                For Each delItemReturnRecd As Param_Deleted_UO_Items_ReturnReceived In inparam_Txn.Deleted_UO_Items_Return_Receipts
                    If Not delItemReturnRecd Is Nothing Then
                        Delete_UO_Item_Return_Received(delItemReturnRecd.Rec_ID, inBasicParam)
                    End If
                Next
            End If
            'Delete Scrap
            If Not inparam_Txn.Deleted_UO_Scrap Is Nothing Then
                For Each delScrap As Param_Deleted_UO_Scrap In inparam_Txn.Deleted_UO_Scrap
                    If Not delScrap Is Nothing Then
                        StockProfile.DeleteStockProfile(delScrap.Rec_ID, inBasicParam)
                    End If
                Next
            End If
            '==Updations in Child Records
            'Update Requested Item
            If Not inparam_Txn.UpdateUOItems Is Nothing Then
                For Each InItem As Param_Update_UO_Item In inparam_Txn.UpdateUOItems
                    If Not InItem Is Nothing Then
                        InItem.UO_ID = inparam_Txn.UOID
                        UpdateUOItem(InItem, inBasicParam)
                    End If
                Next
            End If
            'Update UO Delivery
            If Not inparam_Txn.UpdateUODeliveries Is Nothing Then
                For Each InDelivery As Param_Update_UO_Delivery In inparam_Txn.UpdateUODeliveries
                    If Not InDelivery Is Nothing Then
                        InDelivery.UO_ID = inparam_Txn.UOID
                        UpdateUODelivery(InDelivery, inBasicParam)
                    End If
                Next
            End If
            'Update UO Receipt
            If Not inparam_Txn.UpdateUOReceipts Is Nothing Then
                For Each InReceived As Param_Update_UO_Received In inparam_Txn.UpdateUOReceipts
                    If Not InReceived Is Nothing Then
                        InReceived.UO_ID = inparam_Txn.UOID
                        UpdateUOReceipt(InReceived, inBasicParam)
                    End If
                Next
            End If

            'Update UO Return
            If Not inparam_Txn.UpdateUOReturns Is Nothing Then
                For Each InReturn As Param_Update_UO_Returned In inparam_Txn.UpdateUOReturns
                    If Not InReturn Is Nothing Then
                        InReturn.UO_ID = inparam_Txn.UOID
                        UpdateUOReturn(InReturn, inBasicParam)
                    End If
                Next
            End If
            'Update UO Return Receipt
            If Not inparam_Txn.UpdateUORerturnReceipts Is Nothing Then
                For Each InReturnRec As Param_Update_UO_Return_Received In inparam_Txn.UpdateUORerturnReceipts
                    If Not InReturnRec Is Nothing Then
                        InReturnRec.UO_ID = inparam_Txn.UOID
                        UpdateUOReturnReceived(InReturnRec, inBasicParam)
                    End If
                Next
            End If
            'Update UO Scrap Created 
            If Not inparam_Txn.UpdateUOScrapCreated Is Nothing Then
                For Each InScrapCreated As StockProfile.Param_Update_StockProfile In inparam_Txn.UpdateUOScrapCreated
                    If Not InScrapCreated Is Nothing Then
                        StockProfile.UpdateStockProfile(InScrapCreated, inBasicParam)
                    End If
                Next
            End If

            'Update Attachment
            If Not inparam_Txn.Updated_Attachments Is Nothing Then
                For Each updAttachment As Attachments.Parameter_Update_Attachment In inparam_Txn.Updated_Attachments
                    If Not updAttachment Is Nothing Then
                        Attachments.Update(updAttachment, inBasicParam)
                    End If
                Next
            End If
            Return True
        End Function
        Private Shared Function InsertUO(inparam As Param_Insert_UO, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Details"
            Dim params() As String = {"@UO_Cen_ID", "@UO_Year_ID", "@UO_Initiated_Mode", "@UO_Date", "@UO_Job_ID", "@UO_store_ID", "@UO_Requestor_ID", "@User_ID", "@UO_Requestor_Main_Dept_Id", "@UO_Requestor_Sub_Dept_Id"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inparam.UO_Initiated_Mode, inparam.UO_Date, inparam.UO_Job_ID, inparam.UO_store_ID, inparam.UO_Requestor_ID, inBasicParam.openUserID, inparam.UO_Requestor_Main_Dept_Id, inparam.UO_Requestor_Sub_Dept_Id}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 50, 12, 4, 4, 4, 255, 4, 4}
            Dim UOID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return UOID
        End Function
        Public Shared Function Insert_UO_Item(inparam As Param_Insert_UO_Item, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Items"
            Dim params() As String = {"@UOI_UO_ID", "@UOI_Sub_Item_ID", "@UOI_Make", "@UOI_Model", "@UOI_Unit_ID", "@UOI_Requested_Qty", "@UOI_Required_Date", "@UOI_Scheduled_Delivery_Date", "@UOI_Priority", "@UOI_Part_Delivery_Allowed", "@UOI_Remarks", "@User_ID", "@UOI_Delivery_Location_Id", "@UOI_Approved_Qty"}
            Dim values() As Object = {inparam.UO_ID, inparam.Sub_Item_ID, inparam.Make, inparam.Model, inparam.Unit_ID, inparam.Requested_Qty, inparam.Required_Date, inparam.Scheduled_Delivery_Date, inparam.Priority, inparam.Part_Delivery_Allowed, inparam.Remarks, inBasicParam.openUserID, inparam.Delivery_Location_Id, inparam.Approved_Qty}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal}
            Dim lengths() As Integer = {4, 4, 255, 255, 36, 9, 12, 12, 20, 2, 255, 255, 36, 9}
            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_ITEM, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function Insert_UO_Item_Delivered(inparam As Param_Insert_UO_Item_Delivered, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Item_Delivered"
            Dim params() As String = {"@UD_UO_ID", "@UD_UO_Item_ID", "@UD_Delivered_Qty", "@UD_Delivery_Date", "@UD_Delivery_Mode", "@UD_Delivery_Carrier", "@UD_Delivery_Location_ID", "@UD_Delivered_By_ID", "@UD_Delivery_Driver_ID", "@UD_Delivery_Receiver_Name", "@UD_Delivery_Remarks", "@User_ID", "@VehicleNo"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Item_ID, inparam.Delivered_Qty, inparam.Delivery_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Delivery_Location_ID, inparam.Delivered_By_ID, inparam.Delivery_Driver_ID, inparam.Delivery_Receiver_Name, inparam.Delivery_Remarks, inBasicParam.openUserID, inparam.VehicleNo}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 36, 4, 4, 100, 500, 255, 100}
            Dim UD_ID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Inserting Items added in nested grid 
            If Not inparam._Delivered_Stock Is Nothing Then
                For Each _InDelivered_Stock As Param_Insert_UO_Item_Delivered_Stocks In inparam._Delivered_Stock
                    If Not _InDelivered_Stock Is Nothing Then
                        _InDelivered_Stock.UD_ID = UD_ID
                        Insert_UO_Item_Delivered_Stocks(_InDelivered_Stock, UD_ID, inBasicParam)
                    End If
                Next

            End If
            Return True

        End Function

        Private Shared Function Insert_UO_Item_Delivered_Stocks(inparam As Param_Insert_UO_Item_Delivered_Stocks, UD_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Item_Delivered_Stocks"
            Dim params() As String = {"@UO_ID", "@UD_ID", "@Stock_ID", "@UDS_Qty", "@UserID"}
            Dim values() As Object = {inparam.UO_ID, inparam.UD_ID, inparam.Stock_ID, inparam.UDS_Qty, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 4, 9, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED_STOCKS, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function Insert_UO_Item_Received(inparam As Param_Insert_UO_Item_Received, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Item_Received"
            Dim params() As String = {"@UR_UO_ID", "@UR_UO_Delivered_ID", "@UR_Recd_Qty", "@UR_Recd_Date", "@UR_Delivery_Mode", "@UR_Delivery_Carrier", "@UR_Received_Location_ID", "@UR_Bill_No", "@UR_Challan_No", "@UR_Received_By_ID", "@UR_Receiver_Remarks", "@User_ID", "@UR_UO_Returned_ID", "@UDS_ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Delivered_ID, inparam.Recd_Qty, inparam.Recd_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Received_Location_ID, inparam.Bill_No, inparam.Challan_No, inparam.Received_By_ID, inparam.Receiver_Remarks, inBasicParam.openUserID, inparam.UO_Returned_ID, inparam.UDS_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 36, 50, 50, 4, 500, 255, 4, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function Insert_UO_Item_Returned(inparam As Param_Insert_UO_Item_Returned, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Item_Returned"
            Dim params() As String = {"@UR_UO_ID", "@UR_Recd_Item_ID", "@UR_Returned_Qty", "@UR_Returned_Date", "@UR_Delivery_Mode", "@UR_Delivery_Carrier", "@UR_Returned_Location_ID", "@UR_Returned_By_ID", "@UR_Returned_by_Remarks", "@User_ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.Recd_Item_ID, inparam.Returned_Qty, inparam.Returned_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Returned_Location_ID, inparam.Returned_By_ID, inparam.Returned_by_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 36, 4, 500, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function


        Public Shared Function Insert_UO_Item_Return_Received(inparam As Param_Insert_UO_Item_Return_Received, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Item_Return_Received"
            Dim params() As String = {"@URR_UO_ID", "@URR_UO_Returned_ID", "@URR_Received_Qty", "@URR_Received_Date", "@URR_Received_Mode", "@URR_Delivery_Carrier", "@URR_Ret_Rec_Location_ID", "@URR_Received_By_ID", "@URR_Receiver_Remarks", "@User_ID", "@URR_UO_Delivered_ID", "@URR_UDS_ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Returned_ID, inparam.Received_Qty, inparam.Received_Date, inparam.Received_Mode_ID, inparam.Delivery_Carrier, inparam.Ret_Rec_Location_ID, inparam.Received_By_ID, inparam.Receiver_Remarks, inBasicParam.openUserID, inparam.UO_Delivered_ID, inparam.URR_UDS_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 36, 4, 500, 255, 4, 4}
            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURN_RECEIVED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertUORemarks(inParam As Param_InsertUORemarks, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Remarks"
            Dim params() As String = {"@User_ID", "@RefID", "@SR_Remarks", "@Screen"}
            Dim values() As Object = {inBasicParam.openUserID, inParam.UO_ID, inParam.Remarks, "UO"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 8000, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertUOGoodsDeliverAllPending(inparam As Param_GetUOGoodsDeliverAllPending, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Item_Deliver_All"
            Dim params() As String = {"@UO_ID", "@Delivery_Date", "@Delivery_Mode", "@Delivery_Carrier", "@Delivery_Location_ID", "@Delivered_By_ID", "@Delivery_Driver_ID", "@Delivery_Receiver_Name", "@Delivery_Remarks", "@User_ID", "@VehicleNo"}
            Dim values() As Object = {inparam.UO_ID, inparam.Delivery_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Delivery_Location_ID, inparam.Delivered_By_ID, inparam.Delivery_Driver_ID, inparam.Delivery_Receiver_Name, inparam.Delivery_Remarks, inBasicParam.openUserID, inparam.VehicleNo}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 14, 36, 100, 36, 4, 100, 100, 500, 255, 4, 100}
            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertUOGoodsReceiveAllPending(inparam As Param_GetUOGoodsReceiveAllPending, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Item_Receive_All"
            Dim params() As String = {"@UO_ID", "@Recd_Date", "@Delivery_Mode", "@Delivery_Carrier", "@Received_Location_ID", "@Bill_No", "@Challan_No", "@Received_By_ID", "@User_ID", "@Receiver_Remarks"}
            Dim values() As Object = {inparam.UO_ID, inparam.Recd_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Received_Location_ID, inparam.Bill_No, inparam.Challan_No, inparam.Received_By_ID, inBasicParam.openUserID, inparam.Receiver_Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 12, 36, 100, 36, 50, 50, 4, 500, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertUOGoodsReturnAllPending(inparam As Param_GetUOGoodsReturnAllPending, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_UO_Item_Return_All"
            Dim params() As String = {"@UO_ID", "@Returned_Date", "@Delivery_Mode", "@Delivery_Carrier", "@Returned_Location_ID", "@Returned_By_ID", "@Returned_by_Remarks", "@User_ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.Returned_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Returned_Location_ID, inparam.Returned_By_ID, inparam.Returned_by_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 12, 36, 100, 36, 4, 500, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateUO(inparam As Param_Update_UO, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "[Update_UO_Details]"
            Dim params() As String = {"@UO_ID", "@UO_Initiated_Mode", "@UO_Date", "@UO_Job_ID", "@UO_store_ID", "@UO_Requestor_ID", "@User_ID", "@UO_Requestor_Main_Dept_Id", "@UO_Requestor_Sub_Dept_Id"}
            Dim values() As Object = {inparam.UOID, inparam.UO_Initiated_Mode, inparam.UO_Date, inparam.UO_Job_ID, inparam.UO_store_ID, inparam.UO_Requestor_ID, inBasicParam.openUserID, inparam.UO_Requestor_Main_Dept_Id, inparam.UO_Requestor_Sub_Dept_Id}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 50, 12, 4, 4, 4, 255, 4, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateUOItem(inparam As Param_Update_UO_Item, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_UO_Item_Details"
            Dim params() As String = {"@UOI_UO_ID", "@UOI_Sub_Item_ID", "@UOI_Make", "@UOI_Model", "@UOI_Unit_ID", "@UOI_Requested_Qty", "@UOI_Required_Date", "@UOI_Scheduled_Delivery_Date", "@UOI_Priority", "@UOI_Part_Delivery_Allowed", "@UOI_Remarks", "@User_ID", "@UOI_Delivery_Location_Id", "@UOI_Approved_Qty", "@ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.Sub_Item_ID, inparam.Make, inparam.Model, inparam.Unit_ID, inparam.Requested_Qty, inparam.Required_Date, inparam.Scheduled_Delivery_Date, inparam.Priority, inparam.Part_Delivery_Allowed, inparam.Remarks, inBasicParam.openUserID, inparam.Delivery_Location_Id, inparam.Approved_Qty, inparam.ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 255, 255, 36, 9, 12, 12, 20, 2, 255, 255, 36, 9, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.USER_ORDER_ITEM, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateUODelivery(inparam As Param_Update_UO_Delivery, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_UO_Delivery"
            Dim params() As String = {"@UD_UO_ID", "@UD_UO_Item_ID", "@UD_Delivered_Qty", "@UD_Delivery_Date", "@UD_Delivery_Mode_ID", "@UD_Delivery_Carrier", "@UD_Delivery_Location_ID", "@UD_Delivered_By_ID", "@UD_Delivery_Driver_ID", "@UD_Delivery_Receiver_Name", "@UD_Delivery_Remarks", "@User_ID", "@UD_Vehicle_No", "@ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Item_ID, inparam.Delivered_Qty, inparam.Delivery_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Delivery_Location_ID, inparam.Delivered_By_ID, inparam.Delivery_Driver_ID, inparam.Delivery_Receiver_Name, inparam.Delivery_Remarks, inBasicParam.openUserID, inparam.VehicleNo, inparam.ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 36, 4, 4, 100, 500, 255, 100, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, SPName, params, values, dbTypes, lengths, inBasicParam)

            ' For update - Delete UO Delivery stocks then Insert  UO Delivery stocks in update mode
            dbService.DeleteByCondition(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED_STOCKS, "UDS_UD_ID = " + inparam.ID.ToString(), inBasicParam)


            If Not inparam._Added_Delivered_Stock Is Nothing Then
                For Each inDeliveredStocks As Param_Insert_UO_Item_Delivered_Stocks In inparam._Added_Delivered_Stock
                    If Not inDeliveredStocks Is Nothing Then
                        inDeliveredStocks.UD_ID = inparam.ID
                        Insert_UO_Item_Delivered_Stocks(inDeliveredStocks, inparam.ID, inBasicParam)
                    End If
                Next
            End If

            Return True
        End Function

        'Private Shared Function Update_UO_Item_Delivered_Stocks(inparam As Param_Update_UO_Delivery_Stocks, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim SPName As String = "Update_UO_Item_Delivered_Stocks"
        '    Dim params() As String = {"@UO_ID", "@UD_ID", "@Stock_ID", "@UDS_Qty", "@UDS_ID"}
        '    Dim values() As Object = {inparam.UO_ID, inparam.UD_ID, inparam.Stock_ID, inparam.UDS_Qty, inparam.UDS_ID}
        '    Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Int32}
        '    Dim lengths() As Integer = {4, 4, 4, 9, 4}
        '    dbService.UpdateBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, SPName, params, values, dbTypes, lengths, inBasicParam)



        '    Return True
        'End Function

        Private Shared Function UpdateUOReceipt(inparam As Param_Update_UO_Received, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_UO_Receipt"
            Dim params() As String = {"@UR_UO_ID", "@UR_UO_Delivered_ID", "@UR_Recd_Qty", "@UR_Recd_Date", "@UR_Delivery_Mode_ID", "@UR_Delivery_Carrier", "@UR_Received_Location_ID", "@UR_Bill_No", "@UR_Challan_No", "@UR_Received_By_ID", "@UR_Receiver_Remarks", "@User_ID", "@UR_UO_Returned_ID", "@ID", "@UDS_ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Delivered_ID, inparam.Recd_Qty, inparam.Recd_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Received_Location_ID, inparam.Bill_No, inparam.Challan_No, inparam.Received_By_ID, inparam.Receiver_Remarks, inBasicParam.openUserID, inparam.UO_Returned_ID, inparam.ID, inparam.UDS_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 36, 50, 50, 4, 500, 255, 4, 4, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateUOReturn(inparam As Param_Update_UO_Returned, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_UO_Return"
            Dim params() As String = {"@UR_UO_ID", "@UR_Recd_Item_ID", "@UR_Returned_Qty", "@UR_Returned_Date", "@UR_Delivery_Mode_ID", "@UR_Delivery_Carrier", "@UR_Returned_Location_ID", "@UR_Returned_By_ID", "@UR_Returned_by_Remarks", "@User_ID", "@ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.Recd_Item_ID, inparam.Returned_Qty, inparam.Returned_Date, inparam.Delivery_Mode_ID, inparam.Delivery_Carrier, inparam.Returned_Location_ID, inparam.Returned_By_ID, inparam.Returned_by_Remarks, inBasicParam.openUserID, inparam.ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 36, 4, 500, 255, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateUOReturnReceived(inparam As Param_Update_UO_Return_Received, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_UO_Return_Receipt"
            Dim params() As String = {"@URR_UO_ID", "@URR_UO_Returned_ID", "@URR_Received_Qty", "@URR_Received_Date", "@URR_Received_Mode_ID", "@URR_Delivery_Carrier", "@URR_Ret_Rec_Location_ID", "@URR_Received_By_ID", "@URR_Receiver_Remarks", "@User_ID", "@URR_UO_Delivered_ID", "@ID", "@URR_UDS_ID"}
            Dim values() As Object = {inparam.UO_ID, inparam.UO_Returned_ID, inparam.Received_Qty, inparam.Received_Date, inparam.Received_Mode_ID, inparam.Delivery_Carrier, inparam.Ret_Rec_Location_ID, inparam.Received_By_ID, inparam.Receiver_Remarks, inBasicParam.openUserID, inparam.UO_Delivered_ID, inparam.ID, inparam.URR_UDS_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 36, 4, 500, 255, 4, 4, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURN_RECEIVED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdateUOStatus(inparam As Param_Update_UO_Status, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _Updated_Status As String = ""
            Select Case inparam.UpdatedStatus
                Case UO_Status._New
                    _Updated_Status = "New"
                Case UO_Status.Cancelled
                    _Updated_Status = "Cancelled"
                Case UO_Status.Rejected
                    _Updated_Status = "Rejected"
                Case UO_Status.Changes_Recommended
                    _Updated_Status = "Changes Recommended"
                Case UO_Status.Requested
                    _Updated_Status = "Requested"
                Case UO_Status.Assigned_for_Estimation_Creation
                    _Updated_Status = "Assigned for Estimation Creation"
                Case UO_Status.Submitted_for_Estimate_Approval
                    _Updated_Status = "Submitted for Estimate Approval"
                Case UO_Status.Approved
                    _Updated_Status = "Approved"
                Case UO_Status.Assigned
                    _Updated_Status = "Assigned"
                Case UO_Status.In_Progress
                    _Updated_Status = "In-Progress"
                Case UO_Status.Completed
                    _Updated_Status = "Completed"
            End Select
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_UO_Status"
            Dim params() As String = {"@UserID", "@UO_ID", "@UpdatedStatus", "@Remarks"}
            Dim values() As Object = {inBasicParam.openUserID, inparam.UOID, _Updated_Status, inparam.Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 100, 8000}
            dbService.UpdateBySP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function Update_Scheduled_Delivery(inparam As Param_Update_UO_Scheduled_Delivery, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "UPDATE User_Order_Item Set UOI_Scheduled_Delivery_Date = '" + inparam.Scheduled_Delivery_Date.ToString(Common.Server_Date_Format_Short) + "' Where UOI_UO_ID = " + inparam.UO_ID.ToString() + " "
            dbService.Update(ConnectOneWS.Tables.USER_ORDER_ITEM, Query, inBasicParam)
            Return True
        End Function
        Public Shared Function DeleteUO(UO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Remarks
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_REMARKS_INFO, "SR_Ref_ID = " + UO_ID.ToString() + " AND SR_Screen_Type = 'UO'", inBasicParam)
            'UO Return Received
            dbService.DeleteByCondition(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURN_RECEIVED, "URR_UO_ID = " + UO_ID.ToString(), inBasicParam)
            'UO Returned
            dbService.DeleteByCondition(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, "UR_UO_ID = " + UO_ID.ToString(), inBasicParam)
            'UO Received
            dbService.DeleteByCondition(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, "UR_UO_ID = " + UO_ID.ToString(), inBasicParam)
            'UO Delivery
            dbService.DeleteByCondition(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, "UD_UO_ID = " + UO_ID.ToString(), inBasicParam)
            'UOItem
            dbService.DeleteByCondition(ConnectOneWS.Tables.USER_ORDER_ITEM, "UOI_UO_ID = " + UO_ID.ToString(), inBasicParam)
            'UO Scrap
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_INFO, "STOCK_REF_ID = " + UO_ID.ToString() + " AND STOCK_REF_ENTRY_SOURCE='UO - Scrap'", inBasicParam)
            'UO
            dbService.Delete(ConnectOneWS.Tables.USER_ORDER, UO_ID.ToString(), inBasicParam)

            'Unlink those Documents which are attached somewhere else, and delete those which are not linked elsewhere
            Dim inDocs As New Projects.Param_GetStockDocuments()
            inDocs.RefID = UO_ID
            inDocs.Screen_Type = "UO"

            Dim _Docs_in_PO As DataTable = Projects.GetStockDocuments(inDocs, inBasicParam)
            For Each cRow As DataRow In _Docs_in_PO.Rows
                Dim inparam As New Attachments.Parameter_GetAttachmentLinkCount()
                inparam.RefRecordID = UO_ID
                inparam.RefScreen = "UO"
                inparam.AttachmentID = cRow("ID")
                Dim LinkedScreenName As String = Attachments.GetAttachmentLinkCount(inparam, inBasicParam)
                If String.IsNullOrEmpty(LinkedScreenName) Then
                    Attachments.Delete_Attachment_ByReference(UO_ID, "UO", inBasicParam)
                Else
                    Attachments.Unlink_Attachment_ByReference(UO_ID, "UO", inBasicParam)
                End If
            Next

            Return True
        End Function
        Private Shared Function Delete_UO_Item(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.USER_ORDER_ITEM, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function Delete_UO_Item_Delivered(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'UO Delivery Stock
            dbService.DeleteByCondition(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED_STOCKS, "UDS_UD_ID = " + Rec_ID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        'Private Shared Function Delete_UO_Item_Delivered_Stocks(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    dbService.Delete(ConnectOneWS.Tables.USER_ORDER_ITEM_DELIVERED_STOCKS, Rec_ID.ToString(), inBasicParam)
        '    Return True
        'End Function
        Private Shared Function Delete_UO_Item_Received(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.USER_ORDER_ITEM_RECEIVED, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function Delete_UO_Item_Returned(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURNED, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function Delete_UO_Item_Return_Received(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.USER_ORDER_ITEM_RETURN_RECEIVED, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function Delete_UO_Remarks(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_REMARKS_INFO, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function Delete_UO_Scrap(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_INFO, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Public Shared Function UORRUnmapping(ByVal InParam As Param_UO_RR_Unmapping, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "UORR_Unmap"
            Dim params() As String = {"@UserID", "@UO_ID", "@RR_ID", "@SubItemID"}
            Dim values() As Object = {inBasicParam.openUserID, InParam.UO_ID, InParam.Mapped_RR_ID, InParam.UO_Item_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4, 4}

            dbService.DeleteFromSP(ConnectOneWS.Tables.USER_ORDER_UO_RR_MAPPING, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
    End Class

    <Serializable>
    Public Class StockProduction
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetProductionRegister
            Public From_Date As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_Insert_Production_Txn
            Inherits Param_Insert_Production
            ''' <summary>
            ''' Array of Attachments added during Addition of Production
            ''' </summary>
            Public Added_Attachments As Attachments.Parameter_Insert_Attachment()
            Public Expenses_Incurred As List(Of Param_Insert_Production_Expenses)
            Public Items_Consumed As List(Of Param_Insert_Production_ItemsConsumed)
            Public Items_Produced As List(Of Param_Insert_Production_ItemsProduced)
            Public Machine_Usage As List(Of Param_Insert_Production_MachineUsage)
            Public Manpower_Usage As List(Of Param_Insert_Production_ManpowerUsage)
            Public Scrap_Produced As List(Of Param_Insert_Production_ScrapProduced)
        End Class
        <Serializable>
        Public Class Param_Update_Production_Txn
            Inherits Param_Update_Production
            ''' <summary>
            ''' Array of Attachments added 
            ''' </summary>
            Public Added_Attachments As Attachments.Parameter_Insert_Attachment()
            Public Added_Expenses_Incurred As List(Of Param_Insert_Production_Expenses)
            Public Added_Items_Consumed As List(Of Param_Insert_Production_ItemsConsumed)
            Public Added_Items_Produced As List(Of Param_Insert_Production_ItemsProduced)
            Public Added_Machine_Usage As List(Of Param_Insert_Production_MachineUsage)
            Public Added_Manpower_Usage As List(Of Param_Insert_Production_ManpowerUsage)
            Public Added_Scrap_Produced As List(Of Param_Insert_Production_ScrapProduced)
            Public Updated_Items_Consumed As List(Of Param_Update_Production_ItemsConsumed)
            Public Updated_Items_Produced As List(Of Param_Update_Production_ItemsProduced)
            Public Updated_Machine_Usage As List(Of Param_Update_Production_MachineUsage)
            Public Updated_Manpower_Usage As List(Of Param_Update_Production_ManpowerUsage)
            Public Updated_Scrap_Produced As List(Of Param_Update_Production_ScrapProduced)
            ''' <summary>
            ''' Array of Documents updated during Update of Production
            ''' </summary>
            Public Updated_Attachments As Attachments.Parameter_Update_Attachment()
            Public Deleted_Expenses_Incurred_IDs As List(Of Int32)
            Public Deleted_Items_Consumed_IDs As List(Of Int32)
            Public Deleted_Items_Produced_IDs As List(Of Int32)
            Public Deleted_Machine_Usage_IDs As List(Of Int32)
            Public Deleted_Manpower_Usage_IDs As List(Of Int32)
            Public Deleted_Scrap_Produced_IDs As List(Of Int32)
            Public Deleted_Remarks_IDs As List(Of Int32)
            Public Deleted_Attachment_IDs As List(Of String)
            Public Unlinked_Attachment_IDs As List(Of String)
        End Class
        <Serializable>
        Public Class Param_Insert_Production
            'Public Prod_No As String
            Public Prod_Date As DateTime
            Public Location_ID As String
            Public Lot_no As String
            Public Project_ID As Int32?
            Public StoreID As Int32
            Public Worked_By As String
            Public FromDate As DateTime?
            Public ToDate As DateTime?
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_Production_Expenses
            Public Exp_Tr_ID As String
            Public Exp_Tr_Sr_No As Int32?
        End Class
        <Serializable>
        Public Class Param_Insert_Production_ItemsConsumed
            Public Stock_ID As Int32
            Public Item_Qty As Decimal
            Public Item_Amount As Decimal
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_Production_ItemsProduced
            Public sub_Item_ID As Int32
            Public stock_ID As Int32?
            Public Item_Qty_Produced As Decimal
            Public Item_Qty_Accepted As Decimal
            Public Item_Market_Rate As Decimal?
            Public Item_Market_Price As Decimal?
            Public Remarks As String
            Public TotalValue_Perc As Decimal
            Public Store_ID As Int32
            Public Lot_Serial_no As String
            Public Make As String
            Public Model As String
            Public Warranty As String
            Public Unit_ID As String
            Public Value As Decimal
        End Class
        <Serializable>
        Public Class Param_Insert_Production_MachineUsage
            Public Machine_ID As Int32
            Public Machine_Count As Int32
            Public Machine_Usage As Decimal
            Public Machine_Remarks As String
        End Class
        <Serializable>
        Public Class Param_Insert_Production_ManpowerUsage
            Public Person_ID As Int32
            Public Period_From As DateTime
            Public Period_To As DateTime
            Public Units_Worked As Decimal
            Public Total_Amount As Decimal
            Public Remarks As String
            Public Charge_ID As Int32
        End Class
        <Serializable>
        Public Class Param_Insert_Production_ScrapProduced
            Public sub_Item_ID As Int32
            Public Store_ID As Int32
            Public Lot_Serial_no As String
            Public Make As String
            Public Model As String
            Public Warranty As String
            Public Unit_ID As String
            Public Value As Decimal
            Public Qty As Decimal
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Update_Production
            Inherits Param_Insert_Production
            Public ProdID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Production_ItemsConsumed
            Inherits Param_Insert_Production_ItemsConsumed
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Production_ItemsProduced
            Inherits Param_Insert_Production_ItemsProduced
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Production_MachineUsage
            Inherits Param_Insert_Production_MachineUsage
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Production_ManpowerUsage
            Inherits Param_Insert_Production_ManpowerUsage
            Public ID As Int32
        End Class
        <Serializable>
        Public Class Param_Update_Production_ScrapProduced
            Inherits Param_Insert_Production_ScrapProduced
            Public ID As Int32
        End Class
#End Region
        ''' <summary>
        ''' 
        ''' </summary>
        ''' <param name="StockID"></param>
        ''' <param name="inBasicParam"></param>
        ''' <returns></returns>
        Public Shared Function GetProductions(StockID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Stock_Production"
            Dim params() As String = {"user_id", "StockID", "CEN_ID"}
            Dim values() As Object = {inBasicParam.openUserID, StockID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, ConnectOneWS.Tables.STOCK_PRODUCTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetRegister(inParam As Param_GetProductionRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Production_Register"
            Dim params() As String = {"FromDate", "ToDate", "Logged_User_ID", "@CENID"}
            Dim values() As Object = {inParam.From_Date, inParam.ToDate, inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 12, 255, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUsedLeftManpowerCount(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COUNT(T0.REC_ID) CNT  From Stock_Personnel_Info T0  INNER JOIN Stock_Production_Manpower_Usage T1 ON T0.REC_ID = T1.SPMU_Person_ID Where T1.SPMU_Prod_id = " + ProductionID.ToString() + " AND T0.Pers_Leaving_Date IS NOT NULL "
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_PERSONNEL_INFO, Query, ConnectOneWS.Tables.STOCK_PERSONNEL_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetProdItemsConsumed(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Prod_ItemConsumed"
            Dim params() As String = {"ProdID"}
            Dim values() As Object = {ProductionID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, ConnectOneWS.Tables.STOCK_PRODUCTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetProdExpensesIncurred(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Prod_ExpensesIncurred"
            Dim params() As String = {"ProdID"}
            Dim values() As Object = {ProductionID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, ConnectOneWS.Tables.STOCK_PRODUCTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetProdManpowerUsage(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Prod_ManpowerUsage"
            Dim params() As String = {"ProdID"}
            Dim values() As Object = {ProductionID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, ConnectOneWS.Tables.STOCK_PRODUCTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetProdMachineUsage(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Prod_MachineUsage"
            Dim params() As String = {"ProdID"}
            Dim values() As Object = {ProductionID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, ConnectOneWS.Tables.STOCK_PRODUCTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetProdItemProduced(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Prod_ItemProduced"
            Dim params() As String = {"ProdID"}
            Dim values() As Object = {ProductionID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, ConnectOneWS.Tables.STOCK_PRODUCTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetProdScrapProduced(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Scrap_Items"
            Dim params() As String = {"@Ref_ID", "@ScreenType"}
            Dim values() As Object = {ProductionID, "Production - Scrap"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 50}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_INFO, SPName, ConnectOneWS.Tables.STOCK_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_Prod_Expenses_For_Mapping(ProdcutionID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Expenses_For_Mapping"
            Dim params() As String = {"@RefID", "@CEN_ID", "@YEAR_ID", "@RefScreenType"}
            Dim values() As Object = {ProdcutionID, inBasicParam.openCenID, inBasicParam.openYearID, "Production"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 4, 50}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetProdDetails(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Prod_Details"
            Dim params() As String = {"@ProdID"}
            Dim values() As Object = {ProductionID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, ConnectOneWS.Tables.STOCK_PRODUCTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function InsertProduction_Txn(ByVal InParam As Param_Insert_Production_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim ProductionID As Int32 = InsertProduction(InParam.Prod_Date, InParam.Location_ID, InParam.Lot_no, InParam.Project_ID, InParam.Worked_By, InParam.FromDate, InParam.ToDate, InParam.StoreID, inBasicParam)
            If Not InParam.Remarks = Nothing Then
                InsertProdRemarks(InParam.Remarks, ProductionID, inBasicParam)
            End If
            'Add Attachment
            If Not InParam.Added_Attachments Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In InParam.Added_Attachments
                    InAttachment.Ref_Rec_ID = ProductionID
                    InAttachment.Ref_Screen = "Production"
                    Attachments.Insert(InAttachment, inBasicParam)
                Next
            End If
            For Each inExp As Param_Insert_Production_Expenses In InParam.Expenses_Incurred
                Insert_Production_Expenses(inExp, ProductionID, inBasicParam)
            Next
            For Each inConsumed As Param_Insert_Production_ItemsConsumed In InParam.Items_Consumed
                Insert_Production_ItemsConsumed(inConsumed, ProductionID, inBasicParam)
            Next
            For Each inProduced As Param_Insert_Production_ItemsProduced In InParam.Items_Produced
                Insert_Production_ItemsProduced(inProduced, ProductionID, InParam.Prod_Date, InParam.Location_ID, InParam.Project_ID, inBasicParam)
            Next
            For Each inscrapProduced As Param_Insert_Production_ScrapProduced In InParam.Scrap_Produced
                Insert_Production_ScrapProduced(inscrapProduced, ProductionID, InParam.Prod_Date, InParam.Location_ID, InParam.Project_ID, inBasicParam)
            Next
            For Each inMachUsage As Param_Insert_Production_MachineUsage In InParam.Machine_Usage
                Insert_Production_MachineUsage(inMachUsage, ProductionID, inBasicParam)
            Next
            For Each inManUsage As Param_Insert_Production_ManpowerUsage In InParam.Manpower_Usage
                Insert_Production_ManpowerUsage(inManUsage, ProductionID, inBasicParam)
            Next
            Return True
        End Function
        Public Shared Function UpdateProduction_Txn(ByVal InParam As Param_Update_Production_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim ProductionID As Int32 = InParam.ProdID
            UpdateProduction(InParam.Prod_Date, InParam.Location_ID, InParam.Lot_no, InParam.Project_ID, InParam.Worked_By, InParam.FromDate, InParam.ToDate, ProductionID, inBasicParam)
            If Not InParam.Remarks = Nothing Then
                InsertProdRemarks(InParam.Remarks, InParam.ProdID, inBasicParam)
            End If
            'Add Attachment
            If Not InParam.Added_Attachments Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In InParam.Added_Attachments
                    If Not InAttachment Is Nothing Then
                        InAttachment.Ref_Rec_ID = ProductionID
                        InAttachment.Ref_Screen = "Production"
                        Attachments.Insert(InAttachment, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Added_Expenses_Incurred Is Nothing Then
                For Each inExp As Param_Insert_Production_Expenses In InParam.Added_Expenses_Incurred
                    If Not inExp Is Nothing Then
                        Insert_Production_Expenses(inExp, ProductionID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Added_Items_Consumed Is Nothing Then
                For Each inConsumed As Param_Insert_Production_ItemsConsumed In InParam.Added_Items_Consumed
                    If Not inConsumed Is Nothing Then
                        Insert_Production_ItemsConsumed(inConsumed, ProductionID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Added_Items_Produced Is Nothing Then
                For Each inProduced As Param_Insert_Production_ItemsProduced In InParam.Added_Items_Produced
                    If Not inProduced Is Nothing Then
                        Insert_Production_ItemsProduced(inProduced, ProductionID, InParam.Prod_Date, InParam.Location_ID, InParam.Project_ID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Added_Scrap_Produced Is Nothing Then
                For Each inscrapProduced As Param_Insert_Production_ScrapProduced In InParam.Added_Scrap_Produced
                    If Not inscrapProduced Is Nothing Then
                        Insert_Production_ScrapProduced(inscrapProduced, ProductionID, InParam.Prod_Date, InParam.Location_ID, InParam.Project_ID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Added_Machine_Usage Is Nothing Then
                For Each inMachUsage As Param_Insert_Production_MachineUsage In InParam.Added_Machine_Usage
                    If Not inMachUsage Is Nothing Then
                        Insert_Production_MachineUsage(inMachUsage, ProductionID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Added_Manpower_Usage Is Nothing Then
                For Each inManUsage As Param_Insert_Production_ManpowerUsage In InParam.Added_Manpower_Usage
                    If Not inManUsage Is Nothing Then
                        Insert_Production_ManpowerUsage(inManUsage, ProductionID, inBasicParam)
                    End If
                Next
            End If
            'Updates 
            If Not InParam.Updated_Items_Consumed Is Nothing Then
                For Each upConsumed As Param_Update_Production_ItemsConsumed In InParam.Updated_Items_Consumed
                    If Not upConsumed Is Nothing Then
                        Update_Production_ItemsConsumed(upConsumed, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Updated_Items_Produced Is Nothing Then
                For Each upProduced As Param_Update_Production_ItemsProduced In InParam.Updated_Items_Produced
                    If Not upProduced Is Nothing Then
                        Update_Production_ItemsProduced(upProduced, InParam.Prod_Date, InParam.Location_ID, InParam.Project_ID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Updated_Scrap_Produced Is Nothing Then
                For Each upscrapProduced As Param_Update_Production_ScrapProduced In InParam.Updated_Scrap_Produced
                    If Not upscrapProduced Is Nothing Then
                        Update_Production_ScrapProduced(upscrapProduced, InParam.Prod_Date, InParam.Location_ID, InParam.Project_ID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Updated_Machine_Usage Is Nothing Then
                For Each upMachUsage As Param_Update_Production_MachineUsage In InParam.Updated_Machine_Usage
                    If Not upMachUsage Is Nothing Then
                        Update_Production_MachineUsage(upMachUsage, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Updated_Manpower_Usage Is Nothing Then
                For Each upManUsage As Param_Update_Production_ManpowerUsage In InParam.Updated_Manpower_Usage
                    If Not upManUsage Is Nothing Then
                        Update_Production_ManpowerUsage(upManUsage, inBasicParam)
                    End If
                Next
            End If
            'Update Attachment
            If Not InParam.Updated_Attachments Is Nothing Then
                For Each updAttachment As Attachments.Parameter_Update_Attachment In InParam.Updated_Attachments
                    If Not updAttachment Is Nothing Then
                        Attachments.Update(updAttachment, inBasicParam)
                    End If
                Next
            End If
            'Deletes
            If Not InParam.Deleted_Expenses_Incurred_IDs Is Nothing Then
                For Each delExpID As Int32 In InParam.Deleted_Expenses_Incurred_IDs
                    If Not delExpID = Nothing Then
                        DeleteProductionExpenses(delExpID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Deleted_Items_Consumed_IDs Is Nothing Then
                For Each delConsID As Int32 In InParam.Deleted_Items_Consumed_IDs
                    If Not delConsID = Nothing Then
                        DeleteProductionItemsConsumed(delConsID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Deleted_Items_Produced_IDs Is Nothing Then
                For Each delProdID As Int32 In InParam.Deleted_Items_Produced_IDs
                    If Not delProdID = Nothing Then
                        DeleteProductionItemsProduced(delProdID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Deleted_Scrap_Produced_IDs Is Nothing Then
                For Each delScrapProdID As Int32 In InParam.Deleted_Scrap_Produced_IDs
                    If Not delScrapProdID = Nothing Then
                        DeleteProductionScrapProduced(delScrapProdID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Deleted_Machine_Usage_IDs Is Nothing Then
                For Each delMachUsageID As Int32 In InParam.Deleted_Machine_Usage_IDs
                    If Not delMachUsageID = Nothing Then
                        DeleteProductionMachineUsage(delMachUsageID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Deleted_Manpower_Usage_IDs Is Nothing Then
                For Each delManUsageID As Int32 In InParam.Deleted_Manpower_Usage_IDs
                    If Not delManUsageID = Nothing Then
                        DeleteProductionManpowerUsage(delManUsageID, inBasicParam)
                    End If
                Next
            End If
            If Not InParam.Deleted_Remarks_IDs Is Nothing Then
                For Each delRemarksID As Int32 In InParam.Deleted_Remarks_IDs
                    If Not delRemarksID = Nothing Then
                        Delete_Prod_Remarks(delRemarksID, inBasicParam)
                    End If
                Next
            End If
            'Delete Attachment
            If Not InParam.Deleted_Attachment_IDs Is Nothing Then
                For Each delAttachmentID As String In InParam.Deleted_Attachment_IDs
                    If Not delAttachmentID = Nothing Then
                        Attachments.Delete_Attachment_ByID(delAttachmentID, inBasicParam)
                    End If
                Next
            End If
            'Unlink Attachment 
            If Not InParam.Unlinked_Attachment_IDs Is Nothing Then
                For Each unlinkAttachmentID As String In InParam.Unlinked_Attachment_IDs
                    If Not unlinkAttachmentID = Nothing Then
                        Dim unlinkParam As New Attachments.Parameter_Attachment_Unlink
                        unlinkParam.AttachmentID = unlinkAttachmentID
                        unlinkParam.Ref_Rec_ID = ProductionID
                        Attachments.Unlink_Attachment(unlinkParam, inBasicParam)
                    End If
                Next
            End If
            Return True
        End Function
        Public Shared Function DeleteProduction_Txn(ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_PRODUCTION_EXPENSES_INCURRED, "SPE_PROD_ID=" + ProductionID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_PRODUCTION_ITEMS_CONSUMED, "PIC_PROD_ID=" + ProductionID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_PRODUCTION_ITEMS_PRODUCED, "PIP_PROD_ID=" + ProductionID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_INFO, "Stock_Ref_ID=" + ProductionID.ToString() + " AND Stock_Ref_Entry_Source = 'Production Produced' ", inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_PRODUCTION_MACHINE_USAGE, "PMCH_PROD_ID=" + ProductionID.ToString(), inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_PRODUCTION_MANPOWER_USAGE, "SPMU_PROD_ID=" + ProductionID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, ProductionID, inBasicParam)
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_REMARKS_INFO, "SR_Ref_ID = " + ProductionID.ToString() + " AND SR_Screen_Type = 'Production'", inBasicParam)

            'Unlink those Documents which are attached somewhere else, and delete those which are not linked elsewhere
            Dim inDocs As New Projects.Param_GetStockDocuments()
            inDocs.RefID = ProductionID
            inDocs.Screen_Type = "Production"

            Dim _Docs_in_PO As DataTable = Projects.GetStockDocuments(inDocs, inBasicParam)
            For Each cRow As DataRow In _Docs_in_PO.Rows
                Dim inparam As New Attachments.Parameter_GetAttachmentLinkCount()
                inparam.RefRecordID = ProductionID
                inparam.RefScreen = "Production"
                inparam.AttachmentID = cRow("ID")
                Dim LinkedScreenName As String = Attachments.GetAttachmentLinkCount(inparam, inBasicParam)
                If String.IsNullOrEmpty(LinkedScreenName) Then
                    Attachments.Delete_Attachment_ByReference(ProductionID, "Production", inBasicParam)
                Else
                    Attachments.Unlink_Attachment_ByReference(ProductionID, "Production", inBasicParam)
                End If
            Next

            Return True
        End Function
        Private Shared Function InsertProduction(Prod_Date As DateTime, Location_ID As String, Lot_no As String, Project_ID As Int32?, Worked_By As String, FromDate As DateTime?, ToDate As DateTime?, StoreID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Prod_Info"
            Dim params() As String = {"@Prod_Cen_ID", "@Prod_Cod_Year_ID", "@Prod_Date", "@Prod_Location_ID", "@Prod_Lot_no", "@Prod_Project_ID", "@Prod_Worked_By", "@Prod_FromDate", "@Prod_ToDate", "@UserID", "@Prod_StoreID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, Prod_Date, Location_ID, Lot_no, Project_ID, Worked_By, FromDate, ToDate, inBasicParam.openUserID, StoreID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 12, 36, 50, 4, 50, 12, 12, 255, 4}

            Return CInt(dbService.ScalarFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam))
        End Function
        Private Shared Function InsertProdRemarks(Remarks As String, ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Remarks"
            Dim params() As String = {"@User_ID", "@RefID", "@SR_Remarks", "@Screen"}
            Dim values() As Object = {inBasicParam.openUserID, ProductionID, Remarks, "Production"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 8000, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Insert_Production_Expenses(ByVal InParam As Param_Insert_Production_Expenses, ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Prod_Expense"
            Dim params() As String = {"@SPE_Prod_id", "@SPE_Exp_Tr_ID", "@SPE_Exp_Tr_Sr_No", "@UserID"}
            Dim values() As Object = {ProductionID, InParam.Exp_Tr_ID, InParam.Exp_Tr_Sr_No, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 4, 255}

            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_EXPENSES_INCURRED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Insert_Production_ItemsConsumed(ByVal InParam As Param_Insert_Production_ItemsConsumed, ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Prod_ItemConsumed"
            Dim params() As String = {"@PIC_Prod_ID", "@PIC_Stock_ID", "@PIC_Item_Qty", "@PIC_Item_Amount", "@PIC_Remarks", "@UserID"}
            Dim values() As Object = {ProductionID, InParam.Stock_ID, InParam.Item_Qty, InParam.Item_Amount, InParam.Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 19, 19, 8000, 255}

            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_ITEMS_CONSUMED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Insert_Production_ItemsProduced(ByVal InParam As Param_Insert_Production_ItemsProduced, ProductionID As Int32, Date_Of_Purchase As DateTime, Location_ID As String, Project_ID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As Boolean

            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Prod_ItemProduced"
            Dim params() As String = {"@PIP_Prod_ID", "@PIP_sub_Item_ID", "@PIP_Item_Qty_Produced", "@PIP_Item_Qty_Accepted", "@PIP_Item_Market_Rate", "@PIP_Item_Market_Price", "@PIP_Remarks", "@PIP_TotalValue_Perc", "@UserID"}
            Dim values() As Object = {ProductionID, InParam.sub_Item_ID, InParam.Item_Qty_Produced, InParam.Item_Qty_Accepted, InParam.Item_Market_Rate, InParam.Item_Market_Price, InParam.Remarks, InParam.TotalValue_Perc, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 19, 19, 19, 19, 8000, 5, 255}

            Dim ItemProdID As Integer = dbService.ScalarFromSP(ConnectOneWS.Tables.STOCK_PRODUCTION_ITEMS_PRODUCED, SPName, params, values, dbTypes, lengths, inBasicParam)



            Dim _StockData As New StockProfile.Param_Add_Stock_Addition()
            _StockData.Date_Of_Purchase = Date_Of_Purchase
            _StockData.item_id = InParam.sub_Item_ID
            _StockData.Location_Id = Location_ID
            _StockData.make = InParam.Make
            _StockData.model = InParam.Model
            _StockData.Project_ID = Project_ID
            _StockData.Quantity = InParam.Item_Qty_Accepted
            _StockData.Remarks = InParam.Remarks
            _StockData.serial_no = InParam.Lot_Serial_no
            _StockData.Stock_Ref_Entry_Source = StockProfile.Stock_Addition_Source.Production_Produced
            _StockData.Stock_Ref_ID = ProductionID
            _StockData.Stock_Ref_Sub_ID = ItemProdID
            _StockData.Store_Dept_ID = InParam.Store_ID
            _StockData.total_value = InParam.Value
            _StockData.Unit_Id = InParam.Unit_ID
            _StockData.Warranty = InParam.Warranty
            Dim StockID As Int32 = StockProfile.AddStockAddition(_StockData, inBasicParam)



            Return True
        End Function
        Private Shared Function Insert_Production_ScrapProduced(ByVal InParam As Param_Insert_Production_ScrapProduced, ProductionID As Int32, Date_Of_Purchase As DateTime, Location_ID As String, Project_ID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _StockData As New StockProfile.Param_Add_Stock_Addition()
            _StockData.Date_Of_Purchase = Date_Of_Purchase
            _StockData.item_id = InParam.sub_Item_ID
            _StockData.Location_Id = Location_ID
            _StockData.make = InParam.Make
            _StockData.model = InParam.Model
            _StockData.Project_ID = Project_ID
            _StockData.Quantity = InParam.Qty
            _StockData.Remarks = InParam.Remarks
            _StockData.serial_no = InParam.Lot_Serial_no
            _StockData.Stock_Ref_Entry_Source = StockProfile.Stock_Addition_Source.Production_Scrap
            _StockData.Stock_Ref_ID = ProductionID
            _StockData.Store_Dept_ID = InParam.Store_ID
            _StockData.total_value = InParam.Value
            _StockData.Unit_Id = InParam.Unit_ID
            _StockData.Warranty = InParam.Warranty
            Dim StockID As Int32 = StockProfile.AddStockAddition(_StockData, inBasicParam)

            Return True
        End Function
        Private Shared Function Insert_Production_MachineUsage(ByVal InParam As Param_Insert_Production_MachineUsage, ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Prod_MachineUsage"
            Dim params() As String = {"@PMch_Prod_id", "@PMch_Machine_ID", "@PMch_Machine_Count", "@PMch_Machine_Usage", "@PMch_Machine_Remarks", "@UserID"}
            Dim values() As Object = {ProductionID, InParam.Machine_ID, InParam.Machine_Count, InParam.Machine_Usage, InParam.Machine_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 19, 19, 8000, 255}

            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_MACHINE_USAGE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Insert_Production_ManpowerUsage(ByVal InParam As Param_Insert_Production_ManpowerUsage, ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_Prod_ManpowerUsage"
            Dim params() As String = {"@SPMU_Prod_id", "@SPMU_Person_ID", "@SPMU_Period_From", "@SPMU_Period_To", "@SPMU_Units_Worked", "@SPMU_Total_Amount", "@SPMU_Remarks", "@UserID", "@Pers_Charge_ID"}
            Dim values() As Object = {ProductionID, InParam.Person_ID, InParam.Period_From, InParam.Period_To, InParam.Units_Worked, InParam.Total_Amount, InParam.Remarks, inBasicParam.openUserID, InParam.Charge_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 12, 12, 9, 19, 8000, 255, 4}

            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_MANPOWER_USAGE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateProduction(Prod_Date As DateTime, Location_ID As String, Lot_no As String, Project_ID As Int32?, Worked_By As String, FromDate As DateTime?, ToDate As DateTime?, ProductionID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_Prod_Info"
            Dim params() As String = {"@ProdID", "@Prod_Date", "@Prod_Location_ID", "@Prod_Lot_no", "@Prod_Project_ID", "@Prod_Worked_By", "@Prod_FromDate", "@Prod_ToDate", "@UserID"}
            Dim values() As Object = {ProductionID, Prod_Date, Location_ID, Lot_no, Project_ID, Worked_By, FromDate, ToDate, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.String}
            Dim lengths() As Integer = {4, 12, 36, 50, 4, 50, 12, 12, 255}

            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Update_Production_ItemsConsumed(ByVal InParam As Param_Update_Production_ItemsConsumed, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_Prod_ItemConsumed"
            Dim params() As String = {"@PIC_ID", "@PIC_Stock_ID", "@PIC_Item_Qty", "@PIC_Item_Amount", "@PIC_Remarks", "@UserID"}
            Dim values() As Object = {InParam.ID, InParam.Stock_ID, InParam.Item_Qty, InParam.Item_Amount, InParam.Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 19, 19, 8000, 255}

            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_ITEMS_CONSUMED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Update_Production_ItemsProduced(ByVal InParam As Param_Update_Production_ItemsProduced, Date_Of_Purchase As DateTime, Location_ID As String, Project_ID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _StockData As New StockProfile.Param_Update_StockProfile()
            _StockData.Date_Of_Purchase = Date_Of_Purchase
            _StockData.sub_Item_ID = InParam.sub_Item_ID
            _StockData.Location_Id = Location_ID
            _StockData.make = InParam.Make
            _StockData.model = InParam.Model
            _StockData.Project_ID = Project_ID
            _StockData.Quantity = InParam.Item_Qty_Accepted
            _StockData.Remarks = InParam.Remarks
            _StockData.serial_no = InParam.Lot_Serial_no
            _StockData.Store_Dept_ID = InParam.Store_ID
            _StockData.total_value = InParam.Value
            _StockData.Unit_Id = InParam.Unit_ID
            _StockData.Warranty = InParam.Warranty
            _StockData.Rec_ID = InParam.stock_ID
            StockProfile.UpdateStockProfile(_StockData, inBasicParam)

            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_Prod_ItemProduced"
            Dim params() As String = {"@PIP_ID", "@PIP_sub_Item_ID", "@PIP_Item_Qty_Produced", "@PIP_Item_Qty_Accepted", "@PIP_Item_Market_Rate", "@PIP_Item_Market_Price", "@PIP_Remarks", "@PIP_TotalValue_Perc", "@UserID"}
            Dim values() As Object = {InParam.ID, InParam.sub_Item_ID, InParam.Item_Qty_Produced, InParam.Item_Qty_Accepted, InParam.Item_Market_Rate, InParam.Item_Market_Price, InParam.Remarks, InParam.TotalValue_Perc, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 19, 19, 19, 19, 8000, 5, 255}

            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_ITEMS_PRODUCED, SPName, params, values, dbTypes, lengths, inBasicParam)

            Return True
        End Function
        Private Shared Function Update_Production_MachineUsage(ByVal InParam As Param_Update_Production_MachineUsage, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_Prod_MachineUsage"
            Dim params() As String = {"@Pmch_ID", "@PMch_Machine_ID", "@PMch_Machine_Count", "@PMch_Machine_Usage", "@PMch_Machine_Remarks", "@UserID"}
            Dim values() As Object = {InParam.ID, InParam.Machine_ID, InParam.Machine_Count, InParam.Machine_Usage, InParam.Machine_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 19, 19, 8000, 255}

            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_MACHINE_USAGE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Update_Production_ManpowerUsage(ByVal InParam As Param_Update_Production_ManpowerUsage, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_Prod_ManpowerUsage"
            Dim params() As String = {"@SPMU_ID", "@SPMU_Person_ID", "@SPMU_Period_From", "@SPMU_Period_To", "@SPMU_Units_Worked", "@SPMU_Total_Amount", "@SPMU_Remarks", "@UserID"}
            Dim values() As Object = {InParam.ID, InParam.Person_ID, InParam.Period_From, InParam.Period_To, InParam.Units_Worked, InParam.Total_Amount, InParam.Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.DateTime2, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 12, 12, 9, 19, 8000, 255}

            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_PRODUCTION_MANPOWER_USAGE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Update_Production_ScrapProduced(ByVal InParam As Param_Update_Production_ScrapProduced, Date_Of_Purchase As DateTime, Location_ID As String, Project_ID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _StockData As New StockProfile.Param_Update_StockProfile()
            _StockData.Date_Of_Purchase = Date_Of_Purchase
            _StockData.sub_Item_ID = InParam.sub_Item_ID
            _StockData.Location_Id = Location_ID
            _StockData.make = InParam.Make
            _StockData.model = InParam.Model
            _StockData.Project_ID = Project_ID
            _StockData.Quantity = InParam.Qty
            _StockData.Remarks = InParam.Remarks
            _StockData.serial_no = InParam.Lot_Serial_no
            _StockData.Store_Dept_ID = InParam.Store_ID
            _StockData.total_value = InParam.Value
            _StockData.Unit_Id = InParam.Unit_ID
            _StockData.Warranty = InParam.Warranty
            _StockData.Rec_ID = InParam.ID
            Dim StockID As Int32 = StockProfile.UpdateStockProfile(_StockData, inBasicParam)

            Return True
        End Function
        Private Shared Function Delete_Prod_Remarks(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_REMARKS_INFO, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteProduction(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_PRODUCTION_INFO, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteProductionExpenses(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_PRODUCTION_EXPENSES_INCURRED, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteProductionItemsConsumed(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_PRODUCTION_ITEMS_CONSUMED, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteProductionItemsProduced(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim Query As String = "Select REC_ID From Stock_Info Where Stock_Ref_Sub_ID=" + ID.ToString() + "And Stock_Ref_Entry_Source = 'Production Produced'"

            Dim StockID As Integer = dbService.GetScalar(ConnectOneWS.Tables.STOCK_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)

            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_INFO, "Rec_ID=" + StockID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.STOCK_PRODUCTION_ITEMS_PRODUCED, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteProductionMachineUsage(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_PRODUCTION_MACHINE_USAGE, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteProductionManpowerUsage(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_PRODUCTION_MANPOWER_USAGE, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteProductionScrapProduced(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_INFO, ID, inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class SubItem
#Region "Param Classes"
        <Serializable>
        Public Class Param_SubItem_GetUsageList
            Public SubItemID As Int32
            Public StoreID As Int32 = Nothing
        End Class
        <Serializable>
        Public Class Param_Insert_SubItem
            Public Sub_Item_Item_Id As String
            Public Sub_Item_Name As String
            Public Sub_Item_Main_Category As String
            Public Sub_Item_Sub_Category As String
            Public Sub_Item_Code As String
            Public Sub_Item_Unit_Id As String
            Public Sub_Item_Image As Byte()
            Public Sub_Item_Remarks As String
            Public Sub_Item_Consumption_Type As String

        End Class
        <Serializable>
        Public Class Param_SubItem_Insert_Txn
            Inherits Param_Insert_SubItem
            Public Param_Sub_Item_Store_Mapping As Param_SubItem_Insert_store_Mapping()
            Public Param_Sub_Item_Unit_Conversion As Param_SubItem_Insert_Unit_Conversion()
            Public Param_Sub_Item_Item_Properties As Param_SubItem_Insert_Item_Properties()
        End Class
        <Serializable>
        Public Class Param_SubItem_Update_Txn
            Inherits Param_SubItem_Insert_Txn
            Public Sub_Item_ID As Int32
        End Class
        <Serializable>
        Public Class Param_CloseSubItem
            Public Sub_Item_ID As Int32
            Public CloseDate As DateTime
            Public CloseRemarks As String
        End Class
        <Serializable>
        Public Class Param_GetStockItems
            Public Consumption_Type As String
            Public Main_Category As String
            Public Sub_Category As String
            Public Search_Text As String
            Public StoreID As Int32?
        End Class
        <Serializable>
        Public Class Param_GetFilteredStockItems
            Public Consumption_Type As String
            Public Main_Category As String
            Public Sub_Category As String
            Public Search_Text As String
            Public StoreID As Int32?
        End Class
        <Serializable>
        Public Class Param_GetStoreItems
            Public StoreID As Int32?
            Public CEN_ID As Int32?

        End Class
        <Serializable>
        Public Class Param_SubItem_Update_Store_Mapping
            Public Store_ID As Int32
            Public mapped_id As String
            Public unmapped_id As String
        End Class
        <Serializable>
        Public Class Param_SubItem_Insert_store_Mapping
            Public Sub_Item_ID As Int32
            Public Store_ID As Int32
        End Class
        <Serializable>
        Public Class Param_SubItem_Insert_Unit_Conversion
            Public Sub_Item_ID As Int32
            Public Unit_ID As String
            Public Rate_for_Conversion As Decimal
            Public Effective_From As DateTime
            Public Effective_till As DateTime? = DateTime.MinValue
        End Class
        <Serializable>
        Public Class Param_SubItem_Insert_Item_Properties
            Public Sub_Item_ID As Int32
            Public Property_Name As String
            Public Property_Value As String
            Public Property_Remarks As String

        End Class
#End Region
        Public Shared Function GetList(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_sub_items_Listing"
            Dim params() As String = {"User_ID"}
            Dim values() As Object = {inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String}
            Dim lengths() As Integer = {255}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetList(StoreID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_sub_items_Listing"
            Dim params() As String = {"User_ID", "Store_ID"}
            Dim values() As Object = {inBasicParam.openUserID, StoreID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStockItems(inParam As Param_GetStockItems, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Stock_Items"
            Dim params() As String = {"user_id", "@Consumption_Type", "@Main_Category", "@Sub_Category", "@Search_Text", "@StoreID"}
            Dim values() As Object = {inBasicParam.openUserID, inParam.Consumption_Type, inParam.Main_Category, inParam.Sub_Category, inParam.Search_Text, inParam.StoreID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 255, 255, 255, 255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetFilteredStockItems(inParam As Param_GetFilteredStockItems, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Filtered_Stock_Items"
            Dim params() As String = {"@Consumption_Type", "@Main_Category", "@Sub_Category", "@Search_Text", "@StoreID"}
            Dim values() As Object = {inParam.Consumption_Type, inParam.Main_Category, inParam.Sub_Category, inParam.Search_Text, inParam.StoreID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 255, 255, 255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetStoreItems(inparam As Param_GetStoreItems, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Store_Items"
            Dim params() As String = {"@RequesteeStore", "@CEN_ID"}
            Dim values() As Object = {inparam.StoreID, inparam.CEN_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetUsageList(Inparam As Param_SubItem_GetUsageList, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_get_Sub_Item_Usage"
            Dim params() As String = {"@Sub_Item_Id", "@Store_ID"}
            Dim values() As Object = {Inparam.SubItemID, IIf(Inparam.StoreID = Nothing, DBNull.Value, Inparam.StoreID)}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPropertiesList_SubItem(SubItemID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT ROW_NUMBER()  OVER (ORDER BY  Prop_Property_Name) As SrNo, PROP.Prop_Property_Name AS 'Property Name', PROP.Prop_Property_Value as 'Property Value', PROP.Prop_Property_Remarks AS Remarks FROM Sub_item_Properties PROP WHERE PROP.Prop_Sub_Item_ID = " + SubItemID.ToString() + " ORDER BY SrNo"
            Return dbService.List(ConnectOneWS.Tables.SUB_ITEM_PROPERTIES, Query, ConnectOneWS.Tables.SUB_ITEM_PROPERTIES.ToString(), inBasicParam)
        End Function
        Public Shared Function GetUnitConversionList_SubItem(SubItemID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Dim Query As String = "SELECT ROW_NUMBER()  OVER (ORDER BY UNIT.MISC_NAME) As SrNo, UNIT.MISC_NAME AS 'Converted Unit', CU_Rate as 'Rate of Conversion', CU_Eff_Date as 'Effective Date'" &
            '                        " FROM Sub_item_Unit_Conversion SI_UNIT " &
            '                        " INNER JOIN misc_info AS UNIT ON SI_UNIT.CU_Unit_ID = UNIT.REC_ID" &
            '                        " WHERE SI_UNIT.CU_Sub_Item_ID = " + SubItemID.ToString() + " ORDER BY SrNo"

            Dim Query As String = "SELECT ROW_NUMBER()  OVER (ORDER BY UNIT.MISC_NAME) As SrNo, UNIT.MISC_NAME AS 'Converted Unit', CU_Rate as 'Rate of Conversion', CU_Eff_Date as 'Effective Date', SI_UNIT.CU_Unit_ID As UnitID" &
                                    " FROM Sub_item_Unit_Conversion SI_UNIT " &
                                    " INNER JOIN misc_info AS UNIT ON SI_UNIT.CU_Unit_ID = UNIT.REC_ID" &
                                    " WHERE SI_UNIT.CU_Sub_Item_ID = " + SubItemID.ToString() + " ORDER BY SrNo"
            Return dbService.List(ConnectOneWS.Tables.SUB_ITEM_UNIT_CONVERSION, Query, ConnectOneWS.Tables.SUB_ITEM_UNIT_CONVERSION.ToString(), inBasicParam)
        End Function
        Public Shared Function GetItemPropertiesMaster(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT DISTINCT Prop_Property_Name as Property FROM Sub_item_Properties"
            Return dbService.List(ConnectOneWS.Tables.SUB_ITEM_PROPERTIES, Query, ConnectOneWS.Tables.SUB_ITEM_PROPERTIES.ToString(), inBasicParam)
        End Function
        Public Shared Function GetMainCategoriesMaster(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT DISTINCT Sub_Item_Main_Category as 'Main Category' FROM Sub_item_info"
            Return dbService.List(ConnectOneWS.Tables.SUB_ITEM_INFO, Query, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetSubCategoriesMaster(MainCategory As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT DISTINCT Sub_Item_Sub_Category as 'Sub Category' FROM Sub_item_info where Sub_Item_Main_Category  ='" + MainCategory + "'"
            Return dbService.List(ConnectOneWS.Tables.SUB_ITEM_INFO, Query, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetRecord(SubItemID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim dSet As DataSet = New DataSet
            Dim Query As String = "select SI.*, ITEM_NAME AS Accounting_Item FROM Sub_item_info SI INNER JOIN item_info AS ITEM ON Sub_Item_Item_Id = ITEM.REC_ID where si.Rec_ID  =" + SubItemID.ToString() + ""
            dSet.Tables.Add(dbService.GetSingleRecord(ConnectOneWS.Tables.SUB_ITEM_INFO, Query, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), inBasicParam).Copy)

            Query = "select Map_Store_ID from Sub_item_Store_Mapping where Map_Sub_Item_ID = " + SubItemID.ToString() + ""
            dSet.Tables.Add(dbService.List(ConnectOneWS.Tables.SUB_ITEM_STORE_MAPPING, Query, ConnectOneWS.Tables.SUB_ITEM_STORE_MAPPING.ToString(), inBasicParam).Copy)
            Return dSet
        End Function
        Private Shared Function InsertSubItem(inparam As Param_Insert_SubItem, inBasicParam As ConnectOneWS.Basic_Param) As Integer
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "sp_Insert_Stock_Item"
            Dim params() As String = {"@user_id", "@Sub_Item_Item_Id", "@Sub_Item_Name", "@Sub_Item_Main_Category", "@Sub_Item_Sub_Category", "@Sub_Item_Unit_Id", "@Sub_Item_Image", "@Sub_Item_Remarks", "@Sub_Item_Consumption_Type"}
            Dim values() As Object = {inBasicParam.openUserID, inparam.Sub_Item_Item_Id, inparam.Sub_Item_Name, inparam.Sub_Item_Main_Category, inparam.Sub_Item_Sub_Category, inparam.Sub_Item_Unit_Id, inparam.Sub_Item_Image, inparam.Sub_Item_Remarks, inparam.Sub_Item_Consumption_Type}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, SqlDbType.Binary, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 36, 255, 255, 255, 36, 0, 8000, 255}

            Dim subItemId As Integer = dbService.ScalarFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return subItemId
        End Function
        Public Shared Function InsertSubItem_Txn(InParam_Txn As Param_SubItem_Insert_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim inparam As New Param_Insert_SubItem

            inparam.Sub_Item_Item_Id = InParam_Txn.Sub_Item_Item_Id
            inparam.Sub_Item_Name = InParam_Txn.Sub_Item_Name
            inparam.Sub_Item_Main_Category = InParam_Txn.Sub_Item_Main_Category
            inparam.Sub_Item_Sub_Category = InParam_Txn.Sub_Item_Sub_Category
            inparam.Sub_Item_Code = InParam_Txn.Sub_Item_Code
            inparam.Sub_Item_Unit_Id = InParam_Txn.Sub_Item_Unit_Id
            inparam.Sub_Item_Image = InParam_Txn.Sub_Item_Image
            inparam.Sub_Item_Remarks = InParam_Txn.Sub_Item_Remarks
            inparam.Sub_Item_Consumption_Type = InParam_Txn.Sub_Item_Consumption_Type


            Dim subItemId As Integer = InsertSubItem(inparam, inBasicParam)

            If Not InParam_Txn.Param_Sub_Item_Store_Mapping Is Nothing Then
                For Each inStoreMap_Insert As Param_SubItem_Insert_store_Mapping In InParam_Txn.Param_Sub_Item_Store_Mapping
                    inStoreMap_Insert.Sub_Item_ID = subItemId
                    InsertItemStoreMapping(inStoreMap_Insert, inBasicParam)
                Next
            End If

            If Not InParam_Txn.Param_Sub_Item_Unit_Conversion Is Nothing Then
                For Each InUnitConversion_Insert As Param_SubItem_Insert_Unit_Conversion In InParam_Txn.Param_Sub_Item_Unit_Conversion
                    InUnitConversion_Insert.Sub_Item_ID = subItemId
                    InsertItemUnitconversion(InUnitConversion_Insert, inBasicParam)
                Next
            End If

            If Not InParam_Txn.Param_Sub_Item_Item_Properties Is Nothing Then
                For Each InItemProperties_Insert As Param_SubItem_Insert_Item_Properties In InParam_Txn.Param_Sub_Item_Item_Properties
                    InItemProperties_Insert.Sub_Item_ID = subItemId
                    InsertItemProperties(InItemProperties_Insert, inBasicParam)
                Next
            End If

            Return True
        End Function
        Private Shared Function InsertItemStoreMapping(ByVal InParam As Param_SubItem_Insert_store_Mapping, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "sp_Insert_Stock_Item_Store_Mapping"
            Dim params() As String = {"@user_id", "@Store_ID", "@Sub_Item_Id"}
            Dim values() As Object = {inBasicParam.openUserID, InParam.Store_ID, InParam.Sub_Item_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 4, 4}

            dbService.InsertBySP(ConnectOneWS.Tables.SUB_ITEM_STORE_MAPPING, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertItemUnitconversion(ByVal InParam As Param_SubItem_Insert_Unit_Conversion, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "sp_Insert_Stock_Item_Unit_Conversion"
            Dim params() As String = {"@Sub_Item_Item_Id", "@Sub_Item_Unit_ID", "@Sub_Item_Rate", "@Sub_Item_Eff_Date", "@Sub_Item_Eff_Till"}
            Dim values() As Object = {InParam.Sub_Item_ID, InParam.Unit_ID, InParam.Rate_for_Conversion, InParam.Effective_From, InParam.Effective_till}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.DateTime2}
            Dim lengths() As Integer = {4, 36, 11, 8, 8}

            dbService.InsertBySP(ConnectOneWS.Tables.SUB_ITEM_UNIT_CONVERSION, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertItemProperties(ByVal InParam As Param_SubItem_Insert_Item_Properties, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "sp_Insert_Stock_Item_Properties"
            Dim params() As String = {"@Sub_Item_Item_Id", "@Sub_Item_Property_Name", "@Sub_Item_Property_Value", "@Prop_Property_Remarks"}
            Dim values() As Object = {InParam.Sub_Item_ID, InParam.Property_Name, InParam.Property_Value, InParam.Property_Remarks}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 255, 255}

            dbService.InsertBySP(ConnectOneWS.Tables.SUB_ITEM_PROPERTIES, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function

        Public Shared Function UpdateSubItem_Txn(ByVal InParam As Param_SubItem_Update_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "sp_Update_Stock_Item"
            Dim params() As String = {"@user_id", "@Sub_Item_Item_Id", "@Sub_Item_Name", "@Sub_Item_Main_Category", "@Sub_Item_Sub_Category", "@Sub_Item_Code", "@Sub_Item_Unit_Id", "@Sub_Item_Image", "@Sub_Item_Remarks", "@Sub_Item_Consumption_Type", "@Sub_Item_ID"}
            Dim values() As Object = {inBasicParam.openUserID, InParam.Sub_Item_Item_Id, InParam.Sub_Item_Name, InParam.Sub_Item_Main_Category, InParam.Sub_Item_Sub_Category, InParam.Sub_Item_Code, InParam.Sub_Item_Unit_Id, InParam.Sub_Item_Image, InParam.Sub_Item_Remarks, InParam.Sub_Item_Consumption_Type, InParam.Sub_Item_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, SqlDbType.Binary, Data.DbType.String, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {255, 36, 255, 255, 255, 30, 36, 0, 8000, 255, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Delete and re-add mappings, Units conversion and Properties 
            DeleteSubItemStoreMapping(InParam.Sub_Item_ID, inBasicParam)
            DeleteSubItemUnitconversion(InParam.Sub_Item_ID, inBasicParam)
            DeleteSubItemProperties(InParam.Sub_Item_ID, inBasicParam)
            'Assuming that the check has been made on client side to check any already used Item store mapping is not being removed 
            If Not InParam.Param_Sub_Item_Store_Mapping Is Nothing Then
                For Each inStoreMap_Insert As Param_SubItem_Insert_store_Mapping In InParam.Param_Sub_Item_Store_Mapping
                    InsertItemStoreMapping(inStoreMap_Insert, inBasicParam)
                Next
            End If
            If Not InParam.Param_Sub_Item_Unit_Conversion Is Nothing Then
                For Each InUnitConversion_Insert As Param_SubItem_Insert_Unit_Conversion In InParam.Param_Sub_Item_Unit_Conversion
                    InsertItemUnitconversion(InUnitConversion_Insert, inBasicParam)
                Next
            End If
            If Not InParam.Param_Sub_Item_Item_Properties Is Nothing Then
                For Each InItemProperties_Insert As Param_SubItem_Insert_Item_Properties In InParam.Param_Sub_Item_Item_Properties
                    InsertItemProperties(InItemProperties_Insert, inBasicParam)
                Next
            End If
            Return True
        End Function
        Public Shared Function UpdateSubItem_Store_Mapping(ByVal InParam As Param_SubItem_Update_Store_Mapping, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "sp_Update_Sub_Item_Store_Mapping"
            Dim params() As String = {"@user_id", "@Store_ID", "@mapped_id", "@unmapped_id"}
            Dim values() As Object = {inBasicParam.openUserID, InParam.Store_ID, InParam.mapped_id, InParam.unmapped_id}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, -1, -1}
            dbService.UpdateBySP(ConnectOneWS.Tables.SUB_ITEM_STORE_MAPPING, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function CloseSubItem(ByVal InParam As Param_CloseSubItem, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Update(ConnectOneWS.Tables.SUB_ITEM_INFO, "UPDATE Sub_item_info SET Sub_Item_Close_Date = '" + InParam.CloseDate.ToString(Common.Server_Date_Format_Long) + "', SUB_ITEM_CLOSE_REMARKS = '" + InParam.CloseRemarks + "' WHERE REC_ID = " + InParam.Sub_Item_ID.ToString(), inBasicParam)
            Return True
        End Function
        Public Shared Function ReopenSubItem(ByVal subItemID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Update(ConnectOneWS.Tables.SUB_ITEM_INFO, "UPDATE Sub_item_info SET Sub_Item_Close_Date = NULL, SUB_ITEM_CLOSE_REMARKS = NULL WHERE REC_ID = " + subItemID.ToString(), inBasicParam)
            Return True
        End Function
        Public Shared Function DeleteSubItem_Txn(ByVal SubItemID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            'Assuming that the usage checks have been made on client side 
            'Delete mappings to stores 
            DeleteSubItemStoreMapping(SubItemID, inBasicParam)
            'Delete Unit Conversion 
            DeleteSubItemUnitconversion(SubItemID, inBasicParam)
            'Delete Item Properties
            DeleteSubItemProperties(SubItemID, inBasicParam)

            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.SUB_ITEM_INFO, SubItemID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteSubItemStoreMapping(ByVal SubItemID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.SUB_ITEM_STORE_MAPPING, "Map_Sub_Item_ID = " + SubItemID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteSubItemProperties(ByVal SubItemID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.SUB_ITEM_PROPERTIES, "Prop_Sub_Item_ID = " + SubItemID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteSubItemUnitconversion(ByVal SubItemID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.SUB_ITEM_UNIT_CONVERSION, "CU_Sub_Item_ID = " + SubItemID.ToString(), inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class StockRequisitionRequest
#Region "Param Classes"
        <Serializable>
        Public Enum RR_Status
            _New
            Cancelled
            Rejected
            Changes_Recommended
            Submitted_for_Approval
            Re_Requisition_Requested
            Approved
            Completed
        End Enum
        <Serializable>
        Public Class Param_GetRequisitionRequestRegister
            Public From_Date As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_Get_RR_Items
            Public RR_ID As Int32
            Public UO_ID As Int32?
            Public SubItemID As String

        End Class

        <Serializable>
        Public Class Param_Get_RR_Tax_Detail
            Public RR_ID As Int32
            Public RRItemID As Int32
            Public SubItemID As Int32

        End Class
        <Serializable>
        Public Class Param_Insert_RequisitionRequest_Txn
            Inherits Param_InsertRequisitionRequest
            ''' <summary>
            ''' Array of Attachments added during Addition of Requisition
            ''' </summary>
            Public Added_Attachments As Attachments.Parameter_Insert_Attachment()
            Public Added_Items_Requested As List(Of Param_InsertRequisitionRequestItem)
        End Class
        <Serializable>
        Public Class Param_Update_RequisitionRequest_Txn
            Inherits Param_UpdateRequisitionRequest
            ''' <summary>
            ''' Array of Attachments added during Addition of Requisition
            ''' </summary>
            Public Added_Attachments As Attachments.Parameter_Insert_Attachment()
            Public Added_Items_Requested As List(Of Param_InsertRequisitionRequestItem)

            ''' <summary>
            ''' Array of Documents updated during Update of Production
            ''' </summary>
            Public Updated_Attachments As Attachments.Parameter_Update_Attachment()
            Public Updated_Items_Requested As List(Of Param_UpdateRequisitionRequestItem)

            Public Deleted_Attachment_IDs As List(Of String)
            Public Unlinked_Attachment_IDs As List(Of String)
            Public Deleted_Items_Requested_IDs As List(Of Int32)
            Public Deleted_Remarks_IDs As List(Of Int32)
        End Class
        <Serializable>
        Public Class Param_InsertRequisitionRequest
            Public RR_Date As DateTime
            Public Project_ID As Int32?
            Public Job_ID As Int32?
            Public Requestor_ID As Int32
            Public RR_Type As String
            Public Purchased_by_ID As Int32?
            Public Trf_From_Dept_ID As Int32?
            Public Requesting_Dept_ID As Int32
            Public Special_Discount As Decimal?
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_InsertRequisitionRequestItem
            Public supplier_ID As Int32?
            Public Make As String
            Public Model As String
            Public Qty_Requested As Decimal
            Public Qty_Approved As Decimal?
            Public Unit_ID As String
            Public Rate As Decimal?
            Public Discount_Promised As Decimal?
            Public Taxes As Decimal?
            Public Amount As Decimal?
            Public Priority As String
            Public Reqd_Delivery_Date As DateTime
            Public Remarks As String
            Public Sub_Item_ID As Int32
            Public Dest_Location_ID As String

            Public _Item_Taxes As List(Of Param_Insert_Tax_Details)
        End Class

        <Serializable>
        Public Class Param_Insert_Tax_Details
            Public RequestedItem_ID As Int32
            Public TaxTypeID As String
            Public TaxPercent As Decimal
            Public TaxRemarks As String
        End Class
        <Serializable>
        Public Class Param_UpdateRequisitionRequest
            Inherits Param_InsertRequisitionRequest
            Public RR_ID As Int32
        End Class
        <Serializable>
        Public Class Param_UpdateRequisitionRequestItem
            Inherits Param_InsertRequisitionRequestItem
            Public RRI_ID As Int32
            Public _Added_Item_Taxes As List(Of Param_Insert_Tax_Details)
            Public _Deleted_Item_Taxes As List(Of Int32)
        End Class
        <Serializable>
        Public Class Param_Update_RR_Status
            Public UpdatedStatus As RR_Status
            Public RRID As Int32
        End Class
#End Region
        Public Shared Function GetRegister(inParam As Param_GetRequisitionRequestRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_RR_Register"
            Dim params() As String = {"FromDate", "ToDate", "Logged_User_ID", "CEN_ID"}
            Dim values() As Object = {inParam.From_Date, inParam.ToDate, inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 12, 255, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_RR_Detail(RR_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_RR_Details"
            Dim params() As String = {"@RR_ID"}
            Dim values() As Object = {RR_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_RR_Items(inparam As Param_Get_RR_Items, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_RR_Items"
            Dim params() As String = {"@RR_ID", "@UO_ID", "@UOI_SubItems"}
            Dim values() As Object = {inparam.RR_ID, inparam.UO_ID, IIf(inparam.SubItemID = Nothing, DBNull.Value, inparam.SubItemID)}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, -1}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_RR_Linked_UO(RR_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_RR_LinkedUO"
            Dim params() As String = {"@RR_ID"}
            Dim values() As Object = {RR_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_RR_Usage_Count(RR_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE((SELECT count(RRM_UO_ID) from RR_UO_Mapping Where RRM_RR_ID =" + RR_ID.ToString() + "),0)UO_Count , COALESCE((Select count(RRM_PO_ID) PO_Count from RR_PO_Mapping Where RRM_RR_ID = " + RR_ID.ToString() + "),0) PO_Count, COALESCE((Select count(REC_ID) TO_Count from Stock_Transfer_Order Where STO_RR_ID = " + RR_ID.ToString() + "),0) TO_Count"
            Return dbService.List(ConnectOneWS.Tables.STOCK_TRANSFER_ORDER, Query, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_PO_TO_Incomplete_Count(RR_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE(SUM(A.Incomplete_Count), 0) Incomplete_Count From (Select COUNT(T0.RRM_PO_ID) Incomplete_Count From RR_PO_Mapping T0 Inner Join Purchase_Order_Info T1 On T0.RRM_PO_ID = T1.REC_ID Where T0.RRM_RR_ID = " + RR_ID.ToString() + " And T1.PO_Status <> 'Completed' UNION ALL Select COALESCE((Select count(REC_ID) Incomplete_Count from Stock_Transfer_Order Where STO_RR_ID = " + RR_ID.ToString() + " And STO_Status <> 'Completed'),0) TO_Count) A" '//Mantis bug 0000715 fixed
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_TRANSFER_ORDER, Query, ConnectOneWS.Tables.REQUISITION_REQUEST_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_RR_Tax_Detail(inparam As Param_Get_RR_Tax_Detail, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_RR_Item_Taxes"
            Dim params() As String = {"@RR_ID", "@RRI_ID", "@SubItemID"}
            Dim values() As Object = {inparam.RR_ID, inparam.RRItemID, inparam.SubItemID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM_TAXES, SPName, ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM_TAXES.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function InsertRequisitionRequest_Txn(ByVal InParam As Param_Insert_RequisitionRequest_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim RR_ID As Int32 = InsertRequisitionRequest(InParam.RR_Date, InParam.Project_ID, InParam.Job_ID, InParam.Requestor_ID, InParam.RR_Type, InParam.Purchased_by_ID, InParam.Trf_From_Dept_ID, InParam.Requesting_Dept_ID, InParam.Special_Discount, inBasicParam)
            If Not InParam.Remarks = Nothing Then
                InsertRequisitionRequestRemarks(InParam.Remarks, RR_ID, inBasicParam)
            End If
            'Add Attachment
            If Not InParam.Added_Attachments Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In InParam.Added_Attachments
                    InAttachment.Ref_Rec_ID = RR_ID
                    InAttachment.Ref_Screen = "Requisition"
                    Attachments.Insert(InAttachment, inBasicParam)
                Next
            End If
            For Each inItemRequested As Param_InsertRequisitionRequestItem In InParam.Added_Items_Requested
                InsertRequisitionRequestItem(RR_ID, inItemRequested, inBasicParam)
            Next
            Return RR_ID
        End Function
        Public Shared Function UpdateRequisitionRequest_Txn(ByVal InParam As Param_Update_RequisitionRequest_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            UpdateRequisitionRequest(InParam.RR_ID, InParam.RR_Date, InParam.Project_ID, InParam.Job_ID, InParam.Requestor_ID, InParam.RR_Type, InParam.Purchased_by_ID, InParam.Trf_From_Dept_ID, InParam.Requesting_Dept_ID, InParam.Special_Discount, inBasicParam)
            If Not InParam.Remarks = Nothing Then
                InsertRequisitionRequestRemarks(InParam.Remarks, InParam.RR_ID, inBasicParam)
            End If
            'Add Attachment
            If Not InParam.Added_Attachments Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In InParam.Added_Attachments
                    If Not InAttachment Is Nothing Then
                        InAttachment.Ref_Rec_ID = InParam.RR_ID
                        InAttachment.Ref_Screen = "Requisition"
                        Attachments.Insert(InAttachment, inBasicParam)
                    End If
                Next
            End If
            For Each inItemRequested As Param_InsertRequisitionRequestItem In InParam.Added_Items_Requested
                If Not inItemRequested Is Nothing Then InsertRequisitionRequestItem(InParam.RR_ID, inItemRequested, inBasicParam)
            Next
            '''Updates
            'Update Attachment
            If Not InParam.Updated_Attachments Is Nothing Then
                For Each updAttachment As Attachments.Parameter_Update_Attachment In InParam.Updated_Attachments
                    If Not updAttachment Is Nothing Then
                        Attachments.Update(updAttachment, inBasicParam)
                    End If
                Next
            End If
            'update Item Requested
            For Each upItemRequested As Param_UpdateRequisitionRequestItem In InParam.Updated_Items_Requested
                If Not upItemRequested Is Nothing Then UpdateRequisitionRequestItem(InParam.RR_ID, upItemRequested, inBasicParam)
            Next
            '''Deletes
            If Not InParam.Deleted_Remarks_IDs Is Nothing Then
                For Each delRemarksID As Int32 In InParam.Deleted_Remarks_IDs
                    If Not delRemarksID = Nothing Then
                        Delete_RR_Remarks(delRemarksID, inBasicParam)
                    End If
                Next
            End If
            'Delete Attachment
            If Not InParam.Deleted_Attachment_IDs Is Nothing Then
                For Each delAttachmentID As String In InParam.Deleted_Attachment_IDs
                    If Not delAttachmentID = Nothing Then
                        Attachments.Delete_Attachment_ByID(delAttachmentID, inBasicParam)
                    End If
                Next
            End If
            'Unlink Attachment 
            If Not InParam.Unlinked_Attachment_IDs Is Nothing Then
                For Each unlinkAttachmentID As String In InParam.Unlinked_Attachment_IDs
                    If Not unlinkAttachmentID = Nothing Then
                        Dim unlinkParam As New Attachments.Parameter_Attachment_Unlink
                        unlinkParam.AttachmentID = unlinkAttachmentID
                        unlinkParam.Ref_Rec_ID = InParam.RR_ID
                        Attachments.Unlink_Attachment(unlinkParam, inBasicParam)
                    End If
                Next
            End If
            'Delete Requested Items
            If Not InParam.Deleted_Items_Requested_IDs Is Nothing Then
                For Each delItemReqID As Int32 In InParam.Deleted_Items_Requested_IDs
                    If Not delItemReqID = Nothing Then
                        DeleteRequisitionRequestItem(delItemReqID, inBasicParam)
                    End If
                Next
            End If
            Return True
        End Function
        Public Shared Function DeleteRequisitionRequest_Txn(RR_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Remarks
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_REMARKS_INFO, "SR_Ref_ID = " + RR_ID.ToString() + " AND SR_Screen_Type = 'UO'", inBasicParam)
            'Item Requested
            dbService.DeleteByCondition(ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM, "RRI_RR_ID = " + RR_ID.ToString(), inBasicParam)
            'RR
            dbService.Delete(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, RR_ID.ToString(), inBasicParam)

            'Unlink those Documents which are attached somewhere else, and delete those which are not linked elsewhere
            Dim inDocs As New Projects.Param_GetStockDocuments()
            inDocs.RefID = RR_ID
            inDocs.Screen_Type = "Requisition"

            Dim _Docs_in_PO As DataTable = Projects.GetStockDocuments(inDocs, inBasicParam)
            For Each cRow As DataRow In _Docs_in_PO.Rows
                Dim inparam As New Attachments.Parameter_GetAttachmentLinkCount()
                inparam.RefRecordID = RR_ID
                inparam.RefScreen = "Requisition"
                inparam.AttachmentID = cRow("ID")
                Dim LinkedScreenName As String = Attachments.GetAttachmentLinkCount(inparam, inBasicParam)
                If String.IsNullOrEmpty(LinkedScreenName) Then
                    Attachments.Delete_Attachment_ByReference(RR_ID, "Requisition", inBasicParam)
                Else
                    Attachments.Unlink_Attachment_ByReference(RR_ID, "Requisition", inBasicParam)
                End If
            Next

            Return True
        End Function
        Private Shared Function InsertRequisitionRequest(RR_Date As DateTime, Project_ID As Int32?, Job_ID As Int32?, Requestor_ID As Int32, RR_Type As String, Purchased_by_ID As Int32?, Trf_From_Dept_ID As Int32?, Requesting_Dept_ID As Int32, Special_Discount As Decimal?, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_RR"
            Dim params() As String = {"@RR_Cen_ID", "@RR_Cod_Year_ID", "@RR_Date", "@RR_Project_ID", "@RR_Job_ID", "@RR_Requestor_ID", "@RR_Type", "@RR_Purchased_by_ID", "@RR_Trf_From_Dept_ID", "@RR_Dept_ID", "@UserID", "@SpecialDiscount"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, RR_Date, Project_ID, Job_ID, Requestor_ID, RR_Type, Purchased_by_ID, Trf_From_Dept_ID, Requesting_Dept_ID, inBasicParam.openUserID, Special_Discount}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal}
            Dim lengths() As Integer = {4, 4, 12, 4, 4, 4, 30, 4, 4, 4, 255, 5}

            Return CInt(dbService.ScalarFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam))
        End Function
        Private Shared Function InsertRequisitionRequestItem(RR_ID As Int32, inparam As Param_InsertRequisitionRequestItem, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Insert_RR_Item_Details"
            Dim params() As String = {"@RRI_RR_ID", "@RRI_supplier_ID", "@RRI_Make", "@RRI_Model", "@RRI_Qty_Requested", "@RRI_Qty_Approved", "@RRI_Unit_ID", "@RRI_Rate", "@RRI_Discount_Promised", "@RRI_Taxes", "@RRI_Amount", "@RRI_Priority", "@RRI_Reqd_Delivery_Date", "@RRI_Remarks", "@UserID", "@RRI_Sub_Item_ID", "@RR_Dest_Location_ID"}
            Dim values() As Object = {RR_ID, inparam.supplier_ID, inparam.Make, inparam.Model, inparam.Qty_Requested, inparam.Qty_Approved, inparam.Unit_ID, inparam.Rate, inparam.Discount_Promised, inparam.Taxes, inparam.Amount, inparam.Priority, inparam.Reqd_Delivery_Date, inparam.Remarks, inBasicParam.openUserID, inparam.Sub_Item_ID, inparam.Dest_Location_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255, 255, 19, 19, 36, 19, 5, 19, 19, 20, 12, 255, 255, 4, 36}

            Dim ReqItemID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Inserting Items Taxes in nested grid 
            If Not inparam._Item_Taxes Is Nothing Then
                For Each _In_Item_Taxes As Param_Insert_Tax_Details In inparam._Item_Taxes
                    If Not _In_Item_Taxes Is Nothing Then
                        _In_Item_Taxes.RequestedItem_ID = ReqItemID
                        Insert_RR_TaxDetails(RR_ID, _In_Item_Taxes, inBasicParam)
                    End If
                Next

            End If



            Return True
        End Function

        Private Shared Function Insert_RR_TaxDetails(RR_ID As Int32, inparam As Param_Insert_Tax_Details, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_RR_Taxes"
            Dim params() As String = {"@RRT_RR_ID", "@RRT_RRI_ID", "@RRT_Tax_Type_ID", "@RRT_Tax_percent", "@RRT_Tax_Remarks", "@UserID"}
            Dim values() As Object = {RR_ID, inparam.RequestedItem_ID, inparam.TaxTypeID, inparam.TaxPercent, inparam.TaxRemarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 36, 9, 255, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM_TAXES, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function InsertRequisitionRequestRemarks(Remarks As String, RR_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Remarks"
            Dim params() As String = {"@User_ID", "@RefID", "@SR_Remarks", "@Screen"}
            Dim values() As Object = {inBasicParam.openUserID, RR_ID, Remarks, "Requisition"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 8000, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateRequisitionRequest(RR_ID As Int32, RR_Date As DateTime, Project_ID As Int32?, Job_ID As Int32?, Requestor_ID As Int32, RR_Type As String, Purchased_by_ID As Int32?, Trf_From_Dept_ID As Int32?, Requesting_Dept_ID As Int32, Special_Discount As Decimal?, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_RR"
            Dim params() As String = {"@RR_ID", "@RR_Date", "@RR_Project_ID", "@RR_Job_ID", "@RR_Requestor_ID", "@RR_Type", "@RR_Purchased_by_ID", "@RR_Trf_From_Dept_ID", "@RR_Dept_ID", "@UserID", "@SpecialDiscount"}
            Dim values() As Object = {RR_ID, RR_Date, Project_ID, Job_ID, Requestor_ID, RR_Type, Purchased_by_ID, Trf_From_Dept_ID, Requesting_Dept_ID, inBasicParam.openUserID, Special_Discount}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal}
            Dim lengths() As Integer = {4, 12, 4, 4, 4, 30, 4, 4, 4, 255, 5}
            dbService.UpdateBySP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdateRequisitionRequestItem(RR_ID As Int32, inparam As Param_UpdateRequisitionRequestItem, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim SPName As String = "Update_RR_Item_Details"
            Dim params() As String = {"@RRI_ID", "@RRI_supplier_ID", "@RRI_Make", "@RRI_Model", "@RRI_Qty_Requested", "@RRI_Qty_Approved", "@RRI_Unit_ID", "@RRI_Rate", "@RRI_Discount_Promised", "@RRI_Taxes", "@RRI_Amount", "@RRI_Priority", "@RRI_Reqd_Delivery_Date", "@RRI_Remarks", "@UserID", "@RRI_Sub_Item_ID", "@RR_Dest_Location_ID"}
            Dim values() As Object = {inparam.RRI_ID, inparam.supplier_ID, inparam.Make, inparam.Model, inparam.Qty_Requested, inparam.Qty_Approved, inparam.Unit_ID, inparam.Rate, inparam.Discount_Promised, inparam.Taxes, inparam.Amount, inparam.Priority, inparam.Reqd_Delivery_Date, inparam.Remarks, inBasicParam.openUserID, inparam.Sub_Item_ID, inparam.Dest_Location_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255, 255, 19, 19, 36, 19, 5, 19, 19, 20, 12, 255, 255, 4, 36}

            dbService.UpdateBySP(ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM, SPName, params, values, dbTypes, lengths, inBasicParam)

            ' For update - Delete RR Taxes then Insert  RR Taxes in update mode
            dbService.DeleteByCondition(ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM_TAXES, "RRT_RRI_ID = " + inparam.RRI_ID.ToString(), inBasicParam)


            'Inserting Items Taxes in nested grid 
            If Not inparam._Added_Item_Taxes Is Nothing Then
                For Each _InItem_Taxes As Param_Insert_Tax_Details In inparam._Added_Item_Taxes
                    If Not _InItem_Taxes Is Nothing Then
                        _InItem_Taxes.RequestedItem_ID = inparam.RRI_ID
                        Insert_RR_TaxDetails(RR_ID, _InItem_Taxes, inBasicParam)
                    End If
                Next

            End If





            Return True
        End Function
        Public Shared Function UpdateRRStatus(inparam As Param_Update_RR_Status, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _Updated_Status As String = ""
            Select Case inparam.UpdatedStatus
                Case RR_Status._New
                    _Updated_Status = "New"
                Case RR_Status.Cancelled
                    _Updated_Status = "Cancelled"
                Case RR_Status.Rejected
                    _Updated_Status = "Rejected"
                Case RR_Status.Changes_Recommended
                    _Updated_Status = "Changes Recommended"
                Case RR_Status.Submitted_for_Approval
                    _Updated_Status = "Submitted for Approval"
                Case RR_Status.Re_Requisition_Requested
                    _Updated_Status = "Re-Requisition Requested"
                Case RR_Status.Approved
                    _Updated_Status = "Approved"
                Case RR_Status.Completed
                    _Updated_Status = "Completed"
            End Select
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_RR_Status"
            Dim params() As String = {"@RR_ID", "@UpdatedStatus", "@Logged_In_User"}
            Dim values() As Object = {inparam.RRID, _Updated_Status, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 100, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Delete_RR_Remarks(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_REMARKS_INFO, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteRequisitionRequestItem(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM_TAXES, "RRT_RRI_ID = " + ID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.REQUISITION_REQUEST_ITEM, ID, inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class StockPurchaseOrder
#Region "Param Classes"
        <Serializable>
        Public Enum PO_Status
            _New
            Cancelled
            Rejected
            Approved
            Completed
            Re_Requisition_Requested
            In_Progress
        End Enum
        <Serializable>
        Public Class Param_GetPurcahseOrderRegister
            Public From_Date As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetPOItemsOrdered
            Public PO_ID As Int32
            Public ItemRequested_not_FullyReceived_Only As Boolean = False
            Public Recd_ID As Int32? = Nothing

        End Class
        <Serializable>
        Public Class Param_GetPOGoodsReceived
            Public PO_ID As Int32
            Public ItemReceived_not_FullyReturned_Only As Boolean = False
            Public RetID As Int32? = Nothing

        End Class



        <Serializable>
        Public Class Param_Get_PriceHistory
            Public SupplierID As Int32?
            Public ItemCode As String
            Public ItemID As Int32

        End Class
        <Serializable>
        Public Class Param_Get_Lotno_Duplication
            Public StockID As Int32?
            Public Lot_Serial_No As String
            Public StockRefID As Int32?
            Public SubItemID As Int32?

        End Class
        <Serializable>
        Public Class Param_Get_PO_Tax_Detail
            Public PO_ID As Int32
            Public POItemID As Int32
            Public SubItemID As Int32

        End Class
        <Serializable>
        Public Class Param_InsertPurchaseOrderItem
            Public PO_ID As Int32
            Public RR_Item_Sr_No As Int32?
            Public Make As String
            Public Model As String
            Public Purchase_Qty As Decimal
            Public Unit_ID As String
            Public Rate As Decimal?
            Public Discount_Promised As Decimal?
            Public Taxes As Decimal?
            Public Amount As Decimal?
            Public Priority As String
            Public Reqd_Delivery_Date As DateTime? '0000109 bug Fixed
            Public Remarks As String
            Public Sub_Item_ID As Int32
            Public Dest_Location_ID As String
            Public Requested_Qty As Decimal
            Public Add_Update_Reason As String

            Public _Item_Taxes As List(Of PO_Param_Insert_Tax_Details)

        End Class

        <Serializable>
        Public Class PO_Param_Insert_Tax_Details
            Public RequestedItem_ID As Int32
            Public TaxTypeID As String
            Public TaxPercent As Decimal
            Public TaxRemarks As String
        End Class
        <Serializable>
        Public Class Param_InsertPurchaseOrderGoodsReceived
            Public PO_ID As Int32
            Public PO_Item_ID As Int32
            Public Recd_Qty As Decimal
            Public Recd_Date As DateTime
            Public Delivery_Mode As String
            Public FOB As Boolean?
            Public Delivery_Carrier As String
            Public Delivery_Location_ID As String
            Public Bill_No As String
            Public Challan_No As String
            Public Received_By_ID As Int32?
            Public Receiver_Remarks As String
            'Stock Specific Properties 
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public sub_Item_ID As Int32
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public Make As String
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public Model As String
            ''' <summary>
            ''' Shall be fetched from RR of Selected Ordered Item
            ''' </summary>
            Public Project_ID As Int32?
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public Lot_Serial_no As String
            ''' <summary>
            ''' Shall be fetched from Store mapped to destination location 
            ''' </summary>
            Public Store_Dept_ID As Int32
            ''' <summary>
            ''' Qty Recd. X Rate
            ''' </summary>
            Public Stock_Value As Decimal
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public Unit_ID As String
            ''' <summary>
            ''' As there is no field on screen for this, so will keep blank for now, but a field should be kept for same 
            ''' </summary>
            Public Warranty As String
        End Class
        <Serializable>
        Public Class Param_InsertPurchaseOrderGoodsReturned
            Public PO_ID As Int32
            Public Recd_Item_ID As Int32
            Public Returned_Qty As Decimal
            Public Returned_Date As DateTime
            Public Delivery_Mode As String
            Public Delivery_Carrier As String
            Public Challan_No As String
            Public Returned_By_ID As Int32?
            Public Returned_by_Remarks As String
        End Class
        <Serializable>
        Public Class Param_InsertPurchaseOrderPayment
            Public PO_id As Int32
            Public Exp_Tr_ID As String
            Public Exp_Tr_Sr_No As Int32
        End Class
        <Serializable>
        Public Class Param_UpdatePurchaseOrderDetails
            Public ID As Int32
            Public PO_Date As DateTime
            Public Supplier_ID As Int32?
            Public Remarks As String
            Public Special_Discount As Decimal?
        End Class
        <Serializable>
        Public Class Param_UpdatePurchaseOrderItem
            Public ID As Int32
            Public Make As String
            Public Model As String
            Public Purchase_Qty As Decimal
            Public Unit_ID As String
            Public Rate As Decimal?
            Public Discount_Promised As Decimal?
            Public Taxes As Decimal?
            Public Amount As Decimal?
            Public Priority As String
            Public Reqd_Delivery_Date As DateTime? '0000109 bug Fixed
            Public Remarks As String
            Public Sub_Item_ID As Int32
            Public Dest_Location_ID As String

            Public Add_Update_Reason As String

            Public _Added_Item_Taxes As List(Of PO_Param_Insert_Tax_Details)
            Public _Deleted_Item_Taxes As List(Of Int32)
        End Class
        <Serializable>
        Public Class Param_UpdatePurchaseOrderGoodsReceived
            Public ID As Int32
            Public PO_Item_ID As Int32
            Public Recd_Qty As Decimal
            Public Recd_Date As DateTime
            Public Delivery_Mode As String
            Public FOB As Boolean?
            Public Delivery_Carrier As String
            Public Delivery_Location_ID As String
            Public Bill_No As String
            Public Challan_No As String
            Public Received_By_ID As Int32?
            Public Receiver_Remarks As String
            'Stock Specific Properties 
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public sub_Item_ID As Int32
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public Make As String
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public Model As String
            ''' <summary>
            ''' Shall be fetched from RR of Selected Ordered Item
            ''' </summary>
            Public Project_ID As Int32?
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public Lot_Serial_no As String
            ''' <summary>
            ''' Shall be fetched from Store mapped to destination location 
            ''' </summary>
            Public Store_Dept_ID As Int32
            ''' <summary>
            ''' Qty Recd. X Rate
            ''' </summary>
            Public Stock_Value As Decimal
            ''' <summary>
            ''' Shall be fetched from Selected Ordered Item
            ''' </summary>
            Public Unit_ID As String
            ''' <summary>
            ''' As there is no field on screen for this, so will keep blank for now, but a field should be kept for same 
            ''' </summary>
            Public Warranty As String
            ''' <summary>
            ''' Shall be fetched from Received ITem Grid, for row which is being edited 
            ''' </summary>
            Public StockID As Int32
        End Class
        <Serializable>
        Public Class Param_UpdatePurchaseOrderStatus
            Public UpdatedStatus As PO_Status
            Public PO_ID As Int32
            Public Logged_In_User As String
        End Class
        <Serializable>
        Public Class Param_UpdatePurchaseOrderGoodsReturned
            Public ID As Int32
            Public Recd_Item_ID As Int32
            Public Returned_Qty As Decimal
            Public Returned_Date As DateTime
            Public Delivery_Mode As String
            Public Delivery_Carrier As String
            Public Challan_No As String
            Public Returned_By_ID As Int32?
            Public Returned_by_Remarks As String
        End Class
        <Serializable>
        Public Class Param_Update_PurchaseOrder_Txn
            Inherits Param_UpdatePurchaseOrderDetails
            ''' <summary>
            ''' Array of Attachments added during Addition of Requisition
            ''' </summary>
            Public Added_Attachments As Attachments.Parameter_Insert_Attachment()
            Public Added_Items_Ordered As List(Of Param_InsertPurchaseOrderItem)
            Public Added_Items_Received As List(Of Param_InsertPurchaseOrderGoodsReceived)
            Public Added_Items_Returned As List(Of Param_InsertPurchaseOrderGoodsReturned)
            Public Added_Payments_Mapped As List(Of Param_InsertPurchaseOrderPayment)

            ''' <summary>
            ''' Array of Documents updated during Update of Production
            ''' </summary>
            Public Updated_Attachments As Attachments.Parameter_Update_Attachment()
            Public Updated_Items_Ordered As List(Of Param_UpdatePurchaseOrderItem)
            Public Updated_Items_Received As List(Of Param_UpdatePurchaseOrderGoodsReceived)
            Public Updated_Items_Returned As List(Of Param_UpdatePurchaseOrderGoodsReturned)

            Public Deleted_Attachment_IDs As List(Of String)
            Public Unlinked_Attachment_IDs As List(Of String)
            Public Deleted_Items_Ordered_IDs As List(Of Int32)
            Public Deleted_Items_Received_IDs As List(Of Int32)
            Public Deleted_Items_Returned_IDs As List(Of Int32)
            Public Deleted_Payment_Mapped_IDs As List(Of Int32)
            Public Deleted_Remarks_IDs As List(Of Int32)
        End Class
#End Region
        Public Shared Function GetRegister(inParam As Param_GetPurcahseOrderRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_Register"
            Dim params() As String = {"FromDate", "ToDate", "Logged_User_ID", "CEN_ID"}
            Dim values() As Object = {inParam.From_Date, inParam.ToDate, inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 12, 255, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_PO_Detail(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_Details"
            Dim params() As String = {"@PO_ID"}
            Dim values() As Object = {PO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_INFO, SPName, ConnectOneWS.Tables.PURCHASE_ORDER_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPOItemsOrdered(inparam As Param_GetPOItemsOrdered, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_Items_Ordered"
            Dim params() As String = {"@PO_ID", "@ItemRequested_not_FullyReceived_Only", "@RecdID"}
            Dim values() As Object = {inparam.PO_ID, inparam.ItemRequested_not_FullyReceived_Only, inparam.Recd_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 2, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPOLinkedUserOrders(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_LinkedUO"
            Dim params() As String = {"@PO_ID"}
            Dim values() As Object = {PO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_Dept_for_POItem_DestLoc(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Dept_for_POItem_DestLoc"
            Dim params() As String = {"@PO_ID"}
            Dim values() As Object = {PO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.USER_ORDER, SPName, ConnectOneWS.Tables.USER_ORDER.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPOLinkedRequisitions(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_LinkedRR"
            Dim params() As String = {"@PO_ID"}
            Dim values() As Object = {PO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.REQUISITION_REQUEST_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPOPayments(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_mappedPayments"
            Dim params() As String = {"@PO_ID"}
            Dim values() As Object = {PO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPOPaymentsForMapping(SupplierID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_PaymentsForMapping"
            Dim params() As String = {"@SupplierID"}
            Dim values() As Object = {SupplierID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.TRANSACTION_INFO, SPName, ConnectOneWS.Tables.TRANSACTION_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPOGoodsReceived(inparam As Param_GetPOGoodsReceived, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_GoodsRecd"
            Dim params() As String = {"@PO_ID", "@ItemReceived_not_FullyReturned_Only", "@RetID"}
            Dim values() As Object = {inparam.PO_ID, inparam.ItemReceived_not_FullyReturned_Only, inparam.RetID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Boolean, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 2, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RECEIVED, SPName, ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RECEIVED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetPOGoodsReturned(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_GoodsReturn"
            Dim params() As String = {"@PO_ID"}
            Dim values() As Object = {PO_ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RETURNED, SPName, ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RETURNED.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_PO_Latest_RR_ID(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select T0.RRM_RR_ID From RR_PO_Mapping T0 Where T0.RRM_PO_ID = " + PO_ID.ToString + " AND T0.REC_ADD_ON = (Select MAX(REC_ADD_ON) from RR_PO_Mapping Where RRM_PO_ID = " + PO_ID.ToString + ")"
            Return dbService.GetScalar(ConnectOneWS.Tables.RR_PO_MAPPING, Query, ConnectOneWS.Tables.RR_PO_MAPPING.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_PO_Latest_UO_ID(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select T.RRM_UO_ID PO_UO from RR_UO_Mapping T Where T.RRM_RR_ID IN (Select T.RRM_RR_ID from RR_PO_Mapping T Where T.RRM_PO_ID =" + PO_ID.ToString + ")  AND T.REC_ADD_ON = (Select MAX(REC_ADD_ON) FROM RR_UO_Mapping Where RRM_RR_ID IN (Select T.RRM_RR_ID from RR_PO_Mapping T Where T.RRM_PO_ID = " + PO_ID.ToString + "))"
            Return dbService.GetScalar(ConnectOneWS.Tables.RR_PO_MAPPING, Query, ConnectOneWS.Tables.RR_PO_MAPPING.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_PO_Job_Project_Completed(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Case when A.Job_Status = 1 Then 1 when A.Proj_Status = 1 Then 1 Else 0 End Job_Proj_Status from(Select Case When T1.Job_Status = 'Completed' Then 1 Else 0 End as Job_Status,Case when T2.Proj_Status = 'Completed' Then 1 Else 0 End as Proj_Status From Requisition_Request_Info T Left Join Job_info T1 on T.RR_Job_ID = T1.REC_ID Left Join Project_Info T2 on T.RR_Project_ID = T2.REC_ID Where T.REC_ID = (Select A.RRM_RR_ID from RR_PO_Mapping A Where A.RRM_PO_ID = " + PO_ID.ToString + "))A"
            Return dbService.GetScalar(ConnectOneWS.Tables.JOB_INFO, Query, ConnectOneWS.Tables.JOB_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_PO_Non_Rate_Items(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE((Select 1 PO_DEL From Purchase_Order_Info T0 Inner Join Purchase_Order_Item T1 on T0.REC_ID = T1.POI_PO_ID Where T0.REC_ID = " + PO_ID.ToString + " AND (T1.POI_Rate IS NULL)) ,0) PO_DEL"
            Return dbService.GetScalar(ConnectOneWS.Tables.PURCHASE_ORDER_INFO, Query, ConnectOneWS.Tables.PURCHASE_ORDER_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_PO_Pending_Due(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Decimal
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COALESCE((Select SUM(T4.RRI_Amount) RRI_Amount From RR_PO_Mapping T0 Inner Join Requisition_Request_Info T1 on T0.RRM_RR_ID = T1.REC_ID Inner Join Requisition_Request_Item T4 on T1.REC_ID = T4.RRI_RR_ID Where T0.RRM_PO_ID = " + PO_ID.ToString + "),0) - COALESCE((Select COALESCE(SUM(T1.TR_REF_AMT),0) PaidAmount From Purchase_Order_Payments_Made T0 Inner Join transaction_d_payment_info T1 on T0.POP_Exp_Tr_ID = T1.REC_ID and T0.POP_Exp_Tr_Sr_No = T1.TR_SR_NO Where T0.POP_PO_id =" + PO_ID.ToString + "),0) AS Pending_PO_Payment"
            Return dbService.GetScalar(ConnectOneWS.Tables.RR_PO_MAPPING, Query, ConnectOneWS.Tables.PURCHASE_ORDER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_Stock_Current_Quantity_Count(StockID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Decimal
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select dbo.Get_Stock_Curr_Qty_fn (" + StockID.ToString + ")" '//Mantis bug 1267 fixed
            Return dbService.GetScalar(ConnectOneWS.Tables.Get_Stock_Curr_Qty_fn, Query, ConnectOneWS.Tables.Get_Stock_Curr_Qty_fn.ToString(), inBasicParam)
        End Function
        Public Shared Function Get_PO_Related_ClosedDept_Count(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Int32
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "SELECT SUM(SD_Close_Count) SD_Close_Count From" &
                                  "(Select COUNT(T0.RRM_RR_ID) SD_Close_Count From RR_PO_Mapping T0 Inner Join Requisition_Request_Info T1 on T0.RRM_RR_ID = T1.REC_ID Inner Join Store_Dept_Info T2 on T1.RR_Dept_Store_ID = T2.REC_ID Where T0.RRM_PO_ID = " + PO_ID.ToString + " AND T2.SD_Close_Date IS NOT NULL " &
                                  "UNION ALL Select COUNT(T0.RRM_RR_ID) SD_Close_Count From RR_PO_Mapping T0 Inner Join Requisition_Request_Info T1 On T0.RRM_RR_ID = T1.REC_ID Inner Join Stock_Personnel_Info T2 On T1.RR_Purchased_by_ID = T2.REC_ID Inner Join Store_Dept_Info T3 On T2.Pers_Dept_ID = T3.REC_ID Where T0.RRM_PO_ID =" + PO_ID.ToString + " And T3.SD_Close_Date Is Not NULL  " &
                                  "UNION ALL Select COUNT(T0.RRM_RR_ID) SD_Close_Count From RR_PO_Mapping T0 Inner Join RR_UO_Mapping T1 On T0.RRM_RR_ID = T1.RRM_RR_ID Inner Join User_Order T2 On T1.RRM_UO_ID = T2.REC_ID Inner Join Store_Dept_Info T3 On T2.UO_store_ID = T3.REC_ID Where T0.RRM_PO_ID = " + PO_ID.ToString + " And T3.SD_Close_Date Is Not NULL " &
                                  "UNION ALL Select COUNT(T0.RRM_RR_ID) SD_Close_Count From RR_PO_Mapping T0 Inner Join RR_UO_Mapping T1 on T0.RRM_RR_ID = T1.RRM_RR_ID Inner Join User_Order T2 on T1.RRM_UO_ID = T2.REC_ID Inner Join Store_Dept_Info T3 on T2.UO_Requestor_Main_Dept_Id = T3.REC_ID Where T0.RRM_PO_ID = " + PO_ID.ToString + " And T3.SD_Close_Date Is Not NULL " &
                                  "UNION ALL Select COUNT(T0.RRM_RR_ID) SD_Close_Count From RR_PO_Mapping T0 Inner Join RR_UO_Mapping T1 on T0.RRM_RR_ID = T1.RRM_RR_ID Inner Join User_Order T2 on T1.RRM_UO_ID = T2.REC_ID Inner Join Store_Dept_Info T3 on T2.UO_Requestor_Sub_Dept_Id = T3.REC_ID Where T0.RRM_PO_ID = " + PO_ID.ToString + " And T3.SD_Close_Date Is Not NULL " &
                                   ") A"
            Return dbService.GetScalar(ConnectOneWS.Tables.RR_PO_MAPPING, Query, ConnectOneWS.Tables.PURCHASE_ORDER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_PO_Duplicate_LotNo_Count(inparam As Param_Get_Lotno_Duplication, inBasicParam As ConnectOneWS.Basic_Param) As Int32

            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  Dim Query As String = "Select COALESCE((Select COUNT(DISTINCT T0.REC_ID) from Stock_Info T0 Left Join Stock_Info T1 On T0.Stock_Ref_ID = T1.Stock_Ref_ID And T0.Stock_sub_Item_ID = T1.Stock_sub_Item_ID Where T1.Stock_Ref_Entry_Source = 'PO - Item Received'  And T0.Stock_Lot_Serial_no = '" + inparam.Lot_Serial_No + "' And COALESCE(T0.REC_ID,0) <> COALESCE('" + inparam.StockID.ToString + "',0) And T1.REC_ID Is NULL),0) SerialNoLotNo_Exists"

            Dim Query As String = "Select COALESCE((Select COUNT(DISTINCT T0.REC_ID) from Stock_Info T0 Left Join Stock_Info T1 On T0.Stock_Ref_ID = T1.Stock_Ref_ID And COALESCE(T0.Stock_Ref_ID,0) = COALESCE('" + inparam.StockRefID.ToString + "',0) And T0.Stock_sub_Item_ID = T1.Stock_sub_Item_ID And COALESCE(T0.Stock_sub_Item_ID,0) = COALESCE('" + inparam.SubItemID.ToString + "',0) And T1.Stock_Ref_Entry_Source = 'PO - Item Received' Where T0.Stock_Lot_Serial_no = '" + inparam.Lot_Serial_No + "' And COALESCE(T0.REC_ID,0) <> COALESCE('" + inparam.StockID.ToString + "',0) And T1.REC_ID Is NULL),0) SerialNoLotNo_Exists"

            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)
        End Function
        Public Shared Function GetPOItem_Received_EntryCount(PO_ReqdItemEntry_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Count(1) from [dbo].[Purchase_Order_Item_Received] Where [POR_PO_Item_ID]  = " + PO_ReqdItemEntry_ID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RECEIVED, Query, ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RECEIVED.ToString(), inBasicParam)
        End Function
        Public Shared Function GetPOReceipt_Return_EntryCount(PO_Received_ID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select Count(1) from [dbo].[Purchase_Order_Item_Returned] Where [POR_Recd_Item_ID] = " + PO_Received_ID.ToString()
            Return dbService.GetScalar(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RETURNED, Query, ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RETURNED.ToString(), inBasicParam)
        End Function

        Public Shared Function GetItemsPriceHistory(SupplierID As Int32?, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_Items_PriceHistory"
            Dim params() As String = {"@CEN_ID", "@SupplierID"}
            Dim values() As Object = {inBasicParam.openCenID, SupplierID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.SUB_ITEM_INFO, SPName, ConnectOneWS.Tables.SUB_ITEM_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function

        Public Shared Function GetSupplier_PriceHistory(inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim Query As String = "Select A1.C_NAME SuppName, A.Supp_Company_Code CompanyCode, COALESCE(A1.C_MOB_NO_1, A1.C_MOB_NO_2) ContactNo, A.REC_ID ID From Stock_Supplier_Info A Inner Join address_book A1 on A.Supp_AB_ID = A1.REC_ID"
            Return dbService.List(ConnectOneWS.Tables.STOCK_SUPPLIER_INFO, Query, ConnectOneWS.Tables.STOCK_SUPPLIER_INFO.ToString(), inBasicParam)
        End Function

        Public Shared Function Get_PriceHistory(inParam As Param_Get_PriceHistory, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PriceHistory"
            Dim params() As String = {"@SupplierID", "@ItemCode", "@ItemID"}
            Dim values() As Object = {inParam.SupplierID, inParam.ItemCode, inParam.ItemID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 255, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_PRICE_HISTORY, SPName, ConnectOneWS.Tables.PURCHASE_ORDER_PRICE_HISTORY.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function Get_PO_Tax_Detail(inparam As Param_Get_PO_Tax_Detail, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_PO_Item_Taxes"
            Dim params() As String = {"@PO_ID", "@POI_ID", "@SubItemID"}
            Dim values() As Object = {inparam.PO_ID, inparam.POItemID, inparam.SubItemID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32}
            Dim lengths() As Integer = {4, 4, 4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_TAXES, SPName, ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_TAXES.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Private Shared Function InsertPurchaseOrderItem(inparam As Param_InsertPurchaseOrderItem, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_PO_Item"
            Dim params() As String = {"@PO_ID", "POI_RR_Item_Sr_No", "@POI_Make", "@POI_Model", "@POI_Purchase_Qty", "@POI_Unit_ID", "@POI_Rate", "@POI_Discount_Promised", "@POI_Taxes", "@POI_Amount", "@POI_Priority", "@POI_Reqd_Delivery_Date", "@POI_Remarks", "@UserID", "@POI_Sub_Item_ID", "@POI_Dest_Location_ID", "@POI_Requested_Qty", "@POI_Add_Update_Reason"}
            Dim values() As Object = {inparam.PO_ID, inparam.RR_Item_Sr_No, inparam.Make, inparam.Model, inparam.Purchase_Qty, inparam.Unit_ID, inparam.Rate, inparam.Discount_Promised, inparam.Taxes, inparam.Amount, inparam.Priority, inparam.Reqd_Delivery_Date, inparam.Remarks, inBasicParam.openUserID, inparam.Sub_Item_ID, inparam.Dest_Location_ID, inparam.Requested_Qty, inparam.Add_Update_Reason}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 255, 255, 19, 36, 19, 5, 19, 19, 20, 14, 255, 255, 4, 36, 19, 255}

            Dim POItemID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Inserting Items Taxes in nested grid 
            If Not inparam._Item_Taxes Is Nothing Then
                For Each _In_Item_Taxes As PO_Param_Insert_Tax_Details In inparam._Item_Taxes
                    If Not _In_Item_Taxes Is Nothing Then
                        _In_Item_Taxes.RequestedItem_ID = POItemID
                        Insert_PO_TaxDetails(inparam.PO_ID, _In_Item_Taxes, inBasicParam)
                    End If
                Next

            End If

            Return True
        End Function

        Private Shared Function Insert_PO_TaxDetails(PO_ID As Int32, inparam As PO_Param_Insert_Tax_Details, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_PO_Taxes"
            Dim params() As String = {"@PO_ID", "@POI_ID", "@Tax_Type_ID", "@Tax_percent", "@Tax_Remarks", "@UserID"}
            Dim values() As Object = {PO_ID, inparam.RequestedItem_ID, inparam.TaxTypeID, inparam.TaxPercent, inparam.TaxRemarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 36, 9, 255, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_TAXES, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertPurchaseOrderGoodsReceived(inparam As Param_InsertPurchaseOrderGoodsReceived, inBasicParam As ConnectOneWS.Basic_Param) As Boolean

            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_PO_GoodsRecd"
            Dim params() As String = {"@POR_PO_ID", "@POR_PO_Item_ID", "@POR_Recd_Qty", "@POR_Recd_Date", "@POR_Delivery_Mode", "@POR_FOB", "@POR_Delivery_Carrier", "@POR_Delivery_Location_ID", "@POR_Bill_No", "@POR_Challan_No", "@POR_Received_By_ID", "@POR_Receiver_Remarks", "@UserID"}
            Dim values() As Object = {inparam.PO_ID, inparam.PO_Item_ID, inparam.Recd_Qty, inparam.Recd_Date, inparam.Delivery_Mode, inparam.FOB, inparam.Delivery_Carrier, inparam.Delivery_Location_ID, inparam.Bill_No, inparam.Challan_No, inparam.Received_By_ID, inparam.Receiver_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 2, 100, 36, 50, 50, 4, 500, 255}
            Dim RecdID As Integer = dbService.ScalarFromSP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RECEIVED, SPName, params, values, dbTypes, lengths, inBasicParam)


            Dim _StockData As New StockProfile.Param_Add_Stock_Addition()
            _StockData.Date_Of_Purchase = inparam.Recd_Date
            _StockData.item_id = inparam.sub_Item_ID
            _StockData.Location_Id = inparam.Delivery_Location_ID
            _StockData.make = inparam.Make
            _StockData.model = inparam.Model
            _StockData.Project_ID = inparam.Project_ID
            _StockData.Quantity = inparam.Recd_Qty
            _StockData.Remarks = inparam.Receiver_Remarks
            _StockData.serial_no = inparam.Lot_Serial_no
            _StockData.Stock_Ref_Entry_Source = StockProfile.Stock_Addition_Source.Purchase_Order_Received
            _StockData.Stock_Ref_ID = inparam.PO_ID
            _StockData.Stock_Ref_Sub_ID = RecdID
            _StockData.Store_Dept_ID = inparam.Store_Dept_ID
            _StockData.total_value = inparam.Stock_Value
            _StockData.Unit_Id = inparam.Unit_ID
            _StockData.Warranty = inparam.Warranty

            Dim StockID As Int32 = StockProfile.AddStockAddition(_StockData, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertPurchaseOrderGoodsReturned(inparam As Param_InsertPurchaseOrderGoodsReturned, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_PO_GoodsReturn"
            Dim params() As String = {"@POR_PO_ID", "@POR_Recd_Item_ID", "@POR_Returned_Qty", "@POR_Returned_Date", "@POR_Delivery_Mode", "@POR_Delivery_Carrier", "@POR_Returned_By_ID", "@POR_Returned_by_Remarks", "@POR_Challan_No", "@UserID"}
            Dim values() As Object = {inparam.PO_ID, inparam.Recd_Item_ID, inparam.Returned_Qty, inparam.Returned_Date, inparam.Delivery_Mode, inparam.Delivery_Carrier, inparam.Returned_By_ID, inparam.Returned_by_Remarks, inparam.Challan_No, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 4, 500, 50, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RETURNED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertPurchaseOrderPayment(inparam As Param_InsertPurchaseOrderPayment, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_PO_Payment_Mapping"
            Dim params() As String = {"@POP_PO_id", "@POP_Exp_Tr_ID", "@POP_Exp_Tr_Sr_No", "@UserID"}
            Dim values() As Object = {inparam.PO_id, inparam.Exp_Tr_ID, inparam.Exp_Tr_Sr_No, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 36, 4, 255}
            dbService.InsertBySP(ConnectOneWS.Tables.PURCHASE_ORDER_PAYMENTS_MADE, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function InsertPurchaseOrderRemarks(Remarks As String, PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Remarks"
            Dim params() As String = {"@User_ID", "@RefID", "@SR_Remarks", "@Screen"}
            Dim values() As Object = {inBasicParam.openUserID, PO_ID, Remarks, "PO"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 8000, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdatePurchaseOrder_Txn(ByVal InParam As Param_Update_PurchaseOrder_Txn, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            UpdatePurchaseOrderDetails(InParam.ID, InParam.PO_Date, InParam.Supplier_ID, InParam.Special_Discount, inBasicParam)
            If Not InParam.Remarks = Nothing Then
                InsertPurchaseOrderRemarks(InParam.Remarks, InParam.ID, inBasicParam)
            End If
            'Add Attachment
            If Not InParam.Added_Attachments Is Nothing Then
                For Each InAttachment As Attachments.Parameter_Insert_Attachment In InParam.Added_Attachments
                    If Not InAttachment Is Nothing Then
                        InAttachment.Ref_Rec_ID = InParam.ID
                        InAttachment.Ref_Screen = "PO"
                        Attachments.Insert(InAttachment, inBasicParam)
                    End If
                Next
            End If
            For Each inItemOrdered As Param_InsertPurchaseOrderItem In InParam.Added_Items_Ordered
                If Not inItemOrdered Is Nothing Then InsertPurchaseOrderItem(inItemOrdered, inBasicParam)
            Next
            For Each inItemReceived As Param_InsertPurchaseOrderGoodsReceived In InParam.Added_Items_Received
                If Not inItemReceived Is Nothing Then InsertPurchaseOrderGoodsReceived(inItemReceived, inBasicParam)
            Next
            For Each inItemReturned As Param_InsertPurchaseOrderGoodsReturned In InParam.Added_Items_Returned
                If Not inItemReturned Is Nothing Then InsertPurchaseOrderGoodsReturned(inItemReturned, inBasicParam)
            Next
            For Each inPaymentMapped As Param_InsertPurchaseOrderPayment In InParam.Added_Payments_Mapped
                If Not inPaymentMapped Is Nothing Then InsertPurchaseOrderPayment(inPaymentMapped, inBasicParam)
            Next

            '''Updates
            'Update Attachment
            If Not InParam.Updated_Attachments Is Nothing Then
                For Each updAttachment As Attachments.Parameter_Update_Attachment In InParam.Updated_Attachments
                    If Not updAttachment Is Nothing Then
                        Attachments.Update(updAttachment, inBasicParam)
                    End If
                Next
            End If
            For Each inItemOrdered As Param_UpdatePurchaseOrderItem In InParam.Updated_Items_Ordered
                If Not inItemOrdered Is Nothing Then UpdatePurchaseOrderItem(InParam.ID, inItemOrdered, inBasicParam)
            Next
            For Each inItemReceived As Param_UpdatePurchaseOrderGoodsReceived In InParam.Updated_Items_Received
                If Not inItemReceived Is Nothing Then UpdatePurchaseOrderGoodsReceived(inItemReceived, inBasicParam)
            Next
            For Each inItemReturned As Param_UpdatePurchaseOrderGoodsReturned In InParam.Updated_Items_Returned
                If Not inItemReturned Is Nothing Then UpdatePurchaseOrderGoodsReturned(inItemReturned, inBasicParam)
            Next

            '''Deletes
            If Not InParam.Deleted_Remarks_IDs Is Nothing Then
                For Each delRemarksID As Int32 In InParam.Deleted_Remarks_IDs
                    If Not delRemarksID = Nothing Then
                        Delete_PO_Remarks(delRemarksID, inBasicParam)
                    End If
                Next
            End If
            'Delete Attachment
            If Not InParam.Deleted_Attachment_IDs Is Nothing Then
                For Each delAttachmentID As String In InParam.Deleted_Attachment_IDs
                    If Not delAttachmentID = Nothing Then
                        Attachments.Delete_Attachment_ByID(delAttachmentID, inBasicParam)
                    End If
                Next
            End If
            'Unlink Attachment 
            If Not InParam.Unlinked_Attachment_IDs Is Nothing Then
                For Each unlinkAttachmentID As String In InParam.Unlinked_Attachment_IDs
                    If Not unlinkAttachmentID = Nothing Then
                        Dim unlinkParam As New Attachments.Parameter_Attachment_Unlink
                        unlinkParam.AttachmentID = unlinkAttachmentID
                        unlinkParam.Ref_Rec_ID = InParam.ID
                        Attachments.Unlink_Attachment(unlinkParam, inBasicParam)
                    End If
                Next
            End If
            'Delete Ordered Items
            If Not InParam.Deleted_Items_Ordered_IDs Is Nothing Then
                For Each delItemOrdID As Int32 In InParam.Deleted_Items_Ordered_IDs
                    If Not delItemOrdID = Nothing Then
                        DeletePurchaseOrderItemOrdered(delItemOrdID, inBasicParam)
                    End If
                Next
            End If
            'Delete Received Items
            If Not InParam.Deleted_Items_Received_IDs Is Nothing Then
                For Each delItemRecdID As Int32 In InParam.Deleted_Items_Received_IDs
                    If Not delItemRecdID = Nothing Then

                        DeletePurchaseOrderItemReceived(delItemRecdID, inBasicParam)

                    End If
                Next

            End If
            'Delete Returned Items
            If Not InParam.Deleted_Items_Returned_IDs Is Nothing Then
                For Each delItemReturnedID As Int32 In InParam.Deleted_Items_Returned_IDs
                    If Not delItemReturnedID = Nothing Then
                        DeletePurchaseOrderItemReturned(delItemReturnedID, inBasicParam)
                    End If
                Next
            End If
            'Delete Payment Mapped
            If Not InParam.Deleted_Payment_Mapped_IDs Is Nothing Then
                For Each delPaymentMappedID As Int32 In InParam.Deleted_Payment_Mapped_IDs
                    If Not delPaymentMappedID = Nothing Then
                        DeletePurchaseOrderPayment(delPaymentMappedID, inBasicParam)
                    End If
                Next
            End If
            Return True
        End Function
        Public Shared Function DeletePurchaseOrder_Txn(PO_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Remarks
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_REMARKS_INFO, "SR_Ref_ID = " + PO_ID.ToString() + " And SR_Screen_Type = 'PO'", inBasicParam)
            'Item Ordered
            dbService.DeleteByCondition(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM, "POI_PO_ID = " + PO_ID.ToString(), inBasicParam)

            'stock for ITem Recieved
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_INFO, "Stock_Ref_ID=" + PO_ID.ToString() + " AND Stock_Ref_Entry_Source = 'PO - Item Received' ", inBasicParam)

            'Item Received
            dbService.DeleteByCondition(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RECEIVED, "POR_PO_ID = " + PO_ID.ToString(), inBasicParam)
            'Item Returned
            dbService.DeleteByCondition(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RETURNED, "POR_PO_ID = " + PO_ID.ToString(), inBasicParam)
            'Item Taxes
            dbService.DeleteByCondition(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_TAXES, "POT_PO_ID = " + PO_ID.ToString(), inBasicParam)
            'Payments Mapped
            dbService.DeleteByCondition(ConnectOneWS.Tables.PURCHASE_ORDER_PAYMENTS_MADE, "POP_PO_ID = " + PO_ID.ToString(), inBasicParam)
            'po
            dbService.Delete(ConnectOneWS.Tables.PURCHASE_ORDER_INFO, PO_ID.ToString(), inBasicParam)

            'Unlink those Documents which are attached somewhere else, and delete those which are not linked elsewhere
            Dim inDocs As New Projects.Param_GetStockDocuments()
            inDocs.RefID = PO_ID
            inDocs.Screen_Type = "PO"

            Dim _Docs_in_PO As DataTable = Projects.GetStockDocuments(inDocs, inBasicParam)
            For Each cRow As DataRow In _Docs_in_PO.Rows
                Dim inparam As New Attachments.Parameter_GetAttachmentLinkCount()
                inparam.RefRecordID = PO_ID
                inparam.RefScreen = "PO"
                inparam.AttachmentID = cRow("ID")
                Dim LinkedScreenName As String = Attachments.GetAttachmentLinkCount(inparam, inBasicParam)
                If String.IsNullOrEmpty(LinkedScreenName) Then
                    Attachments.Delete_Attachment_ByReference(PO_ID, "PO", inBasicParam)
                Else
                    Attachments.Unlink_Attachment_ByReference(PO_ID, "PO", inBasicParam)
                End If
            Next

            Return True
        End Function
        Private Shared Function UpdatePurchaseOrderDetails(ID As Int32, PO_Date As DateTime, SupplierID As Int32?, Special_Discount As Decimal?, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_PO_Details"
            Dim params() As String = {"@PO_ID", "@PO_Date", "@PO_Supplier_ID", "@UserID", "@SpecialDiscount"}
            Dim values() As Object = {ID, PO_Date, SupplierID, inBasicParam.openUserID, Special_Discount}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.String, Data.DbType.Decimal}
            Dim lengths() As Integer = {4, 7, 4, 255, 5}
            dbService.UpdateBySP(ConnectOneWS.Tables.PURCHASE_ORDER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdatePurchaseOrderItem(PO_ID As Int32, inparam As Param_UpdatePurchaseOrderItem, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_PO_Item"
            Dim params() As String = {"@POI_ID", "@POI_Make", "@POI_Model", "@POI_Purchase_Qty", "@POI_Unit_ID", "@POI_Rate", "@POI_Discount_Promised", "@POI_Taxes", "@POI_Amount", "@POI_Priority", "@POI_Reqd_Delivery_Date", "@POI_Remarks", "@UserID", "@POI_Sub_Item_ID", "@POI_Dest_Location_ID", "@POI_Add_Update_Reason"}
            Dim values() As Object = {inparam.ID, inparam.Make, inparam.Model, inparam.Purchase_Qty, inparam.Unit_ID, inparam.Rate, inparam.Discount_Promised, inparam.Taxes, inparam.Amount, inparam.Priority, inparam.Reqd_Delivery_Date, inparam.Remarks, inBasicParam.openUserID, inparam.Sub_Item_ID, inparam.Dest_Location_ID, inparam.Add_Update_Reason}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.Decimal, Data.DbType.String, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.Decimal, Data.DbType.String, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 255, 255, 19, 36, 19, 5, 19, 19, 20, 14, 255, 255, 4, 36, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM, SPName, params, values, dbTypes, lengths, inBasicParam)


            ' For update - Delete  Taxes then Insert   Taxes in update mode
            dbService.DeleteByCondition(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_TAXES, "POT_POI_ID = " + inparam.ID.ToString(), inBasicParam)


            'Inserting Items Taxes in nested grid 
            If Not inparam._Added_Item_Taxes Is Nothing Then
                For Each _InItem_Taxes As PO_Param_Insert_Tax_Details In inparam._Added_Item_Taxes
                    If Not _InItem_Taxes Is Nothing Then
                        _InItem_Taxes.RequestedItem_ID = inparam.ID
                        Insert_PO_TaxDetails(PO_ID, _InItem_Taxes, inBasicParam)
                    End If
                Next

            End If

            Return True


        End Function
        Private Shared Function UpdatePurchaseOrderGoodsReceived(inparam As Param_UpdatePurchaseOrderGoodsReceived, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _StockData As New StockProfile.Param_Update_StockProfile()
            _StockData.Date_Of_Purchase = inparam.Recd_Date
            _StockData.sub_Item_ID = inparam.sub_Item_ID
            _StockData.Location_Id = inparam.Delivery_Location_ID
            _StockData.make = inparam.Make
            _StockData.model = inparam.Model
            _StockData.Project_ID = inparam.Project_ID
            _StockData.Quantity = inparam.Recd_Qty
            _StockData.Remarks = inparam.Receiver_Remarks
            _StockData.serial_no = inparam.Lot_Serial_no
            _StockData.Store_Dept_ID = inparam.Store_Dept_ID
            _StockData.total_value = inparam.Stock_Value
            _StockData.Unit_Id = inparam.Unit_ID
            _StockData.Warranty = inparam.Warranty
            _StockData.Rec_ID = inparam.StockID
            StockProfile.UpdateStockProfile(_StockData, inBasicParam)

            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_PO_GoodsRecd"
            Dim params() As String = {"@POR_ID", "@POR_PO_Item_ID", "@POR_Recd_Qty", "@POR_Recd_Date", "@POR_Delivery_Mode", "@POR_FOB", "@POR_Delivery_Carrier", "@POR_Delivery_Location_ID", "@POR_Bill_No", "@POR_Challan_No", "@POR_Received_By_ID", "@POR_Receiver_Remarks", "@UserID"}
            Dim values() As Object = {inparam.ID, inparam.PO_Item_ID, inparam.Recd_Qty, inparam.Recd_Date, inparam.Delivery_Mode, inparam.FOB, inparam.Delivery_Carrier, inparam.Delivery_Location_ID, inparam.Bill_No, inparam.Challan_No, inparam.Received_By_ID, inparam.Receiver_Remarks, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.Boolean, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 2, 100, 36, 50, 50, 4, 500, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RECEIVED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdatePurchaseOrderStatus(inparam As Param_UpdatePurchaseOrderStatus, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim _Updated_Status As String
            Select Case inparam.UpdatedStatus
                Case PO_Status._New
                    _Updated_Status = "New"
                Case PO_Status.Cancelled
                    _Updated_Status = "Cancelled"
                Case PO_Status.Rejected
                    _Updated_Status = "Rejected"
                Case PO_Status.In_Progress
                    _Updated_Status = "In-Progress"
                Case PO_Status.Re_Requisition_Requested
                    _Updated_Status = "Re-Requisition Requested"
                Case PO_Status.Approved
                    _Updated_Status = "Approved"
                Case PO_Status.Completed
                    _Updated_Status = "Completed"
                Case Else
                    _Updated_Status = "New"
            End Select
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_PO_Status"
            Dim params() As String = {"@PO_ID", "@PO_Status", "@Logged_In_User"}

            Dim values() As Object = {inparam.PO_ID, _Updated_Status, inparam.Logged_In_User}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 100, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.PURCHASE_ORDER_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function UpdatePurchaseOrderGoodsReturned(inparam As Param_UpdatePurchaseOrderGoodsReturned, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_PO_GoodsReturn"
            Dim params() As String = {"@POR_ID", "@POR_Recd_Item_ID", "@POR_Returned_Qty", "@POR_Returned_Date", "@POR_Delivery_Mode", "@POR_Delivery_Carrier", "@POR_Returned_By_ID", "@POR_Returned_by_Remarks", "@POR_Challan_No", "@UserID"}
            Dim values() As Object = {inparam.ID, inparam.Recd_Item_ID, inparam.Returned_Qty, inparam.Returned_Date, inparam.Delivery_Mode, inparam.Delivery_Carrier, inparam.Returned_By_ID, inparam.Returned_by_Remarks, inparam.Challan_No, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal, Data.DbType.DateTime2, Data.DbType.String, Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 9, 12, 36, 100, 4, 500, 50, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RETURNED, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function Delete_PO_Remarks(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_REMARKS_INFO, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeletePurchaseOrderItemOrdered(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.DeleteByCondition(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_TAXES, "POT_POI_ID = " + ID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeletePurchaseOrderItemReceived(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()

            Dim Query As String = "Select REC_ID From Stock_Info Where Stock_Ref_Sub_ID=" + ID.ToString() + "And Stock_Ref_Entry_Source = 'PO - Item Received'"

            Dim StockID As Integer = dbService.GetScalar(ConnectOneWS.Tables.STOCK_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)

            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_INFO, "Rec_ID=" + StockID.ToString(), inBasicParam)
            'dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_INFO, "Stock_Ref_Sub_ID=" + ID.ToString(), inBasicParam)
            dbService.Delete(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RECEIVED, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeletePurchaseOrderItemReturned(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.PURCHASE_ORDER_ITEM_RETURNED, ID, inBasicParam)
            Return True
        End Function
        Private Shared Function DeletePurchaseOrderPayment(ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.PURCHASE_ORDER_PAYMENTS_MADE, ID, inBasicParam)
            Return True
        End Function
    End Class
    <Serializable>
    Public Class StockMachineToolAllocation
#Region "Param Classes"
        <Serializable>
        Public Class Param_GetMachineToolRegister
            Public From_Date As DateTime
            Public ToDate As DateTime
        End Class
        <Serializable>
        Public Class Param_GetMachineToolIssueCount
            ''' <summary>
            ''' Contains IssueItem RecID for which return is posted, so that the same issue is excluded from resultset
            ''' </summary>
            Public IssueItemRecID As Int32
            ''' <summary>
            ''' ID of Stock row for MAchine Tool Issued 
            ''' </summary>
            Public IssuedMachineToolID As Int32
        End Class
        <Serializable>
        Public Class Param_Insert_MachineTool_Issue
            Public Issue_Date As DateTime
            Public Issuing_Store_ID As Int32
            Public Issued_to_ID As Int32
            Public Issued_by_ID As Int32
            Public Job_ID As Integer?
            Public Usage_Site_ID As Int32
            Public Remarks As String
            Public _Items_Issued As List(Of Param_Insert_MachineTool_Item)
        End Class
        <Serializable>
        Public Class Param_Insert_MachineTool_Item
            ' Public Issue_ID As Int32
            Public Stock_ID As Int32
            Public Qty As Decimal
        End Class
        <Serializable>
        Public Class Param_Insert_MachineTool_Return
            Public Issue_Item_ID As Int32
            Public Return_Date As DateTime
            Public Qty_Returned As Decimal
            Public Remarks As String
        End Class
        <Serializable>
        Public Class Param_Update_MachineTool_Issue
            Public Issue_Date As DateTime
            Public Issuing_Store_ID As Int32
            Public Issued_to_ID As Int32
            Public Issued_by_ID As Int32
            Public Job_ID As Int32?
            Public Usage_Site_ID As Int32
            Public Remarks As String
            Public ID As Int32
            Public _Insert_Items_Issued As List(Of Param_Insert_MachineTool_Item)
            Public _updated_Items_Issued As List(Of Param_update_MachineTool_Item)
            Public _deleted_Items_Issued_IDs As List(Of Int32)
            Public _deleted_Remarks_IDs As List(Of Int32)
        End Class
        <Serializable>
        Public Class Param_update_MachineTool_Item
            Public ID As Int32
            Public Stock_ID As Int32
            Public Qty As Decimal
        End Class
        <Serializable>
        Public Class Param_Update_MachineTool_Return
            Public ID As Int32
            ' Public Issue_Item_ID As Int32
            Public Return_Date As DateTime
            Public Qty_Returned As Decimal
            Public Remarks As String
            Public _deleted_Remarks_IDs As List(Of Int32)
        End Class
        'Public Class Param_Delete_MachineTool_Item
        '    Public ID As Int32
        'End Class
        'Public Class Param_Delete_MachineTool_Remarks
        '    Public ID As Int32
        'End Class
#End Region
        'Public Shared Function GetStockIssueCount(StoreDeptID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As Object
        '    Dim dbService As ConnectOneWS = New ConnectOneWS()
        '    Dim Query As String = "Select COALESCE(SUM(Store_Count),0) Store_Count From (Select 1 Store_Count From Stock_Info A Where A.Stock_Store_ID = " + StoreDeptID.ToString() + " Union All Select 1 from Sub_item_Store_Mapping A Where A.Map_Store_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from User_Order A Where A.UO_store_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from Requisition_Request_Info A Where A.RR_Dept_Store_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from Stock_Personnel_Info A Where A.Pers_Dept_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from Store_Dept_Location_info A Where A.SL_SD_ID =  " + StoreDeptID.ToString() + " Union All Select 1 from Stock_Transfer_Order A Where (A.STO_Main_Dept_To_ID =  " + StoreDeptID.ToString() + " Or A.STO_Sub_Dept_To_ID = " + StoreDeptID.ToString() + ") ) A"
        '    Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_INFO, Query, ConnectOneWS.Tables.STOCK_INFO.ToString(), inBasicParam)
        'End Function
        Public Shared Function GetRegister(inParam As Param_GetMachineToolRegister, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Get_MachTool_Register"
            Dim params() As String = {"FromDate", "ToDate", "Logged_User_ID", "CENID"}
            Dim values() As Object = {inParam.From_Date, inParam.ToDate, inBasicParam.openUserID, inBasicParam.openCenID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime, Data.DbType.DateTime, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 12, 255, 4}
            Return dbService.ListDatasetFromSP(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetRecord(MachToolIssueID As Integer, inBasicParam As ConnectOneWS.Basic_Param) As DataSet
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select * from Stock_Tools_Issue_Info  WHERE REC_ID = " + MachToolIssueID.ToString() + ""
            Dim _IssueTable As DataTable = dbService.GetSingleRecord(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_INFO, Query, ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_INFO.ToString(), inBasicParam)

            Query = "Select ITEM.Sub_Item_Name , * from Stock_Tools_Issue_Items ISSUE_ITEM INNER JOIN Stock_Info As STOCK On ISSUE_ITEM.STI_Stock_ID = STOCK.REC_ID INNER JOIN Sub_item_info As ITEM On STOCK.Stock_sub_Item_ID = ITEM.REC_ID WHERE STI_STA_ID = " + MachToolIssueID.ToString() + ""
            Dim _ItemTable As DataTable = dbService.List(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_ITEMS, Query, ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_ITEMS.ToString(), inBasicParam)
            Dim _DS As New DataSet()
            _DS.Tables.Add(_IssueTable.Copy)
            _DS.Tables.Add(_ItemTable.Copy)
            Return _DS
        End Function
        Public Shared Function GetPendingReturns(StoreID As String, inBasicParam As ConnectOneWS.Basic_Param) As DataTable
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "get_store_Pending_Returns"
            Dim params() As String = {"Store_ID"}
            Dim values() As Object = {StoreID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32}
            Dim lengths() As Integer = {4}
            Return dbService.ListFromSP(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_INFO, SPName, ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_INFO.ToString(), params, values, dbTypes, lengths, inBasicParam)
        End Function
        Public Shared Function GetMachineToolIssueCount(inParam As Param_GetMachineToolIssueCount, inBasicParam As ConnectOneWS.Basic_Param) As Object
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim Query As String = "Select COUNT(REC_ID) CNT FROM Stock_Tools_Issue_Items WHERE STI_Stock_ID = " + inParam.IssuedMachineToolID.ToString() + "  And REC_ID <> " + inParam.IssueItemRecID.ToString() + ""
            Return dbService.GetScalar(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_ITEMS, Query, ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_ITEMS.ToString(), inBasicParam)
        End Function
        Private Shared Function InsertMachineToolRemarks(Remarks As String, MachineToolIssueID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Remarks"
            Dim params() As String = {"@User_ID", "@RefID", "@SR_Remarks", "@Screen"}
            Dim values() As Object = {inBasicParam.openUserID, MachineToolIssueID, Remarks, "MachineToolIssue"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 8000, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Private Shared Function InsertMachineToolRemarks_Return(Remarks As String, MachineToolIssueID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_Stock_Remarks"
            Dim params() As String = {"@User_ID", "@RefID", "@SR_Remarks", "@Screen"}
            Dim values() As Object = {inBasicParam.openUserID, MachineToolIssueID, Remarks, "MachineToolReturn"}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.String, Data.DbType.Int32, Data.DbType.String, Data.DbType.String}
            Dim lengths() As Integer = {255, 4, 8000, 50}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_REMARKS_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertMachineToolIssue(inparam As Param_Insert_MachineTool_Issue, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_MachineTool_Issue"
            Dim params() As String = {"@Cen_ID", "@Year_ID", "@Issue_Date", "@Issuing_Store_ID", "@Issued_to_ID", "@Issued_by_ID", "@Job_ID", "@Usage_Site_ID", "@User_ID"}
            Dim values() As Object = {inBasicParam.openCenID, inBasicParam.openYearID, inparam.Issue_Date, inparam.Issuing_Store_ID, inparam.Issued_to_ID, inparam.Issued_by_ID, inparam.Job_ID, inparam.Usage_Site_ID, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String}
            Dim lengths() As Integer = {4, 4, 12, 4, 4, 4, 4, 4, 255}
            Dim MachineToolIssueID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Inserting Items added in Issue 
            For Each _Item_Issued As Param_Insert_MachineTool_Item In inparam._Items_Issued
                InsertMachineToolItem(_Item_Issued, MachineToolIssueID, inBasicParam)
            Next
            'Add Remarks
            InsertMachineToolRemarks(inparam.Remarks, MachineToolIssueID, inBasicParam)

            Return True
        End Function
        Private Shared Function InsertMachineToolItem(inparam As Param_Insert_MachineTool_Item, Issue_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Insert_MachineTool_Issue_Items"
            Dim params() As String = {"@Issue_ID", "@Stock_ID", "@Qty"}
            Dim values() As Object = {Issue_ID, inparam.Stock_ID, inparam.Qty}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal}
            Dim lengths() As Integer = {4, 4, 11}
            dbService.InsertBySP(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_ITEMS, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function InsertMachineToolReturn(InReturn As Param_Insert_MachineTool_Return, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  For Each InReturn As Param_Insert_MachineTool_Return In inparam
            Dim SPName As String = "Insert_MachineTool_Issue_Return"
            Dim params() As String = {"@Issue_Item_ID", "@Return_Date", "@Qty_Returned", "@User_ID"}
            Dim values() As Object = {InReturn.Issue_Item_ID, InReturn.Return_Date, InReturn.Qty_Returned, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 12, 11, 255}
            Dim MachineToolReturnID As Int32 = dbService.ScalarFromSP(ConnectOneWS.Tables.STOCK_TOOLS_RETURN_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Add Remarks
            If InReturn.Remarks.Length > 0 Then
                InsertMachineToolRemarks_Return(InReturn.Remarks, MachineToolReturnID, inBasicParam)
            End If
            '  Next
            Return True
        End Function

        Public Shared Function UpdateMachineToolIssue(UpParam As Param_Update_MachineTool_Issue, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_MachineTool_Issue"
            Dim params() As String = {"@Issue_Date", "@Issuing_Store_ID", "@Issued_to_ID", "@Issued_by_ID", "@Job_ID", "@Usage_Site_ID", "@User_ID", "@ID"}
            Dim values() As Object = {UpParam.Issue_Date, UpParam.Issuing_Store_ID, UpParam.Issued_to_ID, UpParam.Issued_by_ID, UpParam.Job_ID, UpParam.Usage_Site_ID, inBasicParam.openUserID, UpParam.ID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.DateTime2, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Int32, Data.DbType.String, Data.DbType.Int32}
            Dim lengths() As Integer = {12, 4, 4, 4, 4, 4, 255, 4}
            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Inserting Items added in Issue 
            For Each _Item_Issued As Param_Insert_MachineTool_Item In UpParam._Insert_Items_Issued
                InsertMachineToolItem(_Item_Issued, UpParam.ID, inBasicParam)
            Next
            'Add Remarks
            InsertMachineToolRemarks(UpParam.Remarks, UpParam.ID, inBasicParam)

            'Updating Items updated in Issue 
            For Each _Item_Issued As Param_update_MachineTool_Item In UpParam._updated_Items_Issued
                UpdateMachineToolItem(_Item_Issued, inBasicParam)
            Next

            'Deleting Items removed in Issue 
            For Each _Item_Issued_ID As Int32 In UpParam._deleted_Items_Issued_IDs
                DeleteMachineToolItem(_Item_Issued_ID, inBasicParam)
            Next
            'Deleting Remarks
            For Each _Remarks_Deleted_ID As Int32 In UpParam._deleted_Remarks_IDs
                DeleteMachineToolRemarks(_Remarks_Deleted_ID, inBasicParam)
            Next
            Return True
        End Function
        Private Shared Function UpdateMachineToolItem(UpParam As Param_update_MachineTool_Item, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            Dim SPName As String = "Update_MachineTool_Issue_Items"
            Dim params() As String = {"@ID", "@Stock_ID", "@Qty"}
            Dim values() As Object = {UpParam.ID, UpParam.Stock_ID, UpParam.Qty}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.Int32, Data.DbType.Decimal}
            Dim lengths() As Integer = {4, 4, 11}
            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_ITEMS, SPName, params, values, dbTypes, lengths, inBasicParam)
            Return True
        End Function
        Public Shared Function UpdateMachineToolReturn(UpReturn As Param_Update_MachineTool_Return, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            '  For Each UpReturn As Param_Update_MachineTool_Return In UpParam
            Dim SPName As String = "Update_MachineTool_Issue_Return"
            Dim params() As String = {"@ID", "@Return_Date", "@Qty_Returned", "@User_ID"}
            Dim values() As Object = {UpReturn.ID, UpReturn.Return_Date, UpReturn.Qty_Returned, inBasicParam.openUserID}
            Dim dbTypes() As System.Data.DbType = {Data.DbType.Int32, Data.DbType.DateTime2, Data.DbType.Decimal, Data.DbType.String}
            Dim lengths() As Integer = {4, 12, 11, 255}
            dbService.UpdateBySP(ConnectOneWS.Tables.STOCK_TOOLS_RETURN_INFO, SPName, params, values, dbTypes, lengths, inBasicParam)

            'Add Remarks
            InsertMachineToolRemarks(UpReturn.Remarks, UpReturn.ID, inBasicParam)

            'Deleting Remarks
            For Each _Remarks_Deleted_ID As Int32 In UpReturn._deleted_Remarks_IDs
                DeleteMachineToolRemarks(_Remarks_Deleted_ID, inBasicParam)
            Next
            '  Next
            Return True
        End Function
        Public Shared Function DeleteMachineToolIssue(MachineToolIssueID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Delete Items Issued 
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_ITEMS, "STI_STA_ID= " + MachineToolIssueID.ToString(), inBasicParam)
            'Delete Remarks
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_REMARKS_INFO, "SR_SCREEN_TYPE='MachineToolIssue' AND SR_REF_ID = " + MachineToolIssueID.ToString(), inBasicParam)
            'Delete Issue Record 
            dbService.Delete(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_INFO, MachineToolIssueID.ToString(), inBasicParam)

            'Unlink those Documents which are attached somewhere else, and delete those which are not linked elsewhere
            Dim inDocs As New Projects.Param_GetStockDocuments()
            inDocs.RefID = MachineToolIssueID
            inDocs.Screen_Type = "MachineToolIssue"

            Dim _Docs_in_PO As DataTable = Projects.GetStockDocuments(inDocs, inBasicParam)
            For Each cRow As DataRow In _Docs_in_PO.Rows
                Dim inparam As New Attachments.Parameter_GetAttachmentLinkCount()
                inparam.RefRecordID = MachineToolIssueID
                inparam.RefScreen = "MachineToolIssue"
                inparam.AttachmentID = cRow("ID")
                Dim LinkedScreenName As String = Attachments.GetAttachmentLinkCount(inparam, inBasicParam)
                If String.IsNullOrEmpty(LinkedScreenName) Then
                    Attachments.Delete_Attachment_ByReference(MachineToolIssueID, "MachineToolIssue", inBasicParam)
                Else
                    Attachments.Unlink_Attachment_ByReference(MachineToolIssueID, "MachineToolIssue", inBasicParam)
                End If
            Next

            Return True
        End Function
        Private Shared Function DeleteMachineToolItem(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_TOOLS_ISSUE_ITEMS, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
        Public Shared Function DeleteMachineToolReturn(MachineToolReturnID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            'Delete Remarks
            dbService.DeleteByCondition(ConnectOneWS.Tables.STOCK_REMARKS_INFO, "SR_SCREEN_TYPE='MachineToolReturn' AND SR_REF_ID = " + MachineToolReturnID.ToString(), inBasicParam)
            'Delete Remarks
            dbService.Delete(ConnectOneWS.Tables.STOCK_TOOLS_RETURN_INFO, MachineToolReturnID.ToString(), inBasicParam)
            Return True
        End Function
        Private Shared Function DeleteMachineToolRemarks(Rec_ID As Int32, inBasicParam As ConnectOneWS.Basic_Param) As Boolean
            Dim dbService As ConnectOneWS = New ConnectOneWS()
            dbService.Delete(ConnectOneWS.Tables.STOCK_REMARKS_INFO, Rec_ID.ToString(), inBasicParam)
            Return True
        End Function
    End Class
End Namespace