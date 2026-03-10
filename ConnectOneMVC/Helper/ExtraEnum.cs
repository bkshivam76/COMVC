using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC.Helper
{
    public enum PaymentType
    {
        Bank = 0,
        Cash = 1,
        Credit = 2
    }
    public enum ActionStatus
    {
        Open = 0,
        Closed = 1,
        ReOpened = 2,
        Verified = 3,
    }

}
