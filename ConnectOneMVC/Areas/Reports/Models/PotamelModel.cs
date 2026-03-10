using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static Common_Lib.DbOperations.Report_Ledgers;

namespace ConnectOneMVC.Areas.Reports.Models
{
    
    [Serializable]
    public class Potamel
    {      
        public string Receipts { get; set; }
      
        public decimal? Amount_Receipts { get; set; }
    
        public decimal? Total_Receipts { get; set; }

        public string Payments { get; set; }

        public decimal? Amount_Payments { get; set; }

        public decimal? Total_Payments { get; set; }



    }
    public class PartyLedgerPrintableReport 
    {
        public string Name { get; set; }
        public string PAN { get; set; }
        public string GST { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string CityState { get; set; }
        public string MobileEmail { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string PartyID { get; set; }
        public List<PartyLedgerReport> Data { get; set; }

    }
    public class PrintableReportColumnCollection
    {
        public PrintableReportColumnCollection(string _Name,string _Caption,int _Width,string _Align="Left") 
        {
            Name = _Name;
            Caption = _Caption;
            Width = _Width;
            Align = _Align;
        }
        public string Name { get; set; }      
        public string Caption { get; set; }      
        public int Width { get; set; }     
        public string Align { get; set; }     

    }

}