using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common_Lib;
using static Common_Lib.DbOperations.StockUserOrder;
using static Common_Lib.DbOperations.StockProfile;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class UserOrder_Period
    {
        public string UO_BE_View_Period { get; set; }
        public DateTime UO_Fromdate { get; set; }
        public DateTime UO_Todate { get; set; }
        public string UO_PeriodSelection { get; set; }
        public DateTime UO_OpenDate { get; set; }
        public DateTime UO_CloseDate { get; set; }
    }
    [Serializable]
    public class NEVD_UserOrder
    {
        public string UO_ActionMethod { get; set; }


        public string UO_number { get; set; }
        [Display(Name = "UO Initiate Mode")]
        [Required(ErrorMessage = "UO Initiate Mode Can not be blank...!")]
        public string UO_Initiate_Mode { get; set; }

        [Display(Name = "Job Name")]
        [Required(ErrorMessage = "Job Name Can not be blank...!")]
        public int? UOJob_Name_Id { get; set; }
        public string UOJob_Name { get; set; }
        public string UOJob_No { get; set; }
        public string UOJob_Type { get; set; }
        public string UOProj_Name { get; set; }
        public string UOComplex { get; set; }
        public string UOSanction_No { get; set; }

        [Display(Name = "Request Store")]
        [Required(ErrorMessage = "Request Store Can not be blank...!")]
        public int? UORequest_Store_Id { get; set; }
        public string UORequest_Store { get; set; }

        [Display(Name = "Request Name")]
        [Required(ErrorMessage = "Request Name Can not be blank...!")]
        public int? UORequest_Name_Id { get; set; }
        public string UORequest_Name { get; set; }
        public string UOMobile_No { get; set; }

        [Display(Name = "Requestor Department Name")]
        [Required(ErrorMessage = "Requestor Department Name Can not be blank...!")]
        public int? UORequestor_Department_Name_Id { get; set; }
        //  public string UORequestor_Department_Name { get; set; }
        public int? UORequestor_Sub_Department_Name_Id { get; set; }
        //  public string UORequestor_Sub_Department_Name { get; set; }
        public string Uo_Status { get; set; }
        public string UODelivery_Status { get; set; }
        public string UOApproval_Required { get; set; }

        [Display(Name = "UO Date")]
        [Required(ErrorMessage = "UO Date Can not be blank...!")]
        public DateTime User_Order_Date { get; set; }

        public string UOProject_Name { get; set; }

        // public List<ProjectGridList> JobList { get; set; }
        //  public List<GetRegister_MainGrid> GetRegister_MainGrid { get; set; }
        // public List<GetRegister_NestedGrid> GetRegister_NestedGrid { get; set; }
        //public List<Order_Received> Received { get; set; }
        //  public List<Order_Returned> Returned { get; set; }
        public int UOID { get; set; }

        // public int UserID { get; set; }
        public string Uo_Remarks { get; set; }
        public string UO_StatusButtonClick { get; set; }
        public bool is_Requestor { get; set; }
        public bool is_RequestorIncharge { get; set; }

        public bool is_Store_Keeper { get; set; }
        public bool is_Store_KeeperIncharge { get; set; }
        public int? UO_Curr_User_Personnel_ID { get; set; }
        public string UO_openuserID { get; set; }

        public int? UO_Curr_User_Dept_ID { get; set; }
        public int? UO_Curr_User_Store_ID { get; set; }
        public string UO_PostSuccessFunction { get; set; }
        public string UO_PopUPId { get; set; }
        public string UO_CallingScreen { get; set; }

        public int? UOMainDeptIncharge_Name_Id { get; set; }
        public int? UOStoreIncharge_Name_Id { get; set; }
        public string UOUserRole { get; set; }
        public int? UORR_Mapped { get; set; }
        public int? uoprojectid { get; set; }
    }
    //public class GetRegister_MainGrid
    //{
    //    public string TempActionMethod { get; set; }
    //    public string UO_number { get; set; }
    //    public DateTime Edit_Date { get; set; }
    //    public string Edit_By { get; set; }
    //    public DateTime Add_Date { get; set; }
    //    public string Add_by { get; set; }
    //    public string CurrUserRole { get; set; }
    //    public string UO_Status { get; set; }
    //    public string Approval_Required { get; set; }
    //    public string Requestee_Store { get; set; }
    //    public string Requestor_Name { get; set; }
    //    public string Complex { get; set; }
    //    public string Job { get; set; }
    //    public string Project { get; set; }
    //    public string Delivery_Status { get; set; }
    //    public string Initiation_Mode { get; set; }
    //    public DateTime? UO_Date { get; set; }
    //    public int ID { get; set; }
    //}

    //public class GetRegister_NestedGrid
    //{
    //    public string Item_Name { get; set; }
    //    public string Make { get; set; }
    //    public string Model { get; set; }
    //    public string Serial_No_Lot_No { get; set; }
    //    public string Unit { get; set; }
    //    public decimal Requested_Qty { get; set; }
    //    public decimal Approved_Qty { get; set; }
    //    public decimal Delivered_Qty { get; set; }
    //    public decimal Returned_Qty { get; set; }
    //    public decimal Penalty_Charged { get; set; }
    //    public string Shipping_Location { get; set; }
    //    public int ID { get; set; }
    //    public int UOID { get; set; }
    //}
    [Serializable]
    public class UOOrderItemDetails
    {
        public int Sr { get; set; }

        [Display(Name = "Item Name")]
        [Required(ErrorMessage = "Item Name Can not be blank...!")]
        public int? OT_Item_ID { get; set; }
        public string OT_Item_Name { get; set; } //hidden
        public string OT_Head { get; set; }
        public string OT_Item_Type { get; set; }
        public string OT_Item_Code { get; set; }

        [RegularExpression("^[a-zA-Z0-9, - . _]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string OT_Make { get; set; }
        public string OT_Model { get; set; }

        [Display(Name = "Delivery Location")]
        [Required(ErrorMessage = "Delivery Location Can not be blank...!")]
        public string OT_DeliveryLocID { get; set; }
        public string OT_Delivery_Location { get; set; } //hidden

        public string OT_UnitName { get; set; } //hidden

        [Display(Name = "Unit")]
        [Required(ErrorMessage = "Unit Can not be blank...!")]
        public string OT_UnitID { get; set; }

        [Display(Name = "Requested Qty")]
        [Required(ErrorMessage = "Requested Qty Can not be blank...!")]
        public double OT_Requested_Qty { get; set; }
        public double OT_Approved_Qty { get; set; }
        //public double Scrap_Amount { get; set; }

        [Display(Name = "Required Delivery Date")]
        [Required(ErrorMessage = "Required Delivery Date Can not be blank...!")]
        public DateTime OT_Req_Deliver_Date { get; set; }
        public DateTime? OT_Sch_Deliver_Date { get; set; }
        public string OT_Priority { get; set; }
        public bool? OT_Delivery_Allowed { get; set; }
        public string OT_Remarks { get; set; }
        public int UO_OT_ID { get; set; }
        public string UO_OT_ActionMethod { get; set; }
        public DateTime UO_OT_AddedOn { get; set; }
        public string UO_OT_AddedBy { get; set; }
        public int UOid { get; set; }
 public int? UOItemSubDept { get; set; }

        public string UOItemPopupName { get; set; }

        public string UOItemPostSuccessFunction { get; set; }
        public string UO_Schedule_Item_ID { get; set; }
    }

    //public class UOGet_Stocks
    //{
    //    public String Item_Name { get; set; }
    //    public String Item_Code { get; set; }
    //    public String Head { get; set; }
    //    public String Make { get; set; }

    //    public String Model { get; set; }
    //    public String Lot_Serial_No { get; set; }
    //    public String Store { get; set; }
    //    public String Location { get; set; }

    //    public decimal Org_Qty { get; set; }
    //    public decimal Curr_Qty { get; set; }

    //    public decimal Org_Cost { get; set; }
    //    public decimal Curr_Cost { get; set; }

    //    public DateTime PurchaseDate { get; set; }

    //    public int ID { get; set; }

    //    public int ItemID { get; set; }

    //    public String Stock_Unit_ID { get; set; }
    //    public String Unit { get; set; }
    //}
    [Serializable]
    public class UserOrder_Delivery
    {
        //public List<Order_Received> Received { get; set; }
        //  public IEnumerable<dynamic> Dynamics { get; set; }
        //public IEnumerable <dynamic> Grid_GoodsDeliveryData { get; set; }

        public List<Return_Get_UO_Goods_Delivery_Stocks> Grid_GoodsDeliveryData { get; set; }
        public String UO_DL_ActionMethod { get; set; }
        //public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        //public string TempActionMethod { get; set; }

        [Display(Name = "Item Requested")]
        [Required(ErrorMessage = "Item Requested Can not be blank...!")]
        public int? UO_DL_Item_RequestedId { get; set; } //delivery entry id

        public int UO_DL_SubItemID { get; set; }

        public int UO_DL_Sr { get; set; }
        public string UO_DL_Item_Name { get; set; }
        public string UO_DL_Item_Code { get; set; }
        public string UO_DL_Item_Type { get; set; }
        public string UO_DL_Head { get; set; }
        public string UO_DL_Make { get; set; }
        public string UO_DL_ModelName { get; set; }
        public string UO_DL_partialdelivery { get; set; }
        public string UO_DL_Unit { get; set; }

        [Display(Name = "Delivered Qty")]
        [Required(ErrorMessage = "Delivered Qty Can not be blank...!")]
        public decimal UO_DL_Delivered_Qty { get; set; }  //imp

        public decimal UO_DL_Pending_Qty { get; set; }  //not in grid

        public decimal UO_DL_Already_Delivered_Qty { get; set; }  //not in grid

        public decimal UO_DL_Approved_Qty { get; set; }  //not in grid //hidden

        public string UO_DL_Priority { get; set; } //not in grid

        public DateTime? UO_DL_Required_Delivery_Date { get; set; } //disabled  not in grid

        public DateTime? UO_DL_Scheduled_Delivery_Date { get; set; } //disabled  not in grid

        [Display(Name = "Delivered Date")]
        [Required(ErrorMessage = "Delivered Date Can not be blank...!")]
        public DateTime UO_DL_Delivery_Date { get; set; } //imp

        public DateTime UO_DL_StockPurchaseDate { get; set; }
        public string UO_DL_Lot_No { get; set; }

        [Display(Name = "Delivery Mode")]
        [Required(ErrorMessage = "Delivery Mode Can not be blank...!")]
        public string UO_DL_Delivery_ModeId { get; set; } //check //imp
        public string UO_DL_Delivery_Mode { get; set; } //hidden 
        public string UO_DL_Carrier { get; set; } //imp

        [Display(Name = "Shipment To Location ")]
        [Required(ErrorMessage = "Shipment To Location Can not be blank...!")]
        public string UO_DL_Shipment_To_LocationId { get; set; }//imp
        public string UO_DL_Shipment_To_Location { get; set; }//hidden

        public string UO_DL_Shipment_From_Location { get; set; }//autofilled

        public string UO_DL_Delivered_By { get; set; }//hidden     
        public int? UO_DL_Delivered_By_Id { get; set; }

        public string UO_DL_MobileNo { get; set; }//disable

        public string UO_DL_Driver { get; set; }//hidden

        public int? UO_DL_DriverID { get; set; }

        public string UO_DL_Received_By { get; set; }
        public string UO_DL_Vehicle_no { get; set; }

        public string UO_DL_Remarks { get; set; }

        //public int UO_DL_StockRecordID { get; set; }                                 
        public int UO_DL_ID { get; set; }//hidden
        public string UO_DL_Added_By { get; set; } //hidden
        public DateTime UO_DL_Added_On { get; set; }//hidden

        public int UOid { get; set; }
        public string DL_PostSuccessFunction { get; set; }
        public string DL_PopUPId { get; set; }

    }
    [Serializable]
    public class UserOrder_Delivery_All
    {
        //public List<Order_Received> Received { get; set; }
        //  public IEnumerable<dynamic> Dynamics { get; set; }
        //public IEnumerable <dynamic> Grid_GoodsDeliveryData { get; set; }

        //   public List<Return_Get_UO_Goods_Delivery_Stocks> Grid_GoodsDeliveryData { get; set; }
        public String UO_DLA_ActionMethod { get; set; }
        //public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        //public string TempActionMethod { get; set; }


        public int UO_DLA_Sr { get; set; }

        [Display(Name = "Delivered Date and Time")]
        [Required(ErrorMessage = "Delivered Date and Time Can not be blank...!")]
        public DateTime UO_DLA_Delivery_Date { get; set; } //imp

        public string UO_DLA_Lot_No { get; set; }

        [Display(Name = "Delivery Mode")]
        [Required(ErrorMessage = "Delivery Mode Can not be blank...!")]
        public string UO_DLA_Delivery_ModeId { get; set; } //check //imp
        public string UO_DLA_Delivery_Mode { get; set; } //hidden 
        public string UO_DLA_Carrier { get; set; } //imp

        [Display(Name = "Shipment To Location ")]
        [Required(ErrorMessage = "Shipment To Location Can not be blank...!")]
        public string UO_DLA_Shipment_To_LocationId { get; set; }//imp
        public string UO_DLA_Shipment_To_Location { get; set; }//hidden

        public string UO_DLA_Shipment_From_Location { get; set; }//autofilled

        public string UO_DLA_Delivered_By { get; set; }//hidden     
        public int? UO_DLA_Delivered_By_Id { get; set; }

        public string UO_DLA_MobileNo { get; set; }//disable

        public string UO_DLA_Driver { get; set; }//hidden

        public int? UO_DLA_DriverID { get; set; }

        public string UO_DLA_Received_By { get; set; }
        public string UO_DLA_Vehicle_no { get; set; }

        public string UO_DLA_Remarks { get; set; }

        //public int UO_DL_StockRecordID { get; set; }                                 
        public int UO_DLA_ID { get; set; }//hidden
        public string UO_DLA_Added_By { get; set; } //hidden
        public DateTime UO_DLA_Added_On { get; set; }//hidden

        public int UOid { get; set; }
        public string UO_DeliverySelected_Item_ID { get; set; }
        
    }

    [Serializable]
    public class UserOrder_Received
    {
        public String UO_GR_ActionMethod { get; set; }
        //public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        //public string TempActionMethod { get; set; }

        [Display(Name = "Item Received")]
        [Required(ErrorMessage = "Item Received Can not be blank...!")]
        public int? UO_GR_Item_ReceivedId { get; set; } //delivery entry id //no
        public int? UO_GR_Sr { get; set; }
        public string UO_GR_Item_Name { get; set; } //hidden
        public string UO_GR_Item_Code { get; set; }
        public string UO_GR_Item_Type { get; set; }
        public string UO_GR_Head { get; set; }
        public string UO_GR_Make { get; set; }
        public string UO_GR_ModelName { get; set; }
        [Display(Name = "Received Qty")]
        [Required(ErrorMessage = "Received Qty Can not be blank...!")]
        public decimal UO_GR_Received_Qty { get; set; }  //imp

        public decimal UO_GR_Unreceived_Delivered_Qty { get; set; }
        public string UO_GR_Unit { get; set; }
        public decimal UO_GR_Qty_Delivered_by_Store { get; set; }//disabled //not in grid //no

        public string UO_GR_Lot_No { get; set; }
        [Display(Name = "Received Date")]
        [Required(ErrorMessage = "Received Date Can not be blank...!")]
        public DateTime UO_GR_Received_Date { get; set; } //imp
        public DateTime UO_GR_Delivery_Date { get; set; } //disabled //no // not in grid
        [Display(Name = "Received Mode")]
        [Required(ErrorMessage = "Received Mode Can not be blank...!")]
        public string UO_GR_Received_ModeId { get; set; } //check //imp
        public string UO_GR_Received_Mode { get; set; } //hidden 
        public string UO_GR_Carrier { get; set; } //imp
        public string UO_GR_BillNo { get; set; }//imp
        public string UO_GR_ChallanNo { get; set; }//imp
        [Display(Name = "Delivery Location")]
        [Required(ErrorMessage = "Delivery Location Can not be blank...!")]
        public string UO_GR_Delivery_LocationId { get; set; }//imp
        public string UO_GR_Delivery_Location { get; set; }//hidden
        public string UO_GR_Received_By { get; set; }//hidden     
        public int? UO_GR_Received_By_Id { get; set; }//imp
        public string UO_GR_MobileNo { get; set; }//disable
        public string UO_GR_Remarks { get; set; }//imp
        public int UO_GR_ID { get; set; }//hidden

        public int UO_GR_UDS_ID { get; set; }//hidden
        public int? UO_GR_DeliveryEntry_ID { get; set; }//hidden
        public int? UO_GR_ReturnedEntry_ID { get; set; }//hidden
        public string UO_GR_Added_By { get; set; } //hidden
        public DateTime UO_GR_Added_On { get; set; }//hidden
        public int UOid { get; set; }

        public int ItemGridID { get; set; }

        public string GR_PostSuccessFunction { get; set; }
        public string GR_PopUPId { get; set; }

    }
    [Serializable]
    public class UserOrder_Received_All
    {
        public String UO_GRA_ActionMethod { get; set; }
        //public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        //public string TempActionMethod { get; set; }


        public int? UO_GRA_Sr { get; set; }

        public DateTime UO_GRA_Received_Date { get; set; } //imp

        [Display(Name = "Received Mode")]
        [Required(ErrorMessage = "Received Mode Can not be blank...!")]
        public string UO_GRA_Received_ModeId { get; set; } //check //imp
        public string UO_GRA_Received_Mode { get; set; } //hidden 
        public string UO_GRA_Carrier { get; set; } //imp
        public string UO_GRA_BillNo { get; set; }//imp
        public string UO_GRA_ChallanNo { get; set; }//imp
        [Display(Name = "Delivery Location")]
        [Required(ErrorMessage = "Delivery Location Can not be blank...!")]
        public string UO_GRA_Delivery_LocationId { get; set; }//imp
        public string UO_GRA_Delivery_Location { get; set; }//hidden
        public string UO_GRA_Received_By { get; set; }//hidden     
        public int? UO_GRA_Received_By_Id { get; set; }//imp
        public string UO_GRA_MobileNo { get; set; }//disable
        public string UO_GRA_Remarks { get; set; }//imp
        public int UO_GRA_ID { get; set; }//hidden
        public string UO_GRA_Added_By { get; set; } //hidden
        public DateTime UO_GRA_Added_On { get; set; }//hidden
        public int UOid { get; set; }
        public string UO_Selected_Item_ID { get; set; }
    }
    [Serializable]
    public class UserOrder_Returned
    {
        public String UO_GRet_ActionMethod { get; set; }//hidden field

        [Display(Name = "Item Received")]
        [Required(ErrorMessage = "Item Received Can not be blank...!")]
        public int? UO_GRet_Item_ReturnedId { get; set; }
        public int UO_GRet_Sr { get; set; } //hidden field
        public string UO_GRet_RnItem_Name { get; set; }//hidden field
        public string UO_GRet_RnItem_Code { get; set; }
        public string UO_GRet_RnItem_Type { get; set; }
        public string UO_GRet_RnHead { get; set; }
        public string UO_GRet_RnMake { get; set; }
        public string UO_GRet_RnModelName { get; set; }

        [Display(Name = "Returned Qty")]
        [Required(ErrorMessage = "Returned Qty Can not be blank...!")]
        public decimal UO_GRet_Returned_Qty { get; set; }
        public string UO_GRet_RnUnit { get; set; }
        public decimal UO_GRet_Received_RnQty { get; set; } //not in grid
        public string UO_GRet_RnLot_No { get; set; }

        [Display(Name = "Returned Date")]
        [Required(ErrorMessage = "Returned Date Can not be blank...!")]
        public DateTime UO_GRet_Returned_Date { get; set; }
        public DateTime UO_GRet_Received_RnDate { get; set; } //not in grid

        [Display(Name = "Returned Mode")]
        [Required(ErrorMessage = "Returned Mode Can not be blank...!")]
        public string UO_GRet_Returned_ModeId { get; set; }
        public string UO_GRet_Returned_Mode { get; set; } //hidden field
        public string UO_GRet_RnCarrier { get; set; }

        [Display(Name = "Received Location")]
        [Required(ErrorMessage = "Received Location Can not be blank...!")]
        public string UO_GRet_Returned_LocationId { get; set; }
        public string UO_GRet_Returned_Location { get; set; } //hidden field
        public string UO_GRet_Returned_By { get; set; } //not in grid //hidden field
        public int? UO_GRet_Returned_By_Id { get; set; }
        public string UO_GRet_RnMobileNo { get; set; } //not in grid
        public string UO_GRet_RnRemarks { get; set; }
        public int UO_GRet_ID { get; set; } //hidden
        public string UO_GRet_Added_By { get; set; } //hidden
        public DateTime UO_GRet_Added_On { get; set; } //hidden
        public int UOid { get; set; }
        public decimal UO_GRet_pendingQty { get; set; }
        public string GRet_PostSuccessFunction { get; set; }
        public string GRet_PopUPId { get; set; }
    }

    [Serializable]
    public class UserOrder_Returned_All
    {
        public String UO_GRetA_ActionMethod { get; set; }//hidden field

        [Display(Name = "Item Received")]
        [Required(ErrorMessage = "Item Received Can not be blank...!")]

        public int UO_GRetA_Sr { get; set; } //hidden field

        public string UO_GRetA_RnLot_No { get; set; }

        [Display(Name = "Returned Date")]
        [Required(ErrorMessage = "Returned Date Can not be blank...!")]
        public DateTime UO_GRetA_Returned_Date { get; set; }

        [Display(Name = "Returned Mode")]
        [Required(ErrorMessage = "Returned Mode Can not be blank...!")]
        public string UO_GRetA_Returned_ModeId { get; set; }
        public string UO_GRetA_Returned_Mode { get; set; } //hidden field
        public string UO_GRetA_RnCarrier { get; set; }

        [Display(Name = "Received Location")]
        [Required(ErrorMessage = "Received Location Can not be blank...!")]
        public string UO_GRetA_Returned_LocationId { get; set; }
        public string UO_GRetA_Returned_Location { get; set; } //hidden field
        public string UO_GRetA_Returned_By { get; set; } //not in grid //hidden field
        public int? UO_GRetA_Returned_By_Id { get; set; }
        public string UO_GRetA_RnMobileNo { get; set; } //not in grid
        public string UO_GRetA_RnRemarks { get; set; }
        public int UO_GRetA_ID { get; set; } //hidden
        public string UO_GRetA_Added_By { get; set; } //hidden
        public DateTime UO_GRetA_Added_On { get; set; } //hidden
        public int UOid { get; set; }
    }
    [Serializable]
    public class UserOrder_Return_Received
    {
        public String UO_GRetRec_ActionMethod { get; set; } //hidden

        [Display(Name = "Item Return_Received")]
        [Required(ErrorMessage = "Item Return_Received Can not be blank...!")]
        public int? UO_GRetRec_Item_Return_ReceiveId { get; set; } //delivery entry id //no
        public int UO_GRetRec_Sr { get; set; }//hidden
        public string UO_GRetRec_Item_Name { get; set; } //hidden
        public string UO_GRetRec_Item_Code { get; set; }
        public string UO_GRetRec_Item_Type { get; set; }
        public string UO_GRetRec_Head { get; set; }
        public string UO_GRetRec_Make { get; set; }
        public string UO_GRetRec_ModelName { get; set; }
        [Display(Name = "Return Received Qty")]
        [Required(ErrorMessage = "Return Received Qty Can not be blank...!")]
        public decimal UO_GRetRec_Return_Received_Qty { get; set; }  //imp
        public string UO_GRetRec_Unit { get; set; }
        public decimal UO_GRetRec_ReturnedQty { get; set; }//disabled //not in grid //no
        public string UO_GRetRec_Lot_No { get; set; }
        [Display(Name = "Return Received Date")]
        [Required(ErrorMessage = "Received Date Can not be blank...!")]
        public DateTime UO_GRetRec_ReturnReceived_Date { get; set; } //imp
        public string UO_GRetRec_Return_Date { get; set; } //disabled //no // not in grid

        [Display(Name = "Received Mode")]
        [Required(ErrorMessage = "Received Mode Can not be blank...!")]
        public string UO_GRetRec_Received_ModeId { get; set; } //check //imp
        public string UO_GRetRec_Received_Mode { get; set; } //hidden 
        public string UO_GRetRec_Carrier { get; set; } //imp

        [Display(Name = "Received Location")]
        [Required(ErrorMessage = "Received Location Can not be blank...!")]
        public string UO_GRetRec_Received_LocationId { get; set; }//imp
        public string UO_GRetRec_Received_Location { get; set; }//hidden
        public string UO_GRetRec_Received_By { get; set; }//hidden     
        public int? UO_GRetRec_Received_By_Id { get; set; }//imp
        public string UO_GRetRec_MobileNo { get; set; }//disable
        public string UO_GRetRec_Remarks { get; set; }//imp
        public int UO_GRetRec_ID { get; set; }//hidden
        public int? UO_GRetRec_DeliveryEntry_ID { get; set; }//hidden
        public int? UO_GRetRec_ReturnedEntry_ID { get; set; }//hidden
        public string UO_GRetRec_Added_By { get; set; } //hidden
        public DateTime UO_GRetRec_Added_On { get; set; }//hidden
        public int UOid { get; set; }
        public decimal UO_GRetRec_pendingQty { get; set; }
        public string GRetRec_PostSuccessFunction { get; set; }
        public string GRetRec_PopUPId { get; set; }
        public int UO_GRetRec_UDS_ID { get; set; }//hidden
    }

    //public class Item_Received
    //{
    //    public string Item_Name { get; set; }
    //    public int ID { get; set; }
    //}
    //public class ItemNamesInfo
    //{
    //    public int ItemId { get; set; }
    //    public string ItemName { get; set; }
    //    public string ItemCategory { get; set; }
    //    public string ItemType { get; set; }
    //    public string ItemCode { get; set; }
    //    public string Unit { get; set; }
    //    public string UnitID { get; set; }
    //}
    //public class ScrapLocationInfo
    //{
    //    public string LocId { get; set; }
    //    public string LocationName { get; set; }
    //    public string MatchedType { get; set; }
    //    public string MatchedName { get; set; }
    //    public string MatchedInstt { get; set; }
    //}
    [Serializable]
    public class UOScrapDetail
    {
        [Required(ErrorMessage = "Stock Cannot Be Blank...!")]
        public int? UOScrap_ItemId { get; set; }
        public string UOScrapItemName { get; set; } //hidden
        public string UOScrap_Itemcode { get; set; }

        [Range(0.01, 99999999999999999.99, ErrorMessage = "Quantity Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Quantity Cannot Be Blank...!")]
        public double UOScrap_Qty { get; set; }
        public decimal UOScrap_Rate { get; set; } //hidden

        public string UOScrap_Location { get; set; } //hidden

        [Required(ErrorMessage = "Location Cannot Be Blank...!")]
        public string UOScrap_LocationID { get; set; }

        [Required(ErrorMessage = "Unit Cannot Be Blank...!")]
        public string UOScrap_Unit { get; set; }
        public string UOScrapUnitID { get; set; } //from item dropdown        //hidden

        [Range(0.01, 99999999999999999.99, ErrorMessage = "Amount Should Be Between 0.01 and 99999999999999999.99")]
        [Required(ErrorMessage = "Amount Cannot Be Blank...!")]
        public double UOScrap_Amount { get; set; }

        [RegularExpression("^[a-zA-Z0-9, -]+$", ErrorMessage = "Only Alphanumeric,- Are Allowed")]
        public string UOScrap_Remarks { get; set; }
        public string UOScrapActionMethod { get; set; } //hidden

        public int UOScrapID { get; set; }

        public int? UOScrap_Sr { get; set; } //hidden field     

        public DateTime uoscrapAddedOn { get; set; }
        public string uoscrapAddedBy { get; set; }

        public int UOid { get; set; }
        public string UO_number { get; set; }

        public int? UORequestee_Store_Id { get; set; }
        public string Scrap_PostSuccessFunction { get; set; }
        public string Scrap_PopUPId { get; set; }
    }
    //public class ScrapDetailGrid
    //{
    //    public int Sr_No { get; set; }
    //    public string Item_Name { get; set; }
    //    public decimal Qty { get; set; }
    //    public string Unit { get; set; }
    //    public decimal Rate { get; set; }
    //    public decimal Amount { get; set; }
    //    public int ID { get; set; }
    //    public string Remarks { get; set; }
    //    public int ItemID { get; set; }
    //    public string Location { get; set; }
    //    public string UnitID { get; set; }
    //}
    //public class GetJobname
    //{
    //    public string Job_Name { get; set; }
    //    public string Job_No { get; set; }
    //    public string Job_Type { get; set; }
    //    public string Proj_Name { get; set; }
    //    public string Complex { get; set; }
    //    public int ID { get; set; }
    //}

    //public class OrderItemGrid
    //{
    //    public int SrNo { get; set; }
    //    public string Item_Name { get; set; }
    //    public string Item_Code { get; set; }
    //    public string Item_Type { get; set; }
    //    public int Item_Id { get; set; }
    //    public string Head { get; set; }
    //    public string Make { get; set; }
    //    public string Model { get; set; }
    //    public decimal Requested_Qty { get; set; }
    //    public decimal? Approved_Qty { get; set; }
    //    public decimal? Already_Delivered_Qty { get; set; }
    //    public decimal? Pending_Qty { get; set; }
    //    public string Unit { get; set; }
    //    public string UnitID { get; set; }
    //    public string UOI_Priority { get; set; }
    //    public DateTime Requested_Delivery_Date { get; set; }
    //    public DateTime Scheduled_Delivery_Date { get; set; }
    //    public bool? Partial_Delivery_Allowed { get; set; }
    //    public string Delivery_Location { get; set; }
    //    public string Remarks { get; set; }
    //    public int ID { get; set; }
    //    //public string UnitID { get; set; }
    //    public string Delivery_Location_ID { get; set; }
    //    public DateTime Added_On { get; set; }
    //    public string Added_By { get; set; }
    //}
    //public class StoreItemsInfo
    //{
    //    public string Name { get; set; }
    //    public string Head { get; set; }
    //    public string Item_Type { get; set; }
    //    public string Item_Code { get; set; }
    //    public string Unit { get; set; }
    //    public int ItemID { get; set; }
    //    public int UnitID { get; set; }
    //}
    //public class UODocumentsInfo
    //{
    //    public int Sr_no { get; set; }
    //    public string DOC_NAME { get; set; }
    //    public string DOC_TYPE { get; set; }
    //    public string File_Name { get; set; }
    //    public string REMARKS { get; set; }
    //    public string ID { get; set; }
    //    public DateTime? REC_ADD_ON { get; set; }
    //    public string REC_ADD_BY { get; set; }
    //    public string Doc_Id { get; set; }
    //    public byte[] filefield { get; set; }
    //    public DateTime Applicable_From { get; set; }
    //    public DateTime Applicable_To { get; set; }
    //}

    //public class UOExistingRemarksInfo
    //{
    //    public long SR_NO { get; set; }
    //    public string REMARKS { get; set; }
    //    public string REMARKS_BY { get; set; }
    //    public string ADD_DESIGNATION { get; set; }
    //    public int ID { get; set; }
    //    public DateTime REC_ADD_ON { get; set; }
    //}
    [Serializable]
    public class UserUpDateStatus
    {
        public int ID { get; set; }
        public string UO_StatusType { get; set; }
        public string PopupHeader { get; set; }

        [Required(ErrorMessage = "Remarks Cannot be blank...!")]
        [Display(Name = "Remarks:")]
        public string UO_Status_Remark { get; set; }
    }
    [Serializable]
    public class UORRMap
    {
        public int RR_ID { get; set; }
        public int UO_ID { get; set; }

        public string UO_Map_Item_ID { get; set; }


    }
    [Serializable]
    public class UOCheckAvail
    {
        [Required(ErrorMessage = "Item Name Cannot be blank...!")]
        public int? UO_CA_ItemID { get; set; }
        public string UO_CA_Item_Category { get; set; }
        public int UO_CA_Center { get; set; }
        public int? UO_CA_Store_id { get; set; }
        public string UO_CA_LocID { get; set; }
        public int UOid { get; set; }
        public string screenname { get; set; }
    }
}