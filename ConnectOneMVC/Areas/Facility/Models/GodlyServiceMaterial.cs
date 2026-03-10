using Common_Lib;
using ConnectOneMVC.Areas.Help.Models;
using ConnectOneMVC.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Facility.Models
{
    [Serializable]
    public class GodlyServiceMaterial
    {
        public string ActionMethod { get; set; }
                
        [Display(Name = "Project:")]
        public string Look_ProjList_GSM { get; set; }

        public List<ProjectNameList_GSM> ProjectNameList_Data { get; set; }

        [Required(ErrorMessage = "Title cannot be Blank...!")]
        //[Display(Name = "Title:")]
        public string Txt_Title_GSM { get; set; }

        [Required(ErrorMessage = "Material Type Not Selected...!")]
        public string Look_MaterialType_GSM { get; set; }
        public string Look_MaterialSubCategory_GSM { get; set; }

        public DateTime? PublishDate_GSM { get; set; }

        public DateTime? Info_LastEditedOn { get; set; }
        public DateTime? LastEditedOn { get; set; }
        public string TitleX_GSM { get; set; }
        public string xID_GSM { get; set; }
        public string PopupName_GSM { get; set; }

        public Common.Navigation_Mode Tag { get; set; }

        public string Txt_OnlineLink_GSM { get; set; }

        public List<WingsList> Wings_List_GSM { get; set; }
        public List<WingsList> WingListData_GSM { get; set; }
        public string PreSelectedWing { get; set; }
        public string Txt_ProgSpeaker_GSM { get; set; }     //  Speaker/Author/Publisher
        public string Txt_ProgBrief_GSM { get; set; }       //  Brief Summary        
        public string Txt_PreviewImagePath_GSM { get; set; }
        public List<stringData> MaterialCategoryList { get; set; }
        public List<stringData> MaterialSubCategoryList { get; set; }
        //[Required(ErrorMessage = "Master Project Not Selected...!")]
        //public string Look_MasterProject_GSR { get; set; }
        //public int Txt_OnlineProgBeneficiaries_GSR { get; set; }
        //public int Txt_TotalProgBeneficiaries_GSR { get; set; }
        //public List<ProjectNameList> MasterProjectNameList { get; set; }
        public List<SR_AttachmentDetails> AttachmentDetails { get; set; }
        public List<SR_AttachmentDetails> PreviewAttachmentDetails { get; set; }
        public string AttachmentFileNames { get; set; }
        public string PreviewAttachmentFileNames { get; set; }
        public List<Model_Attachment_Window> SaveAttachments { get; set; }
        public bool Switch_PublicPrivate_GSM { get; set; }
    }

    [Serializable]
    public class ProjectNameList_GSM
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    [Serializable]
    public class ServiceMaterial
    {
        public string TITLE { get; set; }
        public string MATERIAL_TYPE { get; set; }
        public string Project_ID { get; set; }
        public string PUBLISH_DATE { get; set; }
        public Int32? CEN_ID { get; set; }
        public string ONLINE_LINK { get; set; }
        public string BRIEF_SUMMARY { get; set; }
        public string SPEAKER_AUTHOR_PUBLISHER { get; set; }
        public string ID { get; set; }
        public string PreviewImagePath { get; set; }
        public string MaterialSubCategory { get; set; }
        public string Project_Occassion { get; set; }
        
        public string Add_By { get; set; }
        public DateTime Add_Date { get; set; }
        public string Edit_By { get; set; }
        public DateTime Edit_Date { get; set; }
        public string Action_Status { get; set; }
        public string Action_By { get; set; }
        public DateTime Action_Date { get; set; }

        public Int32? REQ_ATTACH_COUNT { get; set; }
        public Int32? COMPLETE_ATTACH_COUNT { get; set; }
        public Int32? RESPONDED_COUNT { get; set; }
        public Int32? REJECTED_COUNT { get; set; }
        public Int32? OTHER_ATTACH_CNT { get; set; }
        public Int32? ALL_ATTACH_CNT { get; set; }

        //Added for Audit Icon Filter
        public Int32? VOUCHING_ACCEPTED_COUNT { get; set; }
        public Int32? VOUCHING_PENDING_COUNT { get; set; }
        public Int32? VOUCHING_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? VOUCHING_REJECTED_COUNT { get; set; }
        public Int32? VOUCHING_TOTAL_COUNT { get; set; }
        public Int32? AUDIT_PENDING_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_COUNT { get; set; }
        public Int32? AUDIT_ACCEPTED_WITH_REMARKS_COUNT { get; set; }
        public Int32? AUDIT_REJECTED_COUNT { get; set; }
        public Int32? AUDIT_TOTAL_COUNT { get; set; }
        public Int32? IS_AUTOVOUCHING { get; set; }
        public Int32? IS_CORRECTED_ENTRY { get; set; }
        public string iIcon { get; set; }
    }    
    //[Serializable]
    //public class WingsList
    //{
    //    public string ID { get; set; }
    //    public string Name { get; set; }
    //    public bool? Selected { get; set; }
    //}
    
}