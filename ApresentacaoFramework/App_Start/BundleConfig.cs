using System.Web;
using System.Web.Optimization;

namespace Apresentacao
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            ScriptBundle scripts = new ScriptBundle("~/bundles/scripts");
            scripts.Include("~/Scripts/jquery-3.5.1.js");
            scripts.Include("~/Scripts/jquery.validate.js");
            scripts.Include("~/Scripts/modernizr-2.8.3.js");
            scripts.Include("~/Scripts/bootstrap.js");

            bundles.Add(scripts);

            StyleBundle estilos = new StyleBundle("~/bundles/estilos");
            estilos.Include("~/Content/bootstrap.css");
            estilos.Include("~/Content/Site.css");
            estilos.Include("~/Content/bootstrap*");

            bundles.Add(estilos);            
        }
    }
}
