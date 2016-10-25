using System.Web.Mvc;

namespace VOI.SISAC.Web.Areas.Process
{
    public class ProcessAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Process";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Process_default",
                "Process/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}