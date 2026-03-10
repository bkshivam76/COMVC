using ConnectOneMVC.Areas.Profile.Models;
using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using DevExpress.Web;
using DevExpress.XtraReports.Security;
using ConnectOneMVC.Helper;
namespace ConnectOneMVC
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;


            DevExpress.XtraReports.Web.WebDocumentViewer.Native.WebDocumentViewerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Disabled;
            AreaRegistration.RegisterAllAreas();
            ViewEngines.Engines.Clear();
            IViewEngine razorEngine = new RazorViewEngine() { FileExtensions = new string[] { "cshtml" } };
            ViewEngines.Engines.Add(razorEngine);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //const string postServerItemAccessToken = "4df70c84b3364dada471f6eaed7149e5";
            //Rollbar.RollbarLocator.RollbarInstance.
            //Configure(new Rollbar.RollbarConfig(postServerItemAccessToken)
            //{
            //    Environment = "local"
            //});
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DevExtremeBundleConfig.RegisterBundles(BundleTable.Bundles);
            ScriptPermissionManager.GlobalInstance = new ScriptPermissionManager(ExecutionMode.Unrestricted);
            ModelBinders.Binders.DefaultBinder = new DevExpress.Web.Mvc.DevExpressEditorsBinder();

            DevExpress.Web.ASPxWebControl.CallbackError += Application_Error;
            //date formate set
            CultureInfo newCulture = new CultureInfo("", true);
            newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            newCulture.DateTimeFormat.FullDateTimePattern = "dd-MM-yyyy HH:mm:ss";
            newCulture.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            ModelBinders.Binders.Add(typeof(DateTime?), new CustomDateModelBinderNull());
            ModelBinders.Binders.Add(typeof(DateTime), new CustomDateModelBinder());
            MvcHandler.DisableMvcResponseHeader = true;

            DevExpress.XtraReports.Web.ReportDesigner.Native.ReportDesignerBootstrapper.SessionState = System.Web.SessionState.SessionStateBehavior.Required;
            DevExpress.XtraReports.Web.ASPxReportDesigner.StaticInitialize();
            DevExpress.XtraReports.Expressions.CustomFunctions.Register(new FormatIndianNumber());
            DevExpress.XtraReports.Expressions.CustomFunctions.Register(new FormatDateString());
            ASPxWebControl.BackwardCompatibility.DataControlAllowReadUnlistedFieldsFromClientApiDefaultValue = true;
            DevExtreme.AspNet.Mvc.Compatibility.Rendering.UseJQueryReady = true;
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = System.Web.HttpContext.Current.Server.GetLastError();
            //TODO: Handle Exception
        }
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            DevExpressHelper.Theme = "MetropolisBlue";
        }


    }
    
    public class CustomDateModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value != null)
            {
                DateTime date;
                if (DateTime.TryParseExact(value.AttemptedValue, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    return DateTime.Now;
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
    public class CustomDateModelBinderNull : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var displayFormat = bindingContext.ModelMetadata.DisplayFormatString;
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if ( value != null)
            {
                DateTime date;
                if (DateTime.TryParseExact(value.AttemptedValue, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                {
                    bindingContext.ModelState.AddModelError(
                        bindingContext.ModelName,
                        string.Format("{0} is an invalid date format", value.AttemptedValue)
                    );
                }
            }

            return base.BindModel(controllerContext, bindingContext);
        }
    }
    public class AllowHtmlBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext,
            ModelBindingContext bindingContext)
        {
            var request = controllerContext.HttpContext.Request;
            var name = bindingContext.ModelName;
            return request.Unvalidated[name]; //magic happens here
        }
    }
}