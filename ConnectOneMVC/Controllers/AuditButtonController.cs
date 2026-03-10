using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.Controllers
{
    public class AuditButtonController : BaseController
    {
        public ActionResult AuditButtons(string Grid, string KeyValue, int VisibleIndex)
        {
            ViewBag.Grid = Grid;
            ViewBag.KeyValue = KeyValue;
            ViewBag.VisibleIndex = VisibleIndex;
            ViewBag.UserType = BASE._open_User_Type.ToUpper();
            return View();
        }
        public ActionResult AuditIconAfterClick(string Grid, string KeyValue, int VisibleIndex)
        {
            ViewBag.Grid = Grid;
            ViewBag.KeyValue = KeyValue;
            ViewBag.VisibleIndex = VisibleIndex;
            return View();
        }
        public ActionResult ImageToolButtons(string ImgTag="",bool ViaPopup = false)
        {
            var model = (ViaPopup ? 1 : 0) + "|" + ImgTag;
            return View(model:model);
        }
        public ActionResult VouchingAuditIcons(Int32? VOUCHING_PENDING_COUNT, Int32? VOUCHING_ACCEPTED_COUNT, Int32? VOUCHING_ACCEPTED_WITH_REMARKS_COUNT, Int32? VOUCHING_REJECTED_COUNT, Int32? VOUCHING_TOTAL_COUNT, Int32? AUDIT_PENDING_COUNT, Int32? AUDIT_ACCEPTED_COUNT, Int32? AUDIT_ACCEPTED_WITH_REMARKS_COUNT, Int32? AUDIT_REJECTED_COUNT, Int32? AUDIT_TOTAL_COUNT,string iIcon="")
        {
            ViewBag.VOUCHING_PENDING_COUNT = VOUCHING_PENDING_COUNT;
            ViewBag.VOUCHING_ACCEPTED_COUNT = VOUCHING_ACCEPTED_COUNT;
            ViewBag.VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = VOUCHING_ACCEPTED_WITH_REMARKS_COUNT;
            ViewBag.VOUCHING_REJECTED_COUNT = VOUCHING_REJECTED_COUNT;
            ViewBag.VOUCHING_TOTAL_COUNT = VOUCHING_TOTAL_COUNT;

            ViewBag.AUDIT_PENDING_COUNT = AUDIT_PENDING_COUNT;
            ViewBag.AUDIT_ACCEPTED_COUNT = AUDIT_ACCEPTED_COUNT;
            ViewBag.AUDIT_ACCEPTED_WITH_REMARKS_COUNT = AUDIT_ACCEPTED_WITH_REMARKS_COUNT;
            ViewBag.AUDIT_REJECTED_COUNT = AUDIT_REJECTED_COUNT;
            ViewBag.AUDIT_TOTAL_COUNT = AUDIT_TOTAL_COUNT;
            ViewBag.iIcon = iIcon;
            return View();
        }
        //public ActionResult VouchingAuditIcons(string VouchingRemark,string VouchingStatus,string AuditStatus,string AuditRemarks)
        //{
        //    ViewBag.VouchingRemark = VouchingRemark;
        //    ViewBag.VouchingStatus = VouchingStatus;
        //    ViewBag.AuditStatus = AuditStatus;
        //    ViewBag.AuditRemarks = AuditRemarks;
        //    return View();
        //}
        public ActionResult AttachmentIcons(int? ReqAttachCount, int? CompleteAttachCount, int? RespondedCount, int? RejectedCount, int? OtherCount, int? AllCount)
        {
            ViewBag.ReqAttachCount = ReqAttachCount;
            ViewBag.CompleteAttachCount = CompleteAttachCount;
            ViewBag.RespondedCount = RespondedCount;
            ViewBag.RejectedCount = RejectedCount;
            ViewBag.OtherCount = OtherCount;
            ViewBag.AllCount = AllCount;
            return View();
        }
        public ActionResult AttachmentIcons_demo(string ID)
        {
            ViewBag.ID = ID;
            //ViewBag.CompleteAttachCount = CompleteAttachCount;
            //ViewBag.RespondedCount = RespondedCount;
            //ViewBag.RejectedCount = RejectedCount;
            //ViewBag.OtherCount = OtherCount;
            //ViewBag.AllCount = AllCount;
            return View();
        }
        public ActionResult VouchingAuditIcons_Attachment(bool? Vouching_During_Audit,string VOUCHING_REMARKS, string VOUCHING_STATUS, string VOUCHING_DETAILS, string VOUCHING_AUDITOR)
        {
            ViewBag.Vouching_During_Audit = Vouching_During_Audit;
            ViewBag.VOUCHING_REMARKS = VOUCHING_REMARKS;
            ViewBag.VOUCHING_STATUS = VOUCHING_STATUS;
            ViewBag.VOUCHING_DETAILS = VOUCHING_DETAILS;
            ViewBag.VOUCHING_AUDITOR = VOUCHING_AUDITOR;
            return View();
        }
        public ActionResult AttachmentIcons_Attachment(string Doc_Status, string Doc_Checking_Status, string ItemName, string ATTACH_FILE_NAME, string Rejection_Reason)
        {
            ViewBag.Doc_Status = Doc_Status;
            ViewBag.Doc_Checking_Status = Doc_Checking_Status;
            ViewBag.ItemName = ItemName;
            ViewBag.ATTACH_FILE_NAME = ATTACH_FILE_NAME;
            ViewBag.Rejection_Reason = Rejection_Reason;    
            return View();
        }
    }
}