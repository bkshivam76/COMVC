using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.OpenSsl;

using System.Threading.Tasks;
using ConnectOneMVC.Areas.Account.Models;
using System.Net.Http;
using ConnectOneMVC.Models;
using System.Net;

using ConnectOneMVC.Controllers;
using System.Data;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Utilities;
using System.Windows.Forms;

namespace ConnectOneMVC.Areas.Facility.Models
{
    public class BankEnquiriesController : BaseController
    {     
        public ActionResult BalanceAndAccountStatementEnquiry()
        {
            return View();
        }
        public ActionResult BalanceAndAccountStatement()
        {
            return View();
        }    
    }
}