using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Areas.Stock.Models
{
    [Serializable]
    public class UnitCoversion
    {
        public int SrNo;
        public string Converted_Unit;
        public decimal Rate_Of_Conversion;
        public DateTime Effective_Date;
        public UnitCoversion()
        {

        }
    }
}