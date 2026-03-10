using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Operators;
using Org.BouncyCastle.Crypto.Engines;
//using Microsoft.Build.Utilities;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using System.Web.Configuration;

namespace ConnectOneMVC.Areas.Facility.Models
{      
    [Serializable]
    public class Transaction
    {
        public string TranId { get; set; }
        public decimal TranAmt { get; set; }
        public string TranType { get; set; }
        public string Narration { get; set; }
        public DateTime TranDate { get; set; }
        public decimal Balance { get; set; }
    }
}