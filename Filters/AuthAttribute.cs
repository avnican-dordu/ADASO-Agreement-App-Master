using ADASO_AgreementApp.Helper;
using System.Web.Mvc;

namespace ADASO_AgreementApp.Filters
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var currentUser = UserHelper.GetCurrentUser();
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            var action = filterContext.ActionDescriptor.ActionName;

            if (currentUser == null && !(controller == "Login" && action == "Login"))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary {
                { "controller", "Login" },
                { "action", "Login" }
                    });
            }

            base.OnActionExecuting(filterContext);
        }

    }
}
