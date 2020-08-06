using Apresentacao.Entities;
using Apresentacao.Helpers.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Apresentacao.Helpers
{
    public class AutorizacaoSession : ActionFilterAttribute
    { 
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Login usuario = new Session().GetObject<Login>(State.LoginSession);
            if (usuario == null) 
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Login",
                            action = "Index"
                        }));
            }
        }
    }
}
