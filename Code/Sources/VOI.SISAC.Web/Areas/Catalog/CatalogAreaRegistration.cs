using System.Web.Mvc;

namespace VOI.SISAC.Web.Areas.Catalog
{
    public class CatalogAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Catalog";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Catalog_default",
                "Catalog/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}