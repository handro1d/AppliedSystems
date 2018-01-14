using System.Web.Mvc;

namespace AppliedSystems.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleAppliedSystemsErrorAttribute("AppliedSystems.Web"));
            filters.Add(new AuthorizeAttribute());
        }
    }
}
