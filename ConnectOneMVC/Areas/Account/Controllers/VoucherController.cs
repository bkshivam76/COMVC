using ConnectOneMVC.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExtreme.AspNet.Mvc;
using ConnectOneMVC.Helper;
using Newtonsoft.Json;
using DevExtreme.AspNet.Data;
using ConnectOneMVC.Areas.Account.Models;
using DevExpress.Web.Mvc;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.IO;
using System.Collections;
using ConnectOneMVC.Models;
using Common_Lib;
using Common_Lib.RealTimeService;
using static Common_Lib.DbOperations.Attachments;
using ConnectOne.D0010._001;
using static Common_Lib.DbOperations;
using Newtonsoft.Json.Linq;

namespace ConnectOneMVC.Areas.Account.Controllers
{
    [CheckLogin]
    public class VoucherController : BaseController
    {

        #region New Global varaible
        public DateTime? xFr_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xFr_Date_CB");
            }
            set
            {
                SetBaseSession("xFr_Date_CB", value);
            }
        }
        public DateTime? xTo_Date
        {
            get
            {
                return (DateTime?)GetBaseSession("xTo_Date_CB");
            }
            set
            {
                SetBaseSession("xTo_Date_CB", value);
            }
        }
        //public string Closed_Bank_Account_No
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("Closed_Bank_Account_No_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Closed_Bank_Account_No_CB", value);
        //    }
        //}
        //public string xID
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("xID_CB");

        //    }
        //    set
        //    {
        //        SetBaseSession("xID_CB", value);
        //    }
        //}
        //public string xMID
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("xMID_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("xMID_CB", value);
        //    }
        //}
        public double Open_Cash_Bal
        {
            get
            {
                return (double)GetBaseSession("Open_Cash_Bal_CB");
            }
            set
            {
                SetBaseSession("Open_Cash_Bal_CB", value);
            }
        }
        public double Close_Cash_Bal
        {
            get
            {
                return (double)GetBaseSession("Close_Cash_Bal_CB");
            }
            set
            {
                SetBaseSession("Close_Cash_Bal_CB", value);
            }
        }

        public double Open_Bank_Bal
        {
            get
            {
                return (double)GetBaseSession("Open_Bank_Bal_CB");

            }
            set
            {
                SetBaseSession("Open_Bank_Bal_CB", value);
            }
        }
        //public double Open_Bank_Bal01
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal01_CB");              
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal01_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal02
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal02_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal02_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal03
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal03_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal03_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal04
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal04_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal04_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal05
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal05_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal05_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal06
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal06_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal06_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal07
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal07_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal07_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal08
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal08_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal08_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal09
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal09_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal09_CB", value);
        //    }
        //}
        //public double Open_Bank_Bal10
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Open_Bank_Bal10_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Open_Bank_Bal10_CB", value);
        //    }
        //}

        public double Close_Bank_Bal
        {
            get
            {
                return (double)GetBaseSession("Close_Bank_Bal_CB");
            }
            set
            {
                SetBaseSession("Close_Bank_Bal_CB", value);
            }
        }
        //public double Close_Bank_Bal01
        //{     
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal01_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal01_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal02
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal02_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal02_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal03
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal03_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal03_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal04
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal04_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal04_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal05
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal05_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal05_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal06
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal06_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal06_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal07
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal07_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal07_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal08
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal08_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal08_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal09
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal09_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal09_CB", value);
        //    }
        //}
        //public double Close_Bank_Bal10
        //{
        //    get
        //    {
        //        return (double)GetBaseSession("Close_Bank_Bal10_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("Close_Bank_Bal10_CB", value);
        //    }
        //}
        public string FType
        {
            get
            {
                return (string)GetBaseSession("FType_CB");
            }
            set
            {
                SetBaseSession("FType_CB", value);
            }
        }
        public string Advanced_Filter_Category
        {
            get
            {
                return (string)GetBaseSession("Advanced_Filter_Category_CB");
            }
            set
            {
                SetBaseSession("Advanced_Filter_Category_CB", value);
            }
        }
        public string Advanced_Filter_RefID
        {
            get
            {
                return (string)GetBaseSession("Advanced_Filter_RefID_CB");
            }
            set
            {
                SetBaseSession("Advanced_Filter_RefID_CB", value);
            }
        }
        public string ActiveFilterString
        {
            get
            {
                return (string)GetBaseSession("ActiveFilterString_CB");
            }
            set
            {
                SetBaseSession("ActiveFilterString_CB", value);
            }
        }
        public bool Summary_Column_Status
        {
            get
            {
                return (bool)GetBaseSession("Summary_Column_Status_CB");
            }
            set
            {
                SetBaseSession("Summary_Column_Status_CB", value);
            }
        }
        public string Negative_MsgStr
        {
            get
            {
                return (string)GetBaseSession("Negative_MsgStr_CB");
            }
            set
            {
                SetBaseSession("Negative_MsgStr_CB", value);
            }
        }

        //public string REC_BANK01_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK01_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK01_Field_CB", value);
        //    }       
        //}
        //public string REC_BANK02_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK02_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK02_Field_CB", value);
        //    }
        //}
        //public string REC_BANK03_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK03_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK03_Field_CB", value);
        //    }
        //}
        //public string REC_BANK04_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK04_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK04_Field_CB", value);
        //    }
        //}
        //public string REC_BANK05_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK05_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK05_Field_CB", value);
        //    }
        //}
        //public string REC_BANK06_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK06_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK06_Field_CB", value);
        //    }
        //}
        //public string REC_BANK07_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK07_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK07_Field_CB", value);
        //    }
        //}
        //public string REC_BANK08_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK08_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK08_Field_CB", value);
        //    }
        //}
        //public string REC_BANK09_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK09_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK09_Field_CB", value);
        //    }
        //}
        //public string REC_BANK10_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK10_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK10_Field_CB", value);
        //    }
        //}


        //public string PAY_BANK01_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK01_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK01_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK02_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK02_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK02_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK03_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK03_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK03_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK04_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK04_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK04_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK05_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK05_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK05_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK06_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK06_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK06_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK07_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK07_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK07_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK08_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK08_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK08_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK09_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK09_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK09_Field_CB", value);
        //    }
        //}
        //public string PAY_BANK10_Field
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK10_Field_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK10_Field_CB", value);
        //    }
        //}


        //public string REC_BANK01_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK01_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK01_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK02_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK02_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK02_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK03_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK03_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK03_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK04_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK04_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK04_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK05_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK05_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK05_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK06_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK06_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK06_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK07_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK07_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK07_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK08_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK08_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK08_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK09_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK09_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK09_Caption_CB", value);
        //    }
        //}
        //public string REC_BANK10_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK10_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK10_Caption_CB", value);
        //    }
        //}


        //public string PAY_BANK01_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK01_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK01_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK02_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK02_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK02_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK03_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK03_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK03_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK04_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK04_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK04_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK05_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK05_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK05_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK06_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK06_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK06_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK07_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK07_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK07_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK08_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK08_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK08_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK09_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK09_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK09_Caption_CB", value);
        //    }
        //}
        //public string PAY_BANK10_Caption
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK10_Caption_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK10_Caption_CB", value);
        //    }
        //}


        //public string REC_BANK01_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK01_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK01_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK02_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK02_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK02_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK03_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK03_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK03_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK04_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK04_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK04_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK05_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK05_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK05_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK06_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK06_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK06_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK07_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK07_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK07_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK08_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK08_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK08_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK09_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK09_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK09_Tooltip_CB", value);
        //    }
        //}
        //public string REC_BANK10_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK10_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK10_Tooltip_CB", value);
        //    }
        //}


        //public string PAY_BANK01_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK01_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK01_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK02_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK02_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK02_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK03_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK03_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK03_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK04_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK04_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK04_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK05_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK05_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK05_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK06_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK06_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK06_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK07_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK07_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK07_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK08_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK08_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK08_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK09_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK09_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK09_Tooltip_CB", value);
        //    }
        //}
        //public string PAY_BANK10_Tooltip
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK10_Tooltip_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK10_Tooltip_CB", value);
        //    }
        //}

        //public bool REC_BANK01_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK01_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK01_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK02_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK02_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK02_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK03_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK03_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK03_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK04_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK04_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK04_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK05_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK05_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK05_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK06_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK06_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK06_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK07_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK07_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK07_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK08_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK08_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK08_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK09_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK09_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK09_Visible_CB", value);
        //    }
        //}
        //public bool REC_BANK10_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK10_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK10_Visible_CB", value);
        //    }
        //}


        //public bool PAY_BANK01_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK01_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK01_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK02_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK02_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK02_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK03_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK03_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK03_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK04_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK04_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK04_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK05_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK05_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK05_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK06_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK06_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK06_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK07_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK07_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK07_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK08_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK08_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK08_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK09_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK09_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK09_Visible_CB", value);
        //    }
        //}
        //public bool PAY_BANK10_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK10_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK10_Visible_CB", value);
        //    }
        //}

        //public bool REC_BANK01_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK01_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK01_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK02_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK02_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK02_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK03_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK03_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK03_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK04_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK04_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK04_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK05_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK05_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK05_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK06_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK06_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK06_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK07_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK07_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK07_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK08_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK08_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK08_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK09_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK09_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK09_Remove_CB", value);
        //    }
        //}
        //public bool REC_BANK10_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("REC_BANK10_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK10_Remove_CB", value);
        //    }
        //}


        //public bool PAY_BANK01_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK01_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK01_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK02_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK02_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK02_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK03_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK03_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK03_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK04_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK04_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK04_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK05_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK05_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK05_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK06_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK06_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK06_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK07_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK07_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK07_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK08_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK08_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK08_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK09_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK09_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK09_Remove_CB", value);
        //    }
        //}
        //public bool PAY_BANK10_Remove
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("PAY_BANK10_Remove_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK10_Remove_CB", value);
        //    }
        //}

        //public string REC_BANK01_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK01_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK01_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK02_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK02_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK02_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK03_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK03_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK03_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK04_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK04_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK04_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK05_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK05_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK05_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK06_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK06_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK06_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK07_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK07_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK07_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK08_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK08_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK08_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK09_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK09_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK09_Tag_CB", value);
        //    }
        //}
        //public string REC_BANK10_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK10_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK10_Tag_CB", value);
        //    }
        //}


        //public string PAY_BANK01_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK01_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK01_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK02_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK02_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK02_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK03_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK03_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK03_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK04_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK04_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK04_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK05_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK05_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK05_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK06_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK06_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK06_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK07_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK07_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK07_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK08_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK08_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK08_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK09_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK09_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK09_Tag_CB", value);
        //    }
        //}
        //public string PAY_BANK10_Tag
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK10_Tag_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK10_Tag_CB", value);
        //    }
        //}

        //public string REC_BANK01_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK01_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK01_CC_CB", value);
        //    }
        //}
        //public string REC_BANK02_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK02_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK02_CC_CB", value);
        //    }
        //}
        //public string REC_BANK03_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK03_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK03_CC_CB", value);
        //    }
        //}
        //public string REC_BANK04_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK04_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK04_CC_CB", value);
        //    }
        //}
        //public string REC_BANK05_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK05_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK05_CC_CB", value);
        //    }
        //}
        //public string REC_BANK06_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK06_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK06_CC_CB", value);
        //    }
        //}
        //public string REC_BANK07_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK07_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK07_CC_CB", value);
        //    }
        //}
        //public string REC_BANK08_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK08_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK08_CC_CB", value);
        //    }
        //}
        //public string REC_BANK09_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK09_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK09_CC_CB", value);
        //    }
        //}
        //public string REC_BANK10_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("REC_BANK10_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("REC_BANK10_CC_CB", value);
        //    }
        //}


        //public string PAY_BANK01_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK01_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK01_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK02_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK02_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK02_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK03_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK03_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK03_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK04_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK04_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK04_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK05_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK05_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK05_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK06_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK06_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK06_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK07_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK07_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK07_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK08_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK08_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK08_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK09_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK09_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK09_CC_CB", value);
        //    }
        //}
        //public string PAY_BANK10_CC
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("PAY_BANK10_CC_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("PAY_BANK10_CC_CB", value);
        //    }
        //}

        //public bool iTR_REC_BANK_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("iTR_REC_BANK_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("iTR_REC_BANK_Visible_CB", value);
        //    }       
        //}
        //public bool iTR_PAY_BANK_Visible
        //{
        //    get
        //    {
        //        return (bool)GetBaseSession("iTR_PAY_BANK_Visible_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("iTR_PAY_BANK_Visible_CB", value);
        //    }      
        //}
        public bool iRef_no_Visible
        {
            get
            {
                return (bool)GetBaseSession("iRef_no_Visible_CB");
            }
            set
            {
                SetBaseSession("iRef_no_Visible_CB", value);
            }
        }

        public bool iTR_REC_JOURNAL_Remove
        {
            get
            {
                return (bool)GetBaseSession("iTR_REC_JOURNAL_Remove_CB");
            }
            set
            {
                SetBaseSession("iTR_REC_JOURNAL_Remove_CB", value);
            }
        }
        public bool iTR_REC_TOTAL_Remove
        {
            get
            {
                return (bool)GetBaseSession("iTR_REC_TOTAL_Remove_CB");
            }
            set
            {
                SetBaseSession("iTR_REC_TOTAL_Remove_CB", value);
            }
        }
        public bool iTR_PAY_JOURNAL_Remove
        {
            get
            {
                return (bool)GetBaseSession("iTR_PAY_JOURNAL_Remove_CB");
            }
            set
            {
                SetBaseSession("iTR_PAY_JOURNAL_Remove_CB", value);
            }
        }
        public bool iTR_PAY_TOTAL_Remove
        {
            get
            {
                return (bool)GetBaseSession("iTR_PAY_TOTAL_Remove_CB");
            }
            set
            {
                SetBaseSession("iTR_PAY_TOTAL_Remove_CB", value);
            }
        }
        public string HideBank_Text
        {
            get
            {
                return (string)GetBaseSession("HideBank_Text_CB");
            }
            set
            {
                SetBaseSession("HideBank_Text_CB", value);
            }
        }
        public string BE_Cash_Bank_Text
        {
            get
            {
                return (string)GetBaseSession("BE_Cash_Bank_Text_CB");
            }
            set
            {
                SetBaseSession("BE_Cash_Bank_Text_CB", value);
            }
        }
        //public List<CB_Grid_Model> CB_GridData
        //{
        //    get
        //    {
        //        return (List<CB_Grid_Model>)GetBaseSession("MainGrid_Data_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("MainGrid_Data_CB", value);
        //    }
        //}
        public DataTable CB_GridData_DT
        {
            get
            {
                return (DataTable)GetBaseSession("MainGrid_DataTable_CB");
            }
            set
            {
                SetBaseSession("MainGrid_DataTable_CB", value);
            }
        }

        public bool showDynamicBankColumns_CB
        {
            get
            {
                return (bool)GetBaseSession("showDynamicBankColumns_CB");
            }
            set
            {
                SetBaseSession("showDynamicBankColumns_CB", value);
            }
        }
        public double _Next_Unattended_Attachment_Index 
        {
            get
            {
                return (double)GetBaseSession("Next_Unattended_Attachment_Index_CB");
            }
            set
            {
                SetBaseSession("Next_Unattended_Attachment_Index_CB", value);
            }
        }

        //public string CashBookMID
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("CashBookMID_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("CashBookMID_CB", value);
        //    }
        //}
        //public string CashBookID
        //{
        //    get
        //    {
        //        return (string)GetBaseSession("CashBookID_CB");
        //    }
        //    set
        //    {
        //        SetBaseSession("CashBookID_CB", value);
        //    }
        //}
        public List<DbOperations.Audit.Return_GetEntry_AdditionalInfo> CB_AdditionalInfoGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetEntry_AdditionalInfo>)GetBaseSession("CB_AdditionalInfoGrid_CB");
            }
            set
            {
                SetBaseSession("CB_AdditionalInfoGrid_CB", value);
            }
        }
        public List<DbOperations.Audit.Return_GetDocumentMapping> CashBookNestedGrid
        {
            get
            {
                return (List<DbOperations.Audit.Return_GetDocumentMapping>)GetBaseSession("CashBookNestedGrid_CB");
            }
            set
            {
                SetBaseSession("CashBookNestedGrid_CB", value);
            }
        }
        public List<CB_Period> CB_PeriodSelectionData
        {
            get
            {
                return (List<CB_Period>)GetBaseSession("CB_PeriodSelectionData_CB");
            }
            set
            {
                SetBaseSession("CB_PeriodSelectionData_CB", value);
            }
        }
        public List<Summary> CB_SummaryGridData
        {
            get
            {
                return (List<Summary>)GetBaseSession("CB_SummaryGridData_CB");
            }
            set
            {
                SetBaseSession("CB_SummaryGridData_CB", value);
            }
        }

        #endregion

        #region Cash book

        public ActionResult Frm_Voucher_Info_CB(string PopupID = "", string AttachmentID = "", string Period = "", string FromDate = "", string ToDate = "")
        {
            CashBook_Add_User_Rights();
            CashBook_Other_User_Rights();
            ViewBag.PopupID = PopupID;
            ViewBag.LinkAttachmentID = AttachmentID;
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.self_posted = BASE._open_User_Self_Posted;
            ViewBag.Filename = "CashBook_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            if (!(CheckRights(BASE, ClientScreen.Accounts_CashBook, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove                
            }
            ResetSaticVariable();
            FillChangePeriod();
            if (!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Manage_Remarks))
            {
                ViewData["CB_AddRemarks_Visible"] = false;
            }
            else
            {
                ViewData["CB_AddRemarks_Visible"] = true;
            }
            if ((!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock)) && !(BASE._open_Cen_ID == 4216))
            {
                ViewData["CB_Locked_Visible"] = false;
                ViewData["CB_UnLocked_Visible"] = false;
                ViewData["CB_MatchTransfer_Visible"] = false;
                ViewData["CB_UnMatchTransfer_Visible"] = false;
            }
            else
            {
                ViewData["CB_Locked_Visible"] = true;
                ViewData["CB_UnLocked_Visible"] = true;
                ViewData["CB_MatchTransfer_Visible"] = true;
                ViewData["CB_UnMatchTransfer_Visible"] = true;
            }
            //ViewBag.ShowHorizontalBar = BASE._List_Of_FullData_Screen.FindIndex((ClientScreen.Accounts_CashBook).ToString()) ? 0 : 1;
            ViewBag.CB_ShowHorizontalBar = BASE._List_Of_FullData_Screen.Any(s => s.Equals((ClientScreen.Accounts_CashBook).ToString(), StringComparison.OrdinalIgnoreCase)) ? 1 : 0;
            showDynamicBankColumns_CB = false;
            ViewData["showDynamicBankColumns_CB"] = showDynamicBankColumns_CB;
            Create_Bank_Columns();
            //Get last unaudited Year Closing Cash Balance 
            object MaxValue = 0;
            DateTime xLastDate = DateTime.Now;
            if (string.IsNullOrWhiteSpace(Period))
            {
                MaxValue = BASE._Voucher_DBOps.GetMaxTransactionDate();
                if (MaxValue == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!'); btnCloseActiveTab();</script>");
                }
                if (Convert.IsDBNull(MaxValue))
                {
                    xLastDate = BASE._open_Year_Sdt;
                }
                else
                {
                    xLastDate = Convert.ToDateTime(MaxValue);
                }

                int xMM = xLastDate.Month;
                ViewBag.PeriodSelectedIndex = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;
            }
            else
            {
                ViewBag.PeriodSelectedIndex = Convert.ToInt32(Period);
            }
            if (!string.IsNullOrWhiteSpace(FromDate))
            {
                xFr_Date = Convert.ToDateTime(FromDate);
            }
            if (!string.IsNullOrWhiteSpace(ToDate))
            {
                xTo_Date = Convert.ToDateTime(ToDate);
            }
            return View();
        }
        public PartialViewResult Frm_Voucher_Info_CB_Grid(string command = "", string _ActiveFilterString = "", bool GetLatestData = false, int ShowHorizontalBar = 0, bool AllowCellMerge = true, bool ToExpand = false, string ViewMode = "Default", bool VouchingMode = false, string Layout = null, string ColumnToBeHidddenIndex = "", string ColumnToBeShownIndex = "",string RowKeyToFocus="",int? PageIndex=null, bool showDynamicBankColumns = false)
        {
            ViewBag.ExportGridHeaderLeft = "UID : " + BASE._open_UID_No;
            ViewBag.ExportGridHeaderRight = "Year : " + BASE._open_Year_Name + "";
            ViewBag.ExportGridFooter = "Printed By : " + BASE._open_User_ID + ", On : " + DateTime.Now.ToString();
            ViewBag.CB_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.ShowNarration = AllowCellMerge;
            ViewBag.ToExpand = ToExpand;
            ViewBag.ViewMode = ViewMode;
            ViewBag.VouchingMode = VouchingMode;
            ViewBag.UserType = BASE._open_User_Type.ToUpper();
            ViewBag.ColumnToBeShownIndex = ColumnToBeShownIndex;
            ViewBag.ColumnToBeHidddenIndex = ColumnToBeHidddenIndex;
            ViewData["Layout"] = Layout;
            ViewData["RowKeyToFocus"] = RowKeyToFocus;
            ViewData["PageIndex"] = PageIndex;
            ViewBag.Filename = "CashBook_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            showDynamicBankColumns_CB = showDynamicBankColumns;
            ViewData["showDynamicBankColumns_CB"] = showDynamicBankColumns_CB;
            if (GetLatestData == true || CB_GridData_DT == null)
            {
                var data = Grid_Display(_ActiveFilterString, null, showDynamicBankColumns_CB);
            }
            if (command == "APPLYFILTER")
            {
                ActiveFilterString = "";
            }
            CreateViewData();
            return PartialView("Frm_Voucher_Info_CB_Grid", CB_GridData_DT);
        }
        public PartialViewResult Frm_Voucher_Info_CB_Grid_Nested(string ID, string MID, string command, int ShowHorizontalBar = 0, bool VouchingMode = false)
        {
            ViewBag.CB_ShowHorizontalBar = ShowHorizontalBar;
            ViewBag.CashBookID = ID;
            ViewBag.CashBookMID = MID;
            ViewBag.VouchingMode = VouchingMode;
            if (command == "REFRESH")
            {
                if (VouchingMode == false)
                {
                    List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(ID, MID, ClientScreen.Accounts_CashBook);
                    CashBookNestedGrid = _docList;
                    Session["CashBookNestedGrid"] = _docList;
                }
                else
                {
                    var data = BASE._Audit_DBOps.GetDocumentMapping_With_Additional_Info(ID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook);
                    CashBookNestedGrid = data.DocumentMapping;
                    Session["CashBookNestedGrid"] = data.DocumentMapping;
                    CB_AdditionalInfoGrid = data.AdditionalInfo;
                }
            }
            return PartialView(CashBookNestedGrid);
        }
        public ActionResult AdditionalInfo_Grid(string ID, string MID, string command)
        {
            if (command == "REFRESH") 
            {
                CB_AdditionalInfoGrid = BASE._Audit_DBOps.GetAdditionalInfo(ID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook);
            }
            return View(CB_AdditionalInfoGrid);
        }
        public ActionResult LeftPaneContent(string ID, bool VouchingMode, string MID = "")
        {
            ViewBag.ID = ID;
            ViewBag.MID = MID;
            ViewBag.VouchingMode = VouchingMode;
            return View();
        }
        

        public ActionResult Refresh_GridIcon_PreviewRow(string TempID, string NestedRowKeyValue)
        {
            Grid_Display(ActiveFilterString, null, showDynamicBankColumns_CB);
            List<DbOperations.Audit.Return_GetDocumentMapping> _docList = BASE._Audit_DBOps.GetDocumentMapping(TempID, TempID, ClientScreen.Accounts_CashBook);
            CashBookNestedGrid = _docList;
            var Attachment_VOUCHING_STATUS = "";
            var Attachment_VOUCHING_REMARKS = "";
            bool? Attachment_Vouching_During_Audit = null;
            var Vouching_History = "";
            var VouchingDetails = "";

            if (CashBookNestedGrid.Where(x => x.UniqueID == NestedRowKeyValue).Count() > 0)
            {
                var AttachmentRow = CashBookNestedGrid.Where(x => x.UniqueID == NestedRowKeyValue).First();
                Attachment_VOUCHING_STATUS = AttachmentRow.Vouching_Status;
                Attachment_VOUCHING_REMARKS = AttachmentRow.Vouching_Remarks;
                Attachment_Vouching_During_Audit = AttachmentRow.Vouching_During_Audit;
                Vouching_History = AttachmentRow.Vouching_History;
                VouchingDetails = AttachmentRow.Vouching_Details;
            }
            //string _Main_iIcon = CB_GridData.Where(x => x.iTR_TEMP_ID == TempID).First().iIcon; // Code for the List Type Session Variable
            string Main_iIcon = (CB_GridData_DT.AsEnumerable().Where(x => x.Field<string>("iTR_TEMP_ID") == TempID).First())["iIcon"].ToString();

            return Json(new
            {
                Main_iIcon,
                Attachment_VOUCHING_STATUS,
                Attachment_VOUCHING_REMARKS,
                Attachment_Vouching_During_Audit,
                Vouching_History,
                VouchingDetails
            }, JsonRequestBehavior.AllowGet);
        }

        public static GridViewSettings CreateGeneralDetailGridSettings(string ID, string MID)
        {
            GridViewSettings settings = new GridViewSettings();
            settings.Name = "CashBookListGridNested" + ID + MID;
            settings.SettingsDetail.MasterGridName = "CashBookListGrid";
            settings.KeyFieldName = "ID";
            //settings.KeyFieldName = "pKey";
            settings.Columns.Add("Item_Name").Visible = true;
            settings.Columns.Add("Document_Name").Visible = true;
            settings.Columns.Add("Reason").Visible = true;
            settings.Columns.Add("FromDate").Visible = true;
            settings.Columns.Add("ToDate").Visible = true;
            settings.Columns.Add("Description").Visible = true;
            settings.Columns.Add("ID").Visible = false;
            settings.Columns.Add("MID").Visible = false;
            // settings.ClientSideEvents.FocusedRowChanged = "OnItemUserOrderFocusedRowChange";
            settings.SettingsBehavior.AllowFocusedRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;
            settings.SettingsBehavior.AllowSelectSingleRowOnly = true;
            // settings.ClientSideEvents.RowDblClick = "OnEditButtonClick";
            settings.SettingsCustomizationDialog.ShowColumnChooserPage = true;

            return settings;
        }// setting for exporting nestedgrid
        public static IEnumerable GetAttachments(string ID, string MID)
        {
            List<DbOperations.Audit.Return_GetDocumentMapping> doclist = (List<DbOperations.Audit.Return_GetDocumentMapping>)System.Web.HttpContext.Current.Session["CashBookNestedGrid"];
            return doclist;
        }//binding data to nestedgrid
        public ActionResult CB_GetGridData(string key)
        {
            string itstr = null;
            string NextRowTempID = "";
            if (key != null)
            {
                //var FinalData = CB_GridData as List<CB_Grid_Model>;
                //var actionItems = (CB_Grid_Model)FinalData.Where(f => f.Grid_PK == key).FirstOrDefault();

                //var index = FinalData.FindIndex(a => a.Grid_PK == key);
                //if (FinalData.Count-1== index)
                //{
                //    NextRowTempID = "Last_Row";
                //}
                //else
                //{
                //    NextRowTempID = FinalData[index+1].iTR_TEMP_ID;
                //}
                //if (actionItems != null)
                //{
                //    itstr = actionItems.iTR_TEMP_ID + "![" + actionItems.iREC_ID + "![" + actionItems.iREC_EDIT_ON + "![" + actionItems.iTR_ITEM_ID + "![" + actionItems.iTR_AB_ID_1 + "![" +
                //                actionItems.iTR_M_ID + "![" + actionItems.iACTION_STATUS + "![" + actionItems.iTR_CODE + "![" + actionItems.iREC_EDIT_BY + "![" + actionItems.iTR_SR_NO + "!["
                //                + actionItems.iREC_ADD_ON + "![" + actionItems.iTR_DATE + "![" + actionItems.iTR_ITEM + "![" + actionItems.iREC_EDIT_ON + "![" + actionItems.iREC_ADD_BY + "!["
                //                + actionItems.iREC_STATUS_BY + "![" + actionItems.iREC_STATUS_ON + "![" + actionItems.iRef_no + "![" + actionItems.iTR_REF_NO + "![" + actionItems.iREQ_ATTACH_COUNT
                //                 + "![" + actionItems.iRESPONDED_COUNT + "![" + actionItems.iCOMPLETE_ATTACH_COUNT + "![" + actionItems.iTR_TYPE;// + "![" + NextRowTempID;
                //}

                DataRow dr_CashBookRowData = CB_GridData_DT.AsEnumerable().Where(x => x.Field<string>("Grid_PK") == key).FirstOrDefault();
                if (dr_CashBookRowData != null)
                {
                    itstr = dr_CashBookRowData["iTR_TEMP_ID"].ToString() + "![" + dr_CashBookRowData["iREC_ID"].ToString() + "![" + dr_CashBookRowData["iREC_EDIT_ON"].ToString() + "![" + dr_CashBookRowData["iTR_ITEM_ID"].ToString() + "![" + dr_CashBookRowData["iTR_AB_ID_1"].ToString() + "![" +
                                dr_CashBookRowData["iTR_M_ID"].ToString() + "![" + dr_CashBookRowData["iACTION_STATUS"].ToString() + "![" + dr_CashBookRowData["iTR_CODE"].ToString() + "![" + dr_CashBookRowData["iREC_EDIT_BY"].ToString() + "![" + dr_CashBookRowData["iTR_SR_NO"].ToString() + "!["
                                + dr_CashBookRowData["iREC_ADD_ON"].ToString() + "![" + dr_CashBookRowData["iTR_DATE"].ToString() + "![" + dr_CashBookRowData["iTR_ITEM"].ToString() + "![" + dr_CashBookRowData["iREC_EDIT_ON"].ToString() + "![" + dr_CashBookRowData["iREC_ADD_BY"].ToString() + "!["
                                + dr_CashBookRowData["iREC_STATUS_BY"].ToString() + "![" + dr_CashBookRowData["iREC_STATUS_ON"].ToString() + "![" + dr_CashBookRowData["iRef_no"].ToString() + "![" + dr_CashBookRowData["iTR_REF_NO"].ToString() + "![" + dr_CashBookRowData["REQ_ATTACH_COUNT"].ToString()
                                 + "![" + dr_CashBookRowData["RESPONDED_COUNT"].ToString() + "![" + dr_CashBookRowData["COMPLETE_ATTACH_COUNT"].ToString() + "![" + dr_CashBookRowData["iTR_TYPE"].ToString();// + "![" + NextRowTempID;
                }
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public ActionResult CB_GetNestedGridData(string ID)
        {
            string itstr = "";
            var FinalData = CashBookNestedGrid as List<DbOperations.Audit.Return_GetDocumentMapping>;

            DbOperations.Audit.Return_GetDocumentMapping actionItems = (DbOperations.Audit.Return_GetDocumentMapping)FinalData.Where(f => f.UniqueID == ID).FirstOrDefault();

            if (actionItems != null)
            {
                itstr = actionItems.Doc_Status + "![" + actionItems.Params_Mandatory + "![" + actionItems.LABEL_FROM_DATE + "![" + actionItems.LABEL_TO_DATE + "![" + actionItems.LABEL_DESCRIPTION + "![" + actionItems.Document_Category + "![" + actionItems.Document_ID + "![" + actionItems.ATTACH_ID + "![" + actionItems.TxnID + "![" + actionItems.TxnMID + "![" + actionItems.Tr_Code + "![" + actionItems.MAP_ID + "![" + actionItems.Reason + "![" + actionItems.ATTACH_FILE_NAME + "![" + actionItems.Attachment_Action_Status + "![" + actionItems.ReasonID;
            }
            return GridViewExtension.GetCustomDataCallbackResult(itstr);
        }
        public void Create_Bank_Columns()
        {
            //REC_BANK01_Remove = true;
            //REC_BANK02_Remove = true;
            //REC_BANK03_Remove = true;
            //REC_BANK04_Remove = true;
            //REC_BANK05_Remove = true;
            //REC_BANK06_Remove = true;
            //REC_BANK07_Remove = true;
            //REC_BANK08_Remove = true;
            //REC_BANK09_Remove = true;
            //REC_BANK10_Remove = true;

            //REC_BANK01_Visible = false;
            //REC_BANK02_Visible = false;
            //REC_BANK03_Visible = false;
            //REC_BANK04_Visible = false;
            //REC_BANK05_Visible = false;
            //REC_BANK06_Visible = false;
            //REC_BANK07_Visible = false;
            //REC_BANK08_Visible = false;
            //REC_BANK09_Visible = false;
            //REC_BANK10_Visible = false;

            //REC_BANK01_Field = "REC_BANK01";
            //REC_BANK02_Field = "REC_BANK02";
            //REC_BANK03_Field = "REC_BANK03";
            //REC_BANK04_Field = "REC_BANK04";
            //REC_BANK05_Field = "REC_BANK05";
            //REC_BANK06_Field = "REC_BANK06";
            //REC_BANK07_Field = "REC_BANK07";
            //REC_BANK08_Field = "REC_BANK08";
            //REC_BANK09_Field = "REC_BANK09";
            //REC_BANK10_Field = "REC_BANK10";

            //REC_BANK01_Caption = "REC_BANK01";
            //REC_BANK02_Caption = "REC_BANK02";
            //REC_BANK03_Caption = "REC_BANK03";
            //REC_BANK04_Caption = "REC_BANK04";
            //REC_BANK05_Caption = "REC_BANK05";
            //REC_BANK06_Caption = "REC_BANK06";
            //REC_BANK07_Caption = "REC_BANK07";
            //REC_BANK08_Caption = "REC_BANK08";
            //REC_BANK09_Caption = "REC_BANK09";
            //REC_BANK10_Caption = "REC_BANK10";

            //REC_BANK01_Tag = "NO";
            //REC_BANK02_Tag = "NO";
            //REC_BANK03_Tag = "NO";
            //REC_BANK04_Tag = "NO";
            //REC_BANK05_Tag = "NO";
            //REC_BANK06_Tag = "NO";
            //REC_BANK07_Tag = "NO";
            //REC_BANK08_Tag = "NO";
            //REC_BANK09_Tag = "NO";
            //REC_BANK10_Tag = "NO";

            //PAY_BANK01_Remove = true;
            //PAY_BANK02_Remove = true;
            //PAY_BANK03_Remove = true;
            //PAY_BANK04_Remove = true;
            //PAY_BANK05_Remove = true;
            //PAY_BANK06_Remove = true;
            //PAY_BANK07_Remove = true;
            //PAY_BANK08_Remove = true;
            //PAY_BANK09_Remove = true;
            //PAY_BANK10_Remove = true;

            //PAY_BANK01_Visible = false;
            //PAY_BANK02_Visible = false;
            //PAY_BANK03_Visible = false;
            //PAY_BANK04_Visible = false;
            //PAY_BANK05_Visible = false;
            //PAY_BANK06_Visible = false;
            //PAY_BANK07_Visible = false;
            //PAY_BANK08_Visible = false;
            //PAY_BANK09_Visible = false;
            //PAY_BANK10_Visible = false;

            //PAY_BANK01_Field = "PAY_BANK01";
            //PAY_BANK02_Field = "PAY_BANK02";
            //PAY_BANK03_Field = "PAY_BANK03";
            //PAY_BANK04_Field = "PAY_BANK04";
            //PAY_BANK05_Field = "PAY_BANK05";
            //PAY_BANK06_Field = "PAY_BANK06";
            //PAY_BANK07_Field = "PAY_BANK07";
            //PAY_BANK08_Field = "PAY_BANK08";
            //PAY_BANK09_Field = "PAY_BANK09";
            //PAY_BANK10_Field = "PAY_BANK10";

            //PAY_BANK01_Caption = "PAY_BANK01";
            //PAY_BANK02_Caption = "PAY_BANK02";
            //PAY_BANK03_Caption = "PAY_BANK03";
            //PAY_BANK04_Caption = "PAY_BANK04";
            //PAY_BANK05_Caption = "PAY_BANK05";
            //PAY_BANK06_Caption = "PAY_BANK06";
            //PAY_BANK07_Caption = "PAY_BANK07";
            //PAY_BANK08_Caption = "PAY_BANK08";
            //PAY_BANK09_Caption = "PAY_BANK09";
            //PAY_BANK10_Caption = "PAY_BANK10";

            //PAY_BANK01_Tag = "NO";
            //PAY_BANK02_Tag = "NO";
            //PAY_BANK03_Tag = "NO";
            //PAY_BANK04_Tag = "NO";
            //PAY_BANK05_Tag = "NO";
            //PAY_BANK06_Tag = "NO";
            //PAY_BANK07_Tag = "NO";
            //PAY_BANK08_Tag = "NO";
            //PAY_BANK09_Tag = "NO";
            //PAY_BANK10_Tag = "NO";
        }
        public ActionResult Grid_Display(string _ActiveFilterString, int? _GridFocusedOrExpandedIndex = null, bool showDynamicBankColumns = false)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            showDynamicBankColumns_CB = showDynamicBankColumns;
            Open_Cash_Bal = 0;
            Close_Cash_Bal = 0;
            DataTable Cash_Bal;
            Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (Cash_Bal.Rows.Count > 0 && BASE._open_User_Self_Posted == false)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Open_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                }
                else
                {
                    Open_Cash_Bal = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["CLOSING"]))
                {
                    Close_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["CLOSING"]);
                }
                else
                {
                    Close_Cash_Bal = 0;
                }
            }
            else
            {
                Open_Cash_Bal = 0;
                Close_Cash_Bal = 0;
            }
            Open_Bank_Bal = 0; //Open_Bank_Bal01 = 0; Open_Bank_Bal02 = 0; Open_Bank_Bal03 = 0; Open_Bank_Bal04 = 0; Open_Bank_Bal05 = 0; Open_Bank_Bal06 = 0; Open_Bank_Bal07 = 0; Open_Bank_Bal08 = 0; Open_Bank_Bal09 = 0; Open_Bank_Bal10 = 0;
            Close_Bank_Bal = 0;//Close_Bank_Bal01 = 0; Close_Bank_Bal02 = 0; Close_Bank_Bal03 = 0; Close_Bank_Bal04 = 0; Close_Bank_Bal05 = 0; Close_Bank_Bal06 = 0; Close_Bank_Bal07 = 0; Close_Bank_Bal08 = 0; Close_Bank_Bal09 = 0; Close_Bank_Bal10 = 0;
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            int _bankCnt = 1;
            int _colWidth = 0;
            int _colSetWidth = 90;
            string _online_BANK_COL_TR_REC = "";
            string _online_BANK_COL_NB_REC = "";
            string _online_BANK_COL_TR_PAY = "";
            string _online_BANK_COL_NB_PAY = "";
            string _local__BANK_COL_TR_REC = "";
            string _local__BANK_COL_NB_REC = "";
            string _local__BANK_COL_TR_PAY = "";
            string _local__BANK_COL_NB_PAY = "";
            iTR_REC_JOURNAL_Remove = true;
            iTR_REC_TOTAL_Remove = true;
            //if (Bank_Bal.Rows.Count > 0)
            //{
            //    iTR_REC_BANK_Visible = false;
            //    iTR_PAY_BANK_Visible = false;
            //    _colWidth -= _colSetWidth;
            //    HideBank_Text = "Hide Bank";
            //}
            //else
            //{
            HideBank_Text = "Show Bank";
            //}
            iTR_PAY_JOURNAL_Remove = true;
            iTR_PAY_TOTAL_Remove = true;

            //REC_BANK01_Remove = true;
            //REC_BANK02_Remove = true;
            //REC_BANK03_Remove = true;
            //REC_BANK04_Remove = true;
            //REC_BANK05_Remove = true;
            //REC_BANK06_Remove = true;
            //REC_BANK07_Remove = true;
            //REC_BANK08_Remove = true;
            //REC_BANK09_Remove = true;
            //REC_BANK10_Remove = true;

            //PAY_BANK01_Remove = true;
            //PAY_BANK02_Remove = true;
            //PAY_BANK03_Remove = true;
            //PAY_BANK04_Remove = true;
            //PAY_BANK05_Remove = true;
            //PAY_BANK06_Remove = true;
            //PAY_BANK07_Remove = true;
            //PAY_BANK08_Remove = true;
            //PAY_BANK09_Remove = true;
            //PAY_BANK10_Remove = true;

            //REC_BANK01_Tag = "NO";
            //REC_BANK02_Tag = "NO";
            //REC_BANK03_Tag = "NO";
            //REC_BANK04_Tag = "NO";
            //REC_BANK05_Tag = "NO";
            //REC_BANK06_Tag = "NO";
            //REC_BANK07_Tag = "NO";
            //REC_BANK08_Tag = "NO";
            //REC_BANK09_Tag = "NO";
            //REC_BANK10_Tag = "NO";
            int count = Bank_Bal.Rows.Count;
            if (count > 0 && BASE._open_User_Self_Posted == false)
            {
                for (int i = 0; i < count; i++)
                {
                    DataRow XROW = Bank_Bal.Rows[i];
                    if (!Convert.IsDBNull(XROW["OPENING"]))
                    {
                        Open_Bank_Bal += Convert.ToDouble(XROW["OPENING"]);
                    }
                    else
                    {
                        Open_Bank_Bal += 0;
                    }
                    if (!Convert.IsDBNull(XROW["CLOSING"]))
                    {
                        Close_Bank_Bal += Convert.ToDouble(XROW["CLOSING"]);
                    }
                    else
                    {
                        Close_Bank_Bal += 0;
                    }
                    switch (_bankCnt)
                    {
                        #region Commented Code
                        //    case 1:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal01 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal01 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal01 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal01 = 0;
                        //        }
                        //        REC_BANK01_Field = "REC_BANK01";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK01_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK01_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK01_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK01_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK01_Visible = true;
                        //        REC_BANK01_Tag = "YES";
                        //        REC_BANK01_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK01, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK01, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK01_Field = "PAY_BANK01";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK01_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK01_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK01_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK01_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK01_Visible = true;
                        //        PAY_BANK01_Tag = "YES";
                        //        PAY_BANK01_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK01, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK01, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 2:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal02 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal02 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal02 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal02 = 0;
                        //        }
                        //        REC_BANK02_Field = "REC_BANK02";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK02_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK02_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK02_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK02_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK02_Visible = true;
                        //        REC_BANK02_Tag = "YES";
                        //        REC_BANK02_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK02, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK02, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK02_Field = "PAY_BANK02";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK02_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK02_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK02_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK02_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK02_Visible = true;
                        //        PAY_BANK02_Tag = "YES";
                        //        PAY_BANK02_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK02, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK02, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 3:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal03 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal03 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal03 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal03 = 0;
                        //        }
                        //        REC_BANK03_Field = "REC_BANK03";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK03_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK03_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK03_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK03_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK03_Visible = true;
                        //        REC_BANK03_Tag = "YES";
                        //        REC_BANK03_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK03, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK03, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK03_Field = "PAY_BANK03";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK03_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK03_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK03_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK03_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK03_Visible = true;
                        //        PAY_BANK03_Tag = "YES";
                        //        PAY_BANK03_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK03, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK03, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 4:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal04 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal04 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal04 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal04 = 0;
                        //        }
                        //        REC_BANK04_Field = "REC_BANK04";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK04_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK04_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK04_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK04_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK04_Visible = true;
                        //        REC_BANK04_Tag = "YES";
                        //        REC_BANK04_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK04, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK04, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK04_Field = "PAY_BANK04";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK04_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK04_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK04_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK04_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK04_Visible = true;
                        //        PAY_BANK04_Tag = "YES";
                        //        PAY_BANK04_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK04, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK04, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 5:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal05 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal05 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal05 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal05 = 0;
                        //        }
                        //        REC_BANK05_Field = "REC_BANK05";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK05_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK05_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK05_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK05_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK05_Visible = true;
                        //        REC_BANK05_Tag = "YES";
                        //        REC_BANK05_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK05, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK05, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK05_Field = "PAY_BANK05";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK05_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK05_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK05_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK05_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK05_Visible = true;
                        //        PAY_BANK05_Tag = "YES";
                        //        PAY_BANK05_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK05, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK05, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 6:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal06 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal06 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal06 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal06 = 0;
                        //        }
                        //        REC_BANK06_Field = "REC_BANK06";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK06_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK06_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK06_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK06_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK06_Visible = true;
                        //        REC_BANK06_Tag = "YES";
                        //        REC_BANK06_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK06, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK06, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK06_Field = "PAY_BANK06";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK06_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK06_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK06_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK06_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK06_Visible = true;
                        //        PAY_BANK06_Tag = "YES";
                        //        PAY_BANK06_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK06, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK06, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 7:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal07 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal07 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal07 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal07 = 0;
                        //        }
                        //        REC_BANK07_Field = "REC_BANK07";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK07_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK07_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK07_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK07_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK07_Visible = true;
                        //        REC_BANK07_Tag = "YES";
                        //        REC_BANK07_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK07, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK07, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK07_Field = "PAY_BANK07";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK07_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK07_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK07_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK07_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK07_Visible = true;
                        //        PAY_BANK07_Tag = "YES";
                        //        PAY_BANK07_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK07, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK07, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 8:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal08 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal08 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal08 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal08 = 0;
                        //        }
                        //        REC_BANK08_Field = "REC_BANK08";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK08_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK08_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK08_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK08_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK08_Visible = true;
                        //        REC_BANK08_Tag = "YES";
                        //        REC_BANK08_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK08, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK08, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK08_Field = "PAY_BANK08";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK08_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK08_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK08_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK08_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK08_Visible = true;
                        //        PAY_BANK08_Tag = "YES";
                        //        PAY_BANK08_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK08, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK08, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 9:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal09 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal09 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal09 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal09 = 0;
                        //        }
                        //        REC_BANK09_Field = "REC_BANK09";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK09_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK09_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK09_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK09_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK09_Visible = true;
                        //        REC_BANK09_Tag = "YES";
                        //        REC_BANK09_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK09, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK09, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK09_Field = "PAY_BANK09";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK09_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK09_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK09_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK09_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK09_Visible = true;
                        //        PAY_BANK09_Tag = "YES";
                        //        PAY_BANK09_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK09, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK09, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        //    case 10:
                        //        if (!Convert.IsDBNull(XROW["OPENING"]))
                        //        {
                        //            Open_Bank_Bal10 = Convert.ToDouble(XROW["OPENING"]);
                        //        }
                        //        else
                        //        {
                        //            Open_Bank_Bal10 = 0;
                        //        }
                        //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                        //        {
                        //            Close_Bank_Bal10 = Convert.ToDouble(XROW["CLOSING"]);
                        //        }
                        //        else
                        //        {
                        //            Close_Bank_Bal10 = 0;
                        //        }
                        //        REC_BANK10_Field = "REC_BANK10";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            REC_BANK10_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            REC_BANK10_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["REC_BANK10_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK10_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        REC_BANK10_Visible = true;
                        //        REC_BANK10_Tag = "YES";
                        //        REC_BANK10_Remove = false;
                        //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK10, ";
                        //        _online_BANK_COL_NB_REC += " NULL, ";
                        //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK10, ";
                        //        _local__BANK_COL_NB_REC += " NULL, ";
                        //        PAY_BANK10_Field = "PAY_BANK10";
                        //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                        //        {
                        //            PAY_BANK10_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        }
                        //        else
                        //        {
                        //            PAY_BANK10_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                        //        }
                        //        TempData["PAY_BANK10_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK10_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                        //        PAY_BANK10_Visible = true;
                        //        PAY_BANK10_Tag = "YES";
                        //        PAY_BANK10_Remove = false;
                        //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK10, ";
                        //        _online_BANK_COL_NB_PAY += " NULL, ";
                        //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK10, ";
                        //        _local__BANK_COL_NB_PAY += " NULL, ";
                        //        _colWidth += _colSetWidth;
                        //        break;
                        #endregion Commented Code
                    }
                    _bankCnt += 1;
                }
            }
            else
            {
                Open_Bank_Bal = 0; //Open_Bank_Bal01 = 0; Open_Bank_Bal02 = 0; Open_Bank_Bal03 = 0; Open_Bank_Bal04 = 0; Open_Bank_Bal05 = 0; Open_Bank_Bal06 = 0; Open_Bank_Bal07 = 0; Open_Bank_Bal08 = 0; Open_Bank_Bal09 = 0; Open_Bank_Bal10 = 0;
                Close_Bank_Bal = 0; //Close_Bank_Bal01 = 0; Close_Bank_Bal02 = 0; Close_Bank_Bal03 = 0; Close_Bank_Bal04 = 0; Close_Bank_Bal05 = 0; Close_Bank_Bal06 = 0; Close_Bank_Bal07 = 0; Close_Bank_Bal08 = 0; Close_Bank_Bal09 = 0; Close_Bank_Bal10 = 0;
            }
            iTR_PAY_JOURNAL_Remove = false;
            iTR_PAY_TOTAL_Remove = false;
            iTR_REC_JOURNAL_Remove = false;
            iTR_REC_TOTAL_Remove = false;
            BE_Cash_Bank_Text = "Cash: " + Close_Cash_Bal.ToString("#,0.00") + "  Bank: " + Close_Bank_Bal.ToString("#,0.00");
            string final_str = "";
            if (_ActiveFilterString != null && _ActiveFilterString.Length > 0 && _ActiveFilterString.Contains("[Advanced_Filter]"))
            {
                int Ind = _ActiveFilterString.IndexOf("[Advanced_Filter]");
                string sub_str1 = "";
                if (Ind > 0)
                {
                    sub_str1 = _ActiveFilterString.Substring(0, Ind - 1);
                }
                string sub_str2 = _ActiveFilterString.Substring(Ind);
                if (sub_str2.Split('=').Count() > 1)
                {
                    Advanced_Filter_Category = sub_str2.Split('=')[1].Replace("'", "").Trim();
                    Advanced_Filter_RefID = sub_str2.Split('=')[2].Replace("'", "").Trim();
                }
                final_str = sub_str1 + " [Advanced_Filter] ='" + Advanced_Filter_Category + " = " + Advanced_Filter_RefID + "'";
            }
            DataTable TR_Table = BASE._Voucher_DBOps.GetList(_xFr_Date(), _xTo_Date(), _online_BANK_COL_TR_REC, _online_BANK_COL_NB_REC, _local__BANK_COL_TR_REC, _local__BANK_COL_NB_REC, _online_BANK_COL_TR_PAY, _online_BANK_COL_NB_PAY, _local__BANK_COL_TR_PAY, _local__BANK_COL_NB_PAY, Advanced_Filter_Category, Advanced_Filter_RefID, showDynamicBankColumns_CB) as DataTable;
            if (TR_Table == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (Advanced_Filter_Category.Length > 0 && final_str.Length > 0)
            {
                ActiveFilterString = final_str;
                Advanced_Filter_Category = ""; Advanced_Filter_RefID = "";
            }
            DataSet Voucher_DS = new DataSet();
            Voucher_DS.Tables.Add(TR_Table.Copy());
            Voucher_DS.Tables.Add(Bank_Bal.Copy());
            DataRelation BANK_Relation = Voucher_DS.Relations.Add("BANK_ACC", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_SUB_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            DataRelation BANK_Relation2 = Voucher_DS.Relations.Add("BANK_ACC2", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_CR_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            count = Voucher_DS.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow XROW = Voucher_DS.Tables[0].Rows[i];
                DataRow[] bankrelation_childrows = XROW.GetChildRows(BANK_Relation);
                int bank_relationcount = bankrelation_childrows.Count();
                for (int j = 0; j < bank_relationcount; j++)
                {
                    DataRow _Row = bankrelation_childrows[j];
                    if (XROW["iTR_PARTY_1"].ToString().Length <= 0)
                    {
                        XROW["iTR_PARTY_1"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
                DataRow[] bankrelation2_childrows = XROW.GetChildRows(BANK_Relation2);
                int bank_relation2count = bankrelation2_childrows.Count();
                for (int k = 0; k < bank_relation2count; k++)
                {
                    DataRow _Row = bankrelation2_childrows[k];
                    if (XROW["iTR_CR_NAME"].ToString().Length <= 0)
                    {
                        XROW["iTR_CR_NAME"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
            }
            Voucher_DS.Relations.Clear();
            int _Date_Serial = 0;
            string _Date_Show = "";
            if (Convert.ToInt32(_xFr_Date().ToString("MM")) > 3)
            {
                _Date_Serial = Convert.ToInt32(_xFr_Date().AddMonths(-3).ToString("MM"));
                _Date_Show = BASE._open_Year_Sdt.ToString("yyyy") + "-" + string.Format(_xFr_Date().ToString("MM"), "00") + "-01";
            }
            else
            {
                _Date_Serial = Convert.ToInt32(_xFr_Date().AddMonths(+9).ToString("MM"));
                _Date_Show = BASE._open_Year_Edt.ToString("yyyy") + "-" + string.Format(_xFr_Date().ToString("MM"), "00") + "-01";
            }
            DataRow ROW = default(DataRow);
            ROW = Voucher_DS.Tables[0].NewRow();
            ROW["iTR_DATE_SERIAL"] = _Date_Serial;
            ROW["iTR_DATE_SHOW"] = _Date_Show;
            ROW["iTR_TEMP_ID"] = "OPENING BALANCE";
            ROW["iREC_ID"] = "OPENING BALANCE";
            ROW["iTR_ROW_POS"] = "A";
            ROW["iTR_VNO"] = "";
            ROW["iTR_DATE"] = string.Format(_xFr_Date().ToString(), BASE._Date_Format_Current);
            ROW["iTR_REC_CASH"] = Open_Cash_Bal;
            ROW["iTR_REC_BANK"] = Open_Bank_Bal;
            ROW["iTR_ITEM"] = "OPENING BALANCE";
            //for(int i =0; i < Bank_Bal.Rows.Count; i++)
            //{

            //    string accountNo = Bank_Bal.Rows[i]["BA_ACCOUNT_NO"].ToString();
            //    string columnName = "Dyn_Rec_" + Bank_Bal.Rows[i]["BI_SHORT_NAME"].ToString() + accountNo.Substring(accountNo.Length-4);
            //    int openingValue = Convert.ToInt32(Bank_Bal.Rows[i]["OPENING"]);

            //    ROW[columnName] = openingValue;
            //}

            //if ((string)REC_BANK01_Tag == "YES")
            //{
            //    ROW["REC_BANK01"] = Open_Bank_Bal01;
            //}
            //if ((string)REC_BANK02_Tag == "YES")
            //{
            //    ROW["REC_BANK02"] = Open_Bank_Bal02;
            //}
            //if ((string)REC_BANK03_Tag == "YES")
            //{
            //    ROW["REC_BANK03"] = Open_Bank_Bal03;
            //}
            //if ((string)REC_BANK04_Tag == "YES")
            //{
            //    ROW["REC_BANK04"] = Open_Bank_Bal04;
            //}
            //if ((string)REC_BANK05_Tag == "YES")
            //{
            //    ROW["REC_BANK05"] = Open_Bank_Bal05;
            //}
            //if ((string)REC_BANK06_Tag == "YES")
            //{
            //    ROW["REC_BANK06"] = Open_Bank_Bal06;
            //}
            //if ((string)REC_BANK07_Tag == "YES")
            //{
            //    ROW["REC_BANK07"] = Open_Bank_Bal07;
            //}
            //if ((string)REC_BANK08_Tag == "YES")
            //{
            //    ROW["REC_BANK08"] = Open_Bank_Bal08;
            //}
            //if ((string)REC_BANK09_Tag == "YES")
            //{
            //    ROW["REC_BANK09"] = Open_Bank_Bal09;
            //}
            //if ((string)REC_BANK10_Tag == "YES")
            //{
            //    ROW["REC_BANK10"] = Open_Bank_Bal10;
            //}
            Voucher_DS.Tables[0].Rows.Add(ROW);
            DataView DV1 = new DataView(Voucher_DS.Tables[0]);
            DV1.Sort = "iTR_DATE,iTR_ROW_POS,iTR_ENTRY,iREC_ADD_ON,iTR_M_ID,iTR_SORT,iTR_SR_NO";
            DataTable XTABLE = DV1.ToTable();
            string _TEMP = "";
            if (XTABLE.Rows.Count > 0)
            {
                _TEMP = DV1.ToTable().Rows[0]["iTR_TEMP_ID"].ToString();
            }
            if (XTABLE.Columns.Contains("iIcon") == false)
            {
                XTABLE.Columns.Add("iIcon", typeof(System.String));
            }
            if (XTABLE.Columns.Contains("Grid_PK") == false)
            {
                XTABLE.Columns.Add("Grid_PK", typeof(System.String));
            }
            double _SR = 1;
            _Next_Unattended_Attachment_Index = -1;
            var TotalRowCount = XTABLE.Rows.Count;
            // Code Block for Data Table
            for (int i = 0; i < TotalRowCount; i++)
            {
                DataRow Row = XTABLE.Rows[i];
                if ((string)Row["iTR_TEMP_ID"] == _TEMP)
                {
                    Row["iTR_REF_NO"] = _SR;
                }
                else
                {
                    _TEMP = Row["iTR_TEMP_ID"].ToString();
                    _SR = _SR + 1;
                    Row["iTR_REF_NO"] = _SR;
                }
                string iIcon = "";
                int COMPLETE_ATTACH_COUNT = Row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0;
                int RESPONDED_COUNT = Row.Field<Int32?>("RESPONDED_COUNT") ?? 0;
                int REQ_ATTACH_COUNT = Row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0;
                int REJECTED_COUNT = Row.Field<Int32?>("REJECTED_COUNT") ?? 0;
                int ALL_ATTACH_CNT = Row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0;
                int OTHER_ATTACH_CNT = Row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0;
                int VOUCHING_TOTAL_COUNT = Row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0;
                int VOUCHING_ACCEPTED_COUNT = Row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0;
                int VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = Row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0;
                int VOUCHING_REJECTED_COUNT = Row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0;
                int VOUCHING_PENDING_COUNT = Row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0;
                int AUDIT_TOTAL_COUNT = Row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0;
                int AUDIT_ACCEPTED_COUNT = Row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0;
                int AUDIT_ACCEPTED_WITH_REMARKS_COUNT = Row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0;
                int AUDIT_REJECTED_COUNT = Row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0;
                int AUDIT_PENDING_COUNT = Row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0;
                int IS_AUTOVOUCHING = Row.Field<Int32?>("IS_AUTOVOUCHING") ?? 0;
                int IS_CORRECTED_ENTRY = Row.Field<Int32?>("IS_CORRECTED_ENTRY") ?? 0;
                if ((COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) == 0 && REQ_ATTACH_COUNT > 0)
                {
                    iIcon += "RedShield|";
                }
                else if ((COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) >= REQ_ATTACH_COUNT && (REQ_ATTACH_COUNT > 0) && (RESPONDED_COUNT == 0))
                {
                    iIcon += "GreenShield|";
                }
                else if ((COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) > 0 && (COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) < REQ_ATTACH_COUNT)
                {
                    iIcon += "YellowShield|";
                }
                else if ((COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) >= REQ_ATTACH_COUNT && (REQ_ATTACH_COUNT > 0) && (RESPONDED_COUNT > 0))
                {
                    iIcon += "BlueShield|";
                }
                if (REJECTED_COUNT > 0)
                {
                    iIcon += "RedFlag|";
                }
                if (ALL_ATTACH_CNT > 0 && OTHER_ATTACH_CNT == 0)
                {
                    iIcon += "RequiredAttachment|";
                }
                else if (ALL_ATTACH_CNT > 0 && OTHER_ATTACH_CNT != 0)
                {
                    iIcon += "AdditionalAttachment|";
                }
                if (VOUCHING_TOTAL_COUNT == VOUCHING_ACCEPTED_COUNT && VOUCHING_ACCEPTED_WITH_REMARKS_COUNT == 0 && VOUCHING_ACCEPTED_COUNT > 0)
                { iIcon += "VouchingAccepted|"; }
                if (VOUCHING_REJECTED_COUNT > 0) { iIcon += "VouchingReject|"; }
                if (VOUCHING_TOTAL_COUNT == VOUCHING_ACCEPTED_COUNT && VOUCHING_ACCEPTED_WITH_REMARKS_COUNT > 0) { iIcon += "VouchingAcceptWithRemarks|"; }
                if (VOUCHING_PENDING_COUNT > 0 && (VOUCHING_ACCEPTED_COUNT > 0 || VOUCHING_REJECTED_COUNT > 0)) { iIcon += "VouchingPartial|"; }
                if (AUDIT_TOTAL_COUNT == AUDIT_ACCEPTED_COUNT && AUDIT_ACCEPTED_WITH_REMARKS_COUNT == 0 && AUDIT_ACCEPTED_COUNT > 0)
                { iIcon += "AuditAccepted|"; }
                if (AUDIT_REJECTED_COUNT > 0) { iIcon += "AuditReject|"; }
                if (AUDIT_TOTAL_COUNT == AUDIT_ACCEPTED_COUNT && AUDIT_ACCEPTED_WITH_REMARKS_COUNT > 0) { iIcon += "AuditAcceptWithRemarks|"; }
                if (AUDIT_PENDING_COUNT > 0 && (AUDIT_ACCEPTED_COUNT > 0 || AUDIT_REJECTED_COUNT > 0)) { iIcon += "AuditPartial|"; }
                if (IS_AUTOVOUCHING > 0) { iIcon += "AutoVouching|"; }
                if (IS_CORRECTED_ENTRY > 0) { iIcon += "CorrectedEntry|"; }
                Row["iIcon"] = iIcon;
                string Grid_PK = "";
                string iREC_ID = Row.Field<string>("iREC_ID");
                string iTR_M_ID = Row.Field<string>("iTR_M_ID");
                string iTR_ITEM_ID = Row.Field<string>("iTR_ITEM_ID");
                if (iREC_ID == "NOTE-BOOK")
                {
                    Grid_PK = (string.IsNullOrEmpty(iTR_M_ID) ? "Null" : iTR_M_ID) + (string.IsNullOrEmpty(iREC_ID) ? "Null" : iTR_ITEM_ID);
                }
                else
                {
                    Grid_PK = (string.IsNullOrEmpty(iTR_M_ID) ? "Null" : iTR_M_ID) + (string.IsNullOrEmpty(iREC_ID) ? "Null" : iREC_ID);
                }
                Row["Grid_PK"] = Grid_PK;
                if (_Next_Unattended_Attachment_Index == -1 || _Next_Unattended_Attachment_Index == -2)
                {
                    if ((REQ_ATTACH_COUNT - COMPLETE_ATTACH_COUNT - RESPONDED_COUNT) > 0)
                    {
                        if (i == XTABLE.Rows.Count - 1)
                        {
                            if (_GridFocusedOrExpandedIndex == null || _GridFocusedOrExpandedIndex < 0)
                            {
                                _Next_Unattended_Attachment_Index = i;
                            }
                            else if (i >= _GridFocusedOrExpandedIndex)
                            {
                                _Next_Unattended_Attachment_Index = i;
                            }
                            else
                            {
                                _Next_Unattended_Attachment_Index = -2;
                            }
                        }
                        else if (_TEMP != XTABLE.Rows[i + 1]["iTR_TEMP_ID"].ToString())
                        {
                            if (_GridFocusedOrExpandedIndex == null || _GridFocusedOrExpandedIndex < 0)
                            {
                                _Next_Unattended_Attachment_Index = i;
                            }
                            else if (i >= _GridFocusedOrExpandedIndex)
                            {
                                _Next_Unattended_Attachment_Index = i;
                            }
                            else
                            {
                                _Next_Unattended_Attachment_Index = -2;
                            }
                        }
                    }
                }
            }
            // Previous Code Block for List of Objects - Start
            //for (int i = 0; i < TotalRowCount; i++)
            //{
            //    DataRow Row = XTABLE.Rows[i];
            //    if ((string)Row["iTR_TEMP_ID"] == _TEMP)
            //    {
            //        Row["iTR_REF_NO"] = _SR;
            //    }
            //    else
            //    {
            //        _TEMP = Row["iTR_TEMP_ID"].ToString();
            //        _SR = _SR + 1;
            //        Row["iTR_REF_NO"] = _SR;
            //    }
            //    if (Next_Unattended_Attachment_Index == -1 || Next_Unattended_Attachment_Index == -2)
            //    {
            //        Int32 REQ_ATTACH_COUNT = (Int32)(System.DBNull.Value.Equals(Row["REQ_ATTACH_COUNT"]) ? 0 : Row["REQ_ATTACH_COUNT"]);
            //        Int32 COMPLETE_ATTACH_COUNT = (Int32)(System.DBNull.Value.Equals(Row["COMPLETE_ATTACH_COUNT"]) ? 0 : Row["COMPLETE_ATTACH_COUNT"]);
            //        Int32 RESPONDED_COUNT = (Int32)(System.DBNull.Value.Equals(Row["RESPONDED_COUNT"]) ? 0 : Row["RESPONDED_COUNT"]);
            //        if ((REQ_ATTACH_COUNT - COMPLETE_ATTACH_COUNT - RESPONDED_COUNT) > 0)
            //        {
            //            if (i == XTABLE.Rows.Count - 1)
            //            {
            //                if (_GridFocusedOrExpandedIndex == null || _GridFocusedOrExpandedIndex < 0)
            //                {
            //                    Next_Unattended_Attachment_Index = i;
            //                }
            //                else if (i >= _GridFocusedOrExpandedIndex)
            //                {
            //                    Next_Unattended_Attachment_Index = i;
            //                }
            //                else
            //                {
            //                    Next_Unattended_Attachment_Index = -2;
            //                }
            //            }
            //            else if (_TEMP != XTABLE.Rows[i + 1]["iTR_TEMP_ID"].ToString())
            //            {
            //                if (_GridFocusedOrExpandedIndex == null || _GridFocusedOrExpandedIndex < 0)
            //                {
            //                    Next_Unattended_Attachment_Index = i;
            //                }
            //                else if (i >= _GridFocusedOrExpandedIndex)
            //                {
            //                    Next_Unattended_Attachment_Index = i;
            //                }
            //                else
            //                {
            //                    Next_Unattended_Attachment_Index = -2;
            //                }
            //            }
            //        }
            //    }
            //}
            //var GridData = Helper.DatatableToModel.DataTabletoCashBook(XTABLE);
            //CB_GridData = GridData;
            // Previous Code Block for List of Objects - End

            CB_GridData_DT = XTABLE;
            // XTABLE.Dispose(); // Previous Code
            if (BASE._IsVolumeCenter)
            {
                iRef_no_Visible = true;
            }
            else
            {
                iRef_no_Visible = false;
            }
            if (Negative_MsgStr.Trim().Length > 0)
            {
                jsonParam.message = Negative_MsgStr;
                jsonParam.title = "Alert..";
                jsonParam.result = false;
                jsonParam.closeform = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            jsonParam.message = "";
            jsonParam.title = "";
            jsonParam.result = true;
            jsonParam.closeform = false;
            jsonParam.Next_Unattended_Attachment_Index = Convert.ToInt32(_Next_Unattended_Attachment_Index);
            return Json(new
            {
                jsonParam,
                BE_Cash_Bank_Text,
                HideBank_Text,
                TotalRowCount
            }, JsonRequestBehavior.AllowGet);
        }
        //public ActionResult But_HideBank_Click(string ButText)
        //{
        //    bool _CheckFlag = false;
        //    if (ButText.ToUpper() == "HIDE BANK")
        //    {
        //        //if ((string)REC_BANK01_Tag == "YES")
        //        //{
        //        //    REC_BANK01_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK02_Tag == "YES")
        //        //{
        //        //    REC_BANK02_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK03_Tag == "YES")
        //        //{
        //        //    REC_BANK03_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK04_Tag == "YES")
        //        //{
        //        //    REC_BANK04_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK05_Tag == "YES")
        //        //{
        //        //    REC_BANK05_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK06_Tag == "YES")
        //        //{
        //        //    REC_BANK06_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK07_Tag == "YES")
        //        //{
        //        //    REC_BANK07_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK08_Tag == "YES")
        //        //{
        //        //    REC_BANK08_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK09_Tag == "YES")
        //        //{
        //        //    REC_BANK09_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK10_Tag == "YES")
        //        //{
        //        //    REC_BANK10_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK01_Tag == "YES")
        //        //{
        //        //    PAY_BANK01_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK02_Tag == "YES")
        //        //{
        //        //    PAY_BANK02_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK03_Tag == "YES")
        //        //{
        //        //    PAY_BANK03_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK04_Tag == "YES")
        //        //{
        //        //    PAY_BANK04_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK05_Tag == "YES")
        //        //{
        //        //    PAY_BANK05_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK06_Tag == "YES")
        //        //{
        //        //    PAY_BANK06_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK07_Tag == "YES")
        //        //{
        //        //    PAY_BANK07_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK08_Tag == "YES")
        //        //{
        //        //    PAY_BANK08_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK09_Tag == "YES")
        //        //{
        //        //    PAY_BANK09_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK10_Tag == "YES")
        //        //{
        //        //    PAY_BANK10_Visible = false;
        //        //    _CheckFlag = true;
        //        //}
        //        if (_CheckFlag)
        //        {
        //            HideBank_Text = "Show Bank";
        //            iTR_REC_BANK_Visible = true;
        //            iTR_PAY_BANK_Visible = true;
        //        }
        //    }
        //    else
        //    {
        //        //if ((string)REC_BANK01_Tag == "YES")
        //        //{
        //        //    REC_BANK01_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK02_Tag == "YES")
        //        //{
        //        //    REC_BANK02_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK03_Tag == "YES")
        //        //{
        //        //    REC_BANK03_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK04_Tag == "YES")
        //        //{
        //        //    REC_BANK04_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK05_Tag == "YES")
        //        //{
        //        //    REC_BANK05_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK06_Tag == "YES")
        //        //{
        //        //    REC_BANK06_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK07_Tag == "YES")
        //        //{
        //        //    REC_BANK07_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK08_Tag == "YES")
        //        //{
        //        //    REC_BANK08_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK09_Tag == "YES")
        //        //{
        //        //    REC_BANK09_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)REC_BANK10_Tag == "YES")
        //        //{
        //        //    REC_BANK10_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK01_Tag == "YES")
        //        //{
        //        //    PAY_BANK01_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK02_Tag == "YES")
        //        //{
        //        //    PAY_BANK02_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK03_Tag == "YES")
        //        //{
        //        //    PAY_BANK03_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK04_Tag == "YES")
        //        //{
        //        //    PAY_BANK04_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK05_Tag == "YES")
        //        //{
        //        //    PAY_BANK05_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK06_Tag == "YES")
        //        //{
        //        //    PAY_BANK06_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK07_Tag == "YES")
        //        //{
        //        //    PAY_BANK07_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK08_Tag == "YES")
        //        //{
        //        //    PAY_BANK08_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK09_Tag == "YES")
        //        //{
        //        //    PAY_BANK09_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        //if ((string)PAY_BANK10_Tag == "YES")
        //        //{
        //        //    PAY_BANK10_Visible = true;
        //        //    _CheckFlag = true;
        //        //}
        //        if (_CheckFlag)
        //        {
        //            HideBank_Text = "Hide Bank";
        //            iTR_REC_BANK_Visible = false;
        //            iTR_PAY_BANK_Visible = false;
        //        }
        //    }
        //    return Json(new
        //    {
        //        HideBank_Text
        //    }, JsonRequestBehavior.AllowGet);
        //}
        public void But_ShowTotal_Click(string ButText)
        {
            if (ButText.ToUpper() == "SHOW TOTAL BAL.")
            {
                Summary_Column_Status = true;
            }
            else
            {
                Summary_Column_Status = false;
            }
        }
        //public ActionResult MapAttachment(string RefRecId,string AttachmentID,int Trcode,int MappingID)
        //{
        //    ClientScreen screen = GetScreen(Trcode);
        //    if (BASE._Audit_DBOps.att(RefRecId,AttachmentID, MappingID, screen))
        //    {
        //        return Json(new
        //        {
        //            result = true                
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    return Json(new
        //    {
        //       result=false,
        //       message=Messages.SomeError
        //    }, JsonRequestBehavior.AllowGet);
        //}

        [HttpGet]
        public ActionResult Frm_CB_Reason(CB_Reason model)
        {
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_CB_Reason_Post(CB_Reason model)
        {
            try
            {
                ClientScreen screen = GetScreen(model.TrCode);
                if (model.ActionMethod == "New" || model.ActionMethod == "Edit")
                {
                    if (string.IsNullOrWhiteSpace(model.Reason_CB))
                    {
                        return Json(new
                        {
                            result = false,
                            message = "Reason Not Specified.."
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                model.Reason_CB = model.Reason_CB.Replace('[', '(').Replace(']', ')').Replace("'", "`").Replace('!', '|');
                if (model.ActionMethod == "New")
                {
                    if (BASE._Audit_DBOps.DocumentAbsentReasonAdded(model.RefRecId, model.Reason_CB, model.MappingID, screen))
                    {
                        return Json(new
                        {
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (model.ActionMethod == "Edit")
                {
                    if (BASE._Audit_DBOps.DocumentAbsentReasonUpdated(model.RefRecId, model.Reason_CB, model.MappingID, screen))
                    {
                        return Json(new
                        {
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else if (model.ActionMethod == "Delete")
                {
                    if (BASE._Audit_DBOps.DocumentAbsentReasonDelete(model.RefRecId, model.MappingID))
                    {
                        return Json(new
                        {
                            result = true
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new
                        {
                            result = false,
                            message = Messages.SomeError
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(new
                {
                    result = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                string msg = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                return Json(new
                {
                    message = msg,
                    result = false
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ClientScreen GetScreen(int Trcode)
        {
            ClientScreen screen;
            switch (Trcode)
            {
                case 1:
                    screen = ClientScreen.Accounts_Voucher_CashBank;
                    break;
                case 2:
                    screen = ClientScreen.Accounts_Voucher_BankToBank;
                    break;
                case 3:
                    screen = ClientScreen.Accounts_Voucher_Payment;
                    break;
                case 4:
                    screen = ClientScreen.Accounts_Voucher_Receipt;
                    break;
                case 5:
                    screen = ClientScreen.Accounts_Voucher_Donation;
                    break;
                case 6:
                    screen = ClientScreen.Accounts_Voucher_Donation;
                    break;
                case 7:
                    screen = ClientScreen.Accounts_Voucher_Gift;
                    break;
                case 8:
                    screen = ClientScreen.Accounts_Voucher_Internal_Transfer;
                    break;
                case 9:
                    screen = ClientScreen.Accounts_Voucher_CollectionBox;
                    break;
                case 10:
                    screen = ClientScreen.Accounts_Voucher_FD;
                    break;
                case 11:
                    screen = ClientScreen.Accounts_Voucher_SaleOfAsset;
                    break;
                case 12:
                    screen = ClientScreen.Accounts_Voucher_Membership;
                    break;
                case 13:
                    screen = ClientScreen.Accounts_Voucher_Membership_Renewal;
                    break;
                case 14:
                    screen = ClientScreen.Accounts_Voucher_Journal;
                    break;
                case 15:
                    screen = ClientScreen.Accounts_Voucher_AssetTransfer;
                    break;
                case 16:
                    screen = ClientScreen.Accounts_Voucher_Membership_Conversion;
                    break;
                case 17:
                    screen = ClientScreen.Accounts_Voucher_WIP_Finalization;
                    break;
                default:
                    screen = ClientScreen.Accounts_CashBook;
                    break;
            }
            return screen;
        }
        public bool CheckDocumentStatus()
        {
            bool result = true;
            for (int i = 0; i < CashBookNestedGrid.Count; i++)
            {
                if (CashBookNestedGrid[i].Doc_Checking_Status.ToUpper() == "PENDING")
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        #endregion
        #region Change Period
        public void FillChangePeriod()
        {
            var period = new List<CB_Period>();
            int index = 0;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                CB_Period row1 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row1.Period = xMonth + "-" + BASE._open_Year_Sdt.Year;
                row1.SelectedIndex = index;
                index++;
                period.Add(row1);
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                CB_Period row2 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row2.SelectedIndex = index;
                row2.Period = xMonth + "-" + BASE._open_Year_Edt.Year;
                period.Add(row2);
                index++;
            }
            CB_Period row = new CB_Period
            {
                Period = "1st Quarter",
                SelectedIndex = index
            };
            period.Add(row);
            CB_Period row3 = new CB_Period
            {
                Period = "2nd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row3);
            CB_Period row4 = new CB_Period
            {
                Period = "3rd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row4);
            CB_Period row5 = new CB_Period
            {
                Period = "4th Quarter",
                SelectedIndex = ++index
            };
            period.Add(row5);
            CB_Period row6 = new CB_Period
            {
                Period = "1st Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row6);
            CB_Period row7 = new CB_Period
            {
                Period = "2nd Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row7);
            CB_Period row8 = new CB_Period
            {
                Period = "Nine Months",
                SelectedIndex = ++index
            };
            period.Add(row8);
            CB_Period row9 = new CB_Period
            {
                Period = "Financial Year",
                SelectedIndex = ++index
            };
            period.Add(row9);
            CB_Period row10 = new CB_Period
            {
                Period = "Specific Period",
                SelectedIndex = ++index
            };
            period.Add(row10);
            CB_PeriodSelectionData = period;
        }
        public ActionResult Fill_Change_Period_Items(DataSourceLoadOptions loadOptions)
        {
            if (CB_PeriodSelectionData == null)
            {
                return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(new List<CB_Period>(), loadOptions)), "application/json");
            }
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load((List<CB_Period>)CB_PeriodSelectionData, loadOptions)), "application/json");
        }
        public ActionResult Cmb_View_SelectedIndexChanged(int? SelectedIndex = null)
        {
            var Perioddata = (List<CB_Period>)CB_PeriodSelectionData;
            string Text = Perioddata.Where(x => x.SelectedIndex == SelectedIndex).First().Period;
            if (SelectedIndex >= 0 && SelectedIndex <= 11)
            {
                string Sel_Mon = Text.Substring(0, 3).ToUpper();
                int SEL_MM = Sel_Mon == "JAN" ? 1 : Sel_Mon == "FEB" ? 2 : Sel_Mon == "MAR" ? 3 : Sel_Mon == "APR" ? 4 : Sel_Mon == "MAY" ? 5 : Sel_Mon == "JUN" ? 6 : Sel_Mon == "JUL" ? 7 : Sel_Mon == "AUG" ? 8 : Sel_Mon == "SEP" ? 9 : Sel_Mon == "OCT" ? 10 : Sel_Mon == "NOV" ? 11 : Sel_Mon == "DEC" ? 12 : 4;
                xFr_Date = new DateTime(Convert.ToInt32(Text.Substring(4, 4)), SEL_MM, 1);
                xTo_Date = _xFr_Date().AddMonths(1).AddDays(-1);
            }
            else if (SelectedIndex == 12)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = _xFr_Date().AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 13)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 7, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 14)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 15)
            {
                xFr_Date = new DateTime(BASE._open_Year_Edt.Year, 1, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(3).AddDays(-1);
            }
            else if (SelectedIndex == 16)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(6).AddDays(-1);
            }
            else if (SelectedIndex == 17)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 10, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(6).AddDays(-1);
            }
            else if (SelectedIndex == 18)
            {
                xFr_Date = new DateTime(BASE._open_Year_Sdt.Year, 4, 1);
                xTo_Date = Convert.ToDateTime(xFr_Date).AddMonths(9).AddDays(-1);
            }
            else if (SelectedIndex == 19)
            {
                xFr_Date = BASE._open_Year_Sdt;
                xTo_Date = BASE._open_Year_Edt;
            }
            string BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");
            return Json(new
            {
                BE_View_Period,
                FromDate = _xFr_Date().ToString("MM/dd/yyyy"),
                ToDate = _xTo_Date().ToString("MM/dd/yyyy")
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult frm_Voucher_Specific_Period(bool PeriodSelectionFirstTime = false)
        {
            ViewBag.PeriodSelectionFirstTime = PeriodSelectionFirstTime;
            CB_SpeceficPeriod model = new CB_SpeceficPeriod();
            model.CB_Fromdate = _xFr_Date();
            model.CB_Todate = _xTo_Date();
            return View(model);
        }
        [HttpPost]
        public ActionResult GetSpecificPeriod(CB_SpeceficPeriod model)
        {
            if (model.CB_Todate >= model.CB_Fromdate)
            {
                xFr_Date = model.CB_Fromdate;
                xTo_Date = model.CB_Todate;
                string BE_View_Period = "Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd - MMM, yyyy");
                return Json(new
                {
                    result = true,
                    BE_View_Period,
                    FromDate = _xFr_Date().ToString("MM/dd/yyyy"),
                    ToDate = _xTo_Date().ToString("MM/dd/yyyy")
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    result = false,
                    message = "To Date Cannot Be Less From From Date..!!"
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Procedures
        //public ActionResult Get_Title_List(DataSourceLoadOptions loadOptions)
        //{
        //    DataTable d1 = BASE._Address_DBOps.GetAllMasters("Name", "ID");
        //    DataView DV1 = new DataView(d1);
        //    DV1.RowFilter = " [MASTERID]='TITLE' OR [MASTERID]='BLANK' ";
        //    DV1.Sort = "Name";
        //    var titledata = DatatableToModel.DataTabletoTitle_INFO(DV1.ToTable());
        //    return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(titledata, loadOptions)), "application/json");
        //}
        #endregion
        #region Advance filter
        public ActionResult Frm_AdvancedFilters(string _ActiveFilterString)
        {
            ActiveFilterString = _ActiveFilterString;
            AdvanceFilter model = new AdvanceFilter();

            if (_ActiveFilterString != null && _ActiveFilterString.Contains("[Advanced_Filter]"))
            {
                int Ind = _ActiveFilterString.IndexOf("[Advanced_Filter]");
                string sub_str1 = "";
                if (Ind > 0)
                {
                    sub_str1 = _ActiveFilterString.Substring(0, Ind - 1);
                }
                string sub_str2 = _ActiveFilterString.Substring(Ind);
                if (sub_str2.Split('=').Count() > 1)
                {
                    model.Advanced_Filter_Category = sub_str2.Split('=')[1].Replace("'", "").Trim();
                    model.Advanced_Filter_RefID = sub_str2.Split('=')[2].Replace("'", "").Trim();
                    model.FilterType = FType;
                }
                ActiveFilterString = sub_str1;
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Frm_AdvancedFilters(AdvanceFilter model)
        {

            if (string.IsNullOrEmpty(model.Cmb_FilterTypes_CB))
            {
                return Json(new
                {
                    Message = "Filter Type Not Selected...!",
                    Valid = false,
                    Focus = "Cmb_FilterTypes_CB"
                }, JsonRequestBehavior.AllowGet);

            }
            else if (string.IsNullOrEmpty(model.GlookUp_FilterCriteria_CB))
            {
                return Json(new
                {
                    Message = "Filter Type Not Selected...!",
                    Valid = false,
                    Focus = "GlookUp_FilterCriteria_CB"
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Advanced_Filter_Category = model.Advanced_Filter_Category;
                Advanced_Filter_RefID = model.Advanced_Filter_RefID;
                FType = model.FilterType;
                if (Advanced_Filter_Category != null && Advanced_Filter_Category.Length > 0)
                {
                    if (ActiveFilterString != null && ActiveFilterString.Length > 0)
                    {
                        ActiveFilterString = ActiveFilterString + " OR" + " [Advanced_Filter] ='" + Advanced_Filter_Category + " = " + Advanced_Filter_RefID + "'";
                    }
                    else
                    {
                        ActiveFilterString = "[Advanced_Filter] ='" + Advanced_Filter_Category + " = " + Advanced_Filter_RefID + "'";
                    }
                }
                return Json(new
                {
                    Valid = true,
                    ActiveFilterString
                }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Frm_AdvancedFilters_Lookup(string filterType, DataSourceLoadOptions loadOptions)
        {
            List<CB_AdvanceFilter> data = new List<CB_AdvanceFilter>();
            Param_GetAdvancedFilters Param = new Param_GetAdvancedFilters();
            Param.Asset_Profile = filterType;
            Param.Prev_YearID = BASE._prev_Unaudited_YearID;
            DataTable d1 = BASE._Voucher_DBOps.GetAdvancedFilters(Param);
            data = DatatableToModel.DataTableToAdvanceFilter(d1);
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }

        #endregion
        #region Frm_View_Summary

        public ActionResult Frm_View_Summary(string PopupID = "")
        {
            ViewBag.PopupID = PopupID.Length > 0 ? PopupID : "popup_frm_Frm_View_Summary";
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            double Open_Bank_Bal = 0;
            double Close_Bank_Bal = 0;
            double R_CASH = 0;
            double R_BANK = 0;
            double P_CASH = 0;
            double P_BANK = 0;

            ViewBag.SummaryPeriod = "Period Fr.: " + _xFr_Date().ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date().ToString("dd-MMM, yyyy");

            DataSet CashBank_DS = new DataSet();
            DataTable CashBank_Table = CashBank_DS.Tables.Add("Table");
            DataRow ROW = default(DataRow);
            var _with1 = CashBank_Table;
            _with1.Columns.Add("Title", Type.GetType("System.String"));
            _with1.Columns.Add("Sr", Type.GetType("System.Double"));
            _with1.Columns.Add("Description", Type.GetType("System.String"));
            _with1.Columns.Add("O_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["O_BALANCE"].Caption = "Opening Balance";
            _with1.Columns.Add("R_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["R_BALANCE"].Caption = "Total Receipt";
            _with1.Columns.Add("P_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["P_BALANCE"].Caption = "Total Payment";
            _with1.Columns.Add("C_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["C_BALANCE"].Caption = "Closing Balance";

            Open_Cash_Bal = 0;
            Close_Cash_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'>  MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Open_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                }
                else
                {
                    Open_Cash_Bal = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["RECEIPT"]))
                {
                    R_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["RECEIPT"]);
                }
                else
                {
                    R_CASH = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["PAYMENT"]))
                {
                    P_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["PAYMENT"]);
                }
                else
                {
                    P_CASH = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["CLOSING"]))
                {
                    Close_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["CLOSING"]);
                }
                else
                {
                    Close_Cash_Bal = 0;
                }
            }
            else
            {
                Open_Cash_Bal = 0;
                R_CASH = 0;
                P_CASH = 0;
                Close_Cash_Bal = 0;
            }
            ROW = CashBank_Table.NewRow();
            ROW["Title"] = "CASH";
            ROW["Sr"] = 1;
            ROW["Description"] = "CASH Summary";
            ROW["O_BALANCE"] = Open_Cash_Bal;
            ROW["R_BALANCE"] = R_CASH;
            ROW["P_BALANCE"] = P_CASH;
            ROW["C_BALANCE"] = Close_Cash_Bal;
            CashBank_Table.Rows.Add(ROW);

            //'BANK................................
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date(), _xTo_Date(), BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'>  MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            int XCNT = 2;
            if (Bank_Bal.Rows.Count > 0)
            {
                foreach (DataRow XROW in Bank_Bal.Rows)
                {
                    if (!Convert.IsDBNull(XROW["OPENING"]))
                    {
                        Open_Bank_Bal = Convert.ToDouble(XROW["OPENING"]);
                    }
                    else
                    {
                        Open_Bank_Bal += 0;
                    }
                    if (!Convert.IsDBNull(XROW["RECEIPT"]))
                    {
                        R_BANK = Convert.ToDouble(XROW["RECEIPT"]);
                    }
                    else
                    {
                        R_BANK += 0;
                    }
                    if (!Convert.IsDBNull(XROW["PAYMENT"]))
                    {
                        P_BANK = Convert.ToDouble(XROW["PAYMENT"]);
                    }
                    else
                    {
                        P_BANK += 0;
                    }
                    if (!Convert.IsDBNull(XROW["CLOSING"]))
                    {
                        Close_Bank_Bal = Convert.ToDouble(XROW["CLOSING"]);
                    }
                    else
                    {
                        Close_Bank_Bal += 0;
                    }

                    ROW = CashBank_Table.NewRow();
                    ROW["Title"] = "BANK";
                    ROW["Sr"] = XCNT;
                    ROW["Description"] = XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    ROW["O_BALANCE"] = Open_Bank_Bal;
                    ROW["R_BALANCE"] = R_BANK;
                    ROW["P_BALANCE"] = P_BANK;
                    ROW["C_BALANCE"] = Close_Bank_Bal;
                    CashBank_Table.Rows.Add(ROW);
                    XCNT += 1;
                }
            }
            var SummaryGridData = new List<Summary>();
            foreach (DataRow XROW in CashBank_Table.Rows)
            {
                var newrow = new Summary();
                newrow.Title = XROW["Title"].ToString();
                newrow.Sr = Convert.ToInt32(XROW["Sr"]);
                newrow.Description = XROW["Description"].ToString();
                newrow.O_BALANCE = Convert.ToDouble(XROW["O_BALANCE"]);
                newrow.R_BALANCE = Convert.ToDouble(XROW["R_BALANCE"]);
                newrow.P_BALANCE = Convert.ToDouble(XROW["P_BALANCE"]);
                newrow.C_BALANCE = Convert.ToDouble(XROW["C_BALANCE"]);
                SummaryGridData.Add(newrow);
            }
            CB_SummaryGridData = SummaryGridData;
            return View(SummaryGridData);
        }
        public ActionResult Frm_View_SummaryGrid()
        {
            return View(CB_SummaryGridData);
        }
        public void Frm_View_SummaryGrid_Close()
        {
            BASE._SessionDictionary.Remove("CB_SummaryGridData_CB");
        }

        #endregion
        #region  Frm_Voucher_Type
        [HttpGet]
        public ActionResult Frm_Voucher_Type()
        {
            VoucherType model = new VoucherType();
            ViewBag.AllowForeignDonation = BASE.Allow_Foreign_Donation;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Frm_Voucher_Type(VoucherType model)
        {
            return Json(new
            {
                model.GLookUp_ItemList_CBVoucher,
                model.Voucher_Type,
                model.Selection_By_Item
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Voucher_Win_D_Type(bool CallFromVoucherInfo = false)
        {
            ViewBag.CallFromVoucherInfo = CallFromVoucherInfo;
            return View();
        }
        public ActionResult LookUp_GetItemList(DataSourceLoadOptions loadOptions)
        {
            string ITEM_APPLICABLE = "";
            if (BASE.Is_HQ_Centre)
            {
                ITEM_APPLICABLE = "'GENERAL','H.Q.'";
            }
            else
            {
                ITEM_APPLICABLE = "'GENERAL','CENTRE'";
            }
            DataTable d1 = BASE._Voucher_DBOps.GetItem_LedgerListMain(BASE.Allow_Foreign_Donation, BASE.Allow_Membership, ITEM_APPLICABLE);

            DataView dview = new DataView(d1);
            dview.Sort = "ITEM_NAME";
            var data = DatatableToModel.DataTabletoVoucherTypeLookUp_GetItemList(dview.ToTable());
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(data, loadOptions)), "application/json");
        }
        #endregion
        #region DataNavigation
        public ActionResult DataNavigation(string ActionType = null, string GridPK = null,string iREC_ID="",string EditOn="",string iTR_M_ID="",string iTR_CODE="",string iTR_AB_ID_1="",string iACTION_STATUS="",string iTR_DATE="",string Voucher_Type = "", bool Selection_By_Item = false, string Selected_Item_ID = "", int FocusedRowIndex = -1)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            //var CashbookData = (List<CB_Grid_Model>)CB_GridData;
           // var CashBookRowData = CashbookData.Where(x => x.Grid_PK == GridPK).FirstOrDefault();
            if (ActionType == "UNMATCH TRANSFERS")
            {
                if ((!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock)) && !(BASE._open_Cen_ID == 4216))
                {
                    jsonParam.message = "Not Allowed!!";
                    jsonParam.title = "No Rights";
                    jsonParam.result = false;
                    jsonParam.isconfirm = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else if (ActionType == "REMARKS" || ActionType == "ADD REMARKS")
            {
                if (!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Manage_Remarks) && ActionType == "ADD REMARKS")
                {
                    jsonParam.message = "Not Allowed!!";
                    jsonParam.title = "No Rights";
                    jsonParam.result = false;
                    jsonParam.isconfirm = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.View_Remarks) && ActionType == "REMARKS")
                {
                    jsonParam.message = "Not Allowed!!";
                    jsonParam.title = "No Rights";
                    jsonParam.result = false;
                    jsonParam.isconfirm = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            if (BASE.AllowMultiuser())
            {
                if (ActionType == "MATCH TRANSFERS" || ActionType == "UNMATCH TRANSFERS" || ActionType == "VOUCHER" || ActionType == "PRINT-LIST" || ActionType == "LOCKED" || ActionType == "UNLOCKED")
                {
                    var xTemp_ID = iREC_ID;
                    if (xTemp_ID.ToLower() != "opening balance" && xTemp_ID.ToLower() != "note-book")
                    {
                        var RecEdit_Date = BASE._Voucher_DBOps.GetEditOnByRecID(xTemp_ID);
                        if (Convert.IsDBNull(RecEdit_Date) || RecEdit_Date == null)
                        {
                            jsonParam.message = Messages.RecordChanged("Current Voucher");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        if (Convert.ToDateTime(RecEdit_Date) != Convert.ToDateTime(EditOn))
                        {
                            jsonParam.message = Messages.RecordChanged("Current Voucher");
                            jsonParam.title = "Record Already Changed!!";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                }
            }              
            if (ActionType == "LOCKED")
            {
                string xTemp_ID = iTR_M_ID ?? "";
                if (xTemp_ID.Trim().Length < 36)
                {
                    xTemp_ID = iREC_ID;
                }
                if (xTemp_ID.Length < 36)
                {
                    if (xTemp_ID.ToLower() == "opening balance")
                    {
                        jsonParam.message = "Opening Balances Can Be Locked In Profile Screen Only...!<br><br>Please Unselect Opening Balance Entries ...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (xTemp_ID.ToLower() == "note-book")
                    {
                        jsonParam.message = "Notebook Entries Can Be Locked In NOTEBOOK Screen Only...!<br><br>Please Unselect NoteBook Entries ...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                object MaxValue = 0;
                DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(iREC_ID, xTemp_ID);
                if (Status.Rows.Count > 0)
                {
                    MaxValue = Status.Rows[0]["REC_STATUS"];
                }
                var xRemarks = BASE._Action_Items_DBOps.GetRemarksStatus(Common_Lib.RealTimeService.Tables.TRANSACTION_INFO, xTemp_ID);

                if ((int)MaxValue == (int)Common.Record_Status._Locked)
                {
                    jsonParam.message = "Already Locked Entries Cannot Be Re-Locked...!<br><br>Please Unselect Already Locked Entries ...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                if ((int)MaxValue == (int)Common.Record_Status._Incomplete)
                {
                    jsonParam.message = "Incomplete Entries Cannot Be Locked...!<br><br>Please Unselect InComplete Entries ...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                if (xRemarks != null && !Convert.IsDBNull(xRemarks))
                {
                    if ((int)MaxValue > 0)
                    {
                        jsonParam.message = "Entries With Pending Queries Can't Be Locked...!<br><br>Please Unselect Such Entries...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                xTemp_ID = iTR_M_ID ?? "";
                if (xTemp_ID.Length == 36)
                {
                    if (!BASE._Voucher_DBOps.MarkAsLockedByMasterID(xTemp_ID))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    xTemp_ID = iREC_ID;
                    if (!BASE._Voucher_DBOps.MarkAsLocked(xTemp_ID))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                //xID = iREC_ID;
                jsonParam.message = Messages.LockedSuccess(1);
                jsonParam.title = "Locked...";
                jsonParam.result = true;
                jsonParam.refreshgrid = true;
                return Json(new
                {
                    jsonParam,

                }, JsonRequestBehavior.AllowGet);
            }
            else if (ActionType == "UNLOCKED")
            {
                string xTemp_ID =iTR_M_ID ?? "";
                if (xTemp_ID.Trim().Length < 36)
                {
                    xTemp_ID = iREC_ID;
                }
                if (xTemp_ID.Length == 36)
                {
                    if (xTemp_ID.ToLower() == "opening balance")
                    {
                        jsonParam.message = "Opening Balances Can Be Unlocked In Profile Screen Only...!<br><br>Please Unselect Opening Balance Entries ...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (xTemp_ID.ToLower() == "note-book")
                    {
                        jsonParam.message = "Notebook Entries Can Be Unlocked In NOTEBOOK Screen Only...!<br><br>Please Unselect NoteBook Entries ...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                object MaxValue = 0;
                DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(iREC_ID, xTemp_ID);
                if (Status.Rows.Count > 0)
                {
                    MaxValue = Status.Rows[0]["REC_STATUS"];
                }
                if ((int)MaxValue == (int)Common.Record_Status._Completed)
                {
                    jsonParam.message = "Already Unlocked Entries Cannot Be Re-UnLocked...!<br><br>Please Unselect Already Unlocked Entries ...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                if ((int)MaxValue == (int)Common.Record_Status._Incomplete)
                {
                    jsonParam.message = "Incomplete Entries Cannot Be UnLocked...!<br><br>Please Unselect InComplete Entries ...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                xTemp_ID = iTR_M_ID ?? "";
                if (xTemp_ID.Length == 36)
                {
                    if (!BASE._Voucher_DBOps.MarkAsCompleteByMasterID(xTemp_ID))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    xTemp_ID = iREC_ID;
                    if (!BASE._Voucher_DBOps.MarkAsComplete(xTemp_ID))
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                //xID =iREC_ID;
                jsonParam.message = Messages.UnlockedSuccess(1);
                jsonParam.title = "Locked...";
                jsonParam.result = true;
                jsonParam.refreshgrid = true;
                return Json(new
                {
                    jsonParam,

                }, JsonRequestBehavior.AllowGet);


            }
            else if (ActionType == "MATCH TRANSFERS")
            {
                int xVCode =string.IsNullOrWhiteSpace(iTR_CODE)?0:Convert.ToInt32(iTR_CODE);
                string xTemp_ID = iREC_ID ?? "";
                string xParty_ID = iTR_AB_ID_1 ?? "";
                string xTemp_MID = iTR_M_ID ?? "";
                bool isRecChanged = false;
                DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID);
                if (Status == null)
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                if (Status.Rows.Count > 0)
                {
                    if ((int)Common.Record_Status._Locked == (int)Status.Rows[0]["REC_STATUS"])
                    {
                        jsonParam.message = "Locked Entry Cannot Be Matched...<br><br>Note:<br>---------<br>Drop Your Request To Madhuban To Unlock This Entry,<br> If You Really Want To Do Some Action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    jsonParam.message = "Entry Not Found...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = true;
                    return Json(new
                    {
                        jsonParam,
                    }, JsonRequestBehavior.AllowGet);
                }
                if (xVCode == (int)Common.Voucher_Screen_Code.Internal_Transfer)
                {
                    DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1);
                    if (Convert.ToDateTime(EditOn) != (DateTime)d1.Rows[0]["REC_EDIT_ON"])
                    {
                        isRecChanged = true;
                    }
                    if (Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                    {
                        if (isRecChanged)
                        {
                            jsonParam.message = "Record Has Been Unmatched In The BackGround...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam,

                            }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    jsonParam.result = true;
                    jsonParam.popup_title = "Match Internal Transfer";
                    jsonParam.popup_form_name = "Frm_I_Transfer_Matching";
                    jsonParam.popup_form_path = "/Account/InternalTransfer/Frm_I_Transfer_Matching/";
                    jsonParam.popup_querystring = "to_Match_Txn_ID=" + xTemp_ID + "&to_Cen_Rec_ID=" + BASE._Internal_Tf_Voucher_DBOps.GetCenterID(xParty_ID).ToString();
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }

            }
            else if (ActionType == "UNMATCH TRANSFERS")
            {
                int xVCode = string.IsNullOrWhiteSpace(iTR_CODE)?0:Convert.ToInt32(iTR_CODE);
                string xTemp_ID = iREC_ID ?? "";
                string xTemp_MID = iTR_M_ID ?? "";
                var xTr_Date = iTR_DATE;
                if (xVCode != (int)Common.Voucher_Screen_Code.Internal_Transfer)
                {
                    jsonParam.message = "PLease Select Internal Transfer Voucher Entries...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID);
                if (Status == null)
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                if (Status.Rows.Count > 0)
                {
                    if ((int)Common.Record_Status._Locked == (int)Status.Rows[0]["REC_STATUS"])
                    {
                        jsonParam.message = "Locked Entry Cannot Be UnMatched...<br><br>Note:<br>---------<br>Drop Your Request To Madhuban To Unlock This Entry,<br> If You Really Want To Do Some Action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    jsonParam.message = "Entry Not Found...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = true;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                string multiUserMsg = "";
                bool isRecChanged = false;
                DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1);
                if (Convert.ToDateTime(EditOn) != (DateTime)d1.Rows[0]["REC_EDIT_ON"])
                {
                    isRecChanged = true;
                }
                if (Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]) || d1.Rows[0]["TR_TRF_CROSS_REF_ID"] == null)
                {
                    multiUserMsg = "<br><br>Record Has Already Been Unmatched In The Background";
                    jsonParam.message = "Selected Record Is Already Unmatched...!" + multiUserMsg;
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = isRecChanged;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    if (isRecChanged)
                    {
                        jsonParam.message = "Transfer Voucher Matched In The Background...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = isRecChanged;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                Status = BASE._Voucher_DBOps.GetStatus_TrCode_OtherCentre((string)d1.Rows[0]["TR_TRF_CROSS_REF_ID"]);
                if (Status == null)
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (Status.Rows.Count > 0)
                {
                    if ((int)Common.Record_Status._Locked == (int)Status.Rows[0]["REC_STATUS"])
                    {
                        jsonParam.message = "Entry Matched With This Record Is Locked...<br><br>Note:<br>---------<br>Drop Your Request To Madhuban To Unlock This Entry,<br> If You Really Want To Do Some Action...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    jsonParam.message = "Entry Not Found...!";
                    jsonParam.title = "Information...";
                    jsonParam.result = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam,

                    }, JsonRequestBehavior.AllowGet);
                }
                if (xVCode == (int)Common.Voucher_Screen_Code.Internal_Transfer)
                {
                    if (Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                    {
                        jsonParam.message = "Transfer Voucher Already Unmatched...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (BASE._Internal_Tf_Voucher_DBOps.UnMatchTransfers(xTemp_ID, (string)d1.Rows[0]["TR_TRF_CROSS_REF_ID"], Convert.ToDateTime(xTr_Date)))
                    {
                        jsonParam.message = "Transfer Entry Unmatched Successfully...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = true;
                        jsonParam.refreshgrid = true;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jsonParam.message = "Sorry! Transfer Entry Could Not Be Unmatched Successfully...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam,

                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                jsonParam.result = true;
                jsonParam.refreshgrid = false;
                return Json(new
                {
                    jsonParam,

                }, JsonRequestBehavior.AllowGet);
            }          
            else if (ActionType == "REMARKS")
            {
                string xTemp_ID = iTR_M_ID ?? "";
                if (xTemp_ID.Length < 36)
                {
                    xTemp_ID = iREC_ID;
                }
                if (xTemp_ID.Length < 36)
                {
                    if (xTemp_ID.ToLower() == "opening balance")
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Remarks for Opening Balances can be viewed in profile screens only...!<br><br>Please Unselect Opening Balance Entry...!";
                        jsonParam.title = "Information..";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (xTemp_ID.ToLower() == "note-book")
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Remarks for Notebook Entries can be viewed in Notebook screens only...!<br><br>Please Unselect Notebook Entry...!";
                        jsonParam.title = "Information..";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                string xStatus = iACTION_STATUS;
                //xID = iREC_ID;

                jsonParam.result = true;
                jsonParam.popup_title = "Audit Actions";
                jsonParam.popup_form_name = "Frm_Action_Items_Info";
                jsonParam.popup_form_path = "/Help/ActionItems/Frm_Action_Items_Info/";
                jsonParam.popup_querystring = "RefScreen=" + "ACCOUNTS_VOUCHERS" + "&RefTable=" + "TRANSACTION_INFO" + "&RefRecID=" + xTemp_ID + "&Status=" + xStatus + "&PopupID=" + "CB_ActionItem_Popup";
                //xID = xTemp_ID;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            else if (ActionType == "ADD REMARKS")
            {
                string xTemp_ID = iTR_M_ID ?? "";
                if (xTemp_ID.Length < 36)
                {
                    xTemp_ID = iREC_ID;
                }
                if (xTemp_ID.Length < 36)
                {
                    if (xTemp_ID.ToLower() == "opening balance")
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Remarks for Opening Balances can be viewed in profile screens only...!<br><br>Please Unselect Opening Balance Entry...!";
                        jsonParam.title = "Information..";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (xTemp_ID.ToLower() == "note-book")
                    {
                        jsonParam.result = false;
                        jsonParam.message = "Remarks for Notebook Entries can be viewed in Notebook screens only...!<br><br>Please Unselect Notebook Entry...!";
                        jsonParam.title = "Information..";
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
                string xStatus = iACTION_STATUS;
                //xID = iREC_ID;
                if (xStatus == "LOCKED")
                {
                    jsonParam.result = false;
                    jsonParam.message = "Queries Cannot Be Added To Freezed Entry...!";
                    jsonParam.title = "Information..";
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonParam.result = true;
                    jsonParam.popup_title = "New ~ Action";
                    jsonParam.popup_form_name = "Frm_Action_Items_Window";
                    jsonParam.popup_form_path = "/Help/ActionItems/Frm_Action_Items_Window/";
                    jsonParam.popup_querystring = "ActionMethod=New" + "&RefScreen=" + "ACCOUNTS_VOUCHERS" + "&RefTable=" + "TRANSACTION_INFO" + "&RefRecID=" + xTemp_ID + "&PopupID=" + "CB_ActionItem_Popup" + "&GridToBeRefreshed=" + "CashBookListGrid";
                    //xID = xTemp_ID;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserConfirmation(string ActionType = null,string iREC_ID="",string iTR_M_ID="",string iACTION_STATUS="",string EditOn="",string iTR_CODE="",  string GridPK = null)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();         
            //var CashBookRowData = ((List<CB_Grid_Model>)CB_GridData).Where(x => x.Grid_PK == GridPK).FirstOrDefault();
            if (ActionType == "EDIT")
            {
                string xTemp_ID = iREC_ID ?? "";
                string xTemp_MID = iTR_M_ID ?? "";
                if (xTemp_ID.Length > 0 && xTemp_ID != "NOTE-BOOK" && xTemp_ID != "OPENING BALANCE")
                {
                    string xRec_Status = "";
                    string multiUserMsg = "";
                    bool AllowUser = false;
                    DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID);
                    if (Status == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Status.Rows.Count > 0)
                    {
                        xRec_Status = Status.Rows[0]["REC_STATUS"].ToString();
                        string xStatus = iACTION_STATUS;
                        var value = ((int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus));
                        if (value != Convert.ToInt32(xRec_Status))
                        {
                            if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Locked)
                            {
                                multiUserMsg = "The Record Has Been Locked In The Background By Another User.";
                            }
                            else if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Completed)
                            {
                                multiUserMsg = "The Record Has Been Unlocked In The Background By Another User.";
                                AllowUser = true;
                            }
                            else
                            {
                                multiUserMsg = "The Record Has Been Changed In The Background By Another User.";
                                AllowUser = true;
                            }
                            if (AllowUser)
                            {
                                jsonParam.message = multiUserMsg + "<br><br>Do You Want To Continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.isconfirm = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            else if (ActionType == "DELETE")
            {
                string xTemp_ID = iREC_ID ?? "";
                string xTemp_MID = iTR_M_ID ?? "";
                if (xTemp_ID.Length > 0 && xTemp_ID != "NOTE-BOOK" && xTemp_ID != "OPENING BALANCE")
                {
                    string xRec_Status = "";
                    string multiUserMsg = "";
                    bool AllowUser = false;
                    DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID);
                    if (Status == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Status.Rows.Count > 0)
                    {
                        xRec_Status = Status.Rows[0]["REC_STATUS"].ToString();
                        string xStatus = iACTION_STATUS;
                        var value = ((int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus));
                        if (value != Convert.ToInt32(xRec_Status))
                        {
                            if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Locked)
                            {
                                multiUserMsg = "The Record Has Been Locked In The Background By Another User.";
                            }
                            else if (Convert.ToInt32(xRec_Status) == (int)Common.Record_Status._Completed)
                            {
                                multiUserMsg = "The Record Has Been Unlocked In The Background By Another User.";
                                AllowUser = true;
                            }
                            else
                            {
                                multiUserMsg = "The Record Has Been Changed In The Background By Another User.";
                                AllowUser = true;
                            }
                            if (AllowUser)
                            {
                                jsonParam.message = multiUserMsg + "<br><br>Do You Want To Continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.isconfirm = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            else if (ActionType == "MATCH TRANSFERS")
            {
                if ((!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock)) && !(BASE._open_Cen_ID == 4216))
                {
                    jsonParam.message = "Not Allowed!!";
                    jsonParam.title = "No Rights";
                    jsonParam.result = false;
                    jsonParam.isconfirm = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    int xVCode = string.IsNullOrWhiteSpace(iTR_CODE)?0:Convert.ToInt32(iTR_CODE);
                    string xTemp_ID = iREC_ID;
                    string xTemp_MID = iTR_M_ID;
                    if (xVCode != (int)Common.Voucher_Screen_Code.Internal_Transfer)
                    {
                        jsonParam.message = "PLease Select Internal Transfer Voucher Entries...!";
                        jsonParam.title = "Information...";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    bool isRecChanged = false;
                    DataTable d1 = BASE._Internal_Tf_Voucher_DBOps.GetRecord(xTemp_MID, 1);                    
                    if (Convert.ToDateTime(EditOn) != (DateTime)d1.Rows[0]["REC_EDIT_ON"])
                    {
                        isRecChanged = true;
                    }
                    if (!Convert.IsDBNull(d1.Rows[0]["TR_TRF_CROSS_REF_ID"]))
                    {
                        if (isRecChanged)
                        {
                            jsonParam.message = "Record Has Already Been matched In the Background...!";
                            jsonParam.title = "Information...";
                            jsonParam.result = false;
                            jsonParam.refreshgrid = true;
                            return Json(new
                            {
                                jsonParam
                            }, JsonRequestBehavior.AllowGet);
                        }
                        jsonParam.message = "Transfer Voucher Already Matched...!<br><br>Do You Want To Rematch the Transfer...?";
                        jsonParam.title = "Confirmation...";
                        jsonParam.result = false;
                        jsonParam.isconfirm = true;
                        jsonParam.refreshgrid = isRecChanged;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            else if (ActionType == "LOCKED")
            {
                if ((!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock)) && !(BASE._open_Cen_ID == 4216))
                {
                    jsonParam.message = "Not Allowed!!";
                    jsonParam.title = "No Rights";
                    jsonParam.result = false;
                    jsonParam.isconfirm = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string xTemp_ID = iTR_M_ID ?? "";
                    if (xTemp_ID.Trim().Length < 36)
                    {
                        xTemp_ID = iREC_ID;
                    }
                    object MaxValue = 0;
                    string Msg = "";
                    bool AllowUser = false;
                    DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(iREC_ID, xTemp_ID);
                    if (Status == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam
                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Status.Rows.Count > 0)
                    {
                        MaxValue = Status.Rows[0]["REC_STATUS"];
                        string xStatus = iACTION_STATUS;
                        var value = (int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                        if (value != (int)MaxValue)
                        {
                            Msg = "Record Status Has Been Changed In The Background By Another User";
                            if ((int)MaxValue == (int)Common.Record_Status._Completed)
                            {
                                AllowUser = true;
                            }
                            if (AllowUser)
                            {
                                jsonParam.message = "The Record Has Been UnLocked In The Background By Another User<br><br>Do You Want To Continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.isconfirm = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }
            else if (ActionType == "UNLOCKED")
            {
                if ((!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock)) && !(BASE._open_Cen_ID == 4216))
                {
                    jsonParam.message = "Not Allowed!!";
                    jsonParam.title = "No Rights";
                    jsonParam.result = false;
                    jsonParam.isconfirm = false;
                    jsonParam.refreshgrid = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    string xTemp_ID = iTR_M_ID ?? "";
                    if (xTemp_ID.Trim().Length < 36)
                    {
                        xTemp_ID = iREC_ID;
                    }
                    object MaxValue = 0;
                    string Msg = "";
                    bool AllowUser = false;
                    DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(iREC_ID, xTemp_ID);
                    if (Status == null)
                    {
                        jsonParam.message = Messages.SomeError;
                        jsonParam.title = "Error!!";
                        jsonParam.result = false;
                        jsonParam.refreshgrid = false;
                        return Json(new
                        {
                            jsonParam

                        }, JsonRequestBehavior.AllowGet);
                    }
                    if (Status.Rows.Count > 0)
                    {
                        MaxValue = Status.Rows[0]["REC_STATUS"];
                        string xStatus = iACTION_STATUS;
                        var value = (int)Enum.Parse(typeof(Common.Record_Status), "_" + xStatus);
                        if (value != (int)MaxValue)
                        {
                            Msg = "Record Status Has Been Changed In The Background By Another User";
                            if ((int)MaxValue == (int)Common.Record_Status._Locked)
                            {
                                AllowUser = true;
                            }
                            if (AllowUser)
                            {
                                jsonParam.message = "The Record Has Been Locked In The Background By Another User<br><br>Do You Want To Continue...?";
                                jsonParam.title = "Confirmation...";
                                jsonParam.result = false;
                                jsonParam.isconfirm = true;
                                jsonParam.refreshgrid = true;
                                return Json(new
                                {
                                    jsonParam
                                }, JsonRequestBehavior.AllowGet);
                            }
                        }
                    }
                }
            }

            jsonParam.result = true;
            jsonParam.refreshgrid = false;
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAssetTransferDeleteConfirmation(string GridPK = null, int FocusedRowIndex = -1)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();

            // Previous Code for List Type Session variable
            //var CashbookData = (List<CB_Grid_Model>)CB_GridData;
            //var CashBookRowData = CashbookData.Where(x => x.Grid_PK == GridPK).FirstOrDefault();
            //string _xTemp_ID = CashBookRowData.iREC_ID ?? "";
            //string _xTemp_MID = CashBookRowData.iTR_M_ID ?? "";
            // Previous Code for List Type Session variable

            DataRow dr_CashBookRowData = CB_GridData_DT.AsEnumerable().Where(x => x.Field<string>("Grid_PK") == GridPK).FirstOrDefault();
            string xTemp_ID = Convert.IsDBNull(dr_CashBookRowData["iREC_ID"]) ? "" : dr_CashBookRowData["iREC_ID"].ToString();
            string xTemp_MID = Convert.IsDBNull(dr_CashBookRowData["iTR_M_ID"]) ? "" : dr_CashBookRowData["iTR_M_ID"].ToString();
            int iTR_SR_NO = Convert.IsDBNull(dr_CashBookRowData["iTR_SR_NO"]) ? 0 : Convert.ToInt32(dr_CashBookRowData["iTR_M_ID"]);

            string xCross_Ref_Id = "";
            DataTable Status = BASE._Voucher_DBOps.GetStatus_TrCode(xTemp_ID, xTemp_MID);
            if (Status.Rows.Count > 0)
            {
                foreach (DataRow cRow in Status.Rows)
                {
                    if (!Convert.IsDBNull(cRow["TR_TRF_CROSS_REF_ID"]))
                    {
                        xCross_Ref_Id = (string)cRow["TR_TRF_CROSS_REF_ID"];
                    }
                    if (xCross_Ref_Id.Length > 0)
                    {
                        break;
                    }
                }
            }
            int _RowHandle = 0;
            if (iTR_SR_NO == 2)
            {
                _RowHandle = 1;
            }
            if (FocusedRowIndex - _RowHandle >= 0 && CB_GridData_DT.Rows[FocusedRowIndex - _RowHandle]["iTR_TYPE"].ToString() == "CREDIT" && !string.IsNullOrEmpty(xCross_Ref_Id))
            {
                jsonParam.message = "<b>Sure you want to <font color='red'><u>Delete</u></font> Matched Asset Transfer...?</b>";
                jsonParam.isconfirm = true;
                jsonParam.result = false;
                jsonParam.title = "Cash Book";
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }

            // Previous Code for List Type Session variable
            //if (FocusedRowIndex - _RowHandle >= 0 && CashbookData[FocusedRowIndex - _RowHandle].iTR_TYPE == "CREDIT" && !string.IsNullOrEmpty(xCross_Ref_Id))
            //{
            //    if (xCross_Ref_Id.Length > 0)
            //    {
            //        jsonParam.message = "<b>Sure you want to <font color='red'><u>Delete</u></font> Matched Asset Transfer...?</b>";
            //        jsonParam.isconfirm = true;
            //        jsonParam.result = false;
            //        jsonParam.title = "Cash Book";
            //        return Json(new
            //        {
            //            jsonParam
            //        }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            jsonParam.isconfirm = false;
            jsonParam.result = true;
            return Json(new
            {
                jsonParam
            }, JsonRequestBehavior.AllowGet);
        }
        public bool Get_Closed_Bank_Status(string xRecID)
        {
            bool Flag = false;
            string CR_LED_ID = "";
            string DR_LED_ID = "";
            string xTR_MODE = "";
            int xTR_CODE = 0;

            DataTable d4 = BASE._Voucher_DBOps.GetTransactionDetail(xRecID);
            if (d4.Rows.Count > 0)
            {
                foreach (DataRow xRow in d4.Rows)
                {
                    if (!Convert.IsDBNull(xRow["TR_SUB_CR_LED_ID"]))
                    {
                        CR_LED_ID = xRow["TR_SUB_CR_LED_ID"].ToString();
                    }
                    else
                    {
                        CR_LED_ID = "";
                    }
                    if (!Convert.IsDBNull(xRow["TR_SUB_DR_LED_ID"]))
                    {
                        DR_LED_ID = xRow["TR_SUB_DR_LED_ID"].ToString();
                    }
                    else
                    {
                        DR_LED_ID = "";
                    }
                    if (!Convert.IsDBNull(xRow["TR_CODE"]))
                    {
                        xTR_CODE = Convert.ToInt32(xRow["TR_CODE"]);
                    }
                    else
                    {
                        xTR_CODE = 0;
                    }
                    if (!Convert.IsDBNull(xRow["TR_MODE"]))
                    {
                        xTR_MODE = xRow["TR_MODE"].ToString();
                    }
                    else
                    {
                        xTR_MODE = "";
                    }
                    if (xTR_CODE == 6 || xTR_CODE == 1 || xTR_MODE.ToUpper() != "CASH")
                    {
                        object MaxValue = null;
                        MaxValue = BASE._Voucher_DBOps.GetBankAccount(CR_LED_ID, DR_LED_ID).ToString();
                        if (Convert.IsDBNull(MaxValue) || string.IsNullOrEmpty((string)MaxValue))
                        {
                            Flag = false;
                            //Closed_Bank_Account_No = "";
                        }
                        else
                        {
                            Flag = true;
                            //Closed_Bank_Account_No = (string)MaxValue;
                            break;
                        }
                    }
                }
            }
            return Flag;
        }

        #endregion
        #region Report
        public ActionResult Frm_Export_Options()
        {
            if (!(CheckRights(BASE, Common_Lib.RealTimeService.ClientScreen.Accounts_CashBook, "Export")))
            {
                return Content("<script language='javascript' type='text/javascript'>MultiUserPrevention('report_modal','Not Allowed','No Rights');</script>");//Code written for User Authorization do not remove                
            }
            ViewBag.Filename = "CashBook_" + BASE._open_UID_No + "_" + BASE._open_Year_ID.ToString();
            return PartialView();
        }
        [HttpPost]
        public ActionResult ReportViewerPartial(bool isFromReportViewer = false)
        {
            if (!isFromReportViewer)
            {
                Session["CBReport"] = null;
                Session["CBReport"] = CreateReport();
            }
            return PartialView();
        }
        public ActionResult ReportViewerExportTo()
        {
            return ReportViewerExtension.ExportTo((XtraReport)Session["CBReport"]);
        }
        public ActionResult GridViewExportTo()
        {
            return GridViewExtension.ExportToPdf(GridViewExportHelper.ExportCBGrid, CB_GridData_DT);
        }
        public XtraReport CreateReport()
        {
            PrintingSystem ps = new PrintingSystem();
            var xTopLeft_Header = "Cash Book";
            var xTopCentre_Header = "UID: " + BASE._open_UID_No;
            var xTopRight_Header = "Year: " + BASE._open_Year_Name;

            var xBottomLeft_Footer = "Page [Page # of Pages #]";
            var xBottomRight_Footer = "Print On: [Date Printed], [Time Printed]";
            var xBottomCentre_Footer = "";

            PrintableComponentLink link1 = new PrintableComponentLink(ps);
            link1.Component = GridViewExtension.CreatePrintableObject(GridViewExportHelper.ExportCBGrid, CB_GridData_DT);
            link1.Landscape = true;
            //link1.PaperKind = System.Drawing.Printing.PaperKind.A4Rotated;
            link1.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link1.Margins = new System.Drawing.Printing.Margins(40, 40, 50, 40);


            link1.PrintingSystem.Document.Name = "Cash book";
            DevExpress.XtraPrinting.PageHeaderArea headerArea = new DevExpress.XtraPrinting.PageHeaderArea();
            headerArea.Font = new Font("Arial", 14, FontStyle.Regular);
            headerArea.LineAlignment = BrickAlignment.None;
            headerArea.Content.Add(xTopLeft_Header);
            headerArea.Content.Add(xTopCentre_Header);
            headerArea.Content.Add(xTopRight_Header);
            DevExpress.XtraPrinting.PageFooterArea footerArea = new DevExpress.XtraPrinting.PageFooterArea();
            footerArea.Font = new Font("Verdana", 8, FontStyle.Regular);
            footerArea.LineAlignment = BrickAlignment.Far;
            footerArea.Content.Add(xBottomLeft_Footer);
            footerArea.Content.Add(xBottomCentre_Footer);
            footerArea.Content.Add(xBottomRight_Footer);
            DevExpress.XtraPrinting.PageHeaderFooter HeaderFooter = new DevExpress.XtraPrinting.PageHeaderFooter(headerArea, footerArea);
            link1.PageHeaderFooter = HeaderFooter;

            link1.PrintingSystem.ExecCommand(PrintingSystemCommand.HandTool, new object[] {
                    true});
            link1.CreateDocument();

            link1.PrintingSystem.Document.AutoFitToPagesWidth = 1;
            link1.PrintingSystem.ExecCommand(PrintingSystemCommand.ZoomToTextWidth);

            MemoryStream stream = new MemoryStream();
            link1.PrintingSystem.SaveDocument(stream);

            XtraReport report = new XtraReport();
            /**/
            report.DisplayName = "Cash Book";
            /**/

            report.PrintingSystem.LoadDocument(stream);
            return report;
        }

        #endregion
        #region Devextreme
        public ActionResult Frm_Voucher_Info_CB_dx(string PopupID = "", string AttachmentID = "", string Period = "", string FromDate = "", string ToDate = "")
        {
            if (!(CheckRights(BASE, ClientScreen.Accounts_CashBook, "List")))
            {
                return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Not Allowed','No Rights');btnCloseActiveTab();</script>");//Code written for User Authorization do not remove                
            }
            //ResetSaticVariable();
            CashBook_Add_User_Rights();
            CashBook_Other_User_Rights(); 
            if (!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Manage_Remarks))
            {
                ViewData["CB_AddRemarks_Visible"] = false;
            }
            else
            {
                ViewData["CB_AddRemarks_Visible"] = true;
            }
            if (!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.View_Remarks))
            {
                ViewData["CB_ViewRemarks_Visible"] = false;
            }
            else
            {
                ViewData["CB_ViewRemarks_Visible"] = true;
            }
            if ((!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock)) && !(BASE._open_Cen_ID == 4216))
            {
                ViewData["CB_Locked_Visible"] = false;
                ViewData["CB_UnLocked_Visible"] = false;
                ViewData["CB_MatchTransfer_Visible"] = false;
                ViewData["CB_UnMatchTransfer_Visible"] = false;
            }
            else
            {
                ViewData["CB_Locked_Visible"] = true;
                ViewData["CB_UnLocked_Visible"] = true;
                ViewData["CB_MatchTransfer_Visible"] = true;
                ViewData["CB_UnMatchTransfer_Visible"] = true;
            }
            ViewBag.PopupID = PopupID;
            ViewBag.LinkAttachmentID = AttachmentID;
            ViewBag.UserType = BASE._open_User_Type;
            ViewBag.self_posted = BASE._open_User_Self_Posted;
            ViewBag.CB_ShowHorizontalBar = BASE._List_Of_FullData_Screen.Any(s => s.Equals((ClientScreen.Accounts_CashBook).ToString(), StringComparison.OrdinalIgnoreCase)) ? 1 : 0;
            ViewBag.xFr_Date = FromDate;
            ViewBag.xTo_Date = ToDate;
            ViewBag.OpenYearSdt = BASE._open_Year_Sdt;
            ViewBag.OpenYearEdt = BASE._open_Year_Edt;      
            ViewBag._IsVolumeCenter = BASE._IsVolumeCenter;      
            Create_Bank_Columns();         
            object MaxValue = 0;
            DateTime xLastDate = DateTime.Now;
            if (string.IsNullOrWhiteSpace(Period))
            {
                MaxValue = BASE._Voucher_DBOps.GetMaxTransactionDate();
                if (MaxValue == null)
                {
                    return Content("<script language='javascript' type='text/javascript'>DevExpress.ui.dialog.alert('Some Error Happened During The Current Operation!!','Error!!'); btnCloseActiveTab();</script>");
                }
                if (Convert.IsDBNull(MaxValue))
                {
                    xLastDate = BASE._open_Year_Sdt;
                }
                else
                {
                    xLastDate = Convert.ToDateTime(MaxValue);
                }

                int xMM = xLastDate.Month;
                ViewBag.PeriodSelectedIndex = xMM == 4 ? 0 : xMM == 5 ? 1 : xMM == 6 ? 2 : xMM == 7 ? 3 : xMM == 8 ? 4 : xMM == 9 ? 5 : xMM == 10 ? 6 : xMM == 11 ? 7 : xMM == 12 ? 8 : xMM == 1 ? 9 : xMM == 2 ? 10 : xMM == 3 ? 11 : 0;
            }
            else
            {
                ViewBag.PeriodSelectedIndex = Convert.ToInt32(Period);
            }            
            return View();
        }
        public ActionResult Grid_Display_dx(string xFrDate = "", string xToDate = "", string _ActiveFilterString = "", int? _GridFocusedOrExpandedIndex = null, bool showDynamicBankColumns = false)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            showDynamicBankColumns_CB = showDynamicBankColumns;
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            DateTime _xFr_Date = Convert.ToDateTime(xFrDate);
            DateTime _xTo_Date = Convert.ToDateTime(xToDate);
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date, _xTo_Date, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = true;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            if (Cash_Bal.Rows.Count > 0 && BASE._open_User_Self_Posted == false)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Open_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                }
                else
                {
                    Open_Cash_Bal = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["CLOSING"]))
                {
                    Close_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["CLOSING"]);
                }
                else
                {
                    Close_Cash_Bal = 0;
                }
            }
            else
            {
                Open_Cash_Bal = 0;
                Close_Cash_Bal = 0;
            }
            double Open_Bank_Bal = 0; //Open_Bank_Bal01 = 0; Open_Bank_Bal02 = 0; Open_Bank_Bal03 = 0; Open_Bank_Bal04 = 0; Open_Bank_Bal05 = 0; Open_Bank_Bal06 = 0; Open_Bank_Bal07 = 0; Open_Bank_Bal08 = 0; Open_Bank_Bal09 = 0; Open_Bank_Bal10 = 0;
            double Close_Bank_Bal = 0;//Close_Bank_Bal01 = 0; Close_Bank_Bal02 = 0; Close_Bank_Bal03 = 0; Close_Bank_Bal04 = 0; Close_Bank_Bal05 = 0; Close_Bank_Bal06 = 0; Close_Bank_Bal07 = 0; Close_Bank_Bal08 = 0; Close_Bank_Bal09 = 0; Close_Bank_Bal10 = 0;
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date, _xTo_Date, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }
            int _bankCnt = 1;
            int _colWidth = 0;
            int _colSetWidth = 90;
            string _online_BANK_COL_TR_REC = "";
            string _online_BANK_COL_NB_REC = "";
            string _online_BANK_COL_TR_PAY = "";
            string _online_BANK_COL_NB_PAY = "";
            string _local__BANK_COL_TR_REC = "";
            string _local__BANK_COL_NB_REC = "";
            string _local__BANK_COL_TR_PAY = "";
            string _local__BANK_COL_NB_PAY = "";
            bool iTR_REC_JOURNAL_Remove = true;
            bool iTR_REC_TOTAL_Remove = true;
            //if (Bank_Bal.Rows.Count > 0)
            //{
            //    iTR_REC_BANK_Visible = false;
            //    iTR_PAY_BANK_Visible = false;
            //    _colWidth -= _colSetWidth;
            //    HideBank_Text = "Hide Bank";
            //}
            //else
            //{
            string HideBank_Text = "Show Bank";
            //}
            bool iTR_PAY_JOURNAL_Remove = true;
            bool iTR_PAY_TOTAL_Remove = true;

            //REC_BANK01_Remove = true;
            //REC_BANK02_Remove = true;
            //REC_BANK03_Remove = true;
            //REC_BANK04_Remove = true;
            //REC_BANK05_Remove = true;
            //REC_BANK06_Remove = true;
            //REC_BANK07_Remove = true;
            //REC_BANK08_Remove = true;
            //REC_BANK09_Remove = true;
            //REC_BANK10_Remove = true;

            //PAY_BANK01_Remove = true;
            //PAY_BANK02_Remove = true;
            //PAY_BANK03_Remove = true;
            //PAY_BANK04_Remove = true;
            //PAY_BANK05_Remove = true;
            //PAY_BANK06_Remove = true;
            //PAY_BANK07_Remove = true;
            //PAY_BANK08_Remove = true;
            //PAY_BANK09_Remove = true;
            //PAY_BANK10_Remove = true;

            //REC_BANK01_Tag = "NO";
            //REC_BANK02_Tag = "NO";
            //REC_BANK03_Tag = "NO";
            //REC_BANK04_Tag = "NO";
            //REC_BANK05_Tag = "NO";
            //REC_BANK06_Tag = "NO";
            //REC_BANK07_Tag = "NO";
            //REC_BANK08_Tag = "NO";
            //REC_BANK09_Tag = "NO";
            //REC_BANK10_Tag = "NO";
            int count = Bank_Bal.Rows.Count;
            if (count > 0 && BASE._open_User_Self_Posted == false)
            {
                for (int i = 0; i < count; i++)
                {
                    DataRow XROW = Bank_Bal.Rows[i];
                    if (!Convert.IsDBNull(XROW["OPENING"]))
                    {
                        Open_Bank_Bal += Convert.ToDouble(XROW["OPENING"]);
                    }
                    else
                    {
                        Open_Bank_Bal += 0;
                    }
                    if (!Convert.IsDBNull(XROW["CLOSING"]))
                    {
                        Close_Bank_Bal += Convert.ToDouble(XROW["CLOSING"]);
                    }
                    else
                    {
                        Close_Bank_Bal += 0;
                    }
                    //switch (_bankCnt)
                    //{
                    #region Commented Code
                    //    case 1:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal01 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal01 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal01 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal01 = 0;
                    //        }
                    //        REC_BANK01_Field = "REC_BANK01";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK01_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK01_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK01_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK01_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK01_Visible = true;
                    //        REC_BANK01_Tag = "YES";
                    //        REC_BANK01_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK01, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK01, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK01_Field = "PAY_BANK01";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK01_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK01_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK01_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK01_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK01_Visible = true;
                    //        PAY_BANK01_Tag = "YES";
                    //        PAY_BANK01_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK01, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK01, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 2:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal02 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal02 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal02 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal02 = 0;
                    //        }
                    //        REC_BANK02_Field = "REC_BANK02";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK02_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK02_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK02_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK02_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK02_Visible = true;
                    //        REC_BANK02_Tag = "YES";
                    //        REC_BANK02_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK02, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK02, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK02_Field = "PAY_BANK02";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK02_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK02_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK02_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK02_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK02_Visible = true;
                    //        PAY_BANK02_Tag = "YES";
                    //        PAY_BANK02_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK02, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK02, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 3:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal03 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal03 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal03 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal03 = 0;
                    //        }
                    //        REC_BANK03_Field = "REC_BANK03";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK03_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK03_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK03_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK03_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK03_Visible = true;
                    //        REC_BANK03_Tag = "YES";
                    //        REC_BANK03_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK03, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK03, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK03_Field = "PAY_BANK03";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK03_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK03_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK03_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK03_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK03_Visible = true;
                    //        PAY_BANK03_Tag = "YES";
                    //        PAY_BANK03_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK03, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK03, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 4:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal04 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal04 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal04 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal04 = 0;
                    //        }
                    //        REC_BANK04_Field = "REC_BANK04";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK04_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK04_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK04_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK04_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK04_Visible = true;
                    //        REC_BANK04_Tag = "YES";
                    //        REC_BANK04_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK04, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK04, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK04_Field = "PAY_BANK04";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK04_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK04_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK04_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK04_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK04_Visible = true;
                    //        PAY_BANK04_Tag = "YES";
                    //        PAY_BANK04_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK04, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK04, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 5:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal05 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal05 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal05 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal05 = 0;
                    //        }
                    //        REC_BANK05_Field = "REC_BANK05";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK05_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK05_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK05_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK05_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK05_Visible = true;
                    //        REC_BANK05_Tag = "YES";
                    //        REC_BANK05_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK05, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK05, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK05_Field = "PAY_BANK05";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK05_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK05_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK05_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK05_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK05_Visible = true;
                    //        PAY_BANK05_Tag = "YES";
                    //        PAY_BANK05_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK05, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK05, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 6:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal06 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal06 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal06 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal06 = 0;
                    //        }
                    //        REC_BANK06_Field = "REC_BANK06";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK06_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK06_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK06_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK06_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK06_Visible = true;
                    //        REC_BANK06_Tag = "YES";
                    //        REC_BANK06_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK06, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK06, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK06_Field = "PAY_BANK06";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK06_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK06_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK06_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK06_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK06_Visible = true;
                    //        PAY_BANK06_Tag = "YES";
                    //        PAY_BANK06_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK06, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK06, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 7:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal07 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal07 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal07 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal07 = 0;
                    //        }
                    //        REC_BANK07_Field = "REC_BANK07";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK07_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK07_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK07_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK07_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK07_Visible = true;
                    //        REC_BANK07_Tag = "YES";
                    //        REC_BANK07_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK07, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK07, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK07_Field = "PAY_BANK07";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK07_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK07_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK07_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK07_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK07_Visible = true;
                    //        PAY_BANK07_Tag = "YES";
                    //        PAY_BANK07_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK07, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK07, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 8:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal08 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal08 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal08 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal08 = 0;
                    //        }
                    //        REC_BANK08_Field = "REC_BANK08";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK08_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK08_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK08_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK08_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK08_Visible = true;
                    //        REC_BANK08_Tag = "YES";
                    //        REC_BANK08_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK08, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK08, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK08_Field = "PAY_BANK08";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK08_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK08_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK08_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK08_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK08_Visible = true;
                    //        PAY_BANK08_Tag = "YES";
                    //        PAY_BANK08_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK08, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK08, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 9:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal09 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal09 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal09 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal09 = 0;
                    //        }
                    //        REC_BANK09_Field = "REC_BANK09";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK09_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK09_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK09_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK09_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK09_Visible = true;
                    //        REC_BANK09_Tag = "YES";
                    //        REC_BANK09_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK09, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK09, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK09_Field = "PAY_BANK09";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK09_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK09_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK09_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK09_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK09_Visible = true;
                    //        PAY_BANK09_Tag = "YES";
                    //        PAY_BANK09_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK09, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK09, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    //    case 10:
                    //        if (!Convert.IsDBNull(XROW["OPENING"]))
                    //        {
                    //            Open_Bank_Bal10 = Convert.ToDouble(XROW["OPENING"]);
                    //        }
                    //        else
                    //        {
                    //            Open_Bank_Bal10 = 0;
                    //        }
                    //        if (!Convert.IsDBNull(XROW["CLOSING"]))
                    //        {
                    //            Close_Bank_Bal10 = Convert.ToDouble(XROW["CLOSING"]);
                    //        }
                    //        else
                    //        {
                    //            Close_Bank_Bal10 = 0;
                    //        }
                    //        REC_BANK10_Field = "REC_BANK10";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            REC_BANK10_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            REC_BANK10_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["REC_BANK10_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK10_CC = "Receipt Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        REC_BANK10_Visible = true;
                    //        REC_BANK10_Tag = "YES";
                    //        REC_BANK10_Remove = false;
                    //        _online_BANK_COL_TR_REC += " Sum(  CASE WHEN TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as REC_BANK10, ";
                    //        _online_BANK_COL_NB_REC += " NULL, ";
                    //        _local__BANK_COL_TR_REC += " Sum( IIF(TR_DR_LED_ID='00079' and TR_SUB_DR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as REC_BANK10, ";
                    //        _local__BANK_COL_NB_REC += " NULL, ";
                    //        PAY_BANK10_Field = "PAY_BANK10";
                    //        if (XROW["BA_ACCOUNT_NO"].ToString().Length < 4)
                    //        {
                    //            PAY_BANK10_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        }
                    //        else
                    //        {
                    //            PAY_BANK10_Caption = XROW["BI_SHORT_NAME"].ToString() + " " + XROW["BA_ACCOUNT_NO"].ToString().Substring(XROW["BA_ACCOUNT_NO"].ToString().Length - 4, 4);
                    //        }
                    //        TempData["PAY_BANK10_Tooltip"] = "A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK10_CC = "Payment Bank: " + XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    //        PAY_BANK10_Visible = true;
                    //        PAY_BANK10_Tag = "YES";
                    //        PAY_BANK10_Remove = false;
                    //        _online_BANK_COL_TR_PAY += " Sum(  CASE WHEN TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "' THEN TR_AMOUNT ELSE NULL END ) as PAY_BANK10, ";
                    //        _online_BANK_COL_NB_PAY += " NULL, ";
                    //        _local__BANK_COL_TR_PAY += " Sum( IIF(TR_CR_LED_ID='00079' and TR_SUB_CR_LED_ID='" + XROW["ID"] + "',TR_AMOUNT,NULL) ) as PAY_BANK10, ";
                    //        _local__BANK_COL_NB_PAY += " NULL, ";
                    //        _colWidth += _colSetWidth;
                    //        break;
                    #endregion Commented Code
                    // }
                    _bankCnt += 1;
                }
            }
            else
            {
                Open_Bank_Bal = 0; //Open_Bank_Bal01 = 0; Open_Bank_Bal02 = 0; Open_Bank_Bal03 = 0; Open_Bank_Bal04 = 0; Open_Bank_Bal05 = 0; Open_Bank_Bal06 = 0; Open_Bank_Bal07 = 0; Open_Bank_Bal08 = 0; Open_Bank_Bal09 = 0; Open_Bank_Bal10 = 0;
                Close_Bank_Bal = 0; //Close_Bank_Bal01 = 0; Close_Bank_Bal02 = 0; Close_Bank_Bal03 = 0; Close_Bank_Bal04 = 0; Close_Bank_Bal05 = 0; Close_Bank_Bal06 = 0; Close_Bank_Bal07 = 0; Close_Bank_Bal08 = 0; Close_Bank_Bal09 = 0; Close_Bank_Bal10 = 0;
            }
            iTR_PAY_JOURNAL_Remove = false;
            iTR_PAY_TOTAL_Remove = false;
            iTR_REC_JOURNAL_Remove = false;
            iTR_REC_TOTAL_Remove = false;      
            string Advanced_Filter_Category = "";
            string Advanced_Filter_RefID = "";         
            if (_ActiveFilterString != null && _ActiveFilterString.Length > 0 && _ActiveFilterString.Contains("Advanced_Filter"))
            {
                int Ind = _ActiveFilterString.IndexOf("Advanced_Filter");
                string sub_str1 = "";
                if (Ind > 0)
                {
                    sub_str1 = _ActiveFilterString.Substring(0, Ind - 1);
                }
                string sub_str2 = _ActiveFilterString.Substring(Ind);
                string[] sub_str2_split = sub_str2.Split('=');
                if (sub_str2_split.Count() > 1)
                {
                    Advanced_Filter_Category = sub_str2_split[1].Replace(",", "").Trim();
                    Advanced_Filter_RefID = sub_str2_split[2].Replace("'", "").Trim();
                }               
            }
            DataTable TR_Table = BASE._Voucher_DBOps.GetList(_xFr_Date, _xTo_Date, _online_BANK_COL_TR_REC, _online_BANK_COL_NB_REC, _local__BANK_COL_TR_REC, _local__BANK_COL_NB_REC, _online_BANK_COL_TR_PAY, _online_BANK_COL_NB_PAY, _local__BANK_COL_TR_PAY, _local__BANK_COL_NB_PAY, Advanced_Filter_Category, Advanced_Filter_RefID, showDynamicBankColumns_CB) as DataTable;
            if (TR_Table == null)
            {
                jsonParam.message = Messages.SomeError;
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                jsonParam.closeform = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }       
            DataSet Voucher_DS = new DataSet();
            Voucher_DS.Tables.Add(TR_Table.Copy());
            Voucher_DS.Tables.Add(Bank_Bal.Copy());
            DataRelation BANK_Relation = Voucher_DS.Relations.Add("BANK_ACC", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_SUB_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            DataRelation BANK_Relation2 = Voucher_DS.Relations.Add("BANK_ACC2", Voucher_DS.Tables["Transaction_Info"].Columns["iTR_CR_ID"], Voucher_DS.Tables["BANK_ACCOUNT_INFO"].Columns["ID"], false);
            count = Voucher_DS.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                DataRow XROW = Voucher_DS.Tables[0].Rows[i];
                DataRow[] bankrelation_childrows = XROW.GetChildRows(BANK_Relation);
                int bank_relationcount = bankrelation_childrows.Count();
                for (int j = 0; j < bank_relationcount; j++)
                {
                    DataRow _Row = bankrelation_childrows[j];
                    if (XROW["iTR_PARTY_1"].ToString().Length <= 0)
                    {
                        XROW["iTR_PARTY_1"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
                DataRow[] bankrelation2_childrows = XROW.GetChildRows(BANK_Relation2);
                int bank_relation2count = bankrelation2_childrows.Count();
                for (int k = 0; k < bank_relation2count; k++)
                {
                    DataRow _Row = bankrelation2_childrows[k];
                    if (XROW["iTR_CR_NAME"].ToString().Length <= 0)
                    {
                        XROW["iTR_CR_NAME"] = _Row["BI_SHORT_NAME"] + ", A/c.No.: " + _Row["BA_ACCOUNT_NO"];
                    }
                }
            }
            Voucher_DS.Relations.Clear();
            int _Date_Serial = 0;
            string _Date_Show = "";
            if (Convert.ToInt32(_xFr_Date.ToString("MM")) > 3)
            {
                _Date_Serial = Convert.ToInt32(_xFr_Date.AddMonths(-3).ToString("MM"));
                _Date_Show = BASE._open_Year_Sdt.ToString("yyyy") + "-" + string.Format(_xFr_Date.ToString("MM"), "00") + "-01";
            }
            else
            {
                _Date_Serial = Convert.ToInt32(_xFr_Date.AddMonths(+9).ToString("MM"));
                _Date_Show = BASE._open_Year_Edt.ToString("yyyy") + "-" + string.Format(_xFr_Date.ToString("MM"), "00") + "-01";
            }
            DataRow ROW = default(DataRow);
            ROW = Voucher_DS.Tables[0].NewRow();
            ROW["iTR_DATE_SERIAL"] = _Date_Serial;
            ROW["iTR_DATE_SHOW"] = _Date_Show;
            ROW["iTR_TEMP_ID"] = "OPENING BALANCE";
            ROW["iREC_ID"] = "OPENING BALANCE";
            ROW["iTR_ROW_POS"] = "A";
            ROW["iTR_VNO"] = "";
            ROW["iTR_DATE"] = string.Format(_xFr_Date.ToString(), BASE._Date_Format_Current);
            ROW["iTR_REC_CASH"] = Open_Cash_Bal;
            ROW["iTR_REC_BANK"] = Open_Bank_Bal;
            ROW["iTR_ITEM"] = "OPENING BALANCE";
            //for (int i = 0; i < Bank_Bal.Rows.Count; i++)
            //{
            //    string accountNo = Bank_Bal.Rows[i]["BA_ACCOUNT_NO"].ToString();
            //    string columnName = "Dyn_Rec_" + Bank_Bal.Rows[i]["BI_SHORT_NAME"].ToString() + accountNo.Substring(accountNo.Length - 4);
            //    int openingValue = Convert.ToInt32(Bank_Bal.Rows[i]["OPENING"]);
            //    ROW[columnName] = openingValue;
            //}
            //if ((string)REC_BANK01_Tag == "YES")
            //{
            //    ROW["REC_BANK01"] = Open_Bank_Bal01;
            //}
            //if ((string)REC_BANK02_Tag == "YES")
            //{
            //    ROW["REC_BANK02"] = Open_Bank_Bal02;
            //}
            //if ((string)REC_BANK03_Tag == "YES")
            //{
            //    ROW["REC_BANK03"] = Open_Bank_Bal03;
            //}
            //if ((string)REC_BANK04_Tag == "YES")
            //{
            //    ROW["REC_BANK04"] = Open_Bank_Bal04;
            //}
            //if ((string)REC_BANK05_Tag == "YES")
            //{
            //    ROW["REC_BANK05"] = Open_Bank_Bal05;
            //}
            //if ((string)REC_BANK06_Tag == "YES")
            //{
            //    ROW["REC_BANK06"] = Open_Bank_Bal06;
            //}
            //if ((string)REC_BANK07_Tag == "YES")
            //{
            //    ROW["REC_BANK07"] = Open_Bank_Bal07;
            //}
            //if ((string)REC_BANK08_Tag == "YES")
            //{
            //    ROW["REC_BANK08"] = Open_Bank_Bal08;
            //}
            //if ((string)REC_BANK09_Tag == "YES")
            //{
            //    ROW["REC_BANK09"] = Open_Bank_Bal09;
            //}
            //if ((string)REC_BANK10_Tag == "YES")
            //{
            //    ROW["REC_BANK10"] = Open_Bank_Bal10;
            //}
            Voucher_DS.Tables[0].Rows.Add(ROW);
            DataView DV1 = new DataView(Voucher_DS.Tables[0]);
            DV1.Sort = "iTR_DATE,iTR_ROW_POS,iTR_ENTRY,iREC_ADD_ON,iTR_M_ID,iTR_SORT,iTR_SR_NO";
            DataTable XTABLE = DV1.ToTable();
            string _TEMP = "";
            if (XTABLE.Rows.Count > 0)
            {
                _TEMP = DV1.ToTable().Rows[0]["iTR_TEMP_ID"].ToString();
            }
            if (XTABLE.Columns.Contains("iIcon") == false)
            {
                XTABLE.Columns.Add("iIcon", typeof(System.String));
            }
            if (XTABLE.Columns.Contains("Grid_PK") == false)
            {
                XTABLE.Columns.Add("Grid_PK", typeof(System.String));
            }
            double _SR = 1;
            double Next_Unattended_Attachment_Index = -1;
            string Next_Unattended_Attachment_Key = "";
            var TotalRowCount = XTABLE.Rows.Count;
            for (int i = 0; i < TotalRowCount; i++)
            {
                DataRow Row = XTABLE.Rows[i];
                if ((string)Row["iTR_TEMP_ID"] == _TEMP)
                {
                    Row["iTR_REF_NO"] = _SR;
                }
                else
                {
                    _TEMP = Row["iTR_TEMP_ID"].ToString();
                    _SR = _SR + 1;
                    Row["iTR_REF_NO"] = _SR;
                }
                string iIcon = "";
                int COMPLETE_ATTACH_COUNT = Row.Field<Int32?>("COMPLETE_ATTACH_COUNT") ?? 0;
                int RESPONDED_COUNT = Row.Field<Int32?>("RESPONDED_COUNT") ?? 0;
                int REQ_ATTACH_COUNT = Row.Field<Int32?>("REQ_ATTACH_COUNT") ?? 0;
                int REJECTED_COUNT = Row.Field<Int32?>("REJECTED_COUNT") ?? 0;
                int ALL_ATTACH_CNT = Row.Field<Int32?>("ALL_ATTACH_CNT") ?? 0;
                int OTHER_ATTACH_CNT = Row.Field<Int32?>("OTHER_ATTACH_CNT") ?? 0;
                int VOUCHING_TOTAL_COUNT = Row.Field<Int32?>("VOUCHING_TOTAL_COUNT") ?? 0;
                int VOUCHING_ACCEPTED_COUNT = Row.Field<Int32?>("VOUCHING_ACCEPTED_COUNT") ?? 0;
                int VOUCHING_ACCEPTED_WITH_REMARKS_COUNT = Row.Field<Int32?>("VOUCHING_ACCEPTED_WITH_REMARKS_COUNT") ?? 0;
                int VOUCHING_REJECTED_COUNT = Row.Field<Int32?>("VOUCHING_REJECTED_COUNT") ?? 0;
                int VOUCHING_PENDING_COUNT = Row.Field<Int32?>("VOUCHING_PENDING_COUNT") ?? 0;
                int AUDIT_TOTAL_COUNT = Row.Field<Int32?>("AUDIT_TOTAL_COUNT") ?? 0;
                int AUDIT_ACCEPTED_COUNT = Row.Field<Int32?>("AUDIT_ACCEPTED_COUNT") ?? 0;
                int AUDIT_ACCEPTED_WITH_REMARKS_COUNT = Row.Field<Int32?>("AUDIT_ACCEPTED_WITH_REMARKS_COUNT") ?? 0;
                int AUDIT_REJECTED_COUNT = Row.Field<Int32?>("AUDIT_REJECTED_COUNT") ?? 0;
                int AUDIT_PENDING_COUNT = Row.Field<Int32?>("AUDIT_PENDING_COUNT") ?? 0;
                int IS_AUTOVOUCHING = Row.Field<Int32?>("IS_AUTOVOUCHING") ?? 0;
                int IS_CORRECTED_ENTRY = Row.Field<Int32?>("IS_CORRECTED_ENTRY") ?? 0;
                if ((COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) == 0 && REQ_ATTACH_COUNT > 0)
                {
                    iIcon += "RedShield|";
                }
                else if ((COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) >= REQ_ATTACH_COUNT && (REQ_ATTACH_COUNT > 0) && (RESPONDED_COUNT == 0))
                {
                    iIcon += "GreenShield|";
                }
                else if ((COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) > 0 && (COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) < REQ_ATTACH_COUNT)
                {
                    iIcon += "YellowShield|";
                }
                else if ((COMPLETE_ATTACH_COUNT + RESPONDED_COUNT) >= REQ_ATTACH_COUNT && (REQ_ATTACH_COUNT > 0) && (RESPONDED_COUNT > 0))
                {
                    iIcon += "BlueShield|";
                }
                if (REJECTED_COUNT > 0)
                {
                    iIcon += "RedFlag|";
                }
                if (ALL_ATTACH_CNT > 0 && OTHER_ATTACH_CNT == 0)
                {
                    iIcon += "RequiredAttachment|";
                }
                else if (ALL_ATTACH_CNT > 0 && OTHER_ATTACH_CNT != 0)
                {
                    iIcon += "AdditionalAttachment|";
                }
                if (VOUCHING_TOTAL_COUNT == VOUCHING_ACCEPTED_COUNT && VOUCHING_ACCEPTED_WITH_REMARKS_COUNT == 0 && VOUCHING_ACCEPTED_COUNT > 0)
                { iIcon += "VouchingAccepted|"; }
                if (VOUCHING_REJECTED_COUNT > 0) { iIcon += "VouchingReject|"; }
                if (VOUCHING_TOTAL_COUNT == VOUCHING_ACCEPTED_COUNT && VOUCHING_ACCEPTED_WITH_REMARKS_COUNT > 0) { iIcon += "VouchingAcceptWithRemarks|"; }
                if (VOUCHING_PENDING_COUNT > 0 && (VOUCHING_ACCEPTED_COUNT > 0 || VOUCHING_REJECTED_COUNT > 0)) { iIcon += "VouchingPartial|"; }
                if (AUDIT_TOTAL_COUNT == AUDIT_ACCEPTED_COUNT && AUDIT_ACCEPTED_WITH_REMARKS_COUNT == 0 && AUDIT_ACCEPTED_COUNT > 0) 
                { iIcon += "AuditAccepted|"; }
                if (AUDIT_REJECTED_COUNT > 0) {iIcon += "AuditReject|"; }
                if (AUDIT_TOTAL_COUNT == AUDIT_ACCEPTED_COUNT && AUDIT_ACCEPTED_WITH_REMARKS_COUNT > 0) { iIcon += "AuditAcceptWithRemarks|"; }
                if (AUDIT_PENDING_COUNT > 0 && (AUDIT_ACCEPTED_COUNT > 0 || AUDIT_REJECTED_COUNT > 0)) { iIcon += "AuditPartial|"; }
                if (IS_AUTOVOUCHING>0) { iIcon += "AutoVouching|"; }
                if (IS_CORRECTED_ENTRY > 0) { iIcon += "CorrectedEntry|"; }
                Row["iIcon"] = iIcon;
                string Grid_PK = "";
                string iREC_ID = Row.Field<string>("iREC_ID");
                string iTR_M_ID = Row.Field<string>("iTR_M_ID");
                string iTR_ITEM_ID = Row.Field<string>("iTR_ITEM_ID");
                if (iREC_ID == "NOTE-BOOK")
                {
                    Grid_PK = (string.IsNullOrEmpty(iTR_M_ID) ? "Null" : iTR_M_ID) + (string.IsNullOrEmpty(iREC_ID) ? "Null" : iTR_ITEM_ID);
                }
                else
                {
                    Grid_PK = (string.IsNullOrEmpty(iTR_M_ID) ? "Null" : iTR_M_ID) + (string.IsNullOrEmpty(iREC_ID) ? "Null" : iREC_ID);
                }
                Row["Grid_PK"] = Grid_PK;
                if (Next_Unattended_Attachment_Index == -1 || Next_Unattended_Attachment_Index == -2)
                {
                    if ((REQ_ATTACH_COUNT - COMPLETE_ATTACH_COUNT - RESPONDED_COUNT) > 0)
                    {
                        if (i == XTABLE.Rows.Count - 1)
                        {
                            if (_GridFocusedOrExpandedIndex == null || _GridFocusedOrExpandedIndex < 0)
                            {
                                Next_Unattended_Attachment_Index = i;                              
                            }
                            else if (i >= _GridFocusedOrExpandedIndex)
                            {
                                Next_Unattended_Attachment_Index = i;
                            }
                            else
                            {
                                Next_Unattended_Attachment_Index = -2;
                            }
                        }
                        else if (_TEMP != XTABLE.Rows[i + 1]["iTR_TEMP_ID"].ToString())
                        {
                            if (_GridFocusedOrExpandedIndex == null || _GridFocusedOrExpandedIndex < 0)
                            {
                                Next_Unattended_Attachment_Index = i;
                            }
                            else if (i >= _GridFocusedOrExpandedIndex)
                            {
                                Next_Unattended_Attachment_Index = i;
                            }
                            else
                            {
                                Next_Unattended_Attachment_Index = -2;
                            }
                        }
                    }
                }
            }
            //List<CB_Grid_Model> GridData = Helper.DatatableToModel.DataTabletoCashBook(XTABLE);
            //XTABLE.Dispose();
            if (Next_Unattended_Attachment_Index >= 0) 
            {
               // Next_Unattended_Attachment_Key = GridData[(int)Next_Unattended_Attachment_Index].Grid_PK;
                Next_Unattended_Attachment_Key = (string)XTABLE.Rows[(int)Next_Unattended_Attachment_Index]["Grid_PK"];
            }
            //if (Negative_MsgStr.Trim().Length > 0)
            //{
            //    jsonParam.message = Negative_MsgStr;
            //    jsonParam.title = "Alert..";
            //    jsonParam.result = false;
            //    jsonParam.closeform = false;
            //    return Json(new
            //    {
            //        jsonParam
            //    }, JsonRequestBehavior.AllowGet);
            //}
            jsonParam.message = "";
            jsonParam.title = "";
            jsonParam.result = true;
            jsonParam.closeform = false;
            jsonParam.Next_Unattended_Attachment_Index = Convert.ToInt32(Next_Unattended_Attachment_Index);
            var result = new
            {
                jsonParam,
                Close_Bank_Bal,
                Close_Cash_Bal,
                TotalRowCount,
                iTR_PAY_TOTAL_Remove,
                iTR_PAY_JOURNAL_Remove,
                iTR_REC_TOTAL_Remove,
                iTR_REC_JOURNAL_Remove,
                Next_Unattended_Attachment_Key,
                ShowRECBank = true,
                ShowPAYBank = true,
                GridData = XTABLE   
            };
            return Content(JsonConvert.SerializeObject(result), "application/json");
            //return Json(new
            //{
            //    jsonParam,
            //    Close_Bank_Bal,
            //    Close_Cash_Bal,             
            //    TotalRowCount,              
            //    iTR_PAY_TOTAL_Remove,
            //    iTR_PAY_JOURNAL_Remove,
            //    iTR_REC_TOTAL_Remove,
            //    iTR_REC_JOURNAL_Remove,
            //    Next_Unattended_Attachment_Key,
            //    ShowRECBank =true,
            //    ShowPAYBank=true,
            //    GridData = JsonConvert.SerializeObject(XTABLE),              
            //}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Frm_Voucher_Info_CB_Grid_Nested_dx(string ID, string MID, bool VouchingMode = false)
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetDocumentMapping(ID, MID, ClientScreen.Accounts_CashBook, !VouchingMode)), "application/json");
        }
        public ActionResult Frm_Voucher_Info_CB_AdditionalGridData_dx(bool VouchingMode = false, string ID = "", string MID = "")
        {
            return Content(JsonConvert.SerializeObject(BASE._Audit_DBOps.GetAdditionalInfo(ID, MID, BASE._open_Cen_ID, ClientScreen.Accounts_CashBook)), "application/json");
        }
        public List<CB_Period> FillChangePeriod_dx()
        {
            var period = new List<CB_Period>();
            int index = 0;
            for (int I = BASE._open_Year_Sdt.Month; I <= 12; I++)
            {
                CB_Period row1 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row1.Period = xMonth + "-" + BASE._open_Year_Sdt.Year;
                row1.SelectedIndex = index;
                index++;
                period.Add(row1);
            }
            for (int I = 1; I <= BASE._open_Year_Edt.Month; I++)
            {
                CB_Period row2 = new CB_Period();
                string xMonth = (I == 1 ? "JAN" : (I == 2 ? "FEB" : (I == 3 ? "MAR" : (I == 4 ? "APR" : (I == 5 ? "MAY" : (I == 6 ? "JUN" : (I == 7 ? "JUL" : (I == 8 ? "AUG" : (I == 9 ? "SEP" : (I == 10 ? "OCT" : (I == 11 ? "NOV" : (I == 12 ? "DEC" : ""))))))))))));
                row2.SelectedIndex = index;
                row2.Period = xMonth + "-" + BASE._open_Year_Edt.Year;
                period.Add(row2);
                index++;
            }
            CB_Period row = new CB_Period
            {
                Period = "1st Quarter",
                SelectedIndex = index
            };
            period.Add(row);
            CB_Period row3 = new CB_Period
            {
                Period = "2nd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row3);
            CB_Period row4 = new CB_Period
            {
                Period = "3rd Quarter",
                SelectedIndex = ++index
            };
            period.Add(row4);
            CB_Period row5 = new CB_Period
            {
                Period = "4th Quarter",
                SelectedIndex = ++index
            };
            period.Add(row5);
            CB_Period row6 = new CB_Period
            {
                Period = "1st Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row6);
            CB_Period row7 = new CB_Period
            {
                Period = "2nd Half Yearly",
                SelectedIndex = ++index
            };
            period.Add(row7);
            CB_Period row8 = new CB_Period
            {
                Period = "Nine Months",
                SelectedIndex = ++index
            };
            period.Add(row8);
            CB_Period row9 = new CB_Period
            {
                Period = "Financial Year",
                SelectedIndex = ++index
            };
            period.Add(row9);
            CB_Period row10 = new CB_Period
            {
                Period = "Specific Period",
                SelectedIndex = ++index
            };
            period.Add(row10);
            return period;
        }
        public ActionResult Fill_Change_Period_Items_dx(DataSourceLoadOptions loadOptions)
        {           
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(FillChangePeriod_dx(), loadOptions)), "application/json");
        }     
        public ActionResult frm_Voucher_Specific_Period_dx(bool PeriodSelectionFirstTime = false,string xFrDate = "", string xToDate = "")
        {
            ViewBag.PeriodSelectionFirstTime = PeriodSelectionFirstTime;
            CB_SpeceficPeriod model = new CB_SpeceficPeriod();
            model.CB_Fromdate = Convert.ToDateTime(xFrDate);
            model.CB_Todate = Convert.ToDateTime(xToDate);
            return View("frm_Voucher_Specific_Period_dx", model);
        }
        public ActionResult Frm_View_Summary_dx(string PopupID = "", string xFrDate = "", string xToDate = "")
        {
            ViewBag.PopupID = PopupID.Length > 0 ? PopupID : "popup_frm_Frm_View_Summary";
            double Open_Cash_Bal = 0;
            double Close_Cash_Bal = 0;
            double Open_Bank_Bal = 0;
            double Close_Bank_Bal = 0;
            double R_CASH = 0;
            double R_BANK = 0;
            double P_CASH = 0;
            double P_BANK = 0;
            DateTime _xFr_Date = Convert.ToDateTime(xFrDate);
            DateTime _xTo_Date = Convert.ToDateTime(xToDate);
            ViewBag.SummaryPeriod = "Period Fr.: " + _xFr_Date.ToString("dd-MMM, yyyy") + "  to  " + _xTo_Date.ToString("dd-MMM, yyyy");

            DataSet CashBank_DS = new DataSet();
            DataTable CashBank_Table = CashBank_DS.Tables.Add("Table");
            DataRow ROW = default(DataRow);
            var _with1 = CashBank_Table;
            _with1.Columns.Add("Title", Type.GetType("System.String"));
            _with1.Columns.Add("Sr", Type.GetType("System.Double"));
            _with1.Columns.Add("Description", Type.GetType("System.String"));
            _with1.Columns.Add("O_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["O_BALANCE"].Caption = "Opening Balance";
            _with1.Columns.Add("R_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["R_BALANCE"].Caption = "Total Receipt";
            _with1.Columns.Add("P_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["P_BALANCE"].Caption = "Total Payment";
            _with1.Columns.Add("C_BALANCE", Type.GetType("System.Double"));
            _with1.Columns["C_BALANCE"].Caption = "Closing Balance";

            Open_Cash_Bal = 0;
            Close_Cash_Bal = 0;
            DataTable Cash_Bal = BASE._Voucher_DBOps.GetCashBalanceSummary(_xFr_Date, _xTo_Date, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID, BASE._open_Ins_ID);
            if (Cash_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'> MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            if (Cash_Bal.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["OPENING"]))
                {
                    Open_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["OPENING"]);
                }
                else
                {
                    Open_Cash_Bal = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["RECEIPT"]))
                {
                    R_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["RECEIPT"]);
                }
                else
                {
                    R_CASH = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["PAYMENT"]))
                {
                    P_CASH = Convert.ToDouble(Cash_Bal.Rows[0]["PAYMENT"]);
                }
                else
                {
                    P_CASH = 0;
                }
                if (!Convert.IsDBNull(Cash_Bal.Rows[0]["CLOSING"]))
                {
                    Close_Cash_Bal = Convert.ToDouble(Cash_Bal.Rows[0]["CLOSING"]);
                }
                else
                {
                    Close_Cash_Bal = 0;
                }
            }
            else
            {
                Open_Cash_Bal = 0;
                R_CASH = 0;
                P_CASH = 0;
                Close_Cash_Bal = 0;
            }
            ROW = CashBank_Table.NewRow();
            ROW["Title"] = "CASH";
            ROW["Sr"] = 1;
            ROW["Description"] = "CASH Summary";
            ROW["O_BALANCE"] = Open_Cash_Bal;
            ROW["R_BALANCE"] = R_CASH;
            ROW["P_BALANCE"] = P_CASH;
            ROW["C_BALANCE"] = Close_Cash_Bal;
            CashBank_Table.Rows.Add(ROW);

            //'BANK................................
            DataTable Bank_Bal = BASE._Voucher_DBOps.GetBankBalanceSummary(_xFr_Date, _xTo_Date, BASE._open_Year_Sdt, BASE._open_Cen_ID, BASE._open_Year_ID);
            if (Bank_Bal == null)
            {
                return Content("<script language='javascript' type='text/javascript'>  MultiUserPrevention('popup_frm_Frm_View_Summary','Some Error Happened During The Current Operation!!','Error!!');</script>");
            }
            int XCNT = 2;
            if (Bank_Bal.Rows.Count > 0)
            {
                foreach (DataRow XROW in Bank_Bal.Rows)
                {
                    if (!Convert.IsDBNull(XROW["OPENING"]))
                    {
                        Open_Bank_Bal = Convert.ToDouble(XROW["OPENING"]);
                    }
                    else
                    {
                        Open_Bank_Bal += 0;
                    }
                    if (!Convert.IsDBNull(XROW["RECEIPT"]))
                    {
                        R_BANK = Convert.ToDouble(XROW["RECEIPT"]);
                    }
                    else
                    {
                        R_BANK += 0;
                    }
                    if (!Convert.IsDBNull(XROW["PAYMENT"]))
                    {
                        P_BANK = Convert.ToDouble(XROW["PAYMENT"]);
                    }
                    else
                    {
                        P_BANK += 0;
                    }
                    if (!Convert.IsDBNull(XROW["CLOSING"]))
                    {
                        Close_Bank_Bal = Convert.ToDouble(XROW["CLOSING"]);
                    }
                    else
                    {
                        Close_Bank_Bal += 0;
                    }

                    ROW = CashBank_Table.NewRow();
                    ROW["Title"] = "BANK";
                    ROW["Sr"] = XCNT;
                    ROW["Description"] = XROW["BI_SHORT_NAME"].ToString() + ", A/c. No.: " + XROW["BA_ACCOUNT_NO"].ToString();
                    ROW["O_BALANCE"] = Open_Bank_Bal;
                    ROW["R_BALANCE"] = R_BANK;
                    ROW["P_BALANCE"] = P_BANK;
                    ROW["C_BALANCE"] = Close_Bank_Bal;
                    CashBank_Table.Rows.Add(ROW);
                    XCNT += 1;
                }
            }
            List<Summary> SummaryGridData = new List<Summary>();
            foreach (DataRow XROW in CashBank_Table.Rows)
            {
                var newrow = new Summary();
                newrow.Title = XROW["Title"].ToString();
                newrow.Sr = Convert.ToInt32(XROW["Sr"]);
                newrow.Description = XROW["Description"].ToString();
                newrow.O_BALANCE = Convert.ToDouble(XROW["O_BALANCE"]);
                newrow.R_BALANCE = Convert.ToDouble(XROW["R_BALANCE"]);
                newrow.P_BALANCE = Convert.ToDouble(XROW["P_BALANCE"]);
                newrow.C_BALANCE = Convert.ToDouble(XROW["C_BALANCE"]);
                SummaryGridData.Add(newrow);
            }
            return View("Frm_View_Summary", SummaryGridData);
        }       
        public ActionResult Frm_AdvancedFilters_dx(string _ActiveFilterString)
        {
            AdvanceFilter model = new AdvanceFilter();         
            if (_ActiveFilterString != null && _ActiveFilterString.Contains("Advanced_Filter"))
            {
                int Ind = _ActiveFilterString.IndexOf("Advanced_Filter");
                string sub_str1 = "";
                if (Ind > 0)
                {
                    sub_str1 = _ActiveFilterString.Substring(0, Ind - 1);
                }
                string sub_str2 = _ActiveFilterString.Substring(Ind);
                string[] sub_str2_split= sub_str2.Split('=');
                if (sub_str2_split.Count() > 1)
                {
                    model.Advanced_Filter_Category = sub_str2_split[1].Replace(",", "").Trim();
                    model.Advanced_Filter_RefID = sub_str2_split[2].Replace("'", "").Trim();
                    model.FilterType = model.Advanced_Filter_Category.ToUpper();
                }              
            }
            return View("Frm_AdvancedFilters",model);
        }

        #endregion
        public DateTime _xFr_Date()
        {
            return Convert.ToDateTime(xFr_Date);
        }
        public DateTime _xTo_Date()
        {
            return Convert.ToDateTime(xTo_Date);
        }
        public void CreateViewData()
        {
            //ViewData["REC_BANK01_Remove"] = REC_BANK01_Remove;
            //ViewData["REC_BANK02_Remove"] = REC_BANK02_Remove;
            //ViewData["REC_BANK03_Remove"] = REC_BANK03_Remove;
            //ViewData["REC_BANK04_Remove"] = REC_BANK04_Remove;
            //ViewData["REC_BANK05_Remove"] = REC_BANK05_Remove;
            //ViewData["REC_BANK06_Remove"] = REC_BANK06_Remove;
            //ViewData["REC_BANK07_Remove"] = REC_BANK07_Remove;
            //ViewData["REC_BANK08_Remove"] = REC_BANK08_Remove;
            //ViewData["REC_BANK09_Remove"] = REC_BANK09_Remove;
            //ViewData["REC_BANK10_Remove"] = REC_BANK10_Remove;

            //ViewData["PAY_BANK01_Remove"] = PAY_BANK01_Remove;
            //ViewData["PAY_BANK02_Remove"] = PAY_BANK02_Remove;
            //ViewData["PAY_BANK03_Remove"] = PAY_BANK03_Remove;
            //ViewData["PAY_BANK04_Remove"] = PAY_BANK04_Remove;
            //ViewData["PAY_BANK05_Remove"] = PAY_BANK05_Remove;
            //ViewData["PAY_BANK06_Remove"] = PAY_BANK06_Remove;
            //ViewData["PAY_BANK07_Remove"] = PAY_BANK07_Remove;
            //ViewData["PAY_BANK08_Remove"] = PAY_BANK08_Remove;
            //ViewData["PAY_BANK09_Remove"] = PAY_BANK09_Remove;
            //ViewData["PAY_BANK10_Remove"] = PAY_BANK10_Remove;

            //ViewData["REC_BANK01_Visible"] = REC_BANK01_Visible;
            //ViewData["REC_BANK02_Visible"] = REC_BANK02_Visible;
            //ViewData["REC_BANK03_Visible"] = REC_BANK03_Visible;
            //ViewData["REC_BANK04_Visible"] = REC_BANK04_Visible;
            //ViewData["REC_BANK05_Visible"] = REC_BANK05_Visible;
            //ViewData["REC_BANK06_Visible"] = REC_BANK06_Visible;
            //ViewData["REC_BANK07_Visible"] = REC_BANK07_Visible;
            //ViewData["REC_BANK08_Visible"] = REC_BANK08_Visible;
            //ViewData["REC_BANK09_Visible"] = REC_BANK09_Visible;
            //ViewData["REC_BANK10_Visible"] = REC_BANK10_Visible;

            //ViewData["PAY_BANK01_Visible"] = PAY_BANK01_Visible;
            //ViewData["PAY_BANK02_Visible"] = PAY_BANK02_Visible;
            //ViewData["PAY_BANK03_Visible"] = PAY_BANK03_Visible;
            //ViewData["PAY_BANK04_Visible"] = PAY_BANK04_Visible;
            //ViewData["PAY_BANK05_Visible"] = PAY_BANK05_Visible;
            //ViewData["PAY_BANK06_Visible"] = PAY_BANK06_Visible;
            //ViewData["PAY_BANK07_Visible"] = PAY_BANK07_Visible;
            //ViewData["PAY_BANK08_Visible"] = PAY_BANK08_Visible;
            //ViewData["PAY_BANK09_Visible"] = PAY_BANK09_Visible;
            //ViewData["PAY_BANK10_Visible"] = PAY_BANK10_Visible;

            //ViewData["REC_BANK01_Field"] = REC_BANK01_Field;
            //ViewData["REC_BANK02_Field"] = REC_BANK02_Field;
            //ViewData["REC_BANK03_Field"] = REC_BANK03_Field;
            //ViewData["REC_BANK04_Field"] = REC_BANK04_Field;
            //ViewData["REC_BANK05_Field"] = REC_BANK05_Field;
            //ViewData["REC_BANK06_Field"] = REC_BANK06_Field;
            //ViewData["REC_BANK07_Field"] = REC_BANK07_Field;
            //ViewData["REC_BANK08_Field"] = REC_BANK08_Field;
            //ViewData["REC_BANK09_Field"] = REC_BANK09_Field;
            //ViewData["REC_BANK10_Field"] = REC_BANK10_Field;

            //ViewData["PAY_BANK01_Field"] = PAY_BANK01_Field;
            //ViewData["PAY_BANK02_Field"] = PAY_BANK02_Field;
            //ViewData["PAY_BANK03_Field"] = PAY_BANK03_Field;
            //ViewData["PAY_BANK04_Field"] = PAY_BANK04_Field;
            //ViewData["PAY_BANK05_Field"] = PAY_BANK05_Field;
            //ViewData["PAY_BANK06_Field"] = PAY_BANK06_Field;
            //ViewData["PAY_BANK07_Field"] = PAY_BANK07_Field;
            //ViewData["PAY_BANK08_Field"] = PAY_BANK08_Field;
            //ViewData["PAY_BANK09_Field"] = PAY_BANK09_Field;
            //ViewData["PAY_BANK10_Field"] = PAY_BANK10_Field;

            //ViewData["REC_BANK01_Caption"] = REC_BANK01_Caption;
            //ViewData["REC_BANK02_Caption"] = REC_BANK02_Caption;
            //ViewData["REC_BANK03_Caption"] = REC_BANK03_Caption;
            //ViewData["REC_BANK04_Caption"] = REC_BANK04_Caption;
            //ViewData["REC_BANK05_Caption"] = REC_BANK05_Caption;
            //ViewData["REC_BANK06_Caption"] = REC_BANK06_Caption;
            //ViewData["REC_BANK07_Caption"] = REC_BANK07_Caption;
            //ViewData["REC_BANK08_Caption"] = REC_BANK08_Caption;
            //ViewData["REC_BANK09_Caption"] = REC_BANK09_Caption;
            //ViewData["REC_BANK10_Caption"] = REC_BANK10_Caption;

            //ViewData["PAY_BANK01_Caption"] = PAY_BANK01_Caption;
            //ViewData["PAY_BANK02_Caption"] = PAY_BANK02_Caption;
            //ViewData["PAY_BANK03_Caption"] = PAY_BANK03_Caption;
            //ViewData["PAY_BANK04_Caption"] = PAY_BANK04_Caption;
            //ViewData["PAY_BANK05_Caption"] = PAY_BANK05_Caption;
            //ViewData["PAY_BANK06_Caption"] = PAY_BANK06_Caption;
            //ViewData["PAY_BANK07_Caption"] = PAY_BANK07_Caption;
            //ViewData["PAY_BANK08_Caption"] = PAY_BANK08_Caption;
            //ViewData["PAY_BANK09_Caption"] = PAY_BANK09_Caption;
            //ViewData["PAY_BANK10_Caption"] = PAY_BANK10_Caption;

            //ViewData["REC_BANK01_Tooltip"] = REC_BANK01_Tooltip;
            //ViewData["REC_BANK02_Tooltip"] = REC_BANK02_Tooltip;
            //ViewData["REC_BANK03_Tooltip"] = REC_BANK03_Tooltip;
            //ViewData["REC_BANK04_Tooltip"] = REC_BANK04_Tooltip;
            //ViewData["REC_BANK05_Tooltip"] = REC_BANK05_Tooltip;
            //ViewData["REC_BANK06_Tooltip"] = REC_BANK06_Tooltip;
            //ViewData["REC_BANK07_Tooltip"] = REC_BANK07_Tooltip;
            //ViewData["REC_BANK08_Tooltip"] = REC_BANK08_Tooltip;
            //ViewData["REC_BANK09_Tooltip"] = REC_BANK09_Tooltip;
            //ViewData["REC_BANK10_Tooltip"] = REC_BANK10_Tooltip;

            //ViewData["PAY_BANK01_Tooltip"] = PAY_BANK01_Tooltip;
            //ViewData["PAY_BANK02_Tooltip"] = PAY_BANK02_Tooltip;
            //ViewData["PAY_BANK03_Tooltip"] = PAY_BANK03_Tooltip;
            //ViewData["PAY_BANK04_Tooltip"] = PAY_BANK04_Tooltip;
            //ViewData["PAY_BANK05_Tooltip"] = PAY_BANK05_Tooltip;
            //ViewData["PAY_BANK06_Tooltip"] = PAY_BANK06_Tooltip;
            //ViewData["PAY_BANK07_Tooltip"] = PAY_BANK07_Tooltip;
            //ViewData["PAY_BANK08_Tooltip"] = PAY_BANK08_Tooltip;
            //ViewData["PAY_BANK09_Tooltip"] = PAY_BANK09_Tooltip;
            //ViewData["PAY_BANK10_Tooltip"] = PAY_BANK10_Tooltip;

            ViewData["iTR_REC_JOURNAL_Remove"] = iTR_REC_JOURNAL_Remove;
            ViewData["iTR_REC_TOTAL_Remove"] = iTR_REC_TOTAL_Remove;
            ViewData["iTR_PAY_JOURNAL_Remove"] = iTR_PAY_JOURNAL_Remove;
            ViewData["iTR_PAY_TOTAL_Remove"] = iTR_PAY_TOTAL_Remove;

            ViewData["iTR_REC_BANK_Visible"] = true;
            ViewData["iTR_PAY_BANK_Visible"] = true;
            ViewData["iRef_no_Visible"] = iRef_no_Visible;
            ViewData["Summary_Column_Status"] = Summary_Column_Status;
            ViewData["ActiveFilterString"] = ActiveFilterString;

            ViewData["HideBank_Text"] = HideBank_Text;
            ViewData["BE_Cash_Bank_Text"] = BE_Cash_Bank_Text;
            ViewData["Next_Unattended_Attachment_Index"] = _Next_Unattended_Attachment_Index;

            if (!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Manage_Remarks))
            {
                ViewData["CB_AddRemarks_Visible"] = false;
            }
            else
            {
                ViewData["CB_AddRemarks_Visible"] = true;
            }
            if ((!BASE.CheckActionRights(ClientScreen.Accounts_Vouchers, Common.ClientAction.Lock_Unlock)) && !(BASE._open_Cen_ID == 4216))
            {
                ViewData["CB_Locked_Visible"] = false;
                ViewData["CB_UnLocked_Visible"] = false;
                ViewData["CB_MatchTransfer_Visible"] = false;
                ViewData["CB_UnMatchTransfer_Visible"] = false;
            }
            else
            {
                ViewData["CB_Locked_Visible"] = true;
                ViewData["CB_UnLocked_Visible"] = true;
                ViewData["CB_MatchTransfer_Visible"] = true;
                ViewData["CB_UnMatchTransfer_Visible"] = true;
            }
            ViewData["Help_Attachments_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
            ViewData["CashBook_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_CashBook, "Export");
            ViewData["Help_Attachments_ListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");
        }
        public bool IsDate(string text)
        {
            DateTime temp;
            if (DateTime.TryParse(text, out temp))
                return true;
            else
                return false;
        }
        public void ResetSaticVariable()
        {
            xFr_Date = null;
            xTo_Date = null;
            //Closed_Bank_Account_No = "";
            //xID = "";
            //xMID = "";

            Open_Cash_Bal = 0;
            Close_Cash_Bal = 0;

            Open_Bank_Bal = 0;
            //Open_Bank_Bal01 = 0;
            //Open_Bank_Bal02 = 0;
            //Open_Bank_Bal03 = 0;
            //Open_Bank_Bal04 = 0;
            //Open_Bank_Bal05 = 0;
            //Open_Bank_Bal06 = 0;
            //Open_Bank_Bal07 = 0;
            //Open_Bank_Bal08 = 0;
            //Open_Bank_Bal09 = 0;
            //Open_Bank_Bal10 = 0;

            Close_Bank_Bal = 0;
            //Close_Bank_Bal01 = 0;
            //Close_Bank_Bal02 = 0;
            //Close_Bank_Bal03 = 0;
            //Close_Bank_Bal04 = 0;
            //Close_Bank_Bal05 = 0;
            //Close_Bank_Bal06 = 0;
            //Close_Bank_Bal07 = 0;
            //Close_Bank_Bal08 = 0;
            //Close_Bank_Bal09 = 0;
            //Close_Bank_Bal10 = 0;

            FType = "";
            Advanced_Filter_Category = "";
            Advanced_Filter_RefID = "";
            ActiveFilterString = "";

            Summary_Column_Status = false;
            Negative_MsgStr = "";

            //REC_BANK01_Field = "";
            //REC_BANK02_Field = "";
            //REC_BANK03_Field = "";
            //REC_BANK04_Field = "";
            //REC_BANK05_Field = "";
            //REC_BANK06_Field = "";
            //REC_BANK07_Field = "";
            //REC_BANK08_Field = "";
            //REC_BANK09_Field = "";
            //REC_BANK10_Field = "";

            //PAY_BANK01_Field = "";
            //PAY_BANK02_Field = "";
            //PAY_BANK03_Field = "";
            //PAY_BANK04_Field = "";
            //PAY_BANK05_Field = "";
            //PAY_BANK06_Field = "";
            //PAY_BANK07_Field = "";
            //PAY_BANK08_Field = "";
            //PAY_BANK09_Field = "";
            //PAY_BANK10_Field = "";

            //REC_BANK01_Caption = "";
            //REC_BANK02_Caption = "";
            //REC_BANK03_Caption = "";
            //REC_BANK04_Caption = "";
            //REC_BANK05_Caption = "";
            //REC_BANK06_Caption = "";
            //REC_BANK07_Caption = "";
            //REC_BANK08_Caption = "";
            //REC_BANK09_Caption = "";
            //REC_BANK10_Caption = "";

            //PAY_BANK01_Caption = "";
            //PAY_BANK02_Caption = "";
            //PAY_BANK03_Caption = "";
            //PAY_BANK04_Caption = "";
            //PAY_BANK05_Caption = "";
            //PAY_BANK06_Caption = "";
            //PAY_BANK07_Caption = "";
            //PAY_BANK08_Caption = "";
            //PAY_BANK09_Caption = "";
            //PAY_BANK10_Caption = "";

            //REC_BANK01_Tooltip = "";
            //REC_BANK02_Tooltip = "";
            //REC_BANK03_Tooltip = "";
            //REC_BANK04_Tooltip = "";
            //REC_BANK05_Tooltip = "";
            //REC_BANK06_Tooltip = "";
            //REC_BANK07_Tooltip = "";
            //REC_BANK08_Tooltip = "";
            //REC_BANK09_Tooltip = "";
            //REC_BANK10_Tooltip = "";

            //PAY_BANK01_Tooltip = "";
            //PAY_BANK02_Tooltip = "";
            //PAY_BANK03_Tooltip = "";
            //PAY_BANK04_Tooltip = "";
            //PAY_BANK05_Tooltip = "";
            //PAY_BANK06_Tooltip = "";
            //PAY_BANK07_Tooltip = "";
            //PAY_BANK08_Tooltip = "";
            //PAY_BANK09_Tooltip = "";
            //PAY_BANK10_Tooltip = "";

            //REC_BANK01_Visible = false;
            //REC_BANK02_Visible = false;
            //REC_BANK03_Visible = false;
            //REC_BANK04_Visible = false;
            //REC_BANK05_Visible = false;
            //REC_BANK06_Visible = false;
            //REC_BANK07_Visible = false;
            //REC_BANK08_Visible = false;
            //REC_BANK09_Visible = false;
            //REC_BANK10_Visible = false;

            //PAY_BANK01_Visible = false;
            //PAY_BANK02_Visible = false;
            //PAY_BANK03_Visible = false;
            //PAY_BANK04_Visible = false;
            //PAY_BANK05_Visible = false;
            //PAY_BANK06_Visible = false;
            //PAY_BANK07_Visible = false;
            //PAY_BANK08_Visible = false;
            //PAY_BANK09_Visible = false;
            //PAY_BANK10_Visible = false;

            //REC_BANK01_Remove = false;
            //REC_BANK02_Remove = false;
            //REC_BANK03_Remove = false;
            //REC_BANK04_Remove = false;
            //REC_BANK05_Remove = false;
            //REC_BANK06_Remove = false;
            //REC_BANK07_Remove = false;
            //REC_BANK08_Remove = false;
            //REC_BANK09_Remove = false;
            //REC_BANK10_Remove = false;

            //PAY_BANK01_Remove = false;
            //PAY_BANK02_Remove = false;
            //PAY_BANK03_Remove = false;
            //PAY_BANK04_Remove = false;
            //PAY_BANK05_Remove = false;
            //PAY_BANK06_Remove = false;
            //PAY_BANK07_Remove = false;
            //PAY_BANK08_Remove = false;
            //PAY_BANK09_Remove = false;
            //PAY_BANK10_Remove = false;

            //REC_BANK01_Tag = "";
            //REC_BANK02_Tag = "";
            //REC_BANK03_Tag = "";
            //REC_BANK04_Tag = "";
            //REC_BANK05_Tag = "";
            //REC_BANK06_Tag = "";
            //REC_BANK07_Tag = "";
            //REC_BANK08_Tag = "";
            //REC_BANK09_Tag = "";
            //REC_BANK10_Tag = "";

            //PAY_BANK01_Tag = "";
            //PAY_BANK02_Tag = "";
            //PAY_BANK03_Tag = "";
            //PAY_BANK04_Tag = "";
            //PAY_BANK05_Tag = "";
            //PAY_BANK06_Tag = "";
            //PAY_BANK07_Tag = "";
            //PAY_BANK08_Tag = "";
            //PAY_BANK09_Tag = "";
            //PAY_BANK10_Tag = "";

            //REC_BANK01_CC = "";
            //REC_BANK02_CC = "";
            //REC_BANK03_CC = "";
            //REC_BANK04_CC = "";
            //REC_BANK05_CC = "";
            //REC_BANK06_CC = "";
            //REC_BANK07_CC = "";
            //REC_BANK08_CC = "";
            //REC_BANK09_CC = "";
            //REC_BANK10_CC = "";

            //PAY_BANK01_CC = "";
            //PAY_BANK02_CC = "";
            //PAY_BANK03_CC = "";
            //PAY_BANK04_CC = "";
            //PAY_BANK05_CC = "";
            //PAY_BANK06_CC = "";
            //PAY_BANK07_CC = "";
            //PAY_BANK08_CC = "";
            //PAY_BANK09_CC = "";
            //PAY_BANK10_CC = "";

            //iTR_REC_BANK_Visible = false;
            //iTR_PAY_BANK_Visible = false;
            iRef_no_Visible = false;

            iTR_REC_JOURNAL_Remove = true;// false;
            iTR_REC_TOTAL_Remove = true;// false;
            iTR_PAY_JOURNAL_Remove = false;
            iTR_PAY_TOTAL_Remove = false;

            HideBank_Text = "";
            BE_Cash_Bank_Text = "";
        }
        public ActionResult Frm_PrintVoucher_CollectionBox(string RecID)
        {
            Return_Json_Param jsonParam = new Return_Json_Param();
            try
            {
                ViewBag.IsMobile = Request.Browser.IsMobileDevice;
                DataTable dtTransaction = BASE._Reports_Common_DBOps.GetCollectionBoxTransactionList(RecID, ClientScreen.Report_Collection_Box_Voucher);
                if (dtTransaction == null)
                {
                    jsonParam.message = Messages.SomeError;
                    jsonParam.title = "Error!!";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                if (dtTransaction.Rows.Count < 1)
                {
                    jsonParam.title = "Invalid Record ID";
                    jsonParam.message = "Please select proper Collection Box entry";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                DataTable dtItem = (DataTable)BASE._Reports_Common_DBOps.GetItemsAndLedger(ClientScreen.Report_Collection_Box_Voucher, "COLLECTION BOX");

                if (dtItem.Rows.Count < 1)
                {
                    jsonParam.title = "Invalid Item Information";
                    jsonParam.message = "There are no entries in item info. Please contact administrator.";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                DataTable dtAddress = BASE._Reports_Common_DBOps.GetAddressList(ClientScreen.Report_Collection_Box_Voucher, "'" + dtTransaction.Rows[0]["TR_AB_ID_1"].ToString() + "' , '" + dtTransaction.Rows[0]["TR_AB_ID_2"].ToString() + "'");

                if (dtAddress.Rows.Count < 1)
                {
                    jsonParam.title = "Invalid Address Information";
                    jsonParam.message = "There are no entries in Address book for this item. Please contact administrator.";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                DataTable dtCentreInfo = BASE._Reports_Common_DBOps.GetCentreDetails(BASE._open_Cen_ID, ClientScreen.Report_Collection_Box_Voucher);
                var buildData = from Tr in dtTransaction.AsEnumerable()
                                join A1 in dtAddress.AsEnumerable() on Tr.Field<string>("TR_AB_ID_1") equals A1.Field<string>("rec_id")
                                join A2 in dtAddress.AsEnumerable() on Tr.Field<string>("TR_AB_ID_2") equals A2.Field<string>("rec_id")
                                select new ReportDataObjects.CollectionBoxVoucherReport
                                {
                                    Person1_Name = A1.Field<string>("name"),
                                    Person2_Name = A2.Field<string>("name"),
                                    Total_Amount = Convert.ToDouble(Tr.Field<decimal>("Amt")),
                                    DateOf_CollectionBox = Tr.Field<string>("tr_date"),
                                    Voucher_No = string.IsNullOrEmpty(Tr.Field<string>("TR_VNO")) ? " " : Tr.Field<string>("TR_VNO"),
                                    Centre_Name = dtCentreInfo.Rows[0]["CEN_NAME"].ToString(),
                                    Centre_UIDNo = dtCentreInfo.Rows[0]["CEN_UID"].ToString(),
                                    Zone_Name = dtCentreInfo.Rows[0]["CEN_ZONE_ID"].ToString(),
                                    Item_No = dtItem.Rows[0]["Item"].ToString(),
                                    Account_Head = dtItem.Rows[0]["Head"].ToString(),
                                    Ins_Name = BASE._open_Ins_Name,
                                    Amount_InWord = BASE.ConvertNumToAlphaValue(Tr.Field<decimal>("Amt")).ToString().ToTitleCase(),
                                    narration = Tr.Field<string>("TR_NARRATION")
                                };
                var Final_Data = buildData.SingleOrDefault();
                if (Final_Data == null)
                {
                    jsonParam.title = "Voucher cannot be generated";
                    jsonParam.message = "Sorry! Voucher cannot be generated.  Please contact administrator to solve the issue.";
                    jsonParam.result = false;
                    return Json(new
                    {
                        jsonParam
                    }, JsonRequestBehavior.AllowGet);
                }
                var report = new CBVoucher(RecID, BASE, Final_Data);
                return View(report);
            }
            catch (Exception ex) 
            {
                jsonParam.message = string.Format("<b>Message:</b> {0}<br /><br />", ex.Message);
                jsonParam.title = "Error!!";
                jsonParam.result = false;
                return Json(new
                {
                    jsonParam
                }, JsonRequestBehavior.AllowGet);
            }           
        }
        public void SessionClear()
        {
            ClearBaseSession("_CB");
            //foreach (var item in BASE._SessionDictionary.Where(x => x.Key.EndsWith("_CB")).ToList())
            //{
            //    BASE._SessionDictionary.Remove(item.Key);
            //}
            Session.Remove("CashBookNestedGrid");
        }
        public void CashBook_Add_User_Rights()
        {
            ViewData["CashBook_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_CashBook, "Add");
            ViewData["Acc_Vou_CashBank_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Add");
            ViewData["Acc_Vou_B2B_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "Add");
            ViewData["Acc_Vou_Payment_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Add");
            ViewData["Acc_Vou_Receipt_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Receipt, "Add");
            ViewData["Acc_Vou_Donation_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Donation, "Add");
            ViewData["Acc_Vou_Gift_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Gift, "Add");
            ViewData["Acc_Vou_Int_Transfer_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Internal_Transfer, "Add");
            ViewData["Acc_Vou_ColBox_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CollectionBox, "Add");
            ViewData["Acc_Vou_FD_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_FD, "Add");
            ViewData["Acc_Vou_SaleAsset_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_SaleOfAsset, "Add");
            ViewData["Acc_Vou_Journal_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Journal, "Add");
            ViewData["Acc_Vou_AsetTransfer_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_AssetTransfer, "Add");
            ViewData["Acc_Vou_WIP_Finalization_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_WIP_Finalization, "Add");

            //for Membership, Membership_Renewal, Membership_Conversion----Screen Name will be added when they are created
            ViewData["Acc_Vou_CashBank_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Add");
            ViewData["Acc_Vou_CashBank_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Add");
            ViewData["Acc_Vou_CashBank_AddRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Add");
            ViewData["Help_Attachments_AddRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Add");
        }
        public void CashBook_Other_User_Rights()
        {
            ViewData["CashBook_ExportRight"] = CheckRights(BASE, ClientScreen.Accounts_CashBook, "Export");

            ViewData["Acc_Vou_CashBank_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Update");
            ViewData["Acc_Vou_CashBank_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "View");
            ViewData["Acc_Vou_CashBank_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CashBank, "Delete");

            ViewData["Acc_Vou_B2B_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "Update");
            ViewData["Acc_Vou_B2B_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "View");
            ViewData["Acc_Vou_B2B_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_BankToBank, "Delete");

            ViewData["Acc_Vou_Payment_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Update");
            ViewData["Acc_Vou_Payment_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "View");
            ViewData["Acc_Vou_Payment_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Payment, "Delete");

            ViewData["Acc_Vou_Receipt_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Receipt, "Update");
            ViewData["Acc_Vou_Receipt_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Receipt, "View");
            ViewData["Acc_Vou_Receipt_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Receipt, "Delete");

            ViewData["Acc_Vou_Donation_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Donation, "Update");
            ViewData["Acc_Vou_Donation_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Donation, "View");
            ViewData["Acc_Vou_Donation_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Donation, "Delete");

            ViewData["Acc_Vou_Gift_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Gift, "Update");
            ViewData["Acc_Vou_Gift_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Gift, "View");
            ViewData["Acc_Vou_Gift_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Gift, "Delete");

            ViewData["Acc_Vou_Int_Transfer_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Internal_Transfer, "Update");
            ViewData["Acc_Vou_Int_Transfer_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Internal_Transfer, "View");
            ViewData["Acc_Vou_Int_Transfer_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Internal_Transfer, "Delete");

            ViewData["Acc_Vou_ColBox_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CollectionBox, "Update");
            ViewData["Acc_Vou_ColBox_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CollectionBox, "View");
            ViewData["Acc_Vou_ColBox_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_CollectionBox, "Delete");

            ViewData["Acc_Vou_FD_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_FD, "Update");
            ViewData["Acc_Vou_FD_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_FD, "View");
            ViewData["Acc_Vou_FD_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_FD, "Delete");

            ViewData["Acc_Vou_SaleAsset_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_SaleOfAsset, "Update");
            ViewData["Acc_Vou_SaleAsset_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_SaleOfAsset, "View");
            ViewData["Acc_Vou_SaleAsset_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_SaleOfAsset, "Delete");

            ViewData["Acc_Vou_Journal_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Journal, "Update");
            ViewData["Acc_Vou_Journal_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Journal, "View");
            ViewData["Acc_Vou_Journal_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_Journal, "Delete");

            ViewData["Acc_Vou_AsetTransfer_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_AssetTransfer, "Update");
            ViewData["Acc_Vou_AsetTransfer_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_AssetTransfer, "View");
            ViewData["Acc_Vou_AsetTransfer_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_AssetTransfer, "Delete");

            ViewData["Acc_Vou_WIP_Finalization_UpdateRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_WIP_Finalization, "Update");
            ViewData["Acc_Vou_WIP_Finalization_ViewRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_WIP_Finalization, "View");
            ViewData["Acc_Vou_WIP_Finalization_DeleteRight"] = CheckRights(BASE, ClientScreen.Accounts_Voucher_WIP_Finalization, "Delete");

            //for Membership, Membership_Renewal, Membership_Conversion----Screen Name will be added when they are created
            ViewData["Help_Attachments_DeleteRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Delete");
            ViewData["Help_Attachments_ViewRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "View");
            ViewData["Help_Attachments_UpdateRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "Update");
            ViewData["Help_Attachments_ListRight"] = CheckRights(BASE, ClientScreen.Help_Attachments, "List");

            ViewData["CB_Auto_Vouching_Mode"] = (BASE._IsUnderAudit && ((BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.SuperUser.ToUpper())
                        || (BASE._open_User_Type.ToUpper() == Common_Lib.Common.ClientUserType.Auditor.ToUpper()))) ? true : false;
        }
    }
}