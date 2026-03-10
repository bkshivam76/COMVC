using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Models
{
    [Serializable]
    public class AllRights
    {
        public bool AddRight { get; set; }

        public bool AddRight_XtraScreen1 { get; set; }//Mantis bug 0000380 fixed

        public bool AddRight_XtraScreen2 { get; set; }//Mantis bug 0000380 fixed

        public bool AddRight_XtraScreen3 { get; set; }//Mantis bug 0000380 fixed

        public bool AddRight_XtraScreen4 { get; set; }//Mantis bug 0000421 fixed
        public bool UpdateRight { get; set; }

        public bool DeleteRight { get; set; }

        public bool ViewRight { get; set; }
        public bool ViewRight_XtraScreen1 { get; set; }//Mantis bug 0000421 fixed

        public bool ExportRight { get; set; }

        public bool ListRight { get; set; }

        public bool FullRight { get; set; }

        public bool ReportRight { get; set; }

        public bool Accounts_ResponsibleRight { get; set; }

        public bool AUDITRight { get; set; }

        public bool ApproveRight { get; set; }        
    }
}