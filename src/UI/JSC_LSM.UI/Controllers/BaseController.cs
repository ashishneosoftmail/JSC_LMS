using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace JSC_LSM.UI.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if (HttpContext.Session.GetString("Email") == null || HttpContext.Session.GetString("Email") == "")
            //{
            //    filterContext.Result = new RedirectToRouteResult(new
            //           RouteValueDictionary(new { controller = "Login", action = "Login" }));
            //}

            if (Request.Cookies["Email"] == null || Convert.ToString(Request.Cookies["Email"]) == "")
            {
                filterContext.Result = new RedirectToRouteResult(new
                         RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
        }
    }
}
