using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Magazine.Models
{
    [Serializable]
    public class MagazineDispatchRegisterInfo
    {
        public string xID { get; set; }
        public string BE_Issue { get; set; }
        public string BE_Magazine { get; set; }
        public string BE_Member { get; set; }
        public int? Txt_Subscriped_count { get; set; }
        public int? Txt_Dispatched_Count { get; set; }
        public int? Txt_Returned_Count { get; set; }
        public int? Txt_Net_Dispatched { get; set; }
        public string Tr_M_ID { get; set; }
        public string Issue_ID { get; set; }
        public string MEM_ID { get; set; }
        public string ActionMethodName { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string Actiontype { get; set; }

    }
    [Serializable]
    public class MagazineList_LookUp
    {
        public string Name { get; set; }
        public string Short_Name { get; set; }
        public string Language { get; set; }
        public string Publish_On { get; set; }
        public string Magazine_Regd_No { get; set; }
        public string Postal_Regd_No { get; set; }
        public int Membership_Start_No { get; set; }
        public string Foreign_Subscriptions { get; set; }
        public string ID { get; set; }
        public string Add_By { get; set; }
        public DateTime? Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime? Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime? Action_Date { get; set; }
    }
    [Serializable]
    public class MagazineDispatch_Info
    {
        public string ID { get; set; }
        public string PSO { get; set; }
        public string CONTACT_NO { get; set; }
        public string CURRTIME { get; set; }
        public string RMS { get; set; }
        public string REGION { get; set; }
        public string Expiry_Status { get; set; }
        public string MAG_REC_ID { get; set; }
        public string DISP_DONE_MODE { get; set; }
        public string MII_ISSUE_DATE { get; set; }
        public string BUNDLE_WEIGHT { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string MEM_CATEGORY { get; set; }
        public string MII_COPY_WT { get; set; }
        public string MII_RPC_SEED { get; set; }
        public string Member_Status { get; set; }
        public string DispatchedCopies { get; set; }
        public string Dispatch_Member_ID { get; set; }
        public string Member_ID { get; set; }
        public string Member_Old_ID { get; set; }
        public string Member { get; set; }
        public string Magazine { get; set; }
        public string Issue { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public string MD_ID { get; set; }
        public string ISSUE_ID { get; set; }
        public string MEM_ID { get; set; }
        public string TR_ID { get; set; }
        public string NAME { get; set; }
        public string ADD1 { get; set; }
        public string ADD2 { get; set; }
        public string ADD3 { get; set; }
        public string ADD4 { get; set; }
        public string ADD5 { get; set; }
        public string Mem_Issue_Info { get; set; }
        public string Copies { get; set; }
        public string MODE { get; set; }
        public string MODE_ID { get; set; }
        public string REG_MODE { get; set; }
        public string MII_REG_SIZE { get; set; }
        public string MII_MAX_BUNDLE_COPY { get; set; }
        public string MEM_STATUS { get; set; }
        public string MEM_CLOSE_DATE { get; set; }
        public string DISP_ADDED_ON { get; set; }
        public string TotalCopies { get; set; }
        public string ISSUE_MEMBER { get; set; }
        public string Date { get; set; }
        public string Dispatch_Mode { get; set; }
        public string Dispatch_Status { get; set; }
        public string Remarks { get; set; }
        public int Count { get; set; }
        public int Weight { get; set; }
        public int Total { get; set; }
    }
    [Serializable]
    public class Magazine_Dispatch_Info_Details
    {
        public int? Sr { get; set; }
        public bool UPDATED { get; set; }
        public DateTime? Txt_Date { get; set; }
        public string GLookUp_DTypeList { get; set; }
        public string Cmb_Status { get; set; }
        public int Txt_Dispatched_count { get; set; }
        public string Me_Remarks { get; set; }
        public string Disp_Count { get; set; }
        public Common_Lib.Common.Navigation_Mode ActionMethod { get; set; }
        public Common_Lib.Common.Navigation_Mode Tag { get; set; }
        public string Mode_Name { get; set; }
        public string Mode_ID { get; set; }
        public string Tr_M_ID { get; set; }
        public string Issue_Member { get; set; }
    }
    [Serializable]
    public class LookUp_GetDispatch_info
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Default { get; set; }
        public string Action_Status { get; set; }
    }
    [Serializable]
    public class Magazine_Dispatch_Detail_Grid
    {
        public Int64 Sr { get; set; }
        public DateTime? Date { get; set; }
        public int Copies { get; set; }
        public int Disp_Count { get; set; }
        public string Mode { get; set; }
        public string Mode_ID { get; set; }
        public string Status { get; set; }
        public string Tr_M_ID { get; set; }
        public string Remarks { get; set; }
        public int UPDATED { get; set; }
    }
    [Serializable]
    public class Deleted_Dispatch_Vouchers
    {
        public string Tr_M_ID { get; set; }
    }
}