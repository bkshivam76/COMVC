using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Account.Models
{
    [Serializable]
    public class Frm_Doc_Library_Models
    {
        public List<Return_GetDocumentList> GridView1 { get; set; }
    }
    [Serializable]
    public class Return_GetDocumentList
    {
        public string ID { get; set; }

        public string Title { get; set; }
        public string FileName { get; set; }
        public string Category { get; set; }
        public string FileLocationPath { get; set; }

    }
}