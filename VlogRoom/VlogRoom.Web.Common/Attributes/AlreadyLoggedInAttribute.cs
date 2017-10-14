using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using VlogRoom.Web.Common.Constants;

namespace VlogRoom.Web.Common.Attributes
{
    public class AlreadyLoggedInAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Controller.TempData[GlobalConstants.SuccessMessage] = $"You are alredy logged in!";
                filterContext.HttpContext.Response.Redirect("/");
            }
        }
    }
}
