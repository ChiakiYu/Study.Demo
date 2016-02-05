using System.Web;
using System.Web.Optimization;

namespace ChiakiYu
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.Add(
               new StyleBundle("~/Bundles/Assets/css")
                   .Include("~/Assets/global/plugins/bootstrap/css/bootstrap.css", new CssRewriteUrlTransform())
                   .Include("~/Assets/global/plugins/font-awesome/css/font-awesome.css", new CssRewriteUrlTransform())
                   .Include("~/Assets/global/css/components.min.css", new CssRewriteUrlTransform())
                   .Include("~/Assets/global/css/plugins.min.css", new CssRewriteUrlTransform())
                   .Include("~/Assets/pages/css/coming-soon.min.css", new CssRewriteUrlTransform())
                   );

            bundles.Add(
               new ScriptBundle("~/Bundles/Assets/js")
               .Include("~/Assets/global/plugins/jquery.min.js", 
               "~/Assets/global/plugins/bootstrap/js/bootstrap.min.js",
               "~/Assets/global/plugins/jquery.blockui.min.js",
               "~/Assets/global/plugins/countdown/jquery.countdown.min.js",
               "~/Assets/global/plugins/countdown/plugin/jquery.countdown-zh-CN.js",
               "~/Assets/global/plugins/backstretch/jquery.backstretch.min.js",
               "~/Assets/pages/scripts/coming-soon.min.js"
               )

                   );
        }
    }
}
