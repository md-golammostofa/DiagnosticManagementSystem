using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace MMSLHMS.App_Start
{
    public class BundleConfiq
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/Scripts/DataTable").Include(
                        "~/Scripts/DataTable/dataTables.jqueryui.js"));

            //bundles.Add(new StyleBundle("~/Content/DataTable").In)
        }
    }
}