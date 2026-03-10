using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Help.Models
{
    [Serializable]
    public class Model_Attachment_Window
    {   
        public byte[] Help_filefield { get; set; }
        public List<byte[]> Help_FileList { get; set; }
        [Required(ErrorMessage = "Document Cannot Be Blank...!")]
        public string Help_Document_FileName { get; set; }
        [Required(ErrorMessage = "Document Category Cannot Be Blank...!")]
        public string Help_Document_CategoryID { get; set; }
        [Required(ErrorMessage = "Document Name Cannot Be Blank...!")]
        public string Help_Document_NameID { get; set; }
        public string Help_Document_Name { get; set; }
        public DateTime? Help_Doc_From_Date { get; set; }
        public DateTime? Help_Doc_To_Date { get; set; }
        public string Help_Document_Description { get; set; }
        public bool Help_Checked { get; set; }
        public string ActionMethod { get; set; }        
        public string ID { get; set; }
        public int Sr_no { get; set;}
        public string Help_Document_PopupName { get; set; }
        public string Help_Post_Action_Name { get; set; }
        public string Help_Byte_SessionName { get; set; }
        public string Help_Post_Controller_Name { get; set; }
        public string Help_Post_Area_Name { get; set; } 
        public string Help_PostSucessFunctionName { get; set; }
        public string Help_REF_REC_ID { get; set; }
        public string Help_REF_SCREEN { get; set; }
        public bool Help_ParamMandatory { get; set; }
        public string Help_FromDate_Label { get; set; }
        public string Help_ToDate_Label { get; set; }
        public string Help_Description_Label { get; set; }
        public bool Help_CallFromHelpAttachment { get; set; }
        public string Help_uploadControlName { get; set; }
        public string Help_uploadControlActionMethod { get; set; }
    
        public string Help_uploadMethod { get; set; }
        public List<string> Help_File_caption { get; set; }
        public List<int> Help_File_Ratings { get; set; }
        public List<string> Help_File_Old_captions { get; set; }
        public List<string> Help_File_Old_Attachment_IDs { get; set; }
        public List<int> Help_File_Old_Ratings { get; set; }
    }
    [Serializable]
    public class Return_Attachment_Post
    {
        public string Message { get; set; }    
        public bool result { get; set; }
        public string AttachmentID { get; set; }
        public string FileList { get; set; }
        public string AttachmentIdsList { get; set; }
        public string captionsList { get; set; }
    }
}