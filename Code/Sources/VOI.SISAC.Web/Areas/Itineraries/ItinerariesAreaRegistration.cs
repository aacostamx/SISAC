using System.Web.Mvc;

namespace VOI.SISAC.Web.Areas.Itineraries
{
    public class ItinerariesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Itineraries";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Itineraries_default",
                "Itineraries/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}