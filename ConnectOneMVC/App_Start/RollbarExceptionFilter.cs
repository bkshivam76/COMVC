using Rollbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ConnectOneMVC.App_Start
{
    public class RollbarExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
            {
                return;
            }

            RollbarLocator.RollbarInstance.Error(filterContext.Exception);
        }
    }
}