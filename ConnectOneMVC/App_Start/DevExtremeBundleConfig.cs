
using System.Web.Optimization;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConnectOneMVC
{
    public class DevExtremeBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            var scriptBundle = new ScriptBundle("~/Scripts/DevExtremeBundle");
            var styleBundle = new StyleBundle("~/Content/DevExtremeBundle");

            // CLDR scripts
            scriptBundle
                .Include("~/Scripts/cldr.js")
                .Include("~/Scripts/cldr/event.js")
                .Include("~/Scripts/cldr/supplemental.js")
                .Include("~/Scripts/cldr/unresolved.js");

            // Globalize 1.x
            scriptBundle
                .Include("~/Scripts/globalize.js")
                .Include("~/Scripts/globalize/message.js")
                .Include("~/Scripts/globalize/number.js")
                .Include("~/Scripts/globalize/currency.js")
                .Include("~/Scripts/globalize/date.js");

            // NOTE: jQuery may already be included in the default script bundle. Check the BundleConfig.cs file​​​
            //scriptBundle
            //    .Include("~/Scripts/jquery-3.0.0.js");

            // JSZip for client side export
            //scriptBundle
            //    .Include("~/Scripts/jszip.js");

            // DevExtreme + extensions
            scriptBundle
                .Include("~/Scripts/dx.all.js")
                .Include("~/Scripts/aspnet/dx.aspnet.data.js")
                .Include("~/Scripts/aspnet/dx.aspnet.mvc.js");

            // dxVectorMap data (http://js.devexpress.com/Documentation/Guide/Data_Visualization/VectorMap/Providing_Data/#Data_for_Areas)
            //scriptBundle
            //    .Include("~/Scripts/vectormap-data/africa.js")
            //    .Include("~/Scripts/vectormap-data/canada.js")
            //    .Include("~/Scripts/vectormap-data/eurasia.js")
            //    .Include("~/Scripts/vectormap-data/europe.js")
            //    .Include("~/Scripts/vectormap-data/usa.js")
            //    .Include("~/Scripts/vectormap-data/world.js");


            // DevExtreme
            // NOTE: see the available theme list here: http://js.devexpress.com/Documentation/Guide/Themes/Predefined_Themes/                    
            styleBundle
                .Include("~/Content/dx.common.css")
                .Include("~/Content/dx.light.css");


            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);


            var scriptQuillBundle = new ScriptBundle("~/Scripts/QuillBundle");
            var styleQuillBundle = new StyleBundle("~/Content/QuillBundle");
            scriptQuillBundle
       .Include("~/Scripts/quill/quill.js");
            styleQuillBundle
           .Include("~/Scripts/quill/quill.bubble.css")
           .Include("~/Scripts/quill/quill.core.css")
           .Include("~/Scripts/quill/quill.snow.css");
            bundles.Add(scriptQuillBundle);
            bundles.Add(styleQuillBundle);

            var scriptBundleReporting = new ScriptBundle("~/Scripts/DevExtremeBundleReporting");
            var styleBundleReporting = new StyleBundle("~/Content/DevExtremeBundleReporting");

            scriptBundleReporting            
               .Include("~/Scripts/aspnet/dx.aspnet.data.js")
               .Include("~/Scripts/aspnet/dx.aspnet.mvc.js");
      
            bundles.Add(scriptBundleReporting);
            bundles.Add(styleBundleReporting);

#if !DEBUG
            BundleTable.EnableOptimizations = true;
#endif
        }
    }
}